/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
File name:     fileinfo.cs
Description:   file info function
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
using System.Runtime.InteropServices;

namespace Next_View
{
	/// <summary>
	/// Description of FileInfo.
	/// </summary>
	public static class FileInfo
	{

		[DllImport("shell32.dll", CharSet = CharSet.Auto)]
		static extern bool ShellExecuteEx(ref SHELLEXECUTEINFO lpExecInfo);

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct SHELLEXECUTEINFO
		{
	    public int cbSize;
	    public uint fMask;
	    public IntPtr hwnd;
	    [MarshalAs(UnmanagedType.LPTStr)]
	    public string lpVerb;
	    [MarshalAs(UnmanagedType.LPTStr)]
	    public string lpFile;
	    [MarshalAs(UnmanagedType.LPTStr)]
	    public string lpParameters;
	    [MarshalAs(UnmanagedType.LPTStr)]
	    public string lpDirectory;
	    public int nShow;
	    public IntPtr hInstApp;
	    public IntPtr lpIDList;
	    [MarshalAs(UnmanagedType.LPTStr)]
	    public string lpClass;
	    public IntPtr hkeyClass;
	    public uint dwHotKey;
	    public IntPtr hIcon;
	    public IntPtr hProcess;
		}

		private const int SW_SHOW = 5;
		private const uint SEE_MASK_INVOKEIDLIST = 12;

		public static bool ShowFileProperties(string Filename)
		{
		    SHELLEXECUTEINFO info = new SHELLEXECUTEINFO();
		    info.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(info);
		    info.lpVerb = "properties";
		    info.lpFile = Filename;
		    info.nShow = SW_SHOW;
		    info.fMask = SEE_MASK_INVOKEIDLIST;
		    return ShellExecuteEx(ref info);
		}

	}
}
