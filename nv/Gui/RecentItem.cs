/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
File name:     recentitem.cs
Description:   list of last 5 entry images in menu
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
using System.Drawing;  // Size
using System.Linq;   // ToList
using System.Windows.Forms;
using Microsoft.Win32;
using System.ComponentModel;


namespace	Next_View
{
	public class RecentItem : ToolStripMenuItem
	{
	
		int _varMaxItems = 5;
		List<string> _recentList = new List<string>();
		
		public RecentItem()
		{
			this.Enabled = false;
		}
	
		public event EventHandler ItemClick;
			
		[DefaultValue(5)]
		public int MaxItems
		{
			get { return _varMaxItems; }
			set { _varMaxItems = value; }
		}
		
		public void UpdateList()
		{
			DropDownItems.Clear();
			if (_recentList.Count == 0) {
				this.Enabled = false;
			}
			else {
				this.Enabled = true;
				int i = 0;
				foreach (string value in _recentList)
				{
					i++;
					var item = new ToolStripMenuItem();
					item.Name = i.ToString();
					item.Size = new Size(156, 22);
					item.Text = value;
					if (this.ItemClick == null) {
						item.Enabled = false;
					}
					else {
						item.Click += this.ItemClick;
					}
					this.DropDownItems.Add(item);
				}
			}
		}

		public void AddRecentItem(string filePath)
		{
			int rPos = _recentList.IndexOf(filePath);
			if(rPos > -1) {
				_recentList.RemoveAt(rPos);
			}
			_recentList.Insert(0, filePath);
			if (_recentList.Count > 5){
				do {    
					_recentList.RemoveAt(5);
				} while (_recentList.Count > 5);
			}
							
			UpdateList();
		}

		public void LoadList(string pathString)
		{
			_recentList = pathString.Split('\t').ToList();
			if (_recentList[_recentList.Count - 1] == ""){
				_recentList.RemoveAt(_recentList.Count - 1);                // last entry empty from default, delete
			}
		}

		public void StringList(ref string pathString)
		{
			pathString = string.Join("\t", _recentList);
		}
						
	}
}
