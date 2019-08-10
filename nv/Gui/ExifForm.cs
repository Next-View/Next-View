/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
File name:     exifform.cs
Description:   exifform form
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
using System.Collections.Generic;  // IEnumerable
using System.Diagnostics;  //   debug
using System.Drawing;  // rectangle
//  using System.IO;    duplicate names with metadataExtractor 
using System.Linq;	 //	OfType
using System.Windows.Forms;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using MetadataExtractor.Formats.Exif.Makernotes;
using MetadataExtractor.Formats.Jpeg;
using MetadataExtractor.Formats.Bmp;
using MetadataExtractor.Formats.Ico;
using MetadataExtractor.Formats.Png;
using MetadataExtractor.Formats.Gif;
using Next_View.Properties;

namespace Next_View
{
	/// <summary>
	/// Description of ExifForm.
	/// </summary>
	public partial class ExifForm : Form
	{
		string _fPath = "";
		string _gps2 = "";

		public event HandleKeyChange  KeyChanged;


		public ExifForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}

		// ------------------------------   events form ----------------------------------------------------------

		void ExifFormKeyDown(object sender, KeyEventArgs e)
		{
			bool alt = false;
			if (e.Modifiers == Keys.Alt){
				alt = true;
			}
			bool ctrl = false;
			if (e.Modifiers == Keys.Control){
				ctrl = true;
			}

			SetKeyChange(e.KeyValue, alt, ctrl);
		}

