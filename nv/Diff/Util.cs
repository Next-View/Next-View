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
using System.Drawing;  // graphics
using System.Diagnostics;
using System.Linq;	 //	OfType
using System.Globalization;   // CultureInfo
using System.Runtime.InteropServices;    // DllImport
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
				MessageBox.Show(eMessage, T._("Error"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
				MessageBox.Show(e.Message, T._("Error"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
		}

        [DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
        public enum DeviceCap
        {
            VERTRES = 10,
            DESKTOPVERTRES = 117,
        	// https://stackoverflow.com/questions/5977445/how-to-get-windows-display-settings
            // http://pinvoke.net/default.aspx/gdi32/GetDeviceCaps.html
        }

        public static float getScalingFactor()
        {
            Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr desktop = g.GetHdc();
            int LogicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.VERTRES);
            int PhysicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.DESKTOPVERTRES); 
        
            float ScreenScalingFactor = (float)PhysicalScreenHeight / (float)LogicalScreenHeight;
        
            return ScreenScalingFactor; // 1.25 = 125%
        }
	}
}
