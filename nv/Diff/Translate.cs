/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
File name:     translate.cs
Description:   Translate with NGettext
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
using System.IO;   // stream
using System.Globalization;   // CultureInfo
using NGettext;

namespace Next_View
{
	/// <summary>
	/// Description of Translate.
	/// </summary>
	public static class T
	{
		static Stream moFileStream; 
		static ICatalog catalog;
		static bool _catExist = false;

		public static void SetCatalog(string moPath)
		{
			if (File.Exists(moPath)){
				moFileStream = File.OpenRead(moPath); 
				catalog = new Catalog(moFileStream, new CultureInfo("de-de"));
			}
			else {
				_catExist = false;
			}
		}
				
		public static string _(string text)
		{
			if (_catExist){
				return catalog.GetString(text);
			}
			else {
				return text;
			}
		}
	}
}
