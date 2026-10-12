using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;

using System.Text;
using System.Xml;
using System.Windows.Forms;


namespace IPA_Sorter
{
    public partial class MainForm
    {
        string[] configSeparators;
        string folderIn = "";
        string folderOut = "";
        string fileExtension;

        public MainForm()
        {
            InitializeComponent();

            this.Text += " " + Application.ProductVersion;

            LoadMyConfig();
        }


        private string ConfigSingleString(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }


        private string[] ConfigMultiString(string key)
        {
            return ConfigurationManager.AppSettings[key].Split(configSeparators, StringSplitOptions.RemoveEmptyEntries);
        }


        private void LoadMyConfig()
        {
            configSeparators = new string[] {ConfigurationManager.AppSettings["separator"]};

            fileExtension = ConfigSingleString("fileExtension");

            textBoxUnprocessed.Text = ConfigSingleString("errorPrefix");

            Program.plistPaths = ConfigMultiString("fileList");
            textBoxExtractSuffixes.Text = ConfigSingleString("suffixList");

            textBoxPatternDevices.Text = ConfigSingleString("patternDevices");
            textBoxPatternIosX.Text = ConfigSingleString("patternIosX");
            textBoxNoCracker.Text = ConfigSingleString("noCracker");

            textBoxRenamePattern.Text = ConfigSingleString("patternRename");
            textBoxSortPattern.Text = ConfigSingleString("patternSort");
        }


        private void GroupBoxFolderEffect(GroupBox groupBox, bool enable)
        {
            groupBox.ForeColor = enable ? Color.OrangeRed : SystemColors.ControlText;
        }


        // This event occurs when the user drags over the form with 
        // the mouse during a drag drop operation 
        private void groupBoxFolder_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ?
                DragDropEffects.Copy : DragDropEffects.None;

            GroupBoxFolderEffect(groupBoxFolder, true);
        }

        private void groupBoxFolder_DragLeave(object sender, EventArgs e)
        {
            GroupBoxFolderEffect(groupBoxFolder, false);
        }

        // Occurs when the user releases the mouse over the drop target 
        private void groupBoxFolder_DragDrop(object sender, DragEventArgs e)
        {
            GroupBoxFolderEffect(groupBoxFolder, false);

            if (Program.isTaskOn)
                return;

            // Extract the data from the DataObject-Container into a string list
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            if (FileList.Length == 1  &&  Directory.Exists(FileList[0]))
                FolderInSelected(FileList[0]);
            else
            {
                if (MessageBox.Show("This is not a folder, do you want to process some potential IPAs ?",
                        this.Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    RunProgram(FileList);

                    if (folderIn == "")
                        buttonRun.Enabled = false;
                }

            }
                
        }


        //******************************************************************************


        public string GetUnprocessedPrefix()
        {
            return checkBoxUnprocessed.Checked ? textBoxUnprocessed.Text : "";
        }


        private void FolderInSelected(string folder)
        {
            folderIn = folder;
            labelFolderIn.Text = folderIn + "\\";
     
            buttonCount.Enabled = true;

            groupBoxTasks.Enabled = true;
            groupBoxOptions.Enabled = true;
            buttonRun.Enabled = true;
        }


        private void buttonFolderIn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();

            folderDialog.Description = "Select IPA Folder";
            folderDialog.ShowNewFolderButton = false;
            folderDialog.SelectedPath = folderIn == "" ? System.AppDomain.CurrentDomain.BaseDirectory : folderIn;

            DialogResult result = folderDialog.ShowDialog();
            if(result == DialogResult.OK)
            {
                FolderInSelected(folderDialog.SelectedPath);
            }
        }


        private void buttonCount_Click(object sender, EventArgs e)
        {
            buttonCount.Enabled = false;
            buttonCount.Text = "Counting";
            Application.DoEvents();

            buttonCount.Text = "" + GetFilesFromFolder(folderIn).Length + " IPA";
            buttonCount.Enabled = true;
        }


        private void SelectFolderOut()
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();

            folderDialog.Description = "Select Destination Folder";
            folderDialog.SelectedPath = folderOut == "" ? folderIn : folderOut;

