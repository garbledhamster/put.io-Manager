namespace putio
{
    partial class FormPutioManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPutioManager));
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDownloadsFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStripPutioFiles = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.autoDownloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.downloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zipDownloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListTreeView = new System.Windows.Forms.ImageList(this.components);
            this.treeViewPutioFiles = new System.Windows.Forms.TreeView();
            this.groupBoxFoldersFiles = new System.Windows.Forms.GroupBox();
            this.splitContainerFiles = new System.Windows.Forms.SplitContainer();
            this.treeViewAutoDownloads = new System.Windows.Forms.TreeView();
            this.contextMenuStripAutoDownload = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBoxDownloads = new System.Windows.Forms.GroupBox();
            this.dataGridViewDownloads = new System.Windows.Forms.DataGridView();
            this.ColumnFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStarted = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCompleted = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBoxTransfers = new System.Windows.Forms.GroupBox();
            this.dataGridViewTransfers = new System.Windows.Forms.DataGridView();
            this.contextMenuStripTransfers = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.retryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cleanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.refreshToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPeers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnUploaded = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransferStarted = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStripMain.SuspendLayout();
            this.statusStripMain.SuspendLayout();
            this.contextMenuStripPutioFiles.SuspendLayout();
            this.groupBoxFoldersFiles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerFiles)).BeginInit();
            this.splitContainerFiles.Panel1.SuspendLayout();
            this.splitContainerFiles.Panel2.SuspendLayout();
            this.splitContainerFiles.SuspendLayout();
            this.contextMenuStripAutoDownload.SuspendLayout();
            this.groupBoxDownloads.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDownloads)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBoxTransfers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTransfers)).BeginInit();
            this.contextMenuStripTransfers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.BackColor = System.Drawing.Color.Gray;
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(625, 24);
            this.menuStripMain.TabIndex = 0;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openDownloadsFolderToolStripMenuItem,
            this.toolStripSeparator2,
            this.preferencesToolStripMenuItem,
            this.toolStripSeparator3,
            this.closeToolStripMenuItem,
            this.testToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openDownloadsFolderToolStripMenuItem
            // 
            this.openDownloadsFolderToolStripMenuItem.Name = "openDownloadsFolderToolStripMenuItem";
            this.openDownloadsFolderToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.openDownloadsFolderToolStripMenuItem.Text = "Open Downloads Folder";
            this.openDownloadsFolderToolStripMenuItem.Click += new System.EventHandler(this.openDownloadsFolderToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(198, 6);
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.preferencesToolStripMenuItem.Text = "Preferences";
            this.preferencesToolStripMenuItem.Click += new System.EventHandler(this.preferencesToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(198, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.testToolStripMenuItem.Text = "Test";
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click_1);
            // 
            // statusStripMain
            // 
            this.statusStripMain.BackColor = System.Drawing.Color.Gray;
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStripMain.Location = new System.Drawing.Point(0, 422);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Size = new System.Drawing.Size(625, 22);
            this.statusStripMain.TabIndex = 1;
            this.statusStripMain.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(779, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // contextMenuStripPutioFiles
            // 
            this.contextMenuStripPutioFiles.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autoDownloadToolStripMenuItem,
            this.toolStripSeparator4,
            this.downloadToolStripMenuItem,
            this.zipDownloadToolStripMenuItem,
            this.toolStripSeparator1,
            this.renameToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.refreshToolStripMenuItem1});
            this.contextMenuStripPutioFiles.Name = "contextMenuStripPutioFiles";
            this.contextMenuStripPutioFiles.Size = new System.Drawing.Size(162, 148);
            // 
            // autoDownloadToolStripMenuItem
            // 
            this.autoDownloadToolStripMenuItem.Name = "autoDownloadToolStripMenuItem";
            this.autoDownloadToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.autoDownloadToolStripMenuItem.Text = "Auto Download";
            this.autoDownloadToolStripMenuItem.Click += new System.EventHandler(this.autoDownloadToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(158, 6);
            // 
            // downloadToolStripMenuItem
            // 
            this.downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            this.downloadToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.downloadToolStripMenuItem.Text = "Download";
            this.downloadToolStripMenuItem.Click += new System.EventHandler(this.downloadToolStripMenuItem_Click);
            // 
            // zipDownloadToolStripMenuItem
            // 
            this.zipDownloadToolStripMenuItem.Name = "zipDownloadToolStripMenuItem";
            this.zipDownloadToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.zipDownloadToolStripMenuItem.Text = "Zip && Download";
            this.zipDownloadToolStripMenuItem.Click += new System.EventHandler(this.zipDownloadToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(158, 6);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // imageListTreeView
            // 
            this.imageListTreeView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTreeView.ImageStream")));
            this.imageListTreeView.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTreeView.Images.SetKeyName(0, "folder-closed");
            this.imageListTreeView.Images.SetKeyName(1, "folder-open");
            this.imageListTreeView.Images.SetKeyName(2, "document");
            this.imageListTreeView.Images.SetKeyName(3, "folder-icon-clean");
            // 
            // treeViewPutioFiles
            // 
            this.treeViewPutioFiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeViewPutioFiles.ContextMenuStrip = this.contextMenuStripPutioFiles;
            this.treeViewPutioFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewPutioFiles.HideSelection = false;
            this.treeViewPutioFiles.ImageIndex = 3;
            this.treeViewPutioFiles.ImageList = this.imageListTreeView;
            this.treeViewPutioFiles.Indent = 10;
            this.treeViewPutioFiles.LabelEdit = true;
            this.treeViewPutioFiles.Location = new System.Drawing.Point(0, 0);
            this.treeViewPutioFiles.Name = "treeViewPutioFiles";
            this.treeViewPutioFiles.SelectedImageIndex = 3;
            this.treeViewPutioFiles.ShowNodeToolTips = true;
            this.treeViewPutioFiles.Size = new System.Drawing.Size(194, 244);
            this.treeViewPutioFiles.TabIndex = 2;
            this.treeViewPutioFiles.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeViewPutioFiles_AfterLabelEdit);
            this.treeViewPutioFiles.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewPutioFiles_BeforeExpand);
            // 
            // groupBoxFoldersFiles
            // 
            this.groupBoxFoldersFiles.Controls.Add(this.splitContainerFiles);
            this.groupBoxFoldersFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxFoldersFiles.Location = new System.Drawing.Point(0, 0);
            this.groupBoxFoldersFiles.Name = "groupBoxFoldersFiles";
            this.groupBoxFoldersFiles.Size = new System.Drawing.Size(200, 398);
            this.groupBoxFoldersFiles.TabIndex = 3;
            this.groupBoxFoldersFiles.TabStop = false;
            this.groupBoxFoldersFiles.Text = "Folders && Files";
            // 
            // splitContainerFiles
            // 
            this.splitContainerFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerFiles.Location = new System.Drawing.Point(3, 16);
            this.splitContainerFiles.Name = "splitContainerFiles";
            this.splitContainerFiles.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerFiles.Panel1
            // 
            this.splitContainerFiles.Panel1.Controls.Add(this.treeViewPutioFiles);
            // 
            // splitContainerFiles.Panel2
            // 
            this.splitContainerFiles.Panel2.Controls.Add(this.treeViewAutoDownloads);
            this.splitContainerFiles.Size = new System.Drawing.Size(194, 379);
            this.splitContainerFiles.SplitterDistance = 244;
            this.splitContainerFiles.TabIndex = 4;
            // 
            // treeViewAutoDownloads
            // 
            this.treeViewAutoDownloads.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeViewAutoDownloads.ContextMenuStrip = this.contextMenuStripAutoDownload;
            this.treeViewAutoDownloads.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewAutoDownloads.FullRowSelect = true;
            this.treeViewAutoDownloads.HideSelection = false;
            this.treeViewAutoDownloads.Indent = 8;
            this.treeViewAutoDownloads.Location = new System.Drawing.Point(0, 0);
            this.treeViewAutoDownloads.Name = "treeViewAutoDownloads";
            this.treeViewAutoDownloads.ShowLines = false;
            this.treeViewAutoDownloads.ShowNodeToolTips = true;
            this.treeViewAutoDownloads.ShowPlusMinus = false;
            this.treeViewAutoDownloads.ShowRootLines = false;
            this.treeViewAutoDownloads.Size = new System.Drawing.Size(194, 131);
            this.treeViewAutoDownloads.TabIndex = 3;
            // 
            // contextMenuStripAutoDownload
            // 
            this.contextMenuStripAutoDownload.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem});
            this.contextMenuStripAutoDownload.Name = "contextMenuStripAutoDownload";
            this.contextMenuStripAutoDownload.Size = new System.Drawing.Size(118, 26);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // groupBoxDownloads
            // 
            this.groupBoxDownloads.Controls.Add(this.dataGridViewDownloads);
            this.groupBoxDownloads.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxDownloads.Location = new System.Drawing.Point(0, 0);
            this.groupBoxDownloads.Margin = new System.Windows.Forms.Padding(6);
            this.groupBoxDownloads.Name = "groupBoxDownloads";
            this.groupBoxDownloads.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxDownloads.Size = new System.Drawing.Size(418, 223);
            this.groupBoxDownloads.TabIndex = 5;
            this.groupBoxDownloads.TabStop = false;
            this.groupBoxDownloads.Text = "Downloads";
            // 
            // dataGridViewDownloads
            // 
            this.dataGridViewDownloads.AllowUserToAddRows = false;
            this.dataGridViewDownloads.AllowUserToDeleteRows = false;
            this.dataGridViewDownloads.AllowUserToResizeRows = false;
            this.dataGridViewDownloads.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewDownloads.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewDownloads.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewDownloads.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewDownloads.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDownloads.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnFile,
            this.ColumnType,
            this.ColumnStarted,
            this.ColumnCompleted,
            this.ColumnStatus});
            this.dataGridViewDownloads.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewDownloads.GridColor = System.Drawing.Color.Gray;
            this.dataGridViewDownloads.Location = new System.Drawing.Point(4, 17);
            this.dataGridViewDownloads.Name = "dataGridViewDownloads";
            this.dataGridViewDownloads.ReadOnly = true;
            this.dataGridViewDownloads.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridViewDownloads.RowHeadersVisible = false;
            this.dataGridViewDownloads.Size = new System.Drawing.Size(410, 202);
            this.dataGridViewDownloads.TabIndex = 0;
            // 
            // ColumnFile
            // 
            this.ColumnFile.HeaderText = "File";
            this.ColumnFile.Name = "ColumnFile";
            this.ColumnFile.ReadOnly = true;
            // 
            // ColumnType
            // 
            this.ColumnType.HeaderText = "Type";
            this.ColumnType.Name = "ColumnType";
            this.ColumnType.ReadOnly = true;
            // 
            // ColumnStarted
            // 
            this.ColumnStarted.HeaderText = "Started";
            this.ColumnStarted.Name = "ColumnStarted";
            this.ColumnStarted.ReadOnly = true;
            // 
            // ColumnCompleted
            // 
            this.ColumnCompleted.HeaderText = "Completed";
            this.ColumnCompleted.Name = "ColumnCompleted";
            this.ColumnCompleted.ReadOnly = true;
            // 
            // ColumnStatus
            // 
            this.ColumnStatus.HeaderText = "Status";
            this.ColumnStatus.Name = "ColumnStatus";
            this.ColumnStatus.ReadOnly = true;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 24);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 398);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBoxFoldersFiles);
            this.splitContainer1.Panel1MinSize = 200;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2MinSize = 300;
            this.splitContainer1.Size = new System.Drawing.Size(622, 398);
            this.splitContainer1.SplitterDistance = 200;
            this.splitContainer1.TabIndex = 6;
            // 
            // groupBoxTransfers
            // 
            this.groupBoxTransfers.Controls.Add(this.dataGridViewTransfers);
            this.groupBoxTransfers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxTransfers.Location = new System.Drawing.Point(0, 0);
            this.groupBoxTransfers.Margin = new System.Windows.Forms.Padding(6);
            this.groupBoxTransfers.Name = "groupBoxTransfers";
            this.groupBoxTransfers.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxTransfers.Size = new System.Drawing.Size(418, 171);
            this.groupBoxTransfers.TabIndex = 6;
            this.groupBoxTransfers.TabStop = false;
            this.groupBoxTransfers.Text = "Transfers";
            // 
            // dataGridViewTransfers
            // 
            this.dataGridViewTransfers.AllowUserToAddRows = false;
            this.dataGridViewTransfers.AllowUserToDeleteRows = false;
            this.dataGridViewTransfers.AllowUserToResizeRows = false;
            this.dataGridViewTransfers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTransfers.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewTransfers.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewTransfers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewTransfers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTransfers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.ColumnSize,
            this.ColumnPeers,
            this.ColumnUploaded,
            this.TransferStarted,
            this.dataGridViewTextBoxColumn5});
            this.dataGridViewTransfers.ContextMenuStrip = this.contextMenuStripTransfers;
            this.dataGridViewTransfers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewTransfers.GridColor = System.Drawing.Color.Gray;
            this.dataGridViewTransfers.Location = new System.Drawing.Point(4, 17);
            this.dataGridViewTransfers.Name = "dataGridViewTransfers";
            this.dataGridViewTransfers.ReadOnly = true;
            this.dataGridViewTransfers.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridViewTransfers.RowHeadersVisible = false;
            this.dataGridViewTransfers.Size = new System.Drawing.Size(410, 150);
            this.dataGridViewTransfers.TabIndex = 0;
            // 
            // contextMenuStripTransfers
            // 
            this.contextMenuStripTransfers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.cancelToolStripMenuItem,
            this.toolStripSeparator5,
            this.retryToolStripMenuItem,
            this.cleanToolStripMenuItem,
            this.refreshToolStripMenuItem});
            this.contextMenuStripTransfers.Name = "contextMenuStripTransfers";
            this.contextMenuStripTransfers.Size = new System.Drawing.Size(114, 120);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // cancelToolStripMenuItem
            // 
            this.cancelToolStripMenuItem.Name = "cancelToolStripMenuItem";
            this.cancelToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.cancelToolStripMenuItem.Text = "Cancel";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(110, 6);
            // 
            // retryToolStripMenuItem
            // 
            this.retryToolStripMenuItem.Name = "retryToolStripMenuItem";
            this.retryToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.retryToolStripMenuItem.Text = "Retry";
            // 
            // cleanToolStripMenuItem
            // 
            this.cleanToolStripMenuItem.Name = "cleanToolStripMenuItem";
            this.cleanToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cleanToolStripMenuItem.Text = "Clean";
            this.cleanToolStripMenuItem.Click += new System.EventHandler(this.cleanToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBoxDownloads);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBoxTransfers);
            this.splitContainer2.Size = new System.Drawing.Size(418, 398);
            this.splitContainer2.SplitterDistance = 223;
            this.splitContainer2.TabIndex = 7;
            // 
            // refreshToolStripMenuItem1
            // 
            this.refreshToolStripMenuItem1.Name = "refreshToolStripMenuItem1";
            this.refreshToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.refreshToolStripMenuItem1.Text = "Refresh";
            this.refreshToolStripMenuItem1.Click += new System.EventHandler(this.refreshToolStripMenuItem1_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "File";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // ColumnSize
            // 
            this.ColumnSize.HeaderText = "Size";
            this.ColumnSize.Name = "ColumnSize";
            this.ColumnSize.ReadOnly = true;
            // 
            // ColumnPeers
            // 
            this.ColumnPeers.HeaderText = "Peers";
            this.ColumnPeers.Name = "ColumnPeers";
            this.ColumnPeers.ReadOnly = true;
            // 
            // ColumnUploaded
            // 
            this.ColumnUploaded.HeaderText = "Uploaded";
            this.ColumnUploaded.Name = "ColumnUploaded";
            this.ColumnUploaded.ReadOnly = true;
            // 
            // TransferStarted
            // 
            this.TransferStarted.HeaderText = "Started";
            this.TransferStarted.Name = "TransferStarted";
            this.TransferStarted.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Status";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // FormPutioManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(625, 444);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.menuStripMain);
            this.MainMenuStrip = this.menuStripMain;
            this.MinimumSize = new System.Drawing.Size(641, 483);
            this.Name = "FormPutioManager";
            this.Text = "Put.io Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPutioManager_FormClosing);
            this.Load += new System.EventHandler(this.FormPutioManager_Load);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.contextMenuStripPutioFiles.ResumeLayout(false);
            this.groupBoxFoldersFiles.ResumeLayout(false);
            this.splitContainerFiles.Panel1.ResumeLayout(false);
            this.splitContainerFiles.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerFiles)).EndInit();
            this.splitContainerFiles.ResumeLayout(false);
            this.contextMenuStripAutoDownload.ResumeLayout(false);
            this.groupBoxDownloads.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDownloads)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBoxTransfers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTransfers)).EndInit();
            this.contextMenuStripTransfers.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ImageList imageListTreeView;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripPutioFiles;
        private System.Windows.Forms.TreeView treeViewPutioFiles;
        private System.Windows.Forms.GroupBox groupBoxFoldersFiles;
        private System.Windows.Forms.GroupBox groupBoxDownloads;
        private System.Windows.Forms.DataGridView dataGridViewDownloads;
        private System.Windows.Forms.ToolStripMenuItem downloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zipDownloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStarted;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCompleted;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStatus;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDownloadsFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoDownloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.TreeView treeViewAutoDownloads;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.SplitContainer splitContainerFiles;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripAutoDownload;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBoxTransfers;
        private System.Windows.Forms.DataGridView dataGridViewTransfers;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTransfers;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem retryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cleanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPeers;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnUploaded;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransferStarted;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    }
}

