using ConfluenceAutomator.Library;
using Newtonsoft.Json;
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
        private string rightSideSpace = string.Empty;
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
            r.results.Insert(0, new Result() { id = 0, name = "--SELECT--", key = string.Empty, type = string.Empty });
            this.MaincomboBox.DataSource = r.results;


            AllSpaces r2 = confSpaceService.Execute();

            this.TargetcomboBox.ValueMember = Strings.KEY;
            this.TargetcomboBox.DisplayMember = Strings.NAME;
            r2.results.Insert(0, new Result() { id = 0, name = "--SELECT--", key = string.Empty, type = string.Empty });
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
            string status = string.Empty;
            status = ((enabled == false) ? "Work in progress." : "Work Complete.");
            this.Log(status);
        }

        private void ConfluenceBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (e.Argument != null && !string.IsNullOrEmpty(e.Argument.ToString()))
            {
                ConfluenceSpaceTaskExecutor confSpaceService = new ConfluenceSpaceTaskExecutor(this);
                e.Result = confSpaceService.CreateSpaceTreeNode(confSpaceService.Execute().results.Where(x => x.key == e.Argument.ToString()).FirstOrDefault());
            }
        }

        private void ConfluenceBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                this.ConfluencetreeView.Nodes.Add(e.Result as TreeNode);
                this.ConfluencetreeView.Nodes[0].EnsureVisible();
                FormIsWorking(true);
            }
        }

        private void ConfluenceBackgroundWorkerRIGHT_DoWork(object sender, DoWorkEventArgs e)
        {
            if (e.Argument != null && !string.IsNullOrEmpty(e.Argument.ToString()))
            {
                rightSideSpace = e.Argument.ToString();
                ConfluenceSpaceTaskExecutor confSpaceService = new ConfluenceSpaceTaskExecutor(this);
                e.Result = confSpaceService.CreateSpaceTreeNode(confSpaceService.Execute().results.Where(x => x.key == e.Argument.ToString()).FirstOrDefault());
            }
        }

        private void ConfluenceBackgroundWorkerRIGHT_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                this.ConfluencetreeView2.Nodes.Add(e.Result as TreeNode);
                this.ConfluencetreeView2.Nodes[0].EnsureVisible();
                FormIsWorking(true);
            }
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

        private ChildPagesOutput_Result GetFirstChecked()
        {
            ChildPagesOutput_Result result = null;
            foreach (TreeNode node in this.ConfluencetreeView.Nodes)
            {
                if (node.Checked)
                {
                    return node.Tag as ChildPagesOutput_Result;
                }
                result = GetFirstChecked(node.Nodes);
                if (result != null)
                    break;
            }
            return result;
        }

        private ChildPagesOutput_Result GetFirstChecked(TreeNodeCollection nodes)
        {
            ChildPagesOutput_Result result = null;
            foreach (TreeNode node in nodes)
            {
                if (node.Checked)
                {
                    return node.Tag as ChildPagesOutput_Result;
                }
                result = GetFirstChecked(node.Nodes);
                if (result != null)
                    break;
            }
            return result;
        }


        private void Copybutton_Click(object sender, EventArgs e)
        {
            ConfluenceContext.SaveCredentials("vd2", "Welcome4");

            ChildPagesOutput_Result firstchecked = GetFirstChecked();
            ConfluencePageTreeTaskExecutor task = new ConfluencePageTreeTaskExecutor();

            ChildPagesOutput_Result lastProcessed = firstchecked;
            string leftSideSpace = firstchecked.ParentSpace;
            string currentParentId = firstchecked.id;


            var projectNode = this.ConfluencetreeView2.Nodes[0];

            if (projectNode != null)
            {
                lastProcessed = task.CreateChildPageX(
                            AppSettingsHelper.GetValue(Strings.CREATE_PAGE_URL_KEY),
                            JsonConvert.SerializeObject(
                                task.CreateChildPageInstance(leftSideSpace, currentParentId, projectNode.Text, string.Format(AppSettingsHelper.GetValue(Strings.INCLUDE_PAGECONTENT_KEY), string.Format("{0} Home", projectNode.Text), rightSideSpace)
                                )
                            )
                        );
            }
            firstchecked = lastProcessed;
            currentParentId = lastProcessed.id;

            foreach (TreeNode node in this.ConfluencetreeView2.Nodes[0].Nodes)
            {
                if (node.Checked && node.Tag != null)
                {
                    ChildPagesOutput_Result tag = node.Tag as ChildPagesOutput_Result;
                    if (tag != null)
                    {

                        lastProcessed = task.CreateChildPageX(
                            AppSettingsHelper.GetValue(Strings.CREATE_PAGE_URL_KEY),
                            JsonConvert.SerializeObject(
                                task.CreateChildPageInstance(leftSideSpace, currentParentId, string.Format("{0} - {1}", tag.ParentSpace, tag.title), string.Format(AppSettingsHelper.GetValue(Strings.INCLUDE_PAGECONTENT_KEY), tag.title, tag.ParentSpace)
                                )
                            )
                        );


                        if (node.Nodes.Count > 0)
                        {
                            currentParentId = lastProcessed.id;
                            CreateChildrenNodes(task, lastProcessed, currentParentId, leftSideSpace, node.Nodes);
                            currentParentId = firstchecked.id;
                        }
                    }
                }
            }


            MessageBox.Show("All Done.");
        }


        private void CreateChildrenNodes(ConfluencePageTreeTaskExecutor task,
                        ChildPagesOutput_Result lastProcessed, string currentParentId, string parentSpace, TreeNodeCollection nodes)
        {
            string oldParentId = currentParentId;
            foreach (TreeNode node in nodes)
            {
                if (node.Checked && node.Tag != null)
                {
                    ChildPagesOutput_Result tag = node.Tag as ChildPagesOutput_Result;
                    if (tag != null)
                    {

                        lastProcessed = task.CreateChildPageX(
                            AppSettingsHelper.GetValue(Strings.CREATE_PAGE_URL_KEY),
                            JsonConvert.SerializeObject(
                                task.CreateChildPageInstance(parentSpace, currentParentId, string.Format("{0} - {1}", tag.ParentSpace, tag.title), string.Format(AppSettingsHelper.GetValue(Strings.INCLUDE_PAGECONTENT_KEY), tag.title, tag.ParentSpace)
                                )
                            )
                        );




                        if (node.Nodes.Count > 0)
                        {
                            currentParentId = lastProcessed.id;
                            CreateChildrenNodes(task, lastProcessed, currentParentId, parentSpace, node.Nodes);
                            currentParentId = oldParentId;
                        }
                    }
                }
            }
        }


        // Updates all child tree nodes recursively.
        private void CheckAllChildNodes(TreeNode treeNode, bool nodeChecked)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                node.Checked = nodeChecked;
                if (node.Nodes.Count > 0)
                {
                    // If the current node has child nodes, call the CheckAllChildsNodes method recursively.
                    this.CheckAllChildNodes(node, nodeChecked);
                }
            }
        }

        // NOTE   This code can be added to the BeforeCheck event handler instead of the AfterCheck event.
        // After a tree node's Checked property is changed, all its child nodes are updated to the same value.
        private void node_AfterCheck(object sender, TreeViewEventArgs e)
        {
            // The code only executes if the user caused the checked state to change.
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.Nodes.Count > 0)
                {
                    /* Calls the CheckAllChildNodes method, passing in the current 
                    Checked value of the TreeNode whose checked state changed. */
                    this.CheckAllChildNodes(e.Node, e.Node.Checked);
                }
            }
        }

        private void AppendButton_Click(object sender, EventArgs e)
        {
            ConfluenceContext.SaveCredentials("vd2", "Welcome4");

            ChildPagesOutput_Result firstchecked = GetFirstChecked();
            ConfluencePageTreeTaskExecutor task = new ConfluencePageTreeTaskExecutor();

            ChildPagesOutput_Result lastProcessed = firstchecked;
            string leftSideSpace = firstchecked.ParentSpace;
            string currentParentId = firstchecked.id;

            List<ChildPagesOutput_Result> list = new List<ChildPagesOutput_Result>();
            List<ChildPagesOutput_Result> result = GetAllToAppend(this.ConfluencetreeView2.Nodes, list);




            foreach (ChildPagesOutput_Result node in result)
            {
                lastProcessed = task.CreateChildPageX(
                    AppSettingsHelper.GetValue(Strings.CREATE_PAGE_URL_KEY),
                    JsonConvert.SerializeObject(
                        task.CreateChildPageInstance(leftSideSpace, currentParentId, string.Format("{0} - {1}", node.ParentSpace, node.title), string.Format(AppSettingsHelper.GetValue(Strings.INCLUDE_PAGECONTENT_KEY), node.title, node.ParentSpace)
                        )
                    )
                );
            }

            MaincomboBox_SelectedIndexChanged(sender, e);


            MessageBox.Show("All Done.");


        }


        public List<ChildPagesOutput_Result> GetAllToAppend(TreeNodeCollection nodes, List<ChildPagesOutput_Result> list)
        {
            //List<ChildPagesOutput_Result> list = new List<ChildPagesOutput_Result>();
            foreach (TreeNode node in nodes)
            {
                if (node.Checked)
                {
                    ChildPagesOutput_Result tag = node.Tag as ChildPagesOutput_Result;
                    if (tag != null)
                    {
                        list.Add(tag);
                    }
                }
                GetAllToAppend(node.Nodes, list);
            }

            return list;
        }
    }
}
