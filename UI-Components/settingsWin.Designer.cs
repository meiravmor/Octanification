﻿namespace Octanification.UI_Components
{
    partial class SettingsWin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.lboxUsers = new System.Windows.Forms.ListBox();
            this.lboxUsersNot = new System.Windows.Forms.ListBox();
            this.lblUsers = new System.Windows.Forms.Label();
            this.lblGetNotifications = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(557, 359);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 19);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lboxUsers
            // 
            this.lboxUsers.FormattingEnabled = true;
            this.lboxUsers.Location = new System.Drawing.Point(35, 58);
            this.lboxUsers.Margin = new System.Windows.Forms.Padding(2);
            this.lboxUsers.Name = "lboxUsers";
            this.lboxUsers.Size = new System.Drawing.Size(179, 316);
            this.lboxUsers.TabIndex = 1;
            this.lboxUsers.SelectedIndexChanged += new System.EventHandler(this.usersList_SelectedIndexChanged);
            this.lboxUsers.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lboxUsers_MouseDown);
            // 
            // lboxUsersNot
            // 
            this.lboxUsersNot.AllowDrop = true;
            this.lboxUsersNot.FormattingEnabled = true;
            this.lboxUsersNot.Location = new System.Drawing.Point(267, 58);
            this.lboxUsersNot.Name = "lboxUsersNot";
            this.lboxUsersNot.Size = new System.Drawing.Size(185, 316);
            this.lboxUsersNot.TabIndex = 2;
            this.lboxUsersNot.DragDrop += new System.Windows.Forms.DragEventHandler(this.lboxUsersNot_DragDrop);
            this.lboxUsersNot.DragOver += new System.Windows.Forms.DragEventHandler(this.lboxUsersNot_DragOver);
            // 
            // lblUsers
            // 
            this.lblUsers.AutoSize = true;
            this.lblUsers.BackColor = System.Drawing.Color.Transparent;
            this.lblUsers.ForeColor = System.Drawing.Color.White;
            this.lblUsers.Location = new System.Drawing.Point(35, 40);
            this.lblUsers.Name = "lblUsers";
            this.lblUsers.Size = new System.Drawing.Size(110, 13);
            this.lblUsers.TabIndex = 3;
            this.lblUsers.Text = "All uers in workspace:";
            // 
            // lblGetNotifications
            // 
            this.lblGetNotifications.AutoSize = true;
            this.lblGetNotifications.BackColor = System.Drawing.Color.Transparent;
            this.lblGetNotifications.ForeColor = System.Drawing.Color.White;
            this.lblGetNotifications.Location = new System.Drawing.Point(267, 39);
            this.lblGetNotifications.Name = "lblGetNotifications";
            this.lblGetNotifications.Size = new System.Drawing.Size(109, 13);
            this.lblGetNotifications.TabIndex = 4;
            this.lblGetNotifications.Text = "Get notifications from:";
            // 
            // SettingsWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.BackgroundImage = global::Octanification.Properties.Resources.hpe_logo;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(669, 431);
            this.Controls.Add(this.lblGetNotifications);
            this.Controls.Add(this.lblUsers);
            this.Controls.Add(this.lboxUsersNot);
            this.Controls.Add(this.lboxUsers);
            this.Controls.Add(this.button1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsWin";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox lboxUsers;
        private System.Windows.Forms.ListBox lboxUsersNot;
        private System.Windows.Forms.Label lblUsers;
        private System.Windows.Forms.Label lblGetNotifications;
    }
}