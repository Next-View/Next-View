/*
 * Created by SharpDevelop.
 * User: martin
 * Date: 05.10.2018
 * Time: 22:49
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Next_View
{
	partial class frmRename
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button cmdRenameOk;
		private System.Windows.Forms.Button cmdRenameCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox edFilename;
		private System.Windows.Forms.TextBox edExt;

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
			this.cmdRenameOk = new System.Windows.Forms.Button();
			this.cmdRenameCancel = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.edFilename = new System.Windows.Forms.TextBox();
			this.edExt = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// cmdRenameOk
			// 
			this.cmdRenameOk.Location = new System.Drawing.Point(341, 97);
			this.cmdRenameOk.Name = "cmdRenameOk";
			this.cmdRenameOk.Size = new System.Drawing.Size(75, 23);
			this.cmdRenameOk.TabIndex = 3;
			this.cmdRenameOk.Text = "&Ok";
			this.cmdRenameOk.UseVisualStyleBackColor = true;
			this.cmdRenameOk.Click += new System.EventHandler(this.CmdRenameOkClick);
			// 
			// cmdRenameCancel
			// 
			this.cmdRenameCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdRenameCancel.Location = new System.Drawing.Point(26, 97);
			this.cmdRenameCancel.Name = "cmdRenameCancel";
			this.cmdRenameCancel.Size = new System.Drawing.Size(75, 23);
			this.cmdRenameCancel.TabIndex = 2;
			this.cmdRenameCancel.Text = "&Cancel";
			this.cmdRenameCancel.UseVisualStyleBackColor = true;
			this.cmdRenameCancel.Click += new System.EventHandler(this.CmdRenameCancelClick);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(26, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 23);
			this.label1.TabIndex = 2;
			this.label1.Text = "New name:";
			// 
			// edFilename
			// 
			this.edFilename.Location = new System.Drawing.Point(26, 54);
			this.edFilename.Name = "edFilename";
			this.edFilename.Size = new System.Drawing.Size(288, 20);
			this.edFilename.TabIndex = 1;
			// 
			// edExt
			// 
			this.edExt.Location = new System.Drawing.Point(341, 54);
			this.edExt.Name = "edExt";
			this.edExt.ReadOnly = true;
			this.edExt.Size = new System.Drawing.Size(75, 20);
			this.edExt.TabIndex = 0;
			this.edExt.TabStop = false;
			// 
			// frmRename
			// 
			this.AcceptButton = this.cmdRenameOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(469, 140);
			this.Controls.Add(this.edExt);
			this.Controls.Add(this.edFilename);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cmdRenameCancel);
			this.Controls.Add(this.cmdRenameOk);
			this.Name = "frmRename";
			this.Text = "Rename";
			this.Shown += new System.EventHandler(this.FrmRenameShown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
