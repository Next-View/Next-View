﻿/*
 * Created by SharpDevelop.
 * User: martin
 * Date: 01.11.2019
 * Time: 21:39
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;  // Image
using System.Drawing.Imaging;  // frame
using System.IO;   // FileStream

namespace Next_View
{
	/// <summary>
	/// Description of GifImage.
	// stackoverflow.com/questions/13485477/can-a-picturebox-show-animated-gif-in-windows-application
	/// </summary>
	public class GifImage
	{
		private Image gifImage;
		private FrameDimension dimension;
		private int frameCount;
		private int currentFrame = -1;

		public GifImage(string path)
		{
			FileStream  fs = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
			MemoryStream ms = new System.IO.MemoryStream();
			fs.CopyTo(ms);
			fs.Close();
			ms.Position = 0;               
			gifImage = Image.FromStream(ms);
			dimension = new FrameDimension(gifImage.FrameDimensionsList[0]);
			frameCount = gifImage.GetFrameCount(dimension);
		}

		public Image GetNextFrame(out int fCount, out int fIndex)
		{
			currentFrame++;
			if (currentFrame >= frameCount) {
					currentFrame = 0;
			}
			fCount = frameCount; 
			fIndex = currentFrame + 1;
			return GetFrame(currentFrame);
		}

		public Image GetPriorFrame(out int fCount, out int fIndex)
		{
			currentFrame--;
			if (currentFrame < 0) {
					currentFrame = frameCount - 1;
			}
			fCount = frameCount; 
			fIndex = currentFrame + 1;			
			return GetFrame(currentFrame);
		}

		public Image GetFrame(int index)
		{
			gifImage.SelectActiveFrame(dimension, index);
			return (Image)gifImage.Clone();
		}

	}
}
