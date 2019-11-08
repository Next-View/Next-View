/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
File name:     exifdash.cs
Description:   exifdash form
Copyright:     Copyright (c) Martin A. Schnell, 2018
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
using System.Collections.Generic;  // list
using System.ComponentModel;  // BackgroundWorker
using System.Diagnostics;  // Debug
using System.IO;	 //	Directory
using System.Linq;	 //	OfType
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;  // chart
using Next_View.Properties;
using WeifenLuo.WinFormsUI.Docking;


namespace Next_View
{
	/// <summary>
	/// Description of StatsForm.
	/// </summary>
	public partial class ExifDash : DockContent
	{
		readonly string[] _validExtensions  = new [] {".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tif", ".ico", ".wmf", ".emf"};
		bool _stop = false;
		bool _leave = true;
		string _imgDir = "";

		Stopwatch _sw1 = new Stopwatch();

		List<Exif> exList = new List<Exif>();
		List<ImgFile> _exifImgList = new List<ImgFile>();
		List<string> errorList = new List<string>();
		
		Dictionary<int, int> dicExift = new Dictionary<int, int>();
		Dictionary<int, int> dicToD = new Dictionary<int, int>();
		Dictionary<string, int> dicModel = new Dictionary<string, int>();
		Dictionary<string, int> dicExpot = new Dictionary<string, int>();
		Dictionary<string, int> dicFNum = new Dictionary<string, int>();
		Dictionary<string, int> dicFLen = new Dictionary<string, int>();
		int _flashCount = 0;
		Dictionary<string, int> dicExposi = new Dictionary<string, int>();
		Dictionary<string, int> dicLens = new Dictionary<string, int>();
		Dictionary<string, int> dicScene = new Dictionary<string, int>();
		int _gpsCount = 0;
		int _faceCount = 0;

		int _dateCount = 0;
		int _rangeType = 0;
		DateTime _minDate = DateTime.MaxValue;
		DateTime _maxDate = DateTime.MinValue;

		public event HandleWindowSize  WindowSize;

		public event HandleCommandChange  CommandChanged;

		public event HandleStatusMainChange  StatusChanged;

		public ExifDash()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}

    // ------------------------------   functions  ----------------------------------------------------------

		public void SetPath2(string fPath)
		// from imageForm
		{
			edImgPath.Text = Path.GetDirectoryName(fPath);
			this.Text = edImgPath.Text;
		}

		public void NoLeave()
		// from main
		{
			_leave = false;
		}
		
		public void FormClear()
		{
			lblInfo.Text = T._("Info:");
			exList.Clear();
			_exifImgList.Clear();
			errorList.Clear();
			listImg.Items.Clear();
			listExift.Items.Clear();
			listToD.Items.Clear();
			listModel.Items.Clear();
			listLens.Items.Clear();
			listExpo.Items.Clear();
			listScene.Items.Clear();
			listFLen.Items.Clear();
			dicExift.Clear();
			dicToD.Clear();
			dicModel.Clear();
			dicExpot.Clear();
			dicFNum.Clear();
			dicFLen.Clear();
			dicExposi.Clear();
			dicLens.Clear();
			dicScene.Clear();
			_gpsCount = 0;
			_faceCount = 0;
			_flashCount = 0;
			lblGps.Text = "GPS: ";
			lblFlash.Text = T._("Flash") + ": ";

			_dateCount = 0;
			_minDate = DateTime.MaxValue;
			_maxDate = DateTime.MinValue;
			chartImg.Series.Clear( );

		}

