using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;

namespace MyExplorer
{
    class Explorer
    {
        private int pointCurrentPath = -1;
        private List<string> collectionPath = new List<string>();
        private int countDrive;

        public buffer Buffer = new buffer();

        public string CurrentPath
        {
            get { return collectionPath[pointCurrentPath]; }
        }

        public Explorer()
        {
        }

        public void PopulateDriveTree(TreeView tvFolders)//
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();    // поулучить список дисков
            if (allDrives.Length == countDrive) return; //если количество узлов != количеству дисков, то добавился съёмный диск
            countDrive = 0;
            tvFolders.Nodes.Clear();
            TreeNode nodeTreeBase = new TreeNode("My Computer", 0, 0);
            tvFolders.Nodes.Add(nodeTreeBase);
            TreeNodeCollection nodeCollection = nodeTreeBase.Nodes;   // установить колекцию узлов

            foreach (var drive in allDrives)
            {
                int imageIndex = Info.SwitchDriveType(drive.DriveType.ToString());

                TreeNode nodeTree = new TreeNode(string.Format("{0}", drive.Name), imageIndex, imageIndex);   // создать новый узел диска
                nodeCollection.Add(nodeTree); // добавить новый узел
                countDrive++;
                PopulateLevelDirectoryTree(nodeTree);
            }
            nodeTreeBase.Toggle();
        }

        public void PopulateDriveList(ListView lvFiles)
        {
            lvFiles.Clear();
            lvFiles.Columns.Add("Имя диска", 100);
            lvFiles.Columns.Add("Свободно", 150);
            lvFiles.Columns.Add("Всего памяти", 150);
            var lvData = new string[3];
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (var drive in allDrives)
            {
                int imageIndex;
                if (drive.DriveType.ToString() == "CDRom")
                    imageIndex = 2;
                else
                    imageIndex = 1;
                try
                {
                    lvData[0] = drive.Name;
                    lvData[1] = Info.FormatDriveSize(drive.TotalFreeSpace);
                    lvData[2] = Info.FormatDriveSize(drive.TotalSize);
                }
                catch (IOException)
                {
                    lvData[1] = "";
                    lvData[2] = "";
                }
                finally
                {
                    var newItem = new ListViewItem(lvData, imageIndex);
                    lvFiles.Items.Add(newItem);
                }
            }
        }

        public void PopulateLevelDirectoryTree(TreeNode nodeCurrent, int lvl = 0)
        {
            TreeNodeCollection tnCollection = nodeCurrent.Nodes;
            tnCollection.Clear();
            string[] stringDirectories;
            try
            {
                stringDirectories = Directory.GetDirectories(Info.GetFullPath(nodeCurrent.FullPath)); // попытаться достать имя подпапок
            }
            catch (Exception) // если не получилось, то просто пропустить
            {
                return;
            }

            if (stringDirectories.Length == 0) return;
            foreach (string stringDir in stringDirectories)
            {
                string stringPathName = Info.GetPathName(stringDir);
                var nodeDir = new TreeNode(stringPathName, 3, 3);     // создать новый узел для директории
                tnCollection.Add(nodeDir);
                if (lvl == 1)
                {
                    PopulateLevelDirectoryTree(nodeDir);       // проставление, где есть, плюсиков
                }
            }
            if (tnCollection.Count == 0)
                tnCollection.Clear();
        }

        public bool PopulateDirectoryTree(TreeNode nodeCurrent, ListView lvFiles)
        {
            string path = Info.GetFullPath(nodeCurrent.FullPath);
            try
            {
                bool flag = GetCurrentDirectory(path, lvFiles);
                if (!flag) return false;

                if (nodeCurrent.Nodes.Count == 0)          // чтобы не перезагружать по сто раз
                    PopulateLevelDirectoryTree(nodeCurrent, 1);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        public void PopulateFullFiles(string path, ListView lvFiles)
        {
            string[] stringDirectories = Directory.GetDirectories(path);
            string[] stringFiles = Directory.GetFiles(path);
            Info.BuildListView(lvFiles, stringDirectories, stringFiles);

        }

        public bool GetDriveList(ListView lvFiles)
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            bool flag = pointCurrentPath >= 0 && CurrentPath == "\\";
            if (allDrives.Length == countDrive) 
                if (flag) return false;
            PopulateDriveList(lvFiles);
            if (flag) return false;
            int count = collectionPath.Count;
            int start = pointCurrentPath + 1;
            collectionPath.RemoveRange(start, count - start);
            collectionPath.Add("\\");
            pointCurrentPath++;
            return true;
        }

        public bool GetPrevDirectory(ListView lvFiles)
        {
            string path = collectionPath[pointCurrentPath - 1];
            try
            {
                if (path == "\\")
                    PopulateDriveList(lvFiles);
                else
                    PopulateFullFiles(path, lvFiles);
                pointCurrentPath--;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            return (pointCurrentPath == 0) ? true : false;
        }

        public bool GetCurrentDirectory(string path, ListView lvFiles)
        {
            try
            {
                if (pointCurrentPath >= 0 && path == CurrentPath) return false;
                PopulateFullFiles(path, lvFiles);
                int count = collectionPath.Count;
                int start = pointCurrentPath + 1;
                collectionPath.RemoveRange(start, count - start);
                collectionPath.Add(path);
                pointCurrentPath++;
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        public bool GetNextDirectory(ListView lvFiles)
        {
            string path = collectionPath[pointCurrentPath + 1];
            try
            {
                if (path == "\\")
                    PopulateDriveList(lvFiles);
                else
                    PopulateFullFiles(path, lvFiles);
                pointCurrentPath++;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            return (pointCurrentPath == collectionPath.Count - 1) ? true : false;
        }

        public bool GetUpDirectory(ListView lvFiles)
        {
            string lastPath = CurrentPath;
            string path = lastPath.Replace("\\" + Info.GetPathName(lastPath), "");
            if (path.Length == 2)
            {
                GetDriveList(lvFiles);
                return false;
            }
            else
                GetCurrentDirectory(path, lvFiles);
            return true;
        }

        private string GetTargetPath(ListView lvFiles)
        {
            string fileName = Info.GetPathName(this.Buffer.path);      // имя файла или папки
            string target;
            if (ReferenceEquals(lvFiles.FocusedItem, null))      // вычисляем новый адрес
                target = this.CurrentPath + "\\" + fileName;
            else
                target = this.CurrentPath + "\\" + lvFiles.FocusedItem.Text + "\\" + fileName;
            return target;
        }

        public void Move(ListView lvFiles)
        {
            try
            {
                string nameItem = GetTargetPath(lvFiles);
                if (File.Exists(this.Buffer.path))
                    FileSystem.MoveFile(this.Buffer.path, nameItem, UIOption.AllDialogs);
                else
                    FileSystem.MoveDirectory(this.Buffer.path, nameItem, UIOption.AllDialogs);
                var item = Info.BuildListViewItem(lvFiles, nameItem);
                item.Selected = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            this.Buffer.path = "";
            this.Buffer.operation = Operation.none;
        }
    }
}