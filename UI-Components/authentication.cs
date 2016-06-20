using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoApp
{
    public partial class authentication : Form
    {
        int sec = 0;

        public authentication()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!isValideArgs())
            {
                MessageBox.Show("Enter Material Name Please.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                faildState(false, false, false, false, true);
                message.Visible = true;
                timer1.Interval = 1000;
                timer1.Start();
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
            connect.Enabled = connection;
            cancel.Enabled = close;
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
    }
}
