/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
File name:     mainform.cs
Description:   main form to which other forms are docked as tabs
Copyright:     Copyright (c) Martin A. Schnell, 2012
Licence:       GNU General Public License
               This program is free software; you can redistribute it and/or
               modify it under the terms of the GNU General Public License
               as published by the Free Software Foundation.

               This program is free software: you can redistribute it and/or modify
               it under the terms of the GNU General Public License as published by
               the Free Software Foundation, either version 3 of the License, or
               (at your option) any later version.
History:

* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

using System;
using System.Collections.Generic;  // list
using System.Diagnostics;  // Debug
using System.Drawing;  // rectangle
using System.Globalization;   // CultureInfo
using System.IO;   // path
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Threading;
using System.Windows.Forms;
using Next_View.Properties;
using WeifenLuo.WinFormsUI.Docking;
using XDMessaging;   // XDBroadcast

namespace Next_View
{
	/// <summary>
	/// MainForm, with 3 docked tabs
	/// </summary>
	public partial class frmMain : Form
	{
		private DeserializeDockContent _deserializeDockContent;
		public frmImage  m_Image;
		public ExifDash  m_ExifDash;
		static EventWaitHandle s_event ;
		private XDListener listener;

		string _currentPath = "";
		private int _step = 0;
		private int _maxStep = 0;

		public frmMain()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

			_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
		}

		protected override void WndProc(ref Message m) {
			if(m.Msg == NativeMethods.WM_SHOWME) {
				ShowMe();
			}
			base.WndProc(ref m);
 		}

		void ShowMe()
		{
			WindowState = FormWindowState.Minimized;
			WindowState = FormWindowState.Normal;
			this.BringToFront();
			this.TopMost = true;
			this.TopMost = false;
			this.Activate();
			m_Image.Show(dockPanel1, DockState.Document);
    	}

		//--------------------------  form  ---------------------------//

		void FrmMainLoad(object sender, EventArgs e)
		{
			//Debug.WriteLine("Main start: ");
			listener = new XDListener();
			listener.MessageReceived += new XDListener.XDMessageHandler(listener_MessageReceived);
			listener.RegisterChannel("NVMessage");

			bool kShift = (Control.ModifierKeys == Keys.Shift);
			  
			bool created;
			s_event = new EventWaitHandle (false, EventResetMode.ManualReset, "Next-View", out created);   //  instead of mutex
   		if (created || kShift){         // 1st instance or shift key 
				if (Properties.Settings.Default.UpgradeRequired) {
					Settings.Default.Upgrade();
					Settings.Default.UpgradeRequired = false;
					Settings.Default.Save( );
				}
				int wX;
				int wY;
				int wW = Settings.Default.MainW;
				int wH = Settings.Default.MainH;

				Multi.MainLoad(out wX, out wY);
				//Debug.WriteLine("open main 1 y: {0} ", wY);
				bool visible;
				// menu bar visible
				Rectangle screenRectangle = RectangleToScreen(this.ClientRectangle);
				int titleHeight = screenRectangle.Top - this.Top;
				Multi.FormShowVisible(out visible, ref wX, ref wY, wW, titleHeight);
				if (!visible){
					this.Left = wX;
					this.Top = wY;
					//Debug.WriteLine("open main 2 y: {0} ", wY);
				}
				else {
					Multi.FormShowVisible(out visible, ref wX, ref wY, wW, wH);
					this.Left = wX;
					this.Top = wY;
					//Debug.WriteLine("open main 3 y: {0} ", wY);
				}
				this.Width = wW;
				this.Height = wH;

				string curDir = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
				string moPath = curDir + @"\language\";
				string cultureStr = Settings.Default.Language;
				if (cultureStr == ""){
					cultureStr = CultureInfo.InstalledUICulture.ToString();
					Settings.Default.Language = cultureStr;
				}
				//Debug.WriteLine("Culture: " + cultureStr);
				T.SetCatalog(moPath, cultureStr);
				TranslateMainForm();

				string recentPath = Settings.Default.RecentImgs;
				this.recentItem1.LoadList(recentPath);
				this.recentItem1.UpdateList();
				this.recentItem1.MaxItems = 5;
				this.recentItem1.ItemClick += new System.EventHandler(recentItem_Click);
				this.recentItem1.UpdateList();

				//Debug.WriteLine("open main y: {0} ", Settings.Default.MainY);
			}
			else {              // 2nd instance, give image path to 1st instance and end this 2nd
				string[] args = Environment.GetCommandLineArgs();
				string commandLine = "S";
				if (args.Length > 1){
					commandLine = args[1];
				}
				XDBroadcast.SendToChannel("NVMessage", commandLine);   // receive: listener_MessageReceived
				// NvSendMsg();  does not work for strings,
				ExitApp();
			}
		}

