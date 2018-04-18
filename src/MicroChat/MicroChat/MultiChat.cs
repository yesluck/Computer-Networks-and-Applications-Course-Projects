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
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace MicroChat
{
    public partial class MultiChat : Form
    {
        public struct friendInfo
        {
            public string friendName;        //用户名
            public string friendIp;          //ip地址
            public bool friendOnline;        //是否在线
            public string friendPortrait;
        }

        int send_port = 8010;
        string mesfrom;
        string username;
        string userip;
        public string state;
        public MultiChat(string userName, string userIp, int userPortrait, string[] group, string recvConversation,string mesFrom,string mesIp,string situation)
        {
            InitializeComponent();

            if (recvConversation != "")
                RecvShow(recvConversation, mesFrom, group);

            mesfrom = mesFrom;
            state = situation;
            username = userName;
            userip = userIp;
            int len = group.Length;

            friendInfo[] friend1 = new friendInfo[10];
            StreamReader fr = new StreamReader("friends.txt", Encoding.Default);
            String line;
            int count = 0;
            string[] lines = new string[5];
            while ((line = fr.ReadLine()) != null)
            {
                int tag = 0;
                lines = line.Split(' ');
                for (int k = 0; k < len; k++)           //判断是否属于被选中的朋友
                {
                    if (lines[0] == group[k])
                    {
                        tag = 1;
                        break;
                    }
                    else
                        tag = 0;
                }
                if (tag == 1)
                {
                    friend1[count].friendName = lines[0];
                    friend1[count].friendPortrait = lines[3];
                    count++;
                }
            }
            fr.Close();

            IPAddress server_ip = IPAddress.Parse("166.111.140.14");
            IPEndPoint hostEP = new IPEndPoint(server_ip, 8000);
            Socket client_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                client_socket.Connect(hostEP);
            }
            catch (Exception se)
            {
                MessageBox.Show("连接错误" + se.Message, "提示信息", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                return;
            }
            string[] recv_info = new string[count];
            int p = 0;
            for (int i = 0; i < len; i++)
            {
                string search_num;
                search_num = friend1[i].friendName;
                string search_num2 = "q" + search_num;
                byte[] bytes_send_info = new byte[1024];
                bytes_send_info = Encoding.ASCII.GetBytes(search_num2);
                try
                {
                    client_socket.Send(bytes_send_info, bytes_send_info.Length, 0);
                }
                catch (Exception ce)
                {
                    MessageBox.Show("发送错误:" + ce.Message, "提示信息", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                    return;
                }
                byte[] bytes_recv_info = new byte[1024];
                int bytes = 0;
                while (true)
                {
                    bytes = client_socket.Receive(bytes_recv_info, bytes_recv_info.Length, 0);
                    if (bytes <= 0)
                        break;
                    recv_info[i] += Encoding.ASCII.GetString(bytes_recv_info, 0, bytes);
                    break;
                }
                if (recv_info[i] != "n")
                {
                    Friend friend_list_1 = new Friend(userName, userIp, userPortrait, 1);
                    this.friends_list.Controls.Add(friend_list_1);
                    this.friends_list.Controls[p].BackColor = Color.Transparent;
                    string a = Application.StartupPath + "\\resources\\portrait" + friend1[i].friendPortrait + ".jpg";            //导入用户头像
                    friend_list_1.friendPortrait.Image = Image.FromFile(@a);
                    friend_list_1.friendPortrait.SizeMode = PictureBoxSizeMode.Zoom;
                    this.friends_list.Controls[p].Tag = friend1[i].friendPortrait;
                    friend_list_1.friendListName.Text = friend1[i].friendName;
                    friend_list_1.friendListIp.Text = recv_info[i];
                    friend_list_1.friendListSituation.Text = "在线";
                    p++;
                }
                else
                {
                    MessageBox.Show("好友" + search_num + "不在线，无法添加");
                }
            }
            client_socket.Close();
        }
        
        /// <summary>
        /// 显示收到的信息
        /// </summary>
        /// <param name="rev_info"></param>
        /// <param name="mes_from"></param>
        /// <param name="multi_fri_now"></param>
        public void RecvShow(string rev_info, string mes_from,string[] multi_fri_now)
        {
            if (this.state == "client")                     //若接收对象为群成员，则对收到的信息进行拆包，更新自己的好友列表
            {
                int n_real = multi_fri_now.Length;
                int n_before = friends_list.Controls.Count;
                int tag = 0;
                for (int i = 0; i < n_before; i++)
                {
                    Friend tem = (Friend)friends_list.Controls[i];
                    for (int j = 0; j < n_real; j++)
                    {
                        if (tem.friendListName.Text == multi_fri_now[j])
                        {
                            tag = 1;
                            break;
                        }
                        else
                            tag = 0;
                    }
                    if (tag == 0)
                    {
                        friends_list.Controls.RemoveAt(i);
                    }
                }
            }
            string[] rev_infos = rev_info.Split('$');
            rev_info = rev_infos[2];
            this.chat_text.Select(chat_text.TextLength, 0);
            this.chat_text.SelectionColor = Color.Black;
            this.chat_text.AppendText(DateTime.Now.ToLocalTime().ToString() + "  " + mes_from + "  " + "\r\n");
            this.chat_text.AppendText(rev_info + "\r\n\r\n");
            this.chat_text.SelectionStart = chat_text.TextLength;
            this.chat_text.ScrollToCaret();
            this.Refresh();
        }

        /// <summary>
        /// 群主转发收到的消息
        /// </summary>
        /// <param name="rev_info"></param>
        /// <param name="mes_from"></param>
        /// <param name="mes_from_ip"></param>
        public void ownerTrans(string rev_info, string mes_from,string mes_from_ip)
        {
            string[] rev_infos = rev_info.Split('$');
            rev_info = rev_infos[2];

            if (state == "server")
            {
                friendInfo[] multichat_fri = new friendInfo[10];
                int n = this.friends_list.Controls.Count;
                int index = 0;
                //确认群聊有几人在线
                for (int i = 0; i < n; i++)
                {
                    Friend temp = (Friend)this.friends_list.Controls[i];
                    string search_num2 = "q" + temp.friendListName.Text;
                    IPAddress server_ip1 = IPAddress.Parse("166.111.140.14");
                    IPEndPoint hostEP1 = new IPEndPoint(server_ip1, 8000);
                    Socket client_socket1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    try
                    {
                        client_socket1.Connect(hostEP1);
                    }
                    catch (Exception se)
                    {
                        MessageBox.Show("连接错误" + se.Message, "提示信息", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                        return;
                    }
                    byte[] bytes_send_info1 = new byte[1024];
                    bytes_send_info1 = Encoding.ASCII.GetBytes(search_num2);
                    try
                    {
                        client_socket1.Send(bytes_send_info1, bytes_send_info1.Length, 0);
                    }
                    catch (Exception ce)
                    {
                        MessageBox.Show("发送错误:" + ce.Message, "提示信息", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                        return;
                    }
                    string temp_recv = "";
                    byte[] bytes_recv_info = new byte[1024];
                    int bytes = 0;
                    while (true)
                    {
                        bytes = client_socket1.Receive(bytes_recv_info, bytes_recv_info.Length, 0);
                        if (bytes <= 0)
                            break;
                        temp_recv += Encoding.ASCII.GetString(bytes_recv_info, 0, bytes);
                        break;
                    }
                    client_socket1.Close();
                    if (temp_recv == "n")
                    {
                        MessageBox.Show(temp.friendListName.Text + "已经离线，将其移出群聊！");
                        this.friends_list.Controls.RemoveAt(i);
                    }
                    else
                    {
                        if (temp.friendListName.Text != mes_from)
                        {
                            multichat_fri[index].friendName = temp.friendListName.Text;
                            multichat_fri[index].friendIp = temp.friendListIp.Text;
                            index++;
                        }
                    }

                }
                n = index;
                string multi_fri_now = "";
                for (int i = 0; i < n; i++)
                {
                    multi_fri_now = multi_fri_now + " " + multichat_fri[i].friendName;
                }

                string user_send = "START_MHAT" + mes_from + mes_from_ip + "$" + multi_fri_now + "$" + rev_info;

                for (int i = 0; i < n; i++)
                {
                    IPAddress server_ip = IPAddress.Parse(multichat_fri[i].friendIp);
                    IPEndPoint hostEP = new IPEndPoint(server_ip, send_port);
                    Socket client_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    try
                    {
                        //尝试连接
                        client_socket.Connect(hostEP);
                    }
                    catch (Exception se)
                    {
                        MessageBox.Show("连接错误" + se.Message, "提示信息", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                        return;
                    }
                    byte[] bytes_send_info = new byte[1024];
                    bytes_send_info = Encoding.Unicode.GetBytes(user_send);
                    try
                    {
                        client_socket.Send(bytes_send_info, bytes_send_info.Length, 0);    //发送消息
                    }
                    catch (Exception ce)
                    {
                        MessageBox.Show("发送错误:" + ce.Message, "提示信息", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                        return;
                    }
                  
                }
            }
        }

        private void button_send_Click(object sender, EventArgs e)
        {

            if (state == "server")
            {
                friendInfo[] multichat_fri = new friendInfo[10];
                int n = this.friends_list.Controls.Count;
                int index = 0;
                //确认群聊有几人在线
                for (int i = 0; i < n; i++)
                {
                    Friend temp = (Friend)this.friends_list.Controls[i];
                    string search_num2 = "q" + temp.friendListName.Text;
                    IPAddress server_ip1 = IPAddress.Parse("166.111.140.14");
                    IPEndPoint hostEP1 = new IPEndPoint(server_ip1, 8000);
                    Socket client_socket1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    try                         //尝试连接
                    {
                        client_socket1.Connect(hostEP1);
                    }
                    catch (Exception se)
                    {
                        MessageBox.Show("连接错误" + se.Message, "提示信息", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                        return;
                    }
                    byte[] bytes_send_info1 = new byte[1024];
                    bytes_send_info1 = Encoding.ASCII.GetBytes(search_num2);
                    try                         //向主机发送请求
                    {

                        client_socket1.Send(bytes_send_info1, bytes_send_info1.Length, 0);
                    }
                    catch (Exception ce)
                    {
                        MessageBox.Show("发送错误:" + ce.Message, "提示信息", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                        return;
                    }
                    string temp_recv = "";
                    byte[] bytes_recv_info = new byte[1024];
                    int bytes = 0;
                    while (true)
                    {
                        bytes = client_socket1.Receive(bytes_recv_info, bytes_recv_info.Length, 0);
                        if (bytes <= 0)
                            break;
                        temp_recv += Encoding.ASCII.GetString(bytes_recv_info, 0, bytes);
                        break;
                    }
                    client_socket1.Close();
                    if (temp_recv == "n")
                    {
                        MessageBox.Show(temp.friendListName.Text + "不在线上！");
                        this.friends_list.Controls.RemoveAt(i);
                    }
                    else
                    {
                        multichat_fri[index].friendName = temp.friendListName.Text;
                        multichat_fri[index].friendIp = temp.friendListIp.Text;
                        index++;
                    }

                }
                string multi_fri_now = "";
                n = index;
                for (int i = 0; i < n; i++)
                {
                    multi_fri_now = multi_fri_now + " " + multichat_fri[i].friendName;
                }

                string user_send = "START_MHAT" + username + userip + "$" + multi_fri_now + "$" + user_text.Text;

                n = this.friends_list.Controls.Count;
                for (int i = 0; i < n; i++)
                {
                    IPAddress server_ip = IPAddress.Parse(multichat_fri[i].friendIp);
                    IPEndPoint hostEP = new IPEndPoint(server_ip, send_port);
                    Socket client_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    try
                    {
                        client_socket.Connect(hostEP);
                    }
                    catch (Exception se)
                    {
                        MessageBox.Show("连接错误" + se.Message, "提示信息", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                        return;
                    }
                    byte[] bytes_send_info = new byte[1024];
                    bytes_send_info = Encoding.Unicode.GetBytes(user_send);
                    try
                    {
                        client_socket.Send(bytes_send_info, bytes_send_info.Length, 0);
                    }
                    catch (Exception ce)
                    {
                        MessageBox.Show("发送错误:" + ce.Message, "提示信息", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                        return;
                    }
                }
                this.chat_text.Select(chat_text.TextLength, 0);
                this.chat_text.SelectionColor = Color.Green;
                this.chat_text.AppendText(DateTime.Now.ToLocalTime().ToString() + "  " + username + "(我)  " + "\r\n");
                this.chat_text.Select(chat_text.TextLength, 0);
                this.chat_text.SelectionColor = Color.Green;
                this.chat_text.AppendText(user_text.Text + "\r\n\r\n");
                this.chat_text.SelectionStart = chat_text.TextLength;
                this.chat_text.ScrollToCaret();
                this.Refresh();
                user_text.Text = "";

            }
            else if (state == "client")
            {
                string user_send = "START_MHAT" + username + userip + "$" + " " + "$" + user_text.Text;

                string search_num2 = "q" + mesfrom;
                IPAddress server_ip1 = IPAddress.Parse("166.111.140.14");
                IPEndPoint hostEP1 = new IPEndPoint(server_ip1, 8000);
                Socket client_socket1 = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    client_socket1.Connect(hostEP1);
                }
                catch (Exception se)
                {
                    MessageBox.Show("连接错误" + se.Message, "提示信息", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                    return;
                }
                byte[] bytes_send_info1 = new byte[1024];
                bytes_send_info1 = Encoding.ASCII.GetBytes(search_num2);
                try
                {
                    client_socket1.Send(bytes_send_info1, bytes_send_info1.Length, 0);
                }
                catch (Exception ce)
                {
                    MessageBox.Show("发送错误:" + ce.Message, "提示信息", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                    return;
                }
                byte[] bytes_recv_info = new byte[1024];
                string temp_rcv = "";
                int bytes = 0;
                while (true)
                {
                    bytes = client_socket1.Receive(bytes_recv_info, bytes_recv_info.Length, 0);
                    if (bytes <= 0)
                        break;
                    temp_rcv += Encoding.ASCII.GetString(bytes_recv_info, 0, bytes);
                    break;
                }
                client_socket1.Close();

                if (temp_rcv == "n")
                    MessageBox.Show("对不起，对方已下线，无法发送！");
                else
                {
                    IPAddress server_ip = IPAddress.Parse(temp_rcv);
                    IPEndPoint hostEP = new IPEndPoint(server_ip, send_port);
                    Socket client_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    try
                    {
                        client_socket.Connect(hostEP);
                    }
                    catch (Exception se)
                    {
                        MessageBox.Show("连接错误" + se.Message, "提示信息", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                        return;
                    }
                    byte[] bytes_send_info = new byte[1024];
                    bytes_send_info = Encoding.Unicode.GetBytes(user_send);
                    try
                    {
                        client_socket.Send(bytes_send_info, bytes_send_info.Length, 0);    //发送消息
                    }
                    catch (Exception ce)
                    {
                        MessageBox.Show("发送错误:" + ce.Message, "提示信息", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                        return;
                    }
                    this.chat_text.Select(chat_text.TextLength, 0);
                    this.chat_text.SelectionColor = Color.Green;
                    this.chat_text.AppendText(DateTime.Now.ToLocalTime().ToString() + "  " + username + "(我)  " + "\r\n");   //将自己发送的消息显示
                    this.chat_text.Select(chat_text.TextLength, 0);
                    this.chat_text.SelectionColor = Color.Green;
                    this.chat_text.AppendText(user_text.Text + "\r\n\r\n");
                    this.chat_text.SelectionStart = chat_text.TextLength;
                    this.chat_text.ScrollToCaret();
                    this.Refresh();
                    user_text.Text = "";
                }
            }
        }
    }
}
