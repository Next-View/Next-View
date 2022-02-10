/*
 * Created by SharpDevelop.
 * User: martin
 * Date: 13.01.2019
 * Time: 20:53
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Next_View
{
	partial class ExifForm0
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.TextBox textExif0;
		private System.Windows.Forms.Button cmdClip;

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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExifForm0));
			this.textExif0 = new System.Windows.Forms.TextBox();
			this.cmdClip = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textExif0
			// 
			this.textExif0.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.textExif0.Location = new System.Drawing.Point(1, 1);
			this.textExif0.Margin = new System.Windows.Forms.Padding(4);
			this.textExif0.Multiline = true;
			this.textExif0.Name = "textExif0";
			this.textExif0.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textExif0.Size = new System.Drawing.Size(895, 414);
			this.textExif0.TabIndex = 1;
			// 
			// cmdClip
			// 
			this.cmdClip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cmdClip.Location = new System.Drawing.Point(13, 423);
			this.cmdClip.Margin = new System.Windows.Forms.Padding(4);
			this.cmdClip.Name = "cmdClip";
			this.cmdClip.Size = new System.Drawing.Size(100, 28);
			this.cmdClip.TabIndex = 0;
			this.cmdClip.Text = "Clipboard";
			this.cmdClip.UseVisualStyleBackColor = true;
			this.cmdClip.Click += new System.EventHandler(this.CmdClipClick);
			// 
			// ExifForm0
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(900, 464);
			this.Controls.Add(this.cmdClip);
			this.Controls.Add(this.textExif0);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(600, 400);
			this.Name = "ExifForm0";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "ExifForm0";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExifForm0FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
