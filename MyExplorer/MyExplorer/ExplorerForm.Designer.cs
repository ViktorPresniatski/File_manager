namespace MyExplorer
{
    partial class ExplorerForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExplorerForm));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.tvFolders = new System.Windows.Forms.TreeView();
            this.lvFiles = new System.Windows.Forms.ListView();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.cmOpenInNewWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.cmRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.cmCut = new System.Windows.Forms.ToolStripMenuItem();
            this.cmCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.cmPast = new System.Windows.Forms.ToolStripMenuItem();
            this.cmDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.cmRename = new System.Windows.Forms.ToolStripMenuItem();
            this.cmCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.cmCreateFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.cmCreateFile = new System.Windows.Forms.ToolStripMenuItem();
            this.cmProperty = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolBack = new System.Windows.Forms.ToolStripMenuItem();
            this.toolForward = new System.Windows.Forms.ToolStripMenuItem();
            this.toolUp = new System.Windows.Forms.ToolStripMenuItem();
            this.adressString = new System.Windows.Forms.TextBox();
            this.contextMenu.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "komp.jpg");
            this.imageList1.Images.SetKeyName(1, "Снимок.PNG");
            this.imageList1.Images.SetKeyName(2, "Снимок2.PNG");
            this.imageList1.Images.SetKeyName(3, "1421599547_papka.jpg");
            this.imageList1.Images.SetKeyName(4, "Снимок3.PNG");
            // 
            // splitter1
            // 
            this.splitter1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.splitter1.Location = new System.Drawing.Point(179, 24);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 395);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // tvFolders
            // 
            this.tvFolders.Dock = System.Windows.Forms.DockStyle.Left;
            this.tvFolders.ImageIndex = 0;
            this.tvFolders.ImageList = this.imageList1;
            this.tvFolders.Location = new System.Drawing.Point(0, 24);
            this.tvFolders.Name = "tvFolders";
            this.tvFolders.SelectedImageIndex = 0;
            this.tvFolders.Size = new System.Drawing.Size(179, 395);
            this.tvFolders.TabIndex = 0;
            this.tvFolders.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvFolders_BeforeExpand);
            this.tvFolders.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvFolders_NodeMouseClick);
            // 
            // lvFiles
            // 
            this.lvFiles.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvFiles.CausesValidation = false;
            this.lvFiles.ContextMenuStrip = this.contextMenu;
            this.lvFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvFiles.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.lvFiles.LabelEdit = true;
            this.lvFiles.Location = new System.Drawing.Point(179, 24);
            this.lvFiles.Name = "lvFiles";
            this.lvFiles.Size = new System.Drawing.Size(506, 395);
            this.lvFiles.SmallImageList = this.imageList1;
            this.lvFiles.TabIndex = 1;
            this.lvFiles.UseCompatibleStateImageBehavior = false;
            this.lvFiles.View = System.Windows.Forms.View.Details;
            this.lvFiles.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.lvFiles_AfterLabelEdit);
            this.lvFiles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvFiles_MouseDoubleClick);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmOpen,
            this.cmOpenInNewWindow,
            this.cmRefresh,
            this.cmCut,
            this.cmCopy,
            this.cmPast,
            this.cmDelete,
            this.cmRename,
            this.cmCreate,
            this.cmProperty});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(199, 246);
            this.contextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenu_Opening);
            // 
            // cmOpen
            // 
            this.cmOpen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.cmOpen.Name = "cmOpen";
            this.cmOpen.Size = new System.Drawing.Size(198, 22);
            this.cmOpen.Text = "Открыть";
            this.cmOpen.Click += new System.EventHandler(this.cmOpen_Click);
            // 
            // cmOpenInNewWindow
            // 
            this.cmOpenInNewWindow.Name = "cmOpenInNewWindow";
            this.cmOpenInNewWindow.Size = new System.Drawing.Size(198, 22);
            this.cmOpenInNewWindow.Text = "Открыть в новом окне";
            this.cmOpenInNewWindow.Click += new System.EventHandler(this.cmOpenInNewWindow_Click);
            // 
            // cmRefresh
            // 
            this.cmRefresh.Name = "cmRefresh";
            this.cmRefresh.Size = new System.Drawing.Size(198, 22);
            this.cmRefresh.Text = "Обновить";
            this.cmRefresh.Click += new System.EventHandler(this.cmRefresh_Click);
            // 
            // cmCut
            // 
            this.cmCut.Name = "cmCut";
            this.cmCut.Size = new System.Drawing.Size(198, 22);
            this.cmCut.Text = "Вырезать";
            this.cmCut.Click += new System.EventHandler(this.cmMove_Click);
            // 
            // cmCopy
            // 
            this.cmCopy.Name = "cmCopy";
            this.cmCopy.Size = new System.Drawing.Size(198, 22);
            this.cmCopy.Text = "Копировать";
            this.cmCopy.Click += new System.EventHandler(this.cmCopy_Click);
            // 
            // cmPast
            // 
            this.cmPast.Name = "cmPast";
            this.cmPast.Size = new System.Drawing.Size(198, 22);
            this.cmPast.Text = "Вставить";
            this.cmPast.Click += new System.EventHandler(this.cmPast_Click);
            // 
            // cmDelete
            // 
            this.cmDelete.Name = "cmDelete";
            this.cmDelete.Size = new System.Drawing.Size(198, 22);
            this.cmDelete.Text = "Удалить";
            this.cmDelete.Click += new System.EventHandler(this.cmDelete_Click);
            // 
            // cmRename
            // 
            this.cmRename.Name = "cmRename";
            this.cmRename.Size = new System.Drawing.Size(198, 22);
            this.cmRename.Text = "Переименовать";
            this.cmRename.Click += new System.EventHandler(this.cmRename_Click);
            // 
            // cmCreate
            // 
            this.cmCreate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmCreateFolder,
            this.cmCreateFile});
            this.cmCreate.Name = "cmCreate";
            this.cmCreate.Size = new System.Drawing.Size(198, 22);
            this.cmCreate.Text = "Создать";
            // 
            // cmCreateFolder
            // 
            this.cmCreateFolder.Image = ((System.Drawing.Image)(resources.GetObject("cmCreateFolder.Image")));
            this.cmCreateFolder.Name = "cmCreateFolder";
            this.cmCreateFolder.Size = new System.Drawing.Size(165, 22);
            this.cmCreateFolder.Text = "Папку";
            this.cmCreateFolder.Click += new System.EventHandler(this.cmCreateFolder_Click);
            // 
            // cmCreateFile
            // 
            this.cmCreateFile.Image = ((System.Drawing.Image)(resources.GetObject("cmCreateFile.Image")));
            this.cmCreateFile.Name = "cmCreateFile";
            this.cmCreateFile.Size = new System.Drawing.Size(165, 22);
            this.cmCreateFile.Text = "Текстовый файл";
            this.cmCreateFile.Click += new System.EventHandler(this.cmCreateFile_Click);
            // 
            // cmProperty
            // 
            this.cmProperty.Name = "cmProperty";
            this.cmProperty.Size = new System.Drawing.Size(198, 22);
            this.cmProperty.Text = "Свойства";
            this.cmProperty.Click += new System.EventHandler(this.cmProperty_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBack,
            this.toolForward,
            this.toolUp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(685, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolBack
            // 
            this.toolBack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.toolBack.Enabled = false;
            this.toolBack.Image = ((System.Drawing.Image)(resources.GetObject("toolBack.Image")));
            this.toolBack.Name = "toolBack";
            this.toolBack.Size = new System.Drawing.Size(67, 20);
            this.toolBack.Text = "Назад";
            this.toolBack.Click += new System.EventHandler(this.toolBack_Click);
            // 
            // toolForward
            // 
            this.toolForward.Enabled = false;
            this.toolForward.Image = ((System.Drawing.Image)(resources.GetObject("toolForward.Image")));
            this.toolForward.Name = "toolForward";
            this.toolForward.Size = new System.Drawing.Size(74, 20);
            this.toolForward.Text = "Вперед";
            this.toolForward.Click += new System.EventHandler(this.toolForward_Click);
            // 
            // toolUp
            // 
            this.toolUp.Enabled = false;
            this.toolUp.Image = ((System.Drawing.Image)(resources.GetObject("toolUp.Image")));
            this.toolUp.Name = "toolUp";
            this.toolUp.Size = new System.Drawing.Size(66, 20);
            this.toolUp.Text = "Вверх";
            this.toolUp.Click += new System.EventHandler(this.toolUp_Click);
            // 
            // adressString
            // 
            this.adressString.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.adressString.Location = new System.Drawing.Point(214, 4);
            this.adressString.Name = "adressString";
            this.adressString.Size = new System.Drawing.Size(471, 20);
            this.adressString.TabIndex = 4;
            this.adressString.KeyDown += new System.Windows.Forms.KeyEventHandler(this.adressString_KeyDown);
            // 
            // ExplorerForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(685, 419);
            this.Controls.Add(this.adressString);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.lvFiles);
            this.Controls.Add(this.tvFolders);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ExplorerForm";
            this.Text = "Explorer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenu.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.TreeView tvFolders;
        private System.Windows.Forms.ListView lvFiles;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolBack;
        private System.Windows.Forms.ToolStripMenuItem toolForward;
        private System.Windows.Forms.ToolStripMenuItem toolUp;
        private System.Windows.Forms.TextBox adressString;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem cmOpen;
        private System.Windows.Forms.ToolStripMenuItem cmCut;
        private System.Windows.Forms.ToolStripMenuItem cmCopy;
        private System.Windows.Forms.ToolStripMenuItem cmPast;
        private System.Windows.Forms.ToolStripMenuItem cmDelete;
        private System.Windows.Forms.ToolStripMenuItem cmRename;
        private System.Windows.Forms.ToolStripMenuItem cmProperty;
        private System.Windows.Forms.ToolStripMenuItem cmCreate;
        private System.Windows.Forms.ToolStripMenuItem cmOpenInNewWindow;
        private System.Windows.Forms.ToolStripMenuItem cmRefresh;
        private System.Windows.Forms.ToolStripMenuItem cmCreateFolder;
        private System.Windows.Forms.ToolStripMenuItem cmCreateFile;
    }
}

