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
		private System.Windows.Forms.Button button1;

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
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listExif
			// 
			this.listExif.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.listExif.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.colTag,
			this.colValue});
			this.listExif.Location = new System.Drawing.Point(0, 0);
			this.listExif.Margin = new System.Windows.Forms.Padding(4);
			this.listExif.MultiSelect = false;
			this.listExif.Name = "listExif";
			this.listExif.Size = new System.Drawing.Size(337, 429);
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
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button1.Location = new System.Drawing.Point(0, 432);
			this.button1.Margin = new System.Windows.Forms.Padding(4);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(100, 28);
			this.button1.TabIndex = 1;
			this.button1.TabStop = false;
			this.button1.Text = "gps";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// ExifForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(337, 463);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.listExif);
			this.KeyPreview = true;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "ExifForm";
			this.Text = "ExifForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExifFormFormClosing);
			this.Load += new System.EventHandler(this.ExifFormLoad);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ExifFormKeyDown);
			this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.ExifFormPreviewKeyDown);
			this.ResumeLayout(false);

		}
	}
}
