﻿/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
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
using System.Collections.Generic;
using System.ComponentModel;  // BackgroundWorker
using System.Diagnostics;  // Debug
using System.IO;	 //	Directory
using System.Linq;	 //	OfType
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
		ImgList _il;
		bool _stop = false;
		string _imgDir = "";
		public string _exifImg {get;set;}
		public bool _ExifReturn {get;set;}

		Stopwatch _sw1 = new Stopwatch();

		List<Exif> exList = new List<Exif>();

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

		int _dateCount = 0;
		int _rangeType = 0;
		DateTime _minDate = DateTime.MaxValue;
		DateTime _maxDate = DateTime.MinValue;

		List<string> exifImgList = new List<string>();

		public event HandleStatusMainChange  StatusChanged;

		public ExifDash()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

			_ExifReturn = false;
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}

    // ------------------------------   functions  ----------------------------------------------------------

		public void SetPath(string fPath, ImgList il)
		// from imageForm
		{
			_il = il;
			edImgPath.Text = Path.GetDirectoryName(fPath);
		}

		public void FormClear()
		{
			exList.Clear();
			exifImgList.Clear();
			listImg.Items.Clear();
			listExift.Items.Clear();
			listToD.Items.Clear();
			listModel.Items.Clear();
			listLens.Items.Clear();
			listExpo.Items.Clear();
			listScene.Items.Clear();
			dicExift.Clear();
			dicToD.Clear();
			dicModel.Clear();
			dicExpot.Clear();
			dicFNum.Clear();
			dicFLen.Clear();
			_flashCount = 0;
			dicExposi.Clear();
			dicLens.Clear();
			dicScene.Clear();
			_gpsCount = 0;

			_dateCount = 0;
			_minDate = DateTime.MaxValue;
			_maxDate = DateTime.MinValue;

		}

		public void ProjectShow(int rNum, int rMax)    // details for max entry only
		// called by: main.ProjectLoad, ListSourceDirClick, BackgroundWorker1RunWorkerCompleted
		{
			//Debug.WriteLine("form p1: " + this.Width.ToString());
			listImg.Items.Clear();

			int pCount = 0;
			string[] items = { " ", " ", " ", " ", " " };   // for product column with 5 lines
			int start = 0;
			if (pCount > 2){
				items[0] = pCount.ToString() + " Products";
				items[1] = "Double click for list";
				start = 2;
			}

			int i = start;
		}

		bool CheckStart()
		{
			string imgDir = edImgPath.Text;
			if (string.IsNullOrEmpty(imgDir)) {
				MessageBox.Show ("No directory given", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return false;
			}
			if (!Directory.Exists(imgDir)) {
				MessageBox.Show ("Directory does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
				cmdStart.Text = "&Stop";
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

		void ExifDataShow()
		{
			if (_dateCount > 0){
				TimeSpan imgSpan = _maxDate.Subtract(_minDate);
				if (imgSpan.TotalDays > 730){
					_rangeType = 1;      // years
					double years = Math.Ceiling(imgSpan.TotalDays / 365.0);
					string spanS = years.ToString("0.0");
					lblInfo.Text = String.Format("Info: Number of images {0}. Between {1:yyyy-MM-dd} and {2:yyyy-MM-dd}, a span of {3} years", exList.Count, _minDate, _maxDate, spanS );
				}
				else if (imgSpan.TotalDays > 60){
					_rangeType = 2;      // months
					double months = Math.Ceiling(imgSpan.TotalDays / 30.0);
					string spanS = months.ToString("0");
					lblInfo.Text = String.Format("Info: Number of images {0}. Between {1:yyyy-MM-dd} and {2:yyyy-MM-dd}, a span of {3} months", exList.Count, _minDate, _maxDate, spanS );
				}
				else if (imgSpan.TotalDays > 1){
					_rangeType = 3;      // days
					double days = (int) Math.Ceiling(imgSpan.TotalHours / 24.0); 
					string spanS = days.ToString("0");
					lblInfo.Text = String.Format("Info: Number of images {0}. Between {1:yyyy-MM-dd} and {2:yyyy-MM-dd}, a span of {3} days", exList.Count, _minDate, _maxDate, spanS );
				}
				else {
					_rangeType = 4;      // hours
					int hours = (int) Math.Ceiling(imgSpan.TotalMinutes / 60.0);
					string spanS = hours.ToString("0");
					lblInfo.Text = String.Format("Info: Number of images {0}. On {1:yyyy-MM-dd}. Between {2:HH:mm} and {3:HH:mm}, a span of {4} hours", exList.Count, _minDate, _minDate, _maxDate, spanS );					
				}
				CheckDays( );  
			}
			else {
				lblInfo.Text = String.Format("Info: Number of images {0} ", exList.Count);
			}
			
			// show listviews										
			foreach (KeyValuePair<int, int> det in dicExift.OrderByDescending(key=> key.Key))
			{
				string exift;
				switch(det.Key)
				{
					case 1: exift = "Reduced Exif";
					break;
					case 2: exift = "Full Exif";
					break;
					default: exift = "No Exif";  // 0
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
					case 0: tod = "Night";
					break;
					case 1: tod = "Morning";
					break;
					case 2: tod = "Noon";
					break;
					case 3: tod = "Afternoon";
					break;
					case 4: tod = "Evening";
					break;
					default: tod = "No time";
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
				if (string.IsNullOrEmpty(mo)) mo = "<no data>";
				ListViewItem item = this.listModel.Items.Add(mo);
				item.ImageIndex = 0;
				item.SubItems.Add(dm.Value.ToString());
			}
			listModel.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
			listModel.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
			
			foreach (KeyValuePair<string, int> dl in dicLens.OrderBy(key=> key.Key))
			{
				string le = dl.Key;
				if (string.IsNullOrEmpty(le)) le = "<no data>";
				ListViewItem item = this.listLens.Items.Add(le);
				item.ImageIndex = 0;
				item.SubItems.Add(dl.Value.ToString());
			}
			listLens.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
			listLens.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
			
			foreach (KeyValuePair<string, int> dfl in dicFLen.OrderBy(key=> key.Key))
			{
				string fl = dfl.Key;
				if (string.IsNullOrEmpty(fl)) fl = "<no data>";
				ListViewItem item = this.listFLen.Items.Add(fl);
				item.ImageIndex = 0;
				item.SubItems.Add(dfl.Value.ToString());
			}
			listFLen.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
			listFLen.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
			
			foreach (KeyValuePair<string, int> dex in dicExposi.OrderBy(key=> key.Key))
			{
				string exp = dex.Key;
				if (string.IsNullOrEmpty(exp)) exp = "<no data>";
				ListViewItem item = this.listExpo.Items.Add(exp);
				item.ImageIndex = 0;
				item.SubItems.Add(dex.Value.ToString());
			}
			listExpo.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
			listExpo.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

			foreach (KeyValuePair<string, int> ds in dicScene.OrderBy(key=> key.Key))
			{
				string sc = ds.Key;
				if (string.IsNullOrEmpty(sc)) sc = "<no data>";
				ListViewItem item = this.listScene.Items.Add(sc);
				item.ImageIndex = 0;
				item.SubItems.Add(ds.Value.ToString());
			}
			listScene.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
			listScene.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

			lblGps.Text = "GPS: " + _gpsCount.ToString();
			lblFlash.Text = "Flash: " + _flashCount.ToString();
		}
		 
		void CheckDays( )
		{
			Dictionary<string, int> dicPeriod = new Dictionary<string, int>();
			//  
			//Debug.WriteLine("year start: " + nextYear.ToString());
			string periodFormat = "";
			string chartTitle = "";
			if (_rangeType == 1){  // years 
				DateTime nextYear = new DateTime (_minDate.Year, 1, 1);  // time 0:0:0
				periodFormat = "{0:yyyy}";
				chartTitle = "Images per year";
				do {    // pre fill 
					string year = String.Format(periodFormat , nextYear);
					dicPeriod.Add(year, 0);
					nextYear = nextYear.AddYears(1);
				} while (nextYear <= _maxDate);
			}
			else if (_rangeType == 2){
				DateTime nextMonth = new DateTime (_minDate.Year, _minDate.Month, 1);  // time 0:0:0
				periodFormat = "{0:yyyy-MM}";
				chartTitle = "Images per month";
				do {     
					string month = String.Format(periodFormat , nextMonth);
					dicPeriod.Add(month, 0);
					nextMonth = nextMonth.AddMonths(1);
					Debug.WriteLine("period0: " + month);
				} while (nextMonth <= _maxDate);				
			}
			else if (_rangeType == 3){   // days
				DateTime nextDay = new DateTime (_minDate.Year, _minDate.Month, _minDate.Day);   
				periodFormat = "{0:yyyy-MM-dd}";
				chartTitle = "Images per day";
				do {     
					string day = String.Format(periodFormat , nextDay);
					dicPeriod.Add(day, 0);
					nextDay = nextDay.AddDays(1);
				} while (nextDay <= _maxDate);
			}
			else {   // hours
				DateTime nextHour = new DateTime(_minDate.Year, _minDate.Month, _minDate.Day, _minDate.Hour, 0, 0);   
				periodFormat = "{0:yyyy-MM-dd HH}";
				chartTitle = "Images per hour";
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
			int i = 0;
			foreach (CustomLabel lbl in chartImg.ChartAreas[0].AxisX.CustomLabels)
			{
				Int32.TryParse(lbl.Text, out i);
				if (_rangeType == 1){
					var startYear = new DateTime (_minDate.Year - 1, 01, 01);
					DateTime lDate = startYear.AddYears(i);
					lbl.Text = string.Format("{0:yyyy}", lDate);
				}
				else if (_rangeType == 2){
					var startMonth = new DateTime (_minDate.Year, _minDate.Month - 1, 01);
					DateTime lDate = startMonth.AddMonths(i);
					lbl.Text = string.Format("{0:yyyy-MM}", lDate);
				}
				else if (_rangeType == 3){
					var startDay = new DateTime (_minDate.Year, _minDate.Month,  _minDate.Day);
					DateTime lDate = startDay.AddDays(i);
					lbl.Text = string.Format("{0:MM-dd}", lDate);
				}
				else {
					var startHour = new DateTime (_minDate.Year, _minDate.Month,  _minDate.Day, _minDate.Hour - 1, 0, 0);
					DateTime lDate = startHour.AddHours(i);
					lbl.Text = string.Format("{0:HH:mm}", lDate);
				}				
			}
		}
		
		void SearchExif(string searchStr)
		{
			int ext = 0;     // No Exif
			if (string.Compare("Reduced Exif", searchStr) == 0) ext = 1;
			else if (string.Compare("Full Exif", searchStr) == 0) ext = 2;
						
			foreach (Exif exi in exList) {
				if (exi.eType ==  ext){
					ListViewItem item = this.listImg.Items.Add(exi.eFname);
					exifImgList.Add(exi.eFname);
				}
			}
		}

		void SearchExifModel(string searchStr)
		{
			foreach (Exif exi in exList) {
				if (string.Compare(exi.eModel, searchStr) == 0){
					ListViewItem item = this.listImg.Items.Add(exi.eFname);
					exifImgList.Add(exi.eFname);
				}
			}
		}

		void SearchExifLens(string searchStr)
		{
			foreach (Exif exi in exList) {
				if (string.Compare(exi.eLensmodel, searchStr) == 0){
					ListViewItem item = this.listImg.Items.Add(exi.eFname);
					exifImgList.Add(exi.eFname);
				}
			}
		}

		void SearchExifExpo(string searchStr)
		{
			foreach (Exif exi in exList) {
				if (string.Compare(exi.eExposi, searchStr) == 0){
					ListViewItem item = this.listImg.Items.Add(exi.eFname);
					exifImgList.Add(exi.eFname);
				}
			}
		}
								
		void SearchExifScene(string searchStr)
		{
			foreach (Exif exi in exList) {
				if (string.Compare(exi.eScene, searchStr) == 0){
					ListViewItem item = this.listImg.Items.Add(exi.eFname);
					exifImgList.Add(exi.eFname);
				}
			}
		}

		void SearchExifToD(string searchStr)
		{
			int tod = -1;
			if (string.Compare("Night", searchStr) == 0) tod = 0;
			else if (string.Compare("Morning", searchStr) == 0) tod = 1;
			else if (string.Compare("Noon", searchStr) == 0) tod = 2;
			else if (string.Compare("Afternoon", searchStr) == 0) tod = 3;
			else if (string.Compare("Evening", searchStr) == 0) tod = 4;

			foreach (Exif exi in exList) {
				if (exi.eTimeOfD ==  tod){
					ListViewItem item = this.listImg.Items.Add(exi.eFname);
					exifImgList.Add(exi.eFname);
				}
			}
		}
		
		void SearchExifFLen(string searchStr)
		{
			foreach (Exif exi in exList) {
				if (string.Compare(exi.eFLength, searchStr) == 0){
					ListViewItem item = this.listImg.Items.Add(exi.eFname);
					exifImgList.Add(exi.eFname);
				}
			}
		}

		void SearchExifGps()
		{
			foreach (Exif exi in exList) {
				if (exi.eGps == true){
					ListViewItem item = this.listImg.Items.Add(exi.eFname);
					exifImgList.Add(exi.eFname);
				}
			}
		}

		void SearchExifFlash()
		{
			foreach (Exif exi in exList) {
				if (exi.eFlash == true){
					ListViewItem item = this.listImg.Items.Add(exi.eFname);
					exifImgList.Add(exi.eFname);
				}
			}
		}

    // ------------------------------   events form ----------------------------------------------------------

		void ExifDashEnter(object sender, EventArgs e)
		{
			Settings.Default.LastTab = 2;
		}

		void ExifDashFormClosing(object sender, FormClosingEventArgs e)
		{
			//e.Cancel = true;
			//this.Hide();
			this.Close();
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
			}
		}


    // ------------------------------   events  ----------------------------------------------------------

		void CmdStartClick(object sender, EventArgs e)
		{
			CheckStart();
		}
		
		void ListExiftDoubleClick(object sender, EventArgs e)
		{
			string selExif = listExift.SelectedItems[0].Text;
			SearchExif(selExif);
		}

		void ListModelDoubleClick(object sender, EventArgs e)
		{
			string selList = listModel.SelectedItems[0].Text;
			SearchExifModel(selList);
		}

		void ListLensDoubleClick(object sender, EventArgs e)
		{
			string selLens = listLens.SelectedItems[0].Text;
			SearchExifLens(selLens);
		}

		void ListExpoDoubleClick(object sender, EventArgs e)
		{
			string selExpo = listExpo.SelectedItems[0].Text;
			SearchExifExpo(selExpo);
		}

		void ListSceneDoubleClick(object sender, EventArgs e)
		{
			string selScene = listScene.SelectedItems[0].Text;
			SearchExifScene(selScene);
		}

		void ListToDDoubleClick(object sender, EventArgs e)
		{
			string selToD = listToD.SelectedItems[0].Text;
			SearchExifToD(selToD);	
		}

		void LblGpsDoubleClick(object sender, EventArgs e)
		{
			SearchExifGps();
		}

		void LblFlashDoubleClick(object sender, EventArgs e)
		{
			SearchExifFlash();
		}

		void ListFLenDoubleClick(object sender, EventArgs e)
		{
			string selFLen = listFLen.SelectedItems[0].Text;
			SearchExifFLen(selFLen);
		}

				
		void PopPathRemoveClick(object sender, EventArgs e)
		{

		}


		void ListProjectDoubleClick(object sender, EventArgs e)
		{

		}


		void CmdShowClick(object sender, EventArgs e)
		{
			if (exifImgList.Count > 0){
					_il.DirClear();
					_il._imList = exifImgList;
					if (listImg.SelectedItems.Count > 0){
						_exifImg = listImg.SelectedItems[0].Text;
					}
					else {
						_exifImg = "";
					}
					_ExifReturn = true;
			}
			this.Close();
		}

    // ------------------------------   BackgroundWorker  ----------------------------------------------------------

		public bool ScanImages(BackgroundWorker bw)
		// called by: BackgroundWorker1DoWork
		// shown with ExifDataShow
		{
			string[] imgList = Directory.GetFiles(_imgDir, "*.jpg", SearchOption.AllDirectories);
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
			DateTime nullDate = DateTime.MinValue;


			foreach (string picPath in imgList)
			{

				Util.CheckExif(out exType, out orientation, out model, out dtOriginal, out timeOfD, out expotime, out fnumber, out fLength, out flash, out exposi, out lens, out scene,
				               out gps, picPath);
				exList.Add(new Exif(exType, model, dtOriginal, timeOfD,
				                    expotime, fnumber, fLength, flash,  exposi, lens, scene,
					                  gps, picPath));
				if (dtOriginal != nullDate){
					//Debug.WriteLine("path: " + picPath + " " + dtOriginal.ToString());
					_dateCount++;
					if (_minDate > dtOriginal) _minDate = dtOriginal;
					if (_maxDate < dtOriginal) _maxDate = dtOriginal;
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

			}

			foreach (KeyValuePair<string, int> dfn in dicFNum)
			{
				//Debug.WriteLine("F Num: " + dfn.Key + " " + dfn.Value);
			}

			return true;
		}


		public void SetText(string text1)
		{
		  // called by: BackgroundWorker1RunWorkerCompleted, BackgroundWorker1ProgressChanged
			// output: main.HandleStatus
			OnStatusChanged(new SetStatusMainEventArgs(text1));
			Application.DoEvents();
		}

		protected virtual void OnStatusChanged(SetStatusMainEventArgs e)
		{
			if(this.StatusChanged != null)     // nothing subscribed to this event
			{
				this.StatusChanged(this, e);
			}
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

				worker.ReportProgress(1, "Start");
				ScanImages(sender as BackgroundWorker);

			}
		}

		void BackgroundWorker1ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
		// called by: BackgroundWorker1.ReportProgress
		{
			int steps = e.ProgressPercentage;
			if (steps > 0){
				SetText(steps.ToString());
			}
			string m1 = (string)e.UserState;
			//Debug.WriteLine("bw: " + m1 );
			SetText(m1);
		}

		void BackgroundWorker1RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			_stop = false;
			cmdStart.Text = "&Start";
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

				Debug.WriteLine("bw: Scan complete: " + edImgPath.Text + "  "  + ptime);


				ExifDataShow( );


				// MessageBox.Show ("Directories in Source Path now sorted by release date", "Wrong sort sequence",

				SetText("Scan complete - " + edImgPath.Text + "  "  + ptime);
				SetText("999");  // progress reset
				DateTime lastScan = DateTime.Now;



			}
		}
		void ExifDashLoad(object sender, EventArgs e)
		{

		}



  // end Background

	}  // end exif
}
