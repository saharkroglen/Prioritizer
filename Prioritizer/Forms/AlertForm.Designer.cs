namespace Prioritizer.Forms
{
    partial class AlertForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlertForm));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.chkEmail = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.chkActivate = new DevExpress.XtraEditors.CheckEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnOpenItem = new DevExpress.XtraEditors.SimpleButton();
            this.btnDismiss = new DevExpress.XtraEditors.SimpleButton();
            this.cboSnooze = new DevExpress.XtraEditors.LookUpEdit();
            this.btnSnooze = new DevExpress.XtraEditors.SimpleButton();
            this.lblSnooze = new DevExpress.XtraEditors.LabelControl();
            this.dtmAlert = new DevExpress.XtraEditors.DateEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.chkEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkActivate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboSnooze.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmAlert.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmAlert.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(9, 5);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(59, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Alert Set to:";
            // 
            // chkEmail
            // 
            this.chkEmail.Location = new System.Drawing.Point(91, 35);
            this.chkEmail.Name = "chkEmail";
            this.chkEmail.Properties.Caption = "";
            this.chkEmail.Size = new System.Drawing.Size(94, 19);
            this.chkEmail.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(9, 38);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(55, 13);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Send Email:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(16, 12);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(70, 13);
            this.labelControl3.TabIndex = 5;
            this.labelControl3.Text = "Activate Alert:";
            // 
            // chkActivate
            // 
            this.chkActivate.EditValue = true;
            this.chkActivate.Location = new System.Drawing.Point(98, 10);
            this.chkActivate.Name = "chkActivate";
            this.chkActivate.Properties.Caption = "";
            this.chkActivate.Size = new System.Drawing.Size(94, 19);
            this.chkActivate.TabIndex = 4;
            this.chkActivate.CheckedChanged += new System.EventHandler(this.chkActivate_CheckedChanged);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnOpenItem);
            this.panelControl1.Controls.Add(this.btnDismiss);
            this.panelControl1.Controls.Add(this.cboSnooze);
            this.panelControl1.Controls.Add(this.btnSnooze);
            this.panelControl1.Controls.Add(this.lblSnooze);
            this.panelControl1.Controls.Add(this.dtmAlert);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.chkEmail);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Location = new System.Drawing.Point(7, 48);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(320, 136);
            this.panelControl1.TabIndex = 6;
            // 
            // btnOpenItem
            // 
            this.btnOpenItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenItem.Location = new System.Drawing.Point(146, 110);
            this.btnOpenItem.Name = "btnOpenItem";
            this.btnOpenItem.Size = new System.Drawing.Size(76, 21);
            this.btnOpenItem.TabIndex = 26;
            this.btnOpenItem.Text = "Open Item";
            this.btnOpenItem.Click += new System.EventHandler(this.btnOpenItem_Click);
            // 
            // btnDismiss
            // 
            this.btnDismiss.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDismiss.Location = new System.Drawing.Point(238, 110);
            this.btnDismiss.Name = "btnDismiss";
            this.btnDismiss.Size = new System.Drawing.Size(76, 21);
            this.btnDismiss.TabIndex = 25;
            this.btnDismiss.Text = "Dismiss";
            this.btnDismiss.Click += new System.EventHandler(this.btnDismiss_Click);
            // 
            // cboSnooze
            // 
            this.cboSnooze.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSnooze.Location = new System.Drawing.Point(93, 74);
            this.cboSnooze.Name = "cboSnooze";
            this.cboSnooze.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboSnooze.Properties.NullText = " ";
            this.cboSnooze.Size = new System.Drawing.Size(129, 20);
            this.cboSnooze.TabIndex = 24;
            // 
            // btnSnooze
            // 
            this.btnSnooze.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSnooze.Location = new System.Drawing.Point(238, 73);
            this.btnSnooze.Name = "btnSnooze";
            this.btnSnooze.Size = new System.Drawing.Size(76, 21);
            this.btnSnooze.TabIndex = 13;
            this.btnSnooze.Text = "Snooze it !";
            this.btnSnooze.Click += new System.EventHandler(this.btnSnooze_Click);
            // 
            // lblSnooze
            // 
            this.lblSnooze.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSnooze.Location = new System.Drawing.Point(23, 77);
            this.lblSnooze.Name = "lblSnooze";
            this.lblSnooze.Size = new System.Drawing.Size(39, 13);
            this.lblSnooze.TabIndex = 23;
            this.lblSnooze.Text = "Snooze:";
            // 
            // dtmAlert
            // 
            this.dtmAlert.EditValue = null;
            this.dtmAlert.Location = new System.Drawing.Point(93, 2);
            this.dtmAlert.Name = "dtmAlert";
            this.dtmAlert.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtmAlert.Properties.DisplayFormat.FormatString = "g";
            this.dtmAlert.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtmAlert.Properties.EditFormat.FormatString = "g";
            this.dtmAlert.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtmAlert.Properties.Mask.EditMask = "f";
            this.dtmAlert.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
            this.dtmAlert.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;
            this.dtmAlert.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtmAlert.Size = new System.Drawing.Size(207, 20);
            this.dtmAlert.TabIndex = 13;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(273, 202);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(48, 28);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(218, 202);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(48, 28);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // AlertForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 233);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.chkActivate);
            this.Controls.Add(this.panelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AlertForm";
            this.Text = "Alert";
            this.Load += new System.EventHandler(this.AlertForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chkEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkActivate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboSnooze.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmAlert.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmAlert.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.CheckEdit chkEmail;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.CheckEdit chkActivate;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.DateEdit dtmAlert;
        private DevExpress.XtraEditors.SimpleButton btnSnooze;
        private DevExpress.XtraEditors.LabelControl lblSnooze;
        private DevExpress.XtraEditors.LookUpEdit cboSnooze;
        private DevExpress.XtraEditors.SimpleButton btnDismiss;
        private DevExpress.XtraEditors.SimpleButton btnOpenItem;
    }
}