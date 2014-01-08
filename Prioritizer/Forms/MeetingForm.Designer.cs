namespace Prioritizer.Forms
{
    partial class MeetingForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MeetingForm));
            this.cboMeetingCategory = new DevExpress.XtraEditors.LookUpEdit();
            this.lblAssignedTo = new DevExpress.XtraEditors.LabelControl();
            this.txtMeetingName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.dtmMeetingDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddCategory = new DevExpress.XtraEditors.SimpleButton();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.cboMeetingCategory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMeetingName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmMeetingDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmMeetingDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // cboMeetingCategory
            // 
            this.cboMeetingCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboMeetingCategory.Location = new System.Drawing.Point(77, 67);
            this.cboMeetingCategory.Name = "cboMeetingCategory";
            this.cboMeetingCategory.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboMeetingCategory.Properties.NullText = " ";
            this.cboMeetingCategory.Size = new System.Drawing.Size(159, 20);
            this.cboMeetingCategory.TabIndex = 3;
            // 
            // lblAssignedTo
            // 
            this.lblAssignedTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAssignedTo.Location = new System.Drawing.Point(26, 70);
            this.lblAssignedTo.Name = "lblAssignedTo";
            this.lblAssignedTo.Size = new System.Drawing.Size(45, 13);
            this.lblAssignedTo.TabIndex = 1;
            this.lblAssignedTo.Text = "Category";
            // 
            // txtMeetingName
            // 
            this.txtMeetingName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMeetingName.Location = new System.Drawing.Point(77, 15);
            this.txtMeetingName.Name = "txtMeetingName";
            this.txtMeetingName.Size = new System.Drawing.Size(192, 20);
            this.txtMeetingName.TabIndex = 1;
            // 
            // labelControl6
            // 
            this.labelControl6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl6.Location = new System.Drawing.Point(3, 18);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(68, 13);
            this.labelControl6.TabIndex = 7;
            this.labelControl6.Text = "Meeting Name";
            // 
            // dtmMeetingDate
            // 
            this.dtmMeetingDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtmMeetingDate.EditValue = null;
            this.dtmMeetingDate.Location = new System.Drawing.Point(77, 41);
            this.dtmMeetingDate.Name = "dtmMeetingDate";
            this.dtmMeetingDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtmMeetingDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtmMeetingDate.Size = new System.Drawing.Size(192, 20);
            this.dtmMeetingDate.TabIndex = 2;
            // 
            // labelControl9
            // 
            this.labelControl9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl9.Location = new System.Drawing.Point(7, 44);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(64, 13);
            this.labelControl9.TabIndex = 21;
            this.labelControl9.Text = "Meeting Date";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(221, 93);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(48, 28);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click_1);
            // 
            // btnAddCategory
            // 
            this.btnAddCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddCategory.Location = new System.Drawing.Point(242, 67);
            this.btnAddCategory.Name = "btnAddCategory";
            this.btnAddCategory.Size = new System.Drawing.Size(27, 20);
            this.btnAddCategory.TabIndex = 22;
            this.btnAddCategory.Text = "...";
            this.btnAddCategory.Click += new System.EventHandler(this.btnAddCategory_Click);
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            // 
            // MeetingForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 132);
            this.Controls.Add(this.btnAddCategory);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.labelControl9);
            this.Controls.Add(this.dtmMeetingDate);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.txtMeetingName);
            this.Controls.Add(this.lblAssignedTo);
            this.Controls.Add(this.cboMeetingCategory);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MeetingForm";
            this.Text = "New Meeting";
            ((System.ComponentModel.ISupportInitialize)(this.cboMeetingCategory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMeetingName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmMeetingDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmMeetingDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LookUpEdit cboMeetingCategory;
        private DevExpress.XtraEditors.LabelControl lblAssignedTo;
        private DevExpress.XtraEditors.TextEdit txtMeetingName;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.DateEdit dtmMeetingDate;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnAddCategory;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
    }
}