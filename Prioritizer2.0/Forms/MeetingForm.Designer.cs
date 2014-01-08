namespace Prioritizer2._0.Forms
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
            this.cboMeetingCategory = new Telerik.WinControls.UI.RadDropDownList();
            this.lblAssignedTo = new System.Windows.Forms.Label();
            this.txtMeetingName = new Telerik.WinControls.UI.RadTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtmMeetingDate = new Telerik.WinControls.UI.RadDateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.btnEditMeetingCategories = new Telerik.WinControls.UI.RadButton();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.cboMeetingCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMeetingName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmMeetingDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditMeetingCategories)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            this.SuspendLayout();
            // 
            // cboMeetingCategory
            // 
            this.cboMeetingCategory.Location = new System.Drawing.Point(100, 96);
            this.cboMeetingCategory.Name = "cboMeetingCategory";
            this.cboMeetingCategory.ShowImageInEditorArea = true;
            this.cboMeetingCategory.Size = new System.Drawing.Size(158, 21);
            this.cboMeetingCategory.TabIndex = 27;
            // 
            // lblAssignedTo
            // 
            this.lblAssignedTo.AutoSize = true;
            this.lblAssignedTo.Location = new System.Drawing.Point(41, 96);
            this.lblAssignedTo.Name = "lblAssignedTo";
            this.lblAssignedTo.Size = new System.Drawing.Size(49, 13);
            this.lblAssignedTo.TabIndex = 28;
            this.lblAssignedTo.Text = "Category";
            // 
            // txtMeetingName
            // 
            this.txtMeetingName.Location = new System.Drawing.Point(100, 35);
            this.txtMeetingName.Name = "txtMeetingName";
            this.txtMeetingName.Size = new System.Drawing.Size(410, 20);
            this.txtMeetingName.TabIndex = 25;
            this.txtMeetingName.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "Meeting Name";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // dtmMeetingDate
            // 
            this.dtmMeetingDate.Culture = new System.Globalization.CultureInfo("he-IL");
            this.dtmMeetingDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtmMeetingDate.Location = new System.Drawing.Point(100, 65);
            this.dtmMeetingDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtmMeetingDate.MinDate = new System.DateTime(1760, 12, 31, 0, 0, 0, 0);
            this.dtmMeetingDate.Name = "dtmMeetingDate";
            this.dtmMeetingDate.NullDate = new System.DateTime(1760, 12, 31, 0, 0, 0, 0);
            this.dtmMeetingDate.Size = new System.Drawing.Size(158, 20);
            this.dtmMeetingDate.TabIndex = 29;
            this.dtmMeetingDate.TabStop = false;
            this.dtmMeetingDate.Text = "radDateTimePicker1";
            this.dtmMeetingDate.Value = new System.DateTime(1760, 12, 31, 0, 0, 0, 0);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(19, 65);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 13);
            this.label11.TabIndex = 30;
            this.label11.Text = "Meeting Date";
            // 
            // btnEditMeetingCategories
            // 
            this.btnEditMeetingCategories.Location = new System.Drawing.Point(268, 98);
            this.btnEditMeetingCategories.Name = "btnEditMeetingCategories";
            this.btnEditMeetingCategories.Size = new System.Drawing.Size(24, 18);
            this.btnEditMeetingCategories.TabIndex = 31;
            this.btnEditMeetingCategories.Text = ". . .";
            this.btnEditMeetingCategories.Click += new System.EventHandler(this.btnEditMeetingCategories_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(436, 122);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(74, 22);
            this.btnSave.TabIndex = 32;
            this.btnSave.Text = "Save And Exit";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // MeetingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 156);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnEditMeetingCategories);
            this.Controls.Add(this.dtmMeetingDate);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cboMeetingCategory);
            this.Controls.Add(this.lblAssignedTo);
            this.Controls.Add(this.txtMeetingName);
            this.Controls.Add(this.label1);
            this.Name = "MeetingForm";
            this.Text = "New Meeting";
            ((System.ComponentModel.ISupportInitialize)(this.cboMeetingCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMeetingName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmMeetingDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditMeetingCategories)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadDropDownList cboMeetingCategory;
        private System.Windows.Forms.Label lblAssignedTo;
        private Telerik.WinControls.UI.RadTextBox txtMeetingName;
        private System.Windows.Forms.Label label1;
        private Telerik.WinControls.UI.RadDateTimePicker dtmMeetingDate;
        private System.Windows.Forms.Label label11;
        private Telerik.WinControls.UI.RadButton btnEditMeetingCategories;
        private Telerik.WinControls.UI.RadButton btnSave;
    }
}