using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading; 

namespace MicroChat
{
    public partial class Chat : Form
    {
        int send_port = 8010;
        int send_file_port = 8040;
        string tempRcv = "";
        string fri;

        struct friendInfo               //好友信息
        {
            public string friendName;
            public string friendIp;
            public bool friendOnline;
            public string friendPortrait;
        }
        
        /// <summary>
        /// 构造聊天界面信息
        /// </summary>
        /// <param name="title"></param>
        /// <param name="name"></param>
        /// <param name="ip"></param>
        /// <param name="portrait"></param>
        /// <param name="recvConversation"></param>
        public Chat(string title, string name, string ip, int portrait, string recvConversation)
        {
            InitializeComponent();

            if (recvConversation != "") ShowMessage(recvConversation);      //显示朋友发来的消息

            this.Text = "和" + title + "的聊天";    fri = title;
            friendInfo friendC = new friendInfo();
            string a1 = Application.StartupPath + "\\resources\\portrait" + portrait + ".jpg";
            this.user_name.Text = name;
            this.user_ip.Text = ip;

            //显示朋友用户名、IP、头像等信息
            friendC.friendName = title;
            StreamReader stream = new StreamReader("friends.txt", Encoding.Default);
            string[] oriLine = new string[5];
            String splitedLine;
            while ((splitedLine = stream.ReadLine()) != null)
            {
                oriLine = splitedLine.Split(' ');
                if (oriLine[0] == friendC.friendName)
                {
                    friendC.friendIp = oriLine[1];
                    friendC.friendPortrait = oriLine[3];
                    if (oriLine[2] == "True")
                        friendC.friendOnline = true;
                    else
                        friendC.friendOnline = false;
                }
            }
            stream.Close();



            string searchNum = "q" + title;
            IPAddress serverIp = IPAddress.Parse("166.111.140.14");
            IPEndPoint hostEP = new IPEndPoint(serverIp, 8000);
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                clientSocket.Connect(hostEP);
           
                byte[] bytesSendInfo = new byte[1024];
                bytesSendInfo = Encoding.ASCII.GetBytes(searchNum);
                try
                {
                    clientSocket.Send(bytesSendInfo, bytesSendInfo.Length, 0);
                }
                catch (Exception ce)
                {
                    MessageBox.Show("发送错误:" + ce.Message, "提示信息", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                    return;
                }
                byte[] bytesRecvInfo = new byte[1024];
                int bytes = 0;
                while (true)
                {
                    bytes = clientSocket.Receive(bytesRecvInfo, bytesRecvInfo.Length, 0);
                    if (bytes <= 0) break;
                    tempRcv += Encoding.ASCII.GetString(bytesRecvInfo, 0, bytes);
                    break;
                }
                clientSocket.Close();
                if (tempRcv == "n")     MessageBox.Show("对不起，对方已下线！");
                else
                {
                    friendIpLabel.Text = tempRcv;
                    friendNameLabel.Text = friendC.friendName;
                    string a = Application.StartupPath + "\\resources\\portrait" + friendC.friendPortrait + ".jpg";            //导入用户头像
                    portraitBox.Image = Image.FromFile(@a);
                    portraitBox.SizeMode = PictureBoxSizeMode.Zoom;
                    this.Refresh();
                }
            }
            catch (Exception se)
            {
                MessageBox.Show("连接错误" + se.Message, "提示信息", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                return;
            }
        }

        /// <summary>
        /// 在对话框显示消息
        /// </summary>
        /// <param name="recvConversation"></param>
        public void ShowMessage(string recvConversation)
        {
            string[] splitedRecvConver = new string[2];
            splitedRecvConver = recvConversation.Split('$');
            recvConversation = splitedRecvConver[1];
            this.converBox.Select(converBox.TextLength, 0);
            this.converBox.SelectionColor = Color.Black;
            this.converBox.AppendText(DateTime.Now.ToLocalTime().ToString()+"  "+fri+"  "+"\r\n");
            this.converBox.AppendText(recvConversation+"\r\n\r\n");
            this.converBox.SelectionStart = converBox.TextLength;
            this.converBox.ScrollToCaret();
            this.Refresh();
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_send_Click(object sender, EventArgs e)
        {
            string userSend = "START_CHAT" + user_name.Text + user_ip.Text + "$" + textBox.Text;        //向好友发送开始聊天的信息

            string searchnum = "q" + fri;
            IPAddress serverip = IPAddress.Parse("166.111.140.14");
            IPEndPoint hostEP1 = new IPEndPoint(serverip, 8000);
            Socket clientsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                clientsocket.Connect(hostEP1);
                byte[] bytes_send_info1 = new byte[1024];
                bytes_send_info1 = Encoding.ASCII.GetBytes(searchnum);
                try                         //向主机发送请求
                {
                    clientsocket.Send(bytes_send_info1, bytes_send_info1.Length, 0);
                }
                catch (Exception ce)
                {
                    MessageBox.Show("发送错误:" + ce.Message, "提示信息", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                    return;
                }
                byte[] bytesRecvInfo = new byte[1024];
                int bytes = 0;
                while (true)
                {
                    bytes = clientsocket.Receive(bytesRecvInfo, bytesRecvInfo.Length, 0);
                    if (bytes <= 0)
                        break;
                    tempRcv += Encoding.ASCII.GetString(bytesRecvInfo, 0, bytes);
                    break;
                }
                clientsocket.Close();

                if (tempRcv == "n")
                    MessageBox.Show("对不起，对方已下线，无法发送！");
                else
                {
                    IPAddress serverIp = IPAddress.Parse(friendIpLabel.Text);
                    IPEndPoint hostEP = new IPEndPoint(serverIp, send_port);
                    Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    try
                    {
                        clientSocket.Connect(hostEP);
                    }
                    catch (Exception se)
                    {
                        MessageBox.Show("连接错误" + se.Message, "提示信息", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                        return;
                    }
                    byte[] bytesSendInfo = new byte[1024];
                    bytesSendInfo = Encoding.Unicode.GetBytes(userSend);
                    try
                    {
                        clientSocket.Send(bytesSendInfo, bytesSendInfo.Length, 0);    //发送消息
                    }
                    catch (Exception ce)
                    {
                        MessageBox.Show("发送错误:" + ce.Message, "提示信息", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                        return;
                    }
                    this.converBox.Select(converBox.TextLength, 0);
                    this.converBox.SelectionColor = Color.Green;
                    this.converBox.AppendText(DateTime.Now.ToLocalTime().ToString() + "  " + user_name.Text + "(我)  " + "\r\n");   //将自己发送的消息显示
                    this.converBox.Select(converBox.TextLength, 0);
                    this.converBox.SelectionColor = Color.Green;
                    this.converBox.AppendText(textBox.Text + "\r\n\r\n");
                    this.converBox.SelectionStart = converBox.TextLength;
                    this.converBox.ScrollToCaret();
                    this.Refresh();
                    textBox.Text = "";
                }
            }
            catch (Exception se)
            {
                MessageBox.Show("连接错误" + se.Message, "提示信息", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                return;
            }
        }
        
        /// <summary>
        /// 【功能5】发送文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public int fileTag = 0;
        Socket fileSocket;

        private void button_file_send_Click(object sender, EventArgs e)
        {
            string searchnum = "q" + fri;

            IPAddress serverip = IPAddress.Parse("166.111.140.14");
            IPEndPoint hostEP1 = new IPEndPoint(serverip, 8000);
            Socket clientsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try                         //尝试连接
            {
                clientsocket.Connect(hostEP1);

                byte[] bytesSendInfo = new byte[1024];
                bytesSendInfo = Encoding.ASCII.GetBytes(searchnum);
                try                         //向主机发送请求
                {
                    clientsocket.Send(bytesSendInfo, bytesSendInfo.Length, 0);
                }
                catch (Exception ce)
                {
                    MessageBox.Show(ce.Message, "发送错误", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                    return;
                }
                byte[] bytesRecvInfo = new byte[1024];
                int bytes = 0;
                while (true)
                {
                    bytes = clientsocket.Receive(bytesRecvInfo, bytesRecvInfo.Length, 0);
                    if (bytes <= 0)
                        break;
                    tempRcv += Encoding.ASCII.GetString(bytesRecvInfo, 0, bytes);
                    break;
                }
                clientsocket.Close();

                if (tempRcv == "n")
                    MessageBox.Show("对不起，对方不在线，无法发送文件！");
                else
                {
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string fileName = openFileDialog1.FileName;
                        Socket sendFile = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        sendFile.Connect(friendIpLabel.Text, send_port);
                        string send_text = "SEND_FILES" + user_name.Text + user_ip.Text + "$" + fileName;
                        byte[] bytes0 = Encoding.Unicode.GetBytes(send_text);
                        sendFile.Send(bytes0);
                        sendFile.Close();
                    }
                }
            }
            catch (Exception se)
            {
                MessageBox.Show(se.Message, "连接错误", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                return;
            }
        }

        /// <summary>
        /// 示意开始发送文件
        /// </summary>
        public void fileSign()
        {
            fileSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            fileSocket.Connect(friendIpLabel.Text, send_file_port);                //向8030端口发送文件
        }

        /// <summary>
        /// 发送文件
        /// </summary>
        public void fileSend()
        {
            int len;
            FileStream fileStream = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
            NetworkStream stream = new NetworkStream(fileSocket);
            Thread.Sleep(1000);                                 //停止进程一段时间，保证对方已经准备好接收
            byte[] fileRead = new byte[1024];
            int i = 0;
            len = fileStream.Read(fileRead, 0, 1024);
            while (len != 0)                                    //以1024为单位循环发送，直到fl为0
            {
                stream.Write(fileRead, 0, len);
                stream.Flush();
                i++;
                len = fileStream.Read(fileRead, 0, 1024);
            }
            fileStream.Close();                 
            MessageBox.Show("文件已成功发送。");
            fileSocket.Close();
            stream.Close();
        }
    }
}
