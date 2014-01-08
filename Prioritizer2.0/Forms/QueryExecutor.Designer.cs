namespace Prioritizer2._0
{
    partial class QueryExecutor
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
            this.radioTasksDB = new System.Windows.Forms.RadioButton();
            this.radioAttachDB = new System.Windows.Forms.RadioButton();
            this.gridResults = new Telerik.WinControls.UI.RadGridView();
            this.txtSql = new Telerik.WinControls.UI.RadTextBox();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.btnGo = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSql)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGo)).BeginInit();
            this.SuspendLayout();
            // 
            // radioTasksDB
            // 
            this.radioTasksDB.AutoSize = true;
            this.radioTasksDB.Location = new System.Drawing.Point(31, 12);
            this.radioTasksDB.Name = "radioTasksDB";
            this.radioTasksDB.Size = new System.Drawing.Size(72, 17);
            this.radioTasksDB.TabIndex = 0;
            this.radioTasksDB.TabStop = true;
            this.radioTasksDB.Text = "Tasks DB";
            this.radioTasksDB.UseVisualStyleBackColor = true;
            // 
            // radioAttachDB
            // 
            this.radioAttachDB.AutoSize = true;
            this.radioAttachDB.Location = new System.Drawing.Point(109, 12);
            this.radioAttachDB.Name = "radioAttachDB";
            this.radioAttachDB.Size = new System.Drawing.Size(102, 17);
            this.radioAttachDB.TabIndex = 1;
            this.radioAttachDB.TabStop = true;
            this.radioAttachDB.Text = "Attachments DB";
            this.radioAttachDB.UseVisualStyleBackColor = true;
            // 
            // gridResults
            // 
            this.gridResults.Location = new System.Drawing.Point(29, 141);
            this.gridResults.Name = "gridResults";
            this.gridResults.Size = new System.Drawing.Size(747, 275);
            this.gridResults.TabIndex = 4;
            this.gridResults.Text = "radGridView1";
            // 
            // txtSql
            // 
            this.txtSql.AcceptsReturn = true;
            this.txtSql.AcceptsTab = true;
            this.txtSql.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSql.Location = new System.Drawing.Point(29, 69);
            this.txtSql.Multiline = true;
            this.txtSql.Name = "txtSql";
            // 
            // 
            // 
            this.txtSql.RootElement.StretchVertically = true;
            this.txtSql.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSql.Size = new System.Drawing.Size(697, 50);
            this.txtSql.TabIndex = 2;
            this.txtSql.TabStop = false;
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(31, 122);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(44, 16);
            this.radLabel1.TabIndex = 4;
            this.radLabel1.Text = "Results";
            // 
            // radLabel2
            // 
            this.radLabel2.Location = new System.Drawing.Point(31, 48);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(29, 16);
            this.radLabel2.TabIndex = 5;
            this.radLabel2.Text = "SQL";
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(732, 69);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(40, 50);
            this.btnGo.TabIndex = 3;
            this.btnGo.Text = "Go";
            //this.btnGo.Click += new System.EventHandler(this.radButton1_Click);
            // 
            // QueryExecutor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 462);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.radLabel2);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.txtSql);
            this.Controls.Add(this.gridResults);
            this.Controls.Add(this.radioAttachDB);
            this.Controls.Add(this.radioTasksDB);
            this.Name = "QueryExecutor";
            this.Text = "QueryExecutor";
            ((System.ComponentModel.ISupportInitialize)(this.gridResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSql)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioTasksDB;
        private System.Windows.Forms.RadioButton radioAttachDB;
        private Telerik.WinControls.UI.RadGridView gridResults;
        private Telerik.WinControls.UI.RadTextBox txtSql;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadButton btnGo;
    }
}