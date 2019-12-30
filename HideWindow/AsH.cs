using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace HideWindow
{
    public partial class AsH : Form
    {
        string curItem = null;
        int hWnd = 0;
        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;
        [DllImport("User32")]
        private static extern int ShowWindow(int hwnd, int nCmdShow);
        //WORKING
        public AsH()
        {
            InitializeComponent();
        }

        private void Hide(object sender, EventArgs e)
        {
            if (curItem == null)
            {
                MessageBox.Show("Please Choose a Window to hide");
            }
            else
            {
                Process[] processRunning = Process.GetProcesses();
                foreach (Process pr in processRunning)
                {
                    if (pr.ProcessName == curItem)
                    {
                        hWnd = pr.MainWindowHandle.ToInt32();
                        ShowWindow(hWnd, SW_HIDE);
                    }
                }
            }

        }

        private void Show(object sender, EventArgs e)
        {
            if (hWnd != 0)
            {
                ShowWindow(hWnd, SW_SHOW);
                hWnd = 0;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Process[] processlist = Process.GetProcesses();

            foreach (Process process in processlist)
            {
                if (!String.IsNullOrEmpty(process.MainWindowTitle))
                {
                    listBox1.Items.Add(process.MainWindowTitle);
                }
            }
        }



        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }



        private void terminateToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Process[] prs = Process.GetProcesses();
            foreach (Process pr in prs)
            {
                if (pr.ProcessName == curItem)
                {

                    pr.Kill();

                }

            }
            Environment.Exit(0);
        }
        string ChildName2 = Process.GetCurrentProcess().ProcessName;
        int hWnd2 = 0;

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Process[] processRunning = Process.GetProcesses();
                foreach (Process pr in processRunning)
                {
                    if (pr.ProcessName == ChildName2)
                    {
                        hWnd2 = pr.MainWindowHandle.ToInt32();
                        ShowWindow(hWnd2, SW_HIDE);
                    }
                }
            }
        }
        private void showToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (hWnd2 != 0)
            {
                ShowWindow(hWnd2, SW_SHOW);
                hWnd2 = 0;
            }
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string title = listBox1.SelectedItem.ToString();
            Process[] processlist = Process.GetProcesses();
            bool windowfound= false;
            foreach (Process process in processlist)
            {
                if (String.Equals(process.MainWindowTitle, title))
                {
                    curItem = process.ProcessName;
                    windowfound = true;
                }
                else
                {

                }
            }
            if (windowfound)
            {

            }
            else
            {
                MessageBox.Show("Window not found!");
                listBox1.Items.Clear();
                Process[] processlist2 = Process.GetProcesses();

                foreach (Process process2 in processlist2)
                {
                    if (!String.IsNullOrEmpty(process2.MainWindowTitle))
                    {
                        listBox1.Items.Add(process2.MainWindowTitle);
                    }
                }
            }
        }
    

    private void Reload(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            Process[] processlist = Process.GetProcesses();

            foreach (Process process in processlist)
            {
                if (!String.IsNullOrEmpty(process.MainWindowTitle))
                {
                    listBox1.Items.Add(process.MainWindowTitle);
                }
            }
        }
    }
}
