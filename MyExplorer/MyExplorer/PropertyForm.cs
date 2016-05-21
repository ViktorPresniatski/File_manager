using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace MyExplorer
{
    public partial class PropertyForm : Form
    {
        Explorer exp;
        string path;
        ListView lvFiles;
        TextBox adressString;

        public PropertyForm(Explorer exp, string path, ListView lvFiles, TextBox adressString)
        {
            InitializeComponent();
            this.path = path;
            this.exp = exp;
            this.lvFiles = lvFiles;
            this.adressString = adressString;
        }

        private void Property_Load(object sender, EventArgs e)
        {
            if (File.Exists(path))
            {
                GetPropertyFile(path);
            }
            else if (Directory.Exists(path))
            {
                if (path.Length == 3)
                    GetPropertyDrive(path);
                else
                    GetPropertyFolder(path);
            }
            else
                Close();
        }

        private void GetPropertyFile(string path)
        {
            DateTime createDate;
            DateTime modifyDate;
            Int64 fileSize;
            pictureBox1.Image = imageList1.Images[1];
            FileInfo objFile = new FileInfo(path);
            fileSize = objFile.Length;
            createDate = objFile.CreationTime;
            modifyDate = objFile.LastWriteTime;

            Text = "Свойства: " + objFile.Name;
            textBox1.Text = objFile.Name; //Info.GetPathName(path);
            label6.Text = "Файл (" + objFile.Extension + ")";
            label7.Text = objFile.DirectoryName;//GetLocation(path);
            label8.Text = GetSize(fileSize);
            label9.Text = GetDate(createDate);
            label5.Text = "Изменён:";
            label10.Text = GetDate(modifyDate);
        }

        private void GetPropertyFolder(string path)
        {
            DateTime createDate;
            Int64 fileSize;
            pictureBox1.Image = imageList1.Images[0];
            DirectoryInfo objDir = new DirectoryInfo(path);
            fileSize = GetSizeFolder(path);
            createDate = objDir.CreationTime;

            Text = "Свойства: " + objDir.Name;
            textBox1.Text = objDir.Name;
            label6.Text = "Папка с файлами";
            label7.Text = objDir.Parent.FullName;
            label8.Text = GetSize(fileSize);
            label9.Text = GetDate(createDate);
            label10.Text = GetCountSubFoldersAndFiles(path);
        }

        private void GetPropertyDrive(string path)
        {
            pictureBox1.Image = imageList1.Images[2];
            DriveInfo drive = new DriveInfo(path);

            Text = drive.VolumeLabel + " (" + drive.Name.Replace("\\", "") + ")";
            textBox1.Text = drive.VolumeLabel;
            label6.Text = drive.DriveType.ToString();
            label2.Text = "Файл. система:";
            label7.Text = drive.DriveFormat;
            label3.Text = "Занято:";
            long driveSize = drive.TotalSize - drive.TotalFreeSpace;
            label8.Text = GetSize(driveSize) + "  (" + Info.FormatDriveSize(driveSize) + ")";
            label4.Text = "Свободно:";
            label9.Text = GetSize(drive.TotalFreeSpace) + "  (" + Info.FormatDriveSize(drive.TotalFreeSpace) + ")";
            label5.Text = "Ёмкость:";
            label10.Text = GetSize(drive.TotalSize) + "  (" + Info.FormatDriveSize(drive.TotalSize) + ")";
        }

        private string GetDate(DateTime dt)
        {
            string result = dt.ToLongDateString() + ", " + dt.ToLongTimeString();
            return result;
        }

        private string GetSize(Int64 size)
        {
            string sizeB = size.ToString();
            List<string> str = new List<string>();
            string ss = "";
            int n = sizeB.Length;
            for (int i = n - 1; i >= 0; i--)
            {
                ss = sizeB[i] + ss;
                if ((n - i) % 3 == 0)
                {
                    str.Add(ss);
                    ss = "";
                }
            }
            if (ss != "") str.Insert(0, ss);
            sizeB = String.Join(" ", str.ToArray());
            return sizeB + " байт";
        }

        private Int64 GetSizeFolder(string path)
        {
            Int64 folderSize = 0;
            try
            {
                string[] files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
                foreach (string str in files) folderSize += (new FileInfo(str)).Length;
            }
            catch
            {
                MessageBox.Show("Нет доступа");
                Close();
            }
            return folderSize;
        }

        private string GetCountSubFoldersAndFiles(string path)
        {
            int countFiles = 0, countFolders = 0;
            try
            {
                string[] files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
                countFiles = files.Length;
                string[] folders = Directory.GetDirectories(path, "*", SearchOption.AllDirectories);
                countFolders = folders.Length;
            }
            catch
            {
                MessageBox.Show("Нет доступа");
                Close();
            }
            return "Файлов: " + countFiles + "; папок: " + countFolders;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (path.Length == 3) { Close(); return; }
                string newPath = path.Replace(Info.GetPathName(path), "") + textBox1.Text;
                if (path == newPath) { Close(); return; } 
                exp.Rename(path, textBox1.Text);
                if (!ReferenceEquals(lvFiles.FocusedItem, null) && lvFiles.FocusedItem.Selected)
                    lvFiles.FocusedItem.Text = textBox1.Text;
                else
                {
                    exp.CurrentPath = newPath;
                    exp.GetCurrentDirectory(newPath, lvFiles, true);
                    adressString.Text = newPath;
                }
                    
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                Close();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }   
    }
}
