/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
File name:     cexif.cs
Description:   class exif data
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
	/// exif data for one image
	/// </summary>
	public class Exif
	{
		public int eType;
		public string eModel;
		public DateTime eDtOriginal;
		public int eTimeOfD;

		public string eExpotime;
		public string eFNumber;
		public string eFLength;
		public bool eFlash;
		public string eExposi;
		public string eLensmodel;
		public string eScene;

		public bool eGps;
		public string eFname;

		public Exif()
		{}

		public Exif(int type, string model, DateTime dtOriginal, int timeOfD,
		      string expotime, string fNumber, string fLength, bool flash, string exposi, string lensmodel, string scene,
					bool gps, string fname)
		{
			this.eType = type;
			this.eModel = model;
			this.eDtOriginal = dtOriginal;
			this.eTimeOfD = timeOfD;

			this.eExpotime = expotime;
			this.eFNumber = fNumber;
			this.eFLength = fLength;
			this.eFlash = flash;
			this.eExposi = exposi;
			this.eLensmodel = lensmodel;
			this.eScene = scene;

			this.eGps = gps;
			this.eFname = fname;
		}

	}
}