		bool CheckStart()
		{
			string imgDir = edImgPath.Text;
			if (string.IsNullOrEmpty(imgDir)) {
				MessageBox.Show (T._("No directory given"), T._("Error"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
			if (!Directory.Exists(imgDir)) {
				MessageBox.Show (T._("Directory does not exist"), T._("Error"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}

			_imgDir = imgDir;
			FormClear();
			StartScan();
			return true;
		}

		public void StartScan()
		{
			if (_stop == false){
				_stop = true;
				cmdStart.Text = T._("&Stop");
				//Debug.WriteLine("bw: start: ");
				if (backgroundWorker1.IsBusy != true)
				{
					//Debug.WriteLine("bw: run: ");
					_sw1.Start();
					backgroundWorker1.RunWorkerAsync();
				}
			}
			else {
				backgroundWorker1.CancelAsync();

				//Debug.WriteLine("bw: cancel1: ");
			}
		}

		public void DashImgList(out List<ImgFile> exImgList)
		// return img list
		{
			exImgList = _exifImgList;
		}

		void ExifDataShow()
		// called by: bw end
		{
			// timespan for info
			string error1 = "";
			if (errorList.Count > 0) {
				error1 = "               " + errorList[0] + " :" + errorList.Count.ToString(); 
			}
			
			if (_dateCount > 0){
				TimeSpan imgSpan = _maxDate.Subtract(_minDate);
				if (imgSpan.TotalDays > 730){
					_rangeType = 1;      // years
					double years = Math.Ceiling(imgSpan.TotalDays / 365.0);
					string spanS = years.ToString("0.0");
					lblInfo.Text = String.Format(T._("Info: Number of images {0}. Between {1:yyyy-MM-dd} and {2:yyyy-MM-dd}, a span of {3} years"), exList.Count, _minDate, _maxDate, spanS ) + error1;
				}
				else if (imgSpan.TotalDays > 60){
					_rangeType = 2;      // months
					double months = Math.Ceiling(imgSpan.TotalDays / 30.0);
					string spanS = months.ToString("0");
					lblInfo.Text = String.Format(T._("Info: Number of images {0}. Between {1:yyyy-MM-dd} and {2:yyyy-MM-dd}, a span of {3} months"), exList.Count, _minDate, _maxDate, spanS ) + error1;
				}
				else if (imgSpan.TotalDays > 1){
					_rangeType = 3;      // days
					double days = (int) Math.Ceiling(imgSpan.TotalHours / 24.0);
					string spanS = days.ToString("0");
					lblInfo.Text = String.Format(T._("Info: Number of images {0}. Between {1:yyyy-MM-dd} and {2:yyyy-MM-dd}, a span of {3} days"), exList.Count, _minDate, _maxDate, spanS ) + error1;
				}
				else {
					_rangeType = 4;      // hours
					int hours = (int) Math.Ceiling(imgSpan.TotalMinutes / 60.0);
					string spanS = hours.ToString("0");
					lblInfo.Text = String.Format(T._("Info: Number of images {0}. On {1:yyyy-MM-dd}. Between {2:HH:mm} and {3:HH:mm}, a span of {4} hours"), exList.Count, _minDate, _minDate, _maxDate, spanS ) + error1;
				}
				CreateTimelineChart( );
			}
			else {
				lblInfo.Text = String.Format(T._("Info: Number of images {0} "), exList.Count) + error1;
			}

			// show listviews
			foreach (KeyValuePair<int, int> det in dicExift.OrderByDescending(key=> key.Key))
			{
				string exift;
				switch(det.Key)
				{
					case 1: exift = T._("Reduced Exif");
					break;
					case 2: exift = T._("Full Exif");
					break;
					default: exift = T._("No Exif");  // 0
					break;
				}
				ListViewItem item = this.listExift.Items.Add(exift);
				item.ImageIndex = 0;
				item.SubItems.Add(det.Value.ToString());
			}
			listExift.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
			listExift.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

			foreach (KeyValuePair<int, int> dtd in dicToD.OrderBy(key=> key.Key))
			{
				string tod;
				switch(dtd.Key)
				{
					case 0: tod = T._("Night");
					break;
					case 1: tod = T._("Morning");
					break;
					case 2: tod = T._("Noon");
					break;
					case 3: tod = T._("Afternoon");
					break;
					case 4: tod = T._("Evening");
					break;
					default: tod = T._("No time");
					break;
				}
				ListViewItem item = this.listToD.Items.Add(tod);
				item.ImageIndex = 0;
				item.SubItems.Add(dtd.Value.ToString());
			}
			listToD.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
			listToD.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

			foreach (KeyValuePair<string, int> dm in dicModel.OrderBy(key=> key.Key))
			{
				string mo = dm.Key;
				if (string.IsNullOrEmpty(mo)) mo = T._("<no data>");
				ListViewItem item = this.listModel.Items.Add(mo);
				item.ImageIndex = 0;
				item.SubItems.Add(dm.Value.ToString());
			}
			listModel.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
			listModel.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

			foreach (KeyValuePair<string, int> dl in dicLens.OrderBy(key=> key.Key))
			{
				string le = dl.Key;
				if (string.IsNullOrEmpty(le)) le = T._("<no data>");
				ListViewItem item = this.listLens.Items.Add(le);
				item.ImageIndex = 0;
				item.SubItems.Add(dl.Value.ToString());
			}
			listLens.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
			listLens.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

			foreach (KeyValuePair<string, int> dfl in dicFLen.OrderBy(key=> key.Key, new NsComparer()))
			{
				string fl = dfl.Key;
				if (string.IsNullOrEmpty(fl)) fl = T._("<no data>");
				ListViewItem item = this.listFLen.Items.Add(fl);
				item.ImageIndex = 0;
				item.SubItems.Add(dfl.Value.ToString());
			}
			listFLen.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
			listFLen.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

			foreach (KeyValuePair<string, int> dex in dicExposi.OrderBy(key=> key.Key))
			{
				string exp = dex.Key;
				if (string.IsNullOrEmpty(exp)) exp = T._("<no data>");
				ListViewItem item = this.listExpo.Items.Add(exp);
				item.ImageIndex = 0;
				item.SubItems.Add(dex.Value.ToString());
			}
			listExpo.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
			listExpo.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

			foreach (KeyValuePair<string, int> ds in dicScene.OrderBy(key=> key.Key))
			{
				string sc = ds.Key;
				if (string.IsNullOrEmpty(sc)) sc = T._("<no data>");
				ListViewItem item = this.listScene.Items.Add(sc);
				item.ImageIndex = 0;
				item.SubItems.Add(ds.Value.ToString());
			}
			listScene.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
			listScene.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

			lblGps.Text = "GPS: " + _gpsCount.ToString();
			lblFace.Text = T._("Face") + ": " + _faceCount.ToString();
			lblFlash.Text = T._("Flash") + ": " + _flashCount.ToString();
		}

		void CreateTimelineChart( )
		{
			var dicPeriod = new Dictionary<string, int>();
			//
			//Debug.WriteLine("year start: " + nextYear.ToString());
			string periodFormat = "";
			string chartTitle = "";
			if (_rangeType == 1){  // years
				DateTime nextYear = new DateTime (_minDate.Year, 1, 1);  // time 0:0:0
				periodFormat = "{0:yyyy}";
				chartTitle = T._("Images per year");
				do {    // pre fill
					string year = String.Format(periodFormat , nextYear);
					dicPeriod.Add(year, 0);
					nextYear = nextYear.AddYears(1);
				} while (nextYear <= _maxDate);
			}
			else if (_rangeType == 2){
				var nextMonth = new DateTime (_minDate.Year, _minDate.Month, 1);  // time 0:0:0
				periodFormat = "{0:yyyy-MM}";
				chartTitle = T._("Images per month");
				do {
					string month = String.Format(periodFormat , nextMonth);
					dicPeriod.Add(month, 0);
					nextMonth = nextMonth.AddMonths(1);
					//Debug.WriteLine("period0: " + month);
				} while (nextMonth <= _maxDate);
			}
			else if (_rangeType == 3){   // days
				var nextDay = new DateTime (_minDate.Year, _minDate.Month, _minDate.Day);
				periodFormat = "{0:yyyy-MM-dd}";
				chartTitle = T._("Images per day");
				do {
					string day = String.Format(periodFormat , nextDay);
					dicPeriod.Add(day, 0);
					nextDay = nextDay.AddDays(1);
				} while (nextDay <= _maxDate);
			}
			else {   // hours
				var nextHour = new DateTime(_minDate.Year, _minDate.Month, _minDate.Day, _minDate.Hour, 0, 0);
				periodFormat = "{0:yyyy-MM-dd HH}";
				chartTitle = T._("Images per hour");
				do {
					string hour = String.Format(periodFormat , nextHour);
					dicPeriod.Add(hour, 0);
					nextHour = nextHour.AddHours(1);
				} while (nextHour <= _maxDate);
			}

			DateTime nullDate = DateTime.MinValue;
			foreach (Exif exi in exList) {
				if (exi.eDtOriginal != nullDate){
					string period = String.Format(periodFormat , exi.eDtOriginal);
					//Debug.WriteLine("period: " + period);
					dicPeriod[period] += 1;
				}
			}

			chartImg.Palette = ChartColorPalette.Pastel;
			chartImg.Titles[0].Text = chartTitle;
			chartImg.Series.Clear( );
			chartImg.ChartAreas[0].AxisX.Minimum = 0;
			var sImgCount = new Series();
			sImgCount.ChartType = SeriesChartType.Column;
			int periodCol = 0;
			foreach (KeyValuePair<string, int> dp in dicPeriod)
			{
				periodCol++;
				sImgCount.Points.AddXY(periodCol, dp.Value);
			}
			chartImg.Series.Add(sImgCount);
		}

		void ChartImgCustomize(object sender, EventArgs e)
		{
			int lblCount = 0;
			string lblCap;
			foreach (CustomLabel lbl in chartImg.ChartAreas[0].AxisX.CustomLabels)
			{
				Int32.TryParse(lbl.Text, out lblCount);
				ChartXLabel(out lblCap, lblCount);
				lbl.Text = lblCap;
			}

			foreach (CustomLabel lbl in chartImg.ChartAreas[0].AxisY.CustomLabels)
			{
				if (!Int32.TryParse(lbl.Text, out lblCount)){
					lbl.Text = " ";
				}
			}
		}

		void ChartXLabel(out string labelCaption, int labelCount)
		{
			if (_rangeType == 1){
				var startYear = new DateTime (_minDate.Year - 1, 01, 01);
				DateTime lDate = startYear.AddYears(labelCount);
				labelCaption = string.Format("{0:yyyy}", lDate);
			}
			else if (_rangeType == 2){
				DateTime startMonth;
				if (_minDate.Month == 1){
					startMonth = new DateTime (_minDate.Year - 1, 12, 01);
				}
				else {
					startMonth = new DateTime (_minDate.Year, _minDate.Month - 1, 01);
				}
				DateTime lDate = startMonth.AddMonths(labelCount);
				labelCaption = string.Format("{0:yyyy-MM}", lDate);
			}
			else if (_rangeType == 3){
				var startDay = new DateTime (_minDate.Year, _minDate.Month,  _minDate.Day);
				startDay = startDay.AddDays(-1);
				DateTime lDate = startDay.AddDays(labelCount);
				labelCaption = string.Format("{0:MM-dd}", lDate);
			}
			else {
				var startHour = new DateTime (_minDate.Year, _minDate.Month,  _minDate.Day, _minDate.Hour, 0, 0);
				startHour = startHour.AddHours(-1);
				DateTime lDate = startHour.AddHours(labelCount);
				labelCaption = string.Format("{0:HH:mm}", lDate);
			}
		}

		void SearchExif(string searchStr)
		{
			int ext = 0;     // No Exif
			if (string.Compare(T._("Reduced Exif"), searchStr, StringComparison.InvariantCulture) == 0) ext = 1;
			else if (string.Compare(T._("Full Exif"), searchStr, StringComparison.InvariantCulture) == 0) ext = 2;

			foreach (Exif exi in exList) {
				if (exi.eType ==  ext){
					ListViewItem item = this.listImg.Items.Add(exi.eFname);
					_exifImgList.Add(new ImgFile(exi.eFname, exi.dtFile, exi.eDtOriginal));   
				}
			}
		}

		void SearchExifModel(string searchStr)
		{
			foreach (Exif exi in exList) {
				if (string.Compare(exi.eModel, searchStr, StringComparison.InvariantCulture) == 0){
					ListViewItem item = this.listImg.Items.Add(exi.eFname);
					_exifImgList.Add(new ImgFile(exi.eFname, exi.dtFile, exi.eDtOriginal));   
				}
			}
		}

		void SearchExifLens(string searchStr)
		{
			foreach (Exif exi in exList) {
				if (string.Compare(exi.eLensmodel, searchStr, StringComparison.InvariantCulture) == 0){
					ListViewItem item = this.listImg.Items.Add(exi.eFname);
					_exifImgList.Add(new ImgFile(exi.eFname, exi.dtFile, exi.eDtOriginal));   
				}
			}
		}

		void SearchExifExpo(string searchStr)
		{
			foreach (Exif exi in exList) {
				if (string.Compare(exi.eExposi, searchStr, StringComparison.InvariantCulture) == 0){
					ListViewItem item = this.listImg.Items.Add(exi.eFname);
					_exifImgList.Add(new ImgFile(exi.eFname, exi.dtFile, exi.eDtOriginal));   
				}
			}
		}

		void SearchExifScene(string searchStr)
		{
			foreach (Exif exi in exList) {
				if (string.Compare(exi.eScene, searchStr, StringComparison.InvariantCulture) == 0){
					ListViewItem item = this.listImg.Items.Add(exi.eFname);
					_exifImgList.Add(new ImgFile(exi.eFname, exi.dtFile, exi.eDtOriginal));   
				}
			}
		}

		void SearchExifToD(string searchStr)
		{
			int tod = -1;
			if (string.Compare(T._("Night"), searchStr, StringComparison.InvariantCulture) == 0) tod = 0;
			else if (string.Compare(T._("Morning"), searchStr, StringComparison.InvariantCulture) == 0) tod = 1;
			else if (string.Compare(T._("Noon"), searchStr, StringComparison.InvariantCulture) == 0) tod = 2;
			else if (string.Compare(T._("Afternoon"), searchStr, StringComparison.InvariantCulture) == 0) tod = 3;
			else if (string.Compare(T._("Evening"), searchStr, StringComparison.InvariantCulture) == 0) tod = 4;

			foreach (Exif exi in exList) {
				if (exi.eTimeOfD ==  tod){
					ListViewItem item = this.listImg.Items.Add(exi.eFname);
					_exifImgList.Add(new ImgFile(exi.eFname, exi.dtFile, exi.eDtOriginal));   
				}
			}
		}

		void SearchExifFLen(string searchStr)
		{
			foreach (Exif exi in exList) {
				if (string.Compare(exi.eFLength, searchStr, StringComparison.InvariantCulture) == 0){
					ListViewItem item = this.listImg.Items.Add(exi.eFname);
					_exifImgList.Add(new ImgFile(exi.eFname, exi.dtFile, exi.eDtOriginal));   
				}
			}
		}

		void SearchExifGps()
		{
			foreach (Exif exi in exList) {
				if (exi.eGps == true){
					ListViewItem item = this.listImg.Items.Add(exi.eFname);
					_exifImgList.Add(new ImgFile(exi.eFname, exi.dtFile, exi.eDtOriginal));   
				}
			}
		}

		void SearchExifFace()
		{
			foreach (Exif exi in exList) {
				if (exi.eFace == true){
					ListViewItem item = this.listImg.Items.Add(exi.eFname);
					_exifImgList.Add(new ImgFile(exi.eFname, exi.dtFile, exi.eDtOriginal));   
				}
			}
		}

		void SearchExifFlash()
		{
			foreach (Exif exi in exList) {
				if (exi.eFlash == true){
					ListViewItem item = this.listImg.Items.Add(exi.eFname);
					_exifImgList.Add(new ImgFile(exi.eFname, exi.dtFile, exi.eDtOriginal));   
				}
			}
		}

		void SearchExifDate(string searchStr)
		{
			foreach (Exif exi in exList) {
				string datePart = "";
				if (_rangeType == 1){
					datePart = String.Format("{0:yyyy}", exi.eDtOriginal );
				}
				else if (_rangeType == 2){
					datePart = String.Format("{0:yyyy-MM}", exi.eDtOriginal );
				}
				else if (_rangeType == 3){
					datePart = String.Format("{0:MM-dd}", exi.eDtOriginal );
				}
				else {
					datePart = String.Format("{0:HH}", exi.eDtOriginal);
					searchStr = searchStr.Substring(0, 2);
				}
				if (string.Compare(datePart, searchStr, StringComparison.InvariantCulture) == 0){
					ListViewItem item = this.listImg.Items.Add(exi.eFname);
					_exifImgList.Add(new ImgFile(exi.eFname, exi.dtFile, exi.eDtOriginal));   
				}
			}
		}

    // ------------------------------   events form ----------------------------------------------------------

		void ExifDashEnter(object sender, EventArgs e)
		{
			Debug.WriteLine("dash enter: ");
			Settings.Default.LastTab = 2;
			int dW = Settings.Default.DashW;
			int dH = Settings.Default.DashH;
			SetWindowSize(dW, dH, 0);
			SetCommand('p', "");
		}

		void ExifDashLeave(object sender, EventArgs e)
		{
		  if (_leave){
			  Debug.WriteLine("dash leave: ");
			  SetCommand('l', "");
		  }
		  _leave = true;
		}

		void ExifDashFormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
			this.Hide();
			//this.Close();
		}

		void ExifDashDragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.Copy;
		}

		void ExifDashDragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
				string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
				string dropFile = files[0];
				string dropDir = "";
				if (File.Exists(dropFile)) {
					dropDir = Path.GetDirectoryName(dropFile);
				}
				else if (Directory.Exists(dropFile)){
						dropDir = dropFile;
				}
				edImgPath.Text = dropDir;
				this.Text = dropDir;
			}
		}


    // ------------------------------   events  ----------------------------------------------------------

		void CmdStartClick(object sender, EventArgs e)
		{
			CheckStart();
		}

		void ListExiftDoubleClick(object sender, EventArgs e)
		{
			listImg.Items.Clear();
			_exifImgList.Clear();
			string selExif = listExift.SelectedItems[0].Text;
			SearchExif(selExif);
		}

		void ListModelDoubleClick(object sender, EventArgs e)
		{
			listImg.Items.Clear();
			_exifImgList.Clear();
			string selList = listModel.SelectedItems[0].Text;
			SearchExifModel(selList);
		}

		void ListLensDoubleClick(object sender, EventArgs e)
		{
			listImg.Items.Clear();
			_exifImgList.Clear();
			string selLens = listLens.SelectedItems[0].Text;
			SearchExifLens(selLens);
		}

		void ListExpoDoubleClick(object sender, EventArgs e)
		{
			listImg.Items.Clear();
			_exifImgList.Clear();
			string selExpo = listExpo.SelectedItems[0].Text;
			SearchExifExpo(selExpo);
		}

		void ListSceneDoubleClick(object sender, EventArgs e)
		{
			listImg.Items.Clear();
			_exifImgList.Clear();
			string selScene = listScene.SelectedItems[0].Text;
			SearchExifScene(selScene);
		}

		void ListToDDoubleClick(object sender, EventArgs e)
		{
			listImg.Items.Clear();
			_exifImgList.Clear();
			string selToD = listToD.SelectedItems[0].Text;
			SearchExifToD(selToD);
		}

		void LblGpsDoubleClick(object sender, EventArgs e)
		{
			listImg.Items.Clear();
			_exifImgList.Clear();
			SearchExifGps();
		}

		void LblFaceDoubleClick(object sender, EventArgs e)
		{
			listImg.Items.Clear();
			_exifImgList.Clear();
			SearchExifFace();
		}

		void LblFlashDoubleClick(object sender, EventArgs e)
		{
			listImg.Items.Clear();
			_exifImgList.Clear();
			SearchExifFlash();
		}

		void ListFLenDoubleClick(object sender, EventArgs e)
		{
			listImg.Items.Clear();
			_exifImgList.Clear();
			string selFLen = listFLen.SelectedItems[0].Text;
			SearchExifFLen(selFLen);
		}

		void PopPropertiesClick(object sender, EventArgs e)
		{
			if (listImg.SelectedItems.Count > 0){
				FileInfo.ShowFileProperties(listImg.SelectedItems[0].Text);
			}
		}

		void ListProjectDoubleClick(object sender, EventArgs e)
		{
			if (listImg.SelectedItems.Count > 0){
				string eImg = listImg.SelectedItems[0].Text;
				SetCommand('i', eImg);
			}
		}

		void CmdShowClick(object sender, EventArgs e)
		{
			if (_exifImgList.Count > 0){
				string eImg = listImg.Items[0].Text;
				if (listImg.SelectedItems.Count > 0){
					eImg = listImg.SelectedItems[0].Text;
				}
				SetCommand('i', eImg);
			}
		}

		void ChartImgMouseDoubleClick(object sender, MouseEventArgs e)
		{
			var pos = e.Location;
			var results = chartImg.HitTest(pos.X, pos.Y, false, ChartElementType.DataPoint);//  .PlottingArea);
			foreach (var result in results)
			{
				if (result.ChartElementType == ChartElementType.DataPoint){
					var xVal = Math.Round(result.ChartArea.AxisX.PixelPositionToValue(pos.X));
					string lblCap;
					int xValInt = (int)xVal;
					ChartXLabel(out lblCap, xValInt);
					//Debug.WriteLine("chart pos: " + xValInt + " " + lblCap);

					listImg.Items.Clear();
					_exifImgList.Clear();
					SearchExifDate(lblCap);
				}
			}
		}

		void CmdUpClick(object sender, EventArgs e)
		{
			string sDir = edImgPath.Text;
			if (Directory.Exists(sDir)){
				var upperDir = Directory.GetParent(sDir);
				if (upperDir != null) {
					edImgPath.Text = Directory.GetParent(sDir).FullName;
					this.Text = edImgPath.Text;
				}
			}
			else {
				MessageBox.Show(T._("Invalid directory"), T._("Error"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

    // ------------------------------   BackgroundWorker  ----------------------------------------------------------

		public bool ScanImages(BackgroundWorker bw)
		// called by: BackgroundWorker1DoWork
		// shown with ExifDataShow
		{
			var dashImgList = new List<string>();
			bool allDirs = false;			
			if (allDirs){
				dashImgList = Directory.GetFiles(_imgDir, "*.*", SearchOption.AllDirectories)
									.Where(file => _validExtensions.Any(file.ToLower().EndsWith))
									.ToList();
			}
			else {
				dashImgList = Directory.GetFiles(_imgDir)
									.Where(file => _validExtensions.Any(file.ToLower().EndsWith))
									.ToList();
			}
			FilenameComparer fc = new FilenameComparer();
			dashImgList.Sort(fc);					
				
			int exType;
			string orientation;
			string model;
			DateTime dtOriginal;
			int timeOfD;
			string expotime;
			string fnumber;
			string fLength;
			bool flash;
			string exposi;
			string lens;
			string scene;
			bool gps;
			bool face;
			string exError;
			DateTime nullDate = DateTime.MinValue;
			int maxTicks = dashImgList.Count() / 100;
			if (maxTicks < 1) maxTicks = 1;
			bw.ReportProgress(maxTicks, T._("Start"));

			int fCount = 0;
			DateTime priorDate = DateTime.MaxValue;
			var spanDict = new Dictionary<int, int>();
			ExifRead.ResetBox();
			
			foreach (string picPath in dashImgList)
			{	
				ExifRead.CheckExif(out exType, out orientation, out model, out dtOriginal, out timeOfD, out expotime, out fnumber, out fLength, out flash, out exposi, out lens, out scene,
				               out gps, out face, out exError, picPath);
				DateTime dtCreation = File.GetCreationTime(picPath);
				DateTime dtChanged = File.GetLastWriteTime(picPath);
				DateTime dtFile;
				if (dtCreation < dtChanged){
					dtFile = dtCreation;
				}
				else {
					dtFile = dtChanged;
				}
				
				exList.Add(new Exif(exType, model, dtOriginal, timeOfD,
				                  expotime, fnumber, fLength, flash,  exposi, lens, scene,
					                gps, face, picPath, dtFile));
				if (exError != ""){
					errorList.Add(exError);
				}
				fCount++;
				int mod = fCount % 100;
				if (mod == 0){
					bw.ReportProgress(-1, "");
				}

				if (dtOriginal != nullDate){
					//Debug.WriteLine("path: " + picPath + " " + dtOriginal.ToString());
					_dateCount++;
					if (_minDate > dtOriginal) _minDate = dtOriginal;
					if (_maxDate < dtOriginal) _maxDate = dtOriginal;

					if (_dateCount > 1){
						TimeSpan span = dtOriginal.Subtract(priorDate);
						int spanSec = Math.Abs((int) span.TotalSeconds);
						spanDict.Add(fCount, spanSec);
					}
					priorDate = dtOriginal;
				}

				if (dicExift.ContainsKey(exType)) dicExift[exType] +=1;
				else dicExift[exType] =1;
				if (dicModel.ContainsKey(model)) dicModel[model] +=1;
				else dicModel[model] =1;
				if (dicToD.ContainsKey(timeOfD)) dicToD[timeOfD] +=1;
				else dicToD[timeOfD] =1;

				if (dicExpot.ContainsKey(expotime)) dicExpot[expotime] +=1;
				else dicExpot[expotime] =1;
				if (dicFNum.ContainsKey(fnumber)) dicFNum[fnumber] +=1;
				else dicFNum[fnumber] =1;

				if (dicFLen.ContainsKey(fLength)) dicFLen[fLength] +=1;
				else dicFLen[fLength] =1;
				if (flash) ++_flashCount;
				if (dicExposi.ContainsKey(exposi)) dicExposi[exposi] +=1;
				else dicExposi[exposi] =1;

				if (dicLens.ContainsKey(lens)) dicLens[lens] +=1;
				else dicLens[lens] =1;
				if (dicScene.ContainsKey(scene)) dicScene[scene] +=1;
				else dicScene[scene] =1;

				if (gps) ++_gpsCount;
				if (face) ++_faceCount;

			}
			return true;
		}

		void BackgroundWorker1DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		// started with RunWorkerAsync
		{
			BackgroundWorker worker = sender as BackgroundWorker;
			if (worker.CancellationPending == true)
			{
				//Debug.WriteLine("bw2: cancel");
				e.Cancel = true;
			}
			else
			{
				//Debug.WriteLine("bw2: start scan");
				// init progress bar
				ScanImages(sender as BackgroundWorker);

			}
		}

		void BackgroundWorker1ProgressChanged(object sender, ProgressChangedEventArgs e)
		// called by: BackgroundWorker1.ReportProgress
		{
			int steps = e.ProgressPercentage;
				SetStatusText(steps, "");
		}

		void BackgroundWorker1RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			_stop = false;
			cmdStart.Text = T._("&Start");
			if (e.Cancelled == true)
			{
				//Debug.WriteLine("bw: cancel2: ");
			}
			else if (e.Error != null)
			{
				string ExceptionFormatString = "Exception bw 1: {0}{1}Exception Source: {2}{1}Exception StackTrace: {3}{1}";
				string exDetail = String.Format(ExceptionFormatString, e.Error.Message, Environment.NewLine, e.Error.Source, e.Error.StackTrace);
				Util.ReportError(exDetail);
			}
			else
			{
				_sw1.Stop();
				TimeSpan t = TimeSpan.FromMilliseconds(_sw1.Elapsed.TotalMilliseconds);
				string ptime = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms", t.Hours, t.Minutes, t.Seconds, t.Milliseconds);

				ExifDataShow( );

				// MessageBox.Show ("Directories in Source Path now sorted by release date", "Wrong sort sequence",

				SetStatusText(-9, "");  // progress reset
				SetStatusText(0, T._("Scan complete") + " - " + edImgPath.Text + "  "  + ptime);
				DateTime lastScan = DateTime.Now;
			}
		}

		void ExifDashLoad(object sender, EventArgs e)
		{
			TranslateExifDashForm();
			ToolTip toolTip1 = new ToolTip();
			// Set up the delays for the ToolTip.
			toolTip1.AutoPopDelay = 5000;
			toolTip1.InitialDelay = 500;
			toolTip1.ReshowDelay = 500;
			// Force the ToolTip text to be displayed whether or not the form is active.
			toolTip1.ShowAlways = true;
			// Set up the ToolTip text for the Button and Checkbox.
			toolTip1.SetToolTip(this.cmdUp, T._("One directory up"));
		}



  // end Background

		public void TranslateExifDashForm( )
		{
			Text = T._("Exif dashboard");
			cmdStart.Text = T._("&Start");
			label1.Text = T._("Path");
			colFiles.Text = T._("Filename");
			popProperties.Text = T._("Properties...");
			columnHeader1.Text = T._("Model");
			columnHeader2.Text = T._("Count");
			columnHeader3.Text = T._("Lens");
			columnHeader4.Text = T._("Count");
			columnHeader5.Text = T._("Scene");
			columnHeader6.Text = T._("Count");
			columnHeader7.Text = T._("Exposition");
			columnHeader8.Text = T._("Count");
			columnHeader9.Text = T._("Exif");
			columnHeader10.Text = T._("Count");
			cmdShow.Text = T._("Show");
			columnHeader11.Text = T._("Time");
			columnHeader12.Text = T._("Count");
			lblFlash.Text = T._("Flash" + ": ");
			columnHeader13.Text = T._("Focus Length");
			columnHeader14.Text = T._("Count");
			lblInfo.Text = T._("Info:");
			chartImg.Titles[0].Text = T._("Images per day");
			lblFace.Text = T._("Face" + ": ");
		}

		// ------------------------------   delegates   ----------------------------------------------------------

		public void SetStatusText(int sVal, string sText)
		{
		  // called by: BackgroundWorker1RunWorkerCompleted, BackgroundWorker1ProgressChanged
			// output: main.HandleStatus
			OnStatusChanged(new SetStatusMainEventArgs(sVal, sText));
			Application.DoEvents();
		}

		protected virtual void OnStatusChanged(SetStatusMainEventArgs e)
		{
			if(this.StatusChanged != null)     // nothing subscribed to this event
			{
				this.StatusChanged(this, e);
			}
		}

		public void SetWindowSize(int w, int h, int exifType)
		{
			// called by: PicLoad 3*, Enter
			// output: main.SetWindowSize
			OnWindowSize(new SetSizeEventArgs(w, h, exifType));
			Application.DoEvents();
		}

		protected virtual void OnWindowSize(SetSizeEventArgs  e)
		{
			if(this.WindowSize != null)    // nothing subscribed to this event
			{
				this.WindowSize(this, e);
			}
		}

		public void SetCommand(char comm, string fName)
		{
		// called by: close: CmdShow, dblclick
		// main HandleCommand
			OnCommandChanged(new SetCommandEventArgs(comm, fName));
			Application.DoEvents();
		}

		protected virtual void OnCommandChanged(SetCommandEventArgs e)
		{
			if(this.CommandChanged != null)
			{
				this.CommandChanged(this, e);
			}
		}


	}  // end ExifDash

	public class NsComparer: IComparer<string>
	{
		[DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
		static extern Int32 StrCmpLogical(String x, String y);

		public int Compare(string x, string y)
		{
			return StrCmpLogical(x, y);
		}
	}

}
