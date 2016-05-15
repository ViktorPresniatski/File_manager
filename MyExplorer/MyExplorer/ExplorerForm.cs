using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace MyExplorer
{
    public partial class ExplorerForm : Form
    {
        Explorer exp;
        public ExplorerForm()
        {
            InitializeComponent();
            exp = new Explorer();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            exp.PopulateDriveTree(tvFolders);      // если выбран Computer, то получить диски
            exp.GetDriveList(lvFiles);
            lvFiles.FullRowSelect = true;
            adressString.Text = Info.GetAdressPath(exp.CurrentPath);
        }

        private void Open(string itemsName)
        {
            string path;
            bool flag = false;
            Cursor = Cursors.WaitCursor;

            if (exp.CurrentPath == "\\")
                path = itemsName;
            else
                path = exp.CurrentPath + "\\" + itemsName;
            if (lvFiles.FocusedItem.SubItems[2].Text == "Файл")
                try { Process.Start(path); }
                catch (Exception exc) { MessageBox.Show(exc.Message); }
            else
            {
                flag = exp.GetCurrentDirectory(path, lvFiles);
                adressString.Text = Info.GetAdressPath(exp.CurrentPath);
            }
            if (flag)
            {
                toolBack.Enabled = true;
                toolUp.Enabled = true;
                toolForward.Enabled = false;
            }
            Cursor = Cursors.Default;
        }

        private void lvFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {       
            if (e.Button == MouseButtons.Left)
            {
                Open(lvFiles.FocusedItem.Text);
            }
        }

        private void toolBack_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            bool flag = exp.GetPrevDirectory(lvFiles);
            if (flag)
                toolBack.Enabled = false;
            toolForward.Enabled = true;
            adressString.Text = Info.GetAdressPath(exp.CurrentPath);
            if (adressString.Text == "My Computer")
                toolUp.Enabled = false;
            else
                toolUp.Enabled = true;
            Cursor = Cursors.Default;
        }

        private void toolForward_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            bool flag = exp.GetNextDirectory(lvFiles);
            if (flag)
                toolForward.Enabled = false;
            toolBack.Enabled = true;
            adressString.Text = Info.GetAdressPath(exp.CurrentPath);
            if (adressString.Text == "My Computer")
                toolUp.Enabled = false;
            else
                toolUp.Enabled = true;
            Cursor = Cursors.Default;
        }

        private void toolUp_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            bool flag = exp.GetUpDirectory(lvFiles);
            if (flag)
                toolForward.Enabled = true;
            else
                toolUp.Enabled = false;
            adressString.Text = Info.GetAdressPath(exp.CurrentPath);
            Cursor = Cursors.Default;
        }

        private void tvFolders_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Cursor = Cursors.WaitCursor;
                TreeNode nodeCurrent = e.Node;          //получить текущий выбранный диск или папку

                bool flag = true;

                if (nodeCurrent.SelectedImageIndex == 0)
                {
                    flag = exp.GetDriveList(lvFiles);
                    exp.PopulateDriveTree(tvFolders);      // если выбран Computer, то получить диски
                }
                else
                {
                    flag = exp.PopulateDirectoryTree(nodeCurrent, lvFiles);     //получить подкаталоги и файлы
                }
                if (flag)
                {
                    toolBack.Enabled = true;
                    toolForward.Enabled = false;
                }
                adressString.Text = Info.GetAdressPath(exp.CurrentPath);
                if (adressString.Text == "My Computer")
                    toolUp.Enabled = false;
                else
                    toolUp.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void tvFolders_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode nodeCurrent = e.Node;
            TreeNodeCollection tnCollection = nodeCurrent.Nodes;
            foreach (TreeNode node in tnCollection)
            {
                if (node.Nodes.Count > 0) return;
                Cursor = Cursors.WaitCursor;
                exp.PopulateLevelDirectoryTree(node, 0);
            }
            Cursor = Cursors.Default;
        }

        private void adressString_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string path = adressString.Text;
                bool flag = false;
                Cursor = Cursors.WaitCursor;
                if (path.ToLower() == "my computer")
                {
                    exp.GetDriveList(lvFiles);
                    toolUp.Enabled = false;
                    Cursor = Cursors.Default;
                    return;
                }
                if (path.Contains(".") == false)
                    flag = exp.GetCurrentDirectory(path, lvFiles);
                else
                {
                    try
                    { Process.Start(path); }
                    catch (Exception exc)
                    { MessageBox.Show(exc.Message); }
                }
                
                if (flag)
                {
                    toolBack.Enabled = true;
                    toolForward.Enabled = false;
                }
                Cursor = Cursors.Default;
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ReferenceEquals(lvFiles.FocusedItem, null))
                Open(lvFiles.FocusedItem.Text);
        }
    }
}