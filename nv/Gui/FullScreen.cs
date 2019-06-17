/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
File name:     fullscreen.cs
Description:   full screen to show images
Copyright:     Copyright (c) Martin A. Schnell, 2018
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
using	System.Diagnostics;	 //	Debug
using System.Drawing;
using	System.IO;	 //	directory
using System.Windows.Forms;
using	Next_View.Properties;   // settings

namespace Next_View
{
	/// <summary>
	/// Description of FullScreen.
	/// </summary>
	public partial class FullScreen : Form
	{
		ImgList _il;
		int _fHeight = 0;
		int _fWidth = 0;
		string _currentPath = "";
		Image _fullImg;

		public string ReturnPath {get;set;}

		public FullScreen(ImgList il)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			_il = il;

			_fHeight = Screen.FromControl(this).Bounds.Height;
			_fWidth = Screen.FromControl(this).Bounds.Width;
			Debug.WriteLine("Full Image W / H: {0}/{1}", _fWidth, _fHeight);

		}

		void FullScreenKeyDown(object sender, KeyEventArgs e)
		{
			//Debug.WriteLine("full:	" + e.KeyValue.ToString());
			bool alt = false;
			if (e.Modifiers == Keys.Alt){
				alt = true;
			}

			bool ctrl = false;
			if (e.Modifiers == Keys.Control){
				ctrl = true;
			}

			switch(e.KeyValue)
			{
				case 39:  //  ->
					if (alt){
						FForwardPic();
					}
					else {
						FNextPic();
					}
					break;
				case 34:  //  pd
				case 32:  //  space
					FNextPic();
					break;
				case 37:  // <-
					if (alt){
						FBackPic();
					}
					else {
						FPriorPic();
					}
					break;
				case 33:  // pu
					FPriorPic();
					break;
				case 36:    // pos 1
					FFirstPic();
					break;
				case 35:    // end
					FLastPic();
					break;
				case 8:    // back
					FBackPic();
					break;

				case 68:    // d   dark
					FDarkPic();
					break;
				case 113:    // F2
					FRenamePic();
					break;
				case 116:    // F5
					FRefreshDir();
					break;
				case 46:    // del
					FDelPic();
					break;
				case 70:    // ctrl F
					if (ctrl){
						FSearchPic();
					}
					break;

				case 107:    // +
				case 187:    // +
					FRenamePicPlus();
					break;
				case 109:    // -
				case 189:    // -
					FRemovePicPlus();
					break;
				case 13:     // Enter
				case 27:     // esc   end full screen
					FullScreenEnd();
					break;
			}
		}

