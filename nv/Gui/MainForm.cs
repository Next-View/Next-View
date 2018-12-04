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
using System.Diagnostics;  // Debug
using System.IO;   // path
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Threading;
using System.Windows.Forms;
using Next_View.Properties;
using WeifenLuo.WinFormsUI.Docking;
using XDMessaging;

namespace Next_View
{
	/// <summary>
	/// MainForm, with 3 docked tabs
	/// </summary>
	public partial class frmMain : Form
	{
		private DeserializeDockContent _deserializeDockContent;
		public frmImage  m_Image; //   new frmImage();
		static EventWaitHandle s_event ;
		private XDListener listener;

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
			if(WindowState == FormWindowState.Minimized) {
				WindowState = FormWindowState.Normal;
			}
			bool top = TopMost;
			TopMost = true;
			TopMost = top;
    	}

		//--------------------------  form  ---------------------------//

		void FrmMainLoad(object sender, EventArgs e)
		{
			listener = new XDListener();
			listener.MessageReceived += new XDListener.XDMessageHandler(listener_MessageReceived);
			listener.RegisterChannel("NVMessage");

			bool created ;
			s_event = new EventWaitHandle (false, EventResetMode.ManualReset, "Next-View", out created);   //  instead of mutex
    		if (created){         // 1st instance
				if (Properties.Settings.Default.UpgradeRequired)
				{
					Settings.Default.Upgrade();
					Settings.Default.UpgradeRequired = false;
					Settings.Default.Save( );
					Debug.WriteLine("settings upgrade done: ");
				}
				int wW = Settings.Default.MainW;
				int wH = Settings.Default.MainH;
				int wX = Settings.Default.MainX;
				int wY = Settings.Default.MainY;
				int sWidth = Screen.FromControl(this).Bounds.Width;
				int sHeight = Screen.FromControl(this).Bounds.Height;

				this.Width = wW;
				this.Height = wH;
				if (wX + wW < 0) this.Left = -50;      // for screen settings change
				else if (wX > sWidth) this.Left = sWidth - 100;
				else this.Left = wX;
				if (wY + wH < 0) this.Top = -50;
				else this.Top = wY;
				
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
				// NvSendMsg();  does not work work strings,
				ExitApp();
			}
		}

		void FrmMainShown(object sender, EventArgs e)
		{
			int fHeight = this.Height;
			int fWidth = this.Width;

			m_Image  = new frmImage(fWidth, fHeight, WinType.normal);
			m_Image.StatusChanged += new HandleStatusMainChange(HandleStatus);
			m_Image.WindowChanged += new HandleWindowMainChange(HandleWindow);
			m_Image.WindowSize += new HandleWindowSize(HandleSize);
			m_Image.FilenameChanged += new HandleFilenameChange(HandleFilename);
			
			m_Image.Show(dockPanel1, DockState.Document);      // sequence of tabs
			//m_Image.Show(dockPanel1, DockState.Document);     // set active

			string firstImage = "";
			string[] args = Environment.GetCommandLineArgs();
			if (args.Length > 1){
				firstImage = args[1];
			}
			if (File.Exists(firstImage)) {
				m_Image.PicScan(firstImage, false);
				m_Image.PicLoad(firstImage, true);
			}
			else if (File.Exists(Settings.Default.LastImage)) {
				m_Image.PicScan(Settings.Default.LastImage, false);
				m_Image.PicLoad(Settings.Default.LastImage, true);
			}
			else {
				string userImagePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Pictures";
				if (Directory.Exists(userImagePath)) {
					m_Image.PicScan(userImagePath, true);
				}
				Debug.WriteLine("pic path : " + userImagePath);
				firstImage = Directory.GetCurrentDirectory() + @"\Next-View-0.4.png";
				recentItem1.AddRecentItem(firstImage);
				m_Image.PicLoad(firstImage, true);
			}
		}


		void FrmMainFormClosed(object sender, FormClosedEventArgs e)
		{
		// DockContent has no close event when main form closes
			if (m_Image != null){
				m_Image.Close2nd();
			}
			Debug.WriteLine("main FormClosed");
			Settings.Default.MainX = this.Left;
			Settings.Default.MainY = this.Top;
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
			if ((e.KeyState & 8) == 8){
				Debug.WriteLine("ctrl");
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
				m_Image.PicScan(picPath, false);
				m_Image.PicLoad(picPath, true);
			}
			else
				MessageBox.Show (sender.ToString(), "File does not exist",
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

		void MnuShowPanelClick(object sender, EventArgs e)
		{
			m_Image.Show(dockPanel1, DockState.Document);
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

		void BnSearchClick(object sender, EventArgs e)
		{
			this.mnuSearch.PerformClick();
		}
		
		void BnFullscreenClick(object sender, EventArgs e)
		{
			this.mnuFullScreen.PerformClick();
		}


		void BnHelpClick(object sender, EventArgs e)
		{
			this.mnuHelp1.PerformClick();
		}


		//--------------------------  test  ---------------------------//

		void MnuTestClick(object sender, EventArgs e)
		{
			TestScreen();

		}

		//--------------------------  functions  ---------------------------//


		void ExitApp()
		{
			this.Close();
			Debug.WriteLine("Exit:");
			Application.Exit();      // exit self
			Environment.Exit(0);     // kill by win
		}

		void listener_MessageReceived(object sender, XDMessageEventArgs e)
		{
			string commandLine = e.DataGram.Message;
			if (File.Exists(commandLine)) {
				m_Image.PicScan(commandLine, false);
				m_Image.PicLoad(commandLine, true);
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

		void UnlockDir()
		{
   			try
     	 	{
     			string folderPath = @"C:\temp\Images\6Pic";
     			string adminUserName = Environment.UserName;// getting your adminUserName
     			DirectorySecurity ds = Directory.GetAccessControl(folderPath);
     			FileSystemAccessRule fsa = new FileSystemAccessRule(adminUserName,FileSystemRights.FullControl, AccessControlType.Deny);
     			ds.RemoveAccessRule(fsa);
     			Directory.SetAccessControl(folderPath, ds);
     			MessageBox.Show("UnLocked");
     		}
     		catch (Exception ex)
     		{
        		MessageBox.Show(ex.Message);
     		}
		}


		//--------------------------  events  ------------------------------------//


		private void HandleStatus(object sender, SetStatusMainEventArgs e)
		// called by: SetStatusText
		{
			string par1 = e.NewValue;
			this.statusLabel1.Text = par1;
		}

		private void HandleWindow(object sender, SetStatusMainEventArgs e)
		// called by: SetWindowText
		{
			string pPath = e.NewValue;
			this.Text = pPath + "  -  Next-View";
			// this.Text = Path.GetFileName(pPath)	+	"  -  Next-View";
			//recentItem1.AddRecentItem(pPath);
		}


		private void HandleSize(object sender, SetSizeEventArgs e)
		// called by: SetWindowSize
		{
			int w = e.nWidth;
			int h = e.nHeight;
			this.Width = w;
			this.Height = h;
			// Debug.WriteLine("set size W / H: {0}/{1}", w, h);
		}

		private void HandleFilename(object sender, SetFilenameEventArgs e)
		// called by: SetFilename: openPic, FrmImageDragDrop 
		{
			string pPath = e.NewValue;
			recentItem1.AddRecentItem(pPath);
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

	public delegate void HandleWindowMainChange(object sender, SetStatusMainEventArgs e);

	public delegate void HandleWindowSize(object sender, SetSizeEventArgs e);

	public delegate void HandleFilenameChange(object sender, SetFilenameEventArgs e);
}
