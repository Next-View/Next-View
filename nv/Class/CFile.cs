/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
File name:     cfile.cs
Description:   class file  data
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

namespace Next_View
{
	/// <summary>
	/// exif data for one image
	/// </summary>
	public class ImgFile
	{
		public string fName;
		public DateTime fDate;
		public DateTime fDateOriginal;

		public ImgFile()
		{}

		public ImgFile(string fName, DateTime fDate, DateTime fDateOriginal)
		{
			this.fName = fName;
			this.fDate = fDate;
			this.fDateOriginal = fDateOriginal;
		}

	}
}
