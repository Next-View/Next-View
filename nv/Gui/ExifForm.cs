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
using Next_View.Properties;

namespace Next_View
{
	/// <summary>
	/// Description of ExifForm.
	/// </summary>
	public partial class ExifForm : Form
	{
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
		}
		
	
		public bool CheckFile(string fName)
		{
			listExif.Items.Clear();
			this.Text = System.IO.Path.GetFileName(fName);
			try
			{
				IEnumerable<Directory> directories = ImageMetadataReader.ReadMetadata(fName);


				var ifd0Directory = directories.OfType<ExifIfd0Directory>().FirstOrDefault();
				if (ifd0Directory != null){				
					string idescription = ifd0Directory.GetDescription(ExifDirectoryBase.TagImageDescription); 
					AddListItem("Description", idescription);

					string dt = ifd0Directory.GetDescription(ExifDirectoryBase.TagDateTime); 
					AddListItem("Date Time", dt);

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
					AddListItem("Image Width", iWidth);

					string iHeight = subIfdDirectory.GetDescription(ExifDirectoryBase.TagExifImageHeight);
					AddListItem("Image Height", iHeight);
					
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
				}


				// ------------------------------   makernotes   ----------------------------------------------------------
				
				var olympusCameraDirectory = directories.OfType<OlympusCameraSettingsMakernoteDirectory>().FirstOrDefault();
				if (olympusCameraDirectory != null){
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
					string pictureMode = fujiDirectory.GetDescription(FujifilmMakernoteDirectory.TagPictureMode);
					AddListItem("Picture Mode", pictureMode);
				}
																
				return true;
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.Message);
				MessageBox.Show("File is invalid" + "\n " + e.Message, "Invalid file", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
		}

		bool AddListItem(string tag, string tagValue)
		{
			if (tagValue == null) return false;
			
			tagValue = tagValue.Trim();
			if (tagValue == "") return false;
			
			ListViewItem item = listExif.Items.Add(tag);
			item.ImageIndex = 0;
			item.SubItems.Add(tagValue);
			return true;
		}
		
				
	}
}
