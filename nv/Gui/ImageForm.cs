/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
File name:     imageform.cs
Description:   image form
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
using System.Drawing;  // Bitmap
using System.Diagnostics;  // Debug
using System.IO;   // directory
using System.Linq;	 //	OfType
using System.Windows.Forms;
using Next_View.Properties;
using WeifenLuo.WinFormsUI.Docking;

namespace Next_View
{
	/// <summary>
	/// Description of StatsForm.
	/// </summary>
	public partial class frmImage : DockContent
	{
		ImgList _il = new ImgList();
		int _scHeight = 0;
		int _scWidth = 0;
		int _mainWidth = 0;
		int _mainHeight = 0;
		int _borderHeight = 0;
		int _borderWidth = 0;
		int _currentWidth = 0;
		int _currentHeight = 0;
		string _picSelection = "";
		string _currentPath = "";
		string _orientation = "";
		int _exifType = 0;
		string _lastSearchStr = "";
		Image _myImg;
		bool _loadNextPic = true;

		WinType _wType;   // normal, full, second
		public bool _ndRunning {get;set;}
		string _priorPath = "";

		ExifForm m_Exif;

		public frmImage  m_Image2;

		public event HandleStatusMainChange  StatusChanged;

		public event HandleWindowMainChange  WindowChanged;

		public event HandleWindowSize WindowSize;

		public event HandleCommandChange  CommandChanged;

		public frmImage(int mainWidth, int mainHeight, WinType wType)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			_wType = wType;

			_mainWidth = mainWidth;
			_mainHeight = mainHeight;

			//Debug.WriteLine("Screen W / H: {0}/{1}", _scWidth, _scHeight);

			//Debug.WriteLine("main W / H: {0}/{1}", mainWidth, mainHeight);

		}


		// ------------------------------   events form ----------------------------------------------------------

		private void HandleKey(object sender, SetKeyEventArgs e)
		// called by:
		{
			int kVal = e.kValue;
			bool alt = e.alt;
			bool ctrl = e.ctrl;

			if (_loadNextPic){
				_loadNextPic = false;           // eat up keys
				KDown(kVal, ctrl, alt);
			}
		}

		void FrmImageLoad(object sender, EventArgs e)
		{
			if (_wType == WinType.normal){
				_ndRunning = false;
				_scWidth = Screen.FromControl(this).Bounds.Width;
				_scHeight = Screen.FromControl(this).Bounds.Height;

			}

			TranslateImageForm();

			if (_wType == WinType.second){
				popClose.Text = T._("Close");    // not Exit

				int wX;
				int wY;
				int wW = Settings.Default.SecondW;
				int wH = Settings.Default.SecondH;
				Multi.SecondLoad(out wX, out wY);

				bool visible;
				// menu bar visible
				Rectangle screenRectangle = RectangleToScreen(this.ClientRectangle);
				int titleHeight = screenRectangle.Top - this.Top;
				Multi.FormShowVisible(out visible, ref wX, ref wY, wW, titleHeight);
				if (!visible){
					this.Left = wX;
					this.Top = wY;
				}
				else {
					Multi.FormShowVisible(out visible, ref wX, ref wY, wW, wH);
					this.Left = wX;
					this.Top = wY;
				}
				this.Width = wW;
				this.Height = wH;

				Debug.WriteLine("open 2nd y: {0} ", Settings.Default.SecondY);

				_ndRunning = true;
				_mainWidth = picBox.Width;
				_mainHeight = picBox.Height;
				_scWidth = this.Width;
				_scHeight = this.Height;
			}
		}

		void FrmImageShown(object sender, EventArgs e)
		{
			int pbHeight = picBox.Height;
			int pbWidth = picBox.Width;
			//Debug.WriteLine("picbox-2 W / H: {0}/{1}", pbWidth, pbHeight);
			_borderWidth = _mainWidth - pbWidth;
			_borderHeight = _mainHeight - pbHeight;

		}

