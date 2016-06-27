using Octanification.restClient;
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

namespace Octanification.UI_Components
{
    public partial class SettingsWin : Form
    {
        RestClient client;

        public SettingsWin(RestClient clientReceived)
        {
            InitializeComponent();
            client = clientReceived;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            HttpResponseMessage response = await client.get("api/shared_spaces/1001/workspaces/2009/defects/2279");
            if(response.StatusCode.ToString().Equals("OK"))
            {
                string defect = response.Content.ReadAsStringAsync().Result;
                string stop = "STOP";
            }
        }

        private void usersList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
