/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
File name:     StatusMainEvent.cs
Description:   event for main status bar 
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

namespace Next_View
{
	/// <summary>
	/// Description of StatusMainEvent.
	/// </summary>
	public class SetStatusMainEventArgs : EventArgs
	{
		public string NewValue;
		public SetStatusMainEventArgs(string value)
			: base()
		{
			this.NewValue = value;
		}
	}
	
	public class SetSizeEventArgs : EventArgs
	{
		public int nWidth;
		public int nHeight;
		public SetSizeEventArgs(int w, int h)
			: base()
		{
			this.nWidth = w;
			this.nHeight = h;
		}
	}
	
}
