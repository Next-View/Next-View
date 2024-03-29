﻿/*
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRename));
			this.cmdRenameOk = new System.Windows.Forms.Button();
			this.cmdRenameCancel = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.edFilename = new System.Windows.Forms.TextBox();
			this.edExt = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// cmdRenameOk
			// 
			this.cmdRenameOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmdRenameOk.Location = new System.Drawing.Point(344, 95);
			this.cmdRenameOk.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cmdRenameOk.Name = "cmdRenameOk";
			this.cmdRenameOk.Size = new System.Drawing.Size(71, 22);
			this.cmdRenameOk.TabIndex = 3;
			this.cmdRenameOk.Text = "&Ok";
			this.cmdRenameOk.UseVisualStyleBackColor = true;
			this.cmdRenameOk.Click += new System.EventHandler(this.CmdRenameOkClick);
			// 
			// cmdRenameCancel
			// 
			this.cmdRenameCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cmdRenameCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmdRenameCancel.Location = new System.Drawing.Point(30, 95);
			this.cmdRenameCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.cmdRenameCancel.Name = "cmdRenameCancel";
			this.cmdRenameCancel.Size = new System.Drawing.Size(84, 22);
			this.cmdRenameCancel.TabIndex = 2;
			this.cmdRenameCancel.Text = "&Cancel";
			this.cmdRenameCancel.UseVisualStyleBackColor = true;
			this.cmdRenameCancel.Click += new System.EventHandler(this.CmdRenameCancelClick);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(30, 21);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(158, 22);
			this.label1.TabIndex = 2;
			this.label1.Text = "New name:";
			// 
			// edFilename
			// 
			this.edFilename.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.edFilename.Location = new System.Drawing.Point(30, 53);
			this.edFilename.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.edFilename.Name = "edFilename";
			this.edFilename.Size = new System.Drawing.Size(273, 24);
			this.edFilename.TabIndex = 1;
			// 
			// edExt
			// 
			this.edExt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.edExt.Location = new System.Drawing.Point(329, 53);
			this.edExt.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.edExt.Name = "edExt";
			this.edExt.ReadOnly = true;
			this.edExt.Size = new System.Drawing.Size(88, 24);
			this.edExt.TabIndex = 0;
			this.edExt.TabStop = false;
			// 
			// frmRename
			// 
			this.AcceptButton = this.cmdRenameOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(450, 138);
			this.Controls.Add(this.edExt);
			this.Controls.Add(this.edFilename);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cmdRenameCancel);
			this.Controls.Add(this.cmdRenameOk);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(400, 160);
			this.Name = "frmRename";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Rename";
			this.Activated += new System.EventHandler(this.FrmRenameActivated);
			this.Deactivate += new System.EventHandler(this.FrmRenameDeactivate);
			this.Load += new System.EventHandler(this.FrmRenameLoad);
			this.Shown += new System.EventHandler(this.FrmRenameShown);
			this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.FrmRenameHelpRequested);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
