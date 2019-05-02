/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
File name:     imglist.cs
Description:   list of images to view
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
using System.Collections.Generic;   // List
using System.Diagnostics;  // Debug
using System.IO;   // path
using System.Linq;   // order by
using System.Runtime.InteropServices;

namespace Next_View
{
	/// <summary>
	///
	/// </summary>
	public class ImgList
	{
		public List<ImgFile> _imList = new List<ImgFile>();
		int _picPos = 1;
		string _picDir = "";
		readonly string[] _validExtensions  = new [] {".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tif", ".ico", ".wmf", ".emf"};
		// no .lnk files

		List<string> _logList = new List<string>();
		string _lastPic = "";
		int _logPos = -1;
		int _logMax = -1;

		List<string> _markList = new List<string>();
		int _markPos = -1;

		public ImgList()
		{

		}

		public void ImgListOut(out List<ImgFile> pList)
		{
			pList = _imList;
		}

		public bool FileIsValid(string pPath)
		{
			string ext = Path.GetExtension(pPath).ToLower();
			return _validExtensions.Contains(ext);
		}

		//--------------------------  pic  ------------------------------------

		public void DirClear()
		{
			// called by: refresh, drop, searchForm
			_imList.Clear();
			_picDir = "";
		}

		public void DirPicAdd(string picPath)
		{
			_imList.Add(new ImgFile(picPath, DateTime.MinValue, DateTime.MinValue));
		}

		public int DirCount()
		{
			return _imList.Count;
		}

		public void DirScan(out int picCount, string picPath, bool allDirs)
		{
			// called by: PicScan - open, refesh, drop, main: formShow, recent
			string picDir = "";
			if (File.Exists(picPath)) {
				picDir = Path.GetDirectoryName(picPath);
			}
			else if (Directory.Exists(picPath)){
				picDir = picPath;
			}
			else if (Directory.Exists(Path.GetDirectoryName(picPath))){
				picDir = Path.GetDirectoryName(picPath);
			}
			if (_picDir !=  picDir) {
				_picDir = picDir;
				var iList = new List<string>();
				if (allDirs){
					iList = Directory.GetFiles(picDir, "*.*", SearchOption.AllDirectories)
										.Where(file => _validExtensions.Any(file.ToLower().EndsWith))
										.ToList();
				}
				else {
					iList = Directory.GetFiles(picDir)
										.Where(file => _validExtensions.Any(file.ToLower().EndsWith))
										.ToList();
				}
				FilenameComparer fc = new FilenameComparer();
				iList.Sort(fc);

				foreach(string fName in iList)
				{
					_imList.Add(new ImgFile(fName, DateTime.MinValue, DateTime.MinValue));
				}

			}
			else {
				Debug.WriteLine("no dir change");
			}
			picCount = _imList.Count();
			if (picCount == 0) _picDir = "";   // fix for re-scan
		}

		public void DirPosPath(ref int picPos, ref int picAll, string pPath)
		{
			// position of image in imageList
			_picPos = _imList.FindIndex(x => x.fName == pPath);   //.IndexOf( .IndexOf   (pPath);
			if(_picPos < 0) _picPos = 0;
			picPos = _picPos + 1;
			picAll = _imList.Count;
		}

		public bool DirPicNext(ref string pPath)
		{
			if (_imList.Count < 1) {
				return false;
			}
			_picPos++;
			if (_picPos >= _imList.Count)
			{
				_picPos = 0;
			}
			pPath = _imList[_picPos].fName;
			return true;
		}

		public bool DirPicSearchNext(string pSearch, ref string pPath)
		{
			if (_imList.Count < 1) {
				return false;
			}
			int pPos = _picPos;
			do
			{
				pPos++;
				if (pPos >= _imList.Count)
				{
					pPos = 0;
				}
				string sPath = _imList[pPos].fName;
				string sFileName = Path.GetFileName(sPath);
				if (sFileName.Contains(pSearch)){
					_picPos = pPos;
					pPath = sPath;
					return true;
				}
				//Debug.WriteLine("s pos: " + pPos + " " + _picPos);
			} while (pPos != _picPos);
			return false;
		}

		public bool DirPicSearchPrior(string pSearch, ref string pPath)
		{
			if (_imList.Count < 1) {
				return false;
			}
			int pPos = _picPos;
			do
			{
				pPos--;
				if (pPos < 0)
				{
					pPos = _imList.Count - 1;
				}
				string sPath = _imList[pPos].fName;
				string 	sFileName = Path.GetFileName(sPath);
				if (sFileName.Contains(pSearch)){
					_picPos = pPos;
					pPath = sPath;
					return true;
				}
			} while (pPos != _picPos);
			return false;
		}

