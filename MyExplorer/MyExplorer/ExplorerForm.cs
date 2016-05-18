using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;

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
        }

        private void lvFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {       
            if (e.Button == MouseButtons.Left)
            {
                Cursor = Cursors.WaitCursor;
                Open(lvFiles.FocusedItem.Text);
                Cursor = DefaultCursor;
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
            Cursor = DefaultCursor;
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
            Cursor = DefaultCursor;
        }

        private void toolUp_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            bool flag = exp.GetUpDirectory(lvFiles);
            if (!flag)
                toolUp.Enabled = false;
            toolForward.Enabled = false;     
            adressString.Text = Info.GetAdressPath(exp.CurrentPath);
            Cursor = DefaultCursor;
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
            }
            Cursor = DefaultCursor;
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
            Cursor = DefaultCursor;
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
                if (File.Exists(path))
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
                Cursor = DefaultCursor;
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ReferenceEquals(lvFiles.FocusedItem, null) && lvFiles.FocusedItem.Selected)
                Open(lvFiles.FocusedItem.Text);
        }

        private void RememberPathForMoveAndCopy(Operation oper)
        {
            if (!ReferenceEquals(lvFiles.FocusedItem, null) && lvFiles.FocusedItem.Selected)
            {
                foreach (ListViewItem lvItem in lvFiles.SelectedItems)
                {
                    string rememberPath = adressString.Text + "\\" + lvItem.Text;
                    exp.Buffer.pathColl.Add(rememberPath);
                }
                exp.Buffer.operation = oper;

                MessageBox.Show("запомнили путь");
            }
        }

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exp.Buffer.pathColl.Clear();
            RememberPathForMoveAndCopy(Operation.move);
        }

        private void cmCopy_Click(object sender, EventArgs e)
        {
            exp.Buffer.pathColl.Clear();
            RememberPathForMoveAndCopy(Operation.copy);
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string target;
                string[] lsBuff = new string[exp.Buffer.pathColl.Count];
                exp.Buffer.pathColl.CopyTo(lsBuff);
                if (!exp.Buffer.Empty)
                {
                    foreach (string path in lsBuff)
                    {
                        target = Info.GetTargetPath(lvFiles, exp, path);
                        if (exp.Buffer.operation == Operation.move)   // узнаём, какую операцию использовали;
                            exp.Move(path, target);
                        else if (exp.Buffer.operation == Operation.copy)
                            exp.Copy(path, target);
                        if (ReferenceEquals(lvFiles.FocusedItem, null) || !lvFiles.FocusedItem.Selected)
                        {
                            ListViewItem lvItem = Info.BuildListViewItem(lvFiles, target);
                            lvItem.Selected = true;
                        }
                        else
                        {//ломает копирование, сделать удаление после вырезания
                            //lvFiles.FocusedItem.Remove();
                        }

                    }
                    
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void cmDelete_Click(object sender, EventArgs e)
        {
            string target;
            if (!ReferenceEquals(lvFiles.FocusedItem, null) && lvFiles.FocusedItem.Selected)
            {
                try
                {
                    foreach (ListViewItem lvItem in lvFiles.SelectedItems)
                    {
                        target = exp.CurrentPath + "\\" + lvItem.Text;
                        exp.Delete(target);
                        lvItem.Remove();
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }

        private void contextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!ReferenceEquals(lvFiles.FocusedItem, null) && lvFiles.FocusedItem.Selected)
            {
                foreach (ToolStripMenuItem cm in contextMenu.Items)
                    cm.Visible = true;
                cmRefresh.Visible = false;
                cmCreate.Visible = false;
            }
            else if (ReferenceEquals(lvFiles.FocusedItem, null) || !lvFiles.FocusedItem.Selected)
            {
                foreach (ToolStripMenuItem cm in contextMenu.Items)
                    cm.Visible = false;
                cmPast.Visible = true;
                cmRefresh.Visible = true;
                cmCreate.Visible = true;
                cmProperty.Visible = true;        
            }   
        }

    }
}