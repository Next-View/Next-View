/*
 * Created by SharpDevelop.
 * User: mschnell
 * Date: 02/10/2018
 * Time: 14:30
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Next_View
{
	partial class FullScreen
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.PictureBox fullBox;
		
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
			this.fullBox = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.fullBox)).BeginInit();
			this.SuspendLayout();
			// 
			// fullBox
			// 
			this.fullBox.BackColor = System.Drawing.Color.Black;
			this.fullBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fullBox.Location = new System.Drawing.Point(0, 0);
			this.fullBox.Margin = new System.Windows.Forms.Padding(2);
			this.fullBox.Name = "fullBox";
			this.fullBox.Size = new System.Drawing.Size(764, 423);
			this.fullBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.fullBox.TabIndex = 0;
			this.fullBox.TabStop = false;
			// 
			// FullScreen
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(764, 423);
			this.Controls.Add(this.fullBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "FullScreen";
			this.Text = "FullScreen";
			this.Load += new System.EventHandler(this.FullScreenLoad);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FullScreenKeyDown);
			((System.ComponentModel.ISupportInitialize)(this.fullBox)).EndInit();
			this.ResumeLayout(false);

		}
	}
}
