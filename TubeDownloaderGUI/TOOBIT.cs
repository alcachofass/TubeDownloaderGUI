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
//

namespace TubeDownloaderGUI
{
    public partial class TubeDownloaderGUI : Form
    {
        public TubeDownloaderGUI()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Files are saved to the desktop for now
            string cPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string playlistArgument = "";
            string mediaTypeFlag = "";
            
            if (checkBox1.Checked == true)
            {
                //We want a playlist
                playlistArgument = " ";
            }

            else
            {
                //We don't want a playlist
                playlistArgument = " --no-playlist ";
            }

            if (radioButton1.Checked == true)
            {
                //Video output to desktop
                mediaTypeFlag = " -o " + cPath + "\\" + "%(title)s.%(ext)s" + "\"";
            }

            else
            {
                //MP3 output to desktop
                mediaTypeFlag = " --extract-audio --audio-format mp3 --audio-quality 0 -o " + cPath + "\\" + "%(title)s.%(ext)s" + "\"";
            }
          
            //Pulling the files with desired flags
            Process toobit = new Process();
            toobit.StartInfo.FileName = "youtube-dl.exe";
            toobit.StartInfo.Arguments = " \"" + textBox1.Text + "\"" + playlistArgument + mediaTypeFlag;
            toobit.Start();

            //Show effectiver command line
            textBox2.Text = "youtube-dl.exe" + toobit.StartInfo.Arguments;
            
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
