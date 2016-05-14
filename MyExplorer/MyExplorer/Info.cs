using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace MyExplorer
{
    static class Info
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
            lvFiles.Columns.Add("Тип", 75);
            lvFiles.Columns.Add("Размер", 150);
        }

        public static string GetAdressPath(string str)
        {
            if (str == "\\")
                return "My Computer";
            else
                return str;
        }
    }
}
