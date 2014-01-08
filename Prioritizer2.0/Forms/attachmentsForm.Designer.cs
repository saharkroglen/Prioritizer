namespace Prioritizer2._0
{
    partial class attachmentsForm
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
            this.attachmentsGrid = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.attachmentsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // attachmentsGrid
            // 
            this.attachmentsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.attachmentsGrid.Location = new System.Drawing.Point(22, 43);
            // 
            // attachmentsGrid
            // 
            this.attachmentsGrid.MasterTemplate.AllowEditRow = false;
            this.attachmentsGrid.Name = "attachmentsGrid";
            this.attachmentsGrid.Size = new System.Drawing.Size(271, 277);
            this.attachmentsGrid.TabIndex = 0;
            this.attachmentsGrid.Text = "radGridView1";
            this.attachmentsGrid.ThemeName = "ControlDefault";
            this.attachmentsGrid.UserDeletingRow += new Telerik.WinControls.UI.GridViewRowCancelEventHandler(this.attachmentsGrid_UserDeletingRow);
            this.attachmentsGrid.UserDeletedRow += new Telerik.WinControls.UI.GridViewRowEventHandler(this.attachmentsGrid_UserDeletedRow);
            this.attachmentsGrid.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.attachmentsGrid_CellClick);
            // 
            // attachmentsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(315, 366);
            this.Controls.Add(this.attachmentsGrid);
            this.Name = "attachmentsForm";
            this.Text = "Attachments";
            this.Load += new System.EventHandler(this.attachmentsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.attachmentsGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView attachmentsGrid;
    }
}