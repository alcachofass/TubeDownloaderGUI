using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;


namespace TubeDownloaderGUI
{
    public partial class TubeDownloaderGUI : Form
    {
        //List box index
        int index = 0;
        
        //GUI starts background processes
        TubeDownloaderBackground backEnd = new TubeDownloaderBackground();

        public TubeDownloaderGUI()
        {
            InitializeComponent();            

            //check the preReq's after initialization
            int reqMet = backEnd.checkPreReqs();

            if (reqMet == 1)
            {
                MessageBox.Show("Requirements NOT met! Check for missing pre-req's in log files.");
            }           

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
            
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            //Process the download, insert into list and increase counter
            textBox2.Text = backEnd.processDL(checkBox1.Checked, radioButton1.Checked, textBox1.Text, textBox2.Text);
            listBox1.Items.Insert(index, textBox1.Text);
            index++;
     
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Enter key will default to button click (process DL)
            this.AcceptButton = button1;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }            

        private void button2_Click(object sender, EventArgs e)
        {
            
            if (listBox1.SelectedIndex == -1)
                MessageBox.Show("Nothing selected....");

            else
            {                
                backEnd.launchURL(listBox1.SelectedItem.ToString());                         
            }                     

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            backEnd.updateYTDL();           

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }


    public class TubeDownloaderBackground
    {
        string lPath = "Log\\";

        public int checkPreReqs()
        {            
                                     
            //if any pre-req's is missing, fail checks. Otherwise return 0
            if (!File.Exists("Bin\\youtube-dl.exe"))
            {
                File.AppendAllText(lPath + "errors.log", getTimeStamp() + "," + "Error: youtube-dl.exe missing!" + Environment.NewLine);
                File.AppendAllText(lPath + "errors.log", getTimeStamp() + "," + "---------------PRE-REQ-CHECK-FAIL--------------" + Environment.NewLine);

                return 1;
            }

            if (!File.Exists("Bin\\ffmpeg.exe"))
            {
                File.AppendAllText(lPath + "errors.log", getTimeStamp() + "," + "Error: ffmpeg.exe missing!" + Environment.NewLine);
                File.AppendAllText(lPath + "errors.log", getTimeStamp() + "," + "---------------PRE-REQ-CHECK-FAIL--------------" + Environment.NewLine);

                return 1;
            }

            if (!File.Exists("Bin\\ffprobe.exe"))
            {
                File.AppendAllText(lPath + "errors.log", getTimeStamp() + "," + "Error: ffprobe.exe missing!" + Environment.NewLine);
                File.AppendAllText(lPath + "errors.log", getTimeStamp() + "," + "---------------PRE-REQ-CHECK-FAIL--------------" + Environment.NewLine);

                return 1;
            }

            else
            {
                
                File.AppendAllText(lPath + "errors.log", getTimeStamp() + "," + "---------------PRE-REQ-CHECK-PASS--------------" + Environment.NewLine);
                return 0;
            }


        }

        public string processDL(bool checkBox1Checked, bool radioButton1Checked, string textBox1Text, string textBox2Text)
        {

            string playlistArgument = "";
            string mediaTypeFlag = "";
            string cPath = "\"" + Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\"";
            string effectiveCommandLine = "";
                     

            //Do we want a playlist downloaded?
            if (checkBox1Checked == true)
            {
                //We want a playlist
                playlistArgument = " ";
            }

            else
            {
                //We don't want a playlist
                playlistArgument = " --no-playlist";
            }

            if (radioButton1Checked == true)
            {
                //Video output always to desktop
                mediaTypeFlag = " -o " + cPath + "\\" + "%(title)s.%(ext)s" + "\"";
            }

            else
            {
                //MP3 output to Desktop 
                mediaTypeFlag = " --extract-audio --audio-format mp3 --audio-quality 0 -o " + cPath + "\\" + "%(title)s.%(ext)s" + " --add-metadata";
            }

            if (File.Exists("Bin\\youtube-dl.exe"))
            {

                //Pulling the files with desired flags
                Process toobit = new Process();
                toobit.StartInfo.FileName = "Bin\\youtube-dl.exe";
                toobit.StartInfo.Arguments = " \"" + textBox1Text + "\"" + playlistArgument + mediaTypeFlag;
                toobit.Start();

                //Show effectiver command line
                effectiveCommandLine = "youtube-dl.exe" + toobit.StartInfo.Arguments;
                       

                File.AppendAllText(lPath + "actions.log", getTimeStamp() + "," + "Started conversion with command line: " + effectiveCommandLine  + Environment.NewLine);
           
            }

            else
            {
               
                MessageBox.Show("youtube-dl.exe missing!");
                File.AppendAllText(lPath + "errors.log", getTimeStamp() + "," + "Error: youtube-dl.exe missing!" + Environment.NewLine);
            }

            //effectiveCommandLine is returned to the GUI
            return effectiveCommandLine;
            
        }

        public void updateYTDL()
        {             
           

            if (File.Exists("Bin\\youtube-dl.exe"))
            {
                Process toobit = new Process();
                toobit.StartInfo.FileName = "Bin\\youtube-dl.exe";
                toobit.StartInfo.Arguments = " -U";
                toobit.Start();

                File.AppendAllText(lPath + "actions.log", getTimeStamp() + "," + "Triggered native auto-updater"  + Environment.NewLine);
            }
            else
            {
                MessageBox.Show("youtube-dl.exe missing!");
                File.AppendAllText(lPath + "errors.log", getTimeStamp() + "," + "Error: youtube-dl.exe missing during update check!" + Environment.NewLine);
            }
        }

        public void launchURL(string url)
        {           
                
            Process.Start("iexplore", url);
            File.AppendAllText(lPath + "actions.log", getTimeStamp() + "," + "Launched browser with URL: " + url + Environment.NewLine);
          
        }

        private string getTimeStamp()
        {
            DateTime time = DateTime.Now;
            string timeStamp = time.ToString();

            return timeStamp;
        }

    }

}
