namespace MicroChat
{
    partial class MultiChat
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
            this.user_text = new System.Windows.Forms.RichTextBox();
            this.chat_text = new System.Windows.Forms.RichTextBox();
            this.button_send = new System.Windows.Forms.Button();
            this.friends_list = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // user_text
            // 
            this.user_text.BackColor = System.Drawing.Color.White;
            this.user_text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.user_text.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.user_text.Location = new System.Drawing.Point(27, 256);
            this.user_text.Name = "user_text";
            this.user_text.Size = new System.Drawing.Size(415, 120);
            this.user_text.TabIndex = 18;
            this.user_text.Text = "";
            // 
            // chat_text
            // 
            this.chat_text.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.chat_text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chat_text.Cursor = System.Windows.Forms.Cursors.Default;
            this.chat_text.Location = new System.Drawing.Point(27, 12);
            this.chat_text.Name = "chat_text";
            this.chat_text.Size = new System.Drawing.Size(415, 227);
            this.chat_text.TabIndex = 17;
            this.chat_text.Text = "";
            // 
            // button_send
            // 
            this.button_send.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.button_send.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_send.FlatAppearance.BorderSize = 0;
            this.button_send.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_send.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_send.Location = new System.Drawing.Point(468, 346);
            this.button_send.Name = "button_send";
            this.button_send.Size = new System.Drawing.Size(75, 30);
            this.button_send.TabIndex = 22;
            this.button_send.Text = "发送";
            this.button_send.UseVisualStyleBackColor = false;
            this.button_send.Click += new System.EventHandler(this.button_send_Click);
            // 
            // friends_list
            // 
            this.friends_list.BackColor = System.Drawing.Color.Transparent;
            this.friends_list.Location = new System.Drawing.Point(468, 12);
            this.friends_list.Name = "friends_list";
            this.friends_list.Size = new System.Drawing.Size(152, 312);
            this.friends_list.TabIndex = 23;
            // 
            // MultiChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::MicroChat.Resource1.chatBG;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(683, 388);
            this.Controls.Add(this.friends_list);
            this.Controls.Add(this.button_send);
            this.Controls.Add(this.user_text);
            this.Controls.Add(this.chat_text);
            this.MaximumSize = new System.Drawing.Size(699, 426);
            this.MinimumSize = new System.Drawing.Size(699, 426);
            this.Name = "MultiChat";
            this.Opacity = 0.95D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "群聊";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox user_text;
        private System.Windows.Forms.RichTextBox chat_text;
        private System.Windows.Forms.Button button_send;
        public System.Windows.Forms.FlowLayoutPanel friends_list;
    }
}