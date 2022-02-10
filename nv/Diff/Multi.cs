/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
File name:     multi.cs
Description:   handling multiple screens
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
using System.Diagnostics;
using System.Drawing;  // rectangle
using System.Windows.Forms;  // Screen
using Next_View.Properties;

namespace Next_View
{
	/// <summary>
	/// Description of Multi
	/// </summary>
	public static class Multi
	{

		public static int ScreenNumbers()
		{
		    return Screen.AllScreens.Length;
		}
		
		public static void MainLoad(out int left, out int top)
		{
			if(Screen.AllScreens.Length == 1){
				left = Settings.Default.MainX;
				top = Settings.Default.MainY;
			}
			else {
				left = Settings.Default.Main2X;
				top = Settings.Default.Main2Y;
			}
		}
		
		public static void MainSave(int left, int top)
		{
			if(Screen.AllScreens.Length == 1){
				Settings.Default.MainX = left;
				Settings.Default.MainY = top;
			}
			else {
				Settings.Default.Main2X = left;
				Settings.Default.Main2Y = top;
			}
		}
		
		public static void ExifLoad(out int left, out int top)
		{
			if(Screen.AllScreens.Length == 1){
				left = Settings.Default.ExifX;
				top = Settings.Default.ExifY;
			}
			else {
				left = Settings.Default.Exif2X;
				top = Settings.Default.Exif2Y;
			}
		}
		
		public static void ExifSave(int left, int top)
		{
			if(Screen.AllScreens.Length == 1){
				Settings.Default.ExifX = left;
				Settings.Default.ExifY = top;
			}
			else {
				Settings.Default.Exif2X = left;
				Settings.Default.Exif2Y = top;
			}
		}

		public static void SecondLoad(out int left, out int top)
		{
			if(Screen.AllScreens.Length == 1){
				left = Settings.Default.SecondX;
				top = Settings.Default.SecondY;
			}
			else {
				left = Settings.Default.Second2X;
				top = Settings.Default.Second2Y;
			}
		}

		public static void SecondSave(int left, int top)
		{
			if(Screen.AllScreens.Length == 1){
				Settings.Default.SecondX = left;
				Settings.Default.SecondY = top;
			}
			else {
				Settings.Default.Second2X = left;
				Settings.Default.Second2Y = top;
			}
		}
		
		public static void FormShowVisible(out bool visible, ref int left, ref int top, int width, int height)
		// checks x, y, pos, and fix position if form not visible
		// called by: main, image (2nd), exif; each 2*
		{
			visible = false;
			int areaForm = width * height;
			int areaVisible = 0;
			int areaScreenVisibleMax = 0;
			var rectItersect = new Rectangle();
			var screenFix = new Rectangle();        // screen with largest share of form
			var rectForm = new Rectangle(left, top, width, height);
			foreach (var screen in Screen.AllScreens)
			{
				var rectScreen = new Rectangle(screen.WorkingArea.Left, screen.WorkingArea.Top, screen.WorkingArea.Width, screen.WorkingArea.Height);
				rectItersect = Rectangle.Intersect(rectScreen, rectForm);
				if (!rectItersect.IsEmpty){
					int areaScreenVisible = rectItersect.Width * rectItersect.Height;
					areaVisible += areaScreenVisible;
					if (areaScreenVisible > areaScreenVisibleMax){
						areaScreenVisibleMax = areaScreenVisible;
						screenFix = rectScreen;
					}
				}
				//Debug.WriteLine("screens: X: {0}  Y: {1}  W: {2}  H: {3} ", screen.WorkingArea.Left, screen.WorkingArea.Top, screen.WorkingArea.Width, screen.WorkingArea.Height);
			}
			double visPerc = (double) areaVisible * 100.0 / areaForm;
			if (visPerc >= 60.0){
				visible = true;
			}
			else if (visPerc == 0){
				left = 100;
				top = 100;
			}
			else {
				if (screenFix.Top > rectForm.Top){
					top = 10;
				}
				if (screenFix.Left > rectForm.Left){
					left = 10;
				}
				if (screenFix.Right < rectForm.Right){
					left = screenFix.Right - rectForm.Width;
				}
				if (screenFix.Bottom < rectForm.Bottom){
					top = screenFix.Bottom - rectForm.Height;
				}
				if (top < 0) top = 10;
				if (left < 0) left = 10;
				//Debug.WriteLine("sc: ", screenFix.ToString());
				//Debug.WriteLine("ar: ", rectForm.ToString());
			}
			//Debug.WriteLine("visib: {0} ", visPerc);
		}
		
	}
}