		public bool FPicLoad(string pPath, bool log)
		 {
			try
			{
				_currentPath = pPath;

			 	if (!File.Exists(pPath)){
			 		fullBox.SizeMode = PictureBoxSizeMode.CenterImage;
			 		fullBox.Image = fullBox.ErrorImage;
			 		return false;
			 	}

				using (FileStream stream = new FileStream(pPath, FileMode.Open, FileAccess.Read))
				{
					_fullImg = Image.FromStream(stream);  // abort for gif
					stream.Close();
				}
				GC.Collect();
 				Application.DoEvents();

				string ext = Path.GetExtension(pPath).ToLower();
				if (ext == ".gif"){
					fullBox.Image = Image.FromFile(pPath);    // workaround, only direct load makes gif animation, but file can't be renamed
				}
				else {
					fullBox.Image = _fullImg;
				}

				int pHeight = _fullImg.Height;
				int pWidth = _fullImg.Width;
				if ((pHeight > _fHeight) || (pWidth > _fWidth)){
					fullBox.SizeMode = PictureBoxSizeMode.Zoom;
				}
				else {
					fullBox.SizeMode = PictureBoxSizeMode.CenterImage;
				}
				Settings.Default.LastImage = pPath;
				return true;
			}
			catch (Exception e)
			{
				fullBox.Image = null;
				MessageBox.Show("File is invalid \n" + pPath + "\n " + e.Message, "Invalid file", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
		}

		public void FPicScan(string	pPath, bool allDirs)
		{
			int pCount;
			_il.DirScan(out pCount, pPath, allDirs);
		}

		void FDarkPic()
		{
			fullBox.Image = null;
		}

		public void	FNextPic()
		{
			string pPath = "";
			_il.DirPicNext(ref pPath);
			FPicLoad(pPath, true);
		}

		public void	FPriorPic()
		{
			string pPath = "";
			_il.DirPicPrior(ref pPath);
			FPicLoad(pPath, true);
		}

		public void	FFirstPic()
		{
			string pPath = "";
			_il.DirPicFirst(ref pPath);
			FPicLoad(pPath, true);
		}

		public void	FLastPic()
		{
			string pPath = "";
			_il.DirPicLast(ref pPath);
			FPicLoad(pPath, true);
		}

		public void	FBackPic()
		{
			string pPath = "";
			if (_il.LogBack(ref pPath)){
				FPicLoad(pPath, false);
			}
		}

		public void	FForwardPic()
		{
			string pPath = "";
			if (_il.LogForward(ref pPath)){
				FPicLoad(pPath, false);
			}
		}

		public void	FRenamePic()
		{
			string pPath = "";
			frmRename frm = new frmRename(_currentPath);
			var result = frm.ShowDialog();
			if (frm._ReturnPath != "") {
				pPath = frm._ReturnPath;
				_il.RenameListLog(_currentPath, pPath);
				_currentPath = pPath;
			}
		}

		public void	FRenamePicPlus()
		{
			string fname = Path.GetFileNameWithoutExtension(_currentPath);
			string fext = Path.GetExtension(_currentPath);
			string newPath = Path.GetDirectoryName(_currentPath) + @"\" + fname + "+" + fext;
			if (FFileRename(_currentPath, newPath)) {
				_il.RenameListLog(_currentPath, newPath);
				_currentPath = newPath;
				FPicLoad(_currentPath, true);
			}
		}

		public void	FRemovePicPlus()
		{
			string fname = Path.GetFileNameWithoutExtension(_currentPath);
			string fext = Path.GetExtension(_currentPath);
			string lastChar = fname.Substring(fname.Length - 1);
			if (lastChar == "+"){
				fname = fname.Substring(0, fname.Length - 1);
				string newPath = Path.GetDirectoryName(_currentPath) + @"\" + fname + fext;
				if (FFileRename(_currentPath, newPath)) {
					_il.RenameListLog(_currentPath, newPath);
					_currentPath = newPath;
					FPicLoad(_currentPath, true);
				}
			}
		}

		bool FFileRename(string nameFrom, string nameTo)
		{
			try {
				File.Move(nameFrom, nameTo);
				return true;
			}
			catch (Exception e){
				string eMessage = e.Message;
				return false;
			}
		}

		public void	FRefreshDir()
		{
			_il.DirClear();
			FPicScan(_currentPath, false);
			FPicLoad(_currentPath, true);
		}

		public void	FDelPic()
		{
			if (DelFile.MoveToRecycleBin(_currentPath)){
				string nextPath = "";
				int imgCount = 0;
				if(_il.DeleteListLog(_currentPath, ref nextPath, ref imgCount)){
					FPicLoad(nextPath, true);
					_currentPath = nextPath;
				}
				else {
					fullBox.Image = null;
				}
			}
		}

		public void	FSearchPic()
		{
			SearchForm frm = new SearchForm(_currentPath, "", _il);
			frm.ShowDialog();

			string pPath = "";
			if (frm._SearchReturn) {
				if (_il.DirPicFirst(ref pPath)){
					_currentPath = pPath;
					FPicLoad(_currentPath, true);
				}
			}
		}

		void FullScreenEnd()
		{
			string pPath = "";
			_il.DirPosCurrent(ref pPath);
			this.ReturnPath = pPath;
			this.Close();
		}
		void FullScreenLoad(object sender, EventArgs e)
		{
			this.Bounds = Screen.PrimaryScreen.Bounds;
		}
	}
}
