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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImage));
			this.popImage = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.popOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.popRename = new System.Windows.Forms.ToolStripMenuItem();
			this.popDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.popSearch = new System.Windows.Forms.ToolStripMenuItem();
			this.popStartEditor = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.popNext = new System.Windows.Forms.ToolStripMenuItem();
			this.popPrior = new System.Windows.Forms.ToolStripMenuItem();
			this.popRefresh = new System.Windows.Forms.ToolStripMenuItem();
			this.popFullscreen = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.popClose = new System.Windows.Forms.ToolStripMenuItem();
			this.picBox = new System.Windows.Forms.PictureBox();
			this.popImage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
			this.SuspendLayout();
			// 
			// popImage
			// 
			this.popImage.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.popImage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.popOpen,
			this.popRename,
			this.popDelete,
			this.toolStripSeparator1,
			this.popSearch,
			this.popStartEditor,
			this.toolStripSeparator2,
			this.popNext,
			this.popPrior,
			this.popRefresh,
			this.popFullscreen,
			this.toolStripSeparator3,
			this.popClose});
			this.popImage.Name = "popPath";
			this.popImage.Size = new System.Drawing.Size(195, 310);
			// 
			// popOpen
			// 
			this.popOpen.Image = ((System.Drawing.Image)(resources.GetObject("popOpen.Image")));
			this.popOpen.Name = "popOpen";
			this.popOpen.Size = new System.Drawing.Size(194, 26);
			this.popOpen.Text = "Open...";
			this.popOpen.Click += new System.EventHandler(this.PopOpenClick);
			// 
			// popRename
			// 
			this.popRename.Name = "popRename";
			this.popRename.ShortcutKeyDisplayString = "F2";
			this.popRename.Size = new System.Drawing.Size(194, 26);
			this.popRename.Text = "Rename...";
			this.popRename.Click += new System.EventHandler(this.PopRenameClick);
			// 
			// popDelete
			// 
			this.popDelete.Name = "popDelete";
			this.popDelete.ShortcutKeyDisplayString = "Del";
			this.popDelete.Size = new System.Drawing.Size(194, 26);
			this.popDelete.Text = "Delete";
			this.popDelete.Click += new System.EventHandler(this.PopDeleteClick);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(191, 6);
			// 
			// popSearch
			// 
			this.popSearch.Image = ((System.Drawing.Image)(resources.GetObject("popSearch.Image")));
			this.popSearch.Name = "popSearch";
			this.popSearch.Size = new System.Drawing.Size(194, 26);
			this.popSearch.Text = "Search...";
			this.popSearch.Click += new System.EventHandler(this.PopSearchClick);
			// 
			// popStartEditor
			// 
			this.popStartEditor.Name = "popStartEditor";
			this.popStartEditor.Size = new System.Drawing.Size(194, 26);
			this.popStartEditor.Text = "Start editor...";
			this.popStartEditor.Click += new System.EventHandler(this.PopStartEditorClick);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(191, 6);
			// 
			// popNext
			// 
			this.popNext.Image = ((System.Drawing.Image)(resources.GetObject("popNext.Image")));
			this.popNext.Name = "popNext";
			this.popNext.ShortcutKeyDisplayString = "->";
			this.popNext.Size = new System.Drawing.Size(194, 26);
			this.popNext.Text = "Next image";
			this.popNext.Click += new System.EventHandler(this.PopNextClick);
			// 
			// popPrior
			// 
			this.popPrior.Image = ((System.Drawing.Image)(resources.GetObject("popPrior.Image")));
			this.popPrior.Name = "popPrior";
			this.popPrior.ShortcutKeyDisplayString = "<-";
			this.popPrior.Size = new System.Drawing.Size(194, 26);
			this.popPrior.Text = "Prior image";
			this.popPrior.Click += new System.EventHandler(this.PopPriorClick);
			// 
			// popRefresh
			// 
			this.popRefresh.Name = "popRefresh";
			this.popRefresh.ShortcutKeyDisplayString = "F5";
			this.popRefresh.Size = new System.Drawing.Size(194, 26);
			this.popRefresh.Text = "Refresh";
			this.popRefresh.Click += new System.EventHandler(this.PopRefreshClick);
			// 
			// popFullscreen
			// 
			this.popFullscreen.Name = "popFullscreen";
			this.popFullscreen.ShortcutKeyDisplayString = "Enter";
			this.popFullscreen.Size = new System.Drawing.Size(194, 26);
			this.popFullscreen.Text = "Full screen";
			this.popFullscreen.Click += new System.EventHandler(this.PopFullscreenClick);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(191, 6);
			// 
			// popClose
			// 
			this.popClose.Image = ((System.Drawing.Image)(resources.GetObject("popClose.Image")));
			this.popClose.Name = "popClose";
			this.popClose.Size = new System.Drawing.Size(194, 26);
			this.popClose.Text = "Exit ";
			this.popClose.Click += new System.EventHandler(this.PopCloseClick);
			// 
			// picBox
			// 
			this.picBox.BackColor = System.Drawing.Color.Black;
			this.picBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.picBox.Location = new System.Drawing.Point(0, 0);
			this.picBox.Margin = new System.Windows.Forms.Padding(2);
			this.picBox.Name = "picBox";
			this.picBox.Size = new System.Drawing.Size(545, 343);
			this.picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.picBox.TabIndex = 45;
			this.picBox.TabStop = false;
			// 
			// frmImage
			// 
			this.AllowDrop = true;
			this.AllowEndUserDocking = false;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDark;
			this.ClientSize = new System.Drawing.Size(545, 343);
			this.ContextMenuStrip = this.popImage;
			this.Controls.Add(this.picBox);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) 
			| WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) 
			| WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom) 
			| WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
			this.popImage.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
			this.ResumeLayout(false);

		}
		private System.Windows.Forms.ContextMenuStrip popImage;
		private System.Windows.Forms.PictureBox picBox;
		private System.Windows.Forms.ToolStripMenuItem popOpen;
		private System.Windows.Forms.ToolStripMenuItem popRename;
		private System.Windows.Forms.ToolStripMenuItem popDelete;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem popSearch;
		private System.Windows.Forms.ToolStripMenuItem popStartEditor;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem popNext;
		private System.Windows.Forms.ToolStripMenuItem popPrior;
		private System.Windows.Forms.ToolStripMenuItem popRefresh;
		private System.Windows.Forms.ToolStripMenuItem popFullscreen;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem popClose;
	}
}
