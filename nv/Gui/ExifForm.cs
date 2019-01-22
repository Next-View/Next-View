/*
 * Created by SharpDevelop.
 * User: martin
 * Date: 12.01.2019
 * Time: 21:56
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;  // IEnumerable
using System.Diagnostics;  //   debug
using System.Linq;	 //	OfType
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
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

		string _gps2 = "";

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

		void ExifFormFormClosing(object sender, FormClosingEventArgs e)
		{
			Settings.Default.ExifX = this.Left;
			Settings.Default.ExifY = this.Top;
			Settings.Default.ExifW = this.Width;
			Settings.Default.ExifH = this.Height;
			Settings.Default.Save( );
			e.Cancel = true;
			this.Hide();
		}

		void ExifFormLoad(object sender, EventArgs e)
		{
			this.Width = Settings.Default.ExifW;
			this.Height = Settings.Default.ExifH;
			this.Left = Settings.Default.ExifX;
			this.Top = Settings.Default.ExifY;
			Debug.WriteLine("Exif pos: X: {0}  Y: {1}  W: {2}  H: {3} ", Left, Top, Width, Height);
		}


		public bool CheckFile(ref string orientation, string fName)
		{
			listExif.Items.Clear();
			_gps2 = "";
			orientation = "";

			try
			{
				string ext = System.IO.Path.GetExtension(fName).ToLower();
				if (ext == ".wmf" || ext == ".emf"){         // invalid for MetadataExtractor
					AddFileData(fName);
					return true;
				}
				IEnumerable<Directory> directories = ImageMetadataReader.ReadMetadata(fName);

				// ------------------------------   jpg, bmp, png, ico, gif   ---------------------------------------------

				var jpgDirectory = directories.OfType<JpegDirectory>().FirstOrDefault();
				if (jpgDirectory != null){
					AddListItem("Jpeg:", "+");
					string iWidth = jpgDirectory.GetDescription(JpegDirectory.TagImageWidth);
					AddListItem("Image Width", iWidth);
					string iHeight = jpgDirectory.GetDescription(JpegDirectory.TagImageHeight);
					AddListItem("Image Height", iHeight);
				}

				var jpgComDirectory = directories.OfType<JpegCommentDirectory>().FirstOrDefault();
				if (jpgComDirectory != null){
					string jComm = jpgComDirectory.GetDescription(JpegCommentDirectory.TagComment);
					AddListItem("Comment", jComm);
				}


				var bmpDirectory = directories.OfType<BmpHeaderDirectory>().FirstOrDefault();
				if (bmpDirectory != null){
					AddListItem("BMP:", "+");
					string iWidth = bmpDirectory.GetDescription(BmpHeaderDirectory.TagImageWidth);
					AddListItem("Image Width", iWidth);
					string iHeight = bmpDirectory.GetDescription(BmpHeaderDirectory.TagImageHeight);
					AddListItem("Image Height", iHeight);
					string bBits = bmpDirectory.GetDescription(BmpHeaderDirectory.TagBitsPerPixel);
					AddListItem("Bits per pixel", bBits);
				}

				var pngDirectory = directories.OfType<PngDirectory>().FirstOrDefault();
				if (pngDirectory != null){
					AddListItem("PNG:", "+");
					string iWidth = pngDirectory.GetDescription(PngDirectory.TagImageWidth);
					AddListItem("Image Width", iWidth);
					string iHeight = pngDirectory.GetDescription(PngDirectory.TagImageHeight);
					AddListItem("Image Height", iHeight);
					string pColor = pngDirectory.GetDescription(PngDirectory.TagColorType);
					AddListItem("Color", pColor);
				}

				var icDirectory = directories.OfType<IcoDirectory>().FirstOrDefault();
				if (icDirectory != null){
					AddListItem("Icon:", "+");
					string iWidth = icDirectory.GetDescription(IcoDirectory.TagImageWidth);
					AddListItem("Image Width", iWidth);
					string iHeight = icDirectory.GetDescription(IcoDirectory.TagImageHeight);
					AddListItem("Image Height", iHeight);
					string bBits = icDirectory.GetDescription(IcoDirectory.TagBitsPerPixel);
					AddListItem("Bits per pixel", bBits);
				}

				var gifDirectory = directories.OfType<GifHeaderDirectory>().FirstOrDefault();
				if (gifDirectory != null){
					AddListItem("GIF:", "+");
					string iWidth = gifDirectory.GetDescription(GifHeaderDirectory.TagImageWidth);
					AddListItem("Image Width", iWidth);
					string iHeight = gifDirectory.GetDescription(GifHeaderDirectory.TagImageHeight);
					AddListItem("Image Height", iHeight);
					string bBits = gifDirectory.GetDescription(GifHeaderDirectory.TagBitsPerPixel);
					AddListItem("Bits per pixel", bBits);
				}

				// ------------------------------   exif   ----------------------------------------------------------

				var ifd0Directory = directories.OfType<ExifIfd0Directory>().FirstOrDefault();
				if (ifd0Directory != null){
					AddListItem(" ", "+");
					AddListItem("EXIF:", "+");
					string idescription = ifd0Directory.GetDescription(ExifDirectoryBase.TagImageDescription);
					AddListItem("Description", idescription);

					string dt = ifd0Directory.GetDescription(ExifDirectoryBase.TagDateTime);
					AddListItem("Date Time", dt);

					string iw = ifd0Directory.GetDescription(ExifDirectoryBase.TagImageWidth);
					AddListItem("Image Width", iw);

					string ih = ifd0Directory.GetDescription(ExifDirectoryBase.TagImageHeight);
					AddListItem("Image Height", ih);

					string or = ifd0Directory.GetDescription(ExifDirectoryBase.TagOrientation);
					if (or != null) orientation = or.ToLower();
					AddListItem("Orientation", orientation);

					string make = ifd0Directory.GetDescription(ExifDirectoryBase.TagMake);
					AddListItem("Make", make);

					string model = ifd0Directory.GetDescription(ExifDirectoryBase.TagModel);
					AddListItem("Camera model", model);

					string software = ifd0Directory.GetDescription(ExifDirectoryBase.TagSoftware);
					AddListItem("Software", software);

					string copyright = ifd0Directory.GetDescription(ExifDirectoryBase.TagCopyright);
					AddListItem("Copyright", copyright);

					string artist = ifd0Directory.GetDescription(ExifDirectoryBase.TagArtist);
					AddListItem("Artist", artist);
				}

				var subIfdDirectory = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
				if (subIfdDirectory != null){
					string dtOriginal = subIfdDirectory.GetDescription(ExifDirectoryBase.TagDateTimeOriginal);
					AddListItem("Original date", dtOriginal);

					string dtDigitized = subIfdDirectory.GetDescription(ExifDirectoryBase.TagDateTimeDigitized);
					AddListItem("Digitized date", dtDigitized);

					string iWidth = subIfdDirectory.GetDescription(ExifDirectoryBase.TagExifImageWidth);
					AddListItem("Exif Image Width", iWidth);

					string iHeight = subIfdDirectory.GetDescription(ExifDirectoryBase.TagExifImageHeight);
					AddListItem("Exif Image Height", iHeight);

					string exposure = subIfdDirectory.GetDescription(ExifDirectoryBase.TagExposureTime);
					if (!AddListItem("Exposure Time", exposure)){
						string shutterSpeed = subIfdDirectory.GetDescription(ExifDirectoryBase.TagShutterSpeed);
						AddListItem("Shutter Speed", shutterSpeed);
					}

					string fNumber = subIfdDirectory.GetDescription(ExifDirectoryBase.TagFNumber);
					if (!AddListItem("F Number", fNumber)){
						string aperture = subIfdDirectory.GetDescription(ExifDirectoryBase.TagAperture);
						AddListItem("Aperture", aperture);
					}

					string isoSpeed = subIfdDirectory.GetDescription(ExifDirectoryBase.TagIsoEquivalent);
					AddListItem("ISO Speed", isoSpeed);

					string fLength = subIfdDirectory.GetDescription(ExifDirectoryBase.TagFocalLength);
					AddListItem("Focal Length", fLength);

					string flash = subIfdDirectory.GetDescription(ExifDirectoryBase.TagFlash);
					AddListItem("Flash", flash);

					string expo = subIfdDirectory.GetDescription(ExifDirectoryBase.TagExposureProgram);
					AddListItem("Exposure", expo);

					string lensmodel = subIfdDirectory.GetDescription(ExifDirectoryBase.TagLensModel);
					AddListItem("Lens Model", lensmodel);

					string scene = subIfdDirectory.GetDescription(ExifDirectoryBase.TagSceneCaptureType);
					AddListItem("Scene", scene);
				}


				// ------------------------------   makernotes   ----------------------------------------------------------

				var olympusCameraDirectory = directories.OfType<OlympusCameraSettingsMakernoteDirectory>().FirstOrDefault();
				if (olympusCameraDirectory != null){
					AddListItem("Makernote:", "+");
					string artFilter = olympusCameraDirectory.GetDescription(OlympusCameraSettingsMakernoteDirectory.TagArtFilter);
					AddListItem("Art Filter", artFilter);
				}

				var olympusEquipmentDirectory = directories.OfType<OlympusEquipmentMakernoteDirectory>().FirstOrDefault();
				if (olympusEquipmentDirectory != null){
					string lensModel = olympusEquipmentDirectory.GetDescription(OlympusEquipmentMakernoteDirectory.TagLensModel);
					AddListItem("Lens Model", lensModel);
				}

				var fujiDirectory = directories.OfType<FujifilmMakernoteDirectory>().FirstOrDefault();
				if (fujiDirectory != null){
					AddListItem("Makernote:", "+");
					string pictureMode = fujiDirectory.GetDescription(FujifilmMakernoteDirectory.TagPictureMode);
					AddListItem("Picture Mode", pictureMode);
				}

				var canonDirectory = directories.OfType<CanonMakernoteDirectory>().FirstOrDefault();
				if (canonDirectory != null){
					AddListItem("Makernote:", "+");
					string lensType = canonDirectory.GetDescription(CanonMakernoteDirectory.CameraSettings.TagLensType);
					AddListItem("Lens Type", lensType);
				}

				var sonyDirectory = directories.OfType<SonyType1MakernoteDirectory>().FirstOrDefault();
				if (sonyDirectory != null){
					AddListItem("Makernote:", "+");
					string afMode = sonyDirectory.GetDescription(SonyType1MakernoteDirectory.TagAfMode);
					AddListItem("AF Mode", afMode);
				}

				// ------------------------------   gps    --------------
				var gpsDirectory = directories.OfType<GpsDirectory>().FirstOrDefault();
				if (gpsDirectory != null){
					AddListItem(" ", "+");
					AddListItem("GPS:", "+");
					string laRef = gpsDirectory.GetDescription(GpsDirectory.TagLatitudeRef);
					string latitude = gpsDirectory.GetDescription(GpsDirectory.TagLatitude);
					AddListItem("Latitude", laRef + latitude);
					string loRef = gpsDirectory.GetDescription(GpsDirectory.TagLongitudeRef);
					string longitude = gpsDirectory.GetDescription(GpsDirectory.TagLongitude);
					AddListItem("Longitude", loRef + longitude);
					string gps1 = latitude + laRef + "+" + longitude + loRef;
					_gps2 = gps1.Replace(" ", "").Replace(",", ".");

				}

				AddFileData(fName);

				return true;
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.Message);
				MessageBox.Show("Exif, file is invalid" + "\n " + e.Message, "Invalid file", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
		}

		bool AddFileData(string fName)
		{
				// ------------------------------   file    --------------
				AddListItem(" ", "+");
				AddListItem("File:", "+");
				string aName = System.IO.Path.GetFileName(fName);    // full path, no directory dup
				AddListItem("Name", aName);
				long size = new System.IO.FileInfo(fName).Length;
				string sizeS = Util.SizeFormat(size);
				AddListItem("Size", sizeS);
				string creation = System.IO.File.GetCreationTime(fName).ToString();
				AddListItem("Creation", creation);
				string changed = System.IO.File.GetLastWriteTime(fName).ToString();
				AddListItem("Changed", changed);
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
		void Button1Click(object sender, EventArgs e)
		{
			Clipboard.SetText(_gps2);
		}


	}
}