		void FrmImageEnter(object sender, EventArgs e)
		{
			if (_currentWidth > 0){
				SetWindowSize(_currentWidth, _currentHeight, _exifType);
			}
		}

		void FrmImageLeave(object sender, EventArgs e)
		{
			// Debug.WriteLine("img leave");
		}

		void FrmImageHelpRequested(object sender, HelpEventArgs hlpevent)
		{
			//Help.ShowHelp(this, "Next-View.chm", "Fieldlist.htm");
			MessageBox.Show("Help not yet done", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		void FrmImagePreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			e.IsInputKey = true;     // triggers keydown for arrow keys
		}

		void FrmImageKeyUp(object sender, KeyEventArgs e)
		{
			_loadNextPic = true;
		}

		void FrmImageFormClosing(object sender, FormClosingEventArgs e)
		{
			_ndRunning = false;
			if (_wType == WinType.second){
				int wX = this.Left;
				int wY = this.Top;
				Multi.SecondSave(wX, wY);
				Settings.Default.SecondW = this.Width;
				Settings.Default.SecondH = this.Height;
				Settings.Default.Save( );
				Debug.WriteLine("close 2nd y: {0} ", Settings.Default.SecondY);
			}
			if (_wType == WinType.normal){
				e.Cancel = true;
				this.Hide();
				Debug.WriteLine("hide img ");
			}
		}

		void FrmImageFormClosed(object sender, FormClosedEventArgs e)
		{

		}

		void RClose()
		// remote close
		{
			//this.Hide();
			this.Close();
		}

		// ------------------------------   drop  ----------------------------------------------------------

		void FrmImageDragDrop(object sender, DragEventArgs e)
		{
			bool allDirs = false;
			if ((e.KeyState & 8) == 8){
				//Debug.WriteLine("ctrl");
				allDirs = true;
			}

			if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
				e.Effect = DragDropEffects.Copy;
				ProcessDrop((string[])e.Data.GetData(DataFormats.FileDrop), allDirs);

			}
			else {
				e.Effect = DragDropEffects.None;
			}
		}

