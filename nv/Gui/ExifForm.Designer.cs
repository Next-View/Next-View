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
			this.listExif = new System.Windows.Forms.ListView();
			this.colTag = new System.Windows.Forms.ColumnHeader();
			this.colValue = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// listExif
			// 
			this.listExif.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.colTag,
			this.colValue});
			this.listExif.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listExif.Location = new System.Drawing.Point(0, 0);
			this.listExif.Margin = new System.Windows.Forms.Padding(4);
			this.listExif.Name = "listExif";
			this.listExif.Size = new System.Drawing.Size(337, 453);
			this.listExif.TabIndex = 0;
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
			// ExifForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(337, 453);
			this.Controls.Add(this.listExif);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "ExifForm";
			this.Text = "ExifForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExifFormFormClosing);
			this.Load += new System.EventHandler(this.ExifFormLoad);
			this.ResumeLayout(false);

		}
	}
}
