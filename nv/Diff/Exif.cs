/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
File name:     exif.cs
Description:   different exif functions
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
using System.Diagnostics;
using System.Linq;	 //	OfType
using System.Globalization;   // CultureInfo
using System.Text.RegularExpressions;  // Regex
using System.Windows.Forms;  // MessageBox
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using MetadataExtractor.Formats.Exif.Makernotes;

namespace Next_View
{
	/// <summary>
	/// Description of Util.
	/// </summary>
	public static class ExifRead
	{
		public static bool ExifODate(out DateTime dtOriginal, string fName)
		// called by: image.PicLoad
		{
			dtOriginal = default(DateTime);
			try
			{
				string ext = System.IO.Path.GetExtension(fName).ToLower();
				if (ext != ".jpg"){         
					return false;
				}
				IEnumerable<Directory> directories = ImageMetadataReader.ReadMetadata(fName);

				// ------------------------------   exif   ----------------------------------------------------------
				var subIfdDirectory = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
				if (subIfdDirectory != null){
					string dtOriginalS = subIfdDirectory.GetDescription(ExifDirectoryBase.TagDateTimeOriginal);
					DateTime.TryParseExact(dtOriginalS, "yyyy:MM:dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtOriginal);
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
				
		public static bool ExifOrient(ref int exifType, ref string orientation, string fName)
		// called by: image.PicLoad
		{
			try
			{
				int exCount = 0;
				exifType = 0;     
				orientation = "";
				string ext = System.IO.Path.GetExtension(fName).ToLower();
				if (ext == ".wmf" || ext == ".emf"){         // invalid for MetadataExtractor
					return false;
				}
				IEnumerable<Directory> directories = ImageMetadataReader.ReadMetadata(fName);

				// ------------------------------   exif   ----------------------------------------------------------

				var ifd0Directory = directories.OfType<ExifIfd0Directory>().FirstOrDefault();
				if (ifd0Directory != null){
					exCount = ifd0Directory.TagCount;
					string or = ifd0Directory.GetDescription(ExifDirectoryBase.TagOrientation);
					if (or != null) orientation = or.ToLower();
				}
				
				var subIfdDirectory = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
				if (subIfdDirectory != null){
					exCount += subIfdDirectory.TagCount;
				}
				
				if (exCount > 5) exifType = 1;
				if (exCount > 15) exifType = 2;

				// ------------------------------   gps    --------------
				var gpsDirectory = directories.OfType<GpsDirectory>().FirstOrDefault();
				if (gpsDirectory != null){
					string latitude = gpsDirectory.GetDescription(GpsDirectory.TagLatitude);
					if (latitude != null) exifType = 3;
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

		public static bool CheckExif(out int exType, out string orientation, out string model, out DateTime dtOriginal, out int timeOfD,
		                             out string expotime, out string fNumber, out string fLength, out bool flash, out string exposi, out string lensmodel, out string scene,
		                             out bool gps, out bool face, string fName)
		// called by: exifDash.ScanImages
		{
			exType = 0;
			orientation = "";
			model = "";
			timeOfD = -1;
			dtOriginal = default(DateTime);
			expotime = "";
			fNumber = "";
			fLength = "";
			flash = false;
			exposi = "";
			lensmodel = "";
			scene = "";
			gps = false;
			face = false;

			int exCount = 0;

			try
			{

				IEnumerable<Directory> directories = ImageMetadataReader.ReadMetadata(fName);

				// ------------------------------   exif   ----------------------------------------------------------

				var ifd0Directory = directories.OfType<ExifIfd0Directory>().FirstOrDefault();
				if (ifd0Directory != null){
					exCount = ifd0Directory.TagCount;
					string orientation1 = ifd0Directory.GetDescription(ExifDirectoryBase.TagOrientation);
					if (orientation1 != null) orientation = orientation1.ToLower();
					
					string make = "";
					string make1 = ifd0Directory.GetDescription(ExifDirectoryBase.TagMake);
					if (make1 != null){ 
						Match match1 = Regex.Match(make1, @"^([\w\-]+)");
						if (match1.Success){
							make =  match1.Groups[0].Value;
							
						}
					}
					
					string model1 = ifd0Directory.GetDescription(ExifDirectoryBase.TagModel);
					if (model1 != null) model = model1;
					if (!model.Contains(make)){
						model = make + " " + model;
					}
					if (model.Length > 40){
						int pos = model.IndexOf(' ', 30);
						model = model.Substring(0, pos - 1);
					}

					string software = ifd0Directory.GetDescription(ExifDirectoryBase.TagSoftware);
				}


				var subIfdDirectory = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
				if (subIfdDirectory != null){
					exCount += subIfdDirectory.TagCount;
					string dtOriginalS = subIfdDirectory.GetDescription(ExifDirectoryBase.TagDateTimeOriginal);

					if (DateTime.TryParseExact(dtOriginalS, "yyyy:MM:dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtOriginal)){
						DateTime myTime = default(DateTime).Add(dtOriginal.TimeOfDay);
						TimeSpan orTime = dtOriginal.TimeOfDay;
						int orHours = (int)orTime.Hours;
						if (orHours < 6) timeOfD = 0;
						else if (orHours >= 6 && orHours < 12) timeOfD = 1;
						else if (orHours >= 12 && orHours < 15) timeOfD = 2;
						else if (orHours >= 15 && orHours < 18) timeOfD = 3;
						else if (orHours >= 18 && orHours < 22) timeOfD = 4;
						else if (orHours >= 20) timeOfD = 0;
						//Debug.WriteLine("date " + dtOriginal + " " + orHours.ToString() + " " + timeOfD.ToString());
					}

					string expotime1 = subIfdDirectory.GetDescription(ExifDirectoryBase.TagExposureTime);
					if (expotime1 != null) expotime = expotime1;

					string fNumber1 = subIfdDirectory.GetDescription(ExifDirectoryBase.TagFNumber);
					if (fNumber1 != null) fNumber = fNumber1;

					string isoSpeed = subIfdDirectory.GetDescription(ExifDirectoryBase.TagIsoEquivalent);

					string fLength1 = subIfdDirectory.GetDescription(ExifDirectoryBase.TagFocalLength);
					if (fLength1 != null) fLength = fLength1;

					string flash1 = subIfdDirectory.GetDescription(ExifDirectoryBase.TagFlash);
					if (flash1 != null){
						if(flash1.IndexOf("not") == -1) flash = true;
					}

					string exposi1 = subIfdDirectory.GetDescription(ExifDirectoryBase.TagExposureProgram);
					if (exposi1 != null) exposi = exposi1;

					string lensmodel1 = subIfdDirectory.GetDescription(ExifDirectoryBase.TagLensModel);
					if (lensmodel1 != null) lensmodel = lensmodel1;

					string scene1 = subIfdDirectory.GetDescription(ExifDirectoryBase.TagSceneCaptureType);
					if (scene1 != null) scene = scene1;
				}

				if (exCount > 5) exType = 1;
				if (exCount > 15) exType = 2;
				// ------------------------------   makernotes   ----------------------------------------------------------

				var canonDirectory = directories.OfType<CanonMakernoteDirectory>().FirstOrDefault();
				if (canonDirectory != null){
					string lensType = canonDirectory.GetDescription(CanonMakernoteDirectory.CameraSettings.TagLensType);
				}

				var fujiDirectory = directories.OfType<FujifilmMakernoteDirectory>().FirstOrDefault();
				if (fujiDirectory != null){
					string pictureMode = fujiDirectory.GetDescription(FujifilmMakernoteDirectory.TagPictureMode);
				}

				string focusMode = "";
				var olympusCameraDirectory = directories.OfType<OlympusCameraSettingsMakernoteDirectory>().FirstOrDefault();
				if (olympusCameraDirectory != null){
					string focusMode1 = olympusCameraDirectory.GetDescription(OlympusCameraSettingsMakernoteDirectory.TagFocusMode);
					if (focusMode1 != null) focusMode = focusMode1;				
					if (focusMode.Contains("Face detect")){
						face = true;
					}
				}

				var olympusEquipmentDirectory = directories.OfType<OlympusEquipmentMakernoteDirectory>().FirstOrDefault();
				if (olympusEquipmentDirectory != null){
					string lensModel2 = olympusEquipmentDirectory.GetDescription(OlympusEquipmentMakernoteDirectory.TagLensModel);
					if (string.IsNullOrEmpty(lensmodel))  {    // from subIfd
						if (lensModel2 != null) lensmodel = lensModel2;
					}
				}

				var panasonicDirectory = directories.OfType<PanasonicMakernoteDirectory>().FirstOrDefault();
				if (panasonicDirectory != null){
					string faceNumber = panasonicDirectory.GetDescription(PanasonicMakernoteDirectory.TagFacesDetected);
					if (faceNumber != "0"){
						face = true;
					}
				}
				
				var samsungDirectory = directories.OfType<SamsungType2MakernoteDirectory>().FirstOrDefault();
				if (samsungDirectory != null){
					string faceDetect = samsungDirectory.GetDescription(SamsungType2MakernoteDirectory.TagFaceDetect);
					if (faceDetect == "On"){
						face = true;
					}
				}

				var sonyDirectory = directories.OfType<SonyType1MakernoteDirectory>().FirstOrDefault();
				if (sonyDirectory != null){
					string afMode = sonyDirectory.GetDescription(SonyType1MakernoteDirectory.TagAfMode);
					if (afMode != null){
						if (afMode == "Face Detected"){
							face = true;
						}
					}
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
				MessageBox.Show(T._("Exif is invalid") + "\n " + e.Message, T._("Invalid file") + fName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
		}


	}
}
