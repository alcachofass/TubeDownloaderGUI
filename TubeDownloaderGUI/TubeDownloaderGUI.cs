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

        //Log path
        string lPath = "Log\\";

        //Files are saved to the desktop for now
        string cPath = "\"" + Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\"";
          

        public TubeDownloaderGUI()
        {
            InitializeComponent();

            //Check for prereqs & log if necessary
            string timeStamp = getTimeStamp();

            File.AppendAllText(lPath + "actions.log", timeStamp + "," + "---------------LAUNCHED---------------" + Environment.NewLine);
            File.AppendAllText(lPath + "errors.log", timeStamp + "," + "---------------LAUNCHED---------------" + Environment.NewLine);

            if (!File.Exists("Bin\\youtube-dl.exe"))
            {
                MessageBox.Show("youtube-dl.exe missing!");
                File.AppendAllText(lPath + "errors.log", timeStamp + "," + "Error: youtube-dl.exe missing!" + Environment.NewLine);
            }

            if (!File.Exists("Bin\\ffmpeg.exe"))
            {
                MessageBox.Show("ffmpeg.exe missing!");
                File.AppendAllText(lPath + "errors.log", timeStamp + "," + "Error: ffmpeg.exe missing!" + Environment.NewLine);
            }

            if (!File.Exists("Bin\\ffprobe.exe"))
            {
                MessageBox.Show("ffprobe.exe missing!");
                File.AppendAllText(lPath + "errors.log", timeStamp + "," + "Error: ffprobe.exe missing!" + Environment.NewLine);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
          
        }
        
        private void button1_Click(object sender, EventArgs e)
        {            
            string playlistArgument = "";
            string mediaTypeFlag = "";
            
            //Update list box for visual tracking            
            listBox1.Items.Insert(index, textBox1.Text);            
            index++;
                                    
            //Do we want a playlist?
            if (checkBox1.Checked == true)
            {
                //We want a playlist
                playlistArgument = " ";
            }

            else
            {
                //We don't want a playlist
                playlistArgument = " --no-playlist";
            }

            if (radioButton1.Checked == true)
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
                toobit.StartInfo.Arguments = " \"" + textBox1.Text + "\"" + playlistArgument + mediaTypeFlag;
                toobit.Start();

                //Show effectiver command line
                textBox2.Text = "youtube-dl.exe" + toobit.StartInfo.Arguments;

                string timeStamp = getTimeStamp();

                File.AppendAllText(lPath + "actions.log", timeStamp + "," + "Started conversion with command line: " + textBox2.Text.ToString() + Environment.NewLine);                
            }

            else
            {
                string timeStamp = getTimeStamp();

                MessageBox.Show("youtube-dl.exe missing!");
                File.AppendAllText(lPath + "errors.log", timeStamp + "," + "Error: youtube-dl.exe missing!"  + Environment.NewLine);
            }          
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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
            string timeStamp = getTimeStamp();

            if (listBox1.SelectedIndex == -1)
                MessageBox.Show("Nothing selected....");
            else
            {
                string url = listBox1.SelectedItem.ToString();
                Process.Start("iexplore", url);

                File.AppendAllText(lPath + "actions.log", timeStamp + "," + "Launched browser with URL: " + url +  Environment.NewLine);
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string timeStamp = getTimeStamp();

            if (File.Exists("Bin\\youtube-dl.exe"))
            {
                Process toobit = new Process();
                toobit.StartInfo.FileName = "Bin\\youtube-dl.exe";
                toobit.StartInfo.Arguments = " -U";
                toobit.Start();

                File.AppendAllText(lPath + "actions.log", timeStamp + "," + "Triggered native auto-updater"  + Environment.NewLine);
            }
            else
            {
                MessageBox.Show("youtube-dl.exe missing!");
            }
                      
        }

        private string getTimeStamp()
        {
            DateTime time = DateTime.Now;
            string timeStamp = time.ToString();

            return timeStamp;
        }

    }
}
