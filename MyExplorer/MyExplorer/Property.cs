using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyExplorer
{
    public partial class Property : Form
    {
        public Property(bool type, string path)
        {
            InitializeComponent();
            if (type)
            {
                pictureBox1.Image = imageList1.Images[1];
                label5.Visible = false;
                label10.Visible = false;
            }
            else
            {
                pictureBox1.Image = imageList1.Images[0];
            }   


        }


    }
}
