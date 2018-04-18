using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MicroChat
{
    public partial class portrait : Form
    {
        public portrait()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            login tempOwner = (login)this.Owner;
            tempOwner.choosePortrait(1);
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            login tempOwner = (login)this.Owner;
            tempOwner.choosePortrait(2);
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            login tempOwner = (login)this.Owner;
            tempOwner.choosePortrait(3);
            this.Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            login tempOwner = (login)this.Owner;
            tempOwner.choosePortrait(4);
            this.Close();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            login tempOwner = (login)this.Owner;
            tempOwner.choosePortrait(5);
            this.Close();
        }
    }
}
