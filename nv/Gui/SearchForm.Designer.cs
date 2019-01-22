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
		private System.Windows.Forms.Button cmdUp;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox chkStartWith;
		
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchForm));
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
			this.cmdUp = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.chkStartWith = new System.Windows.Forms.CheckBox();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmdCancel
			// 
			this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmdCancel.Location = new System.Drawing.Point(25, 486);
			this.cmdCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.Size = new System.Drawing.Size(124, 37);
			this.cmdCancel.TabIndex = 7;
			this.cmdCancel.Text = "&Cancel";
			this.cmdCancel.UseVisualStyleBackColor = true;
			this.cmdCancel.Click += new System.EventHandler(this.CmdCancelClick);
			// 
			// cmdOk
			// 
			this.cmdOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cmdOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmdOk.Location = new System.Drawing.Point(905, 486);
			this.cmdOk.Margin = new System.Windows.Forms.Padding(5);
			this.cmdOk.Name = "cmdOk";
			this.cmdOk.Size = new System.Drawing.Size(133, 37);
			this.cmdOk.TabIndex = 8;
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
			this.listSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.listSearch.Location = new System.Drawing.Point(16, 96);
			this.listSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.listSearch.Name = "listSearch";
			this.listSearch.Size = new System.Drawing.Size(1044, 366);
			this.listSearch.TabIndex = 6;
			this.listSearch.UseCompatibleStateImageBehavior = false;
			this.listSearch.View = System.Windows.Forms.View.Details;
			this.listSearch.DoubleClick += new System.EventHandler(this.ListSearchDoubleClick);
			// 
			// colFilename
			// 
			this.colFilename.Text = "Filename";
			this.colFilename.Width = 640;
			// 
			// edSearchFor
			// 
			this.edSearchFor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.edSearchFor.Location = new System.Drawing.Point(156, 15);
			this.edSearchFor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.edSearchFor.Name = "edSearchFor";
			this.edSearchFor.Size = new System.Drawing.Size(369, 21);
			this.edSearchFor.TabIndex = 1;
			this.edSearchFor.Enter += new System.EventHandler(this.EdSearchForEnter);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(16, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(133, 28);
			this.label1.TabIndex = 5;
			this.label1.Text = "Search for:";
			// 
			// cmdSearch
			// 
			this.cmdSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmdSearch.Location = new System.Drawing.Point(465, 486);
			this.cmdSearch.Margin = new System.Windows.Forms.Padding(5);
			this.cmdSearch.Name = "cmdSearch";
			this.cmdSearch.Size = new System.Drawing.Size(176, 37);
			this.cmdSearch.TabIndex = 2;
			this.cmdSearch.Text = "&Start search";
			this.cmdSearch.UseVisualStyleBackColor = true;
			this.cmdSearch.Click += new System.EventHandler(this.CmdSearchClick);
			// 
			// chkSubdir
			// 
			this.chkSubdir.Checked = true;
			this.chkSubdir.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkSubdir.Location = new System.Drawing.Point(789, 10);
			this.chkSubdir.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.chkSubdir.Name = "chkSubdir";
			this.chkSubdir.Size = new System.Drawing.Size(291, 32);
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
			this.statusStrip1.Location = new System.Drawing.Point(0, 554);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Padding = new System.Windows.Forms.Padding(3, 0, 19, 0);
			this.statusStrip1.Size = new System.Drawing.Size(1084, 22);
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
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(16, 52);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(133, 28);
			this.label2.TabIndex = 9;
			this.label2.Text = "Search in:";
			// 
			// edSearchIn
			// 
			this.edSearchIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.edSearchIn.Location = new System.Drawing.Point(156, 49);
			this.edSearchIn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.edSearchIn.Name = "edSearchIn";
			this.edSearchIn.Size = new System.Drawing.Size(609, 21);
			this.edSearchIn.TabIndex = 4;
			this.edSearchIn.Enter += new System.EventHandler(this.EdSearchInEnter);
			// 
			// cmdUp
			// 
			this.cmdUp.Image = ((System.Drawing.Image)(resources.GetObject("cmdUp.Image")));
			this.cmdUp.Location = new System.Drawing.Point(787, 47);
			this.cmdUp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.cmdUp.Name = "cmdUp";
			this.cmdUp.Size = new System.Drawing.Size(45, 28);
			this.cmdUp.TabIndex = 5;
			this.cmdUp.Click += new System.EventHandler(this.CmdUpClick);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(860, 57);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(107, 22);
			this.label3.TabIndex = 10;
			this.label3.Text = "One dir up";
			// 
			// chkStartWith
			// 
			this.chkStartWith.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkStartWith.Location = new System.Drawing.Point(575, 10);
			this.chkStartWith.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.chkStartWith.Name = "chkStartWith";
			this.chkStartWith.Size = new System.Drawing.Size(191, 32);
			this.chkStartWith.TabIndex = 11;
			this.chkStartWith.Text = "Start with";
			this.chkStartWith.UseVisualStyleBackColor = true;
			// 
			// SearchForm
			// 
			this.AcceptButton = this.cmdSearch;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1084, 576);
			this.Controls.Add(this.chkStartWith);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.cmdUp);
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
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.Name = "SearchForm";
			this.Text = "Search files";
			this.Load += new System.EventHandler(this.SearchFormLoad);
			this.Shown += new System.EventHandler(this.SearchFormShown);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
