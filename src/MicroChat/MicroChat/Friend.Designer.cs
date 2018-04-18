namespace MicroChat
{
    partial class Friend
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.friendListSituation = new System.Windows.Forms.Label();
            this.friendListIp = new System.Windows.Forms.Label();
            this.friendListName = new System.Windows.Forms.Label();
            this.friendPortrait = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.friendPortrait)).BeginInit();
            this.SuspendLayout();
            // 
            // friendListSituation
            // 
            this.friendListSituation.AutoSize = true;
            this.friendListSituation.Font = new System.Drawing.Font("微软雅黑", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.friendListSituation.Location = new System.Drawing.Point(45, 33);
            this.friendListSituation.Name = "friendListSituation";
            this.friendListSituation.Size = new System.Drawing.Size(28, 16);
            this.friendListSituation.TabIndex = 7;
            this.friendListSituation.Text = "在线";
            this.friendListSituation.Click += new System.EventHandler(this.friend_state_Click);
            // 
            // friendListIp
            // 
            this.friendListIp.AutoSize = true;
            this.friendListIp.Font = new System.Drawing.Font("微软雅黑", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.friendListIp.Location = new System.Drawing.Point(45, 19);
            this.friendListIp.Name = "friendListIp";
            this.friendListIp.Size = new System.Drawing.Size(37, 16);
            this.friendListIp.TabIndex = 6;
            this.friendListIp.Text = "他的IP";
            this.friendListIp.Click += new System.EventHandler(this.friend_ip_Click);
            // 
            // friendListName
            // 
            this.friendListName.AutoSize = true;
            this.friendListName.Font = new System.Drawing.Font("微软雅黑", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.friendListName.Location = new System.Drawing.Point(45, 5);
            this.friendListName.Name = "friendListName";
            this.friendListName.Size = new System.Drawing.Size(67, 15);
            this.friendListName.TabIndex = 5;
            this.friendListName.Text = "2014011548";
            this.friendListName.Click += new System.EventHandler(this.friend_name_Click);
            // 
            // friendPortrait
            // 
            this.friendPortrait.Cursor = System.Windows.Forms.Cursors.Hand;
            this.friendPortrait.ErrorImage = null;
            this.friendPortrait.Location = new System.Drawing.Point(5, 6);
            this.friendPortrait.Name = "friendPortrait";
            this.friendPortrait.Size = new System.Drawing.Size(40, 40);
            this.friendPortrait.TabIndex = 4;
            this.friendPortrait.TabStop = false;
            this.friendPortrait.Click += new System.EventHandler(this.friend_picture_Click);
            // 
            // Friend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.friendListSituation);
            this.Controls.Add(this.friendListIp);
            this.Controls.Add(this.friendListName);
            this.Controls.Add(this.friendPortrait);
            this.Name = "Friend";
            this.Size = new System.Drawing.Size(112, 49);
            this.Click += new System.EventHandler(this.Friend_Click);
            ((System.ComponentModel.ISupportInitialize)(this.friendPortrait)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label friendListSituation;
        public System.Windows.Forms.Label friendListIp;
        public System.Windows.Forms.Label friendListName;
        public System.Windows.Forms.PictureBox friendPortrait;

    }
}
