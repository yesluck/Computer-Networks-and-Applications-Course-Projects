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

namespace MicroChat
{
    public partial class login : Form
    {
        //【功能1】
        /// <summary>
        /// 根据输入数值选择头像（输入数值由portrait.cs文件及其窗体获得）
        /// </summary>
        /// <param name="choice"></param>
        public int portrait = 1;
        public void choosePortrait(int choice)
        {
            portrait = choice;
            string a = Application.StartupPath + "\\resources\\portrait" + choice.ToString() + ".jpg";
            this.portraitBox.Image = Image.FromFile(@a);
        }

        public login()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            string loginUsername = username.Text.ToString();
            string loginPassword = password.Text.ToString();
            if (loginPassword == "net2016" && loginUsername.Length == 10)   //如果账号密码正确
            {
                //第一步：向服务器发送上线请求
                string clientSend = loginUsername + "_" + loginPassword;
                IPAddress serverIP = IPAddress.Parse("166.111.140.14");
                IPEndPoint hostEP = new IPEndPoint(serverIP, 8000);
                Socket sckt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                byte[] bytesSendInfo = new byte[1024];
                bytesSendInfo = Encoding.ASCII.GetBytes(clientSend);
                //1.1 尝试连接
                try
                {
                    sckt.Connect(hostEP);
                }
                catch (Exception ce)
                {
                    MessageBox.Show(ce.Message, "连接错误", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                    return;
                }
                //1.2 向服务器发送上线请求
                try
                {
                    sckt.Send(bytesSendInfo, bytesSendInfo.Length, 0);
                }
                catch (Exception se)
                {
                    MessageBox.Show(se.Message, "发送错误", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                    return;
                }
                //1.3 接收服务器的返回内容
                string connRecvInfo = "";                       //接收的返回内容
                byte[] bytesRecvInfo = new byte[1024];
                int bytes = 0;                                  //实际接收数据长度
                while (true)                                    //读取相应数据长度的内容
                {
                    bytes = sckt.Receive(bytesRecvInfo, bytesRecvInfo.Length, 0);
                    if (bytes <= 0) break;
                    connRecvInfo += Encoding.ASCII.GetString(bytesRecvInfo, 0, bytes);
                    break;
                }

                //第二步：从服务器获取IP地址
                clientSend = "q" + loginUsername;
                bytesSendInfo = Encoding.ASCII.GetBytes(clientSend);
                //2.1 尝试向服务器发出请求
                try
                {
                    sckt.Send(bytesSendInfo, bytesSendInfo.Length, 0);
                }
                catch (Exception ce)
                {
                    MessageBox.Show("发送错误:" + ce.Message, "提示信息", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                    return;
                }
                //2.2 接收服务器的返回内容
                string ipRecvInfo = "";                         //接收的返回内容
                byte[] bytesIPRecvInfo = new byte[1024];
                int ipBytes = 0;                                //实际接收数据长度
                while (true)                                    //读取相应数据长度的内容
                {
                    ipBytes = sckt.Receive(bytesIPRecvInfo, bytesIPRecvInfo.Length, 0);
                    if (ipBytes <= 0) break; 
                    ipRecvInfo += Encoding.ASCII.GetString(bytesIPRecvInfo, 0, ipBytes);
                    break;
                }
                string user_ip = ipRecvInfo;
                sckt.Close();
                if (connRecvInfo == "lol")                      //登录成功，进入主界面
                {
                    Main mainForm = new Main(portrait, loginUsername, ipRecvInfo);//(login_name, user_ip, pic_id);                        //跳转到主界面
                    mainForm.Show();
                    this.Hide();
                }
                else
                    MessageBox.Show("用户名不存在，请重新登录！", "提示信息", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
            }
            else
            {
                if (loginUsername.Length != 10)
                    MessageBox.Show("用户名输入有误，请重新输入！", "提示信息", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                else
                    MessageBox.Show("密码错误，请重新输入！", "提示信息", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
            }
        }

        private void portraitBox_Click(object sender, EventArgs e)
        {
            portrait portraitForm = new portrait();             //进入头像修改界面
            portraitForm.Owner = this;
            portraitForm.Show();
        }
    }
}