		void FrmMainShown(object sender, EventArgs e)
		{
			Form fm = this;
			m_Image  = new frmImage(fm, WinType.normal);
			m_Image.StatusChanged += new HandleStatusMainChange(HandleStatus);
			m_Image.WindowChanged += new HandleWindowMainChange(HandleWindow);
			m_Image.WindowSize += new HandleWindowSize(HandleSize);
			m_Image.CommandChanged += new HandleCommandChange(HandleCommand);

			m_Image.Show(dockPanel1, DockState.Document);      // sequence of tabs
			m_ExifDash = new ExifDash();
			m_ExifDash.StatusChanged += new HandleStatusMainChange(HandleStatus);
			m_ExifDash.WindowSize += new HandleWindowSize(HandleSize);
			m_ExifDash.CommandChanged += new HandleCommandChange(HandleCommand);
			//m_Image.Show(dockPanel1, DockState.Document);     // set active

			bool doShow = true;
			if (Control.ModifierKeys == Keys.Control){  // ctrl
				doShow = false;
				//Debug.WriteLine(" key control ");
			}

			string firstImage = "";
			string[] args = Environment.GetCommandLineArgs();
			if (args.Length > 1){
				firstImage = args[1];
			}
			if (File.Exists(firstImage)) {
				m_Image.PicScan(firstImage, false, 0);
				if (doShow){
					m_Image.PicLoadPos(firstImage, true);
					recentItem1.AddRecentItem(firstImage);
				}
				else _currentPath = firstImage;
			}
			else if (File.Exists(Settings.Default.LastImage)) {
				Debug.WriteLine("Last image settings: " + Settings.Default.LastImage);
				m_Image.PicScan(Settings.Default.LastImage, false, 0);
				if (doShow){
					 m_Image.PicLoadPos(Settings.Default.LastImage, true);
				}
				else _currentPath = Settings.Default.LastImage;
			}
			else {
				string userImagePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Pictures";
				if (Directory.Exists(userImagePath)) {
					m_Image.PicScan(userImagePath, true, 0);
				}
				firstImage = Directory.GetCurrentDirectory() + @"\Next-View-0.6.jpg";
				recentItem1.AddRecentItem(firstImage);
				if (doShow) {
					m_Image.PicLoadPos(firstImage, true);
				}
				else _currentPath = firstImage;
				Debug.WriteLine("Default image: " + _currentPath);
			}
		}


		void FrmMainFormClosed(object sender, FormClosedEventArgs e)
		{
		// DockContent has no close event when main form closes
			if (m_Image != null){
				m_Image.Close2nd();
			}
			//Debug.WriteLine("main FormClosed:");
			int le = this.Left;
			int to = this.Top;
			Multi.MainSave(le, to);
			Settings.Default.MainW = this.Width;
			Settings.Default.MainH = this.Height;
			string recentPath = "";
			recentItem1.StringList(ref recentPath);
			Settings.Default.RecentImgs = recentPath;
			Settings.Default.Save( );                  // last program line for debugger

		}

		//--------------------------  drop  ---------------------------

