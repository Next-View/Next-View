/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
File name:     searchform.cs
Description:   Search form for next-view
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
using System.Collections.Generic;		// List
using System.Diagnostics;  // Debug
using System.IO;	 //	directory
using System.Linq;	 //	Where
using System.Text.RegularExpressions;  // Regex
using System.Windows.Forms;
using Next_View.Properties;


namespace Next_View
{
	/// <summary>
	/// Description of SearchForm.
	/// </summary>
	public partial class SearchForm : Form
	{
		ImgList _il;
		string _pDir;
		public bool _SearchReturn {get;set;}
		public string _lastSearchStr {get;set;}
		public string _selImg {get;set;}
		
		List<string> _searchList2 = new List<string>();

        public event HandleKeyChange  KeyChanged;
        		
		public SearchForm(string pPath, string lastSearchStr, ImgList il)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			_il = il;
			if (File.Exists(pPath)){
				_pDir = Path.GetDirectoryName(pPath);
			}
			_lastSearchStr = lastSearchStr;
			_SearchReturn = false;
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}

		// ------------------------------		events form	----------------------------------------------------------

		void SearchFormLoad(object sender, EventArgs e)
		{
			TranslateSearchForm( );
		}
				
		void SearchFormShown(object sender, EventArgs e)
		{
			edSearchIn.Text = _pDir;
			edSearchFor.Text = _lastSearchStr;
			AcceptButton = cmdSearch;
		}

		void SearchFormActivated(object sender, EventArgs e)
		{
	        SetKeyChange(82, true, false);    // R efresh	
		}
		
		void SearchFormDeactivate(object sender, EventArgs e)
		{
		    if (Settings.Default.HideImg)
		    {
    	        if (frmRename.ActiveForm == null)   //  app inactive
    	        {
    	            SetKeyChange(68, true, false);     // D ark
    	        }
    	    }
		}
		
		void EdSearchForEnter(object sender, EventArgs e)
		{
			AcceptButton = cmdSearch;
		}
		
		void EdSearchInEnter(object sender, EventArgs e)
		{
			AcceptButton = cmdSearch;
		}

				
		void ListSearchDoubleClick(object sender, EventArgs e)
		{
			this.cmdOk.PerformClick();
		}
		
		// ------------------------------		buttons	----------------------------------------------------------
		
		void CmdCancelClick(object sender, EventArgs e)
		{
			this.Close();
		}

		void CmdOkClick(object sender, EventArgs e)
		{
			if (_searchList2.Count > 0){
				_il.DirClear();
				foreach(string fName in _searchList2)
				{
					_il._imList.Add(new ImgFile(fName, DateTime.MinValue, DateTime.MinValue));
				}
									
				if (listSearch.SelectedItems.Count > 0){
					_selImg = listSearch.SelectedItems[0].Text;
				}
				else {
					_selImg = "";
				}
				_SearchReturn = true;
			}
			this.Close();
		}

		void CmdSearchClick(object sender, EventArgs e)
		{
			_lastSearchStr = edSearchFor.Text;
			string sFor = edSearchFor.Text;
			_pDir = edSearchIn.Text;
			if (sFor == ""){
				MessageBox.Show (T._("Search for something") , T._("Error"),
						MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (_pDir == ""){
				MessageBox.Show (T._("Search directory must not be empty") , T._("Error"),
						MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (!Directory.Exists(_pDir)) { 
				MessageBox.Show (T._("Search directory does not exist") , T._("Error"),
						MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else {		
				listSearch.Items.Clear();
				_searchList2.Clear();
				int fCount = 0;
				this.statusLabel2.Text = "";
				DoSearch(_pDir, sFor, chkSubdir.Checked, ref fCount);
				if (fCount > 0){
					AcceptButton = cmdOk;
				}
				statusLabel2.Text = T._("Files found") + ": " + fCount.ToString();
			}
		}

		void CmdUpClick(object sender, EventArgs e)
		{
			string sDir = edSearchIn.Text;
			if (Directory.Exists(sDir)){
				var upperDir = Directory.GetParent(sDir);
				if (upperDir != null) {
					edSearchIn.Text = Directory.GetParent(sDir).FullName;
				}
			}
		}
		
		// ------------------------------		functions 	----------------------------------------------------------
		
		public void	DoSearch(string startDir, string sFor, bool subDirs, ref int findCount)
		{
			var searchList1 = new List<string>();
			string[]	validExtensions	=	new	[] {".jpg", ".jpeg", ".jfif", ".png", ".gif", ".bmp", ".tif", ".ico", ".wmf", ".emf"};
			if (subDirs){
				searchList1 = Directory.GetFiles(startDir, "*.*", SearchOption.AllDirectories)
									.Where(file	=> validExtensions.Any(file.ToLower().EndsWith))
									.ToList();
			}
			else {
				searchList1	=	Directory.GetFiles(startDir)
									.Where(file	=> validExtensions.Any(file.ToLower().EndsWith))
									.ToList();
			}
			var fc = new FilenameComparer();
			searchList1.Sort(fc);
			
			string sForReg = WildcardToRegex(sFor);			
			foreach (string picPath in searchList1)
			{
				string fName = Path.GetFileName(picPath);
				if (Regex.IsMatch(fName, sForReg, RegexOptions.IgnoreCase)){
				//int strPos = Path.GetFileName(picPath).IndexOf(sFor);
				//if (strPos > -1){
					ListViewItem item = this.listSearch.Items.Add(picPath);
					item.ImageIndex = 0;
					_searchList2.Add(picPath);
				}
			}
			findCount = _searchList2.Count;
		}

		public string WildcardToRegex(string pattern)
		{
			string regReturn;
			if (this.chkStartWith.Checked){
				regReturn = "^" + Regex.Escape(pattern)   
					.Replace(@"\*", ".*");
			}
			else {
				regReturn = Regex.Escape(pattern)   
					.Replace(@"\*", ".*");				
			}
			return regReturn;
		}

		public void TranslateSearchForm( )
		{
			Text = T._("Search files");
			cmdOk.Text = T._("&OK");
			cmdCancel.Text = T._("&Cancel"); 
			colFilename.Text = T._("Filename");
			label1.Text = T._("Search for:");
			cmdSearch.Text = T._("&Start search");
			chkSubdir.Text = T._("Search subdirectories");
			label2.Text = T._("Search in:");
			label3.Text = T._("One dir up");
			chkStartWith.Text = T._("Start with");
		}
		
		void SearchFormHelpRequested(object sender, HelpEventArgs hlpevent)
		{
		    var c = this.ActiveControl;
            if(c!=null)
                MessageBox.Show(c.Name);
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

		
	}
}
