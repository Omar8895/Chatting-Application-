using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace client
{
    
    public partial class Form1 : Form
    {
        TcpClient client;
        IPAddress localAdd;
        NetworkStream nstream;
        BinaryReader br;
        BinaryWriter bw;
        string m;
        byte[] bt;
        public Form1()
        {
            InitializeComponent();
          
            Thread th = new Thread(() =>
            {
                while (true)
                {
                    if (nstream != null)
                    {
                        br = new BinaryReader(nstream);
                        m = br.ReadString();
                        Invoke(new Action(() =>
                        {
                            listBox1.Items.Add($"Client : {m}");
                        }));
                        nstream.Flush();
                    }

                }
            });
            th.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            client = new TcpClient();
            bt = new byte[] { 127, 0, 0, 1 };
            localAdd = new IPAddress(bt);
            client.Connect(localAdd, 1025);
            nstream = client.GetStream();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bw = new BinaryWriter(nstream);
            bw.Write(textBox1.Text);
            listBox1.Items.Add($"Client :{textBox1.Text}");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            br = new BinaryReader(nstream);
            listBox1.Items.Add($"Server : {br.ReadString()}");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            br.Close();
            bw.Close();
            nstream.Close();
        }

      
    }
}
