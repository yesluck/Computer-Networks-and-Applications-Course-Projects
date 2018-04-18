namespace MicroChat
{
    partial class Chat
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
            this.converBox = new System.Windows.Forms.RichTextBox();
            this.textBox = new System.Windows.Forms.RichTextBox();
            this.friendIpLabel = new System.Windows.Forms.Label();
            this.friendNameLabel = new System.Windows.Forms.Label();
            this.portraitBox = new System.Windows.Forms.PictureBox();
            this.fileButton = new System.Windows.Forms.Button();
            this.sendButton = new System.Windows.Forms.Button();
            this.user_ip = new System.Windows.Forms.Label();
            this.user_name = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.portraitBox)).BeginInit();
            this.SuspendLayout();
            // 
            // converBox
            // 
            this.converBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.converBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.converBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.converBox.Location = new System.Drawing.Point(27, 12);
            this.converBox.Name = "converBox";
            this.converBox.Size = new System.Drawing.Size(415, 227);
            this.converBox.TabIndex = 15;
            this.converBox.Text = "";
            // 
            // textBox
            // 
            this.textBox.BackColor = System.Drawing.Color.White;
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.textBox.Location = new System.Drawing.Point(27, 256);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(415, 120);
            this.textBox.TabIndex = 16;
            this.textBox.Text = "";
            // 
            // friendIpLabel
            // 
            this.friendIpLabel.AutoSize = true;
            this.friendIpLabel.BackColor = System.Drawing.Color.Transparent;
            this.friendIpLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.friendIpLabel.ForeColor = System.Drawing.Color.White;
            this.friendIpLabel.Location = new System.Drawing.Point(550, 68);
            this.friendIpLabel.Name = "friendIpLabel";
            this.friendIpLabel.Size = new System.Drawing.Size(101, 17);
            this.friendIpLabel.TabIndex = 20;
            this.friendIpLabel.Text = "255.255.255.255";
            // 
            // friendNameLabel
            // 
            this.friendNameLabel.AutoSize = true;
            this.friendNameLabel.BackColor = System.Drawing.Color.Transparent;
            this.friendNameLabel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.friendNameLabel.ForeColor = System.Drawing.Color.White;
            this.friendNameLabel.Location = new System.Drawing.Point(549, 39);
            this.friendNameLabel.Name = "friendNameLabel";
            this.friendNameLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.friendNameLabel.Size = new System.Drawing.Size(99, 19);
            this.friendNameLabel.TabIndex = 19;
            this.friendNameLabel.Text = "2014011548";
            this.friendNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // portraitBox
            // 
            this.portraitBox.Location = new System.Drawing.Point(468, 33);
            this.portraitBox.Name = "portraitBox";
            this.portraitBox.Size = new System.Drawing.Size(75, 75);
            this.portraitBox.TabIndex = 17;
            this.portraitBox.TabStop = false;
            // 
            // fileButton
            // 
            this.fileButton.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.fileButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.fileButton.FlatAppearance.BorderSize = 0;
            this.fileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fileButton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fileButton.Location = new System.Drawing.Point(558, 346);
            this.fileButton.Name = "fileButton";
            this.fileButton.Size = new System.Drawing.Size(75, 30);
            this.fileButton.TabIndex = 23;
            this.fileButton.Text = "发送文件";
            this.fileButton.UseVisualStyleBackColor = false;
            this.fileButton.Click += new System.EventHandler(this.button_file_send_Click);
            // 
            // sendButton
            // 
            this.sendButton.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.sendButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.sendButton.FlatAppearance.BorderSize = 0;
            this.sendButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sendButton.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sendButton.Location = new System.Drawing.Point(468, 346);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(75, 30);
            this.sendButton.TabIndex = 21;
            this.sendButton.Text = "发送";
            this.sendButton.UseVisualStyleBackColor = false;
            this.sendButton.Click += new System.EventHandler(this.button_send_Click);
            // 
            // user_ip
            // 
            this.user_ip.AutoSize = true;
            this.user_ip.BackColor = System.Drawing.Color.Transparent;
            this.user_ip.Location = new System.Drawing.Point(722, 191);
            this.user_ip.Name = "user_ip";
            this.user_ip.Size = new System.Drawing.Size(41, 12);
            this.user_ip.TabIndex = 25;
            this.user_ip.Text = "label2";
            // 
            // user_name
            // 
            this.user_name.AutoSize = true;
            this.user_name.BackColor = System.Drawing.Color.Transparent;
            this.user_name.Location = new System.Drawing.Point(701, 169);
            this.user_name.Name = "user_name";
            this.user_name.Size = new System.Drawing.Size(65, 12);
            this.user_name.TabIndex = 24;
            this.user_name.Text = "2014011541";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Chat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::MicroChat.Resource1.chatBG;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(683, 388);
            this.Controls.Add(this.user_ip);
            this.Controls.Add(this.user_name);
            this.Controls.Add(this.fileButton);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.friendIpLabel);
            this.Controls.Add(this.friendNameLabel);
            this.Controls.Add(this.portraitBox);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.converBox);
            this.MaximumSize = new System.Drawing.Size(699, 426);
            this.MinimumSize = new System.Drawing.Size(699, 426);
            this.Name = "Chat";
            this.Opacity = 0.95D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chat";
            ((System.ComponentModel.ISupportInitialize)(this.portraitBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox converBox;
        private System.Windows.Forms.RichTextBox textBox;
        private System.Windows.Forms.Label friendIpLabel;
        public System.Windows.Forms.Label friendNameLabel;
        private System.Windows.Forms.PictureBox portraitBox;
        private System.Windows.Forms.Button fileButton;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.Label user_ip;
        private System.Windows.Forms.Label user_name;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}