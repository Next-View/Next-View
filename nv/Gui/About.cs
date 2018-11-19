/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
File name:     about.cs
Description:   about form for next-view
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
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;   // for webBrowser

namespace Next_View
{
	/// <summary>
	/// About form.
	/// </summary>
	public partial class frmAbout : Form
	{
		public frmAbout()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}

		void frmAboutShown(object sender, EventArgs e)
		{
			Version v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
			string ver = String.Format("Next-View Version: {0}.{1}.{2}.{3}", v.Major, v.Minor, v.Build, v.Revision);
			this.Text = ver;

			string curDir = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
			string aboutPath = curDir + @"\about\about.html";
			if (File.Exists(aboutPath)) {
				webBrowser1.Url = new Uri("file:///" + aboutPath);
			}
			else {
				webBrowser1.DocumentText = aboutPath + " file is missing";
			}
		}

		void WebBrowser1Navigating(object sender, WebBrowserNavigatingEventArgs e)
		{
			string bUrl = e.Url.ToString();
			Debug.WriteLine("url: " + bUrl);
			if (bUrl.StartsWith("http")){
				e.Cancel = true;
				Process.Start(bUrl);
			}
		}


		void CmdOkClick(object sender, EventArgs e)
		{
			Debug.WriteLine("Path: " + System.Reflection.Assembly.GetExecutingAssembly().Location);
			Debug.WriteLine("Version: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);
			Debug.WriteLine("Conf: " + ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath);
			Debug.WriteLine("App path: " + ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath);
			this.Close();
		}


		void FrmAboutFormClosed(object sender, FormClosedEventArgs e)
		{
			webBrowser1.Dispose();
		}


	}
}
