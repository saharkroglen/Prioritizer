namespace Prioritizer.Forms
{
    partial class ExitForUpgrade
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExitForUpgrade));
            this.label1 = new System.Windows.Forms.Label();
            this.btnExitAndLaunchSetup = new DevExpress.XtraEditors.SimpleButton();
            this.btnDownloadSetup = new DevExpress.XtraEditors.SimpleButton();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(12, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(306, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "New Version of Prioritizer is Available";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnExitAndLaunchSetup
            // 
            this.btnExitAndLaunchSetup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExitAndLaunchSetup.Enabled = false;
            this.btnExitAndLaunchSetup.Location = new System.Drawing.Point(209, 152);
            this.btnExitAndLaunchSetup.Name = "btnExitAndLaunchSetup";
            this.btnExitAndLaunchSetup.Size = new System.Drawing.Size(122, 29);
            this.btnExitAndLaunchSetup.TabIndex = 2;
            this.btnExitAndLaunchSetup.Text = "Exit and Launch Setup";
            this.btnExitAndLaunchSetup.Click += new System.EventHandler(this.btnExitAndLaunchSetup_Click);
            // 
            // btnDownloadSetup
            // 
            this.btnDownloadSetup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownloadSetup.Location = new System.Drawing.Point(84, 152);
            this.btnDownloadSetup.Name = "btnDownloadSetup";
            this.btnDownloadSetup.Size = new System.Drawing.Size(119, 29);
            this.btnDownloadSetup.TabIndex = 3;
            this.btnDownloadSetup.Text = "Download Setup";
            this.btnDownloadSetup.Click += new System.EventHandler(this.btnDownloadSetup_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(12, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(305, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Note: Your work has been automatically saved.";
            // 
            // ExitForUpgrade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 193);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDownloadSetup);
            this.Controls.Add(this.btnExitAndLaunchSetup);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ExitForUpgrade";
            this.Text = "Upgrade Notification";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExitForUpgrade_FormClosing);
            this.Load += new System.EventHandler(this.ExitForUpgrade_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton btnExitAndLaunchSetup;
        private DevExpress.XtraEditors.SimpleButton btnDownloadSetup;
        private System.Windows.Forms.Label label2;
    }
}