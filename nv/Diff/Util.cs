/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
File name:     util.cs
Description:   different utility functions
Copyright:     Copyright (c) Martin A. Schnell, 2013
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
using System.Windows.Forms;  // MessageBox

namespace Next_View
{
	/// <summary>
	/// Description of Util.
	/// </summary>
	public static class Util
	{
		private static int boxCount = 0;
		
		public static string SizeFormat(long fSize)
		{
			float fSizeKb = fSize / 1024;
			if (fSizeKb > 1024){
				float fSizeMb = fSizeKb / 1024;
				return fSizeMb.ToString("0.##") + " MB";
			}
			else return fSizeKb.ToString("0.##") + " KB";
		}

		public static bool ReportError(string eMessage)
		{		
			Debug.WriteLine(eMessage);
			if (boxCount == 0){
				MessageBox.Show(eMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			boxCount++;
			return true;
		}
		
		public static bool StartEditor(string eFilename, string eArg)
		{		
			try {			
				Process editor = new Process();
				editor.StartInfo.Arguments = eArg;
				editor.StartInfo.FileName   = eFilename;
				editor.Start();
				return true;
			}
			catch (Exception e){
				MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}			
		}
		
	}
}
