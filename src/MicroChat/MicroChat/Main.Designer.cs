namespace MicroChat
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.portraitBox = new System.Windows.Forms.PictureBox();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.ipLabel = new System.Windows.Forms.Label();
            this.friendsList = new System.Windows.Forms.FlowLayoutPanel();
            this.addButton = new System.Windows.Forms.Button();
            this.situationButton = new System.Windows.Forms.Button();
            this.multichatButton = new System.Windows.Forms.Button();
            this.quitButton = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.portraitBox)).BeginInit();
            this.SuspendLayout();
            // 
            // portraitBox
            // 
            this.portraitBox.BackColor = System.Drawing.Color.Transparent;
            this.portraitBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.portraitBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.portraitBox.Image = global::MicroChat.Resource1.portrait1;
            this.portraitBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.portraitBox.InitialImage = null;
            this.portraitBox.Location = new System.Drawing.Point(27, 17);
            this.portraitBox.Name = "portraitBox";
            this.portraitBox.Size = new System.Drawing.Size(75, 75);
            this.portraitBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.portraitBox.TabIndex = 8;
            this.portraitBox.TabStop = false;
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.BackColor = System.Drawing.Color.Transparent;
            this.usernameLabel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.usernameLabel.Location = new System.Drawing.Point(129, 26);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(65, 19);
            this.usernameLabel.TabIndex = 9;
            this.usernameLabel.Text = "我的名字";
            // 
            // ipLabel
            // 
            this.ipLabel.AutoSize = true;
            this.ipLabel.BackColor = System.Drawing.Color.Transparent;
            this.ipLabel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ipLabel.Location = new System.Drawing.Point(129, 57);
            this.ipLabel.Name = "ipLabel";
            this.ipLabel.Size = new System.Drawing.Size(50, 20);
            this.ipLabel.TabIndex = 10;
            this.ipLabel.Text = "我的IP";
            // 
            // friendsList
            // 
            this.friendsList.AutoScroll = true;
            this.friendsList.BackColor = System.Drawing.Color.Transparent;
            this.friendsList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.friendsList.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.friendsList.Location = new System.Drawing.Point(27, 109);
            this.friendsList.Name = "friendsList";
            this.friendsList.Size = new System.Drawing.Size(167, 272);
            this.friendsList.TabIndex = 11;
            // 
            // addButton
            // 
            this.addButton.BackColor = System.Drawing.Color.Transparent;
            this.addButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.addButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonFace;
            this.addButton.FlatAppearance.BorderSize = 0;
            this.addButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addButton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.addButton.ForeColor = System.Drawing.Color.Black;
            this.addButton.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.addButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.addButton.Location = new System.Drawing.Point(200, 122);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 29);
            this.addButton.TabIndex = 12;
            this.addButton.Text = "添加好友";
            this.addButton.UseVisualStyleBackColor = false;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // situationButton
            // 
            this.situationButton.BackColor = System.Drawing.Color.Transparent;
            this.situationButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.situationButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.situationButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonFace;
            this.situationButton.FlatAppearance.BorderSize = 0;
            this.situationButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.situationButton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.situationButton.ForeColor = System.Drawing.Color.Black;
            this.situationButton.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.situationButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.situationButton.Location = new System.Drawing.Point(200, 172);
            this.situationButton.Name = "situationButton";
            this.situationButton.Size = new System.Drawing.Size(75, 29);
            this.situationButton.TabIndex = 13;
            this.situationButton.Text = "刷新好友";
            this.situationButton.UseVisualStyleBackColor = false;
            this.situationButton.Click += new System.EventHandler(this.situationButton_Click);
            // 
            // multichatButton
            // 
            this.multichatButton.BackColor = System.Drawing.Color.Transparent;
            this.multichatButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.multichatButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.multichatButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonFace;
            this.multichatButton.FlatAppearance.BorderSize = 0;
            this.multichatButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.multichatButton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.multichatButton.ForeColor = System.Drawing.Color.Black;
            this.multichatButton.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.multichatButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.multichatButton.Location = new System.Drawing.Point(200, 222);
            this.multichatButton.Name = "multichatButton";
            this.multichatButton.Size = new System.Drawing.Size(75, 29);
            this.multichatButton.TabIndex = 14;
            this.multichatButton.Text = "发起群聊";
            this.multichatButton.UseVisualStyleBackColor = false;
            this.multichatButton.Click += new System.EventHandler(this.multichatButton_Click);
            // 
            // quitButton
            // 
            this.quitButton.BackColor = System.Drawing.Color.Transparent;
            this.quitButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.quitButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.quitButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonFace;
            this.quitButton.FlatAppearance.BorderSize = 0;
            this.quitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.quitButton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.quitButton.ForeColor = System.Drawing.Color.Black;
            this.quitButton.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.quitButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.quitButton.Location = new System.Drawing.Point(200, 272);
            this.quitButton.Name = "quitButton";
            this.quitButton.Size = new System.Drawing.Size(75, 29);
            this.quitButton.TabIndex = 15;
            this.quitButton.Text = "退出登录";
            this.quitButton.UseVisualStyleBackColor = false;
            this.quitButton.Click += new System.EventHandler(this.quitButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(29, 363);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 16);
            this.label1.TabIndex = 16;
            this.label1.Text = "点击好友头像开始聊天";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::MicroChat.Resource1.MainBGtokyo;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(284, 498);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.quitButton);
            this.Controls.Add(this.multichatButton);
            this.Controls.Add(this.situationButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.friendsList);
            this.Controls.Add(this.ipLabel);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.portraitBox);
            this.Location = new System.Drawing.Point(950, 100);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(300, 536);
            this.MinimumSize = new System.Drawing.Size(300, 536);
            this.Name = "Main";
            this.Opacity = 0.95D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "子坤畅聊";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.portraitBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox portraitBox;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Label ipLabel;
        public System.Windows.Forms.FlowLayoutPanel friendsList;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button situationButton;
        private System.Windows.Forms.Button multichatButton;
        private System.Windows.Forms.Button quitButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label1;
    }
}