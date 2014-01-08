namespace Prioritizer2._0
{
    partial class AuthorizationForm
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
            this.authorizationGrid = new Telerik.WinControls.UI.RadGridView();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.authorizationGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            this.SuspendLayout();
            // 
            // authorizationGrid
            // 
            this.authorizationGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.authorizationGrid.Location = new System.Drawing.Point(22, 43);
            // 
            // authorizationGrid
            // 
            this.authorizationGrid.MasterTemplate.AllowEditRow = false;
            this.authorizationGrid.Name = "authorizationGrid";
            this.authorizationGrid.Size = new System.Drawing.Size(271, 277);
            this.authorizationGrid.TabIndex = 0;
            this.authorizationGrid.Text = "radGridView1";
            this.authorizationGrid.ThemeName = "ControlDefault";
            this.authorizationGrid.UserDeletingRow += new Telerik.WinControls.UI.GridViewRowCancelEventHandler(this.authorizationGrid_UserDeletingRow);
            this.authorizationGrid.UserDeletedRow += new Telerik.WinControls.UI.GridViewRowEventHandler(this.authorizationGrid_UserDeletedRow);
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
            // AuthorizationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(315, 366);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.authorizationGrid);
            this.Name = "AuthorizationForm";
            this.Text = "Authorization Form";
            this.Load += new System.EventHandler(this.AuthorizationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.authorizationGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView authorizationGrid;
        private Telerik.WinControls.UI.RadButton btnSave;
    }
}