            DialogResult result = folderDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                folderOut = folderDialog.SelectedPath;
                labelFolderOut.Text = folderOut + "\\";
            }
            else
            {
                checkBoxOutFolder.Checked = false;
            }
        }


        public string GetPatternOption(string name)
        {
            switch(name)
            {
                case "Devices": return textBoxPatternDevices.Text;
                case "IosX": return textBoxPatternIosX.Text;
                case "Cracker": return textBoxNoCracker.Text;
                default: return null;
            }
        }


        int progressBarPos;

        public void ProgressBarInitialize(int max)
        {
            progressBarTotal.Minimum = 0;
            progressBarTotal.Maximum = max;
            progressBarTotal.Step = 1;
            progressBarTotal.Value = 0;
            progressBarPos = 0;
        }

        public void ProgressBarStep(string filename)
        {
            progressBarPos++;

            progressBarTotal.Value = progressBarPos;
            // Trick to refresh fast enough... Awful but works !
            progressBarTotal.Value = progressBarPos - 1;
            progressBarTotal.Value = progressBarPos;

            progressBarTotal.Refresh();

            //progressBarTotal.PerformStep();
            //progressBarTotal.Update();
            //Application.DoEvents();

            int max = progressBarTotal.Maximum;
            int percent = (int)(100.0*progressBarPos / max);

            labelInfo1.Text = "Processing File " + progressBarPos + " / " + max + " (" + percent + "%) / "
                + Program.filesUnprocessed + " Errors";

            labelInfo2.Text = Path.GetFileName(filename);

            Application.DoEvents();
        }


        public void ProgressBarFinished(int filesUnprocessed, int logEntries)
        {
            labelInfo1.Text = "Processed " + progressBarPos + " Files";
            labelInfo2.Text = "" + filesUnprocessed + " Errors - " + logEntries + " log entries";

            this.Activate();
        }



        private void checkBoxUnprocessed_CheckedChanged(object sender, EventArgs e)
        {
            textBoxUnprocessed.Enabled = checkBoxUnprocessed.Checked;
        }


        private void checkBoxOutFolder_CheckedChanged(object sender, EventArgs e)
        {
            bool outFolderChecked = checkBoxOutFolder.Checked;
            labelFolderOut.Enabled = outFolderChecked;

            folderOut = "";
            labelFolderOut.Text = "No Folder Selected";

            if (outFolderChecked)
                SelectFolderOut();
        }


        private string[] GetFilesFromFolder(string folder)
        {
            try
            {
                return Directory.GetFiles(folder, "*." + fileExtension,
                    checkBoxSubfolders.Checked ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Cannot Access one of the Folders / Files", this.Text);
                return new string[0];
            }

        }


        private string[] GetSuffixes(string groupedSuffixes)
        {
            string[] suffixesExploded = groupedSuffixes.Split('|');
            int nb = 0;

            foreach (string suffix in suffixesExploded)
            {
                if (suffix.Length > 0)
                {
                    nb++;
                }
            }

            int nbNeeded = Program.plistPaths.Length;
            if (nb != nbNeeded)
            {
                MessageBox.Show("Needs " + nbNeeded.ToString() + " Suffixes", this.Text);
                return null;
            }

            return suffixesExploded;
        }


        private void RunProgram(string[] files)
        {
            string[] suffixesExploded = GetSuffixes(textBoxExtractSuffixes.Text);
            if (suffixesExploded == null)
                return;

            buttonUndo.Enabled = false;

            string buttonRunOriginalText = buttonRun.Text;
            buttonRun.Text = "CANCEL";

            if (radioExtract.Checked)
            {
                Program.RunExtract(files, folderOut, suffixesExploded);
            }
            else
            {
                Program.RunRenameSort(files, folderOut, fileExtension,
                    checkBoxExternalPlist.Checked, suffixesExploded,
                    checkBoxRename.Checked, textBoxRenamePattern.Text,
                    checkBoxSort.Checked, textBoxSortPattern.Text
                );
            }

            buttonRun.Text = buttonRunOriginalText;

            buttonUndo.Enabled = true;
        }


        private void buttonRun_Click(object sender, EventArgs e)
        {
            if (Program.isTaskOn) {
                if(MessageBox.Show("Stop the Current Tasks ?", this.Text, MessageBoxButtons.YesNo)
                    == DialogResult.Yes)
                    Program.isTaskOn = false;

                return;
            }

            RunProgram(GetFilesFromFolder(folderIn));
        }


        private void buttonUndo_Click(object sender, EventArgs e)
        {
            int nbActions = Program.Undo();

            labelInfo1.Text = "Undone " + nbActions + " Actions";
            labelInfo2.Text = "Files and Folders should be back to their original state";

            buttonUndo.Enabled = false;
        }


        private void radioExtract_CheckedChanged(object sender, EventArgs e)
        {
            textBoxExtractSuffixes.Enabled = radioExtract.Checked;
        }


        private void checkBoxRename_CheckedChanged(object sender, EventArgs e)
        {
            textBoxRenamePattern.Enabled = checkBoxRename.Checked;
            textBoxNoCracker.Enabled = checkBoxRename.Checked;
        }


        private void checkBoxSort_CheckedChanged(object sender, EventArgs e)
        {
            textBoxSortPattern.Enabled = checkBoxSort.Checked;
            textBoxPatternIosX.Enabled = checkBoxSort.Checked;
        }

        private void radioRenameSort_CheckedChanged(object sender, EventArgs e)
        {
            panelRenameSort.Enabled = radioRenameSort.Checked;
        }

    }

}
