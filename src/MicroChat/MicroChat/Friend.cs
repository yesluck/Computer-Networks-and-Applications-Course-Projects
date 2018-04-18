using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MicroChat
{
    public partial class Friend : UserControl
    {
        string userName;
        string userIp;
        int userPic;

        int count = 0;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="myName"></param>
        /// <param name="myIp"></param>
        /// <param name="myPortrait"></param>
        /// <param name="theme"></param>
        public Friend (string myName,string myIp,int myPortrait, int theme)
        {
            InitializeComponent();
            userName = myName;
            userIp = myIp;
            userPic = myPortrait;
            if(theme==1)
            {
                friendListName.ForeColor = Color.White;
                friendListIp.ForeColor = Color.White;
                friendListSituation.ForeColor = Color.White;
            }
        }

        /// <summary>
        /// 点击朋友头像：进入一对一聊天模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void friend_picture_Click(object sender, EventArgs e)
        {
            if (friendListSituation.Text == "在线")
            {
                string name = friendListName.Text;
                Chat chatForm = new Chat(name, userName, userIp, userPic, "");
                chatForm.Show();
                chatForm.Owner = this.ParentForm;
                Main temp = (Main)this.ParentForm;
                for (int i = 0; i < temp.chattList.Length; i++)
                {
                    if (temp.chattList[i] == "0")
                        temp.chattList[i] = "chat_with" + friendListName.Text;
                }
            }
            else
                MessageBox.Show("该用户不在线，请等待他上线后发起聊天。");
            this.BackColor = Color.Transparent;
            count = 0;
        }

        /// <summary>
        /// 点击其他部分：选中该好友
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void friend_name_Click(object sender, EventArgs e)
        {
            if (count == 0)
            {
                this.BackColor = Color.LightBlue;
                count = 1;
            }
            else
            {
                this.BackColor = Color.Transparent;
                count = 0;
            }
        }

        private void friend_ip_Click(object sender, EventArgs e)
        {
            if (count == 0)
            {
                this.BackColor = Color.LightBlue;
                count = 1;
            }
            else
            {
                this.BackColor = Color.Transparent;
                count = 0;
            }
        }

        private void friend_state_Click(object sender, EventArgs e)
        {
            if (count == 0)
            {
                this.BackColor = Color.LightBlue;
                count = 1;
            }
            else
            {
                this.BackColor = Color.Transparent;
                count = 0;
            }
        }

        private void Friend_Click(object sender, EventArgs e)
        {
            if (count == 0)
            {
                this.BackColor = Color.LightBlue;
                count = 1;
            }
            else
            {
                this.BackColor = Color.Transparent;
                count = 0;
            }
        }
    }
}
