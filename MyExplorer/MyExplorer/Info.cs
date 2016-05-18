using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;

namespace MyExplorer
{
    class Info
    {    
        public static string GetFullPath(string stringPath)
        {
            string stringParse = stringPath.Replace("My Computer\\", "");
            return stringParse;
        }

        public static string GetPathName(string stringPath)
        {
            string[] stringSplit = stringPath.Split('\\');   // получить имя папки
            return stringSplit[stringSplit.Length - 1];
        }

        public static string FormatDate(DateTime dtDate)
        {
            string stringDate;
            stringDate = dtDate.ToShortDateString() + " " + dtDate.ToShortTimeString();
            return stringDate;
        }

        public static string FormatDriveSize(long lSize)
        {
            string stringSize;
            long size = lSize / (1024 * 1024 * 100);
            stringSize = (size / 10).ToString() + "," + (size % 10).ToString() + " Гб";
            return stringSize;
        }

        public static string FormatSize(Int64 lSize)
        {
            string stringSize;
            List<string> str = new List<string>();
            string ss = "";
            if (lSize < 1024)
            {
                stringSize = lSize == 0 ? "0" : "1";
            }
            else
            {
                lSize /= 1024;
                stringSize = lSize.ToString();
                int n = stringSize.Length;
                for (int i = n - 1; i >= 0; i--)
                {
                    ss = stringSize[i] + ss;
                    if ((n - i) % 3 == 0)
                    {
                        str.Add(ss);
                        ss = "";
                    }
                }
                if (ss != "") str.Insert(0, ss);
                stringSize = String.Join(" ", str.ToArray());
            }
            return stringSize + " KB";
        }

        public static int SwitchDriveType(string str)
        {
            switch (str)
            {
                case "Fixed":         //Local drives
                    return 1;
                case "CDRom":           //CD rom drives
                    return 2;
                case "Removable":
                    return 1;           //removable drives
                default:                //defalut to folder
                    return 3;
            }
        }

        public static void InitListView(ListView lvFiles)
        {
            lvFiles.Clear();        // чистим весь лист и создаем шапку
            lvFiles.Columns.Add("Имя", 150);
            lvFiles.Columns.Add("Дата изменения", 150);
            lvFiles.Columns.Add("Тип", 85);
            lvFiles.Columns.Add("Размер", 100);
        }

        public static ListViewItem BuildListViewItem(ListView lvFiles, string itemPath)
        {
            var lvData = new string[4];
            DateTime modifyDate;
            ListViewItem lvItem;

            if (File.Exists(itemPath))
            {
                var objFileSize = new FileInfo(itemPath);
                Int64 fileSize = objFileSize.Length;
                modifyDate = objFileSize.LastWriteTime;
                lvData[0] = Info.GetPathName(itemPath);
                lvData[1] = Info.FormatDate(modifyDate);
                lvData[2] = "Файл";
                lvData[3] = Info.FormatSize(fileSize);
                lvItem = new ListViewItem(lvData, 4);
                lvFiles.Items.Add(lvItem);
            }
            else
            {
                string stringPathName = Info.GetPathName(itemPath);
                modifyDate = Directory.GetLastWriteTime(Info.GetFullPath(itemPath));
                lvData[0] = Info.GetPathName(itemPath);
                lvData[1] = Info.FormatDate(modifyDate);
                lvData[2] = "Папка с файлами";
                lvData[3] = "";
                lvItem = new ListViewItem(lvData, 3);
                lvFiles.Items.Add(lvItem);
            }
            return lvItem;
        }

        public static void BuildListView(ListView lvFiles, string[] stringDirectories, string[] stringFiles)
        {
            Info.InitListView(lvFiles);    // очистить список
            foreach (string stringDir in stringDirectories)
            {
                BuildListViewItem(lvFiles, stringDir);
            }

            foreach (string stringFile in stringFiles)
            {
                BuildListViewItem(lvFiles, stringFile);
            }
        }

        public static string GetAdressPath(string str)
        {
            if (str == "\\")
                return "My Computer";
            else
                return str;
        }

        public static string GetTargetPath(ListView lvFiles, Explorer exp, string fullPath)
        {
            string fileName = Info.GetPathName(fullPath);      // имя файла или папки
            string target;
            if (!ReferenceEquals(lvFiles.FocusedItem, null) && lvFiles.FocusedItem.Selected)      // вычисляем новый адрес
                target = exp.CurrentPath + "\\" + lvFiles.FocusedItem.Text + "\\" + fileName;
            else
                target = exp.CurrentPath + "\\" + fileName;
            return target;
        }
    }
}
