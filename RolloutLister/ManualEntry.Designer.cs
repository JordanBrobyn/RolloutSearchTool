namespace RolloutLister
{
    partial class Manual_Entry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Manual_Entry));
            this.ACF2_List = new System.Windows.Forms.RichTextBox();
            this.done = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Button();
            this.label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ACF2_List
            // 
            this.ACF2_List.Location = new System.Drawing.Point(13, 13);
            this.ACF2_List.Name = "ACF2_List";
            this.ACF2_List.Size = new System.Drawing.Size(328, 147);
            this.ACF2_List.TabIndex = 0;
            this.ACF2_List.Text = "";
            // 
            // done
            // 
            this.done.Location = new System.Drawing.Point(187, 162);
            this.done.Name = "done";
            this.done.Size = new System.Drawing.Size(75, 23);
            this.done.TabIndex = 1;
            this.done.Text = "Submit";
            this.done.UseVisualStyleBackColor = true;
            this.done.Click += new System.EventHandler(this.done_Click);
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(266, 162);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 2;
            this.close.Text = "Cancel";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(13, 167);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(106, 13);
            this.label.TabIndex = 3;
            this.label.Text = "*Delimeters: \",!\' / \\:.\"";
            // 
            // Manual_Entry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 197);
            this.Controls.Add(this.label);
            this.Controls.Add(this.close);
            this.Controls.Add(this.done);
            this.Controls.Add(this.ACF2_List);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Manual_Entry";
            this.Text = "Manual Input";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox ACF2_List;
        private System.Windows.Forms.Button done;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.Label label;
    }
}