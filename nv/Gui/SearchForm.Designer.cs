/*
 * Created by SharpDevelop.
 * User: mschnell
 * Date: 02/11/2018
 * Time: 15:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Next_View
{
	partial class SearchForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdOk;
		private System.Windows.Forms.ListView listSearch;
		private System.Windows.Forms.ColumnHeader colFilename;
		private System.Windows.Forms.TextBox edSearchFor;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button cmdSearch;
		private System.Windows.Forms.CheckBox chkSubdir;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox edSearchIn;
		private System.Windows.Forms.ToolStripStatusLabel statusLabel2;
		
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
			this.cmdCancel = new System.Windows.Forms.Button();
			this.cmdOk = new System.Windows.Forms.Button();
			this.listSearch = new System.Windows.Forms.ListView();
			this.colFilename = new System.Windows.Forms.ColumnHeader();
			this.edSearchFor = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cmdSearch = new System.Windows.Forms.Button();
			this.chkSubdir = new System.Windows.Forms.CheckBox();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.label2 = new System.Windows.Forms.Label();
			this.edSearchIn = new System.Windows.Forms.TextBox();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cmdCancel.Location = new System.Drawing.Point(21, 267);
			this.cmdCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(75, 24);
			this.cmdCancel.TabIndex = 6;
			this.cmdCancel.Text = "&Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.CmdCancelClick);
			// 
			// cmdOk
			// 
			this.cmdOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cmdOk.Location = new System.Drawing.Point(518, 266);
			this.cmdOk.Name = "cmdOk";
			this.cmdOk.Size = new System.Drawing.Size(75, 24);
			this.cmdOk.TabIndex = 7;
			this.cmdOk.Text = "&OK";
			this.cmdOk.UseVisualStyleBackColor = true;
			this.cmdOk.Click += new System.EventHandler(this.CmdOkClick);
			// 
			// listSearch
			// 
			this.listSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left)));
			this.listSearch.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.colFilename});
			this.listSearch.Location = new System.Drawing.Point(0, 55);
			this.listSearch.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.listSearch.Name = "listSearch";
			this.listSearch.Size = new System.Drawing.Size(632, 195);
			this.listSearch.TabIndex = 5;
			this.listSearch.UseCompatibleStateImageBehavior = false;
			this.listSearch.View = System.Windows.Forms.View.Details;
			// 
			// colFilename
			// 
			this.colFilename.Text = "Filename";
			this.colFilename.Width = 640;
			// 
			// edSearchFor
			// 
			this.edSearchFor.Location = new System.Drawing.Point(117, 10);
			this.edSearchFor.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.edSearchFor.Name = "edSearchFor";
			this.edSearchFor.Size = new System.Drawing.Size(210, 20);
			this.edSearchFor.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(9, 12);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(75, 19);
			this.label1.TabIndex = 5;
			this.label1.Text = "Search for:";
			// 
			// cmdSearch
			// 
			this.cmdSearch.Location = new System.Drawing.Point(362, 10);
			this.cmdSearch.Name = "cmdSearch";
			this.cmdSearch.Size = new System.Drawing.Size(99, 21);
			this.cmdSearch.TabIndex = 2;
			this.cmdSearch.Text = "&Start search";
			this.cmdSearch.UseVisualStyleBackColor = true;
			this.cmdSearch.Click += new System.EventHandler(this.CmdSearchClick);
			// 
			// chkSubdir
			// 
			this.chkSubdir.Location = new System.Drawing.Point(488, 10);
			this.chkSubdir.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.chkSubdir.Name = "chkSubdir";
			this.chkSubdir.Size = new System.Drawing.Size(143, 20);
			this.chkSubdir.TabIndex = 3;
			this.chkSubdir.Text = "Search subdirectories";
			this.chkSubdir.UseVisualStyleBackColor = true;
			// 
			// statusStrip1
			// 
			this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolStripStatusLabel1,
			this.statusLabel2});
			this.statusStrip1.Location = new System.Drawing.Point(0, 290);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
			this.statusStrip1.Size = new System.Drawing.Size(631, 22);
			this.statusStrip1.TabIndex = 8;
			this.statusStrip1.Text = "statusSearch";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
			// 
			// statusLabel2
			// 
			this.statusLabel2.Name = "statusLabel2";
			this.statusLabel2.Size = new System.Drawing.Size(0, 17);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(9, 34);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(75, 19);
			this.label2.TabIndex = 9;
			this.label2.Text = "Search in:";
			// 
			// edSearchIn
			// 
			this.edSearchIn.Location = new System.Drawing.Point(117, 35);
			this.edSearchIn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.edSearchIn.Name = "edSearchIn";
			this.edSearchIn.Size = new System.Drawing.Size(345, 20);
			this.edSearchIn.TabIndex = 4;
			// 
			// SearchForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(631, 312);
			this.Controls.Add(this.edSearchIn);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.chkSubdir);
			this.Controls.Add(this.cmdSearch);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.edSearchFor);
			this.Controls.Add(this.listSearch);
			this.Controls.Add(this.cmdOk);
			this.Controls.Add(this.cmdCancel);
			this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.Name = "SearchForm";
			this.Text = "SearchForm";
			this.Shown += new System.EventHandler(this.SearchFormShown);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
