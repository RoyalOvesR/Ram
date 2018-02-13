using Microsoft.Win32;
using Ram.SAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ram
{
    public partial class Form1 : Form
    {
        SystemInfo si;
        DrawIcon di;
        int f;
        bool autorun;

        public Form1()
        {
            InitializeComponent();
            si = new SystemInfo();
            di = new DrawIcon(this);
            autorun = false;
            f = 0;
        }

        public void getUsed()
        {
            di.SetTaskIconDynamic(si.getCPUUsed(),si.getMemoryUsed());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            di.setCpuColor(colorDialog1.Color);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            getUsed();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            di.setTip(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            di.setTip(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            di.setMemColor(colorDialog1.Color);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            di.setFonColor(colorDialog1.Color);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (di.getTip() == 3)
            {
                di.setTip(0);
            }
            else
            {
                di.setTip(3);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (f == -1)
            {
                timer1.Interval = 1000;
                f = 1;
            }
            if (f == 0)
            {
                timer1.Interval = 500;
                f = -1;
            }
            if (f == 1)
            {
                timer1.Interval = 1500;
                f = 0;
            }
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Visible = true;
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            if (!autorun)
            {
                RegistryKey rk = Registry.LocalMachine;
                RegistryKey rk2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                rk2.SetValue("autorun", Application.ExecutablePath);
                rk2.Close();
                rk.Close();
                autorun = true;
            }
            else
            {
                string path = Application.ExecutablePath;
                RegistryKey rk = Registry.LocalMachine;
                RegistryKey rk2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                rk2.DeleteValue("autorun", false);
                rk2.Close();
                rk.Close();
                autorun = false;
            }
            
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            MessageBox.Show("更你妹啊！！", "FBI Warning...");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
            {
                timer1.Enabled = true;
            }
            else
            {
                timer1.Enabled = false;
                this.notifyIcon1.Icon = this.Icon;
            }
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            di.upFontSize();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            di.downFontSize();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            di.setFont(fontDialog1.Font);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


    }
}