		void FrmMainDragDrop(object sender, DragEventArgs e)
		{
			bool allDirs = false;
			if ((e.KeyState & 8) == 8){      // ctrl
				allDirs = true;
			}

			if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
				e.Effect = DragDropEffects.Copy;
				m_Image.ProcessDrop((string[])e.Data.GetData(DataFormats.FileDrop), allDirs);

			}
			else {
				e.Effect = DragDropEffects.None;
			}
		}

		void FrmMainDragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Copy;
		}

		void FrmMainDragOver(object sender, DragEventArgs e)
		{
			if (ModifierKeys.HasFlag(Keys.Control)) {
				e.Effect = DragDropEffects.Copy;
			}
			else {
				e.Effect = DragDropEffects.Move;
			}
		}


		//--------------------------  menu  ---------------------------//
		//--------------------------  menu file ---------------------------//


		void MnuOpenImageClick(object sender, EventArgs e)
		{
			m_Image.OpenPic();
			//recentItem1.AddRecentItem(Settings.Default.LastImage);
		}

		private void recentItem_Click(object sender, EventArgs e)
		{
			string picPath = sender.ToString();
			if (File.Exists(picPath))
			{
				recentItem1.AddRecentItem(picPath);
				m_Image.PicScan(picPath, false, 0);
				m_Image.PicLoadPos(picPath, true);
			}
			else
				MessageBox.Show (sender.ToString(), T._("File does not exist"),
				      MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		void MnuRenameClick(object sender, EventArgs e)
		{
			m_Image.RenamePic();
		}

		void MnuDeleteClick(object sender, EventArgs e)
		{
			m_Image.DelPic();
		}

		void MnuSaveOriClick(object sender, EventArgs e)
		{
			m_Image.SaveOri();
		}

		void MnuExitClick(object sender, EventArgs e)
		{
			ExitApp();
		}


		//--------------------------  menu edit ---------------------------//

		void MnuOptionsClick(object sender, EventArgs e)
		{
			frmOption frm = new frmOption();
			frm.ShowDialog();
		}

		void MnuStartEditorClick(object sender, EventArgs e)
		{
			m_Image.StartEditor();
		}

		void MnuSearchClick(object sender, EventArgs e)
		{
			m_Image.SearchPic();
		}

		void MnuSearchBarClick(object sender, EventArgs e)
		{
			if (mnuSearchBar.Checked == true) {
				mnuSearchBar.Checked = false;
			}
			else {
				mnuSearchBar.Checked = true;
			}
			m_Image.ScollbarVis(mnuSearchBar.Checked);
		}

		//--------------------------  menu view ---------------------------//

		void MnuNextImageClick(object sender, EventArgs e)
		{
			m_Image.NextPic();
		}

		void MnuPriorImageClick(object sender, EventArgs e)
		{
			m_Image.PriorPic();
		}

		void MnuFirstImageClick(object sender, EventArgs e)
		{
			m_Image.FirstPic();
		}

		void MnuLastImageClick(object sender, EventArgs e)
		{
			m_Image.LastPic();
		}


		void MnuBackClick(object sender, EventArgs e)
		{
			m_Image.BackPic();
		}

		void MnuForwardClick(object sender, EventArgs e)
		{
			m_Image.ForwardPic();
		}

		void MnuRefreshClick(object sender, EventArgs e)
		{
			m_Image.RefreshDir();
		}

		void MnuFullScreenClick(object sender, EventArgs e)
		{
			m_Image.ShowFullScreen();
		}

		void MnuRotateLeftClick(object sender, EventArgs e)
		{
			m_Image.RotateLeft();
		}

		void MnuRotateRightClick(object sender, EventArgs e)
		{
			m_Image.RotateRight();
		}

		void MnuShowImageClick(object sender, EventArgs e)
		{
			m_Image.Show(dockPanel1, DockState.Document);
		}

		void MnuExifClick(object sender, EventArgs e)
		{
			m_Image.StartExif();
		}

		void MnuExifDashClick(object sender, EventArgs e)
		{
			m_ExifDash.SetPath2(_currentPath);
			m_ExifDash.Show(dockPanel1, DockState.Document);
		}

		//--------------------------  menu help ---------------------------//
		void MnuAboutClick(object sender, EventArgs e)
		{
			frmAbout frm = new frmAbout();
			frm.ShowDialog();
		}

		void MnuWebClick(object sender, EventArgs e)
		{
			Process.Start("http://www.next-view.org/index.htm");
		}

		void MnuHelp1Click(object sender, EventArgs e)
		{
			//Help.ShowHelp(this, "Next-View.chm");
			MessageBox.Show("Help not yet done", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		void FrmMainHelpRequested(object sender, HelpEventArgs hlpevent)   // F1
		{
			//Help.ShowHelp(this, "Next-View.chm", "Main.htm");
			MessageBox.Show("Help not yet done", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		void MnuGithubClick(object sender, EventArgs e)
		{
			Process.Start("https://github.com/Next-View/Next-View");
		}

		//--------------------------  methods  ------------------------------------//


		private IDockContent GetContentFromPersistString(string persistString)
		{
			if (persistString == typeof(frmImage).ToString())
				return m_Image;
			else
				return null;
		}


		//--------------------------  toolbar  ---------------------------//


		void BnOpenClick(object sender, EventArgs e)
		{
			this.mnuOpenImage.PerformClick();
		}

		void BnStartEditorClick(object sender, EventArgs e)
		{
			this.mnuStartEditor.PerformClick();
		}

		void BnDeleteClick(object sender, EventArgs e)
		{
			this.mnuDelete.PerformClick();
		}


		void BnPriorClick(object sender, EventArgs e)
		{
			this.mnuPriorImage.PerformClick();
		}

		void BnNextClick(object sender, EventArgs e)
		{
			this.mnuNextImage.PerformClick();
		}


		void BnFullscreenClick(object sender, EventArgs e)
		{
			this.mnuFullScreen.PerformClick();
		}

		void BnExifClick(object sender, EventArgs e)
		{
			m_Image.StartExif();
		}

		void BnSearchClick(object sender, EventArgs e)
		{
			this.mnuSearch.PerformClick();
		}

		void BnSearchPriorClick(object sender, EventArgs e)
		{
			m_Image.PriorSearchPic(edSearch.Text);
		}

		void BnSearchNextClick(object sender, EventArgs e)
		{
			m_Image.NextSearchPic(edSearch.Text);
		}

		void EdSearchKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter){
				m_Image.NextSearchPic(edSearch.Text);
			}
			if (e.KeyCode == Keys.Left){
				m_Image.PriorSearchPic(edSearch.Text);
			}
			if (e.KeyCode == Keys.Right){
				m_Image.NextSearchPic(edSearch.Text);
			}
		}


		void BnHelpClick(object sender, EventArgs e)
		{
			this.mnuHelp1.PerformClick();
		}

		void MnuSortFDateClick(object sender, EventArgs e)
		{
			m_Image.FDateSort();
		}

		void MnuSortExifDateClick(object sender, EventArgs e)
		{
			m_Image.ExifSort();
		}

		void BtnSortNameButtonClick(object sender, EventArgs e)
		{
			m_Image.NameSort();
		}


		//--------------------------  test  ---------------------------//

		void MnuTestClick(object sender, EventArgs e)
		{
			//TestException();
			TestScreen();
		}

		//--------------------------  functions  ---------------------------//


		void ExitApp()
		{
			this.Close();
			//Debug.WriteLine("Exit:");
			Application.Exit();      // exit self
			Environment.Exit(0);     // kill by win
		}

		void listener_MessageReceived(object sender, XDMessageEventArgs e)
		{
			string commandLine = e.DataGram.Message;
			if (File.Exists(commandLine)) {
				m_Image.PicScan(commandLine, false, 0);
				m_Image.PicLoadPos(commandLine, true);
				recentItem1.AddRecentItem(commandLine);
			}
			ShowMe();
		}

		void NvSendMsg()
		{
			NativeMethods.PostMessage((IntPtr)NativeMethods.HWND_BROADCAST,
				NativeMethods.WM_SHOWME,
				IntPtr.Zero,
				IntPtr.Zero);
		}


		void TestScreen()
		{
			foreach (var screen in System.Windows.Forms.Screen.AllScreens)
			{
				Debug.WriteLine("Device Name: " + screen.DeviceName);
				Debug.WriteLine("Bounds: " + screen.Bounds.ToString());
				Debug.WriteLine("Type: " + screen.GetType().ToString());
				Debug.WriteLine("Working Area: " + screen.WorkingArea.ToString());
				Debug.WriteLine("Primary : " + screen.Primary.ToString());
			}
		}

		void TestException()
		{
			int a = 0;
			int b = 100 / a;

		}


		//--------------------------  language  ---------------------------//

		void LangEnglishClick(object sender, EventArgs e)
		{
			Settings.Default.Language = "en-en";
				MessageBox.Show(T._("The language is changed with the next program start"), T._("Change to English"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		void LangGermanClick(object sender, EventArgs e)
		{
			Settings.Default.Language = "de-de";
				MessageBox.Show(T._("The language is changed with the next program start"), T._("Change to German"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		public void TranslateMainForm( )
		{
			mnuFile.Text = T._("File");
			mnuOpenImage.Text = T._("Open...");
			recentItem1.Text = T._("Recent images") + "               ";
			mnuRename.Text = T._("Rename...");
			mnuDelete.Text = T._("Delete");
			mnuSaveOri.Text = T._("Save orientation");
			mnuExit.Text = T._("Exit");

			mnuEdit.Text = T._("Edit");
			mnuOptions.Text = T._("Options...");
			mnuStartEditor.Text = T._("Start editor...");
			mnuSearch.Text = T._("Search...");
			mnuLanguage.Text = T._("Language");
			langEnglish.Text = T._("English");
			langGerman.Text = T._("German");

			mnuView.Text = T._("View");
			mnuNextImage.Text = T._("Next Image") + "               ";
			mnuPriorImage.Text = T._("Prior Image");
			mnuFirstImage.Text = T._("First Image");
			mnuLastImage.Text = T._("Last Image");
			mnuBack.Text = T._("Back");
			mnuForward.Text = T._("Forward");
			mnuRefresh.Text = T._("Refresh");
			mnuFullScreen.Text = T._("Full screen");
			mnuRotateLeft.Text = T._("Rotate left");
			mnuRotateRight.Text = T._("Rotate right");
			mnuExif.Text = T._("Exif...");
			mnuShowImage.Text = T._("Show image");
			mnuExifDash.Text = T._("Exif dashboard...");

			mnuHelp.Text = T._("Help");
			mnuHelp1.Text = T._("Help");
			mnuWeb.Text = T._("Homepage...");
			mnuGithub.Text = T._("Github");
			mnuAbout.Text = T._("About...");

			bnOpen.Text = T._("Open");
			bnStartEditor.Text = T._("Start editor");
			bnDelete.Text = T._("Delete image");
			bnPrior.Text = T._("Prior image");
			bnNext.Text = T._("Next image");
			bnFullscreen.Text = T._("Fullscreen");
			bnExif.Text = T._("Exif");
			bnSearch.Text = T._("Search");
			bnHelp.Text = T._("Help");
		}


		//--------------------------  events  ------------------------------------//


		private void HandleStatus(object sender, SetStatusMainEventArgs e)
		// called by: SetStatusText
		{
			int sVal = e.statusVal;
			string sText = e.statusText;
			//Debug.WriteLine(String.Format("Status msg: {0}, {1} ", sVal, sText));
			switch(sVal)
			{
				case 0:
					this.statusLabel1.Text = sText;
					break;

				case -1:
					_step++;
					if (_step <= _maxStep){
						progress1.Value = _step;
					}
					else {
						progress1.Value = 0;
					}
					break;

				case -9:
					progress1.Value = 0;
					_maxStep = 0;
					break;

				default:  // start progress
					statusLabel1.Text = T._("Scan");
					_maxStep = sVal;
					progress1.Maximum = _maxStep;
					_step = 0;
					break;
			}
		}

		private void HandleWindow(object sender, SetTitleEventArgs e)
		// called by: SetWindowText: picLoad, Rename, Remove
		{
			_currentPath = e.NewValue;
			this.Text = _currentPath + "  -  Next-View";
		}


		private void HandleSize(object sender, SetSizeEventArgs e)
		// called by: SetWindowSize
		{
			int w = e.nWidth;
			int h = e.nHeight;
			int ex = e.exifType;
			this.Width = w;
			this.Height = h;
			bnExif.Image = imageList1.Images[ex];
			//Debug.WriteLine("set size W / H: {0}/{1}", w, h);
		}

		private void HandleCommand(object sender, SetCommandEventArgs e)
		// called via SetCommand: image.KDown, OpenPic, ProcessDrop; exifdash.CmdShowClick
		// commands for main
		{
			char comm = e.Command;
			string fName = e.Fname;
			Debug.WriteLine("Command: " +  comm);
			switch(comm)
			{
				case 'e':  //  exifdash
					m_ExifDash.SetPath2(_currentPath);
					m_ExifDash.Show(dockPanel1, DockState.Document);
					break;
				case 'i':  //  exif dash img
					List<ImgFile> exImgList;
					m_ExifDash.DashImgList(out exImgList);
					//Debug.WriteLine("img on main: " + exImgList.Count.ToString());
					m_Image.Show(dockPanel1, DockState.Document);
					m_Image.ShowExifImages(exImgList, fName);
					break;
				case 'l':  //  leave dash
					Settings.Default.DashW = this.Width;
					Settings.Default.DashH = this.Height;
					Settings.Default.Save( );
					break;
				case 'p':  //  dash enter - exif path
					m_ExifDash.SetPath2(_currentPath);
					break;
				case 'r':  //  recent  for drop and open
					recentItem1.AddRecentItem(fName);
					break;
				case 'w':  //  exit
					ExitApp();
					break;
				default:     //  unknown
					Debug.WriteLine("unknown command: " + comm);
					break;

			}
		}
		void FrmMainActivated(object sender, EventArgs e)
		{
			//Debug.WriteLine("activated main: ");
		}

		void FrmMainKeyDown(object sender, KeyEventArgs e)
		{
			//Debug.WriteLine("main key: " + e.KeyValue.ToString());
		}


	}  // end main

	//--------------------------------------------------------------//

	class NativeMethods {
		public const int HWND_BROADCAST = 0xffff;
		public static readonly int WM_SHOWME = RegisterWindowMessage("WM_SHOWME");
		[DllImport("user32")]
		public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);
		[DllImport("user32")]
		public static extern int RegisterWindowMessage(string message);
	}

	// ------------------------------		delegates 	----------------------------------------------------------

	public delegate void HandleStatusMainChange(object sender, SetStatusMainEventArgs e);

	public delegate void HandleWindowMainChange(object sender, SetTitleEventArgs e);

	public delegate void HandleWindowSize(object sender, SetSizeEventArgs e);

	public delegate void HandleCommandChange(object sender, SetCommandEventArgs e);
}
