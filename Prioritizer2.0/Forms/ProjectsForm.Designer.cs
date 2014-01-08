namespace Prioritizer2._0
{
    partial class projectsForm
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
            this.projectsGrid = new Telerik.WinControls.UI.RadGridView();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.projectsGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            this.SuspendLayout();
            // 
            // projectsGrid
            // 
            this.projectsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.projectsGrid.Location = new System.Drawing.Point(22, 43);
            this.projectsGrid.Name = "projectsGrid";
            this.projectsGrid.Size = new System.Drawing.Size(271, 277);
            this.projectsGrid.TabIndex = 0;
            this.projectsGrid.Text = "radGridView1";
            this.projectsGrid.ThemeName = "ControlDefault";
            this.projectsGrid.UserDeletingRow += new Telerik.WinControls.UI.GridViewRowCancelEventHandler(this.authorizationGrid_UserDeletingRow);
            this.projectsGrid.UserDeletedRow += new Telerik.WinControls.UI.GridViewRowEventHandler(this.authorizationGrid_UserDeletedRow);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(213, 330);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 24);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save And Exit";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // projectsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(315, 366);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.projectsGrid);
            this.Name = "projectsForm";
            this.Text = "projects Form";
            this.Load += new System.EventHandler(this.projectsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.projectsGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView projectsGrid;
        private Telerik.WinControls.UI.RadButton btnSave;
    }
}