		public bool DirPicPrior(ref string pPath)
		{
			if (_imList.Count < 1) {
				return false;
			}
			_picPos--;
			if (_picPos < 0 )
			{
				_picPos = _imList.Count - 1;
			}
			pPath = _imList[_picPos].fName;
			return true;
		}

		public bool DirPicFirst(ref string pPath)
		{
			if (_imList.Count < 1) {
				return false;
			}
			_picPos = 0;
			pPath = _imList[_picPos].fName;
			return true;
		}

		public bool DirPicLast(ref string pPath)
		{
			if (_imList.Count < 1) {
				return false;
			}
			_picPos = _imList.Count - 1;
			pPath = _imList[_picPos].fName;
			return true;
		}

		public bool DirPosCurrent(ref string pPath)
		{
			if (_imList.Count < 1) {
				return false;
			}
			pPath = _imList[_picPos].fName;
			return true;
		}

		public bool DirRefresh(ref string pPath)
		{
			if (_imList.Count < 1) {
				return false;
			}
			pPath = _imList[_picPos].fName;
			return true;
		}

		public bool DirPathPos(ref string pPath, int barPos)
		{
			// path for position
			if (barPos < 1 || barPos > _imList.Count){
				return false;
			}
			else {
				pPath = _imList[barPos - 1].fName;
				return true;
			}
		}

		//--------------------------  sort  ------------------------------------

		public void SortName( )
		{
			_imList = _imList.OrderBy(o => o.fName).ToList();
		}

		public void SortFDate( )
		{
			_imList = _imList.OrderBy(o => o.fDate).ToList();
		}

		public void SortExifDate( )
		{
			_imList = _imList.OrderBy(o => o.fDateOriginal).ToList();
		}

		//--------------------------  log  ------------------------------------

		public void LogPic(string pPath)
		{
			if (_lastPic != pPath)  {
				_lastPic = pPath;
				_logList.Add(pPath);
				_logPos++;
				_logMax++;
			}
		}

		public bool LogBack(ref string pPath)
		{
			if (_logPos > 0 )
			{
				_logPos--;
				pPath = _logList[_logPos];
				return true;
			}
			else return false;
		}

		public bool LogForward(ref string pPath)
		{
			if (_logPos < _logList.Count - 1)
			{
				_logPos++;
				pPath = _logList[_logPos];
				return true;
			}
			else return false;
		}

		public void LogPos(ref int picPos, ref int picAll)
		{
			picPos = _logPos + 1;
			picAll = _logMax + 1;
		}

		public void RenameListLog(string nameFrom, string nameTo)
		{
			int pPos = _imList.FindIndex(x => x.fName == nameFrom);
			if (pPos > -1){
				 _imList[pPos].fName = nameTo;
			}
			pPos = _logList.IndexOf(nameFrom);
			if (pPos > -1){
				 _logList[pPos] = nameTo;
			}
			RenameMarkList(nameFrom, nameTo);
		}

		public bool DeleteListLog(string nameDel, ref string nameNext)
		{
			_picPos = _imList.FindIndex(x => x.fName == nameDel);
			if(_picPos > -1) {
				_imList.RemoveAt(_picPos);
				_picPos--;
				if (_picPos < 0 )
				{
					_picPos = _imList.Count - 1;
				}
			}

			int pPos = _logList.IndexOf(nameDel);
			if (pPos > -1){
				_logList.RemoveAt(pPos);
				_logPos--;
				_logMax--;
			}
			MarkDelete(nameDel);

			if (_imList.Count > 0){
				DirPicNext(ref nameNext);
				return true;
			}
			else {
				return false;
			}
		}

		//--------------------------  temp mark  ------------------------------------

		public bool MarkDelete(string pPath)
		{
			int rPos = _markList.IndexOf(pPath);
			if(rPos > -1) {
				_markList.RemoveAt(rPos);
				return true;
			}
			else {
				return false;
			}
		}

		public bool MarkGo(ref string pPath)
		{
			if (_markList.Count == 0){
				return false;
			}
			_markPos++;
			if (_markPos > _markList.Count - 1){
				_markPos = 0;
			}

			pPath = _markList[_markPos];
			return true;
		}

		public void MarkPic(string pPath)
		{
			if (_markList.IndexOf(pPath) == -1){   // no dups
				_markList.Add(pPath);
			}
		}

		void RenameMarkList(string nameFrom, string nameTo)
		{
			int pPos = _markList.IndexOf(nameFrom);
			if (pPos > -1){
				 _markList[pPos] = nameTo;
			}
		}

	}  // end ImgList



}
