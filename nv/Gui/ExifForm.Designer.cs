/*
 * Created by SharpDevelop.
 * User: martin
 * Date: 12.01.2019
 * Time: 21:56
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Next_View
{
	partial class ExifForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.ListView listExif;
		private System.Windows.Forms.ColumnHeader colTag;
		private System.Windows.Forms.ColumnHeader colValue;
		private System.Windows.Forms.ContextMenuStrip popExif;
		private System.Windows.Forms.ToolStripMenuItem popEdit;

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
			this.listExif = new System.Windows.Forms.ListView();
			this.colTag = new System.Windows.Forms.ColumnHeader();
			this.colValue = new System.Windows.Forms.ColumnHeader();
			this.popExif = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.popEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.popExif.SuspendLayout();
			this.SuspendLayout();
			// 
			// listExif
			// 
			this.listExif.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.colTag,
			this.colValue});
			this.listExif.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listExif.Location = new System.Drawing.Point(0, 0);
			this.listExif.MultiSelect = false;
			this.listExif.Name = "listExif";
			this.listExif.Size = new System.Drawing.Size(253, 376);
			this.listExif.TabIndex = 0;
			this.listExif.TabStop = false;
			this.listExif.UseCompatibleStateImageBehavior = false;
			this.listExif.View = System.Windows.Forms.View.Details;
			// 
			// colTag
			// 
			this.colTag.Text = "Tag";
			this.colTag.Width = 120;
			// 
			// colValue
			// 
			this.colValue.Text = "Value";
			this.colValue.Width = 200;
			// 
			// popExif
			// 
			this.popExif.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.popEdit});
			this.popExif.Name = "popExif";
			this.popExif.Size = new System.Drawing.Size(104, 26);
			// 
			// popEdit
			// 
			this.popEdit.Name = "popEdit";
			this.popEdit.Size = new System.Drawing.Size(103, 22);
			this.popEdit.Text = "Edit...";
			this.popEdit.Click += new System.EventHandler(this.PopEditClick);
			// 
			// ExifForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(253, 376);
			this.ContextMenuStrip = this.popExif;
			this.Controls.Add(this.listExif);
			this.KeyPreview = true;
			this.Name = "ExifForm";
			this.Text = "Exif data";
			this.Activated += new System.EventHandler(this.ExifFormActivated);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExifFormFormClosing);
			this.Load += new System.EventHandler(this.ExifFormLoad);
			this.Shown += new System.EventHandler(this.ExifFormShown);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ExifFormKeyDown);
			this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.ExifFormPreviewKeyDown);
			this.popExif.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	}
}
