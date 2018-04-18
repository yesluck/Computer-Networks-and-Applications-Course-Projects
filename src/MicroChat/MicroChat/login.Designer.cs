namespace MicroChat
{
    partial class login
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.username = new System.Windows.Forms.TextBox();
            this.portraitBox = new System.Windows.Forms.PictureBox();
            this.password = new System.Windows.Forms.TextBox();
            this.loginButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.portraitBox)).BeginInit();
            this.SuspendLayout();
            // 
            // username
            // 
            this.username.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.username.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.username.Location = new System.Drawing.Point(92, 177);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(100, 26);
            this.username.TabIndex = 1;
            this.username.Text = "2014011541";
            // 
            // portraitBox
            // 
            this.portraitBox.BackColor = System.Drawing.Color.Transparent;
            this.portraitBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.portraitBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.portraitBox.Image = global::MicroChat.Resource1.portrait1;
            this.portraitBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.portraitBox.InitialImage = null;
            this.portraitBox.Location = new System.Drawing.Point(92, 37);
            this.portraitBox.Name = "portraitBox";
            this.portraitBox.Size = new System.Drawing.Size(100, 100);
            this.portraitBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.portraitBox.TabIndex = 7;
            this.portraitBox.TabStop = false;
            this.portraitBox.Click += new System.EventHandler(this.portraitBox_Click);
            // 
            // password
            // 
            this.password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.password.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.password.Location = new System.Drawing.Point(92, 218);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(70, 26);
            this.password.TabIndex = 8;
            this.password.Text = "net2016";
            // 
            // loginButton
            // 
            this.loginButton.BackColor = System.Drawing.Color.Transparent;
            this.loginButton.BackgroundImage = global::MicroChat.Resource1.enter;
            this.loginButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.loginButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loginButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonFace;
            this.loginButton.FlatAppearance.BorderSize = 0;
            this.loginButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loginButton.ForeColor = System.Drawing.Color.Black;
            this.loginButton.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.loginButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.loginButton.Location = new System.Drawing.Point(165, 218);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(27, 27);
            this.loginButton.TabIndex = 9;
            this.loginButton.UseVisualStyleBackColor = false;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::MicroChat.Resource1.loginBG;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(284, 362);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.password);
            this.Controls.Add(this.portraitBox);
            this.Controls.Add(this.username);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(300, 400);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 400);
            this.Name = "login";
            this.Opacity = 0.95D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "子坤畅聊";
            ((System.ComponentModel.ISupportInitialize)(this.portraitBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.PictureBox portraitBox;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Button loginButton;
    }
}

