using System;
using System.IO;
using System.Net;

namespace Octanification.Server
{

	class StaticsService {
		private readonly string resourcesPath;

		internal StaticsService(string resourcesPath) {
			this.resourcesPath = resourcesPath;
		}

		internal async void serveStatic(HttpListenerContext context) {
			string filePath = Path.Combine(resourcesPath, context.Request.RawUrl.Substring(8));
			if (File.Exists(filePath)) {
				using (FileStream fs = File.OpenRead(filePath)) {
					Console.WriteLine("starting serving the static file " + filePath);
					try {
						await fs.CopyToAsync(context.Response.OutputStream);
					} catch (Exception e) {
						Console.WriteLine(e.Message);
					} finally {
						context.Response.Close();
						Console.WriteLine("finished serving the static file " + filePath);
					}
				}
			} else {
				context.Response.StatusCode = 404;
				context.Response.Close();
			}
		}
	}
}
