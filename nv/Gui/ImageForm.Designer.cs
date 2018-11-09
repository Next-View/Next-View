/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
File name:     ProjectForm.Designer.cs
Description:   ProjectForm
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
	partial class frmImage
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
			this.popPath = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.picBox = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
			this.SuspendLayout();
			// 
			// popPath
			// 
			this.popPath.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.popPath.Name = "popPath";
			this.popPath.Size = new System.Drawing.Size(61, 4);
			// 
			// picBox
			// 
			this.picBox.BackColor = System.Drawing.Color.Black;
			this.picBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.picBox.Location = new System.Drawing.Point(0, 0);
			this.picBox.Margin = new System.Windows.Forms.Padding(2);
			this.picBox.Name = "picBox";
			this.picBox.Size = new System.Drawing.Size(727, 449);
			this.picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.picBox.TabIndex = 45;
			this.picBox.TabStop = false;
			// 
			// frmImage
			// 
			this.AllowDrop = true;
			this.AllowEndUserDocking = false;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDark;
			this.ClientSize = new System.Drawing.Size(727, 449);
			this.Controls.Add(this.picBox);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) 
			| WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) 
			| WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom) 
			| WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "frmImage";
			this.Text = "Next-View";
			this.Shown += new System.EventHandler(this.FrmImageShown);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FrmImageDragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FrmImageDragEnter);
			this.DragOver += new System.Windows.Forms.DragEventHandler(this.FrmImageDragOver);
			this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.FrmImageHelpRequested);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmImageKeyDown);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmImageKeyUp);
			this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.FrmImagePreviewKeyDown);
			((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
			this.ResumeLayout(false);

		}
		private System.Windows.Forms.ContextMenuStrip popPath;
		private System.Windows.Forms.PictureBox picBox;
	}
}
