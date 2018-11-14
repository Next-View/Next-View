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

		//--------------------------  methods  ------------------------------------//

		public void OptionTranslate()
		{
			cmdOk.Text = "&OK";
			cmdCancel.Text = "&Cancel";
		}

		//--------------------------  events  ------------------------------------//
		void CmdCancelClick(object sender, EventArgs e)
		{
			this.Close();
		}

		void CmdOkClick(object sender, EventArgs e)
		{
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

		void frmOptionLoad(object sender, EventArgs e)
		{
			this.edEditor.Text = Settings.Default.Editor;
			this.chkImageEditor.Checked = Settings.Default.UseMediaDefault;
		}

		void ChkUseProxyCheckedChanged(object sender, EventArgs e)
		{

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

		void CmdExtAssignClick(object sender, EventArgs e)
		{
			foreach(ListViewItem li in listExtensions.Items)
			{
				string ext = li.Text;
				int spacePos = ext.IndexOf(" ");
				if (spacePos > -1){
					ext = ext.Substring(0, spacePos);
				}
				Debug.WriteLine("ext: " + ext);
			}
		}
	}
}
