using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileSizeChecker
{
    public partial class FileChecker : Form
    {
        const long MAX_FILE_SIZE = 99000000;

        string filesFound = "";

        public FileChecker()
        {
            InitializeComponent();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.DoWork +=
                new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(
            backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged +=
                new ProgressChangedEventHandler(
            backgroundWorker1_ProgressChanged);

            richTextBox1.Text =
                       "Loaded.\n" + richTextBox1.Text;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            //textBox1.Text = Directory.GetCurrentDirectory();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy != true && !button1.Text.Contains("Cancel"))
            {
                // Start the asynchronous operation.
                //button1.Text = "Cancel";
                button1.Enabled = false;
                richTextBox1.Text =
                       "Searching.\n" + richTextBox1.Text;
                filesFound = "";

                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void GetFilesOver100()
        {
            string[] files = Directory.GetFileSystemEntries( Directory.GetParent( Directory.GetCurrentDirectory() ).FullName );

            FileInfo fileData;

            foreach (string file in files)
            {
                fileData = new FileInfo(file);
                if (fileData.Length > MAX_FILE_SIZE)
                {
                    richTextBox1.Text = richTextBox1.Text +
                        file + " is over limit.\n";
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            string[] files = Directory.GetFiles(textBox1.Text , "*", SearchOption.AllDirectories);
            filesFound = "";
            filesFound =
                       "Files " + files.Length + " found.\n" + filesFound;

            FileInfo fileData;

            int count = 0;

            foreach (string file in files)
            {
                fileData = new FileInfo(file);
                count++;
                if (fileData.Length > MAX_FILE_SIZE)
                {
                    filesFound = 
                        "FILE IS OVERSIZED: " + file + "\n" + filesFound;
                }

                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    // Perform a time consuming operation and report progress.
                    // System.Threading.Thread.Sleep(500);
                    int percent = (int)(( (float)count / (float)files.Length ) * 100f);
                    worker.ReportProgress(percent);
                }
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                richTextBox1.Text = "Cancelled!\n" + richTextBox1.Text;
                button1.Enabled = true;
                filesFound = "";
            }
            else if (e.Error != null)
            {
                richTextBox1.Text = "Error: " + e.Error.Message + "\n" + richTextBox1.Text;
                button1.Enabled = true;
                filesFound = "";
            }
            else
            {
                richTextBox1.Text = "Done!\n\n" + filesFound + richTextBox1.Text;
                button1.Enabled = true;
                filesFound = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.WorkerSupportsCancellation == true)
            {
                // Cancel the asynchronous operation.
                backgroundWorker1.CancelAsync();
            }
        }
    }
}
