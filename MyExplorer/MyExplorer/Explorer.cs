﻿using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualBasic.FileIO;

namespace MyExplorer
{
    public class Explorer
    {
        private int pointCurrentPath = -1;
        private List<string> collectionPath;
        private int countDrive;

        public BufferFile Buffer;

        public string CurrentPath
        {
            get { return collectionPath[pointCurrentPath]; }
            set { collectionPath[pointCurrentPath] = value; }
        }

        public Explorer()
        {
            Buffer.pathColl = new List<string>();
            collectionPath = new List<string>();
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

        public bool GetDriveList(ListView lvFiles, bool refresh = false)
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            bool flag = pointCurrentPath >= 0 && CurrentPath == "\\";
            if (allDrives.Length == countDrive) 
                if (flag && !refresh) return false;
            PopulateDriveList(lvFiles);                   // обновить, если изменится количество дисков
            if (flag && refresh) return false;          // не перезаписывать путь дважды в коллекцию путей  
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

        public bool GetCurrentDirectory(string path, ListView lvFiles, bool refresh = false)
        {
            try
            {
                if (pointCurrentPath >= 0 && path == CurrentPath && !refresh) return false;
                PopulateFullFiles(path, lvFiles);
                if (refresh) return false;      // если просто обновились, то не записывать тот же путь в коллекцию
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

        public void Move(string sourcePath, string targetPath)
        {
            if (File.Exists(sourcePath))
                FileSystem.MoveFile(sourcePath, targetPath, UIOption.AllDialogs);
            else
                FileSystem.MoveDirectory(sourcePath, targetPath, UIOption.AllDialogs);

            Buffer.pathColl.Remove(sourcePath);
            if (Buffer.pathColl.Count == 0)
                Buffer.operation = Operation.none;
        }

        public void Copy(string sourcePath, string targetPath)
        {
            if (File.Exists(sourcePath))
                FileSystem.CopyFile(sourcePath, targetPath, UIOption.AllDialogs);
            else
                FileSystem.CopyDirectory(sourcePath, targetPath, UIOption.AllDialogs);           
        }

        public void Rename(string source, string target)
        {
            if (ReferenceEquals(target, null)) return;
            if (File.Exists(source))
                FileSystem.RenameFile(source, target);
            else if (Directory.Exists(source))
                FileSystem.RenameDirectory(source, target);
            else
                throw new Exception("Нельзя переименовать этот элемент, пожалуйста, обновите страницу");
        }

        public void Delete(string target)
        { 
            if (File.Exists(target))
                FileSystem.DeleteFile(target, UIOption.AllDialogs, RecycleOption.SendToRecycleBin);
            else
                FileSystem.DeleteDirectory(target, UIOption.AllDialogs, RecycleOption.SendToRecycleBin);
            if (Buffer.pathColl.Contains(target)) Buffer.pathColl.Remove(target);
        }

        public void Create(ListView lvFiles, string type, int method)
        {
            string str = Info.GetCountNew(type, lvFiles);
            if (method == 0) str += ".txt";
            string name = CurrentPath + "\\" + type + str;
            if (method == 0)
            {
                FileStream fl = File.Create(name);
                ProcessStartInfo pr = new ProcessStartInfo(name);
                fl.Close();
            }
            else
                Directory.CreateDirectory(name);
            ListViewItem lvItem = Info.BuildListViewItem(lvFiles, name);
            lvItem.Focused = true;
            lvItem.Selected = true;
        }
    }
}