		public void ProcessDrop(string[] files, bool allDirs)
		{
			int picCount = 0;
			int dirCount = 0;
			Array.Sort(files);

			string loadFile = "";
			_il.DirClear();
			foreach (string dropFile in files)
			{
				string dropDir = "";
				if (File.Exists(dropFile)) {
					if (_il.FileIsValid(dropFile)){
						picCount++;
						_il.DirPicAdd(dropFile);
						dropDir = Path.GetDirectoryName(dropFile);
						loadFile = dropFile;
					}
					else{
						string ext = Path.GetExtension(dropFile).ToLower();
						MessageBox.Show(String.Format(T._("File type {0} not supported"), ext), T._("Error"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}
				else if (Directory.Exists(dropFile)){ // is dir
					dirCount++;
					dropDir = dropFile;
					loadFile = dropFile;
					//Debug.WriteLine("drop dir " + dropDir);
				}
				else if (dropDir  == ""){
					MessageBox.Show(T._("No drop dir"), T._("Error"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}  // end for

			if (picCount == 1) {
				_picSelection = T._("Directory:");
				PicScan(loadFile, allDirs);
			}
			else if (picCount > 0){
				_picSelection = T._("Selection:");
				// pic list already loaded
			}
			else if (dirCount > 0){
				_picSelection = T._("Directory:");
				PicScan(loadFile, allDirs);
				_il.DirPicFirst(ref loadFile);
			}
			else {
				//MessageBox.Show("No drop selection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}

			if (loadFile != ""){
				PicLoad(loadFile, true);
				SetCommand('r', loadFile);
			}
			else {
				picBox.Image = null;
				SetStatusText(0, T._("No image loaded"));
			}
		}

		void FrmImageDragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Copy;
		}

		void FrmImageDragOver(object sender, DragEventArgs e)
		{
			if (ModifierKeys.HasFlag(Keys.Control)) {
				// control is pressed. Copy.
				e.Effect = DragDropEffects.Copy;    // + sign for sub-dirs
			}
			else {
				e.Effect = DragDropEffects.Move;
			}
		}

		// ------------------------------   key functions  ----------------------------------------------------------

		void FrmImageKeyDown(object sender, KeyEventArgs e)
		{

			//Debug.WriteLine(" ");
			Debug.WriteLine("key: " + e.KeyValue.ToString());

			bool alt = false;
			if (e.Modifiers == Keys.Alt){
				alt = true;
			}
			bool ctrl = false;
			if (e.Modifiers == Keys.Control){
				ctrl = true;
			}
			if (_loadNextPic){
				_loadNextPic = false;           // eat up keys
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

				case 66:    // 'b'   boss
				case 68:    // 'd'   dark
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
				case 79:    // 'o'
					if (ctrl){
						OpenPic();
					}
					break;
				case 70:    // ctrl 'f'
					if (ctrl){
						SearchPic();
					}
					break;
				case 50:    // '2'
					if (ctrl){
						Start2ndScreen();
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
				case 84:    // 't'
					if (alt){
						TempmarkDelete();
					}
					else if (ctrl){
						TempmarkGo();
					}
					else {
						TempmarkPic();
					}
					break;

				case 13:    // enter  full screen
					ShowFullScreen();
					break;
				case 65:    // 'a'  for test
					Test();
					break;
				case 69:    // 'e'  for exif
					if (alt){
						SetCommand('e', _currentPath);
						//ShowExifDash();
					}
					else if (ctrl){
						ShowExif0();
					}
					else {
						StartExif();
					}
					break;
			}
			_loadNextPic = true;
			return true;
			//  ctrl 17
		}

		// ------------------------------   pic functions  ----------------------------------------------------------


		public bool PicLoad(string  pPath, bool log)
		{
			try
			{
				int picPos = 0;
				int picAll = 0;
				if (log) {
					_il.DirPosPath(ref picPos, ref picAll, pPath);
					SetStatusText(0, String.Format(_picSelection + " {0}/{1}", picPos, picAll));
					_il.LogPic(pPath);
				}
				else {
					_il.LogPos(ref picPos, ref picAll);
					SetStatusText(0, String.Format(T._("History: {0}/{1}"), picPos, picAll));
				}
				SetWindowText(pPath);
				_priorPath =_currentPath;
				_currentPath = pPath;

				if (!File.Exists(pPath)){
					picBox.SizeMode = PictureBoxSizeMode.CenterImage;
					picBox.Image = picBox.ErrorImage;
					return false;
				}

				Stopwatch sw1 = new Stopwatch();
				sw1.Start();
				bool showOk = ShowExif();
				sw1.Stop();
				if (!showOk){
					Util.ExifOrient(ref _exifType, ref _orientation, _currentPath);
				}

				//Debug.WriteLine("Ticks: " + sw1.Elapsed.Ticks.ToString());
				var t = new DateTime(sw1.Elapsed.Ticks);
				//Debug.WriteLine("Exif time: {0:D2}s:{1:D5}ms", t.Second, t.Millisecond);

				//Image myImg;
				using (FileStream stream = new FileStream(pPath, FileMode.Open, FileAccess.Read))
				{
					_myImg = Image.FromStream(stream);  // abort for gif
					if (_orientation.Equals("right side, top (rotate 90 cw)")){
						_myImg.RotateFlip(RotateFlipType.Rotate90FlipNone);
					}
					else if (_orientation.Equals("bottom, right side (rotate 180)")){
						_myImg.RotateFlip(RotateFlipType.Rotate180FlipNone);
					}
					else if (_orientation.Equals("left side, bottom (rotate 270 cw)")){
						_myImg.RotateFlip(RotateFlipType.Rotate270FlipNone);
					}

					stream.Close();
				}
				GC.Collect();
				Application.DoEvents();

				//using (Image bmpTemp = new Bitmap(pPath))      // abort for invalid jpg
				//{
				//  _myImg = new Bitmap(bmpTemp);
				//  if(bmpTemp != null)
				//    ((IDisposable)bmpTemp).Dispose();
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
						SetWindowSize(_scWidth, ih + _borderHeight, _exifType);
					}
					else {    // high img
						int iw = (imWidth * (_scHeight - _borderHeight) / imHeight);// + _borderWidth;
						SetWindowSize(iw + _borderWidth, _scHeight, _exifType);
					}
				}
				else {  // small img
					picBox.SizeMode = PictureBoxSizeMode.CenterImage;
					picBox.BackColor = Color.Black;
					SetWindowSize(imWidth + _borderWidth, imHeight + _borderHeight, _exifType);
				}
				if (_wType != WinType.second){
					Settings.Default.LastImage = pPath;
				}

				Show2ndPic(_priorPath);
				//Debug.WriteLine("pic end " + pPath);
				return true;
			}
			catch (Exception e)
			{
				picBox.Image = null;
				MessageBox.Show(T._("File is invalid") + "\n "  + pPath + "\n " + e.Message, T._("Invalid file"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
		}


		public void PicScan(string  pPath, bool allDirs)
		{
			_il.DirScan(pPath, allDirs);
		}

		void DarkPic()
		{
			picBox.Image = null;
			Dark2nd();
		}

		public void NextPic()
		{
			string pPath = "";
			if (_il.DirPicNext(ref pPath)){
				PicLoad(pPath, true);
			}
			else {
				SetStatusText(0, T._("No image loaded"));
			}
		}

		public void NextPicDir()
		{
			PicScan(_currentPath, false);
			int picPos = 0;
			int picAll = 0;
			_il.DirPosPath(ref picPos, ref picAll, _currentPath);
			NextPic();
		}

		public void PriorPic()
		{
			string pPath = "";
			if (_il.DirPicPrior(ref pPath)){
				PicLoad(pPath, true);
			}
			else {
				SetStatusText(0, T._("No image loaded"));
			}
		}

		public void PriorPicDir()
		{
			PicScan(_currentPath, false);
			int picPos = 0;
			int picAll = 0;
			_il.DirPosPath(ref picPos, ref picAll, _currentPath);
			PriorPic();
		}

		public void FirstPic()
		{
			string pPath = "";
			if (_il.DirPicFirst(ref pPath)){
				PicLoad(pPath, true);
			}
			else {
				SetStatusText(0, T._("No image loaded"));
			}
		}

		public void LastPic()
		{
			string pPath = "";
			if (_il.DirPicLast(ref pPath)){
				PicLoad(pPath, true);
			}
			else {
				SetStatusText(0, T._("No image loaded"));
			}
		}

		public void BackPic()
		{
			string pPath = "";
			if (_il.LogBack(ref pPath)){
				PicLoad(pPath, false);
			}
		}

		public void ForwardPic()
		{
			string pPath = "";
			if (_il.LogForward(ref pPath)){
				PicLoad(pPath, false);
			}
		}

		public void RefreshDir()
		{
			_il.DirClear();
			PicScan(_currentPath, false);
			PicLoad(_currentPath, true);
		}

		public void RenamePic()
		{
			string newPath = "";
			frmRename frm = new frmRename(_currentPath);
			var result = frm.ShowDialog();
			if (frm._ReturnPath != "") {
				newPath = frm._ReturnPath;
				_il.RenameListLog(_currentPath, newPath);
				_currentPath = newPath;
				SetWindowText(_currentPath);
				//PicLoad(_currentPath, true);
			}
		}

		public void RenamePicPlus()
		{
			string fname = Path.GetFileNameWithoutExtension(_currentPath);
			string fext = Path.GetExtension(_currentPath);
			string newPath = Path.GetDirectoryName(_currentPath) + @"\" + fname + "+" + fext;
			if (FileRename2(_currentPath, newPath)) {
				_il.RenameListLog(_currentPath, newPath);
				_currentPath = newPath;
				SetWindowText(_currentPath);
				//PicLoad(_currentPath, true);
			}
			else {
				Debug.WriteLine("no rename");
			}
		}

		public void RemovePicPlus()
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
					SetWindowText(_currentPath);
					//PicLoad(_currentPath, true);
				}
				else {
					Debug.WriteLine("no rename");
				}
			}
		}

		// ------------------------------   Tempmark functions  ----------------------------------------------------------
		public void TempmarkDelete()
		{
			if (!_il.MarkDelete(_currentPath)){
				MessageBox.Show(T._("This image is not marked"), T._("Error"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		public void TempmarkGo()
		{
			string markPath = "";
			if (_il.MarkGo(ref markPath)){
				PicLoad(markPath, true);
				_currentPath = markPath;
			}
			else {
				MessageBox.Show(T._("No image is marked yet"), T._("Error"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		public void TempmarkPic()
		{
			_il.MarkPic(_currentPath);
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

		public void DelPic()
		{
			if (DelFile.MoveToRecycleBin(_currentPath)){
				string nextPath = "";
				if (_il.DeleteListLog(_currentPath, ref nextPath)){
					PicLoad(nextPath, true);
					_currentPath = nextPath;
				}
				else {  // last img in selection deleted
					picBox.Image = null;
					SetStatusText(0, T._("No image loaded"));
				}
			}
			else {

			}
		}

		public void OpenPic()
		{
			var dialog = new OpenFileDialog();
			string lastPath = Settings.Default.LastImage;
			if (File.Exists(lastPath)){
				if (Directory.Exists(Path.GetDirectoryName(lastPath))) {
					dialog.InitialDirectory = Path.GetDirectoryName(lastPath);
				}
			}
			dialog.Filter = "All images |*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.ico;*.tif;*.wmf;*.emf|JPEG files |*.jpg;*.jpeg|PNG files |*.png|GIF files |*.gif|Bitmap files |*.bmp|Icon files |*.ico|TIF files |*.tif|WMF files |*.wmf|EMF files |*.emf";
			dialog.Title = T._("Select image");

			if(dialog.ShowDialog() == DialogResult.OK)
			{
				string picPath = dialog.FileName;
				PicScan(picPath, false);
				PicLoad(picPath, true);
				SetCommand('r', picPath);
			}
		}

		// ------------------------------   pop up  ----------------------------------------------------------

		void PopOpenClick(object sender, EventArgs e)
		{
			OpenPic();
		}

		void PopRenameClick(object sender, EventArgs e)
		{
			RenamePic();
		}

		void PopDeleteClick(object sender, EventArgs e)
		{
			DelPic();
		}

		void PopSearchClick(object sender, EventArgs e)
		{
			SearchPic();
		}

		void PopStartEditorClick(object sender, EventArgs e)
		{
			StartEditor();
		}

		void PopNextClick(object sender, EventArgs e)
		{
			NextPic();
		}

		void PopPriorClick(object sender, EventArgs e)
		{
			PriorPic();
		}

		void PopRefreshClick(object sender, EventArgs e)
		{
			RefreshDir();
		}

		void PopFullscreenClick(object sender, EventArgs e)
		{
			ShowFullScreen();
		}

		void PopCloseClick(object sender, EventArgs e)
		{
			if (_wType == WinType.second){
				this.Close();
			}
			if (_wType == WinType.normal){
				Application.Exit();
				Environment.Exit(0);
			}
		}

		// ------------------------------   other functions   ----------------------------------------------------------

		public void SearchPic()
		{
			SearchForm frm = new SearchForm(_currentPath, _lastSearchStr, _il);
			frm.ShowDialog();

			_lastSearchStr = frm._lastSearchStr;
			if (frm._SearchReturn) {
				string selImg = frm._selImg;
				int picPos = 0;
				int picAll = 0;
				if (selImg != ""){
					_il.DirPosPath(ref picPos, ref picAll, selImg);
				}
				else {
					_il.DirPicFirst(ref selImg);
				}
				_currentPath = selImg;
				_picSelection = T._("Search:");
				PicLoad(_currentPath, true);
			}
		}

		public void ShowExifImages(List<string> exImgList, string selImg)
		{
			_il._imList = exImgList;
			int picPos = 0;
			int picAll = 0;
			_il.DirPosPath(ref picPos, ref picAll, selImg);

			_currentPath = selImg;
			_picSelection = T._("Search:");
			PicLoad(_currentPath, true);
		}

		public void ShowFullScreen()
		{
			string pPath = "";
			var frm = new FullScreen(_il);
			if (_il.DirPosCurrent(ref pPath)){
				frm.FPicLoad(pPath, false);
				var result = frm.ShowDialog();
				pPath = frm.ReturnPath;
				PicLoad(pPath, true);
			}
			else {
				SetStatusText(0, T._("No image loaded"));
			}
		}

		public void StartEditor()
		{
			string editorPath = Settings.Default.Editor;
			if ((editorPath == "")||(!File.Exists(editorPath))) {    // extension default editor
				Util.StartEditor(_currentPath, "");
			}
			else {
				Util.StartEditor(editorPath, _currentPath);
			}
		}

		public void Test()
		{
			m_Exif.Close();
		}

		public void TranslateImageForm( )
		{
		  _picSelection = T._("Directory:");
		  popOpen.Text = T._("Open...");
		  popRename.Text = T._("Rename...");
		  popDelete.Text = T._("Delete");
		  popSearch.Text = T._("Search...");
		  popStartEditor.Text = T._("Start editor...");
		  popNext.Text = T._("Next image");
		  popPrior.Text = T._("Prior image");
		  popRefresh.Text = T._("Refresh");
		  popFullscreen.Text = T._("Full screen");
		  popClose.Text = T._("Exit");

		}

		// ------------------------------   2nd screen    ----------------------------------------------------------

		public void Start2ndScreen()
		{
			string prPath = _priorPath;
			if (prPath == ""){
				prPath = _currentPath;
			}

			if (CanStart2nd()){
				m_Image2  = new frmImage(0, 0, WinType.second);
				m_Image2.PicLoad(prPath, false);
				m_Image2.Show();

			}
			else {    // img to foreground
				if (CanShow2nd()){
					m_Image2.BringToFront();
				}
			}
		}

		public void Show2ndPic(string prPath)
		// called by: PicLoad
		{
			if (CanShow2nd()){
				if (prPath == ""){
					prPath = _currentPath;
				}
				if (File.Exists(prPath)){
					m_Image2.PicLoad(prPath, false);
				}
			}
		}

		bool CanStart2nd()
		{
			if (_wType == WinType.second){
				return false;
			}
			if (m_Image2 == null){
				return true;
			}
			return !m_Image2._ndRunning;
		}

		bool CanShow2nd()
		{
			if (_wType == WinType.second){
				return false;
			}
			if (m_Image2 == null){
				return false;
			}
			return m_Image2._ndRunning;
		}

		public void Dark2nd()
		{
			if (CanShow2nd()){
				m_Image2.picBox.Image = null;
			}
		}

		public void Close2nd()
		{
			if (CanShow2nd()){
				m_Image2.RClose();
			}
			if (CanShowExif()){
					m_Exif.Close();
			}
		}

		// ------------------------------   Exif screen    ----------------------------------------------------------


		public void ShowExifDash()
		{

//					string selImg = frmDash._exifImg;
//					int picPos = 0;
//					int picAll = 0;
//					if (selImg != ""){
//						_il.DirPosPath(ref picPos, ref picAll, selImg);
//					}
//					else {
//						_il.DirPicFirst(ref selImg);
//					}
//					_currentPath = selImg;
//					_picSelection = T._("Exif Search:");
//					PicLoad(_currentPath, true);


		}

		public void ShowExif0()
		{
			if (File.Exists(_currentPath)) {
				ExifForm0 frmExif0 = new ExifForm0();
				frmExif0.CheckFile0(_currentPath);
				frmExif0.ShowDialog();
			}
		}


		public bool StartExif()   //  with 'e'
		{
			try
			{
				if (!File.Exists(_currentPath))
					return false;

				if (CanStartExif()){
					m_Exif = new ExifForm();
					m_Exif.KeyChanged += new HandleKeyChange(HandleKey);
					m_Exif.CheckFile(ref _exifType, ref _orientation, _currentPath);
					m_Exif.Show();
				}
				else {    // img to foreground
					if (CanShowExif()){
						m_Exif.Show();
						m_Exif.BringToFront();
					}
				}
				return true;
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message, "Exif", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
		}

		public bool ShowExif()
		// for each pic
		{
			bool showRet = CanShowExif();
			if (showRet){
				m_Exif.CheckFile(ref _exifType, ref _orientation, _currentPath);
			}
			return showRet;
		}

		bool CanStartExif()
		{
			if (m_Exif == null){
				return true;
			}
			return false;
		}

		bool CanShowExif()
		{
			if (m_Exif == null){
				return false;
			}
			return true;
		}


		// ------------------------------   delegates   ----------------------------------------------------------

		public void SetWindowText(string text2)
		{
			// called by: PicLoad, RenamePic, RenamePicPlus, RemovePicPlus
			// output: main.HandleWindow
			this.Text = text2 + "  -  Next-View";

			OnWindowChanged(new SetTitleEventArgs(text2));
			Application.DoEvents();
		}

		protected virtual void OnWindowChanged(SetTitleEventArgs e)
		{
			if(this.WindowChanged != null)     // nothing subscribed to this event
			{
				this.WindowChanged(this, e);
			}
		}


		public void SetWindowSize(int w, int h, int exifType)
		{
			// called by: PicLoad 3*, Enter
			// output: main.SetWindowSize
			OnWindowSize(new SetSizeEventArgs(w, h, exifType));
			_currentWidth = w;
			_currentHeight = h;
			Application.DoEvents();
		}

		protected virtual void OnWindowSize(SetSizeEventArgs  e)
		{
			if(this.WindowSize != null)    // nothing subscribed to this event
			{
				this.WindowSize(this, e);
			}
		}


		public void SetStatusText(int sVal, string sText)
		{
			// called by: PicLoad, or 5* 'no img loaded'
			// output: main.HandleStatus
			OnStatusChanged(new SetStatusMainEventArgs(sVal, sText));
			Application.DoEvents();
		}

		protected virtual void OnStatusChanged(SetStatusMainEventArgs e)
		{
			if(this.StatusChanged != null)     // nothing subscribed to this event
			{
				this.StatusChanged(this, e);
			}
		}


		public void SetCommand(char comm, string fName)
		{
		// called by: recent: Filename: openPic, FrmImageDragDrop
		// dash: kdown e

		// main command, HandleFilename
			OnCommandChanged(new SetCommandEventArgs(comm, fName));
			Application.DoEvents();
		}

		protected virtual void OnCommandChanged(SetCommandEventArgs e)
		{
			if(this.CommandChanged != null)
			{
				this.CommandChanged(this, e);
			}
		}


	}  // end frmImage

	public enum WinType
	{
		normal = 0,
		full = 1,
		second = 2
	}

	// ------------------------------		delegates 	----------------------------------------------------------

	public delegate void HandleKeyChange(object sender, SetKeyEventArgs e);

}
