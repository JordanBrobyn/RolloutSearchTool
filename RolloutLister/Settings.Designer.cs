namespace RolloutLister
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.serverAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.safetyOptionBox = new System.Windows.Forms.ComboBox();
            this.loggingCheck = new System.Windows.Forms.CheckBox();
            this.nodeCheck = new System.Windows.Forms.CheckBox();
            this.rejectionCheck = new System.Windows.Forms.CheckBox();
            this.rejectionToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.nodeToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.selectionToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.serverToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(162, 253);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(243, 253);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.serverAddress);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(305, 70);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server Info";
            // 
            // serverAddress
            // 
            this.serverAddress.Location = new System.Drawing.Point(96, 20);
            this.serverAddress.Name = "serverAddress";
            this.serverAddress.Size = new System.Drawing.Size(203, 20);
            this.serverAddress.TabIndex = 1;
            this.serverAddress.Text = "sv72902";
            this.serverToolTip.SetToolTip(this.serverAddress, "This address is the same address SCCM uses.\r\nBe careful to change this value, as " +
        "an incorrect value\r\nwill prevent the rest of the tool to function properly.\r\n(De" +
        "fault: sv72902)");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server Address:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.safetyOptionBox);
            this.groupBox2.Controls.Add(this.loggingCheck);
            this.groupBox2.Controls.Add(this.nodeCheck);
            this.groupBox2.Controls.Add(this.rejectionCheck);
            this.groupBox2.Location = new System.Drawing.Point(13, 89);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(305, 158);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Export Settings";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(199, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Minimum Selections";
            // 
            // safetyOptionBox
            // 
            this.safetyOptionBox.FormattingEnabled = true;
            this.safetyOptionBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.safetyOptionBox.Location = new System.Drawing.Point(219, 49);
            this.safetyOptionBox.Name = "safetyOptionBox";
            this.safetyOptionBox.Size = new System.Drawing.Size(48, 21);
            this.safetyOptionBox.TabIndex = 6;
            // 
            // loggingCheck
            // 
            this.loggingCheck.AutoSize = true;
            this.loggingCheck.Location = new System.Drawing.Point(10, 108);
            this.loggingCheck.Name = "loggingCheck";
            this.loggingCheck.Size = new System.Drawing.Size(158, 17);
            this.loggingCheck.TabIndex = 5;
            this.loggingCheck.Text = "Generate Selection Logging";
            this.selectionToolTip.SetToolTip(this.loggingCheck, "Enabling this feature will produce a \nselection log on export.");
            this.loggingCheck.UseVisualStyleBackColor = true;
            // 
            // nodeCheck
            // 
            this.nodeCheck.AutoSize = true;
            this.nodeCheck.Location = new System.Drawing.Point(10, 71);
            this.nodeCheck.Name = "nodeCheck";
            this.nodeCheck.Size = new System.Drawing.Size(118, 17);
            this.nodeCheck.TabIndex = 4;
            this.nodeCheck.Text = "Generate Node List";
            this.nodeToolTip.SetToolTip(this.nodeCheck, "Select this option to create a full node list\n with the acf2\'s added when exporti" +
        "ng");
            this.nodeCheck.UseVisualStyleBackColor = true;
            // 
            // rejectionCheck
            // 
            this.rejectionCheck.AutoSize = true;
            this.rejectionCheck.Location = new System.Drawing.Point(10, 32);
            this.rejectionCheck.Name = "rejectionCheck";
            this.rejectionCheck.Size = new System.Drawing.Size(137, 17);
            this.rejectionCheck.TabIndex = 3;
            this.rejectionCheck.Text = "Generate Rejection List";
            this.rejectionToolTip.SetToolTip(this.rejectionCheck, "Select this option to create a rejection list\n that you can look at later when ex" +
        "porting.");
            this.rejectionCheck.UseVisualStyleBackColor = true;
            // 
            // rejectionToolTip
            // 
            this.rejectionToolTip.AutoPopDelay = 5000;
            this.rejectionToolTip.InitialDelay = 250;
            this.rejectionToolTip.ReshowDelay = 100;
            this.rejectionToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // nodeToolTip
            // 
            this.nodeToolTip.AutoPopDelay = 5000;
            this.nodeToolTip.InitialDelay = 250;
            this.nodeToolTip.ReshowDelay = 100;
            this.nodeToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // selectionToolTip
            // 
            this.selectionToolTip.AutoPopDelay = 5000;
            this.selectionToolTip.InitialDelay = 250;
            this.selectionToolTip.ReshowDelay = 100;
            this.selectionToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // serverToolTip
            // 
            this.serverToolTip.AutoPopDelay = 5000;
            this.serverToolTip.InitialDelay = 250;
            this.serverToolTip.ReshowDelay = 100;
            this.serverToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // Settings
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(330, 282);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Settings";
            this.Text = "Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox serverAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox loggingCheck;
        private System.Windows.Forms.CheckBox nodeCheck;
        private System.Windows.Forms.CheckBox rejectionCheck;
        private System.Windows.Forms.ToolTip rejectionToolTip;
        private System.Windows.Forms.ToolTip nodeToolTip;
        private System.Windows.Forms.ToolTip selectionToolTip;
        private System.Windows.Forms.ToolTip serverToolTip;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox safetyOptionBox;
    }
}