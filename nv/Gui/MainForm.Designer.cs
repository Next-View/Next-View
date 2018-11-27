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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this.dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
			this.menuMain = new System.Windows.Forms.MenuStrip();
			this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuOpenImage = new System.Windows.Forms.ToolStripMenuItem();
			this.N1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuRename = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.N2 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
			this.recentItem1 = new Next_View.RecentItem();
			this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuOptions = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuStartEditor = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuSearch = new System.Windows.Forms.ToolStripMenuItem();
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
			this.mnuShowPanel = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuHelp1 = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuWeb = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuGithub = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuTest = new System.Windows.Forms.ToolStripMenuItem();
			this.statusMain = new System.Windows.Forms.StatusStrip();
			this.statusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.addCommentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.toolStrip2 = new System.Windows.Forms.ToolStrip();
			this.bnOpen = new System.Windows.Forms.ToolStripButton();
			this.bnStartEditor = new System.Windows.Forms.ToolStripButton();
			this.bnDelete = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.bnPrior = new System.Windows.Forms.ToolStripButton();
			this.bnNext = new System.Windows.Forms.ToolStripButton();
			this.bnFullscreen = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.bnHelp = new System.Windows.Forms.ToolStripButton();
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
			this.dockPanel1.Size = new System.Drawing.Size(884, 482);
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
			this.menuMain.Location = new System.Drawing.Point(0, 27);
			this.menuMain.Name = "menuMain";
			this.menuMain.Size = new System.Drawing.Size(884, 28);
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
			this.N2,
			this.mnuExit});
			this.mnuFile.Name = "mnuFile";
			this.mnuFile.Size = new System.Drawing.Size(44, 24);
			this.mnuFile.Text = "&File";
			// 
			// mnuOpenImage
			// 
			this.mnuOpenImage.Image = ((System.Drawing.Image)(resources.GetObject("mnuOpenImage.Image")));
			this.mnuOpenImage.ImageTransparentColor = System.Drawing.Color.DimGray;
			this.mnuOpenImage.Name = "mnuOpenImage";
			this.mnuOpenImage.ShortcutKeyDisplayString = "Ctrl + O";
			this.mnuOpenImage.Size = new System.Drawing.Size(223, 26);
			this.mnuOpenImage.Text = "&Open...";
			this.mnuOpenImage.Click += new System.EventHandler(this.MnuOpenImageClick);
			// 
			// N1
			// 
			this.N1.Name = "N1";
			this.N1.Size = new System.Drawing.Size(220, 6);
			// 
			// mnuRename
			// 
			this.mnuRename.Name = "mnuRename";
			this.mnuRename.ShortcutKeyDisplayString = "F2";
			this.mnuRename.Size = new System.Drawing.Size(223, 26);
			this.mnuRename.Text = "&Rename...             ";
			this.mnuRename.Click += new System.EventHandler(this.MnuRenameClick);
			// 
			// mnuDelete
			// 
			this.mnuDelete.Name = "mnuDelete";
			this.mnuDelete.ShortcutKeyDisplayString = "Del";
			this.mnuDelete.Size = new System.Drawing.Size(223, 26);
			this.mnuDelete.Text = "&Delete";
			this.mnuDelete.Click += new System.EventHandler(this.MnuDeleteClick);
			// 
			// N2
			// 
			this.N2.Name = "N2";
			this.N2.Size = new System.Drawing.Size(220, 6);
			// 
			// mnuExit
			// 
			this.mnuExit.Image = ((System.Drawing.Image)(resources.GetObject("mnuExit.Image")));
			this.mnuExit.Name = "mnuExit";
			this.mnuExit.Size = new System.Drawing.Size(223, 26);
			this.mnuExit.Text = "&Exit";
			this.mnuExit.Click += new System.EventHandler(this.MnuExitClick);
			// 
			// recentItem1
			// 
			this.recentItem1.Enabled = false;
			this.recentItem1.Name = "recentItem1";
			this.recentItem1.Size = new System.Drawing.Size(223, 26);
			this.recentItem1.Text = "&Recent images";
			// 
			// mnuEdit
			// 
			this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.mnuOptions,
			this.mnuStartEditor,
			this.mnuSearch});
			this.mnuEdit.Name = "mnuEdit";
			this.mnuEdit.Size = new System.Drawing.Size(47, 24);
			this.mnuEdit.Text = "&Edit";
			// 
			// mnuOptions
			// 
			this.mnuOptions.Image = ((System.Drawing.Image)(resources.GetObject("mnuOptions.Image")));
			this.mnuOptions.Name = "mnuOptions";
			this.mnuOptions.Size = new System.Drawing.Size(168, 26);
			this.mnuOptions.Text = "&Options...";
			this.mnuOptions.Click += new System.EventHandler(this.MnuOptionsClick);
			// 
			// mnuStartEditor
			// 
			this.mnuStartEditor.Name = "mnuStartEditor";
			this.mnuStartEditor.Size = new System.Drawing.Size(168, 26);
			this.mnuStartEditor.Text = "&Start editor...";
			this.mnuStartEditor.Click += new System.EventHandler(this.MnuStartEditorClick);
			// 
			// mnuSearch
			// 
			this.mnuSearch.Image = ((System.Drawing.Image)(resources.GetObject("mnuSearch.Image")));
			this.mnuSearch.Name = "mnuSearch";
			this.mnuSearch.Size = new System.Drawing.Size(168, 26);
			this.mnuSearch.Text = "&Search...";
			this.mnuSearch.Click += new System.EventHandler(this.MnuSearchClick);
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
			this.mnuShowPanel});
			this.mnuView.Name = "mnuView";
			this.mnuView.Size = new System.Drawing.Size(53, 24);
			this.mnuView.Text = "&View";
			// 
			// mnuNextImage
			// 
			this.mnuNextImage.Image = ((System.Drawing.Image)(resources.GetObject("mnuNextImage.Image")));
			this.mnuNextImage.Name = "mnuNextImage";
			this.mnuNextImage.ShortcutKeyDisplayString = "->";
			this.mnuNextImage.Size = new System.Drawing.Size(266, 26);
			this.mnuNextImage.Text = "&Next Image                    ";
			this.mnuNextImage.Click += new System.EventHandler(this.MnuNextImageClick);
			// 
			// mnuPriorImage
			// 
			this.mnuPriorImage.Image = ((System.Drawing.Image)(resources.GetObject("mnuPriorImage.Image")));
			this.mnuPriorImage.Name = "mnuPriorImage";
			this.mnuPriorImage.ShortcutKeyDisplayString = "<-";
			this.mnuPriorImage.Size = new System.Drawing.Size(266, 26);
			this.mnuPriorImage.Text = "&Prior Image";
			this.mnuPriorImage.Click += new System.EventHandler(this.MnuPriorImageClick);
			// 
			// mnuFirstImage
			// 
			this.mnuFirstImage.Image = ((System.Drawing.Image)(resources.GetObject("mnuFirstImage.Image")));
			this.mnuFirstImage.Name = "mnuFirstImage";
			this.mnuFirstImage.ShortcutKeyDisplayString = "Pos 1";
			this.mnuFirstImage.Size = new System.Drawing.Size(266, 26);
			this.mnuFirstImage.Text = "&First Image";
			this.mnuFirstImage.Click += new System.EventHandler(this.MnuFirstImageClick);
			// 
			// mnuLastImage
			// 
			this.mnuLastImage.Image = ((System.Drawing.Image)(resources.GetObject("mnuLastImage.Image")));
			this.mnuLastImage.Name = "mnuLastImage";
			this.mnuLastImage.ShortcutKeyDisplayString = "End";
			this.mnuLastImage.Size = new System.Drawing.Size(266, 26);
			this.mnuLastImage.Text = "&Last Image";
			this.mnuLastImage.Click += new System.EventHandler(this.MnuLastImageClick);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(263, 6);
			// 
			// mnuBack
			// 
			this.mnuBack.Name = "mnuBack";
			this.mnuBack.ShortcutKeyDisplayString = "ALT + <-";
			this.mnuBack.Size = new System.Drawing.Size(266, 26);
			this.mnuBack.Text = "&Back";
			this.mnuBack.Click += new System.EventHandler(this.MnuBackClick);
			// 
			// mnuForward
			// 
			this.mnuForward.Name = "mnuForward";
			this.mnuForward.ShortcutKeyDisplayString = "ALT + ->";
			this.mnuForward.Size = new System.Drawing.Size(266, 26);
			this.mnuForward.Text = "&Forward";
			this.mnuForward.Click += new System.EventHandler(this.MnuForwardClick);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(263, 6);
			// 
			// mnuRefresh
			// 
			this.mnuRefresh.Name = "mnuRefresh";
			this.mnuRefresh.ShortcutKeyDisplayString = "F5";
			this.mnuRefresh.Size = new System.Drawing.Size(266, 26);
			this.mnuRefresh.Text = "&Refresh";
			this.mnuRefresh.Click += new System.EventHandler(this.MnuRefreshClick);
			// 
			// mnuFullScreen
			// 
			this.mnuFullScreen.Name = "mnuFullScreen";
			this.mnuFullScreen.ShortcutKeyDisplayString = "Enter";
			this.mnuFullScreen.Size = new System.Drawing.Size(266, 26);
			this.mnuFullScreen.Text = "&Full screen";
			this.mnuFullScreen.Click += new System.EventHandler(this.MnuFullScreenClick);
			// 
			// mnuShowPanel
			// 
			this.mnuShowPanel.Name = "mnuShowPanel";
			this.mnuShowPanel.Size = new System.Drawing.Size(266, 26);
			this.mnuShowPanel.Text = "Show Panel";
			this.mnuShowPanel.Visible = false;
			this.mnuShowPanel.Click += new System.EventHandler(this.MnuShowPanelClick);
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
			this.mnuHelp.Size = new System.Drawing.Size(53, 24);
			this.mnuHelp.Text = "&Help";
			// 
			// mnuHelp1
			// 
			this.mnuHelp1.Image = ((System.Drawing.Image)(resources.GetObject("mnuHelp1.Image")));
			this.mnuHelp1.Name = "mnuHelp1";
			this.mnuHelp1.Size = new System.Drawing.Size(168, 26);
			this.mnuHelp1.Text = "&Help";
			this.mnuHelp1.Click += new System.EventHandler(this.MnuHelp1Click);
			// 
			// mnuWeb
			// 
			this.mnuWeb.Name = "mnuWeb";
			this.mnuWeb.Size = new System.Drawing.Size(168, 26);
			this.mnuWeb.Text = "&Homepage...";
			this.mnuWeb.Click += new System.EventHandler(this.MnuWebClick);
			// 
			// mnuGithub
			// 
			this.mnuGithub.CheckOnClick = true;
			this.mnuGithub.Name = "mnuGithub";
			this.mnuGithub.Size = new System.Drawing.Size(168, 26);
			this.mnuGithub.Text = "&Github";
			this.mnuGithub.Click += new System.EventHandler(this.MnuGithubClick);
			// 
			// mnuAbout
			// 
			this.mnuAbout.Name = "mnuAbout";
			this.mnuAbout.Size = new System.Drawing.Size(168, 26);
			this.mnuAbout.Text = "&About...";
			this.mnuAbout.Click += new System.EventHandler(this.MnuAboutClick);
			// 
			// mnuTest
			// 
			this.mnuTest.Name = "mnuTest";
			this.mnuTest.Size = new System.Drawing.Size(168, 26);
			this.mnuTest.Text = "Test";
			this.mnuTest.Visible = false;
			this.mnuTest.Click += new System.EventHandler(this.MnuTestClick);
			// 
			// statusMain
			// 
			this.statusMain.Dock = System.Windows.Forms.DockStyle.None;
			this.statusMain.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.statusMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.statusLabel1});
			this.statusMain.Location = new System.Drawing.Point(0, 0);
			this.statusMain.Name = "statusMain";
			this.statusMain.Size = new System.Drawing.Size(884, 25);
			this.statusMain.TabIndex = 3;
			this.statusMain.Text = "bla3";
			// 
			// statusLabel1
			// 
			this.statusLabel1.Name = "statusLabel1";
			this.statusLabel1.Size = new System.Drawing.Size(25, 20);
			this.statusLabel1.Text = "    ";
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
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(884, 482);
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
			this.bnFullscreen,
			this.toolStripSeparator3,
			this.bnHelp});
			this.toolStrip2.Location = new System.Drawing.Point(3, 0);
			this.toolStrip2.Name = "toolStrip2";
			this.toolStrip2.Size = new System.Drawing.Size(192, 27);
			this.toolStrip2.TabIndex = 1;
			// 
			// bnOpen
			// 
			this.bnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bnOpen.Image = ((System.Drawing.Image)(resources.GetObject("bnOpen.Image")));
			this.bnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.bnOpen.Name = "bnOpen";
			this.bnOpen.Size = new System.Drawing.Size(24, 24);
			this.bnOpen.Text = "Open";
			this.bnOpen.Click += new System.EventHandler(this.BnOpenClick);
			// 
			// bnStartEditor
			// 
			this.bnStartEditor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bnStartEditor.Image = ((System.Drawing.Image)(resources.GetObject("bnStartEditor.Image")));
			this.bnStartEditor.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.bnStartEditor.Name = "bnStartEditor";
			this.bnStartEditor.Size = new System.Drawing.Size(24, 24);
			this.bnStartEditor.Text = "Start editor";
			this.bnStartEditor.Click += new System.EventHandler(this.BnStartEditorClick);
			// 
			// bnDelete
			// 
			this.bnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bnDelete.Image = ((System.Drawing.Image)(resources.GetObject("bnDelete.Image")));
			this.bnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.bnDelete.Name = "bnDelete";
			this.bnDelete.Size = new System.Drawing.Size(24, 24);
			this.bnDelete.Text = "Delete image";
			this.bnDelete.Click += new System.EventHandler(this.BnDeleteClick);
			// 
			// toolStripSeparator
			// 
			this.toolStripSeparator.Name = "toolStripSeparator";
			this.toolStripSeparator.Size = new System.Drawing.Size(6, 27);
			// 
			// bnPrior
			// 
			this.bnPrior.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bnPrior.Image = ((System.Drawing.Image)(resources.GetObject("bnPrior.Image")));
			this.bnPrior.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.bnPrior.Name = "bnPrior";
			this.bnPrior.Size = new System.Drawing.Size(24, 24);
			this.bnPrior.Text = "Prior image";
			this.bnPrior.Click += new System.EventHandler(this.BnPriorClick);
			// 
			// bnNext
			// 
			this.bnNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bnNext.Image = ((System.Drawing.Image)(resources.GetObject("bnNext.Image")));
			this.bnNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.bnNext.Name = "bnNext";
			this.bnNext.Size = new System.Drawing.Size(24, 24);
			this.bnNext.Text = "Next image";
			this.bnNext.Click += new System.EventHandler(this.BnNextClick);
			// 
			// bnFullscreen
			// 
			this.bnFullscreen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bnFullscreen.Image = ((System.Drawing.Image)(resources.GetObject("bnFullscreen.Image")));
			this.bnFullscreen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.bnFullscreen.Name = "bnFullscreen";
			this.bnFullscreen.Size = new System.Drawing.Size(24, 24);
			this.bnFullscreen.Text = "Fullscreen";
			this.bnFullscreen.Click += new System.EventHandler(this.BnFullscreenClick);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
			// 
			// bnHelp
			// 
			this.bnHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bnHelp.Image = ((System.Drawing.Image)(resources.GetObject("bnHelp.Image")));
			this.bnHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.bnHelp.Name = "bnHelp";
			this.bnHelp.Size = new System.Drawing.Size(24, 24);
			this.bnHelp.Text = "Help";
			this.bnHelp.Click += new System.EventHandler(this.BnHelpClick);
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(884, 562);
			this.Controls.Add(this.toolStripContainer1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuMain;
			this.MinimumSize = new System.Drawing.Size(378, 164);
			this.Name = "frmMain";
			this.Text = "Next-View";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMainFormClosed);
			this.Load += new System.EventHandler(this.FrmMainLoad);
			this.Shown += new System.EventHandler(this.FrmMainShown);
			this.menuMain.ResumeLayout(false);
			this.menuMain.PerformLayout();
			this.statusMain.ResumeLayout(false);
			this.statusMain.PerformLayout();
			this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
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
		private System.Windows.Forms.ToolStripMenuItem mnuShowPanel;
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
	}
}
