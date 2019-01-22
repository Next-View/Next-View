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

		bool _stop = false;
		string _imgDir = "";

		Stopwatch _sw1 = new Stopwatch();

		Dictionary<string, int> dicModel = new Dictionary<string, int>();
		Dictionary<string, int> dicFNum = new Dictionary<string, int>();
		Dictionary<string, int> dicFlash = new Dictionary<string, int>();
		Dictionary<string, int> dicExpo = new Dictionary<string, int>();
		Dictionary<string, int> dicLens = new Dictionary<string, int>();
		Dictionary<bool, int> dicGps = new Dictionary<bool, int>();

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

		public void SetPath(string fPath)
		// from imageForm
		{
			edImgPath.Text = Path.GetDirectoryName(fPath);
		}

		public void ProjectClear()
		{
			listProject.Items.Clear();
		}

		public void ProjectPathClear()
		{

			edImgPath.Text = "";
		}

		public void ProjectShow(int rNum, int rMax)    // details for max entry only
		// called by: main.ProjectLoad, ListSourceDirClick, BackgroundWorker1RunWorkerCompleted
		{
			//Debug.WriteLine("form p1: " + this.Width.ToString());
			listProject.Items.Clear();

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

		void frmProjectEnter(object sender, EventArgs e)
		{
			Settings.Default.LastTab = 2;
		}

		void frmProjectFormClosing(object sender, FormClosingEventArgs e)
		{
			//e.Cancel = true;
			//this.Hide();
			this.Close();
		}




		void frmProjectLoad(object sender, EventArgs e)
		{

		}

		void FrmProjectHelpRequested(object sender, HelpEventArgs hlpevent)
		{
			Help.ShowHelp(this, "NextRelease.chm", "Fieldlist.htm");
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
			ProjectClear();
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
			string orientation;
			string model;
			string fnumber;
			string flash;
			string expo;
			string lens;
			bool gps;

			foreach (string picPath in imgList)
			{
				//Debug.WriteLine("path: " + picPath);
				Util.CheckExif(out orientation, out model, out fnumber, out flash, out expo, out lens, out gps, picPath);
				if (dicModel.ContainsKey(model)){
					dicModel[model] +=1;
				}
				else {
					dicModel[model] =1;
				}

				if (dicFNum.ContainsKey(fnumber)) dicFNum[fnumber] +=1;
				else dicFNum[fnumber] =1;
				if (dicFlash.ContainsKey(flash)) dicFlash[flash] +=1;
				else dicFlash[flash] =1;
				if (dicExpo.ContainsKey(expo)) dicExpo[expo] +=1;
				else dicExpo[expo] =1;

				if (dicLens.ContainsKey(lens)) dicLens[lens] +=1;
				else dicLens[lens] =1;
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
			foreach (KeyValuePair<string, int> dex in dicExpo)
			{
				Debug.WriteLine("Expo: " + dex.Key + " " + dex.Value);
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
		}


	}  // end frmProject
}
