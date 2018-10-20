/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
File name:     nrtest1.cs
Description:   test cases for nunit
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
using System.Collections.Generic;
using System.IO;   // path
using NUnit.Framework;

namespace Next_View
{
	/// <summary>
	/// test cases
	//  nunit runner is at C:\temp\nu\NUnit-Gui-0.6.0
	/// </summary>
	///
	[TestFixture, Category("File")]
	public class nrs_test1
	{
		string testPath;
		[Test]
		public void LoadTest1()
		{
			int i = 1;
			Assert.AreEqual(i, 1);
			testPath = @"C:\Project\Next-View\nv-test\bin\Debug\testdata\";     // rel path does no longer work
			//string cDir = Directory.GetCurrentDirectory();
			bool testDirExist = Directory.Exists(testPath);
			Assert.IsTrue(testDirExist);
		}

		[Test]
		public void Test_01_Pas()
		{
			int source = 0;

			Assert.AreEqual(0, source);          // expected , was

		}




	}
}