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
				MessageBox.Show("Invalid letter in filename" + Environment.NewLine
				+ newName + Environment.NewLine + "Invalid letters are: * / \\ [ ] : ; | = , \" ",
				"Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else {
				string newPath = Path.GetDirectoryName(_pPath) + @"\" + edFilename.Text + edExt.Text;
				string eMessage = "";
				if (!FileRename(_pPath, newPath, ref eMessage)) {
					MessageBox.Show("Can't rename the file" + Environment.NewLine
					+ newName + Environment.NewLine + eMessage,
					"Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

	}
}
