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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolBack = new System.Windows.Forms.ToolStripMenuItem();
            this.toolForward = new System.Windows.Forms.ToolStripMenuItem();
            this.toolUp = new System.Windows.Forms.ToolStripMenuItem();
            this.adressString = new System.Windows.Forms.TextBox();
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
            this.lvFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvFiles.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lvFiles.LabelEdit = true;
            this.lvFiles.Location = new System.Drawing.Point(179, 24);
            this.lvFiles.Name = "lvFiles";
            this.lvFiles.Size = new System.Drawing.Size(506, 395);
            this.lvFiles.SmallImageList = this.imageList1;
            this.lvFiles.TabIndex = 1;
            this.lvFiles.UseCompatibleStateImageBehavior = false;
            this.lvFiles.View = System.Windows.Forms.View.Details;
            this.lvFiles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvFiles_MouseDoubleClick);
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
            // 
            // adressString
            // 
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
    }
}

