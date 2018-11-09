/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
File name:		 imageform.cs
Description:	 image form
Copyright:		 Copyright (c) Martin	A. Schnell,	2012
Licence:		 GNU General Public	License
				 This	program	is free	software;	you	can	redistribute it	and/or
				 modify	it under the terms of	the	GNU	General	Public License
				 as	published	by the Free	Software Foundation.

				 This	program	is free	software:	you	can	redistribute it	and/or modify
				 it	under	the	terms	of the GNU General Public	License	as published by
				 the Free	Software Foundation, either	version	3	of the License,	or
				 (at your	option)	any	later	version.
History:

*	*	*	*	*	*	*	*	*	*	*	*	*	*	*	*	*	*	*	*	*	*	*	*	*	*	*	*	*	*	*	*	*	*	*	*	*	*/

using	System;
using	System.Drawing;	 //	Bitmap
using	System.Diagnostics;	 //	Debug
using	System.IO;	 //	directory
using	System.Windows.Forms;
using	Next_View.Properties;
using	WeifenLuo.WinFormsUI.Docking;

namespace	Next_View
{
	///	<summary>
	///	Description	of StatsForm.
	///	</summary>
	public partial class frmImage	:	DockContent
	{
		ImgList _il = new ImgList();
		int _scHeight = 0;
		int _scWidth = 0;
		int _mainWidth = 0;
		int _mainHeight = 0;
		int _borderHeight = 0;
		int _borderWidth = 0;
		string _picSelection = "Directory:";
		string _currentPath = "";
		Image _myImg;
		bool loadNextPic = true;

		public event HandleStatusMainChange	 StatusChanged;

		public event HandleWindowMainChange	 WindowChanged;

		public event HandleWindowSize WindowSize;

		public frmImage(int mainWidth, int mainHeight)
		{
			//
			// The InitializeComponent() call	is required	for	Windows	Forms	designer support.
			//
			InitializeComponent();
			_mainWidth = mainWidth;
			_mainHeight = mainHeight;
			_scHeight = Screen.FromControl(this).Bounds.Height;
			_scWidth = Screen.FromControl(this).Bounds.Width;
			//Debug.WriteLine("Screen W / H: {0}/{1}", _scWidth, _scHeight);

			//Debug.WriteLine("main W / H: {0}/{1}", mainWidth, mainHeight);

		}


		// ------------------------------		events form	----------------------------------------------------------

		void FrmImageShown(object sender, EventArgs e)
		{
			int pbHeight = picBox.Height;
			int pbWidth = picBox.Width;
			//Debug.WriteLine("picbox-2 W / H: {0}/{1}", pbWidth, pbHeight);
			_borderWidth = _mainWidth - pbWidth;
			_borderHeight = _mainHeight - pbHeight;

		}

