namespace Prioritizer.Forms
{
    partial class DragDecisionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DragDecisionForm));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnAttach = new DevExpress.XtraEditors.SimpleButton();
            this.btnNewTask = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.btnAttach);
            this.panelControl1.Controls.Add(this.btnNewTask);
            this.panelControl1.Location = new System.Drawing.Point(14, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(316, 149);
            this.panelControl1.TabIndex = 0;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(35, 131);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(70, 13);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Attach to Task";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(220, 131);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(46, 13);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "New Task";
            // 
            // btnAttach
            // 
            this.btnAttach.Image = ((System.Drawing.Image)(resources.GetObject("btnAttach.Image")));
            this.btnAttach.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnAttach.Location = new System.Drawing.Point(5, 10);
            this.btnAttach.Name = "btnAttach";
            this.btnAttach.Size = new System.Drawing.Size(134, 117);
            this.btnAttach.TabIndex = 2;
            this.btnAttach.Click += new System.EventHandler(this.btnAttach_Click);
            // 
            // btnNewTask
            // 
            this.btnNewTask.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnNewTask.Image = ((System.Drawing.Image)(resources.GetObject("btnNewTask.Image")));
            this.btnNewTask.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnNewTask.Location = new System.Drawing.Point(177, 10);
            this.btnNewTask.Name = "btnNewTask";
            this.btnNewTask.Size = new System.Drawing.Size(134, 117);
            this.btnNewTask.TabIndex = 1;
            this.btnNewTask.ToolTip = "Create New Task ";
            this.btnNewTask.Click += new System.EventHandler(this.btnNewTask_Click);
            // 
            // DragDecisionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 177);
            this.Controls.Add(this.panelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DragDecisionForm";
            this.Text = "Dragged File Action";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnNewTask;
        private DevExpress.XtraEditors.SimpleButton btnAttach;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}