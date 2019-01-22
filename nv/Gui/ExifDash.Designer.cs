/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
File name:     exifdash.Designer.cs
Description:   exifdash
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

namespace Next_View
{
	partial class ExifDash
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
			this.components = new System.ComponentModel.Container();
			this.cmdStart = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.listProject = new System.Windows.Forms.ListView();
			this.colFiles = new System.Windows.Forms.ColumnHeader();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.popPath = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.popPathRemove = new System.Windows.Forms.ToolStripMenuItem();
			this.edImgPath = new System.Windows.Forms.TextBox();
			this.listModel = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.listLens = new System.Windows.Forms.ListView();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.popPath.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmdStart
			// 
			this.cmdStart.FlatAppearance.BorderSize = 4;
			this.cmdStart.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmdStart.Location = new System.Drawing.Point(729, 7);
			this.cmdStart.Margin = new System.Windows.Forms.Padding(2);
			this.cmdStart.Name = "cmdStart";
			this.cmdStart.Size = new System.Drawing.Size(93, 30);
			this.cmdStart.TabIndex = 43;
			this.cmdStart.Text = "&Start";
			this.cmdStart.UseVisualStyleBackColor = true;
			this.cmdStart.Click += new System.EventHandler(this.CmdStartClick);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(17, 15);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(92, 22);
			this.label1.TabIndex = 47;
			this.label1.Text = "Project";
			// 
			// listProject
			// 
			this.listProject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left)));
			this.listProject.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.colFiles});
			this.listProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.listProject.FullRowSelect = true;
			this.listProject.GridLines = true;
			this.listProject.Location = new System.Drawing.Point(0, 392);
			this.listProject.Margin = new System.Windows.Forms.Padding(2);
			this.listProject.Name = "listProject";
			this.listProject.Size = new System.Drawing.Size(986, 133);
			this.listProject.TabIndex = 50;
			this.listProject.UseCompatibleStateImageBehavior = false;
			this.listProject.View = System.Windows.Forms.View.Details;
			this.listProject.DoubleClick += new System.EventHandler(this.ListProjectDoubleClick);
			// 
			// colFiles
			// 
			this.colFiles.Text = "Filename";
			this.colFiles.Width = 420;
			// 
			// backgroundWorker1
			// 
			this.backgroundWorker1.WorkerReportsProgress = true;
			this.backgroundWorker1.WorkerSupportsCancellation = true;
			this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker1DoWork);
			this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker1ProgressChanged);
			this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker1RunWorkerCompleted);
			// 
			// popPath
			// 
			this.popPath.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.popPath.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.popPathRemove});
			this.popPath.Name = "popPath";
			this.popPath.Size = new System.Drawing.Size(145, 26);
			// 
			// popPathRemove
			// 
			this.popPathRemove.Name = "popPathRemove";
			this.popPathRemove.Size = new System.Drawing.Size(144, 22);
			this.popPathRemove.Text = "Remove path";
			this.popPathRemove.Click += new System.EventHandler(this.PopPathRemoveClick);
			// 
			// edImgPath
			// 
			this.edImgPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.edImgPath.Location = new System.Drawing.Point(99, 15);
			this.edImgPath.Margin = new System.Windows.Forms.Padding(2);
			this.edImgPath.Name = "edImgPath";
			this.edImgPath.Size = new System.Drawing.Size(601, 21);
			this.edImgPath.TabIndex = 57;
			// 
			// listModel
			// 
			this.listModel.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeader1,
			this.columnHeader2});
			this.listModel.Location = new System.Drawing.Point(38, 89);
			this.listModel.Name = "listModel";
			this.listModel.Size = new System.Drawing.Size(187, 137);
			this.listModel.TabIndex = 58;
			this.listModel.UseCompatibleStateImageBehavior = false;
			this.listModel.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Model";
			this.columnHeader1.Width = 120;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Count";
			// 
			// listLens
			// 
			this.listLens.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeader3,
			this.columnHeader4});
			this.listLens.Location = new System.Drawing.Point(38, 250);
			this.listLens.Name = "listLens";
			this.listLens.Size = new System.Drawing.Size(187, 137);
			this.listLens.TabIndex = 59;
			this.listLens.UseCompatibleStateImageBehavior = false;
			this.listLens.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Lens";
			this.columnHeader3.Width = 120;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Count";
			// 
			// ExifDash
			// 
			this.AcceptButton = this.cmdStart;
			this.AllowDrop = true;
			this.AllowEndUserDocking = false;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(993, 524);
			this.Controls.Add(this.listLens);
			this.Controls.Add(this.listModel);
			this.Controls.Add(this.edImgPath);
			this.Controls.Add(this.listProject);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cmdStart);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) 
			| WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) 
			| WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom) 
			| WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "ExifDash";
			this.Text = "Project";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmProjectFormClosing);
			this.Load += new System.EventHandler(this.frmProjectLoad);
			this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.FrmProjectHelpRequested);
			this.Enter += new System.EventHandler(this.frmProjectEnter);
			this.popPath.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		private System.Windows.Forms.Button cmdStart;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListView listProject;
		private System.Windows.Forms.ColumnHeader colFiles;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private System.Windows.Forms.ContextMenuStrip popPath;
		private System.Windows.Forms.ToolStripMenuItem popPathRemove;
		private System.Windows.Forms.TextBox edImgPath;
		private System.Windows.Forms.ListView listModel;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ListView listLens;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
	}
}
