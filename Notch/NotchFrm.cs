using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Notch
{
    public partial class NotchFrm : Form
    {
        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const UInt32 SWP_NOSIZE = 0x0001;
        private const UInt32 SWP_NOMOVE = 0x0002;
        private const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        private ContextMenu cm;

        private Bitmap NotchBmp;

        public NotchFrm()
        {
            InitializeComponent();
        }

        private void NotchFrm_Load(object sender, EventArgs e)
        {
            cm = new ContextMenu();
            cm.MenuItems.Add("Notch 1.0");
            cm.MenuItems.Add("Adam Blunt 2019");
            cm.MenuItems.Add("-");
            cm.MenuItems.Add("https://adam.lgbt", new EventHandler(OpenLGBT));
            cm.MenuItems.Add("https://github.com/adamxp12", new EventHandler(OpenGit));
            cm.MenuItems.Add("-");
            cm.MenuItems.Add("Exit", new EventHandler(Close));
            cm.MenuItems[0].Enabled = false;
            cm.MenuItems[1].Enabled = false;
            NotchBmp = Properties.Resources.notch5;
            NotchBmp.MakeTransparent(NotchBmp.GetPixel(0, 0));
            SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
        }

        private void NotchFrm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(NotchBmp, this.Width / 2 - NotchBmp.Width / 2, 0);
        }

        private void OpenLGBT(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://adam.lgbt");
        }

        private void OpenGit(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/adamxp12");
        }

        private void Close(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NotchFrm_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button.Equals(MouseButtons.Right))
            {
                cm.Show(this, e.Location);
            }
        }
    }
}
