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
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;	 //	OfType
using System.Windows.Forms;  // MessageBox
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using MetadataExtractor.Formats.Exif.Makernotes;

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
				MessageBox.Show(e.Message, T._("Error"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
		}

		public static bool ExifOrient(ref string orientation, string fName)
		{
			try
			{
				orientation = "";
				string ext = System.IO.Path.GetExtension(fName).ToLower();
				if (ext == ".wmf" || ext == ".emf"){         // invalid for MetadataExtractor
					return true;
				}
				IEnumerable<Directory> directories = ImageMetadataReader.ReadMetadata(fName);

				// ------------------------------   exif   ----------------------------------------------------------

				var ifd0Directory = directories.OfType<ExifIfd0Directory>().FirstOrDefault();
				if (ifd0Directory != null){
					string or = ifd0Directory.GetDescription(ExifDirectoryBase.TagOrientation);
					if (or != null) orientation = or.ToLower();
				}

				return true;
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.Message);
				MessageBox.Show("Exif. File is invalid" + "\n " + e.Message, "Invalid orientation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
		}

		public static bool CheckExif(out string orientation, out string model, 
		                             out string fNumber, out string flash, out string expo, out string lensmodel,
		                             out bool gps,  
		                             string fName)
		{
			orientation = "";
			model = "";
			fNumber = "";
			flash = "";
			expo = "";
			lensmodel = "";
			gps = false;
			
			try
			{

				IEnumerable<Directory> directories = ImageMetadataReader.ReadMetadata(fName);

				// ------------------------------   exif   ----------------------------------------------------------

				var ifd0Directory = directories.OfType<ExifIfd0Directory>().FirstOrDefault();
				if (ifd0Directory != null){

					string orientation1 = ifd0Directory.GetDescription(ExifDirectoryBase.TagOrientation);
					if (orientation1 != null) orientation = orientation1.ToLower();

					string model1 = ifd0Directory.GetDescription(ExifDirectoryBase.TagModel);
					if (model1 != null) model = model1;


					string software = ifd0Directory.GetDescription(ExifDirectoryBase.TagSoftware);
				}

				var subIfdDirectory = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
				if (subIfdDirectory != null){
					string dtOriginal = subIfdDirectory.GetDescription(ExifDirectoryBase.TagDateTimeOriginal);

					string exposure = subIfdDirectory.GetDescription(ExifDirectoryBase.TagExposureTime);

					string fNumber1 = subIfdDirectory.GetDescription(ExifDirectoryBase.TagFNumber);
					if (fNumber1 != null) fNumber = fNumber1;
					
					string isoSpeed = subIfdDirectory.GetDescription(ExifDirectoryBase.TagIsoEquivalent);

					string fLength = subIfdDirectory.GetDescription(ExifDirectoryBase.TagFocalLength);

					string flash1 = subIfdDirectory.GetDescription(ExifDirectoryBase.TagFlash);
					if (flash1 != null) flash = flash1;
					
					string expo1 = subIfdDirectory.GetDescription(ExifDirectoryBase.TagExposureProgram);
					if (expo1 != null) expo = expo1;
					
					string lensmodel1 = subIfdDirectory.GetDescription(ExifDirectoryBase.TagLensModel);
					if (lensmodel1 != null) lensmodel = lensmodel1;
					
					string scene = subIfdDirectory.GetDescription(ExifDirectoryBase.TagSceneCaptureType);
				}


				// ------------------------------   makernotes   ----------------------------------------------------------

				var olympusCameraDirectory = directories.OfType<OlympusCameraSettingsMakernoteDirectory>().FirstOrDefault();
				if (olympusCameraDirectory != null){
					string artFilter = olympusCameraDirectory.GetDescription(OlympusCameraSettingsMakernoteDirectory.TagArtFilter);
				}

				var olympusEquipmentDirectory = directories.OfType<OlympusEquipmentMakernoteDirectory>().FirstOrDefault();
				if (olympusEquipmentDirectory != null){
					string lensModel2 = olympusEquipmentDirectory.GetDescription(OlympusEquipmentMakernoteDirectory.TagLensModel);
					if (string.IsNullOrEmpty(lensmodel))  {
						if (lensModel2 != null) lensmodel = lensModel2;
					} 
				}

				var fujiDirectory = directories.OfType<FujifilmMakernoteDirectory>().FirstOrDefault();
				if (fujiDirectory != null){
					string pictureMode = fujiDirectory.GetDescription(FujifilmMakernoteDirectory.TagPictureMode);
				}

				var canonDirectory = directories.OfType<CanonMakernoteDirectory>().FirstOrDefault();
				if (canonDirectory != null){
					string lensType = canonDirectory.GetDescription(CanonMakernoteDirectory.CameraSettings.TagLensType);
				}

				var sonyDirectory = directories.OfType<SonyType1MakernoteDirectory>().FirstOrDefault();
				if (sonyDirectory != null){
					string afMode = sonyDirectory.GetDescription(SonyType1MakernoteDirectory.TagAfMode);
				}

				// ------------------------------   gps    --------------
				var gpsDirectory = directories.OfType<GpsDirectory>().FirstOrDefault();
				if (gpsDirectory != null){
					string latitude = gpsDirectory.GetDescription(GpsDirectory.TagLatitude);
					if (latitude != null) gps = true;

				}


				return true;
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.Message);
				MessageBox.Show("Exif is invalid" + "\n " + e.Message, "Invalid file" + fName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
		}
		

	}
}
