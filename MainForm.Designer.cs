
using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.Remoting;
using System.Threading;
using System.Windows.Forms;


namespace IPA_Sorter
{
    partial class MainForm : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.progressBarTotal = new System.Windows.Forms.ProgressBar();
            this.buttonFolderIn = new System.Windows.Forms.Button();
            this.labelFolderIn = new System.Windows.Forms.Label();
            this.checkBoxSubfolders = new System.Windows.Forms.CheckBox();
            this.groupBoxFolder = new System.Windows.Forms.GroupBox();
            this.buttonCount = new System.Windows.Forms.Button();
            this.textBoxRenamePattern = new System.Windows.Forms.TextBox();
            this.labelFolderOut = new System.Windows.Forms.Label();
            this.textBoxSortPattern = new System.Windows.Forms.TextBox();
            this.groupBoxTasks = new System.Windows.Forms.GroupBox();
            this.panelRenameSort = new System.Windows.Forms.Panel();
            this.textBoxNoCracker = new System.Windows.Forms.TextBox();
            this.checkBoxExternalPlist = new System.Windows.Forms.CheckBox();
            this.checkBoxRename = new System.Windows.Forms.CheckBox();
            this.labelPatternIosX = new System.Windows.Forms.Label();
            this.textBoxPatternDevices = new System.Windows.Forms.TextBox();
            this.textBoxPatternIosX = new System.Windows.Forms.TextBox();
            this.labelRenamePattern = new System.Windows.Forms.Label();
            this.checkBoxSort = new System.Windows.Forms.CheckBox();
            this.labelPatternCracker = new System.Windows.Forms.Label();
            this.labelMovePattern = new System.Windows.Forms.Label();
            this.labelPatternDevice = new System.Windows.Forms.Label();
            this.radioRenameSort = new System.Windows.Forms.RadioButton();
            this.radioExtract = new System.Windows.Forms.RadioButton();
            this.labelExtractSuffixes = new System.Windows.Forms.Label();
            this.textBoxExtractSuffixes = new System.Windows.Forms.TextBox();
            this.labelInfo1 = new System.Windows.Forms.Label();
            this.labelInfo2 = new System.Windows.Forms.Label();
            this.checkBoxUnprocessed = new System.Windows.Forms.CheckBox();
            this.textBoxUnprocessed = new System.Windows.Forms.TextBox();
            this.groupBoxOptions = new System.Windows.Forms.GroupBox();
            this.checkBoxOutFolder = new System.Windows.Forms.CheckBox();
            this.buttonRun = new System.Windows.Forms.Button();
            this.buttonUndo = new System.Windows.Forms.Button();
            this.groupBoxFolder.SuspendLayout();
            this.groupBoxTasks.SuspendLayout();
            this.panelRenameSort.SuspendLayout();
            this.groupBoxOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBarTotal
            // 
            this.progressBarTotal.Location = new System.Drawing.Point(12, 440);
            this.progressBarTotal.Name = "progressBarTotal";
            this.progressBarTotal.Size = new System.Drawing.Size(274, 17);
            this.progressBarTotal.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarTotal.TabIndex = 0;
            // 
            // buttonFolderIn
            // 
            this.buttonFolderIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFolderIn.Location = new System.Drawing.Point(8, 19);
            this.buttonFolderIn.Name = "buttonFolderIn";
            this.buttonFolderIn.Size = new System.Drawing.Size(64, 23);
            this.buttonFolderIn.TabIndex = 1;
            this.buttonFolderIn.Text = "Select ...";
            this.buttonFolderIn.UseVisualStyleBackColor = true;
            this.buttonFolderIn.Click += new System.EventHandler(this.buttonFolderIn_Click);
            // 
            // labelFolderIn
            // 
            this.labelFolderIn.AutoSize = true;
            this.labelFolderIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFolderIn.Location = new System.Drawing.Point(6, 51);
            this.labelFolderIn.Name = "labelFolderIn";
            this.labelFolderIn.Size = new System.Drawing.Size(98, 13);
            this.labelFolderIn.TabIndex = 2;
            this.labelFolderIn.Text = "No Folder Selected";
            // 
            // checkBoxSubfolders
            // 
            this.checkBoxSubfolders.AutoSize = true;
            this.checkBoxSubfolders.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxSubfolders.Location = new System.Drawing.Point(81, 23);
            this.checkBoxSubfolders.Name = "checkBoxSubfolders";
            this.checkBoxSubfolders.Size = new System.Drawing.Size(114, 17);
            this.checkBoxSubfolders.TabIndex = 2;
            this.checkBoxSubfolders.Text = "Include Subfolders";
            this.checkBoxSubfolders.UseVisualStyleBackColor = true;
            // 
            // groupBoxFolder
            // 
            this.groupBoxFolder.AllowDrop = true;
            this.groupBoxFolder.Controls.Add(this.buttonCount);
            this.groupBoxFolder.Controls.Add(this.labelFolderIn);
            this.groupBoxFolder.Controls.Add(this.buttonFolderIn);
            this.groupBoxFolder.Controls.Add(this.checkBoxSubfolders);
            this.groupBoxFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxFolder.Location = new System.Drawing.Point(12, 13);
            this.groupBoxFolder.Name = "groupBoxFolder";
            this.groupBoxFolder.Size = new System.Drawing.Size(275, 72);
            this.groupBoxFolder.TabIndex = 9;
            this.groupBoxFolder.TabStop = false;
            this.groupBoxFolder.Text = "1 - Folder To Process";
            this.groupBoxFolder.DragDrop += new System.Windows.Forms.DragEventHandler(this.groupBoxFolder_DragDrop);
            this.groupBoxFolder.DragEnter += new System.Windows.Forms.DragEventHandler(this.groupBoxFolder_DragEnter);
            this.groupBoxFolder.DragLeave += new System.EventHandler(this.groupBoxFolder_DragLeave);
            // 
            // buttonCount
            // 
            this.buttonCount.Enabled = false;
            this.buttonCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCount.Location = new System.Drawing.Point(201, 19);
            this.buttonCount.Name = "buttonCount";
            this.buttonCount.Size = new System.Drawing.Size(67, 23);
            this.buttonCount.TabIndex = 3;
            this.buttonCount.Text = "IPA ?";
            this.buttonCount.UseVisualStyleBackColor = true;
            this.buttonCount.Click += new System.EventHandler(this.buttonCount_Click);
            // 
            // textBoxRenamePattern
            // 
            this.textBoxRenamePattern.AccessibleDescription = "";
            this.textBoxRenamePattern.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxRenamePattern.Location = new System.Drawing.Point(47, 71);
            this.textBoxRenamePattern.Name = "textBoxRenamePattern";
            this.textBoxRenamePattern.Size = new System.Drawing.Size(193, 20);
            this.textBoxRenamePattern.TabIndex = 31;
            // 
            // labelFolderOut
            // 
            this.labelFolderOut.AutoSize = true;
            this.labelFolderOut.Enabled = false;
            this.labelFolderOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFolderOut.Location = new System.Drawing.Point(110, 44);
            this.labelFolderOut.Name = "labelFolderOut";
            this.labelFolderOut.Size = new System.Drawing.Size(98, 13);
            this.labelFolderOut.TabIndex = 5;
            this.labelFolderOut.Text = "No Folder Selected";
            // 
            // textBoxSortPattern
            // 
            this.textBoxSortPattern.Enabled = false;
            this.textBoxSortPattern.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSortPattern.Location = new System.Drawing.Point(47, 119);
            this.textBoxSortPattern.Name = "textBoxSortPattern";
            this.textBoxSortPattern.Size = new System.Drawing.Size(193, 20);
            this.textBoxSortPattern.TabIndex = 42;
            // 
            // groupBoxTasks
            // 
            this.groupBoxTasks.AllowDrop = true;
            this.groupBoxTasks.Controls.Add(this.panelRenameSort);
            this.groupBoxTasks.Controls.Add(this.radioRenameSort);
            this.groupBoxTasks.Controls.Add(this.radioExtract);
            this.groupBoxTasks.Controls.Add(this.labelExtractSuffixes);
            this.groupBoxTasks.Controls.Add(this.textBoxExtractSuffixes);
            this.groupBoxTasks.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxTasks.Location = new System.Drawing.Point(12, 91);
            this.groupBoxTasks.Name = "groupBoxTasks";
            this.groupBoxTasks.Size = new System.Drawing.Size(275, 233);
            this.groupBoxTasks.TabIndex = 19;
            this.groupBoxTasks.TabStop = false;
            this.groupBoxTasks.Text = "2 - Tasks";
            // 
            // panelRenameSort
            // 
            this.panelRenameSort.Controls.Add(this.textBoxNoCracker);
            this.panelRenameSort.Controls.Add(this.checkBoxExternalPlist);
            this.panelRenameSort.Controls.Add(this.checkBoxRename);
            this.panelRenameSort.Controls.Add(this.labelPatternIosX);
            this.panelRenameSort.Controls.Add(this.textBoxRenamePattern);
            this.panelRenameSort.Controls.Add(this.textBoxPatternDevices);
            this.panelRenameSort.Controls.Add(this.textBoxPatternIosX);
            this.panelRenameSort.Controls.Add(this.labelRenamePattern);
            this.panelRenameSort.Controls.Add(this.checkBoxSort);
            this.panelRenameSort.Controls.Add(this.labelPatternCracker);
            this.panelRenameSort.Controls.Add(this.labelMovePattern);
            this.panelRenameSort.Controls.Add(this.labelPatternDevice);
            this.panelRenameSort.Controls.Add(this.textBoxSortPattern);
            this.panelRenameSort.Location = new System.Drawing.Point(25, 85);
            this.panelRenameSort.Name = "panelRenameSort";
            this.panelRenameSort.Size = new System.Drawing.Size(243, 142);
            this.panelRenameSort.TabIndex = 55;
            // 
            // textBoxNoCracker
            // 
            this.textBoxNoCracker.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNoCracker.Location = new System.Drawing.Point(188, 49);
            this.textBoxNoCracker.Name = "textBoxNoCracker";
            this.textBoxNoCracker.Size = new System.Drawing.Size(52, 20);
            this.textBoxNoCracker.TabIndex = 23;
            // 
            // checkBoxExternalPlist
            // 
            this.checkBoxExternalPlist.AutoSize = true;
            this.checkBoxExternalPlist.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxExternalPlist.Location = new System.Drawing.Point(3, 3);
            this.checkBoxExternalPlist.Name = "checkBoxExternalPlist";
            this.checkBoxExternalPlist.Size = new System.Drawing.Size(211, 17);
            this.checkBoxExternalPlist.TabIndex = 53;
            this.checkBoxExternalPlist.Text = "Use External Plist Files (Suffixes above)";
            this.checkBoxExternalPlist.UseVisualStyleBackColor = true;
            // 
            // checkBoxRename
            // 
            this.checkBoxRename.AutoSize = true;
            this.checkBoxRename.Checked = true;
            this.checkBoxRename.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRename.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxRename.Location = new System.Drawing.Point(3, 51);
            this.checkBoxRename.Name = "checkBoxRename";
            this.checkBoxRename.Size = new System.Drawing.Size(90, 17);
            this.checkBoxRename.TabIndex = 54;
            this.checkBoxRename.Text = "Rename Files";
            this.checkBoxRename.UseVisualStyleBackColor = true;
            this.checkBoxRename.CheckedChanged += new System.EventHandler(this.checkBoxRename_CheckedChanged);
            // 
            // labelPatternIosX
            // 
            this.labelPatternIosX.AutoSize = true;
            this.labelPatternIosX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPatternIosX.Location = new System.Drawing.Point(128, 100);
            this.labelPatternIosX.Name = "labelPatternIosX";
            this.labelPatternIosX.Size = new System.Drawing.Size(44, 13);
            this.labelPatternIosX.TabIndex = 1;
            this.labelPatternIosX.Text = "_IOSX_";
            // 
            // textBoxPatternDevices
            // 
            this.textBoxPatternDevices.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPatternDevices.Location = new System.Drawing.Point(72, 23);
            this.textBoxPatternDevices.Name = "textBoxPatternDevices";
            this.textBoxPatternDevices.Size = new System.Drawing.Size(168, 20);
            this.textBoxPatternDevices.TabIndex = 21;
            // 
            // textBoxPatternIosX
            // 
            this.textBoxPatternIosX.Enabled = false;
            this.textBoxPatternIosX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPatternIosX.Location = new System.Drawing.Point(178, 97);
            this.textBoxPatternIosX.Name = "textBoxPatternIosX";
            this.textBoxPatternIosX.Size = new System.Drawing.Size(62, 20);
            this.textBoxPatternIosX.TabIndex = 22;
            // 
            // labelRenamePattern
            // 
            this.labelRenamePattern.AutoSize = true;
            this.labelRenamePattern.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRenamePattern.Location = new System.Drawing.Point(0, 74);
            this.labelRenamePattern.Name = "labelRenamePattern";
            this.labelRenamePattern.Size = new System.Drawing.Size(41, 13);
            this.labelRenamePattern.TabIndex = 32;
            this.labelRenamePattern.Text = "Pattern";
            // 
            // checkBoxSort
            // 
            this.checkBoxSort.AutoSize = true;
            this.checkBoxSort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxSort.Location = new System.Drawing.Point(3, 99);
            this.checkBoxSort.Name = "checkBoxSort";
            this.checkBoxSort.Size = new System.Drawing.Size(93, 17);
            this.checkBoxSort.TabIndex = 33;
            this.checkBoxSort.Text = "Sort in Folders";
            this.checkBoxSort.UseVisualStyleBackColor = true;
            this.checkBoxSort.CheckedChanged += new System.EventHandler(this.checkBoxSort_CheckedChanged);
            // 
            // labelPatternCracker
            // 
            this.labelPatternCracker.AutoSize = true;
            this.labelPatternCracker.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPatternCracker.Location = new System.Drawing.Point(121, 52);
            this.labelPatternCracker.Name = "labelPatternCracker";
            this.labelPatternCracker.Size = new System.Drawing.Size(61, 13);
            this.labelPatternCracker.TabIndex = 4;
            this.labelPatternCracker.Text = "No Cracker";
            // 
            // labelMovePattern
            // 
            this.labelMovePattern.AutoSize = true;
            this.labelMovePattern.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMovePattern.Location = new System.Drawing.Point(0, 122);
            this.labelMovePattern.Name = "labelMovePattern";
            this.labelMovePattern.Size = new System.Drawing.Size(41, 13);
            this.labelMovePattern.TabIndex = 34;
            this.labelMovePattern.Text = "Pattern";
            // 
            // labelPatternDevice
            // 
            this.labelPatternDevice.AutoSize = true;
            this.labelPatternDevice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPatternDevice.Location = new System.Drawing.Point(1, 26);
            this.labelPatternDevice.Name = "labelPatternDevice";
            this.labelPatternDevice.Size = new System.Drawing.Size(65, 13);
            this.labelPatternDevice.TabIndex = 0;
            this.labelPatternDevice.Text = "_DEVICES_";
            // 
            // radioRenameSort
            // 
            this.radioRenameSort.AutoSize = true;
            this.radioRenameSort.Checked = true;
            this.radioRenameSort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioRenameSort.Location = new System.Drawing.Point(9, 66);
            this.radioRenameSort.Name = "radioRenameSort";
            this.radioRenameSort.Size = new System.Drawing.Size(166, 17);
            this.radioRenameSort.TabIndex = 44;
            this.radioRenameSort.TabStop = true;
            this.radioRenameSort.Text = "Rename or/and Sort IPA Files";
            this.radioRenameSort.UseVisualStyleBackColor = true;
            this.radioRenameSort.CheckedChanged += new System.EventHandler(this.radioRenameSort_CheckedChanged);
            // 
            // radioExtract
            // 
            this.radioExtract.AutoSize = true;
            this.radioExtract.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioExtract.Location = new System.Drawing.Point(9, 17);
            this.radioExtract.Name = "radioExtract";
            this.radioExtract.Size = new System.Drawing.Size(108, 17);
            this.radioExtract.TabIndex = 43;
            this.radioExtract.Text = "Extract PList Files";
            this.radioExtract.UseVisualStyleBackColor = true;
            this.radioExtract.CheckedChanged += new System.EventHandler(this.radioExtract_CheckedChanged);
            // 
            // labelExtractSuffixes
            // 
            this.labelExtractSuffixes.AutoSize = true;
            this.labelExtractSuffixes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelExtractSuffixes.Location = new System.Drawing.Point(27, 43);
            this.labelExtractSuffixes.Name = "labelExtractSuffixes";
            this.labelExtractSuffixes.Size = new System.Drawing.Size(44, 13);
            this.labelExtractSuffixes.TabIndex = 13;
            this.labelExtractSuffixes.Text = "Suffixes";
            // 
            // textBoxExtractSuffixes
            // 
            this.textBoxExtractSuffixes.Enabled = false;
            this.textBoxExtractSuffixes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxExtractSuffixes.Location = new System.Drawing.Point(77, 40);
            this.textBoxExtractSuffixes.Name = "textBoxExtractSuffixes";
            this.textBoxExtractSuffixes.Size = new System.Drawing.Size(189, 20);
            this.textBoxExtractSuffixes.TabIndex = 11;
            // 
            // labelInfo1
            // 
            this.labelInfo1.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelInfo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInfo1.Location = new System.Drawing.Point(12, 467);
            this.labelInfo1.Name = "labelInfo1";
            this.labelInfo1.Size = new System.Drawing.Size(274, 16);
            this.labelInfo1.TabIndex = 13;
            this.labelInfo1.Text = "No Task";
            this.labelInfo1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelInfo2
            // 
            this.labelInfo2.Location = new System.Drawing.Point(12, 488);
            this.labelInfo2.Name = "labelInfo2";
            this.labelInfo2.Size = new System.Drawing.Size(274, 16);
            this.labelInfo2.TabIndex = 15;
            this.labelInfo2.Text = "Drag and Drop a Folder or IPA Files";
            this.labelInfo2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBoxUnprocessed
            // 
            this.checkBoxUnprocessed.AutoSize = true;
            this.checkBoxUnprocessed.Checked = true;
            this.checkBoxUnprocessed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUnprocessed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxUnprocessed.Location = new System.Drawing.Point(9, 19);
            this.checkBoxUnprocessed.Name = "checkBoxUnprocessed";
            this.checkBoxUnprocessed.Size = new System.Drawing.Size(142, 17);
            this.checkBoxUnprocessed.TabIndex = 50;
            this.checkBoxUnprocessed.Text = "Unprocessed Files Prefix";
            this.checkBoxUnprocessed.UseVisualStyleBackColor = true;
            this.checkBoxUnprocessed.CheckedChanged += new System.EventHandler(this.checkBoxUnprocessed_CheckedChanged);
            // 
            // textBoxUnprocessed
            // 
            this.textBoxUnprocessed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxUnprocessed.Location = new System.Drawing.Point(155, 16);
            this.textBoxUnprocessed.Name = "textBoxUnprocessed";
            this.textBoxUnprocessed.Size = new System.Drawing.Size(111, 20);
            this.textBoxUnprocessed.TabIndex = 51;
            // 
            // groupBoxOptions
            // 
            this.groupBoxOptions.Controls.Add(this.labelFolderOut);
            this.groupBoxOptions.Controls.Add(this.checkBoxOutFolder);
            this.groupBoxOptions.Controls.Add(this.checkBoxUnprocessed);
            this.groupBoxOptions.Controls.Add(this.textBoxUnprocessed);
            this.groupBoxOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxOptions.Location = new System.Drawing.Point(12, 330);
            this.groupBoxOptions.Name = "groupBoxOptions";
            this.groupBoxOptions.Size = new System.Drawing.Size(275, 65);
            this.groupBoxOptions.TabIndex = 52;
            this.groupBoxOptions.TabStop = false;
            this.groupBoxOptions.Text = "3 - Options";
            // 
            // checkBoxOutFolder
            // 
            this.checkBoxOutFolder.AutoSize = true;
            this.checkBoxOutFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxOutFolder.Location = new System.Drawing.Point(9, 43);
            this.checkBoxOutFolder.Name = "checkBoxOutFolder";
            this.checkBoxOutFolder.Size = new System.Drawing.Size(96, 17);
            this.checkBoxOutFolder.TabIndex = 52;
            this.checkBoxOutFolder.Text = "Output Folder :";
            this.checkBoxOutFolder.UseVisualStyleBackColor = true;
            this.checkBoxOutFolder.CheckedChanged += new System.EventHandler(this.checkBoxOutFolder_CheckedChanged);
            // 
            // buttonRun
            // 
            this.buttonRun.Enabled = false;
            this.buttonRun.Location = new System.Drawing.Point(151, 406);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(90, 23);
            this.buttonRun.TabIndex = 53;
            this.buttonRun.Text = "RUN Checked";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // buttonUndo
            // 
            this.buttonUndo.Enabled = false;
            this.buttonUndo.Location = new System.Drawing.Point(56, 406);
            this.buttonUndo.Name = "buttonUndo";
            this.buttonUndo.Size = new System.Drawing.Size(90, 23);
            this.buttonUndo.TabIndex = 54;
            this.buttonUndo.Text = "Undo Last Run";
            this.buttonUndo.UseVisualStyleBackColor = true;
            this.buttonUndo.Click += new System.EventHandler(this.buttonUndo_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 513);
            this.Controls.Add(this.buttonUndo);
            this.Controls.Add(this.buttonRun);
            this.Controls.Add(this.groupBoxOptions);
            this.Controls.Add(this.labelInfo2);
            this.Controls.Add(this.labelInfo1);
            this.Controls.Add(this.groupBoxTasks);
            this.Controls.Add(this.progressBarTotal);
            this.Controls.Add(this.groupBoxFolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Ultinio IPA";
            this.groupBoxFolder.ResumeLayout(false);
            this.groupBoxFolder.PerformLayout();
            this.groupBoxTasks.ResumeLayout(false);
            this.groupBoxTasks.PerformLayout();
            this.panelRenameSort.ResumeLayout(false);
            this.panelRenameSort.PerformLayout();
            this.groupBoxOptions.ResumeLayout(false);
            this.groupBoxOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ProgressBar progressBarTotal;
        private Button buttonFolderIn;
        private Label labelFolderIn;
        private CheckBox checkBoxSubfolders;
        private GroupBox groupBoxFolder;
        private Button buttonCount;
        private TextBox textBoxRenamePattern;
        private Label labelFolderOut;
        private GroupBox groupBoxTasks;
        private Label labelInfo1;
        private Label labelExtractSuffixes;
        private TextBox textBoxExtractSuffixes;
        private TextBox textBoxSortPattern;
        private Label labelInfo2;
        private TextBox textBoxPatternIosX;
        private TextBox textBoxPatternDevices;
        private Label labelPatternIosX;
        private Label labelPatternDevice;
        private Label labelPatternCracker;
        private TextBox textBoxNoCracker;
        private CheckBox checkBoxUnprocessed;
        private TextBox textBoxUnprocessed;
        private GroupBox groupBoxOptions;
        private Label labelMovePattern;
        private CheckBox checkBoxSort;
        private Label labelRenamePattern;
        private CheckBox checkBoxOutFolder;
        private Button buttonRun;
        private Button buttonUndo;
        private CheckBox checkBoxExternalPlist;
        private RadioButton radioRenameSort;
        private RadioButton radioExtract;
        private CheckBox checkBoxRename;
        private Panel panelRenameSort;

    }
}

