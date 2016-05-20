using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace MyExplorer
{ 
    public partial class ExplorerForm : Form
    {
        Explorer exp;
        string labelText;

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

        private void Open(string itemsName) //
        {
            string path;
            bool flag = false;

            if (exp.CurrentPath == "\\")
                path = itemsName;
            else
                path = exp.CurrentPath + "\\" + itemsName;
            while (path[0] == '\\') path = path.Remove(0, 1);
            if (!ReferenceEquals(lvFiles.FocusedItem, null) && lvFiles.FocusedItem.SubItems[2].Text == "Файл")
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

        private void cmOpen_Click(object sender, EventArgs e)
        {
            if (!ReferenceEquals(lvFiles.FocusedItem, null) && lvFiles.FocusedItem.Selected)
                Open(lvFiles.FocusedItem.Text);
        }

        private void cmOpenInNewWindow_Click(object sender, EventArgs e)
        {
            ExplorerForm newForm = new ExplorerForm();
            if (!ReferenceEquals(lvFiles.FocusedItem, null) && lvFiles.FocusedItem.Selected)
            {
                newForm.Show();
                string path = exp.CurrentPath + "\\" + lvFiles.FocusedItem.Text;
                newForm.Open(path);
            }
        }

        private void RememberPathForMoveAndCopy(Operation oper) //
        {
            if (!ReferenceEquals(lvFiles.FocusedItem, null) && lvFiles.FocusedItem.Selected)
            {
                foreach (ListViewItem lvItem in lvFiles.SelectedItems)
                {
                    string rememberPath = adressString.Text + "\\" + lvItem.Text;
                    exp.Buffer.pathColl.Add(rememberPath);
                }
                exp.Buffer.operation = oper;
            }
        }

        private void cmMove_Click(object sender, EventArgs e)
        {
            exp.Buffer.pathColl.Clear();
            RememberPathForMoveAndCopy(Operation.move);
        }

        private void cmCopy_Click(object sender, EventArgs e)
        {
            exp.Buffer.pathColl.Clear();
            RememberPathForMoveAndCopy(Operation.copy);
        }

        private void WorkWithLVAfterOperation(string pathFromBuff, string targetPath, Operation oper) //
        {
            string nameItem = Info.GetPathName(pathFromBuff);
            if (ReferenceEquals(lvFiles.FocusedItem, null) || !lvFiles.FocusedItem.Selected) 
            {
                ListViewItem lvItem = Info.BuildListViewItem(lvFiles, targetPath);
                lvItem.Selected = true;
            }
            else if (exp.CurrentPath == pathFromBuff.Replace("\\" + nameItem, "") && oper == Operation.move)
            {                                                         //удаление после вырезания из lisview
                var lvItems = lvFiles.Items;
                foreach (ListViewItem item in lvItems)
                    if (item.Text == nameItem)
                        item.Remove();
            }
        }

        private void cmPast_Click(object sender, EventArgs e)
        {
            try
            {
                string target;
                string[] lsBuff = new string[exp.Buffer.pathColl.Count];
                exp.Buffer.pathColl.CopyTo(lsBuff);
                Operation oper = exp.Buffer.operation;
                if (!exp.Buffer.Empty)
                {
                    foreach (string path in lsBuff)
                    {
                        target = Info.GetTargetPath(lvFiles, exp, path);
                       
                        if (oper == Operation.move)   // узнаём, какую операцию использовали;
                            exp.Move(path, target);
                        else if (oper == Operation.copy)
                            exp.Copy(path, target);
                        WorkWithLVAfterOperation(path, target, oper);
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
     
        private void cmRefresh_Click(object sender, EventArgs e)
        {
            if (exp.CurrentPath == "\\")
                exp.GetDriveList(lvFiles, true);
            else
                exp.GetCurrentDirectory(exp.CurrentPath, lvFiles, true);
        }

        private void cmRename_Click(object sender, EventArgs e)
        {
            ListViewItem lvItem = lvFiles.FocusedItem;
            labelText = exp.CurrentPath + "\\" + lvItem.Text;
            lvFiles.FocusedItem.BeginEdit();
        }

        private void lvFiles_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            string source = Info.GetPathName(labelText);
            try
            {
                if (source == e.Label) return;
                exp.Rename(labelText, e.Label);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                e.CancelEdit = true;
            }
        }

        private void contextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!ReferenceEquals(lvFiles.FocusedItem, null) && lvFiles.FocusedItem.Selected)
            {
                foreach (ToolStripMenuItem cm in contextMenu.Items)
                    cm.Visible = true;
                cmCreate.Visible = false;
                cmRefresh.Visible = false;
                if (lvFiles.FocusedItem.SubItems[2].Text == "Файл")
                {
                    cmOpenInNewWindow.Visible = false;
                    cmPast.Visible = false;
                }
                if (lvFiles.Columns[0].Text == "Имя диска")
                {
                    cmRename.Visible = false;
                    cmCut.Visible = false;
                    cmCopy.Visible = false;
                    cmDelete.Visible = false;
                }
            }
            else if (ReferenceEquals(lvFiles.FocusedItem, null) || !lvFiles.FocusedItem.Selected)
            {
                foreach (ToolStripMenuItem cm in contextMenu.Items)
                    cm.Visible = false;
                cmRefresh.Visible = true;
                cmPast.Visible = true;
                cmCreate.Visible = true;
                cmProperty.Visible = true;
                if (exp.CurrentPath == "\\") cmPast.Visible = false;
            }
            if (exp.CurrentPath == "\\") cmCreate.Visible = false;
            if (exp.Buffer.Empty) cmPast.Enabled = false;
            else cmPast.Enabled = true;
        }

        private void cmCreateFolder_Click(object sender, EventArgs e)
        {
            exp.Create(lvFiles, "Новая папка", 1);
            cmRename_Click(this, e);
        }

        private void cmCreateFile_Click(object sender, EventArgs e)
        {
            exp.Create(lvFiles, "Новый текстовый файл", 0);
            cmRename_Click(this, e);
        }

        private void cmProperty_Click(object sender, EventArgs e)
        {
            string path;
            if (!ReferenceEquals(lvFiles.FocusedItem, null) && lvFiles.FocusedItem.Selected)
                path = adressString.Text + "\\" + lvFiles.FocusedItem.Text;
            else
                path = adressString.Text;
            Property form = new Property(true, path);
            form.Show();
        }
    }
}