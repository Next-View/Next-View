/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
File name:     rename.cs
Description:   rename form for next-view
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
using System.Drawing;
using	System.IO;	 //	path
using System.Windows.Forms;

namespace Next_View
{
	/// <summary>
	/// Description of Rename.
	/// </summary>
	public partial class frmRename : Form
	{
		string _pPath;
		public string _ReturnPath {get;set;}
		char[] _invalidChars = {'*', '/', '\\', '[', ']', ':', ';', '|', '=', ',', '"'};

		public frmRename(string pPath)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			_pPath = pPath;
			_ReturnPath = "";
		}

		void FrmRenameLoad(object sender, EventArgs e)
		{
			TranslateRenameForm( );
		}
		
		void FrmRenameShown(object sender, EventArgs e)
		{
			edFilename.Text = Path.GetFileNameWithoutExtension(_pPath);
			edExt.Text = Path.GetExtension(_pPath);
		}

		void CmdRenameOkClick(object sender, EventArgs e)
		{
			// dialog result must be 'none' to stay open in case of error
			string newName = edFilename.Text;
			if (newName.IndexOfAny(_invalidChars) > -1) {
				MessageBox.Show(T._("Invalid letter in filename") + Environment.NewLine
				+ newName + Environment.NewLine + T._("Invalid letters are") + ": * / \\ [ ] : ; | = , \" ",
				T._("Error"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else {
				string newPath = Path.GetDirectoryName(_pPath) + @"\" + edFilename.Text + edExt.Text;
				string eMessage = "";
				if (!FileRename(_pPath, newPath, ref eMessage)) {
					MessageBox.Show(T._("Can't rename the file") + Environment.NewLine
					+ newName + Environment.NewLine + eMessage,
					T._("Error"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				else {
					this._ReturnPath = newPath;
					this.Close(); 
				}
			}
		}

		bool FileRename(string nameFrom, string nameTo, ref string eMessage)
		{
			try {
				File.Move(nameFrom, nameTo);
				return true;
			}
			catch (Exception e){
				eMessage = e.Message;
				return false;
			}
		}

		void CmdRenameCancelClick(object sender, EventArgs e)
		{
			this._ReturnPath = "";
			this.Close();
		}
		
		public void TranslateRenameForm( )
		{
			Text = T._("Rename");
			cmdRenameOk.Text = T._("&OK");
			cmdRenameCancel.Text = T._("&Cancel"); 
			label1.Text = T._("New name:");
		}


	}
}
