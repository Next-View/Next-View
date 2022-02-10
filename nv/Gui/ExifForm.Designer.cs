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
		private System.Windows.Forms.Button cmdGpsGoogle;
		private System.Windows.Forms.Button cmdGpsOpen;
		private System.Windows.Forms.Button cmdGpsWego;

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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExifForm));
			this.listExif = new System.Windows.Forms.ListView();
			this.colTag = new System.Windows.Forms.ColumnHeader();
			this.colValue = new System.Windows.Forms.ColumnHeader();
			this.popExif = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.popEdit = new System.Windows.Forms.ToolStripMenuItem();
			this.cmdGpsGoogle = new System.Windows.Forms.Button();
			this.cmdGpsOpen = new System.Windows.Forms.Button();
			this.cmdGpsWego = new System.Windows.Forms.Button();
			this.popExif.SuspendLayout();
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
			this.listExif.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.listExif.Location = new System.Drawing.Point(0, 0);
			this.listExif.Margin = new System.Windows.Forms.Padding(4);
			this.listExif.MultiSelect = false;
			this.listExif.Name = "listExif";
			this.listExif.Size = new System.Drawing.Size(337, 428);
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
			this.popExif.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.popExif.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.popEdit});
			this.popExif.Name = "popExif";
			this.popExif.Size = new System.Drawing.Size(114, 28);
			// 
			// popEdit
			// 
			this.popEdit.Name = "popEdit";
			this.popEdit.Size = new System.Drawing.Size(113, 24);
			this.popEdit.Text = "Edit...";
			this.popEdit.Click += new System.EventHandler(this.PopEditClick);
			// 
			// cmdGpsGoogle
			// 
			this.cmdGpsGoogle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cmdGpsGoogle.Location = new System.Drawing.Point(0, 432);
			this.cmdGpsGoogle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.cmdGpsGoogle.Name = "cmdGpsGoogle";
			this.cmdGpsGoogle.Size = new System.Drawing.Size(95, 29);
			this.cmdGpsGoogle.TabIndex = 1;
			this.cmdGpsGoogle.TabStop = false;
			this.cmdGpsGoogle.Text = "Google";
			this.cmdGpsGoogle.UseVisualStyleBackColor = true;
			this.cmdGpsGoogle.Click += new System.EventHandler(this.CmdGpsGoogleClick);
			// 
			// cmdGpsOpen
			// 
			this.cmdGpsOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cmdGpsOpen.Location = new System.Drawing.Point(100, 432);
			this.cmdGpsOpen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.cmdGpsOpen.Name = "cmdGpsOpen";
			this.cmdGpsOpen.Size = new System.Drawing.Size(95, 29);
			this.cmdGpsOpen.TabIndex = 2;
			this.cmdGpsOpen.TabStop = false;
			this.cmdGpsOpen.Text = "Open";
			this.cmdGpsOpen.UseVisualStyleBackColor = true;
			this.cmdGpsOpen.Click += new System.EventHandler(this.CmdGpsOpenClick);
			// 
			// cmdGpsWego
			// 
			this.cmdGpsWego.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cmdGpsWego.Location = new System.Drawing.Point(201, 432);
			this.cmdGpsWego.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.cmdGpsWego.Name = "cmdGpsWego";
			this.cmdGpsWego.Size = new System.Drawing.Size(95, 29);
			this.cmdGpsWego.TabIndex = 3;
			this.cmdGpsWego.TabStop = false;
			this.cmdGpsWego.Text = "Wego";
			this.cmdGpsWego.UseVisualStyleBackColor = true;
			this.cmdGpsWego.Click += new System.EventHandler(this.CmdGpsWegoClick);
			// 
			// ExifForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(337, 461);
			this.ContextMenuStrip = this.popExif;
			this.Controls.Add(this.cmdGpsWego);
			this.Controls.Add(this.cmdGpsOpen);
			this.Controls.Add(this.cmdGpsGoogle);
			this.Controls.Add(this.listExif);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(300, 500);
			this.Name = "ExifForm";
			this.Text = "Exif data";
			this.Activated += new System.EventHandler(this.ExifFormActivated);
			this.Deactivate += new System.EventHandler(this.ExifFormDeactivate);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExifFormFormClosing);
			this.Load += new System.EventHandler(this.ExifFormLoad);
			this.Shown += new System.EventHandler(this.ExifFormShown);
			this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.ExifFormHelpRequested);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ExifFormKeyDown);
			this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.ExifFormPreviewKeyDown);
			this.popExif.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	}
}
