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
//using System.Drawing;
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
			string ver = String.Format("Next-View Version: {0}.{1}.{2}", v.Major, v.Minor, v.Build);
			this.Text = ver;

			string curDir = Directory.GetCurrentDirectory();
			string aboutPath = curDir + @"\about\about.html";
			if (File.Exists(aboutPath)) {
				webBrowser1.Url = new Uri("file:///" + aboutPath);
				//webBrowser1.Url = new Uri("http://www.whatsmyuseragent.com/");
			}
			else {
				webBrowser1.DocumentText = aboutPath + " about.html file is missing";
			}

		}


		void CmdOkClick(object sender, EventArgs e)
		{
			Debug.WriteLine("Path: " + System.Reflection.Assembly.GetExecutingAssembly().Location);
			Debug.WriteLine("Version: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);
			Debug.WriteLine("Conf: " + ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath);
			Debug.WriteLine("Conf rl: " + ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath);
			this.Close();
		}


		void FrmAboutFormClosed(object sender, FormClosedEventArgs e)
		{
			webBrowser1.Dispose();
		}
	}
}
