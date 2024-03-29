﻿/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
File name:     AboutForm.Designer.cs
Description:   AboutForm
Copyright:     Copyright (c) Martin A. Schnell, 2011
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

namespace Next_View
{
	partial class frmAbout
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.webBrowser1 = new System.Windows.Forms.WebBrowser();
			this.cmdOk = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// webBrowser1
			// 
			this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.webBrowser1.Location = new System.Drawing.Point(0, 0);
			this.webBrowser1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.webBrowser1.MinimumSize = new System.Drawing.Size(23, 20);
			this.webBrowser1.Name = "webBrowser1";
			this.webBrowser1.Size = new System.Drawing.Size(355, 267);
			this.webBrowser1.TabIndex = 0;
			this.webBrowser1.TabStop = false;
			this.webBrowser1.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.WebBrowser1Navigating);
			// 
			// cmdOk
			// 
			this.cmdOk.Location = new System.Drawing.Point(251, 296);
			this.cmdOk.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cmdOk.Name = "cmdOk";
			this.cmdOk.Size = new System.Drawing.Size(89, 22);
			this.cmdOk.TabIndex = 1;
			this.cmdOk.Text = "&OK";
			this.cmdOk.UseVisualStyleBackColor = true;
			this.cmdOk.Click += new System.EventHandler(this.CmdOkClick);
			// 
			// frmAbout
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(356, 326);
			this.Controls.Add(this.cmdOk);
			this.Controls.Add(this.webBrowser1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmAbout";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmAboutFormClosed);
			this.Load += new System.EventHandler(this.FrmAboutLoad);
			this.Shown += new System.EventHandler(this.frmAboutShown);
			this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.FrmAboutHelpRequested);
			this.ResumeLayout(false);

		}
		private System.Windows.Forms.Button cmdOk;
		private System.Windows.Forms.WebBrowser webBrowser1;
	}
}
