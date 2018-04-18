namespace MicroChat
{
    partial class NewFriend
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
            this.searchNum = new System.Windows.Forms.TextBox();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.addButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // searchNum
            // 
            this.searchNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchNum.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.searchNum.Location = new System.Drawing.Point(96, 69);
            this.searchNum.Name = "searchNum";
            this.searchNum.Size = new System.Drawing.Size(100, 26);
            this.searchNum.TabIndex = 2;
            this.searchNum.Text = "2014011541";
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.BackColor = System.Drawing.Color.Transparent;
            this.usernameLabel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.usernameLabel.Location = new System.Drawing.Point(55, 31);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(113, 19);
            this.usernameLabel.TabIndex = 10;
            this.usernameLabel.Text = "请输入TA的学号";
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
            this.addButton.Location = new System.Drawing.Point(103, 113);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(87, 29);
            this.addButton.TabIndex = 13;
            this.addButton.Text = "查询并添加";
            this.addButton.UseVisualStyleBackColor = false;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // NewFriend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::MicroChat.Resource1.loginBG;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(304, 212);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.usernameLabel);
            this.Controls.Add(this.searchNum);
            this.MaximumSize = new System.Drawing.Size(320, 250);
            this.MinimumSize = new System.Drawing.Size(320, 250);
            this.Name = "NewFriend";
            this.Text = "添加好友";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox searchNum;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Button addButton;
    }
}