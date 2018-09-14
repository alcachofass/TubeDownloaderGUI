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

namespace TubeDownloaderGUI
{
    public partial class TOOBIT : Form
    {
        public TOOBIT()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (radioButton1.Checked == true)

            {
                Process toobit = new Process();
                toobit.StartInfo.FileName = "youtube-dl.exe";
                toobit.StartInfo.Arguments = textBox1.Text;
                toobit.Start();
                
            }
          

            if (radioButton2.Checked == true)
            {

                Process toobit = new Process();
                toobit.StartInfo.FileName = "youtube-dl.exe";
                toobit.StartInfo.Arguments = "-x --audio-format mp3 --audio-quality 0 -o %(title)s.%(ext)s " + textBox1.Text;
                toobit.Start();

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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
