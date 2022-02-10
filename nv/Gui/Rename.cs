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
using System.Diagnostics;  // Debug
using System.Drawing;
using System.IO;	 //	path
using System.Windows.Forms;
using Next_View.Properties;

namespace Next_View
{
	/// <summary>
	/// Description of Rename.
	/// </summary>
	public partial class frmRename : Form
	{
		string _pPath;
		public string _ReturnPath {get;set;}
		char[] _invalidChars = {'*', '/', '\\', '[', ']', ':', ';', '|', '=', '"'};

        public event HandleKeyChange  KeyChanged;
        
		public frmRename(string pPath)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			_pPath = pPath;
			_ReturnPath = "";
		}

		// ------------------------------   events form ----------------------------------------------------------
		
		void FrmRenameLoad(object sender, EventArgs e)
		{
			TranslateRenameForm( );
		}
		
		void FrmRenameShown(object sender, EventArgs e)
		{
			edFilename.Text = Path.GetFileNameWithoutExtension(_pPath);
			edExt.Text = Path.GetExtension(_pPath);
		}

		void FrmRenameHelpRequested(object sender, HelpEventArgs hlpevent)
		{
		    var c = this.ActiveControl;
            if(c!=null)
                MessageBox.Show(c.Name);
		}

		void FrmRenameActivated(object sender, EventArgs e)
		{
	        SetKeyChange(82, true, false);    // R efresh
		}
		
		void FrmRenameDeactivate(object sender, EventArgs e)
		{
		    if (Settings.Default.HideImg)
		    {
    	        if (frmRename.ActiveForm == null)   //  app inactive
    	        {
    	            SetKeyChange(68, true, false);     // D ark
    	        }
	        }
		}
				
		// ------------------------------   button ----------------------------------------------------------
		
		void CmdRenameOkClick(object sender, EventArgs e)
		{
			// dialog result must be 'none' to stay open in case of error
			string newName = edFilename.Text;
			int invalPos = newName.IndexOfAny(_invalidChars);
			if (invalPos > -1) {
				edFilename.Focus();
				edFilename.Select(invalPos, 1);
				MessageBox.Show(T._("Invalid letter in filename, position " + invalPos) + Environment.NewLine
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

		void CmdRenameCancelClick(object sender, EventArgs e)
		{
			this._ReturnPath = "";
			this.Close();
		}

		// ------------------------------   functions ----------------------------------------------------------
				
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
 	
		public void TranslateRenameForm( )
		{
			Text = T._("Rename");
			cmdRenameOk.Text = T._("&OK");
			cmdRenameCancel.Text = T._("&Cancel"); 
			label1.Text = T._("New name:");
		}

		// ------------------------------   delegates   ----------------------------------------------------------

		public void SetKeyChange(int kVal, bool alt, bool ctrl)
		{
			// called by: PicLoad, 'no img loaded'
			// output: imageForm.HandleKey
			OnKeyChanged(new SetKeyEventArgs(kVal, alt, ctrl));
			Application.DoEvents();
		}

		protected virtual void OnKeyChanged(SetKeyEventArgs e)
		{
			if(this.KeyChanged != null)     // nothing subscribed to this event
			{
				this.KeyChanged(this, e);
			}
		}
		

	}
}
