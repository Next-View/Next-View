/*
 * Created by SharpDevelop.
 * User: martin
 * Date: 13.01.2019
 * Time: 20:53
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;  // Debug
using System.Windows.Forms;
using MetadataExtractor;

namespace Next_View
{
	/// <summary>
	/// Description of ExifForm0.
	/// </summary>
	public partial class ExifForm0 : Form
	{
		public ExifForm0()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void CmdClipClick(object sender, EventArgs e)
		{
			Clipboard.SetText(textExif0.Text);
		}

		public bool CheckFile0(string fName)
		{
			textExif0.Clear();
			this.Text = "File: " + fName;
			try
			{
				IEnumerable<Directory> directories = ImageMetadataReader.ReadMetadata(fName);


				foreach (var directory in directories)
				{
					textExif0.Text += Environment.NewLine;
					textExif0.Text += "Dir: " + directory + Environment.NewLine;
					foreach (var tag in directory.Tags)
					{
						textExif0.Text += directory.Name + " - " + tag.Name + " = " + tag.Description + Environment.NewLine;
					}
				}
				return true;
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.Message);
				MessageBox.Show("Exif. This file is invalid" + "\n " + e.Message, "Invalid file", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
		}

		void ExifForm0FormClosing(object sender, FormClosingEventArgs e)
		{
			this.Close();
		}

	}
}
