/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
File name:     exifform0.cs
Description:   exifform0 form, all exif tags
Copyright:     Copyright (c) Martin A. Schnell, 2019
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
using System.Collections.Generic;
using System.Diagnostics;  // Debug
using System.Drawing;  // Image
using System.Drawing.Imaging;  // frame
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
			this.Text = T._("File") + ": " +  fName;
			try
			{
				IEnumerable<Directory> directories = ImageMetadataReader.ReadMetadata(fName);


				foreach (var directory in directories)
				{
					textExif0.Text += Environment.NewLine;
					textExif0.Text += "Dir: " + directory + Environment.NewLine;
					foreach (var tag in directory.Tags)
					{
						string tDesc = "";
						if (tag.Description != null) tDesc = tag.Description;
						textExif0.Text += directory.Name + " - " + tag.Name + " = " + tDesc.Replace("\0", String.Empty) + Environment.NewLine;
					}
				}
				string ext = System.IO.Path.GetExtension(fName).ToLower();
				if (ext == ".gif"){
					Image gifImage = Image.FromFile(fName);
					var dimension = new FrameDimension(gifImage.FrameDimensionsList[0]);
					int frameCount = gifImage.GetFrameCount(dimension);
					textExif0.Text += " " + Environment.NewLine;
					textExif0.Text += T._("Number of GIF images: ") + frameCount.ToString();
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
