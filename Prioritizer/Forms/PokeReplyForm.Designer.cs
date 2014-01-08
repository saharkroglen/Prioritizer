namespace Prioritizer.Forms
{
    partial class PokeReplyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PokeReplyForm));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.chkEmail = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.cboPokeText = new DevExpress.XtraEditors.LookUpEdit();
            this.lblTo = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtRemarks = new DevExpress.XtraEditors.MemoEdit();
            this.btnPoke = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.chkEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboPokeText.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemarks.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(8, 29);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(49, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Comment:";
            // 
            // chkEmail
            // 
            this.chkEmail.Location = new System.Drawing.Point(117, 165);
            this.chkEmail.Name = "chkEmail";
            this.chkEmail.Properties.Caption = "";
            this.chkEmail.Size = new System.Drawing.Size(94, 19);
            this.chkEmail.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(8, 167);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(55, 13);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Send Email:";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.cboPokeText);
            this.panelControl1.Controls.Add(this.lblTo);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.txtRemarks);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.chkEmail);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Location = new System.Drawing.Point(7, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(312, 188);
            this.panelControl1.TabIndex = 6;
            // 
            // cboPokeText
            // 
            this.cboPokeText.Location = new System.Drawing.Point(119, 31);
            this.cboPokeText.Name = "cboPokeText";
            this.cboPokeText.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPokeText.Properties.NullText = " ";
            this.cboPokeText.Size = new System.Drawing.Size(180, 20);
            this.cboPokeText.TabIndex = 1;
            // 
            // lblTo
            // 
            this.lblTo.Location = new System.Drawing.Point(8, 5);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(16, 13);
            this.lblTo.TabIndex = 16;
            this.lblTo.Text = "To:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(8, 59);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(105, 13);
            this.labelControl3.TabIndex = 15;
            this.labelControl3.Text = "Alternative Comment:";
            // 
            // txtRemarks
            // 
            this.txtRemarks.Location = new System.Drawing.Point(119, 57);
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(180, 99);
            this.txtRemarks.TabIndex = 2;
            this.txtRemarks.EditValueChanged += new System.EventHandler(this.txtRemarks_EditValueChanged);
            // 
            // btnPoke
            // 
            this.btnPoke.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPoke.Location = new System.Drawing.Point(271, 208);
            this.btnPoke.Name = "btnPoke";
            this.btnPoke.Size = new System.Drawing.Size(48, 28);
            this.btnPoke.TabIndex = 8;
            this.btnPoke.Text = "Reply";
            this.btnPoke.Click += new System.EventHandler(this.btnPoke_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(216, 208);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(48, 28);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // PokeReplyForm
            // 
            this.AcceptButton = this.btnPoke;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(330, 239);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnPoke);
            this.Controls.Add(this.panelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PokeReplyForm";
            this.Text = "Poke Reply";
            this.Load += new System.EventHandler(this.PokeReplyForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chkEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboPokeText.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRemarks.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.CheckEdit chkEmail;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnPoke;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.MemoEdit txtRemarks;
        private DevExpress.XtraEditors.LabelControl lblTo;
        private DevExpress.XtraEditors.LookUpEdit cboPokeText;
    }
}