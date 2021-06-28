namespace PoorMansIcom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.labelLog = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ComPortNumber1 = new System.Windows.Forms.ComboBox();
            this.BaudComboBox = new System.Windows.Forms.ComboBox();
            this.labelPort = new System.Windows.Forms.Label();
            this.labelBaud = new System.Windows.Forms.Label();
            this.ComPortConnect1 = new System.Windows.Forms.Button();
            this.ComPortDisconnect1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.TrashButton = new System.Windows.Forms.Button();
            this.checkBoxReadPortLog = new System.Windows.Forms.CheckBox();
            this.checkBoxSendPortLog = new System.Windows.Forms.CheckBox();
            this.checkBoxErrorLog = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // labelLog
            // 
            this.labelLog.AutoSize = true;
            this.labelLog.Location = new System.Drawing.Point(9, 26);
            this.labelLog.Name = "labelLog";
            this.labelLog.Size = new System.Drawing.Size(101, 13);
            this.labelLog.TabIndex = 51;
            this.labelLog.Text = "Communications log";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(12, 42);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(675, 169);
            this.textBox1.TabIndex = 50;
            // 
            // ComPortNumber1
            // 
            this.ComPortNumber1.FormattingEnabled = true;
            this.ComPortNumber1.Location = new System.Drawing.Point(16, 239);
            this.ComPortNumber1.Name = "ComPortNumber1";
            this.ComPortNumber1.Size = new System.Drawing.Size(62, 21);
            this.ComPortNumber1.TabIndex = 40;
            // 
            // BaudComboBox
            // 
            this.BaudComboBox.FormattingEnabled = true;
            this.BaudComboBox.Items.AddRange(new object[] {
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.BaudComboBox.Location = new System.Drawing.Point(84, 239);
            this.BaudComboBox.Name = "BaudComboBox";
            this.BaudComboBox.Size = new System.Drawing.Size(62, 21);
            this.BaudComboBox.TabIndex = 41;
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(10, 221);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(26, 13);
            this.labelPort.TabIndex = 42;
            this.labelPort.Text = "Port";
            // 
            // labelBaud
            // 
            this.labelBaud.AutoSize = true;
            this.labelBaud.Location = new System.Drawing.Point(78, 221);
            this.labelBaud.Name = "labelBaud";
            this.labelBaud.Size = new System.Drawing.Size(32, 13);
            this.labelBaud.TabIndex = 43;
            this.labelBaud.Text = "Baud";
            // 
            // ComPortConnect1
            // 
            this.ComPortConnect1.Location = new System.Drawing.Point(152, 237);
            this.ComPortConnect1.Name = "ComPortConnect1";
            this.ComPortConnect1.Size = new System.Drawing.Size(72, 23);
            this.ComPortConnect1.TabIndex = 44;
            this.ComPortConnect1.Text = "Connect";
            this.ComPortConnect1.UseVisualStyleBackColor = true;
            this.ComPortConnect1.Click += new System.EventHandler(this.ComPortConnect1_Click);
            // 
            // ComPortDisconnect1
            // 
            this.ComPortDisconnect1.Enabled = false;
            this.ComPortDisconnect1.Location = new System.Drawing.Point(230, 237);
            this.ComPortDisconnect1.Name = "ComPortDisconnect1";
            this.ComPortDisconnect1.Size = new System.Drawing.Size(72, 23);
            this.ComPortDisconnect1.TabIndex = 45;
            this.ComPortDisconnect1.Text = "Disconnect";
            this.ComPortDisconnect1.UseVisualStyleBackColor = true;
            this.ComPortDisconnect1.Click += new System.EventHandler(this.ComPortDisconnect1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Georgia", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label8.Location = new System.Drawing.Point(512, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(175, 16);
            this.label8.TabIndex = 55;
            this.label8.Text = "software @ SA6HBR . se";
            // 
            // TrashButton
            // 
            this.TrashButton.Image = ((System.Drawing.Image)(resources.GetObject("TrashButton.Image")));
            this.TrashButton.Location = new System.Drawing.Point(646, 221);
            this.TrashButton.Name = "TrashButton";
            this.TrashButton.Size = new System.Drawing.Size(34, 34);
            this.TrashButton.TabIndex = 56;
            this.TrashButton.UseVisualStyleBackColor = true;
            this.TrashButton.Click += new System.EventHandler(this.TrashButton_Click);
            // 
            // checkBoxReadPortLog
            // 
            this.checkBoxReadPortLog.AutoSize = true;
            this.checkBoxReadPortLog.Location = new System.Drawing.Point(322, 218);
            this.checkBoxReadPortLog.Name = "checkBoxReadPortLog";
            this.checkBoxReadPortLog.Size = new System.Drawing.Size(89, 17);
            this.checkBoxReadPortLog.TabIndex = 57;
            this.checkBoxReadPortLog.Text = "ReadPortLog";
            this.checkBoxReadPortLog.UseVisualStyleBackColor = true;
            // 
            // checkBoxSendPortLog
            // 
            this.checkBoxSendPortLog.AutoSize = true;
            this.checkBoxSendPortLog.Location = new System.Drawing.Point(322, 239);
            this.checkBoxSendPortLog.Name = "checkBoxSendPortLog";
            this.checkBoxSendPortLog.Size = new System.Drawing.Size(88, 17);
            this.checkBoxSendPortLog.TabIndex = 58;
            this.checkBoxSendPortLog.Text = "SendPortLog";
            this.checkBoxSendPortLog.UseVisualStyleBackColor = true;
            // 
            // checkBoxErrorLog
            // 
            this.checkBoxErrorLog.AutoSize = true;
            this.checkBoxErrorLog.Checked = true;
            this.checkBoxErrorLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxErrorLog.Location = new System.Drawing.Point(416, 218);
            this.checkBoxErrorLog.Name = "checkBoxErrorLog";
            this.checkBoxErrorLog.Size = new System.Drawing.Size(66, 17);
            this.checkBoxErrorLog.TabIndex = 59;
            this.checkBoxErrorLog.Text = "ErrorLog";
            this.checkBoxErrorLog.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 267);
            this.Controls.Add(this.checkBoxErrorLog);
            this.Controls.Add(this.checkBoxSendPortLog);
            this.Controls.Add(this.checkBoxReadPortLog);
            this.Controls.Add(this.TrashButton);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.labelLog);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.ComPortNumber1);
            this.Controls.Add(this.BaudComboBox);
            this.Controls.Add(this.labelPort);
            this.Controls.Add(this.labelBaud);
            this.Controls.Add(this.ComPortConnect1);
            this.Controls.Add(this.ComPortDisconnect1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Poor Man\'s Icom - SA6HBR";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelLog;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox ComPortNumber1;
        private System.Windows.Forms.ComboBox BaudComboBox;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.Label labelBaud;
        private System.Windows.Forms.Button ComPortConnect1;
        private System.Windows.Forms.Button ComPortDisconnect1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button TrashButton;
        private System.Windows.Forms.CheckBox checkBoxReadPortLog;
        private System.Windows.Forms.CheckBox checkBoxSendPortLog;
        private System.Windows.Forms.CheckBox checkBoxErrorLog;
    }
}

