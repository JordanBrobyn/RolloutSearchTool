using System;
using System.Windows.Forms;
using Microsoft.ConfigurationManagement.ManagementProvider.WqlQueryEngine;
using Microsoft.ConfigurationManagement.ManagementProvider;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Text;
using System.Windows.Shell;
namespace RolloutLister
{
    partial class Main
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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            menuStrip1.Enabled = false;
            save_Selected.Enabled = false;
            
            menuStrip1.Enabled = true;

            

        }

        protected override void OnFormClosed(FormClosedEventArgs e) {
            if(scope != null)
                scope.Close();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.acf2_Box = new System.Windows.Forms.GroupBox();
            this.nodeList = new System.Windows.Forms.DataGridView();
            this.save_Selected = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addACF2ListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.active_Viewing = new System.Windows.Forms.Label();
            this.ACF2_Tree = new System.Windows.Forms.TreeView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.amountInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.ETAtoolStripStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.foundPage = new System.Windows.Forms.TabPage();
            this.rejectionPage = new System.Windows.Forms.TabPage();
            this.rejectionBox = new System.Windows.Forms.ListBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.exporter = new System.Windows.Forms.FolderBrowserDialog();
            this.importer = new System.Windows.Forms.OpenFileDialog();
            this.acf2_Box.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nodeList)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.foundPage.SuspendLayout();
            this.rejectionPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // acf2_Box
            // 
            this.acf2_Box.AutoSize = true;
            this.acf2_Box.Controls.Add(this.nodeList);
            this.acf2_Box.Controls.Add(this.save_Selected);
            this.acf2_Box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.acf2_Box.Location = new System.Drawing.Point(0, 0);
            this.acf2_Box.Name = "acf2_Box";
            this.acf2_Box.Size = new System.Drawing.Size(599, 416);
            this.acf2_Box.TabIndex = 1;
            this.acf2_Box.TabStop = false;
            this.acf2_Box.Text = "Info";
            // 
            // nodeList
            // 
            this.nodeList.AllowUserToAddRows = false;
            this.nodeList.AllowUserToDeleteRows = false;
            this.nodeList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.nodeList.BackgroundColor = System.Drawing.SystemColors.Control;
            this.nodeList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.nodeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nodeList.Location = new System.Drawing.Point(3, 16);
            this.nodeList.Name = "nodeList";
            this.nodeList.ReadOnly = true;
            this.nodeList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.nodeList.Size = new System.Drawing.Size(593, 374);
            this.nodeList.TabIndex = 0;
            this.nodeList.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.sortButton_Click);
            // 
            // save_Selected
            // 
            this.save_Selected.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.save_Selected.Location = new System.Drawing.Point(3, 390);
            this.save_Selected.Name = "save_Selected";
            this.save_Selected.Size = new System.Drawing.Size(593, 23);
            this.save_Selected.TabIndex = 6;
            this.save_Selected.Text = "Save Selected";
            this.save_Selected.UseVisualStyleBackColor = true;
            this.save_Selected.Click += new System.EventHandler(this.save_Selected_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(853, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.importToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.importToolStripMenuItem.Text = "Import";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addACF2ListToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // addACF2ListToolStripMenuItem
            // 
            this.addACF2ListToolStripMenuItem.Name = "addACF2ListToolStripMenuItem";
            this.addACF2ListToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.addACF2ListToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.addACF2ListToolStripMenuItem.Text = "Add ACF2 List Manually";
            this.addACF2ListToolStripMenuItem.Click += new System.EventHandler(this.addACF2ListToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // active_Viewing
            // 
            this.active_Viewing.AutoSize = true;
            this.active_Viewing.Location = new System.Drawing.Point(203, 38);
            this.active_Viewing.Name = "active_Viewing";
            this.active_Viewing.Size = new System.Drawing.Size(0, 13);
            this.active_Viewing.TabIndex = 3;
            // 
            // ACF2_Tree
            // 
            this.ACF2_Tree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ACF2_Tree.Location = new System.Drawing.Point(3, 3);
            this.ACF2_Tree.Name = "ACF2_Tree";
            this.ACF2_Tree.Size = new System.Drawing.Size(226, 384);
            this.ACF2_Tree.TabIndex = 4;
            this.ACF2_Tree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ACF2_Tree_AfterSelect);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.progressBar,
            this.amountInfo,
            this.toolStripStatusCount,
            this.toolStripStatusUser,
            this.ETAtoolStripStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 440);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(853, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(42, 17);
            this.toolStripStatusLabel1.Text = "Status:";
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // amountInfo
            // 
            this.amountInfo.Name = "amountInfo";
            this.amountInfo.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusCount
            // 
            this.toolStripStatusCount.Name = "toolStripStatusCount";
            this.toolStripStatusCount.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusUser
            // 
            this.toolStripStatusUser.Name = "toolStripStatusUser";
            this.toolStripStatusUser.Size = new System.Drawing.Size(0, 17);
            // 
            // ETAtoolStripStatus
            // 
            this.ETAtoolStripStatus.Name = "ETAtoolStripStatus";
            this.ETAtoolStripStatus.Size = new System.Drawing.Size(104, 17);
            this.ETAtoolStripStatus.Text = "ETA : 0:00 Minutes";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.foundPage);
            this.tabControl.Controls.Add(this.rejectionPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.MaximumSize = new System.Drawing.Size(240, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(240, 416);
            this.tabControl.TabIndex = 7;
            // 
            // foundPage
            // 
            this.foundPage.Controls.Add(this.ACF2_Tree);
            this.foundPage.Location = new System.Drawing.Point(4, 22);
            this.foundPage.Name = "foundPage";
            this.foundPage.Padding = new System.Windows.Forms.Padding(3);
            this.foundPage.Size = new System.Drawing.Size(232, 390);
            this.foundPage.TabIndex = 0;
            this.foundPage.Text = "Found ACF2\'s";
            this.foundPage.UseVisualStyleBackColor = true;
            // 
            // rejectionPage
            // 
            this.rejectionPage.Controls.Add(this.rejectionBox);
            this.rejectionPage.Location = new System.Drawing.Point(4, 22);
            this.rejectionPage.Name = "rejectionPage";
            this.rejectionPage.Padding = new System.Windows.Forms.Padding(3);
            this.rejectionPage.Size = new System.Drawing.Size(232, 0);
            this.rejectionPage.TabIndex = 1;
            this.rejectionPage.Text = "Rejections";
            this.rejectionPage.UseVisualStyleBackColor = true;
            // 
            // rejectionBox
            // 
            this.rejectionBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rejectionBox.FormattingEnabled = true;
            this.rejectionBox.Location = new System.Drawing.Point(3, 3);
            this.rejectionBox.Name = "rejectionBox";
            this.rejectionBox.Size = new System.Drawing.Size(226, 0);
            this.rejectionBox.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.MaximumSize = new System.Drawing.Size(900, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl);
            this.splitContainer1.Panel1MinSize = 250;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.acf2_Box);
            this.splitContainer1.Panel2MinSize = 400;
            this.splitContainer1.Size = new System.Drawing.Size(853, 416);
            this.splitContainer1.SplitterDistance = 250;
            this.splitContainer1.TabIndex = 8;
            // 
            // importer
            // 
            this.importer.FileName = "openFileDialog1";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 462);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.active_Viewing);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(950, 1024);
            this.MinimumSize = new System.Drawing.Size(500, 250);
            this.Name = "Main";
            this.Text = "Rollout Search";
            this.acf2_Box.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nodeList)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.foundPage.ResumeLayout(false);
            this.rejectionPage.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox acf2_Box;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addACF2ListToolStripMenuItem;
        private System.Windows.Forms.DataGridView nodeList;
        private System.Windows.Forms.Label active_Viewing;
        private System.Windows.Forms.TreeView ACF2_Tree;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.Button save_Selected;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage foundPage;
        private System.Windows.Forms.TabPage rejectionPage;
        private System.Windows.Forms.ListBox rejectionBox;
        private SplitContainer splitContainer1;
        private FolderBrowserDialog exporter;
        private OpenFileDialog importer;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripStatusLabel amountInfo;
        private ToolStripStatusLabel toolStripStatusCount;
        private ToolStripStatusLabel toolStripStatusUser;
        private ToolStripStatusLabel ETAtoolStripStatus;
    }
}

