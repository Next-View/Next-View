/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
File name:     filenamecomparer.cs
Description:   windows compare for filenames
Copyright:     Copyright (c) Martin A. Schnell, 2019
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
using System.Collections.Generic;   // IComparer
using System.Runtime.InteropServices;

namespace Next_View
{
	/// <summary>
	/// Description of FilenameComparer.
	/// </summary>
	public class FilenameComparer: IComparer<string>
	// for imgList.DirScan, dash scan, doSearch
	// https://stackoverflow.com/questions/31538293/sorting-listfileinfo-in-natural-sorted-order
	{
		[DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
		static extern Int32 StrCmpLogical(String x, String y);

		public int Compare(string x, string y)
		{
			return StrCmpLogical(x, y);
		}
        
	}
	
	public static class SortN
	{
		[DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
		private static extern int StrCmpLogicalW(string lhs, string rhs);
	
		public static void SortNatural<T>(this List<T> self, Func<T, string> stringSelector)
		{
			self.Sort((lhs, rhs) => StrCmpLogicalW(stringSelector(lhs), stringSelector(rhs)));
		}
	}
	
}
