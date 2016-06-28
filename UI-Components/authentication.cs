using Octanification.Interfaces;
using Octanification.restClient;
using Octanification.Server;
using Octanification.UI_Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Octanification
{
    public partial class frmAuthentication : Form
    {
        RestClient client;
        int sec = 0;

        public frmAuthentication()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (message.Visible)
            {
                timer1.Stop();
                sec = 0;
                faildState(true, true, true, true, true);
                message.Visible = false;
            }
            else
            {
                this.Close();
            }
        }



        private async void loginToServer()
        {
            client = new RestClient(serverAddress.Text);
            HttpResponseMessage response = await client.login(username.Text, password.Text);
            if (response.StatusCode.ToString().Equals("OK"))
            {
                this.Hide();
                SettingsWin settingsWin = new SettingsWin(client);
                settingsWin.ShowDialog();
            }
            else
            {
                timer1.Stop();
                MessageBox.Show("The details you’ve entered are incorrect.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                faildState(true, true, true, true, true);
                message.Visible = false;
            }
        }

        private Boolean isValideArgs()
        {
            Boolean valid = true;
            if (String.IsNullOrEmpty(serverAddress.Text) || String.IsNullOrEmpty(username.Text) || String.IsNullOrEmpty(password.Text)) valid = false;
            return valid;
        }

        private void faildState(Boolean url, Boolean user, Boolean pass, Boolean connection, Boolean close)
        {
            serverAddress.Enabled = url;
            username.Enabled = user;
            password.Enabled = pass;
            btnConnect.Enabled = connection;
            btnCancel.Enabled = close;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            message.Text = "Connecting ... (" + sec + ")";

            sec = sec + 1;

            if (sec == 120)
            {
                timer1.Stop();
                sec = 0;
                MessageBox.Show("Enter Material Name Please.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                faildState(true, true, true, true, true);
                message.Visible = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void start_Click(object sender, EventArgs e)
        {
            IServer server = new ServerListener(5050, "C:\\Users\\gullery\\Documents\\Visual Studio 2010\\Projects\\WebServer\\site_root");
            server.StartServer();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!isValideArgs())
            {
                MessageBox.Show("Enter Material Name Please.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                loginToServer();
                faildState(false, false, false, false, true);
                message.Visible = true;
                timer1.Interval = 1000;
                timer1.Start();
            }
        }

        private void username_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
