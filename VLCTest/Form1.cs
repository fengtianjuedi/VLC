using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;

namespace VLCTest
{
    public partial class Form1 : Form
    {
        public LibVLC libVLC;
        public MediaPlayer mp;

        public Form1()
        {
            if (!DesignMode)
            {
                Core.Initialize();
            }
            InitializeComponent();
            libVLC = new LibVLC();
            mp = new MediaPlayer(libVLC);
            videoView1.MediaPlayer = mp;
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            mp.Playing += mp_Playing;
            mp.Buffering += mp_Buffering;
            mp.Opening += mp_Opening;
            mp.EncounteredError += mp_EncounteredError;
            
            mp.SetVideoTitleDisplay(Position.Center, 5000);
            bool result = mp.Play(new Media(libVLC, "rtsp://wowzaec2demo.streamlock.net/vod/mp4:BigBuckBunny_115j.mov", FromType.FromLocation));
            if (!result)
                MessageBox.Show("播放失败");
        }

        void mp_EncounteredError(object sender, EventArgs e)
        {
            MessageBox.Show("无信号");
        }

        void mp_Opening(object sender, EventArgs e)
        {
            MessageBox.Show("连接中");
        }

        void mp_Buffering(object sender, MediaPlayerBufferingEventArgs e)
        {
            if (e.Cache == 100)
                MessageBox.Show("缓冲成功");
            else
                Console.WriteLine("缓冲中");
        }

        void mp_Playing(object sender, EventArgs e)
        {
            MessageBox.Show("连接成功");
        }
    }
}