		void FrmImageDragDrop(object sender, DragEventArgs e)
		{
			int picCount = 0;
			int dirCount = 0;
			bool allDirs = false;
			if ((e.KeyState & 8) == 8){
				Debug.WriteLine("ctrl");
				allDirs = true;
			}

			if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
				e.Effect = DragDropEffects.Copy;
				string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
				Array.Sort(files);

				string loadFile = "";
				_il.DirClear();
				foreach	(string	dropFile in	files)
				{
					string dropDir = "";
					if (File.Exists(dropFile)) {
						if (_il.FileIsValid(dropFile)){
							picCount++;
							_il.DirPicAdd(dropFile);
							dropDir	= Path.GetDirectoryName(dropFile);
							loadFile = dropFile;
						}
						else{
							string ext = Path.GetExtension(dropFile).ToLower();
							MessageBox.Show("File type " + ext  + " not supported", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						}
					}
					else if	(Directory.Exists(dropFile)){	// is	dir
						dirCount++;
						dropDir	= dropFile;
						loadFile = dropFile;
						Debug.WriteLine("drop dir" + dropDir);
					}
					else if (dropDir	== ""){
						MessageBox.Show("No drop dir", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}  // end for

				if (picCount == 1) {
					_picSelection = "Directory:";
					PicScan(loadFile, allDirs);
				}
				else if (picCount > 0){
					_picSelection = "Selection:";
					// pic list already loaded
				}
				else if (dirCount > 0){
					_picSelection = "Directory:";
					PicScan(loadFile, allDirs);
					_il.DirPicFirst(ref loadFile);
				}
				else {
					//MessageBox.Show("No drop selection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}

				if (loadFile != ""){
					PicLoad(loadFile, true);
				}
				else {
					picBox.Image = null;
					SetStatusText("No image loaded");
				}

			}
			else {
				e.Effect = DragDropEffects.None;
			}
		}

		void FrmImageDragEnter(object	sender,	DragEventArgs	e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Copy;
		}

		void FrmImageDragOver(object sender, DragEventArgs e)
		{
			if (ModifierKeys.HasFlag(Keys.Control))	{
				// control is	pressed. Copy.
				e.Effect = DragDropEffects.Copy;
			}
			else {
				e.Effect = DragDropEffects.Move;
			}
		}

		void FrmImageHelpRequested(object	sender,	HelpEventArgs	hlpevent)
		{
			Help.ShowHelp(this,	"Next-View.chm", "Fieldlist.htm");
		}

		void FrmImagePreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			e.IsInputKey = true;     // triggers keydown for arrow keys
		}

		void FrmImageKeyUp(object sender, KeyEventArgs e)
		{
			Debug.WriteLine("key up");
		}

		void FrmImageKeyDown(object sender, KeyEventArgs e)
		{

			Debug.WriteLine(" ");
			Debug.WriteLine("key:	" + e.KeyValue.ToString());   // KeyCode?

			bool alt = false;
			if (e.Modifiers == Keys.Alt){
				alt = true;
			}
			bool ctrl = false;
			if (e.Modifiers == Keys.Control){
				ctrl = true;
			}
			if (loadNextPic){
				loadNextPic = false;           // eat up keys
				KDown(e.KeyValue, ctrl, alt);
			}
		}

		public bool KDown(int kValue, bool ctrl, bool alt)
		{
			switch(kValue)
			{
				case 39:  //  ->
					if (alt){
						ForwardPic();
					}
					else if (ctrl){
						NextPicDir();
					}
					else {
						NextPic();
					}
					break;
				case 34:  //  pd
				case 32:  //  space
					NextPic();
					break;
				case 37:  // <-
					if (alt){
						BackPic();
					}
					else if (ctrl){
						PriorPicDir();
					}
					else {
						PriorPic();
					}
					break;
				case 33:  // pu
					PriorPic();
					break;
				case 36:    // pos 1
					FirstPic();
					break;
				case 35:    // end
					LastPic();
					break;
				case 8:    // back
					BackPic();
					break;

				case 68:    // d   dark
					DarkPic();
					break;
				case 113:    // F2
					RenamePic();
					break;
				case 116:    // F5
					RefreshDir();
					break;
				case 46:    // del
					DelPic();
					break;
				case 70:    // ctrl F
					if (ctrl){
						SearchPic();
					}
					break;

				case 107:    // +
				case 187:    // +
					RenamePicPlus();
					break;
				case 109:    // -
				case 189:    // -
					RemovePicPlus();
					break;
				case 13:    // enter  full screen
					ShowFullScreen();
					break;
			}
			loadNextPic = true;
			return true;
			//  ctrl 17
		}

		// ------------------------------		pic functions	 ----------------------------------------------------------


		public bool PicLoad(string	pPath, bool log)
		{
			try
			{
				int picPos = 0;
				int picAll = 0;
				if (log) {
					_il.DirPosPath(ref picPos, ref picAll, pPath);
					SetStatusText(String.Format(_picSelection + " {0}/{1}", picPos, picAll));
					_il.LogPic(pPath);
				}
				else {
					_il.LogPos(ref picPos, ref picAll);
					SetStatusText(String.Format("History: {0}/{1}", picPos, picAll));
				}
				SetWindowText(pPath);
				_currentPath = pPath;

				if (!File.Exists(pPath)){
					picBox.SizeMode = PictureBoxSizeMode.CenterImage;
					picBox.Image = picBox.ErrorImage;
					return false;
				}

				//Image myImg;
				using (FileStream stream = new FileStream(pPath, FileMode.Open, FileAccess.Read))
				{
					_myImg = Image.FromStream(stream);  // abort for gif
					stream.Close();
				}
				GC.Collect();
 				Application.DoEvents();

				//using (Image bmpTemp = new Bitmap(pPath))      // abort for invalid jpg
				//{
				//	_myImg = new Bitmap(bmpTemp);
				//	if(bmpTemp != null)
				//		((IDisposable)bmpTemp).Dispose();
				//}
				//GC.Collect();

				string ext = Path.GetExtension(pPath).ToLower();
				if (ext == ".gif"){
					picBox.Image = Image.FromFile(pPath);    // workaround, only direct load makes gif animation, but file can't be renamed
				}
				else {
					picBox.Image = _myImg;
				}

				int imHeight = _myImg.Height;
				int imWidth = _myImg.Width;
				//Debug.WriteLine("Image W / H: {0}/{1}", imWidth, imHeight);

				if ((imWidth + _borderWidth > _scWidth) || (imHeight + _borderHeight > _scHeight)){
					picBox.SizeMode = PictureBoxSizeMode.Zoom;
					picBox.BackColor = SystemColors.Control;
					float scFactor = (float) _scWidth / _scHeight;
					float imFactor = (float) imWidth / imHeight;
					if (imFactor > scFactor){   // wide img
						int ih = (imHeight * (_scWidth - _borderWidth) / imWidth);
						SetWindowSize(_scWidth, ih + _borderHeight);
					}
					else {    // high img
						int iw = (imWidth * (_scHeight - _borderHeight) / imHeight);// + _borderWidth;
						SetWindowSize(iw + _borderWidth, _scHeight);
					}
				}
				else {  // small img
					picBox.SizeMode = PictureBoxSizeMode.CenterImage;
					picBox.BackColor = Color.Black;
					SetWindowSize(imWidth + _borderWidth, imHeight + _borderHeight );
				}
				Settings.Default.LastImage = pPath;
				Debug.WriteLine("pic end " + pPath);
				return true;
			}
			catch (Exception e)
			{
				picBox.Image = null;
				MessageBox.Show("File is invalid \n" + pPath + "\n " + e.Message, "Invalid file", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
		}


		public void PicScan(string	pPath, bool allDirs)
		{
			_il.DirScan(pPath, allDirs);
		}

		void DarkPic()
		{
			picBox.Image = null;
		}

		public void	NextPic()
		{
			string pPath = "";
			if (_il.DirPicNext(ref pPath)){
				PicLoad(pPath, true);
			}
			else {
				SetStatusText("No image loaded");
			}
		}

		public void	NextPicDir()
		{
			PicScan(_currentPath, false);
			int picPos = 0;
			int picAll = 0;
			_il.DirPosPath(ref picPos, ref picAll, _currentPath);
			NextPic();
		}

		public void	PriorPic()
		{
			string pPath = "";
			if (_il.DirPicPrior(ref pPath)){
				PicLoad(pPath, true);
			}
			else {
				SetStatusText("No image loaded");
			}
		}

		public void	PriorPicDir()
		{
			PicScan(_currentPath, false);
			int picPos = 0;
			int picAll = 0;
			_il.DirPosPath(ref picPos, ref picAll, _currentPath);
			PriorPic();
		}

		public void	FirstPic()
		{
			string pPath = "";
			if (_il.DirPicFirst(ref pPath)){
				PicLoad(pPath, true);
			}
			else {
				SetStatusText("No image loaded");
			}
		}

		public void	LastPic()
		{
			string pPath = "";
			if (_il.DirPicLast(ref pPath)){
				PicLoad(pPath, true);
			}
			else {
				SetStatusText("No image loaded");
			}
		}

		public void	BackPic()
		{
			string pPath = "";
			if (_il.LogBack(ref pPath)){
				PicLoad(pPath, false);
			}
		}

		public void	ForwardPic()
		{
			string pPath = "";
			if (_il.LogForward(ref pPath)){
				PicLoad(pPath, false);
			}
		}

		public void	RefreshDir()
		{
			_il.DirClear();
			PicScan(_currentPath, false);
			PicLoad(_currentPath, true);
		}

		public void	RenamePic()
		{
			string newPath = "";
			frmRename frm = new frmRename(_currentPath);
			var result = frm.ShowDialog();
			if (frm._ReturnPath != "") {
				newPath = frm._ReturnPath;
				_il.RenameListLog(_currentPath, newPath);
				_currentPath = newPath;
				PicLoad(_currentPath, true);
			}
		}

		public void	RenamePicPlus()
		{
			string fname = Path.GetFileNameWithoutExtension(_currentPath);
			string fext = Path.GetExtension(_currentPath);
			string newPath = Path.GetDirectoryName(_currentPath) + @"\" + fname + "+" + fext;
			if (FileRename2(_currentPath, newPath)) {
				_il.RenameListLog(_currentPath, newPath);
				_currentPath = newPath;
				PicLoad(_currentPath, true);
			}
			else {
				Debug.WriteLine("no rename");
			}
		}

		public void	RemovePicPlus()
		{
			string fname = Path.GetFileNameWithoutExtension(_currentPath);
			string fext = Path.GetExtension(_currentPath);
			string lastChar = fname.Substring(fname.Length - 1);
			if (lastChar == "+"){
				fname = fname.Substring(0, fname.Length - 1);
				string newPath = Path.GetDirectoryName(_currentPath) + @"\" + fname + fext;
				if (FileRename2(_currentPath, newPath)) {
					_il.RenameListLog(_currentPath, newPath);
					_currentPath = newPath;
					PicLoad(_currentPath, true);
				}
				else {
					Debug.WriteLine("no rename");
				}
			}
		}

		bool FileRename2(string nameFrom, string nameTo)
		{
			try {
				File.Move(nameFrom, nameTo);
				return true;
			}
			catch {
				return false;
			}
		}

		public void	DelPic()
		{
			if (DelFile.MoveToRecycleBin(_currentPath)){
				string nextPath = "";
				if (_il.DeleteListLog(_currentPath, ref nextPath)){
					PicLoad(nextPath, true);
					_currentPath = nextPath;
				}
				else {  // last img in selection deleted
					picBox.Image = null;
					SetStatusText("No image loaded");
				}
			}
			else {

			}
		}

		public void	SearchPic()
		{
			SearchForm frm = new SearchForm(_currentPath, _il);
			frm.ShowDialog();

			string pPath = "";
			if (frm._SearchReturn) {
				if (_il.DirPicFirst(ref pPath)){
					_currentPath = pPath;
					_picSelection = "Search:";
					PicLoad(_currentPath, true);
				}
			}
		}

		public void	ShowFullScreen()
		{
			string pPath = "";
			FullScreen frm = new FullScreen(_il);
			if (_il.DirPosCurrent(ref pPath)){
				frm.FPicLoad(pPath, false);
				var result = frm.ShowDialog();
				pPath = frm.ReturnPath;
				PicLoad(pPath, true);
			}
			else {
				SetStatusText("No image loaded");
			}
		}

		public void	StartEditor()
		{
			string editorPath = Settings.Default.Editor;
			if ((editorPath == "")||(!File.Exists(editorPath))) {    // extension default editor
				Util.StartEditor(_currentPath, "");
			}
			else {
				Util.StartEditor(editorPath, _currentPath);
			}
		}

		// ------------------------------		delegates 	----------------------------------------------------------

		public void	SetWindowText(string text2)
		{
			// called	by:
			// output: main.HandleStatus
			OnWindowChanged(new	SetStatusMainEventArgs(text2));
			Application.DoEvents();
		}

		protected	virtual	void OnWindowChanged(SetStatusMainEventArgs	e)
		{
			if(this.WindowChanged	!= null)		 //	nothing	subscribed to	this event
			{
				this.WindowChanged(this, e);
			}
		}

		public void	SetWindowSize(int w, int h)
		{
			// called	by:
			// output: main.HandleStatus
			OnWindowSize(new SetSizeEventArgs(w, h));
			Application.DoEvents();
		}

		protected	virtual	void OnWindowSize(SetSizeEventArgs	e)
		{
			if(this.WindowSize != null)		 //	nothing	subscribed to	this event
			{
				this.WindowSize(this, e);
			}
		}

		public void	SetStatusText(string text1)
		{
			// called	by:
			// output: main.HandleStatus
			OnStatusChanged(new	SetStatusMainEventArgs(text1));
			Application.DoEvents();
		}

		protected	virtual	void OnStatusChanged(SetStatusMainEventArgs	e)
		{
			if(this.StatusChanged	!= null)		 //	nothing	subscribed to	this event
			{
				this.StatusChanged(this, e);
			}
		}



	}	 //	end	frmImage
}
