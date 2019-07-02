using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TerVer_1_
{
    public partial class Form2 : Form
    {
        Form1 form;
        int k;

        public Form2(Form1 form1)
        {
            InitializeComponent();
            form = form1;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            k = form.K;
            double[] a = new double[10];
            double[] b = new double[10];
            a[0] = Convert.ToDouble(textBox1.Text);
            a[1] = Convert.ToDouble(textBox3.Text);
            a[2] = Convert.ToDouble(textBox5.Text);
            a[3] = Convert.ToDouble(textBox7.Text);
            a[4] = Convert.ToDouble(textBox9.Text);
            a[5] = Convert.ToDouble(textBox11.Text);
            a[6] = Convert.ToDouble(textBox13.Text);
            a[7] = Convert.ToDouble(textBox15.Text);
            a[8] = Convert.ToDouble(textBox17.Text);
            a[9] = Convert.ToDouble(textBox19.Text);

            b[0] = Convert.ToDouble(textBox2.Text);
            b[1] = Convert.ToDouble(textBox4.Text);
            b[2] = Convert.ToDouble(textBox6.Text);
            b[3] = Convert.ToDouble(textBox8.Text);
            b[4] = Convert.ToDouble(textBox10.Text);
            b[5] = Convert.ToDouble(textBox12.Text);
            b[6] = Convert.ToDouble(textBox14.Text);
            b[7] = Convert.ToDouble(textBox16.Text);
            b[8] = Convert.ToDouble(textBox18.Text);
            b[9] = Convert.ToDouble(textBox20.Text);
            for(int i = 0; i < k; i++)
            {
                form.A[i] = a[i];
                form.B[i] = b[i];
            }
        }
    }
}
