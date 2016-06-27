using Octanification.Interfaces;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Octanification.Server
{

	public class ServerListener : IServer
    {
		private readonly int port;
		private readonly string resourcesPath;
		private bool shuttingDown;

		private Thread mainThread;
		private HttpListener listener;

		private readonly RESTsService restsService;
		private readonly StaticsService staticsService;

		public ServerListener(int port, string resourcesPath) {
			this.port = port;
			this.resourcesPath = resourcesPath;
			this.shuttingDown = false;

			this.restsService = new RESTsService();
			this.staticsService = new StaticsService(resourcesPath);
		}

        public void StartServer()
        {
            Start();
        }

        private string resolveAddress()
        {
            return Dns.GetHostEntry("localhost").HostName;
        }

        #region Public APIs
        public void Start() {
            string hostname = resolveAddress();

            if (mainThread == null || mainThread.ThreadState != ThreadState.Running) {
				mainThread = new Thread(startService);
				mainThread.Name = "Server Main Thread";
				mainThread.Start();
				Console.WriteLine("Server started on thread {0} and listening on {1}:{2}", mainThread.ManagedThreadId, hostname, port);
			} else {
				Console.WriteLine("Server may be started only once");
			}
		}

		public void Stop() {
			Console.WriteLine("Server shutting down... ");
			listener.Stop();
			shuttingDown = true;
		}
		#endregion

		#region Internals
		private void startService() {
            string hostname = resolveAddress();

            listener = new HttpListener();
			listener.Prefixes.Add("http://localhost:" + port + "/");
			listener.Start();

			//	main event loop
			while (!shuttingDown) {
				try {
					Console.WriteLine("Waiting for the next connection...");
					HttpListenerContext context = listener.GetContext();
					if (context.Request.RawUrl.IndexOf("/static") == 0) {
						staticsService.serveStatic(context);
					} else {
						restsService.serveREST(context);
					}
				} catch (HttpListenerException le) {
					if (le.ErrorCode == 995) Console.WriteLine("Done");
					else throw le;
				} catch (Exception e) {
					throw e;
				}
			}

			listener.Stop();
			listener.Close();
		}

		private async void serveStatic(string path, HttpListenerContext context) {
			string filePath = Path.Combine(resourcesPath, path.Substring(8));
			if (File.Exists(filePath)) {
				using (FileStream fs = File.OpenRead(filePath)) {
					Console.WriteLine("starting serving the static file " + path);
					try {
						await fs.CopyToAsync(context.Response.OutputStream);
					} catch (Exception e) {
						Console.WriteLine(e.Message);
					} finally {
						context.Response.Close();
						Console.WriteLine("finished serving the static file " + path);
					}
				}
			} else {
				context.Response.StatusCode = 404;
				context.Response.Close();
			}
		}


        #endregion
    }
}