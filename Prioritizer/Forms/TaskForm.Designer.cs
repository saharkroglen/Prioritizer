namespace Prioritizer.Forms
{
    partial class TaskForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskForm));
            this.cmbAssignedTo = new DevExpress.XtraEditors.LookUpEdit();
            this.lblAssignedTo = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtEstimatedHours1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtCompletedPercent1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.cmbRequester1 = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtDefectNum1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.cmbProject1 = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txtActualWork1 = new DevExpress.XtraEditors.TextEdit();
            this.dtmDueDate1 = new DevExpress.XtraEditors.DateEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.txtRemarks = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cmbImportance = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAssignedTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEstimatedHours1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompletedPercent1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbRequester1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDefectNum1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProject1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtActualWork1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmDueDate1.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmDueDate1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemarks.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbImportance.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbAssignedTo
            // 
            this.cmbAssignedTo.Location = new System.Drawing.Point(109, 29);
            this.cmbAssignedTo.Name = "cmbAssignedTo";
            this.cmbAssignedTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbAssignedTo.Properties.NullText = "Decision";
            this.cmbAssignedTo.Size = new System.Drawing.Size(147, 20);
            this.cmbAssignedTo.TabIndex = 0;
            // 
            // lblAssignedTo
            // 
            this.lblAssignedTo.Location = new System.Drawing.Point(45, 32);
            this.lblAssignedTo.Name = "lblAssignedTo";
            this.lblAssignedTo.Size = new System.Drawing.Size(58, 13);
            this.lblAssignedTo.TabIndex = 1;
            this.lblAssignedTo.Text = "Assigned To";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(109, 55);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(439, 20);
            this.txtName.TabIndex = 1;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(51, 58);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(52, 13);
            this.labelControl6.TabIndex = 7;
            this.labelControl6.Text = "Task Name";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(25, 110);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(78, 13);
            this.labelControl2.TabIndex = 9;
            this.labelControl2.Text = "Estimated Hours";
            // 
            // txtEstimatedHours1
            // 
            this.txtEstimatedHours1.Location = new System.Drawing.Point(109, 107);
            this.txtEstimatedHours1.Name = "txtEstimatedHours1";
            this.txtEstimatedHours1.Size = new System.Drawing.Size(58, 20);
            this.txtEstimatedHours1.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(38, 162);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(65, 13);
            this.labelControl3.TabIndex = 11;
            this.labelControl3.Text = "% Completed";
            // 
            // txtCompletedPercent1
            // 
            this.txtCompletedPercent1.Location = new System.Drawing.Point(109, 159);
            this.txtCompletedPercent1.Name = "txtCompletedPercent1";
            this.txtCompletedPercent1.Size = new System.Drawing.Size(58, 20);
            this.txtCompletedPercent1.TabIndex = 5;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(36, 136);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(67, 13);
            this.labelControl4.TabIndex = 13;
            this.labelControl4.Text = "Requested By";
            // 
            // cmbRequester1
            // 
            this.cmbRequester1.Location = new System.Drawing.Point(109, 133);
            this.cmbRequester1.Name = "cmbRequester1";
            this.cmbRequester1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbRequester1.Properties.NullText = "N/A";
            this.cmbRequester1.Size = new System.Drawing.Size(147, 20);
            this.cmbRequester1.TabIndex = 4;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(60, 214);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(43, 13);
            this.labelControl5.TabIndex = 15;
            this.labelControl5.Text = "Defect #";
            this.labelControl5.Click += new System.EventHandler(this.labelControl5_Click);
            // 
            // txtDefectNum1
            // 
            this.txtDefectNum1.Location = new System.Drawing.Point(109, 211);
            this.txtDefectNum1.Name = "txtDefectNum1";
            this.txtDefectNum1.Size = new System.Drawing.Size(58, 20);
            this.txtDefectNum1.TabIndex = 7;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(69, 188);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(34, 13);
            this.labelControl7.TabIndex = 17;
            this.labelControl7.Text = "Project";
            // 
            // cmbProject1
            // 
            this.cmbProject1.Location = new System.Drawing.Point(109, 185);
            this.cmbProject1.Name = "cmbProject1";
            this.cmbProject1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbProject1.Properties.NullText = "N/A";
            this.cmbProject1.Size = new System.Drawing.Size(147, 20);
            this.cmbProject1.TabIndex = 6;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(6, 240);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(97, 13);
            this.labelControl8.TabIndex = 19;
            this.labelControl8.Text = "Actual Work (Hours)";
            // 
            // txtActualWork1
            // 
            this.txtActualWork1.Location = new System.Drawing.Point(109, 237);
            this.txtActualWork1.Name = "txtActualWork1";
            this.txtActualWork1.Size = new System.Drawing.Size(58, 20);
            this.txtActualWork1.TabIndex = 8;
            // 
            // dtmDueDate1
            // 
            this.dtmDueDate1.EditValue = null;
            this.dtmDueDate1.Location = new System.Drawing.Point(109, 263);
            this.dtmDueDate1.Name = "dtmDueDate1";
            this.dtmDueDate1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtmDueDate1.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dtmDueDate1.Size = new System.Drawing.Size(147, 20);
            this.dtmDueDate1.TabIndex = 9;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(58, 266);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(45, 13);
            this.labelControl9.TabIndex = 21;
            this.labelControl9.Text = "Due Date";
            // 
            // txtRemarks
            // 
            this.txtRemarks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRemarks.Location = new System.Drawing.Point(109, 289);
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(434, 94);
            this.txtRemarks.TabIndex = 10;
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(62, 291);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(41, 13);
            this.labelControl10.TabIndex = 23;
            this.labelControl10.Text = "Remarks";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(494, 395);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(48, 28);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click_1);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(51, 84);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(55, 13);
            this.labelControl1.TabIndex = 25;
            this.labelControl1.Text = "Importance";
            // 
            // cmbImportance
            // 
            this.cmbImportance.Location = new System.Drawing.Point(109, 81);
            this.cmbImportance.Name = "cmbImportance";
            this.cmbImportance.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbImportance.Properties.NullText = "N/A";
            this.cmbImportance.Size = new System.Drawing.Size(147, 20);
            this.cmbImportance.TabIndex = 2;
            // 
            // TaskForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 429);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.cmbImportance);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.labelControl10);
            this.Controls.Add(this.txtRemarks);
            this.Controls.Add(this.labelControl9);
            this.Controls.Add(this.dtmDueDate1);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.txtActualWork1);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.cmbProject1);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.txtDefectNum1);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.cmbRequester1);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txtCompletedPercent1);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txtEstimatedHours1);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblAssignedTo);
            this.Controls.Add(this.cmbAssignedTo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TaskForm";
            this.Text = "Task";
            ((System.ComponentModel.ISupportInitialize)(this.cmbAssignedTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEstimatedHours1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompletedPercent1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbRequester1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDefectNum1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProject1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtActualWork1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmDueDate1.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtmDueDate1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemarks.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbImportance.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LookUpEdit cmbAssignedTo;
        private DevExpress.XtraEditors.LabelControl lblAssignedTo;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtEstimatedHours1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtCompletedPercent1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LookUpEdit cmbRequester1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit txtDefectNum1;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LookUpEdit cmbProject1;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.TextEdit txtActualWork1;
        private DevExpress.XtraEditors.DateEdit dtmDueDate1;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.MemoEdit txtRemarks;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit cmbImportance;
    }
}