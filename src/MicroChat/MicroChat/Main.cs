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
    public partial class Main : Form
    {
        int theme = 0;                  //根据头像确定的主界面主题
        int chatMaxNum = 10;

        int listenFilePort = 8040;
        int sendPort = 8010;
        int listenPort = 8010;
        TcpListener ordinListener;
        TcpListener fileListener;
        Thread t1;
        Thread t2;
        
        public string[] chattList;
        int chatListLoc = 0;

        /// <summary>
        /// 个人信息
        /// </summary>
        string myName;                  //用户名字
        string myIp;                    //用户IP
        int myPortrait;                 //用户头像

        /// <summary>
        /// 朋友信息
        /// </summary>
        public struct FriendInfo
        {
            public string friendName;       //用户名
            public string friendIp;         //IP地址
            public bool friendOnline;       //是否在线
            public string friendPortrait;   //朋友头像
        }
        int friendNum = 0;                  //朋友数量

        /// <summary>
        /// 构造主界面成分
        /// </summary>
        /// <param name="portrait"></param>
        /// <param name="userName"></param>
        /// <param name="userIp"></param>
        public Main(int portrait, string userName,string userIp)
        {
            InitializeComponent();
            
            //TCP初始化
            ordinListener = new TcpListener(listenPort);
            fileListener = new TcpListener(listenFilePort);
            ordinListener.Start();
            fileListener.Start();

            //显示个人信息并修改背景风格
            myName = userName;
            myIp = userIp;
            myPortrait = portrait; 
            usernameLabel.Text = myName;
            ipLabel.Text = myIp;
            string portraitPath = Application.StartupPath + "\\resources\\portrait" + portrait.ToString() + ".jpg";
            this.portraitBox.Image = Image.FromFile(portraitPath);
            if (portrait > 1)
            {
                this.BackgroundImage = Resource1.MainBGsy;
                usernameLabel.ForeColor = Color.White;
                ipLabel.ForeColor = Color.White;
                addButton.ForeColor = Color.White;
                situationButton.ForeColor = Color.White;
                multichatButton.ForeColor = Color.White;
                quitButton.ForeColor = Color.White;
                theme = 1;
            }

            ///读取“friends.txt”文件，显示好友列表
            FileStream fs = new FileStream("friends.txt", FileMode.Append);
            fs.Close();
            FriendInfo[] friend = new FriendInfo[10];
            StreamReader fileStream = new StreamReader("friends.txt", Encoding.Default);
            String oriLine;
            string[] splitedLine = new string[5];
            //将文件中的各个信息分别读取并存储
            while ((oriLine = fileStream.ReadLine()) != null)
            {
                splitedLine = oriLine.Split(' ');
                friend[friendNum].friendName = splitedLine[0];
                friend[friendNum].friendIp = splitedLine[1];
                friend[friendNum].friendPortrait = splitedLine[3];
                friendNum++;
            }
            //对每个记录在案的朋友，新建朋友对象
            for (int i = 0; i < friendNum; i++)
            {
                Friend friendA = new Friend(myName, myIp, myPortrait, theme);
                this.friendsList.Controls.Add(friendA);
                this.friendsList.Controls[i].BackColor = Color.Transparent;
                portraitPath = Application.StartupPath + "\\resources\\portrait" + friend[i].friendPortrait + ".jpg";            //导入用户头像
                friendA.friendPortrait.Image = Image.FromFile(portraitPath);
                friendA.friendPortrait.SizeMode = PictureBoxSizeMode.Zoom;
                this.friendsList.Controls[i].Tag = friend[i].friendPortrait;
                friendA.friendListName.Text = friend[i].friendName;
                friendA.friendListIp.Text = "未知，请刷新";
                friendA.friendListSituation.Text = "未知，请刷新";
            }

            //初始化聊天窗口信息
            chattList = new string[chatMaxNum];
            for (int i = 0; i < chatMaxNum; i++)
            {
                chattList[i] = "\0";
            }
        }

        /// <summary>
        /// 初始化完毕后，进行监听操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Load(object sender, EventArgs e)
        {
            t1 = new Thread(listening);
            t1.IsBackground = true;
            t1.Start();
        }

        /// <summary>
        /// 【功能2】“在线状态”：刷新好友列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        int flushFlag = 0;
        private void situationButton_Click(object sender, EventArgs e)
        {
            IPAddress server_ip = IPAddress.Parse("166.111.140.14");
            IPEndPoint hostEP = new IPEndPoint(server_ip, 8000);
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            flushFlag = 1;
            t1.Suspend();
            friendsList.Controls.Clear();
            FriendInfo[] friendS = new FriendInfo[10];
            StreamReader fileStream = new StreamReader("friends.txt", Encoding.Default);
            String oriLine;
            int count = 0;
            string[] splitedLine = new string[5];

            //读取“friends.txt”文件中的好友列表
            while ((oriLine = fileStream.ReadLine()) != null)
            {
                splitedLine = oriLine.Split(' ');
                friendS[count].friendName = splitedLine[0];
                friendS[count].friendIp = splitedLine[1];
                friendS[count].friendPortrait = splitedLine[3];
                if (splitedLine[2] == "True")
                    friendS[count].friendOnline = true;
                else
                    friendS[count].friendOnline = false;
                count++;
            }

            //尝试连接服务器
            try
            {
                clientSocket.Connect(hostEP);
            }
            catch (Exception se)
            {
                MessageBox.Show("连接错误" + se.Message, "提示信息", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                return;
            }

            //依次向主机查询各用户在线状态
            string[] recvInfo = new string[count];
            for (int i = 0; i < count; i++)
            {
                string nameMess;
                nameMess = friendS[i].friendName;
                string searchMessage = "q" + nameMess;
                byte[] bytes_send_info = new byte[1024];
                bytes_send_info = Encoding.ASCII.GetBytes(searchMessage);
                //向主机发送查询请求
                try
                {
                    clientSocket.Send(bytes_send_info, bytes_send_info.Length, 0);
                }
                catch (Exception ce)
                {
                    MessageBox.Show("发送错误:" + ce.Message, "提示信息", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                    return;
                }
                byte[] bytesRecvInfo = new byte[1024];
                int bytes = 0;                                  //实际接受数据长度
                while (true)                                    //读取相应数据长度的内容
                {
                    bytes = clientSocket.Receive(bytesRecvInfo, bytesRecvInfo.Length, 0);
                    if (bytes <= 0) break;
                    recvInfo[i] += Encoding.ASCII.GetString(bytesRecvInfo, 0, bytes);
                    break;
                }
                //新建控件，在好友列表内显示该用户
                Friend Friend_1 = new Friend(myName, myIp, myPortrait, theme);
                this.friendsList.Controls.Add(Friend_1);
                this.friendsList.Controls[i].BackColor = Color.Transparent;
                string a = Application.StartupPath + "\\resources\\portrait" + friendS[i].friendPortrait + ".jpg";
                Friend_1.friendPortrait.Image = Image.FromFile(@a);
                Friend_1.friendPortrait.SizeMode = PictureBoxSizeMode.Zoom;
                this.friendsList.Controls[i].Tag = friendS[i].friendPortrait;
                Friend_1.friendListName.Text = friendS[i].friendName;
                if (recvInfo[i] != "n")
                {
                    Friend_1.friendListIp.Text = recvInfo[i];
                    Friend_1.friendListSituation.Text = "在线";
                }
                else
                {
                    Friend_1.friendListIp.Text = "";
                    Friend_1.friendListSituation.Text = "离线";
                }
            }
            clientSocket.Close();
            fileStream.Close();
            t1.Resume();
        }

        ///【功能3】P2P下的一对一通信
        /// <summary>
        /// “添加好友”：添加好友
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click(object sender, EventArgs e)
        {
            NewFriend newfriendForm = new NewFriend(myName, myIp);                        //跳转到主界面
            newfriendForm.Owner = this;
            newfriendForm.Show();
        }

        /// <summary>
        /// “发起群聊”：发起群聊
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void multichatButton_Click(object sender, EventArgs e)
        {
            tagMultichat = 1;
            string[] group;
            int group_num = 0;
            DialogResult r = MessageBox.Show("将所选中好友全部加入群聊，开始群聊？", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (r == DialogResult.OK)
            {
                int n = this.friendsList.Controls.Count;
                for (int i = 0; i < n; i++)
                    if (this.friendsList.Controls[i].BackColor == Color.LightBlue)
                        group_num++;
                group = new string[group_num];
                int j = 0;
                for (int i = 0; i < n; i++)
                    if (this.friendsList.Controls[i].BackColor == Color.LightBlue)
                    {
                        Friend temp = (Friend)this.friendsList.Controls[i];
                        group[j] = temp.friendListName.Text;
                        j++;
                    }

                MultiChat form3 = new MultiChat(myName, myIp, myPortrait, group, "", "", "", "server");
                form3.Focus();
                form3.Owner = this;
                form3.Show();

            }
        }

        /// <summary>
        /// “退出登录”：退出登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quitButton_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("退出登录后将无法接受好友的消息，请问您确定要退出吗？", "确定退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (r == DialogResult.OK)
            {
                string search_num2 = "logout" + myName;
                IPAddress server_ip = IPAddress.Parse("166.111.140.14");
                IPEndPoint hostEP = new IPEndPoint(server_ip, 8000);
                Socket client_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                client_socket.Connect(hostEP);

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
                if (recv_info == "loo")
                    MessageBox.Show("退出登录成功！");
                else
                    MessageBox.Show("退出登录失败！");
                Application.Exit();
            }
        }

        string[] group_oth;
        int tagMultichat = 0;
        string fileName;
        string fileFrom;
        
        private void listening()
        {
            while (true)
            {
                Socket receive = ordinListener.AcceptSocket();
                byte[] bytes = new byte[2048];
                string recvInfo = "";
                int num = receive.Receive(bytes);
                recvInfo = Encoding.Unicode.GetString(bytes, 0, num);

                ///监听是否有对话请求
                if (recvInfo.StartsWith("START_CHAT"))
                {
                    string mesFrom = recvInfo.Substring(10, 10);
                    int tag = 0;      //标记会话是否已经存在
                    for (int i = 0; i < chatMaxNum; i++)
                    {
                        if (chattList[i] != null && "chat_with" + mesFrom == chattList[i])
                        {
                            tag = 1;                            //表示会话已经存在
                            break;
                        }
                        else
                            tag = 0;                          //如果会话不存在
                    }
                    if (tag == 0)
                    {
                        DialogResult r = MessageBox.Show("您的好友" + mesFrom + "发起会话，开始对话？", "开始会话", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        if (r == DialogResult.OK)
                        {
                            for (int i = 0; i < chatMaxNum; i++)
                                if (chattList[i] == "0")
                                {
                                    chatListLoc = i;
                                    break;
                                }
                            chattList[chatListLoc] = "chat_with" + mesFrom;

                            this.Invoke(new Action(() =>
                           {
                               Chat chatForm = new Chat(mesFrom, myName, myIp, myPortrait, recvInfo);      //新建会话框
                               chatForm.Show();
                               chatForm.Focus();
                               chatForm.Owner = this;
                           }));
                        }
                        else
                        {
                            string[] temp2 = recvInfo.Split('$');
                            string mesFromIp = temp2[0].Substring(20, temp2[0].Length - 20);
                            //拒绝对话请求
                            string userSend = "FALSE_CHAT" + myName;        //关闭聊天
                            IPAddress serverIp = IPAddress.Parse(mesFromIp);                  //对方ip
                            IPEndPoint hostEP = new IPEndPoint(serverIp, sendPort);                   //对方端口地址
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
                            byte[] bytesSendInfo = new byte[1024];
                            bytesSendInfo = Encoding.Unicode.GetBytes(userSend);
                            try
                            {
                                client_socket.Send(bytesSendInfo, bytesSendInfo.Length, 0);    //发送消息
                            }
                            catch (Exception ce)
                            {
                                MessageBox.Show("发送错误:" + ce.Message, "提示信息", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                                return;
                            }
                        }
                    }
                    else
                    {
                        this.Invoke(new Action(() =>
                          {
                              for (int i = 0; i <= chatMaxNum; i++)
                              {
                                  if (this.OwnedForms[i].Name == "Chat")
                                  {
                                      Chat chatTemp = (Chat)this.OwnedForms[i];
                                      if (chatTemp.friendNameLabel.Text == mesFrom)
                                      {
                                          chatTemp.ShowMessage(recvInfo);
                                          break;
                                      }
                                  }
                              }
                          }));
                    }
                }
                    
                ///监听是否有拒绝请求
                else if (recvInfo.StartsWith("FALSE_CHAT"))
                {
                    string[] temp2 = recvInfo.Split('$');
                    string mesFromIp = temp2[0].Substring(20, temp2[0].Length - 20);
                    for (int i = 0; i < chattList.Length; i++)
                        if (chattList[i] == mesFromIp)
                        {
                            chattList[i] = "0";
                            break;
                        }
                    MessageBox.Show("会话被拒绝");
                }
                
                ///监听是否有文件发送请求
                else if (recvInfo.StartsWith("SEND_FILES"))
                {
                    string mesFrom = recvInfo.Substring(10, 10);
                    string[] temp2 = recvInfo.Split('$');
                    fileName = temp2[1];
                    string mes_from_ip = temp2[0].Substring(20, temp2[0].Length - 20);
                    DialogResult r = MessageBox.Show("您的好友" + mesFrom + "向您发出了文件传输请求，请问是否接收？", "文件传输请求", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (r == DialogResult.OK)
                    {
                        //同意文件发送请求
                        t2 = new Thread(listening_file);
                        t2.SetApartmentState(ApartmentState.STA);
                        t2.IsBackground = true;
                        t2.Start();

                        fileFrom = mes_from_ip;

                        string user_send = "ACCPT_FILE" + myName + ipLabel.Text;        //关闭聊天
                        IPAddress server_ip = IPAddress.Parse(mes_from_ip);                  //对方ip
                        IPEndPoint hostEP = new IPEndPoint(server_ip, sendPort);                   //对方端口地址
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
                    else
                    {
                        string user_send = "REFUS_FILE" + myName + ipLabel.Text;        //关闭聊天
                        IPAddress server_ip = IPAddress.Parse(mes_from_ip);                  //对方ip
                        IPEndPoint hostEP = new IPEndPoint(server_ip, sendPort);                   //对方端口地址
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
                
                ///监听文件发送过程
                else if (recvInfo.StartsWith("ACCPT_FILE"))
                {
                    string mes_from = recvInfo.Substring(10, 10);
                    string[] temp2 = recvInfo.Split('$');
                    string mes_from_ip = temp2[0].Substring(20, temp2[0].Length - 20);
                    for (int j = 0; j <= chatMaxNum; j++)
                    {
                        if (this.OwnedForms[j].Name == "Chat")              //找到对应的窗口
                        {
                            Chat chatTemp = (Chat)this.OwnedForms[j];
                            if (chatTemp.friendNameLabel.Text == mes_from)
                            {
                                chatTemp.fileSign();
                                break;
                            }

                        }
                    }
                }
                else if (recvInfo.StartsWith("ACCP2_FILE"))
                {
                    string mes_from = recvInfo.Substring(10, 10);
                    string[] temp2 = recvInfo.Split('$');
                    string mes_from_ip = temp2[0].Substring(20, temp2[0].Length - 20);
                    for (int j = 0; j <= chatMaxNum; j++)
                    {
                        if (this.OwnedForms[j].Name == "Chat")              //找到对应的窗口
                        {
                            Chat chatTemp = (Chat)this.OwnedForms[j];
                            if (chatTemp.friendNameLabel.Text == mes_from)
                            {
                                chatTemp.fileSend();
                                break;
                            }

                        }
                    }
                }
                else if (recvInfo.StartsWith("REFUS_FILE"))
                {
                    MessageBox.Show("文件发送被对方拒绝！");
                }

                ///监听群聊
                else if (recvInfo.StartsWith("START_MHAT"))
                {
                    string mes_from = recvInfo.Substring(10, 10);
                    string[] temp2 = recvInfo.Split('$');
                    string mes_from_ip = temp2[0].Substring(20, temp2[0].Length - 20);
                    int pos = temp2[1].IndexOf(" " + myName);
                    if (pos != -1)
                    {
                        temp2[1] = temp2[1].Remove(pos, myName.Length + 1);          //去掉本身，显示群聊对象
                    }
                    temp2[1] = temp2[1] + " " + mes_from;
                    group_oth = temp2[1].Split(' ');
                    string[] group_oth_real;
                    int group_len = 0;
                    for (int m = 0; m < group_oth.Length; m++)
                        if (group_oth[m] != "")
                            group_len++;
                    group_oth_real = new string[group_len];
                    int index = 0;
                    for (int m = 0; m < group_oth.Length; m++)
                        if (group_oth[m] != "")
                        {
                            group_oth_real[index] = group_oth[m];
                            index++;
                        }
                    if (tagMultichat == 0)
                    {
                        DialogResult r = MessageBox.Show("您的好友" + mes_from + "向您发起了群聊会话，开始对话？", "接受群聊", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        if (r == DialogResult.OK)
                        {
                            this.Invoke(new Action(() =>
                            {
                                MultiChat multiForm = new MultiChat(myName, myIp, myPortrait, group_oth_real, recvInfo, mes_from, mes_from_ip, "client");     //新建会话框
                                multiForm.Show();
                                multiForm.Focus();
                                multiForm.Owner = this;
                            }));
                            tagMultichat = 1;
                        }
                        else
                        {
                            //发送拒绝信息
                            /********************************拒绝对方的群聊请求********************************/
                            string user_send = "FALSE_MHAT" + myName;        //关闭聊天
                            IPAddress server_ip = IPAddress.Parse(mes_from_ip);                  //对方ip
                            IPEndPoint hostEP = new IPEndPoint(server_ip, sendPort);                   //对方端口地址
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

                            tagMultichat = 0;              //表示没有群聊窗口

                        }
                    }
                    else                            //该对话已经存在，直接找到窗口显示
                    {
                        this.Invoke(new Action(() =>
                        {
                            for (int i = 0; i < this.OwnedForms.Count(); i++)
                            {
                                if (this.OwnedForms[i].Name == "multichat")
                                {

                                    MultiChat multiTemp = (MultiChat)this.OwnedForms[i];
                                    {
                                        if (multiTemp.state == "client")             //若是客户机
                                        {
                                            multiTemp.RecvShow(recvInfo, mes_from, group_oth);
                                            break;
                                        }
                                        else if (multiTemp.state == "server")      //若是服务器
                                        {
                                            multiTemp.RecvShow(recvInfo, mes_from, group_oth);     //在自己的窗口上显示
                                            multiTemp.ownerTrans(recvInfo, mes_from, mes_from_ip); //将收到的消息转发

                                        }
                                    }

                                }
                            }
                        }));
                    }
                }
                ///监听群聊是否被拒绝
                else if (recvInfo.StartsWith("FALSE_MHAT"))
                {
                    string mes_from = recvInfo.Substring(10, 10);
                    string[] temp2 = recvInfo.Split('$');
                    string mes_from_ip = temp2[0].Substring(20, temp2[0].Length - 20);

                    MessageBox.Show("朋友" + mes_from + "退出群聊！");
                    this.Invoke(new Action(() =>                                        //关闭对话
                    {
                        for (int j = 0; j < this.OwnedForms.Count(); j++)
                        {
                            if (this.OwnedForms[j].Name == "MultiChat")
                            {
                                MultiChat multiTemp = (MultiChat)this.OwnedForms[j];
                                {
                                    if (multiTemp.state == "server")                     //接收方必为server
                                    {
                                        int nu = multiTemp.friends_list.Controls.Count;
                                        for (int t = 0; t < nu; t++)
                                        {
                                            Friend tem = (Friend)multiTemp.friends_list.Controls[t];
                                            if (tem.friendListName.Text == mes_from)
                                            {
                                                multiTemp.friends_list.Controls.RemoveAt(t);
                                                break;
                                            }
                                        }
                                    }
                                    break;
                                }

                            }
                        }
                    }));
                }
                ///监听好友添加请求
                else if (recvInfo.StartsWith("ADD_FRIEND"))
                {
                    string mesFrom = recvInfo.Substring(10, 10);
                    string[] temp2 = recvInfo.Split('$');
                    string mesFromIp = temp2[0].Substring(20, temp2[0].Length - 20);
                    string friendReply = "";
                    DialogResult r = MessageBox.Show("好友：" + mesFrom + "想添加您为好友，请问您接受吗？", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (r == DialogResult.OK)
                    {
                        friendReply = "ACCPT_ADDF" + usernameLabel.Text + ipLabel.Text + "$";
                        NewFriend nfTemp = new NewFriend(myName, myIp);
                        nfTemp.addFriend(mesFrom, mesFromIp);
                    }
                    else
                    {
                        friendReply = "REJEC_ADDF" + usernameLabel.Text + ipLabel.Text + "$";
                    }
                    IPAddress server_ip = IPAddress.Parse(mesFromIp);
                    IPEndPoint hostEP = new IPEndPoint(server_ip, sendPort);
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
                    byte[] bytesSendInfo = new byte[1024];
                    bytesSendInfo = Encoding.Unicode.GetBytes(friendReply);
                    try
                    {
                        client_socket.Send(bytesSendInfo, bytesSendInfo.Length, 0);    //发送消息
                    }
                    catch (Exception ce)
                    {
                        MessageBox.Show("发送错误:" + ce.Message, "提示信息", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                        return;
                    }
                }
                ///监听好友添加请求是否被拒绝
                else if (recvInfo.StartsWith("REJEC_ADDF"))
                {
                    string mes_from = recvInfo.Substring(10, 10);
                    string[] temp2 = recvInfo.Split('$');
                    string mes_from_ip = temp2[0].Substring(20, temp2[0].Length - 20);
                    this.Invoke(new Action(() =>                                        //关闭对话
                    {
                        for (int j = 0; j < this.OwnedForms.Count(); j++)
                        {
                            if (this.OwnedForms[j].Name == "NewFriend")
                            {
                                NewFriend nfTemp = (NewFriend)this.OwnedForms[j];
                                {
                                    nfTemp.friendRespon("REJEC_ADDF", mes_from, mes_from_ip);
                                }

                            }
                        }
                    }));
                }
                ///监听好友添加请求是否被接受
                else if (recvInfo.StartsWith("ACCPT_ADDF"))
                {
                    string mes_from = recvInfo.Substring(10, 10);
                    string[] temp2 = recvInfo.Split('$');
                    string mes_from_ip = temp2[0].Substring(20, temp2[0].Length - 20);
                    this.Invoke(new Action(() =>                                        //关闭对话
                    {
                        for (int j = 0; j < this.OwnedForms.Count(); j++)
                        {
                            if (this.OwnedForms[j].Name == "NewFriend")
                            {
                                NewFriend nfTemp = (NewFriend)this.OwnedForms[j];
                                {
                                    nfTemp.friendRespon("ACCPT_ADDF", mes_from, mes_from_ip);
                                }
                            }
                        }
                    }));
                }
            }
        }

        /// <summary>
        /// 监听是否有文件待接收
        /// </summary>
        private void listening_file()
        {
            while (true)
            {
                saveFileDialog1.FileName = fileName;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string user_send = "ACCP2_FILE" + myName + ipLabel.Text + "$";        //关闭聊天
                    IPAddress server_ip = IPAddress.Parse(fileFrom);                  //对方ip
                    IPEndPoint hostEP = new IPEndPoint(server_ip, sendPort);                   //对方端口地址
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
                    //接收文件
                    FileStream filestream = new FileStream(saveFileDialog1.FileName, FileMode.OpenOrCreate, FileAccess.Write);
                    Socket receive = fileListener.AcceptSocket();                //监听到端口有信息
                    byte[] bytes = new byte[1024];
                    int received;
                    while (true)                         //以1024为单位接收信息，直到接收到停止信号
                    {
                        received = receive.Receive(bytes);
                        if (received == 0)              //若接收内容为空则退出
                            break;
                        filestream.Write(bytes, 0, received);
                    }
                    filestream.Close();
                    MessageBox.Show("下载成功！");
                    client_socket.Close();
                    break;
                }
            }
        }
    }
}
