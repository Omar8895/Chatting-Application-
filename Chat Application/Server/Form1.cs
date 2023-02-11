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

namespace lab12_client_server
{
    public partial class Form1 : Form
    {
        byte[] bt;
        IPAddress localAddress;
        TcpListener server;
        NetworkStream nstream;
        Socket connection;
        BinaryWriter bw;
        BinaryReader br;
        string m;
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

        private void button3_Click(object sender, EventArgs e)
        {
          
            bt = new byte[] {127,0,0,1};
            localAddress = new IPAddress(bt);
            server = new TcpListener(localAddress, 1025);
            server.Start();
            connection = server.AcceptSocket();
            nstream = new NetworkStream(connection);
            MessageBox.Show("Connection Established");
        }

      
        private void button1_Click(object sender, EventArgs e)
        {
            bw = new BinaryWriter(nstream);
            bw.Write(textBox1.Text);
            listBox1.Items.Add($"Server : {textBox1.Text}");
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            br = new BinaryReader(nstream);
            listBox1.Items.Add($"Client : {br.ReadString()}");
        }


        private void button4_Click(object sender, EventArgs e)
        {
            
            br.Close();
            bw.Close();
            nstream.Close();
            connection.Shutdown(SocketShutdown.Both);
            connection.Close();
             
        }

       
    }
}
