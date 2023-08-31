using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GAN;

namespace RubY
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cube r = new Cube();
            r.Rotate("D");
            tbxW.Lines = r.Face('U');
            tbxY.Lines = r.Face('D');
            tbxG.Lines = r.Face('F');
            tbxB.Lines = r.Face('B');
            tbxR.Lines = r.Face('R');
            tbxO.Lines = r.Face('L');

        }
    }
}