		void ExifFormPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			e.IsInputKey = true;
		}

		void ExifFormFormClosing(object sender, FormClosingEventArgs e)
		{
			int le = this.Left;
			int to = this.Top;
			Multi.ExifSave(le, to);
			Settings.Default.ExifW = this.Width;
			Settings.Default.ExifH = this.Height;
			Settings.Default.Save( );
			e.Cancel = true;
			this.Hide();
		}

		void ExifFormLoad(object sender, EventArgs e)
		{
			TranslateExifForm();
			int le;
			int to;
			int wi = Settings.Default.ExifW;
			int he = Settings.Default.ExifH;
			Multi.ExifLoad(out le, out to);
			this.Left = le;
			this.Top = to;
			this.Width = wi;
			this.Height = he;
			//Debug.WriteLine("Exif pos: X: {0}  Y: {1}  W: {2}  H: {3} ", Left, Top, Width, Height);
		}

		void ExifFormShown(object sender, EventArgs e)
		{

		}

		void ExifFormActivated(object sender, EventArgs e)
		{
			int wX = this.Left;
			int wY = this.Top;
			int wW = this.Width;
			int wH = this.Height;
			bool visible;
			Rectangle screenRectangle = RectangleToScreen(this.ClientRectangle);
			int titleHeight = screenRectangle.Top - this.Top;
			Multi.FormShowVisible(out visible, ref wX, ref wY, wW, titleHeight);		
			if (!visible){
				this.Left = wX;
				this.Top = wY;
			}
			else { 
				Multi.FormShowVisible(out visible, ref wX, ref wY, wW, wH);
				if (!visible){
					this.Left = wX;
					this.Top = wY;
				}
			}
		}

		void Button1Click(object sender, EventArgs e)
		{
			Clipboard.SetText(_gps2);
		}

		// ------------------------------   functions  ----------------------------------------------------------

		public bool CheckFile(ref int exifType, ref string orientation, string fPath)
		// called by: image.StartExif, image.ShowExif
		{
			_fPath = fPath;
			listExif.Items.Clear();
			_gps2 = "";
			int exCount = 0;
			exifType = 0;          // default, no exif
			orientation = "";
			int iWidthVal = 0;
			int exifWidthVal = 0;

			try
			{
				string ext = System.IO.Path.GetExtension(fPath).ToLower();
				if (ext == ".wmf" || ext == ".emf"){         // invalid for MetadataExtractor
					AddFileData(fPath);
					return true;
				}
				IEnumerable<Directory> directories = ImageMetadataReader.ReadMetadata(fPath);

				// ------------------------------   jpg, bmp, png, ico, gif   ---------------------------------------------

				var jpgDirectory = directories.OfType<JpegDirectory>().FirstOrDefault();
				if (jpgDirectory != null){
					AddListItem("Jpeg:", "+");
					string iWidth = jpgDirectory.GetDescription(JpegDirectory.TagImageWidth);
					if (iWidth != null){
						string iWidthDigits = new string(iWidth.TakeWhile(c => Char.IsDigit(c)).ToArray());
						Int32.TryParse(iWidthDigits, out iWidthVal);
						AddListItem(T._("Image Width"), iWidth);
					}
					string iHeight = jpgDirectory.GetDescription(JpegDirectory.TagImageHeight);
					AddListItem(T._("Image Height"), iHeight);
				}

				var jpgComDirectory = directories.OfType<JpegCommentDirectory>().FirstOrDefault();
				if (jpgComDirectory != null){
					string jComm = jpgComDirectory.GetDescription(JpegCommentDirectory.TagComment);
					AddListItem(T._("Comment"), jComm);
				}


				var bmpDirectory = directories.OfType<BmpHeaderDirectory>().FirstOrDefault();
				if (bmpDirectory != null){
					AddListItem("BMP:", "+");
					string iWidth = bmpDirectory.GetDescription(BmpHeaderDirectory.TagImageWidth);
					AddListItem(T._("Image Width"), iWidth);
					string iHeight = bmpDirectory.GetDescription(BmpHeaderDirectory.TagImageHeight);
					AddListItem(T._("Image Height"), iHeight);
					string bBits = bmpDirectory.GetDescription(BmpHeaderDirectory.TagBitsPerPixel);
					AddListItem(T._("Bits per pixel"), bBits);
				}

				var pngDirectory = directories.OfType<PngDirectory>().FirstOrDefault();
				if (pngDirectory != null){
					AddListItem("PNG:", "+");
					string iWidth = pngDirectory.GetDescription(PngDirectory.TagImageWidth);
					AddListItem(T._("Image Width"), iWidth);
					string iHeight = pngDirectory.GetDescription(PngDirectory.TagImageHeight);
					AddListItem(T._("Image Height"), iHeight);
					string pColor = pngDirectory.GetDescription(PngDirectory.TagColorType);
					AddListItem(T._("Color"), pColor);
				}

				var icDirectory = directories.OfType<IcoDirectory>().FirstOrDefault();
				if (icDirectory != null){
					AddListItem("Icon:", "+");
					string iWidth = icDirectory.GetDescription(IcoDirectory.TagImageWidth);
					AddListItem(T._("Image Width"), iWidth);
					string iHeight = icDirectory.GetDescription(IcoDirectory.TagImageHeight);
					AddListItem(T._("Image Height"), iHeight);
					string bBits = icDirectory.GetDescription(IcoDirectory.TagBitsPerPixel);
					AddListItem(T._("Bits per pixel"), bBits);
				}

				var gifDirectory = directories.OfType<GifHeaderDirectory>().FirstOrDefault();
				if (gifDirectory != null){
					AddListItem("GIF:", "+");
					string iWidth = gifDirectory.GetDescription(GifHeaderDirectory.TagImageWidth);
					AddListItem(T._("Image Width"), iWidth);
					string iHeight = gifDirectory.GetDescription(GifHeaderDirectory.TagImageHeight);
					AddListItem(T._("Image Height"), iHeight);
					string bBits = gifDirectory.GetDescription(GifHeaderDirectory.TagBitsPerPixel);
					AddListItem(T._("Bits per pixel"), bBits);
				}

				// ------------------------------   exif   ----------------------------------------------------------

				var ifd0Directory = directories.OfType<ExifIfd0Directory>().FirstOrDefault();
				if (ifd0Directory != null){
					exCount = ifd0Directory.TagCount;
					AddListItem(" ", "+");
					AddListItem("EXIF:", "+");
					string idescription = ifd0Directory.GetDescription(ExifDirectoryBase.TagImageDescription);
					AddListItem(T._("Description"), idescription);

					string dt = ifd0Directory.GetDescription(ExifDirectoryBase.TagDateTime);
					AddListItem(T._("Date Time"), dt);

					string iw = ifd0Directory.GetDescription(ExifDirectoryBase.TagImageWidth);
					AddListItem(T._("Image Width"), iw);

					string ih = ifd0Directory.GetDescription(ExifDirectoryBase.TagImageHeight);
					AddListItem(T._("Image Height"), ih);

					string or = ifd0Directory.GetDescription(ExifDirectoryBase.TagOrientation);
					if (or != null) orientation = or.ToLower();
					AddListItem(T._("Orientation"), orientation);

					string make = ifd0Directory.GetDescription(ExifDirectoryBase.TagMake);
					AddListItem(T._("Make"), make);

					string model = ifd0Directory.GetDescription(ExifDirectoryBase.TagModel);
					AddListItem(T._("Camera model"), model);

					string software = ifd0Directory.GetDescription(ExifDirectoryBase.TagSoftware);
					AddListItem(T._("Software"), software);

					string copyright = ifd0Directory.GetDescription(ExifDirectoryBase.TagCopyright);
					AddListItem(T._("Copyright"), copyright);

					string artist = ifd0Directory.GetDescription(ExifDirectoryBase.TagArtist);
					AddListItem(T._("Artist"), artist);
					
					// ------------------------------   windows  ------------------ 
					string winComment = ifd0Directory.GetDescription(ExifDirectoryBase.TagWinComment);
					AddListItem(T._("Windows Comment"), winComment);					

					string winTitle = ifd0Directory.GetDescription(ExifDirectoryBase.TagWinTitle);
					AddListItem(T._("Windows Title"), winTitle);	

					string winAuthor = ifd0Directory.GetDescription(ExifDirectoryBase.TagWinAuthor);
					AddListItem(T._("Windows Author"), winAuthor);	

					string winKeywords = ifd0Directory.GetDescription(ExifDirectoryBase.TagWinKeywords);
					AddListItem(T._("Windows Keywords"), winKeywords);	

					string winSubject = ifd0Directory.GetDescription(ExifDirectoryBase.TagWinSubject);
					AddListItem(T._("Windows Subject"), winSubject);

					string winRating = ifd0Directory.GetDescription(ExifDirectoryBase.TagRating);
					AddListItem(T._("Windows Rating"), winRating);
																														
				}
				// ------------------------------   subIfd   ------------------ 
				string dtOriginal = "";
				var subIfdDirectory = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
				if (subIfdDirectory != null){
					exCount += subIfdDirectory.TagCount;
					dtOriginal = subIfdDirectory.GetDescription(ExifDirectoryBase.TagDateTimeOriginal);
					AddListItem(T._("Original date"), dtOriginal);

					string dtDigitized = subIfdDirectory.GetDescription(ExifDirectoryBase.TagDateTimeDigitized);
					AddListItem(T._("Digitized date"), dtDigitized);

					string iWidth = subIfdDirectory.GetDescription(ExifDirectoryBase.TagExifImageWidth);
					if (iWidth != null){
						string iWidthDigits = new string(iWidth.TakeWhile(c => Char.IsDigit(c)).ToArray());
						Int32.TryParse(iWidthDigits, out exifWidthVal);
						AddListItem(T._("Exif Image Width"), iWidth);
					}

					string iHeight = subIfdDirectory.GetDescription(ExifDirectoryBase.TagExifImageHeight);
					AddListItem(T._("Exif Image Height"), iHeight);

					if (exifWidthVal != iWidthVal && exifWidthVal != 0){
						float changedSizePerc = (float) iWidthVal * 100 / exifWidthVal;
						AddListItem("!  " + T._("Image reduced to"), changedSizePerc.ToString("0") + "%");
					}

					string exposure = subIfdDirectory.GetDescription(ExifDirectoryBase.TagExposureTime);
					if (!AddListItem(T._("Exposure Time"), exposure)){
						string shutterSpeed = subIfdDirectory.GetDescription(ExifDirectoryBase.TagShutterSpeed);
						AddListItem(T._("Shutter Speed"), shutterSpeed);
					}

					string fNumber = subIfdDirectory.GetDescription(ExifDirectoryBase.TagFNumber);
					if (!AddListItem(T._("F Number"), fNumber)){
						string aperture = subIfdDirectory.GetDescription(ExifDirectoryBase.TagAperture);
						AddListItem(T._("Aperture"), aperture);
					}

					string isoSpeed = subIfdDirectory.GetDescription(ExifDirectoryBase.TagIsoEquivalent);
					AddListItem(T._("ISO Speed"), isoSpeed);

					string fLength = subIfdDirectory.GetDescription(ExifDirectoryBase.TagFocalLength);
					AddListItem(T._("Focal Length"), fLength);

					string flash = subIfdDirectory.GetDescription(ExifDirectoryBase.TagFlash);
					AddListItem(T._("Flash"), flash);

					string expo = subIfdDirectory.GetDescription(ExifDirectoryBase.TagExposureProgram);
					AddListItem(T._("Exposure"), expo);

					string lensmodel = subIfdDirectory.GetDescription(ExifDirectoryBase.TagLensModel);
					AddListItem(T._("Lens Model"), lensmodel);

					string scene = subIfdDirectory.GetDescription(ExifDirectoryBase.TagSceneCaptureType);
					AddListItem(T._("Scene"), scene);
				}

				if (exCount > 5) exifType = 1;      // partial
				if (exCount > 15){
					exifType = 2;     // full exif
					if (string.IsNullOrEmpty(dtOriginal)){
						exifType = 1; 
					}
				}
				// ------------------------------   makernotes   ----------------------------------------------------------

				var canonDirectory = directories.OfType<CanonMakernoteDirectory>().FirstOrDefault();
				if (canonDirectory != null){
					AddListItem("Makernote:", "+");
					string lensType = canonDirectory.GetDescription(CanonMakernoteDirectory.CameraSettings.TagLensType);
					AddListItem(T._("Lens Type"), lensType);
				}

				var fujiDirectory = directories.OfType<FujifilmMakernoteDirectory>().FirstOrDefault();
				if (fujiDirectory != null){
					AddListItem("Makernote:", "+");
					string pictureMode = fujiDirectory.GetDescription(FujifilmMakernoteDirectory.TagPictureMode);
					AddListItem(T._("Picture Mode"), pictureMode);
				}
								
				var olympusCameraDirectory = directories.OfType<OlympusCameraSettingsMakernoteDirectory>().FirstOrDefault();
				if (olympusCameraDirectory != null){
					AddListItem("Makernote:", "+");
					string artFilter = olympusCameraDirectory.GetDescription(OlympusCameraSettingsMakernoteDirectory.TagArtFilter);
					AddListItem(T._("Art Filter"), artFilter);
					string focusMode = olympusCameraDirectory.GetDescription(OlympusCameraSettingsMakernoteDirectory.TagFocusMode);
					if (focusMode.Contains("Face detect")){
						AddListItem(T._("Faces detected"), T._("yes"));
					}
					//Debug.WriteLine("Olympus focus: "+ focusMode);
				}

				var olympusImageProcessingDirectory = directories.OfType<OlympusImageProcessingMakernoteDirectory>().FirstOrDefault();					
				if (olympusImageProcessingDirectory != null){
					string facesDetected = olympusImageProcessingDirectory.GetDescription(OlympusImageProcessingMakernoteDirectory.TagFacesDetected);
					//Debug.WriteLine("Olympus faces: "+ facesDetected);					
				}
						

				var olympusEquipmentDirectory = directories.OfType<OlympusEquipmentMakernoteDirectory>().FirstOrDefault();
				if (olympusEquipmentDirectory != null){
					AddListItem("Makernote:", "+");
					string lensModel = olympusEquipmentDirectory.GetDescription(OlympusEquipmentMakernoteDirectory.TagLensModel);
					AddListItem(T._("Lens Model"), lensModel);
				}

				var panasonicDirectory = directories.OfType<PanasonicMakernoteDirectory>().FirstOrDefault();
				if (panasonicDirectory != null){
					AddListItem("Makernote:", "+");
					string faceNumber = panasonicDirectory.GetDescription(PanasonicMakernoteDirectory.TagFacesDetected);
					AddListItem(T._("Faces detected"), faceNumber);
				}

				var samsungDirectory = directories.OfType<SamsungType2MakernoteDirectory>().FirstOrDefault();
				if (samsungDirectory != null){
					AddListItem("Makernote:", "+");
					string faceDetect = samsungDirectory.GetDescription(SamsungType2MakernoteDirectory.TagFaceDetect);
					if (faceDetect == "On")
						AddListItem(T._("Faces detected"), T._("yes"));
				}
								
				var sonyDirectory = directories.OfType<SonyType1MakernoteDirectory>().FirstOrDefault();
				if (sonyDirectory != null){
					AddListItem("Makernote:", "+");
					string afMode = sonyDirectory.GetDescription(SonyType1MakernoteDirectory.TagAfMode);
					AddListItem(T._("AF Mode"), afMode);
				}

				// ------------------------------   gps    --------------
				var gpsDirectory = directories.OfType<GpsDirectory>().FirstOrDefault();
				if (gpsDirectory != null){
					AddListItem(" ", "+");
					AddListItem("GPS:", "+");
					string laRef = gpsDirectory.GetDescription(GpsDirectory.TagLatitudeRef);
					string latitude = gpsDirectory.GetDescription(GpsDirectory.TagLatitude);
					if (latitude != null) exifType = 3;
					AddListItem(T._("Latitude"), laRef + latitude);
					string loRef = gpsDirectory.GetDescription(GpsDirectory.TagLongitudeRef);
					string longitude = gpsDirectory.GetDescription(GpsDirectory.TagLongitude);
					AddListItem(T._("Longitude"), loRef + longitude);
					string gps1 = latitude + laRef + "+" + longitude + loRef;
					_gps2 = gps1.Replace(" ", "").Replace(",", ".");

				}

				AddFileData(fPath);

				return true;
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.Message);
				MessageBox.Show(T._("Exif is invalid") + "\n " + e.Message, T._("Invalid file") + fPath, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
		}

		bool AddFileData(string fName)
		{
				// ------------------------------   file    --------------
				AddListItem(" ", "+");
				AddListItem(T._("File:"), "+");
				string aName = System.IO.Path.GetFileName(fName);    // full path, no directory dup
				AddListItem(T._("Name"), aName);
				long size = new System.IO.FileInfo(fName).Length;
				string sizeS = Util.SizeFormat(size);
				AddListItem(T._("Size"), sizeS);
				string creation = System.IO.File.GetCreationTime(fName).ToString();
				AddListItem(T._("Creation"), creation);
				string changed = System.IO.File.GetLastWriteTime(fName).ToString();
				AddListItem(T._("Changed"), changed);
				return true;
		}

		bool AddListItem(string tag, string tagValue)
		{
			if (tagValue == null) return false;

			tagValue = tagValue.Trim();
			if (tagValue == "") return false;

			if (tagValue == "+") {
				ListViewItem item = listExif.Items.Add(tag);
				item.ImageIndex = 0;
				item.SubItems.Add("");
			}
			else {
				ListViewItem item = listExif.Items.Add(tag);
				item.ImageIndex = 0;
				item.SubItems.Add(tagValue);
			}
			return true;
		}

		public void TranslateExifForm( )
		{
			Text = T._("Exif data");
		}
		
		// ------------------------------   delegates   ----------------------------------------------------------

		public void SetKeyChange(int kVal, bool alt, bool ctrl)
		{
			// called by: PicLoad, 'no img loaded'
			// output: imageForm.HandleKey
			OnKeyChanged(new SetKeyEventArgs(kVal, alt, ctrl));
			Application.DoEvents();
		}

		protected virtual void OnKeyChanged(SetKeyEventArgs e)
		{
			if(this.KeyChanged != null)     // nothing subscribed to this event
			{
				this.KeyChanged(this, e);
			}
		}

		void PopEditClick(object sender, EventArgs e)
		{
			FileInfo.ShowFileProperties(_fPath);
		}


	}
}
