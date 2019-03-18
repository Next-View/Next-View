/*
 * Erstellt mit SharpDevelop.
 * Benutzer: mschnell
 * Datum: 16.09.2011
 * Zeit: 15:23
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */
namespace Next_View
{
	partial class frmMain
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this.dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
			this.menuMain = new System.Windows.Forms.MenuStrip();
			this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuOpenImage = new System.Windows.Forms.ToolStripMenuItem();
			this.recentItem1 = new Next_View.RecentItem();
			this.N1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuRename = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuSaveOri = new System.Windows.Forms.ToolStripMenuItem();
			this.N2 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuOptions = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuStartEditor = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuSearch = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuLanguage = new System.Windows.Forms.ToolStripMenuItem();
			this.langEnglish = new System.Windows.Forms.ToolStripMenuItem();
			this.langGerman = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuNextImage = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuPriorImage = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFirstImage = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuLastImage = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuBack = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuForward = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuRefresh = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuFullScreen = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuRotateLeft = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuRotateRight = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuExif = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuShowImage = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuExifDash = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuHelp1 = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuWeb = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuGithub = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuTest = new System.Windows.Forms.ToolStripMenuItem();
			this.statusMain = new System.Windows.Forms.StatusStrip();
			this.statusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.progress1 = new System.Windows.Forms.ToolStripProgressBar();
			this.addCommentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.toolStrip2 = new System.Windows.Forms.ToolStrip();
			this.bnOpen = new System.Windows.Forms.ToolStripButton();
			this.bnStartEditor = new System.Windows.Forms.ToolStripButton();
			this.bnDelete = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.bnPrior = new System.Windows.Forms.ToolStripButton();
			this.bnNext = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.bnFullscreen = new System.Windows.Forms.ToolStripButton();
			this.bnExif = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.bnSearch = new System.Windows.Forms.ToolStripButton();
			this.bnHelp = new System.Windows.Forms.ToolStripButton();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.menuMain.SuspendLayout();
			this.statusMain.SuspendLayout();
			this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.toolStrip2.SuspendLayout();
			this.SuspendLayout();
			// 
			// dockPanel1
			// 
			this.dockPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dockPanel1.DockBackColor = System.Drawing.SystemColors.Control;
			this.dockPanel1.DockBottomPortion = 0.15D;
			this.dockPanel1.DockLeftPortion = 0.15D;
			this.dockPanel1.DockRightPortion = 0.15D;
			this.dockPanel1.DockTopPortion = 0.15D;
			this.dockPanel1.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingSdi;
			this.dockPanel1.Location = new System.Drawing.Point(0, 0);
			this.dockPanel1.Name = "dockPanel1";
			this.dockPanel1.Size = new System.Drawing.Size(884, 484);
			this.dockPanel1.TabIndex = 0;
			// 
			// menuMain
			// 
			this.menuMain.Dock = System.Windows.Forms.DockStyle.None;
			this.menuMain.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.mnuFile,
			this.mnuEdit,
			this.mnuView,
			this.mnuHelp});
			this.menuMain.Location = new System.Drawing.Point(0, 31);
			this.menuMain.Name = "menuMain";
			this.menuMain.Size = new System.Drawing.Size(884, 24);
			this.menuMain.TabIndex = 0;
			this.menuMain.Text = "menuMain";
			// 
			// mnuFile
			// 
			this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.mnuOpenImage,
			this.recentItem1,
			this.N1,
			this.mnuRename,
			this.mnuDelete,
			this.mnuSaveOri,
			this.N2,
			this.mnuExit});
			this.mnuFile.Name = "mnuFile";
			this.mnuFile.Size = new System.Drawing.Size(37, 20);
			this.mnuFile.Text = "File";
			// 
			// mnuOpenImage
			// 
			this.mnuOpenImage.Image = ((System.Drawing.Image)(resources.GetObject("mnuOpenImage.Image")));
			this.mnuOpenImage.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.mnuOpenImage.ImageTransparentColor = System.Drawing.Color.DimGray;
			this.mnuOpenImage.Name = "mnuOpenImage";
			this.mnuOpenImage.ShortcutKeyDisplayString = "Ctrl + O";
			this.mnuOpenImage.Size = new System.Drawing.Size(207, 22);
			this.mnuOpenImage.Text = "Open...";
			this.mnuOpenImage.Click += new System.EventHandler(this.MnuOpenImageClick);
			// 
			// recentItem1
			// 
			this.recentItem1.Enabled = false;
			this.recentItem1.Name = "recentItem1";
			this.recentItem1.Size = new System.Drawing.Size(207, 22);
			this.recentItem1.Text = "Recent images";
			// 
			// N1
			// 
			this.N1.Name = "N1";
			this.N1.Size = new System.Drawing.Size(204, 6);
			// 
			// mnuRename
			// 
			this.mnuRename.Name = "mnuRename";
			this.mnuRename.ShortcutKeyDisplayString = "F2";
			this.mnuRename.Size = new System.Drawing.Size(207, 22);
			this.mnuRename.Text = "Rename...             ";
			this.mnuRename.Click += new System.EventHandler(this.MnuRenameClick);
			// 
			// mnuDelete
			// 
			this.mnuDelete.Name = "mnuDelete";
			this.mnuDelete.ShortcutKeyDisplayString = "Del";
			this.mnuDelete.Size = new System.Drawing.Size(207, 22);
			this.mnuDelete.Text = "Delete";
			this.mnuDelete.Click += new System.EventHandler(this.MnuDeleteClick);
			// 
			// mnuSaveOri
			// 
			this.mnuSaveOri.Name = "mnuSaveOri";
			this.mnuSaveOri.ShortcutKeyDisplayString = "Ctrl + S";
			this.mnuSaveOri.Size = new System.Drawing.Size(207, 22);
			this.mnuSaveOri.Text = "Save Orientation";
			this.mnuSaveOri.Click += new System.EventHandler(this.MnuSaveOriClick);
			// 
			// N2
			// 
			this.N2.Name = "N2";
			this.N2.Size = new System.Drawing.Size(204, 6);
			// 
			// mnuExit
			// 
			this.mnuExit.Image = ((System.Drawing.Image)(resources.GetObject("mnuExit.Image")));
			this.mnuExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.mnuExit.Name = "mnuExit";
			this.mnuExit.Size = new System.Drawing.Size(207, 22);
			this.mnuExit.Text = "Exit";
			this.mnuExit.Click += new System.EventHandler(this.MnuExitClick);
			// 
			// mnuEdit
			// 
			this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.mnuOptions,
			this.mnuStartEditor,
			this.mnuSearch,
			this.mnuLanguage});
			this.mnuEdit.Name = "mnuEdit";
			this.mnuEdit.Size = new System.Drawing.Size(39, 20);
			this.mnuEdit.Text = "Edit";
			// 
			// mnuOptions
			// 
			this.mnuOptions.Image = ((System.Drawing.Image)(resources.GetObject("mnuOptions.Image")));
			this.mnuOptions.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.mnuOptions.Name = "mnuOptions";
			this.mnuOptions.Size = new System.Drawing.Size(141, 22);
			this.mnuOptions.Text = "Options...";
			this.mnuOptions.Click += new System.EventHandler(this.MnuOptionsClick);
			// 
			// mnuStartEditor
			// 
			this.mnuStartEditor.Name = "mnuStartEditor";
			this.mnuStartEditor.Size = new System.Drawing.Size(141, 22);
			this.mnuStartEditor.Text = "Start editor...";
			this.mnuStartEditor.Click += new System.EventHandler(this.MnuStartEditorClick);
			// 
			// mnuSearch
			// 
			this.mnuSearch.Image = ((System.Drawing.Image)(resources.GetObject("mnuSearch.Image")));
			this.mnuSearch.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.mnuSearch.Name = "mnuSearch";
			this.mnuSearch.Size = new System.Drawing.Size(141, 22);
			this.mnuSearch.Text = "Search...";
			this.mnuSearch.Click += new System.EventHandler(this.MnuSearchClick);
			// 
			// mnuLanguage
			// 
			this.mnuLanguage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.langEnglish,
			this.langGerman});
			this.mnuLanguage.Name = "mnuLanguage";
			this.mnuLanguage.Size = new System.Drawing.Size(141, 22);
			this.mnuLanguage.Text = "Language";
			// 
			// langEnglish
			// 
			this.langEnglish.Image = ((System.Drawing.Image)(resources.GetObject("langEnglish.Image")));
			this.langEnglish.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.langEnglish.Name = "langEnglish";
			this.langEnglish.Size = new System.Drawing.Size(116, 22);
			this.langEnglish.Text = "English";
			this.langEnglish.Click += new System.EventHandler(this.LangEnglishClick);
			// 
			// langGerman
			// 
			this.langGerman.Image = ((System.Drawing.Image)(resources.GetObject("langGerman.Image")));
			this.langGerman.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.langGerman.Name = "langGerman";
			this.langGerman.Size = new System.Drawing.Size(116, 22);
			this.langGerman.Text = "German";
			this.langGerman.Click += new System.EventHandler(this.LangGermanClick);
			// 
			// mnuView
			// 
			this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.mnuNextImage,
			this.mnuPriorImage,
			this.mnuFirstImage,
			this.mnuLastImage,
			this.toolStripSeparator1,
			this.mnuBack,
			this.mnuForward,
			this.toolStripSeparator2,
			this.mnuRefresh,
			this.mnuFullScreen,
			this.toolStripSeparator5,
			this.mnuRotateLeft,
			this.mnuRotateRight,
			this.toolStripSeparator6,
			this.mnuExif,
			this.mnuShowImage,
			this.mnuExifDash});
			this.mnuView.Name = "mnuView";
			this.mnuView.Size = new System.Drawing.Size(44, 20);
			this.mnuView.Text = "View";
			// 
			// mnuNextImage
			// 
			this.mnuNextImage.Image = ((System.Drawing.Image)(resources.GetObject("mnuNextImage.Image")));
			this.mnuNextImage.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.mnuNextImage.Name = "mnuNextImage";
			this.mnuNextImage.ShortcutKeyDisplayString = "->";
			this.mnuNextImage.Size = new System.Drawing.Size(214, 22);
			this.mnuNextImage.Text = "Next Image                    ";
			this.mnuNextImage.Click += new System.EventHandler(this.MnuNextImageClick);
			// 
			// mnuPriorImage
			// 
			this.mnuPriorImage.Image = ((System.Drawing.Image)(resources.GetObject("mnuPriorImage.Image")));
			this.mnuPriorImage.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.mnuPriorImage.Name = "mnuPriorImage";
			this.mnuPriorImage.ShortcutKeyDisplayString = "<-";
			this.mnuPriorImage.Size = new System.Drawing.Size(214, 22);
			this.mnuPriorImage.Text = "Prior Image";
			this.mnuPriorImage.Click += new System.EventHandler(this.MnuPriorImageClick);
			// 
			// mnuFirstImage
			// 
			this.mnuFirstImage.Image = ((System.Drawing.Image)(resources.GetObject("mnuFirstImage.Image")));
			this.mnuFirstImage.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.mnuFirstImage.Name = "mnuFirstImage";
			this.mnuFirstImage.ShortcutKeyDisplayString = "Pos 1";
			this.mnuFirstImage.Size = new System.Drawing.Size(214, 22);
			this.mnuFirstImage.Text = "First Image";
			this.mnuFirstImage.Click += new System.EventHandler(this.MnuFirstImageClick);
			// 
			// mnuLastImage
			// 
			this.mnuLastImage.Image = ((System.Drawing.Image)(resources.GetObject("mnuLastImage.Image")));
			this.mnuLastImage.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.mnuLastImage.Name = "mnuLastImage";
			this.mnuLastImage.ShortcutKeyDisplayString = "End";
			this.mnuLastImage.Size = new System.Drawing.Size(214, 22);
			this.mnuLastImage.Text = "Last Image";
			this.mnuLastImage.Click += new System.EventHandler(this.MnuLastImageClick);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(211, 6);
			// 
			// mnuBack
			// 
			this.mnuBack.Name = "mnuBack";
			this.mnuBack.ShortcutKeyDisplayString = "ALT + <-";
			this.mnuBack.Size = new System.Drawing.Size(214, 22);
			this.mnuBack.Text = "Back";
			this.mnuBack.Click += new System.EventHandler(this.MnuBackClick);
			// 
			// mnuForward
			// 
			this.mnuForward.Name = "mnuForward";
			this.mnuForward.ShortcutKeyDisplayString = "ALT + ->";
			this.mnuForward.Size = new System.Drawing.Size(214, 22);
			this.mnuForward.Text = "Forward";
			this.mnuForward.Click += new System.EventHandler(this.MnuForwardClick);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(211, 6);
			// 
			// mnuRefresh
			// 
			this.mnuRefresh.Name = "mnuRefresh";
			this.mnuRefresh.ShortcutKeyDisplayString = "F5";
			this.mnuRefresh.Size = new System.Drawing.Size(214, 22);
			this.mnuRefresh.Text = "Refresh";
			this.mnuRefresh.Click += new System.EventHandler(this.MnuRefreshClick);
			// 
			// mnuFullScreen
			// 
			this.mnuFullScreen.Name = "mnuFullScreen";
			this.mnuFullScreen.ShortcutKeyDisplayString = "Enter";
			this.mnuFullScreen.Size = new System.Drawing.Size(214, 22);
			this.mnuFullScreen.Text = "Full screen";
			this.mnuFullScreen.Click += new System.EventHandler(this.MnuFullScreenClick);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(211, 6);
			// 
			// mnuRotateLeft
			// 
			this.mnuRotateLeft.Name = "mnuRotateLeft";
			this.mnuRotateLeft.ShortcutKeyDisplayString = "L";
			this.mnuRotateLeft.Size = new System.Drawing.Size(214, 22);
			this.mnuRotateLeft.Text = "Rotate left";
			this.mnuRotateLeft.Click += new System.EventHandler(this.MnuRotateLeftClick);
			// 
			// mnuRotateRight
			// 
			this.mnuRotateRight.Name = "mnuRotateRight";
			this.mnuRotateRight.ShortcutKeyDisplayString = "R";
			this.mnuRotateRight.Size = new System.Drawing.Size(214, 22);
			this.mnuRotateRight.Text = "Rotate right";
			this.mnuRotateRight.Click += new System.EventHandler(this.MnuRotateRightClick);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(211, 6);
			// 
			// mnuExif
			// 
			this.mnuExif.Name = "mnuExif";
			this.mnuExif.Size = new System.Drawing.Size(214, 22);
			this.mnuExif.Text = "Exif...";
			this.mnuExif.Click += new System.EventHandler(this.MnuExifClick);
			// 
			// mnuShowImage
			// 
			this.mnuShowImage.Name = "mnuShowImage";
			this.mnuShowImage.Size = new System.Drawing.Size(214, 22);
			this.mnuShowImage.Text = "Show image";
			this.mnuShowImage.Click += new System.EventHandler(this.MnuShowImageClick);
			// 
			// mnuExifDash
			// 
			this.mnuExifDash.Name = "mnuExifDash";
			this.mnuExifDash.Size = new System.Drawing.Size(214, 22);
			this.mnuExifDash.Text = "Exif dashboard...";
			this.mnuExifDash.Visible = false;
			this.mnuExifDash.Click += new System.EventHandler(this.MnuExifDashClick);
			// 
			// mnuHelp
			// 
			this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.mnuHelp1,
			this.mnuWeb,
			this.mnuGithub,
			this.mnuAbout,
			this.mnuTest});
			this.mnuHelp.Name = "mnuHelp";
			this.mnuHelp.Size = new System.Drawing.Size(44, 20);
			this.mnuHelp.Text = "Help";
			// 
			// mnuHelp1
			// 
			this.mnuHelp1.Image = ((System.Drawing.Image)(resources.GetObject("mnuHelp1.Image")));
			this.mnuHelp1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.mnuHelp1.Name = "mnuHelp1";
			this.mnuHelp1.Size = new System.Drawing.Size(142, 22);
			this.mnuHelp1.Text = "Help";
			this.mnuHelp1.Click += new System.EventHandler(this.MnuHelp1Click);
			// 
			// mnuWeb
			// 
			this.mnuWeb.Name = "mnuWeb";
			this.mnuWeb.Size = new System.Drawing.Size(142, 22);
			this.mnuWeb.Text = "Homepage...";
			this.mnuWeb.Click += new System.EventHandler(this.MnuWebClick);
			// 
			// mnuGithub
			// 
			this.mnuGithub.CheckOnClick = true;
			this.mnuGithub.Name = "mnuGithub";
			this.mnuGithub.Size = new System.Drawing.Size(142, 22);
			this.mnuGithub.Text = "Github";
			this.mnuGithub.Click += new System.EventHandler(this.MnuGithubClick);
			// 
			// mnuAbout
			// 
			this.mnuAbout.Name = "mnuAbout";
			this.mnuAbout.Size = new System.Drawing.Size(142, 22);
			this.mnuAbout.Text = "About...";
			this.mnuAbout.Click += new System.EventHandler(this.MnuAboutClick);
			// 
			// mnuTest
			// 
			this.mnuTest.Name = "mnuTest";
			this.mnuTest.Size = new System.Drawing.Size(142, 22);
			this.mnuTest.Text = "Test";
			this.mnuTest.Visible = false;
			this.mnuTest.Click += new System.EventHandler(this.MnuTestClick);
			// 
			// statusMain
			// 
			this.statusMain.AutoSize = false;
			this.statusMain.Dock = System.Windows.Forms.DockStyle.None;
			this.statusMain.ImageScalingSize = new System.Drawing.Size(22, 22);
			this.statusMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.statusLabel1,
			this.progress1});
			this.statusMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.statusMain.Location = new System.Drawing.Point(0, 0);
			this.statusMain.Name = "statusMain";
			this.statusMain.Size = new System.Drawing.Size(884, 23);
			this.statusMain.TabIndex = 3;
			this.statusMain.Text = " ";
			// 
			// statusLabel1
			// 
			this.statusLabel1.Name = "statusLabel1";
			this.statusLabel1.Size = new System.Drawing.Size(19, 18);
			this.statusLabel1.Text = "    ";
			this.statusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// progress1
			// 
			this.progress1.Name = "progress1";
			this.progress1.Size = new System.Drawing.Size(400, 17);
			// 
			// addCommentToolStripMenuItem
			// 
			this.addCommentToolStripMenuItem.Name = "addCommentToolStripMenuItem";
			this.addCommentToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.BottomToolStripPanel
			// 
			this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusMain);
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.AutoScroll = true;
			this.toolStripContainer1.ContentPanel.Controls.Add(this.dockPanel1);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(884, 484);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(884, 562);
			this.toolStripContainer1.TabIndex = 4;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip2);
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuMain);
			// 
			// toolStrip2
			// 
			this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.bnOpen,
			this.bnStartEditor,
			this.bnDelete,
			this.toolStripSeparator,
			this.bnPrior,
			this.bnNext,
			this.toolStripSeparator4,
			this.bnFullscreen,
			this.bnExif,
			this.toolStripSeparator3,
			this.bnSearch,
			this.bnHelp});
			this.toolStrip2.Location = new System.Drawing.Point(3, 0);
			this.toolStrip2.Name = "toolStrip2";
			this.toolStrip2.Size = new System.Drawing.Size(256, 31);
			this.toolStrip2.TabIndex = 1;
			// 
			// bnOpen
			// 
			this.bnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bnOpen.Image = ((System.Drawing.Image)(resources.GetObject("bnOpen.Image")));
			this.bnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.bnOpen.Name = "bnOpen";
			this.bnOpen.Size = new System.Drawing.Size(24, 28);
			this.bnOpen.Text = "Open";
			this.bnOpen.Click += new System.EventHandler(this.BnOpenClick);
			// 
			// bnStartEditor
			// 
			this.bnStartEditor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bnStartEditor.Image = ((System.Drawing.Image)(resources.GetObject("bnStartEditor.Image")));
			this.bnStartEditor.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.bnStartEditor.Name = "bnStartEditor";
			this.bnStartEditor.Size = new System.Drawing.Size(24, 28);
			this.bnStartEditor.Text = "Start editor";
			this.bnStartEditor.Click += new System.EventHandler(this.BnStartEditorClick);
			// 
			// bnDelete
			// 
			this.bnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bnDelete.Image = ((System.Drawing.Image)(resources.GetObject("bnDelete.Image")));
			this.bnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.bnDelete.Name = "bnDelete";
			this.bnDelete.Size = new System.Drawing.Size(24, 28);
			this.bnDelete.Text = "Delete image";
			this.bnDelete.Click += new System.EventHandler(this.BnDeleteClick);
			// 
			// toolStripSeparator
			// 
			this.toolStripSeparator.Name = "toolStripSeparator";
			this.toolStripSeparator.Size = new System.Drawing.Size(6, 31);
			// 
			// bnPrior
			// 
			this.bnPrior.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bnPrior.Image = ((System.Drawing.Image)(resources.GetObject("bnPrior.Image")));
			this.bnPrior.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.bnPrior.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.bnPrior.Name = "bnPrior";
			this.bnPrior.Size = new System.Drawing.Size(28, 28);
			this.bnPrior.Text = "Prior image";
			this.bnPrior.Click += new System.EventHandler(this.BnPriorClick);
			// 
			// bnNext
			// 
			this.bnNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bnNext.Image = ((System.Drawing.Image)(resources.GetObject("bnNext.Image")));
			this.bnNext.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.bnNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.bnNext.Name = "bnNext";
			this.bnNext.Size = new System.Drawing.Size(28, 28);
			this.bnNext.Text = "Next image";
			this.bnNext.Click += new System.EventHandler(this.BnNextClick);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
			// 
			// bnFullscreen
			// 
			this.bnFullscreen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bnFullscreen.Image = ((System.Drawing.Image)(resources.GetObject("bnFullscreen.Image")));
			this.bnFullscreen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.bnFullscreen.Name = "bnFullscreen";
			this.bnFullscreen.Size = new System.Drawing.Size(24, 28);
			this.bnFullscreen.Text = "Fullscreen";
			this.bnFullscreen.Click += new System.EventHandler(this.BnFullscreenClick);
			// 
			// bnExif
			// 
			this.bnExif.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bnExif.Image = ((System.Drawing.Image)(resources.GetObject("bnExif.Image")));
			this.bnExif.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.bnExif.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.bnExif.Name = "bnExif";
			this.bnExif.Size = new System.Drawing.Size(26, 28);
			this.bnExif.Text = "Exif data";
			this.bnExif.Click += new System.EventHandler(this.BnExifClick);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			// 
			// bnSearch
			// 
			this.bnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bnSearch.Image = ((System.Drawing.Image)(resources.GetObject("bnSearch.Image")));
			this.bnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.bnSearch.Name = "bnSearch";
			this.bnSearch.Size = new System.Drawing.Size(24, 28);
			this.bnSearch.Text = "Search";
			this.bnSearch.Click += new System.EventHandler(this.BnSearchClick);
			// 
			// bnHelp
			// 
			this.bnHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bnHelp.Image = ((System.Drawing.Image)(resources.GetObject("bnHelp.Image")));
			this.bnHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.bnHelp.Name = "bnHelp";
			this.bnHelp.Size = new System.Drawing.Size(24, 28);
			this.bnHelp.Text = "Help";
			this.bnHelp.Click += new System.EventHandler(this.BnHelpClick);
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "exif-1.png");
			this.imageList1.Images.SetKeyName(1, "exif-2.png");
			this.imageList1.Images.SetKeyName(2, "exif-3.png");
			this.imageList1.Images.SetKeyName(3, "exif-4.png");
			// 
			// frmMain
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(884, 562);
			this.Controls.Add(this.toolStripContainer1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuMain;
			this.MinimumSize = new System.Drawing.Size(378, 159);
			this.Name = "frmMain";
			this.Text = "Next-View";
			this.Activated += new System.EventHandler(this.FrmMainActivated);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMainFormClosed);
			this.Load += new System.EventHandler(this.FrmMainLoad);
			this.Shown += new System.EventHandler(this.FrmMainShown);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FrmMainDragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FrmMainDragEnter);
			this.DragOver += new System.Windows.Forms.DragEventHandler(this.FrmMainDragOver);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMainKeyDown);
			this.menuMain.ResumeLayout(false);
			this.menuMain.PerformLayout();
			this.statusMain.ResumeLayout(false);
			this.statusMain.PerformLayout();
			this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
			this.ResumeLayout(false);

		}
		private System.Windows.Forms.ToolStripMenuItem mnuNextImage;
		private System.Windows.Forms.ToolStripSeparator N2;
		private System.Windows.Forms.ToolStripMenuItem mnuView;
		private System.Windows.Forms.ToolStripMenuItem mnuPriorImage;
		private System.Windows.Forms.ToolStripMenuItem mnuHelp1;
		private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel1;
		private System.Windows.Forms.ToolStripMenuItem addCommentToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton bnOpen;
		private System.Windows.Forms.ToolStripButton bnHelp;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
		private System.Windows.Forms.ToolStrip toolStrip2;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.ToolStripMenuItem mnuAbout;
		private System.Windows.Forms.ToolStripMenuItem mnuWeb;
		private System.Windows.Forms.ToolStripMenuItem mnuHelp;
		private System.Windows.Forms.StatusStrip statusMain;
		private System.Windows.Forms.MenuStrip menuMain;
		private System.Windows.Forms.ToolStripMenuItem mnuExit;
		private System.Windows.Forms.ToolStripSeparator N1;
		private System.Windows.Forms.ToolStripMenuItem mnuOpenImage;
		private System.Windows.Forms.ToolStripMenuItem mnuFile;
		private System.Windows.Forms.ToolStripMenuItem mnuFirstImage;
		private System.Windows.Forms.ToolStripStatusLabel statusLabel1;
		private System.Windows.Forms.ToolStripMenuItem mnuLastImage;
		private System.Windows.Forms.ToolStripMenuItem mnuShowImage;
		private System.Windows.Forms.ToolStripMenuItem mnuTest;
		private System.Windows.Forms.ToolStripMenuItem mnuRename;
		private System.Windows.Forms.ToolStripMenuItem mnuDelete;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem mnuBack;
		private System.Windows.Forms.ToolStripMenuItem mnuForward;
		private System.Windows.Forms.ToolStripMenuItem mnuRefresh;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem mnuFullScreen;
		private System.Windows.Forms.ToolStripMenuItem mnuEdit;
		private System.Windows.Forms.ToolStripMenuItem mnuOptions;
		private System.Windows.Forms.ToolStripButton bnPrior;
		private System.Windows.Forms.ToolStripButton bnNext;
		private System.Windows.Forms.ToolStripButton bnDelete;
		private System.Windows.Forms.ToolStripButton bnFullscreen;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton bnStartEditor;
		private System.Windows.Forms.ToolStripMenuItem mnuStartEditor;
		private System.Windows.Forms.ToolStripMenuItem mnuSearch;
		private System.Windows.Forms.ToolStripMenuItem mnuGithub;
		private Next_View.RecentItem recentItem1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripButton bnSearch;
		private System.Windows.Forms.ToolStripMenuItem mnuLanguage;
		private System.Windows.Forms.ToolStripMenuItem langEnglish;
		private System.Windows.Forms.ToolStripMenuItem langGerman;
		private System.Windows.Forms.ToolStripButton bnExif;
		private System.Windows.Forms.ImageList imageList1;
		internal System.Windows.Forms.ToolStripMenuItem mnuExif;
		private System.Windows.Forms.ToolStripMenuItem mnuExifDash;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripProgressBar progress1;
		private System.Windows.Forms.ToolStripMenuItem mnuRotateLeft;
		private System.Windows.Forms.ToolStripMenuItem mnuRotateRight;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripMenuItem mnuSaveOri;
	}
}
