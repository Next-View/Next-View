/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
File name:     OptionForm.Designer.cs
Description:   OptionForm
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
	partial class frmOption
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
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
			"Jpg / Jpeg",
			"JPEG Format"}, 0);
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
			"Png",
			"Portable Network Graphics"}, 0);
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
			"Gif",
			"GIF Format"}, 0);
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOption));
			this.tabOptions = new System.Windows.Forms.TabControl();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			this.chkImageEditor = new System.Windows.Forms.CheckBox();
			this.cmdEditor = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.edEditor = new System.Windows.Forms.TextBox();
			this.tabExtensions = new System.Windows.Forms.TabPage();
			this.cmdExtAssign = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.listExtensions = new System.Windows.Forms.ListView();
			this.colExtension = new System.Windows.Forms.ColumnHeader();
			this.colDescr = new System.Windows.Forms.ColumnHeader();
			this.colCurrent = new System.Windows.Forms.ColumnHeader();
			this.cmdOk = new System.Windows.Forms.Button();
			this.cmdCancel = new System.Windows.Forms.Button();
			this.tabOptions.SuspendLayout();
			this.tabGeneral.SuspendLayout();
			this.tabExtensions.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabOptions
			// 
			this.tabOptions.Controls.Add(this.tabGeneral);
			this.tabOptions.Controls.Add(this.tabExtensions);
			this.tabOptions.Location = new System.Drawing.Point(0, 0);
			this.tabOptions.Name = "tabOptions";
			this.tabOptions.SelectedIndex = 0;
			this.tabOptions.Size = new System.Drawing.Size(565, 292);
			this.tabOptions.TabIndex = 0;
			// 
			// tabGeneral
			// 
			this.tabGeneral.Controls.Add(this.chkImageEditor);
			this.tabGeneral.Controls.Add(this.cmdEditor);
			this.tabGeneral.Controls.Add(this.label5);
			this.tabGeneral.Controls.Add(this.edEditor);
			this.tabGeneral.Location = new System.Drawing.Point(4, 22);
			this.tabGeneral.Name = "tabGeneral";
			this.tabGeneral.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
			this.tabGeneral.Size = new System.Drawing.Size(557, 266);
			this.tabGeneral.TabIndex = 0;
			this.tabGeneral.Text = "General";
			this.tabGeneral.UseVisualStyleBackColor = true;
			// 
			// chkImageEditor
			// 
			this.chkImageEditor.Location = new System.Drawing.Point(16, 63);
			this.chkImageEditor.Name = "chkImageEditor";
			this.chkImageEditor.Size = new System.Drawing.Size(426, 24);
			this.chkImageEditor.TabIndex = 3;
			this.chkImageEditor.Text = "Use this program to edit images";
			this.chkImageEditor.UseVisualStyleBackColor = true;
			// 
			// cmdEditor
			// 
			this.cmdEditor.Location = new System.Drawing.Point(504, 23);
			this.cmdEditor.Name = "cmdEditor";
			this.cmdEditor.Size = new System.Drawing.Size(39, 23);
			this.cmdEditor.TabIndex = 2;
			this.cmdEditor.Text = "...";
			this.cmdEditor.UseVisualStyleBackColor = true;
			this.cmdEditor.Click += new System.EventHandler(this.CmdEditorClick);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(16, 23);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(67, 23);
			this.label5.TabIndex = 1;
			this.label5.Text = "Image editor";
			// 
			// edEditor
			// 
			this.edEditor.Location = new System.Drawing.Point(89, 23);
			this.edEditor.Name = "edEditor";
			this.edEditor.Size = new System.Drawing.Size(409, 20);
			this.edEditor.TabIndex = 0;
			// 
			// tabExtensions
			// 
			this.tabExtensions.Controls.Add(this.cmdExtAssign);
			this.tabExtensions.Controls.Add(this.label6);
			this.tabExtensions.Controls.Add(this.listExtensions);
			this.tabExtensions.Location = new System.Drawing.Point(4, 22);
			this.tabExtensions.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.tabExtensions.Name = "tabExtensions";
			this.tabExtensions.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.tabExtensions.Size = new System.Drawing.Size(557, 266);
			this.tabExtensions.TabIndex = 2;
			this.tabExtensions.Text = "Extensions";
			this.tabExtensions.UseVisualStyleBackColor = true;
			// 
			// cmdExtAssign
			// 
			this.cmdExtAssign.Location = new System.Drawing.Point(9, 213);
			this.cmdExtAssign.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.cmdExtAssign.Name = "cmdExtAssign";
			this.cmdExtAssign.Size = new System.Drawing.Size(110, 28);
			this.cmdExtAssign.TabIndex = 7;
			this.cmdExtAssign.Text = "Assign";
			this.cmdExtAssign.UseVisualStyleBackColor = true;
			this.cmdExtAssign.Click += new System.EventHandler(this.CmdExtAssignClick);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(9, 173);
			this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(446, 19);
			this.label6.TabIndex = 6;
			this.label6.Text = "Associated file extensions";
			// 
			// listExtensions
			// 
			this.listExtensions.CheckBoxes = true;
			this.listExtensions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.colExtension,
			this.colDescr,
			this.colCurrent});
			this.listExtensions.Dock = System.Windows.Forms.DockStyle.Top;
			this.listExtensions.FullRowSelect = true;
			listViewItem1.StateImageIndex = 0;
			listViewItem2.StateImageIndex = 0;
			listViewItem3.StateImageIndex = 0;
			this.listExtensions.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
			listViewItem1,
			listViewItem2,
			listViewItem3});
			this.listExtensions.Location = new System.Drawing.Point(2, 2);
			this.listExtensions.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.listExtensions.Name = "listExtensions";
			this.listExtensions.Size = new System.Drawing.Size(553, 156);
			this.listExtensions.TabIndex = 5;
			this.listExtensions.UseCompatibleStateImageBehavior = false;
			this.listExtensions.View = System.Windows.Forms.View.Details;
			// 
			// colExtension
			// 
			this.colExtension.Text = "Extension";
			this.colExtension.Width = 120;
			// 
			// colDescr
			// 
			this.colDescr.Text = "Description";
			this.colDescr.Width = 260;
			// 
			// colCurrent
			// 
			this.colCurrent.Text = "Current program";
			this.colCurrent.Width = 260;
			// 
			// cmdOk
			// 
			this.cmdOk.Location = new System.Drawing.Point(480, 329);
			this.cmdOk.Name = "cmdOk";
			this.cmdOk.Size = new System.Drawing.Size(75, 23);
			this.cmdOk.TabIndex = 1;
			this.cmdOk.Text = "&OK";
			this.cmdOk.UseVisualStyleBackColor = true;
			this.cmdOk.Click += new System.EventHandler(this.CmdOkClick);
			// 
			// cmdCancel
			// 
			this.cmdCancel.Location = new System.Drawing.Point(10, 329);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdCancel.TabIndex = 2;
			this.cmdCancel.Text = "&Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.CmdCancelClick);
			// 
			// frmOption
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(565, 370);
			this.Controls.Add(this.cmdCancel);
			this.Controls.Add(this.cmdOk);
			this.Controls.Add(this.tabOptions);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmOption";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Options";
			this.Load += new System.EventHandler(this.frmOptionLoad);
			this.tabOptions.ResumeLayout(false);
			this.tabGeneral.ResumeLayout(false);
			this.tabGeneral.PerformLayout();
			this.tabExtensions.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		private System.Windows.Forms.CheckBox chkImageEditor;
		private System.Windows.Forms.TextBox edEditor;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button cmdEditor;
		private System.Windows.Forms.TabPage tabGeneral;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdOk;
		private System.Windows.Forms.TabControl tabOptions;
		private System.Windows.Forms.TabPage tabExtensions;
		private System.Windows.Forms.ListView listExtensions;
		private System.Windows.Forms.ColumnHeader colExtension;
		private System.Windows.Forms.ColumnHeader colDescr;
		private System.Windows.Forms.ColumnHeader colCurrent;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button cmdExtAssign;
	}
}
