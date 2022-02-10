/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
File name:     optionform.cs
Description:   options form for next-view
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
using System.Diagnostics;
using System.IO;   // path
using System.Windows.Forms;
using System.Xml;        // XmlTextReader
using Next_View.Properties;

namespace Next_View
{
	/// <summary>
	/// Description of OptionForm.
	/// </summary>
	public partial class frmOption : Form
	{
	    public event HandleKeyChange  KeyChanged;
	    
		public frmOption()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}


		//--------------------------  events form  ------------------------------------//

		void frmOptionLoad(object sender, EventArgs e)
		{
			OptionTranslate();
			this.edEditor.Text = Settings.Default.Editor;
			this.chkImageEditor.Checked = Settings.Default.UseMediaDefault;
			this.chkHide.Checked = Settings.Default.HideImg;
		}		

		void FrmOptionHelpRequested(object sender, HelpEventArgs hlpevent)
		{
		    var c = this.ActiveControl;
            if(c!=null)
                MessageBox.Show(c.Name);
		}

		void FrmOptionActivated(object sender, EventArgs e)
		{
	        SetKeyChange(82, true, false);    // R efresh	
		}
		
		void FrmOptionDeactivate(object sender, EventArgs e)
		{
		    if (Settings.Default.HideImg)
		    {
    	        if (frmRename.ActiveForm == null)   //  app inactive
    	        {
    	            SetKeyChange(68, true, false);     // D ark
    	        }
	        }
		}
						
		//--------------------------  buttons  ------------------------------------//
				
		void CmdCancelClick(object sender, EventArgs e)
		{
			this.Close();
		}

		void CmdOkClick(object sender, EventArgs e)
		{
		    Settings.Default.HideImg = this.chkHide.Checked;
			if (File.Exists(edEditor.Text)){

				Settings.Default.Editor = this.edEditor.Text;
				Settings.Default.UseMediaDefault = this.chkImageEditor.Checked;

				Settings.Default.Save( );
				this.Close();
			}
			else {
				MessageBox.Show ("File does not exist " + "\n" + edEditor.Text, "File error",
						MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		void CmdEditorClick(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			//? dialog.InitialDirectory = initialDirectory;
			dialog.Filter = "Exe files (*.exe)|*.exe";
			dialog.Title = "Select editor";

			if(dialog.ShowDialog() == DialogResult.OK)
			{
				edEditor.Text = dialog.FileName;
			}
		}


		void ChkHideCheckedChanged(object sender, EventArgs e)
		{
			Settings.Default.HideImg = this.chkHide.Checked;
		}
				
		//--------------------------  methods  ------------------------------------//

		public void OptionTranslate()
		{
		    this.Text = T._("Options");
		    tabGeneral.Text = T._("General");
			lblEditor.Text = T._("Image editor");
			chkImageEditor.Text = T._("Use this program to edit images");
			chkHide.Text = T._("Hide image on deactivate");
			cmdOk.Text = T._("&OK");
			cmdCancel.Text = T._("&Cancel");
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
