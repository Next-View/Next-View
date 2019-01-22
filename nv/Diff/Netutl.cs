/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
File name:     netutl.cs
Description:   load web pages
Copyright:     Copyright (c) Martin A. Schnell, 2012
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
using System.Diagnostics;
using System.Text;
using System.Net;
using System.IO;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using Next_View.Properties;

namespace Next_View
{
	/// <summary>
	/// Description of Class1.
	/// </summary>
	public class Netutl
	{
		public Netutl()
		{
		}

		public string LoadPage(string loadUrl)
		{
			Debug.WriteLine("url: " + loadUrl);
			HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(loadUrl);
			myRequest.Method = "GET";
			string result = "";
			try
			{
				WebResponse myResponse = myRequest.GetResponse();
				StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
				result = sr.ReadToEnd();
				sr.Close();
				myResponse.Close();
				//System.IO.File.WriteAllText(@"M:\Documents\Test\html\test1.dat", result);
			}
			catch (WebException ex)
			{
				if (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null)
				{
					var resp = (HttpWebResponse) ex.Response;
					if (resp.StatusCode == HttpStatusCode.NotFound)
					{
						// Do something
					}
					else
					{
						// Do something else
					}
				}
				else
				{
					// Do something else
				}
			}
			return result;
		}

		public bool LoadHead(string loadUrl)
		{
			Debug.WriteLine("url head: " + loadUrl);
			HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(loadUrl);
			myRequest.Method = "HEAD";
			bool result = false;
			try
			{
				WebResponse myResponse = myRequest.GetResponse();
				StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
				string head = sr.ReadToEnd();
				Debug.WriteLine("head: " + head);
				sr.Close();
				myResponse.Close();
				result = true;
			}
			catch (WebException ex)
			{
				if (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null)
				{
					var resp = (HttpWebResponse) ex.Response;
					if (resp.StatusCode == HttpStatusCode.NotFound)
					{
						// Do something
					}
					else
					{
						// Do something else
					}
				}
				else
				{
					// Do something else
				}
			}
			return result;
		}

	}
}
