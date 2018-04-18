using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;

namespace MicroChat
{
    public partial class NewFriend : Form
    {
        int send_port = 8010;
        public struct friendInfo
        {
            public string friendName;
            public string friendIp;
            public bool friendOnline;
            public int friendPortrait;
        }

        string userName;
        string userIp;
        public NewFriend(string username, string userip)
        {
            InitializeComponent();
            userName = username;
            userIp = userip;
        }

        public void friendRespon(string res,string mes_from,string mes_from_ip)
        {
            if (res == "ACCPT_ADDF")
            {
                MessageBox.Show("对方接受了您的好友请求，现在你们可以开始聊天了！");
                Random rad = new Random();
                int value = rad.Next(1, 5);
                int picture_num = 1;
                picture_num = value;                //头像
                FileStream fs1 = new FileStream("friends.txt", FileMode.Append);
                byte[] data = System.Text.Encoding.Default.GetBytes(mes_from + " " + mes_from_ip + " " + "True" + " " + picture_num + "\r\n");
                fs1.Write(data, 0, data.Length);
                fs1.Flush();
                fs1.Close();
                MessageBox.Show("添加成功！");
                Main temp = (Main)this.Owner;
                this.Close();
            }
            else
            {
                MessageBox.Show("很遗憾，对方拒绝了您的好友请求。");
            }
        }

        public void addFriend(string mes_from, string mes_from_ip)
        {
            Random rad = new Random();
            int value = rad.Next(1, 5);
            int picture_num = 1;
            picture_num = value;                //头像
            FileStream fs1 = new FileStream("friends.txt", FileMode.Append);
            byte[] data = System.Text.Encoding.Default.GetBytes(mes_from + " " + mes_from_ip + " " + "True" + " " + picture_num + "\r\n");
            fs1.Write(data, 0, data.Length);
            fs1.Flush();
            fs1.Close();
            MessageBox.Show("添加成功！");
            Main temp = (Main)this.Owner;
            this.Close();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            string search_num;
            search_num = searchNum.Text.ToString();                  //得到被查询的学号
            if (search_num.Length != 10)
            {
                MessageBox.Show("查询学号不存在，请确认！");
            }
            else
            {
                string search_num2 = "q" + search_num;
                IPAddress server_ip = IPAddress.Parse("166.111.140.14");
                IPEndPoint hostEP = new IPEndPoint(server_ip, 8000);
                Socket client_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try                         //尝试连接
                {

                    client_socket.Connect(hostEP);
                }
                catch (Exception se)
                {
                    MessageBox.Show("连接错误" + se.Message, "提示信息", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                    return;
                }
                byte[] bytes_send_info = new byte[1024];
                bytes_send_info = Encoding.ASCII.GetBytes(search_num2);
                try                         //向主机发送请求
                {

                    client_socket.Send(bytes_send_info, bytes_send_info.Length, 0);
                }
                catch (Exception ce)
                {
                    MessageBox.Show("发送错误:" + ce.Message, "提示信息", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                    return;
                }
                string recv_info = "";                         //声明接收返回内容的字符串
                byte[] bytes_recv_info = new byte[1024];       //声明字节数组，一次接收数据的长度为1024字节
                int bytes = 0;                                  //实际接受数据长度
                while (true)
                {
                    bytes = client_socket.Receive(bytes_recv_info, bytes_recv_info.Length, 0);
                    if (bytes <= 0)                                                         //读取完成后退出循环
                        break;
                    recv_info += Encoding.ASCII.GetString(bytes_recv_info, 0, bytes);   //将读取的字节数转换为字符串
                    break;
                }
                client_socket.Close();
                friendInfo friend1;
                //初始化
                friend1.friendName = "";
                friend1.friendIp = "";
                friend1.friendOnline = false;
                if (recv_info == "n")
                {
                    MessageBox.Show("该用户不在线，无法添加，请稍后询问！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (recv_info == "Incorrect No.")
                {
                    MessageBox.Show("查询学号不存在，请确认！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    friend1.friendIp = recv_info;                     //ip
                    friend1.friendName = search_num;                  //学号
                    friend1.friendOnline = true;                      //是否在线
                    Random rad = new Random();
                    int value = rad.Next(1, 5);
                    friend1.friendPortrait = value;                //头像
                    DialogResult r = MessageBox.Show("确认添加" + search_num + "为好友？", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (r == DialogResult.OK)
                    {
                        FileStream fs = new FileStream("friends.txt", FileMode.Append);
                        fs.Close();

                        StreamReader fr = new StreamReader("friends.txt", Encoding.Default);
                        String line;
                        int tag = 0;                            //标记该朋友是否已经存在
                        string[] lines = new string[5];
                        while ((line = fr.ReadLine()) != null)
                        {
                            lines = line.Split(' ');
                            if (lines[0] == friend1.friendName)
                            {
                                tag = 1;                        //该朋友已经存在
                                break;
                            }
                            else
                                tag = 0;
                        }
                        fr.Close();
                        if (tag == 0)                               //若该朋友不存在
                        {
                            string friend_ask = "ADD_FRIEND" + userName + userIp + "$";
                            IPAddress server_ip1 = IPAddress.Parse(recv_info);                  //对方ip
                            IPEndPoint hostEP1 = new IPEndPoint(server_ip1, send_port);                   //对方端口地址
                            Socket client_socket1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                            try
                            {
                                //尝试连接
                                client_socket1.Connect(hostEP1);
                            }
                            catch (Exception se)
                            {
                                MessageBox.Show("连接错误" + se.Message, "提示信息", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                                return;
                            }
                            byte[] bytes_send_info1 = new byte[1024];
                            bytes_send_info1 = Encoding.Unicode.GetBytes(friend_ask);
                            try
                            {
                                client_socket1.Send(bytes_send_info1, bytes_send_info1.Length, 0);    //发送消息
                            }
                            catch (Exception ce)
                            {
                                MessageBox.Show("发送错误:" + ce.Message, "提示信息", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                                return;
                            }
                            client_socket1.Close();
                        }
                        else
                        {
                            MessageBox.Show("该好友已经存在！");
                        }
                    }
                }
            }
        }
    }
}
