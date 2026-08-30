using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace iPhoneBypasserUI
{
    public partial class Form1 : Form
    {
        public readonly IConfiguration Configuration;

        public Form1()
        {
            InitializeComponent();
            this.CreateHandle();
            Configuration = Program.Services!.GetRequiredService<IConfiguration>();
            ChangeToken.OnChange(() => Configuration.GetReloadToken(), OnChange);
            OnChange();
            InitializeTreeView();
        }

        private void OnChange()
        {
            this.Invoke((MethodInvoker)delegate { this.Text = Configuration.GetSection("Settings:Subkey1:Value1").Get<string>(); });
        }

        private void InitializeTreeView()
        {
            treeView1.Nodes.Clear();
            var androidNode = treeView1.Nodes.Add("Android");
            androidNode.Nodes.Add("FRP Bypass");
            androidNode.Nodes.Add("Screen Unlock");
            androidNode.Nodes.Add("Firmware Flash");
            androidNode.Nodes.Add("Bootloader Unlock");

            var iosNode = treeView1.Nodes.Add("iPhone");
            iosNode.Nodes.Add("iCloud Bypass");
            iosNode.Nodes.Add("Screen Unlock");
            iosNode.Nodes.Add("Firmware Flash");
            iosNode.Nodes.Add("Baseband Repair");

            treeView1.ExpandAll();
        }

        private void toolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButtonConnect_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Connected";
            toolStripStatusLabel1.ForeColor = Color.Green;
        }

        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Refreshing...";
            // Simulate refresh
            System.Threading.Thread.Sleep(500);
            toolStripStatusLabel1.Text = "Ready";
        }

        private void toolStripButtonLog_Click(object sender, EventArgs e)
        {
            // Show a simple log form or just a message box for now
            MessageBox.Show("Log feature not implemented in this demo.", "Log", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}