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
using System.Collections.Generic;
using System.ComponentModel;  // BackgroundWorker
using System.Diagnostics;  // Debug
using System.IO;	 //	Directory
using System.Linq;	 //	OfType
using System.Windows.Forms;
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
		Dictionary<string, int> dicFlash = new Dictionary<string, int>();
		Dictionary<string, int> dicExposi = new Dictionary<string, int>();
		Dictionary<string, int> dicLens = new Dictionary<string, int>();
		Dictionary<string, int> dicScene = new Dictionary<string, int>();
		Dictionary<bool, int> dicGps = new Dictionary<bool, int>();
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
			listModel.Items.Clear();
			listLens.Items.Clear();
			listExpo.Items.Clear();
			listScene.Items.Clear();
			dicExift.Clear();
			dicToD.Clear();
			dicModel.Clear();
			dicExpot.Clear();
			dicFNum.Clear();
			dicFlash.Clear();
			dicExposi.Clear();
			dicLens.Clear();
			dicScene.Clear();
			dicGps.Clear();
		}

		public void ProjectPathClear()
		{

			edImgPath.Text = "";
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


		void PopPathRemoveClick(object sender, EventArgs e)
		{

		}


		void CmdStartClick(object sender, EventArgs e)
		{
			CheckStart();
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

		void ListProjectDoubleClick(object sender, EventArgs e)
		{

		}

    // ------------------------------   BackgroundWorker  ----------------------------------------------------------

		public void StartScan()
		{
			if (_stop == false){
				_stop = true;
				cmdStart.Text = "&Stop";
				Debug.WriteLine("bw: start: ");
				if (backgroundWorker1.IsBusy != true)
				{
					Debug.WriteLine("bw: run: ");
					_sw1.Start();
					backgroundWorker1.RunWorkerAsync();
				}
			}
			else {
				backgroundWorker1.CancelAsync();

				//Debug.WriteLine("bw: cancel1: ");
			}
		}

		public void BW_Active()
		{
				if (backgroundWorker1.IsBusy == true){
					Debug.WriteLine("bw3: is running ");
				}
				else {
					Debug.WriteLine("bw3: is NOT running ");
				}
		}

		public bool ScanImages(BackgroundWorker bw)
		// called by: BackgroundWorker1DoWork
		{
			string[] imgList = Directory.GetFiles(_imgDir, "*.jpg", SearchOption.AllDirectories);
			int exType;
			string orientation;
			string model;
			int timeOfD;
			string expotime;
			string fnumber;
			string flash;
			string exposi;
			string lens;
			string scene;
			bool gps;

			foreach (string picPath in imgList)
			{
				//Debug.WriteLine("path: " + picPath);
				Util.CheckExif(out exType, out orientation, out model, out timeOfD, out expotime, out fnumber, out flash, out exposi, out lens, out scene,
				               out gps, picPath);
				exList.Add(new Exif(exType, model, exposi, lens, scene,
					gps, picPath));

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
				if (dicFlash.ContainsKey(flash)) dicFlash[flash] +=1;
				else dicFlash[flash] =1;
				if (dicExposi.ContainsKey(exposi)) dicExposi[exposi] +=1;
				else dicExposi[exposi] =1;

				if (dicLens.ContainsKey(lens)) dicLens[lens] +=1;
				else dicLens[lens] =1;
				if (dicScene.ContainsKey(scene)) dicScene[scene] +=1;
				else dicScene[scene] =1;
				if (dicGps.ContainsKey(gps)) dicGps[gps] +=1;
				else dicGps[gps] =1;

			}


			foreach (KeyValuePair<string, int> dfn in dicFNum)
			{
				Debug.WriteLine("F Num: " + dfn.Key + " " + dfn.Value);
			}
			foreach (KeyValuePair<string, int> dfl in dicFlash)
			{
				Debug.WriteLine("Flash: " + dfl.Key + " " + dfl.Value);
			}

			foreach (KeyValuePair<bool, int> dg in dicGps)
			{
				Debug.WriteLine("gps: " + dg.Key + " " + dg.Value);
			}

			Debug.WriteLine("img count: " + imgList.Length.ToString());

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


				ImgListShow( );


				// MessageBox.Show ("Directories in Source Path now sorted by release date", "Wrong sort sequence",

				SetText("Scan complete - " + edImgPath.Text + "  "  + ptime);
				SetText("999");  // progress reset
				DateTime lastScan = DateTime.Now;



			}
		}



  // end Background

		void ImgListShow()
		{
			foreach (KeyValuePair<int, int> det in dicExift)
			{
				string exift;
				switch(det.Key)
				{
					case 1: exift = "Reduced Exif";
					break;
					case 2: exift = "Full Exif";
					break;
					default: exift = "No Exif";
					break;
				}
				ListViewItem item = this.listExift.Items.Add(exift);
				item.ImageIndex = 0;
				item.SubItems.Add(det.Value.ToString());
			}

			foreach (KeyValuePair<int, int> dtd in dicToD)
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

			foreach (KeyValuePair<string, int> dm in dicModel)
			{
				string mo = dm.Key;
				if (string.IsNullOrEmpty(mo)) mo = "<no model>";
				ListViewItem item = this.listModel.Items.Add(mo);
				item.ImageIndex = 0;
				item.SubItems.Add(dm.Value.ToString());
			}

			foreach (KeyValuePair<string, int> dl in dicLens)
			{
				string le = dl.Key;
				if (string.IsNullOrEmpty(le)) le = "<no lens>";
				ListViewItem item = this.listLens.Items.Add(le);
				item.ImageIndex = 0;
				item.SubItems.Add(dl.Value.ToString());
			}

			foreach (KeyValuePair<string, int> dex in dicExposi)
			{
				string exp = dex.Key;
				if (string.IsNullOrEmpty(exp)) exp = "<no exposition>";
				ListViewItem item = this.listExpo.Items.Add(exp);
				item.ImageIndex = 0;
				item.SubItems.Add(dex.Value.ToString());
			}

			foreach (KeyValuePair<string, int> ds in dicScene)
			{
				string sc = ds.Key;
				if (string.IsNullOrEmpty(sc)) sc = "<no scene>";
				ListViewItem item = this.listScene.Items.Add(sc);
				item.ImageIndex = 0;
				item.SubItems.Add(ds.Value.ToString());
			}

		}



		void ListExiftDoubleClick(object sender, EventArgs e)
		{

		}

		void ListModelDoubleClick(object sender, EventArgs e)
		{

		}

		void ListLensDoubleClick(object sender, EventArgs e)
		{

		}

		void ListExpoDoubleClick(object sender, EventArgs e)
		{

		}

		void ListSceneDoubleClick(object sender, EventArgs e)
		{
			string selScene = listScene.SelectedItems[0].Text;
			SearchExif(5, selScene );
		}

		void SearchExif(int sCol, string searchStr)
		{
			switch(sCol)
			{
				case 5:
					foreach (Exif exi in exList) {
						if (string.Compare(exi.eScene, searchStr) == 0){
							ListViewItem item = this.listImg.Items.Add(exi.eFname);
							exifImgList.Add(exi.eFname);
						}
					}
					break;
			}
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




	}  // end frmProject
}
