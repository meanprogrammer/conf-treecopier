using ConfluenceAutomator.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TreeCopier.WinForms
{
    public partial class Form1 : Form, IFormLogger
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConfluenceContext.SaveCredentials("vd2", "Welcome4");

            ConfluenceSpaceTaskExecutor confSpaceService = new ConfluenceSpaceTaskExecutor(this);
            AllSpaces r = confSpaceService.Execute();
            this.MaincomboBox.ValueMember = Strings.KEY;
            this.MaincomboBox.DisplayMember = Strings.NAME;
            this.MaincomboBox.SelectedIndex = -1;
            this.MaincomboBox.DataSource = r.results;

            

            AllSpaces r2 = confSpaceService.Execute();

            this.TargetcomboBox.ValueMember = Strings.KEY;
            this.TargetcomboBox.DisplayMember = Strings.NAME;
            this.TargetcomboBox.SelectedIndex = -1;
            this.TargetcomboBox.DataSource = r2.results;


        }

        public void Log(string message)
        {
            //throw new NotImplementedException();
        }

        private void MaincomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormIsWorking(false);

            this.ConfluencetreeView.Nodes.Clear();
            if (!ConfluenceBackgroundWorker.IsBusy)
            {
                string key = string.Empty;
                var selectedItem = this.MaincomboBox.SelectedItem as Result;
                if (selectedItem != null)
                {
                    key = selectedItem.key;
                }
                ConfluenceBackgroundWorker.RunWorkerAsync(key);
            }
        }

        private void FormIsWorking(bool enabled)
        {
            //this.ParentSpaceComboBox.Enabled = enabled;
            //this.RunButton.Enabled = enabled;
            //this.CancelButton.Enabled = enabled;
            //this.CleanUpButton.Enabled = enabled;

            string status = string.Empty;
            status = ((enabled == false) ? "Work in progress." : "Work Complete.");
            this.Log(status);
        }

        private void ConfluenceBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            ConfluenceSpaceTaskExecutor confSpaceService = new ConfluenceSpaceTaskExecutor(this);
            e.Result = confSpaceService.CreateSpaceTreeNode(confSpaceService.Execute().results.Where(x => x.key == e.Argument.ToString()).FirstOrDefault());
        }

        private void ConfluenceBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.ConfluencetreeView.Nodes.Add(e.Result as TreeNode);
            //this.ConfluencetreeView.ExpandAll();
            this.ConfluencetreeView.Nodes[0].EnsureVisible();
            FormIsWorking(true);
        }

        private void ConfluenceBackgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            ConfluenceSpaceTaskExecutor confSpaceService = new ConfluenceSpaceTaskExecutor(this);
            e.Result = confSpaceService.CreateSpaceTreeNode(confSpaceService.Execute().results.Where(x => x.key == e.Argument.ToString()).FirstOrDefault());
        }

        private void ConfluenceBackgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.ConfluencetreeView2.Nodes.Add(e.Result as TreeNode);
            //this.ConfluencetreeView.ExpandAll();
            this.ConfluencetreeView2.Nodes[0].EnsureVisible();
            FormIsWorking(true);
        }

        private void TargetcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormIsWorking(false);

            this.ConfluencetreeView2.Nodes.Clear();
            if (!ConfluenceBackgroundWorker2.IsBusy)
            {
                string key = string.Empty;
                var selectedItem = this.TargetcomboBox.SelectedItem as Result;
                if (selectedItem != null)
                {
                    key = selectedItem.key;
                }
                ConfluenceBackgroundWorker2.RunWorkerAsync(key);
            }
        }
    }
}
