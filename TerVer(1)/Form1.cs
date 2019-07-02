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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        Random random;// = new Random(2);
        public int K = 0, n = 0;
        public double[] A = new double[10];
        public double[] B = new double[10];
        double[] x = new double[1];
        double[] q = new double[1];
        int RAND_MAX = 32767;double y;

        double f2(double y, double a, double b)
        {
            if (y <= 0)
                return (double)a * Math.Exp(b * y);
            else
                return (double)a * (Math.Exp(-b * y));
        }
        double f(double y,double a)
        {
            if (y <= 0)
                return Math.Exp(2 * a * y) / 2;
            else
                return (2 - Math.Exp(-2 * a * y)) / 2;
        }
        double f1(double y,double a,double b)
        {
            if (y > (double)a / b)
                return -Math.Log(2 - y * b / a) / b;
            else
                return Math.Log(y * b / a) / b;
        }

        double gammaapprox(double x)
        {
            double[] p = new double[8];
            p[0] = -1.71618513886549492533811e+0;
            p[1] = 2.47656508055759199108314e+1;
            p[2] = -3.79804256470945635097577e+2;
            p[3] = 6.29331155312818442661052e+2;
            p[4] = 8.66966202790413211295064e+2;
            p[5] = -3.14512729688483675254357e+4;
            p[6] = -3.61444134186911729807069e+4;
            p[7] = 6.64561438202405440627855e+4;

            double[] q = new double[8];
            q[0] = -3.08402300119738975254353e+1;
            q[1] = 3.15350626979604161529144e+2;
            q[2] = -1.01515636749021914166146e+3;
            q[3] = -3.10777167157231109440444e+3;
            q[4] = 2.25381184209801510330112e+4;
            q[5] = 4.75584627752788110767815e+3;
            q[6] = -1.34659959864969306392456e+5;
            q[7] = -1.15132259675553483497211e+5;

            double z = x - 1.0;
            double a = 0.0;
            double b = 1.0;
            for (int i = 0; i < 8; i++)
            {
                a = (a + p[i]) * z;
                b = b * z + q[i];
            }
            return (a / b + 1.0);
        }

        double gamma(double z)
        {
            if ((z > 0.0) && (z < 1.0))
                return gamma(z + 1.0) / z;      // рекурентное соотношение для 0
            if (z > 2)
                return (z - 1) * gamma(z - 1);       // рекурентное соотношение для z>2 
            if (z <= 0)
                return Math.PI / (Math.Sin(Math.PI * z) * gamma(1 - z));   // рекурентное соотношение для z<=0 
            return gammaapprox(z);  // 1<=z<=2 использовать аппроксимацию
        }

        double chi(double r, double x)
        {
            if (x > 0)
                return Math.Pow(x, r / 2 - 1) * Math.Exp(-x / 2) / gamma(r / 2) / Math.Pow(2, r / 2);
            else
                return 0;
        }

        double Frev(double r, double x)
        {
            int n = (int)(x * 1000);
            double h = x / n;
            double res = 0;
            for (int i = 0; i < n; i++)
                res += (chi(r, i * h) + chi(r, (i + 1) * h)) * h / 2;
            return 1 - res;
        }

        double[] BubbleSort(double[] A)
        {
            for (int i = 0; i < A.Length; i++)
                for (int j = 0; j < A.Length - 1; j++)
                {
                    if (A[j] > A[j + 1])
                    {
                        double temp = A[j];
                        A[j] = A[j + 1];
                        A[j + 1] = temp;
                    }
                }
            return A;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            random = new Random();
            int k;
            double a, r, s = 0, v = 0, med = 0;
            a = Convert.ToDouble(textBox1.Text);
            n = Convert.ToInt32(textBox3.Text);
            k = Convert.ToInt32(textBox2.Text);
            K = k;
            x = new double[n];
            q = new double[n];
            if (k < 10)
            {
                Form2 f = new Form2(this);
                f.Show();

            }
            for (int i = 0; i < n; i++)
            {
                y = Convert.ToDouble((double)random.Next(RAND_MAX) / (RAND_MAX));
                double x1 = 0;
                q[i] = y;
                 if (y >=0.5)
                  {
                    x1 = Math.Log(1 / (2 - 2 * y));
                    x[i] = (double)(x1 / (2 * a));
                  }
                  else
                  {
                    x1 = Math.Log(2 * y);
                    x[i] = (double)(x1 / (2 * a));
                  }
            }
            BubbleSort(x);
            BubbleSort(q);

            r = x[n-1] - x[0];
            //выборочное среднее
            for (int i = 0; i < n; i++)
            {
                v += x[i];
            }
            v /= n;
            //выборочная дисперсия
            for (int i = 0; i < n; i++)
            {
                s += Math.Pow((x[i] - v), 2);
            }
            s /= n;
            //медиана
            if (n % 2 == 1)
                med = x[n / 2];
            else
                med = (x[n / 2 - 1] + x[n / 2]) / 2;

            dataGridView2.Rows.Clear();
            dataGridView2.Rows.Add();
            dataGridView2[0, 0].Value = 0;
            dataGridView2[1, 0].Value = v;
            dataGridView2[2, 0].Value = Math.Abs(v);
            dataGridView2[3, 0].Value = (double)1 / (2 * Math.Pow(a, 2));
            dataGridView2[4, 0].Value = s;
            dataGridView2[5, 0].Value = Math.Abs((double)1 / (2 * Math.Pow(a, 2)) - s);
            dataGridView2[6, 0].Value = med;
            dataGridView2[7, 0].Value = r;
            dataGridView1.Rows.Clear();
            for (int i = 0; i < n; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1[0, i].Value = x[i];
                dataGridView1[1, i].Value = q[i];
            }

            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();
            chart1.Series[1].Color = Color.Red;
            chart1.Series[0].Color = Color.DarkBlue;
            chart1.Series[2].Color = Color.Green;

            for (double i=x[0]; i <= x[n-1]; i+=0.01)
            {
                chart1.Series[1].Points.AddXY(i, f(i, a));
            }

            int s1 = 0;
            double res = 0, vr = 0, m = 0;
            double[] m1 = new double[2 * n];
            chart1.Series[2].Points.AddXY(x[0], 0);
            for (int i = 1; i <n; i++)
            {
                s1 = 0;
                for(int j = 0; j < n; j++)
                {
                    if (x[j] < x[i])
                        s1++;
                }
                res = (double)s1 / n;
                chart1.Series[2].Points.AddXY(x[i - 1], res);
                chart1.Series[2].Points.AddXY(x[i], res);
                if (m < Math.Abs(res - f(x[i], a)))
                    m = Math.Abs(res - f(x[i], a));
            }

            chart1.Series[2].Points.AddXY(x[n - 1], 1 - 1 / n);

            textBox5.Text = Convert.ToString(m);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
           // double a = Convert.ToDouble(textBox1.Text);
            int k_new = Convert.ToInt32(textBox7.Text);

            double positive = double.PositiveInfinity;
            double negative = double.NegativeInfinity;

            double[] ranges = new double[k_new + 1];
            double[] q = new double[k_new + 1];
            double[] nums = new double[k_new];
            double[] value = new double[n];

            double h = (double)(x[n - 1] - x[0]) / k_new;
           // double l0 = x[0];
            double l = x[0] + h;

            dataGridView4.Rows.Clear();

            for (int i = 0; i < k_new; i++)
            {
                dataGridView4.Rows.Add();
                dataGridView4[0, 0].Value = negative;
                dataGridView4[0, i].Value = l;
                l += h;
          //      l0 += h;
            }

            dataGridView4[0, k_new].Value = positive;
            ranges[0] = negative;

            for (int i = 1; i < dataGridView4.RowCount - 1; i++)
            {
                ranges[i] = Convert.ToDouble(dataGridView4.Rows[i].Cells[0].Value);
            }
            ranges[k_new] = positive;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double a = Convert.ToDouble(textBox1.Text);
            int k_new = Convert.ToInt32(textBox7.Text);
            double alpha = Convert.ToDouble(textBox6.Text);

            double[] ranges = new double[k_new + 1];
            double[] q = new double[k_new + 1];
            double[] nums = new double[k_new];
            double[] value = new double[n];

            for (int i = 0; i < n; i++)
            {
                value[i] = Convert.ToDouble(dataGridView1.Rows[i].Cells[0].Value);
            }

            for (int i = 0; i < k_new + 1; i++)
            {
                ranges[i] = Convert.ToDouble(dataGridView4.Rows[i].Cells[0].Value);
            }

            for (int i = 0; i < k_new; i++)
            {
                q[i] = f(ranges[i + 1], a) - f(ranges[i], a);
            }

            int temp = 0;

            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] > ranges[temp + 1])
                {
                    while (value[i] > ranges[temp + 1])
                    { temp++; }
                }
                nums[temp]++;
            }

            for (int i = 0; i < k_new; i++)
            {
                dataGridView4[1, i].Value = q[i];
            }

            //подсчет R0 frev(r0)

            double r0 = 0;

            for (int i = 0; i < k_new; i++)
                r0 = r0 + (nums[i] - n * q[i]) * (nums[i] - n * q[i]) / (n * q[i]);

            double testval = Frev(k_new, r0);

            if (testval > alpha)
                label10.Text = Convert.ToString("Гипотеза принята");
            else
                label10.Text = Convert.ToString("Гипотеза отвергнута");

            textBox8.Text = Convert.ToString(testval);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int k = 0;
            double h = 0, a = 0, b = 0, m = 0;
            a = Convert.ToDouble(textBox1.Text);
            b = 2 * a;
            k = Convert.ToInt32(textBox2.Text);
            h = (double)(x[n - 1] - x[0]) / k;
            double l = 0, l0 = 0;
            double r1 = 0, z1 = 0;
            int r2 = 0, z = 0;
            dataGridView3.Rows.Clear();
            chart1.Series[0].Points.Clear();
            if (k < 10)
            {
                for (int i = 0; i < k; i++)
                {
                    l0 = A[i];
                    l = B[i];
                    chart1.Series[0].Points.AddXY(l0, z);
                    r2 = 0;
                    for (int j = 0; j < n; j++)
                    {
                        if (x[j] >= l0 && x[j] <= l)
                            r2++;
                    }
                    z1 = (double)(l + l0) / 2;
                    r1 = (double)r2 / (n * Math.Abs((l - l0)));
                    dataGridView3.Rows.Add();
                    dataGridView3[0, i].Value = z1;
                    dataGridView3[1, i].Value = f2(z1, a, b);
                    dataGridView3[2, i].Value = r1;
                    if (m < Math.Abs(r1 - f2(z1, a, b)))
                    {
                        m = Math.Abs(r1 - f2(z1, a, b));
                    }
                    chart1.Series[0].Points.AddXY(l0, r1);
                    chart1.Series[0].Points.AddXY(l, r1);
                    chart1.Series[0].Points.AddXY(l, z);
                }
            }
            else
            {
                l0 = x[0];
                l = x[0] + h;
                chart1.Series[0].Points.AddXY(l0, z);
                for (int i = 0; i < k; i++)
                {
                    r2 = 0;
                    for (int j = 0; j < n; j++)
                    {
                        if (x[j] >= l0 && x[j] <= l)
                            r2++;
                    }
                    dataGridView3.Rows.Add();
                    z1 = (double)(l + l0) / 2;
                    r1 = (double)r2 / (n * Math.Abs((l - l0)));

                    dataGridView3[0, i].Value = z1;
                    dataGridView3[1, i].Value = f2(z1, a, b);
                    dataGridView3[2, i].Value = r1;
                    if (m < Math.Abs(r1 - f2(z1, a, b)))
                    {
                        m = Math.Abs(r1 - f2(z1, a, b));
                    }
                    chart1.Series[0].Points.AddXY(l0, r1);
                    chart1.Series[0].Points.AddXY(l, r1);
                    chart1.Series[0].Points.AddXY(l, z);
                    l += h;
                    l0 += h;
                }
            }
            textBox4.Text = Convert.ToString(m);
        }
    }
}
