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
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
			this.cmdStart = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.listImg = new System.Windows.Forms.ListView();
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
			this.listScene = new System.Windows.Forms.ListView();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.listExpo = new System.Windows.Forms.ListView();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.listExift = new System.Windows.Forms.ListView();
			this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
			this.cmdShow = new System.Windows.Forms.Button();
			this.listToD = new System.Windows.Forms.ListView();
			this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
			this.lblGps = new System.Windows.Forms.Label();
			this.lblFlash = new System.Windows.Forms.Label();
			this.listFLen = new System.Windows.Forms.ListView();
			this.columnHeader13 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader14 = new System.Windows.Forms.ColumnHeader();
			this.lblInfo = new System.Windows.Forms.Label();
			this.chartImg = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.popPath.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.chartImg)).BeginInit();
			this.SuspendLayout();
			// 
			// cmdStart
			// 
			this.cmdStart.FlatAppearance.BorderSize = 4;
			this.cmdStart.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmdStart.Location = new System.Drawing.Point(700, 3);
			this.cmdStart.Margin = new System.Windows.Forms.Padding(2);
			this.cmdStart.Name = "cmdStart";
			this.cmdStart.Size = new System.Drawing.Size(78, 24);
			this.cmdStart.TabIndex = 43;
			this.cmdStart.Text = "&Start";
			this.cmdStart.UseVisualStyleBackColor = true;
			this.cmdStart.Click += new System.EventHandler(this.CmdStartClick);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 6);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(63, 18);
			this.label1.TabIndex = 47;
			this.label1.Text = "Path";
			// 
			// listImg
			// 
			this.listImg.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.colFiles});
			this.listImg.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.listImg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.listImg.FullRowSelect = true;
			this.listImg.GridLines = true;
			this.listImg.Location = new System.Drawing.Point(0, 597);
			this.listImg.Margin = new System.Windows.Forms.Padding(2);
			this.listImg.Name = "listImg";
			this.listImg.Size = new System.Drawing.Size(804, 124);
			this.listImg.TabIndex = 50;
			this.listImg.UseCompatibleStateImageBehavior = false;
			this.listImg.View = System.Windows.Forms.View.Details;
			this.listImg.DoubleClick += new System.EventHandler(this.ListProjectDoubleClick);
			// 
			// colFiles
			// 
			this.colFiles.Text = "Filename";
			this.colFiles.Width = 620;
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
			this.popPath.Size = new System.Drawing.Size(167, 28);
			// 
			// popPathRemove
			// 
			this.popPathRemove.Name = "popPathRemove";
			this.popPathRemove.Size = new System.Drawing.Size(166, 24);
			this.popPathRemove.Text = "Remove path";
			this.popPathRemove.Click += new System.EventHandler(this.PopPathRemoveClick);
			// 
			// edImgPath
			// 
			this.edImgPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.edImgPath.Location = new System.Drawing.Point(88, 6);
			this.edImgPath.Margin = new System.Windows.Forms.Padding(2);
			this.edImgPath.Name = "edImgPath";
			this.edImgPath.Size = new System.Drawing.Size(588, 24);
			this.edImgPath.TabIndex = 57;
			// 
			// listModel
			// 
			this.listModel.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeader1,
			this.columnHeader2});
			this.listModel.FullRowSelect = true;
			this.listModel.Location = new System.Drawing.Point(6, 176);
			this.listModel.Margin = new System.Windows.Forms.Padding(2);
			this.listModel.Name = "listModel";
			this.listModel.Size = new System.Drawing.Size(220, 100);
			this.listModel.TabIndex = 58;
			this.listModel.UseCompatibleStateImageBehavior = false;
			this.listModel.View = System.Windows.Forms.View.Details;
			this.listModel.DoubleClick += new System.EventHandler(this.ListModelDoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Model";
			this.columnHeader1.Width = 150;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Count";
			this.columnHeader2.Width = 40;
			// 
			// listLens
			// 
			this.listLens.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeader3,
			this.columnHeader4});
			this.listLens.FullRowSelect = true;
			this.listLens.Location = new System.Drawing.Point(6, 282);
			this.listLens.Margin = new System.Windows.Forms.Padding(2);
			this.listLens.Name = "listLens";
			this.listLens.Size = new System.Drawing.Size(220, 100);
			this.listLens.TabIndex = 59;
			this.listLens.UseCompatibleStateImageBehavior = false;
			this.listLens.View = System.Windows.Forms.View.Details;
			this.listLens.DoubleClick += new System.EventHandler(this.ListLensDoubleClick);
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Lens";
			this.columnHeader3.Width = 150;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Count";
			this.columnHeader4.Width = 40;
			// 
			// listScene
			// 
			this.listScene.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeader5,
			this.columnHeader6});
			this.listScene.FullRowSelect = true;
			this.listScene.Location = new System.Drawing.Point(230, 282);
			this.listScene.Margin = new System.Windows.Forms.Padding(2);
			this.listScene.Name = "listScene";
			this.listScene.Size = new System.Drawing.Size(220, 100);
			this.listScene.TabIndex = 60;
			this.listScene.UseCompatibleStateImageBehavior = false;
			this.listScene.View = System.Windows.Forms.View.Details;
			this.listScene.DoubleClick += new System.EventHandler(this.ListSceneDoubleClick);
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Scene";
			this.columnHeader5.Width = 150;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Count";
			this.columnHeader6.Width = 40;
			// 
			// listExpo
			// 
			this.listExpo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeader7,
			this.columnHeader8});
			this.listExpo.FullRowSelect = true;
			this.listExpo.Location = new System.Drawing.Point(230, 176);
			this.listExpo.Margin = new System.Windows.Forms.Padding(2);
			this.listExpo.Name = "listExpo";
			this.listExpo.Size = new System.Drawing.Size(220, 100);
			this.listExpo.TabIndex = 61;
			this.listExpo.UseCompatibleStateImageBehavior = false;
			this.listExpo.View = System.Windows.Forms.View.Details;
			this.listExpo.DoubleClick += new System.EventHandler(this.ListExpoDoubleClick);
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Exposition";
			this.columnHeader7.Width = 150;
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "Count";
			this.columnHeader8.Width = 40;
			// 
			// listExift
			// 
			this.listExift.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeader9,
			this.columnHeader10});
			this.listExift.FullRowSelect = true;
			this.listExift.Location = new System.Drawing.Point(6, 73);
			this.listExift.Margin = new System.Windows.Forms.Padding(2);
			this.listExift.Name = "listExift";
			this.listExift.Size = new System.Drawing.Size(220, 100);
			this.listExift.TabIndex = 62;
			this.listExift.UseCompatibleStateImageBehavior = false;
			this.listExift.View = System.Windows.Forms.View.Details;
			this.listExift.DoubleClick += new System.EventHandler(this.ListExiftDoubleClick);
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "Exif";
			this.columnHeader9.Width = 150;
			// 
			// columnHeader10
			// 
			this.columnHeader10.Text = "Count";
			this.columnHeader10.Width = 40;
			// 
			// cmdShow
			// 
			this.cmdShow.FlatAppearance.BorderSize = 4;
			this.cmdShow.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmdShow.Location = new System.Drawing.Point(700, 73);
			this.cmdShow.Margin = new System.Windows.Forms.Padding(2);
			this.cmdShow.Name = "cmdShow";
			this.cmdShow.Size = new System.Drawing.Size(78, 23);
			this.cmdShow.TabIndex = 63;
			this.cmdShow.Text = "&Show";
			this.cmdShow.UseVisualStyleBackColor = true;
			this.cmdShow.Click += new System.EventHandler(this.CmdShowClick);
			// 
			// listToD
			// 
			this.listToD.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeader11,
			this.columnHeader12});
			this.listToD.FullRowSelect = true;
			this.listToD.Location = new System.Drawing.Point(458, 73);
			this.listToD.Margin = new System.Windows.Forms.Padding(2);
			this.listToD.Name = "listToD";
			this.listToD.Size = new System.Drawing.Size(220, 100);
			this.listToD.TabIndex = 64;
			this.listToD.UseCompatibleStateImageBehavior = false;
			this.listToD.View = System.Windows.Forms.View.Details;
			this.listToD.DoubleClick += new System.EventHandler(this.ListToDDoubleClick);
			// 
			// columnHeader11
			// 
			this.columnHeader11.Text = "Time";
			this.columnHeader11.Width = 150;
			// 
			// columnHeader12
			// 
			this.columnHeader12.Text = "Count";
			this.columnHeader12.Width = 40;
			// 
			// lblGps
			// 
			this.lblGps.BackColor = System.Drawing.SystemColors.Window;
			this.lblGps.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblGps.Location = new System.Drawing.Point(459, 187);
			this.lblGps.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lblGps.Name = "lblGps";
			this.lblGps.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
			this.lblGps.Size = new System.Drawing.Size(220, 20);
			this.lblGps.TabIndex = 67;
			this.lblGps.Text = "GPS:";
			this.lblGps.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblGps.DoubleClick += new System.EventHandler(this.LblGpsDoubleClick);
			// 
			// lblFlash
			// 
			this.lblFlash.BackColor = System.Drawing.SystemColors.Window;
			this.lblFlash.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblFlash.Location = new System.Drawing.Point(459, 246);
			this.lblFlash.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lblFlash.Name = "lblFlash";
			this.lblFlash.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
			this.lblFlash.Size = new System.Drawing.Size(220, 20);
			this.lblFlash.TabIndex = 68;
			this.lblFlash.Text = "Flash:";
			this.lblFlash.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblFlash.DoubleClick += new System.EventHandler(this.LblFlashDoubleClick);
			// 
			// listFLen
			// 
			this.listFLen.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeader13,
			this.columnHeader14});
			this.listFLen.FullRowSelect = true;
			this.listFLen.Location = new System.Drawing.Point(458, 282);
			this.listFLen.Margin = new System.Windows.Forms.Padding(2);
			this.listFLen.Name = "listFLen";
			this.listFLen.Size = new System.Drawing.Size(220, 100);
			this.listFLen.TabIndex = 69;
			this.listFLen.UseCompatibleStateImageBehavior = false;
			this.listFLen.View = System.Windows.Forms.View.Details;
			this.listFLen.DoubleClick += new System.EventHandler(this.ListFLenDoubleClick);
			// 
			// columnHeader13
			// 
			this.columnHeader13.Text = "Focus Length";
			this.columnHeader13.Width = 150;
			// 
			// columnHeader14
			// 
			this.columnHeader14.Text = "Count";
			this.columnHeader14.Width = 40;
			// 
			// lblInfo
			// 
			this.lblInfo.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblInfo.Location = new System.Drawing.Point(6, 37);
			this.lblInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.lblInfo.Name = "lblInfo";
			this.lblInfo.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
			this.lblInfo.Size = new System.Drawing.Size(670, 26);
			this.lblInfo.TabIndex = 70;
			this.lblInfo.Text = "Info:";
			this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// chartImg
			// 
			this.chartImg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(223)))), ((int)(((byte)(193)))));
			this.chartImg.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
			this.chartImg.BorderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(64)))), ((int)(((byte)(1)))));
			this.chartImg.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
			this.chartImg.BorderlineWidth = 2;
			this.chartImg.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.Emboss;
			chartArea1.Area3DStyle.Inclination = 15;
			chartArea1.Area3DStyle.IsRightAngleAxes = false;
			chartArea1.Area3DStyle.Perspective = 10;
			chartArea1.Area3DStyle.PointDepth = 200;
			chartArea1.Area3DStyle.Rotation = 10;
			chartArea1.Area3DStyle.WallWidth = 0;
			chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
			chartArea1.AxisX.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			chartArea1.AxisX.ScaleView.SizeType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Years;
			chartArea1.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
			chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
			chartArea1.AxisY.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			chartArea1.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			chartArea1.AxisY2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
			chartArea1.AxisY2.LabelStyle.Enabled = false;
			chartArea1.AxisY2.MajorGrid.Enabled = false;
			chartArea1.AxisY2.MajorTickMark.Enabled = false;
			chartArea1.BackColor = System.Drawing.Color.OldLace;
			chartArea1.BackSecondaryColor = System.Drawing.Color.White;
			chartArea1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			chartArea1.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
			chartArea1.Name = "Default";
			chartArea1.ShadowColor = System.Drawing.Color.Transparent;
			this.chartImg.ChartAreas.Add(chartArea1);
			this.chartImg.Location = new System.Drawing.Point(6, 399);
			this.chartImg.Margin = new System.Windows.Forms.Padding(2);
			this.chartImg.Name = "chartImg";
			this.chartImg.Size = new System.Drawing.Size(673, 194);
			this.chartImg.TabIndex = 71;
			this.chartImg.TabStop = false;
			title1.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Bold);
			title1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
			title1.Name = "Title1";
			title1.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			title1.ShadowOffset = 3;
			title1.Text = "Images per day";
			this.chartImg.Titles.Add(title1);
			this.chartImg.Customize += new System.EventHandler(this.ChartImgCustomize);
			this.chartImg.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ChartImgMouseDoubleClick);
			// 
			// ExifDash
			// 
			this.AcceptButton = this.cmdStart;
			this.AllowDrop = true;
			this.AllowEndUserDocking = false;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(804, 721);
			this.Controls.Add(this.chartImg);
			this.Controls.Add(this.lblInfo);
			this.Controls.Add(this.listFLen);
			this.Controls.Add(this.lblFlash);
			this.Controls.Add(this.lblGps);
			this.Controls.Add(this.listToD);
			this.Controls.Add(this.cmdShow);
			this.Controls.Add(this.listExift);
			this.Controls.Add(this.listExpo);
			this.Controls.Add(this.listScene);
			this.Controls.Add(this.listLens);
			this.Controls.Add(this.listModel);
			this.Controls.Add(this.edImgPath);
			this.Controls.Add(this.listImg);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cmdStart);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) 
			| WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) 
			| WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom) 
			| WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(2);
			this.MinimumSize = new System.Drawing.Size(480, 464);
			this.Name = "ExifDash";
			this.Text = "Exif dashboard";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExifDashFormClosing);
			this.Load += new System.EventHandler(this.ExifDashLoad);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.ExifDashDragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.ExifDashDragEnter);
			this.Enter += new System.EventHandler(this.ExifDashEnter);
			this.popPath.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.chartImg)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		private System.Windows.Forms.Button cmdStart;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListView listImg;
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
		private System.Windows.Forms.ListView listScene;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ListView listExpo;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ListView listExift;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.Button cmdShow;
		private System.Windows.Forms.ListView listToD;
		private System.Windows.Forms.ColumnHeader columnHeader11;
		private System.Windows.Forms.ColumnHeader columnHeader12;
		private System.Windows.Forms.Label lblGps;
		private System.Windows.Forms.Label lblFlash;
		private System.Windows.Forms.ListView listFLen;
		private System.Windows.Forms.ColumnHeader columnHeader13;
		private System.Windows.Forms.ColumnHeader columnHeader14;
		private System.Windows.Forms.Label lblInfo;
		private System.Windows.Forms.DataVisualization.Charting.Chart chartImg;
	}
}
