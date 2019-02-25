using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PSMS
{
    public partial class Developer : Form
    {
        public Developer()
        {
            InitializeComponent();
        }

        private void PBEmail_Click(object sender, EventArgs e)
        {
            MessageBox.Show("tahmidhasan.3003@gmail.com\ntahmidhasan.3003@yahoo.com", "Email");
        }

        private void PBFacebook_Click(object sender, EventArgs e)
        {
            MessageBox.Show("https://www.facebook.com/tahmidhasan3003", "Facebook");
        }

        private void PBGooglePlus_Click(object sender, EventArgs e)
        {
            MessageBox.Show("https://plus.google.com/u/0/+tahmidhasan3003", "Google Plus");
        }

        private void PBLinkedIn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("https://www.linkedin.com/in/tahmidhasan3003/", "LinkedIn");
        }

        private void PBTwitter_Click(object sender, EventArgs e)
        {
            MessageBox.Show("https://twitter.com/tahmidhasan3003", "Twitter");
        }

        private void PBSkype_Click(object sender, EventArgs e)
        {
            MessageBox.Show("tahmidhasan.3003", "Skype");
        }

        private void PBYouTube_Click(object sender, EventArgs e)
        {
            MessageBox.Show("https://www.youtube.com/channel/UCTvv-18vrj7k4bJmXZhlQhw", "YouTube");
        }
    }
}
