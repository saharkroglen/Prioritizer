namespace Prioritizer2._0.Forms
{
    partial class MeetingListForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MeetingListGrid = new Telerik.WinControls.UI.RadGridView();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.MeetingListGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            this.SuspendLayout();
            // 
            // MeetingListGrid
            // 
            this.MeetingListGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MeetingListGrid.Location = new System.Drawing.Point(22, 43);
            this.MeetingListGrid.Name = "MeetingListGrid";
            this.MeetingListGrid.Size = new System.Drawing.Size(376, 277);
            this.MeetingListGrid.TabIndex = 0;
            this.MeetingListGrid.Text = "radGridView1";
            this.MeetingListGrid.ThemeName = "ControlDefault";
            this.MeetingListGrid.UserAddedRow += new Telerik.WinControls.UI.GridViewRowEventHandler(this.MeetingListGrid_UserAddedRow);
            this.MeetingListGrid.UserDeletingRow += new Telerik.WinControls.UI.GridViewRowCancelEventHandler(this.authorizationGrid_UserDeletingRow);
            this.MeetingListGrid.UserDeletedRow += new Telerik.WinControls.UI.GridViewRowEventHandler(this.authorizationGrid_UserDeletedRow);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(318, 330);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 24);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save And Exit";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // MeetingListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 366);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.MeetingListGrid);
            this.Name = "MeetingListForm";
            this.Text = "Meeting List";
            this.Load += new System.EventHandler(this.MeetingListForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MeetingListGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView MeetingListGrid;
        private Telerik.WinControls.UI.RadButton btnSave;
    }
}