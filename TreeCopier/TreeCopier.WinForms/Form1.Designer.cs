namespace TreeCopier.WinForms
{
    partial class Form1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ConfluencetreeView = new System.Windows.Forms.TreeView();
            this.MaincomboBox = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ConfluencetreeView2 = new System.Windows.Forms.TreeView();
            this.TargetcomboBox = new System.Windows.Forms.ComboBox();
            this.ConfluenceBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.ConfluenceBackgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.Copybutton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ConfluencetreeView);
            this.groupBox1.Controls.Add(this.MaincomboBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(370, 507);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Main ";
            // 
            // ConfluencetreeView
            // 
            this.ConfluencetreeView.CheckBoxes = true;
            this.ConfluencetreeView.Location = new System.Drawing.Point(7, 58);
            this.ConfluencetreeView.Name = "ConfluencetreeView";
            this.ConfluencetreeView.Size = new System.Drawing.Size(357, 443);
            this.ConfluencetreeView.TabIndex = 1;
            // 
            // MaincomboBox
            // 
            this.MaincomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MaincomboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaincomboBox.FormattingEnabled = true;
            this.MaincomboBox.Location = new System.Drawing.Point(7, 20);
            this.MaincomboBox.Name = "MaincomboBox";
            this.MaincomboBox.Size = new System.Drawing.Size(357, 32);
            this.MaincomboBox.TabIndex = 0;
            this.MaincomboBox.SelectedIndexChanged += new System.EventHandler(this.MaincomboBox_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ConfluencetreeView2);
            this.groupBox2.Controls.Add(this.TargetcomboBox);
            this.groupBox2.Location = new System.Drawing.Point(388, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(370, 507);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " Target ";
            // 
            // ConfluencetreeView2
            // 
            this.ConfluencetreeView2.CheckBoxes = true;
            this.ConfluencetreeView2.Location = new System.Drawing.Point(7, 58);
            this.ConfluencetreeView2.Name = "ConfluencetreeView2";
            this.ConfluencetreeView2.Size = new System.Drawing.Size(357, 443);
            this.ConfluencetreeView2.TabIndex = 2;
            // 
            // TargetcomboBox
            // 
            this.TargetcomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TargetcomboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TargetcomboBox.FormattingEnabled = true;
            this.TargetcomboBox.Location = new System.Drawing.Point(6, 20);
            this.TargetcomboBox.Name = "TargetcomboBox";
            this.TargetcomboBox.Size = new System.Drawing.Size(357, 32);
            this.TargetcomboBox.TabIndex = 1;
            this.TargetcomboBox.SelectedIndexChanged += new System.EventHandler(this.TargetcomboBox_SelectedIndexChanged);
            // 
            // ConfluenceBackgroundWorker
            // 
            this.ConfluenceBackgroundWorker.WorkerReportsProgress = true;
            this.ConfluenceBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ConfluenceBackgroundWorker_DoWork);
            this.ConfluenceBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.ConfluenceBackgroundWorker_RunWorkerCompleted);
            // 
            // ConfluenceBackgroundWorker2
            // 
            this.ConfluenceBackgroundWorker2.WorkerReportsProgress = true;
            this.ConfluenceBackgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ConfluenceBackgroundWorkerRIGHT_DoWork);
            this.ConfluenceBackgroundWorker2.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.ConfluenceBackgroundWorkerRIGHT_RunWorkerCompleted);
            // 
            // Copybutton
            // 
            this.Copybutton.Location = new System.Drawing.Point(662, 525);
            this.Copybutton.Name = "Copybutton";
            this.Copybutton.Size = new System.Drawing.Size(96, 39);
            this.Copybutton.TabIndex = 2;
            this.Copybutton.Text = "Copy";
            this.Copybutton.UseVisualStyleBackColor = true;
            this.Copybutton.Click += new System.EventHandler(this.Copybutton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 573);
            this.Controls.Add(this.Copybutton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox MaincomboBox;
        private System.Windows.Forms.ComboBox TargetcomboBox;
        private System.Windows.Forms.TreeView ConfluencetreeView;
        private System.ComponentModel.BackgroundWorker ConfluenceBackgroundWorker;
        private System.Windows.Forms.TreeView ConfluencetreeView2;
        private System.ComponentModel.BackgroundWorker ConfluenceBackgroundWorker2;
        private System.Windows.Forms.Button Copybutton;
    }
}

