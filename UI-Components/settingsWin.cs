using Newtonsoft.Json.Linq;
using Octanification.Entities;
using Octanification.restClient;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Forms;

namespace Octanification.UI_Components
{
    public partial class SettingsWin : Form
    {
        RestClient client;
        List<User> users = new List<User>();

        public SettingsWin(RestClient clientReceived)
        {
            InitializeComponent();
            client = clientReceived;
            getSettingsparms();
        }

        private async void getSettingsparms()
        {
            getWorkspaceUsers();
            getBusinessRuleEntities();
        }

        private async void getBusinessRuleEntities()
        {
            List<string> response = await client.getEntities();
        }

        private async void getWorkspaceUsers()
        {
            List<Dictionary<string, string>> response = await client.getWorkspaceUsers();
            lboxUsers.DisplayMember = "fullName";
            foreach (Dictionary<string, string> user in response)
            {
                string email;
                string uuid;
                string name;
                user.TryGetValue("full_name", out name);
                user.TryGetValue("uid", out uuid);
                user.TryGetValue("email", out email);
                User newUser = new User(email, uuid, name);
                users.Add(newUser);
            }
            foreach (User user in users)
            {
                lboxUsers.Items.Add(user);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string stop = "Stop";

        }

        private void usersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            User asdasd = lboxUsers.SelectedItem as User;
            string sdsd = "sdf";
        }

        private void lboxUsers_MouseDown(object sender, MouseEventArgs e)
        {
            if (lboxUsers.Items.Count == 0)
                return;
            User s = lboxUsers.Items[lboxUsers.IndexFromPoint(e.X, e.Y)] as User;
            DragDropEffects dde1 = DoDragDrop(s,
                DragDropEffects.All);

            if (dde1 == DragDropEffects.All)
            {
                lboxUsers.Items.RemoveAt(lboxUsers.IndexFromPoint(e.X, e.Y));
            }
        }

        private void lboxUsersNot_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data != null)
            {
                User s = (User)e.Data.GetData(typeof(User));
                lboxUsersNot.DisplayMember = "fullName";
                lboxUsersNot.Items.Add(s);
            }
        }

        private void lboxUsersNot_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }
    }
}
