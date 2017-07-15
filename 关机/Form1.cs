using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 关机
{
    public partial class Form1 : Form
    {
        Sunisoft.IrisSkin.SkinEngine se = null;
        public Form1()
        {
            InitializeComponent();
            se = new Sunisoft.IrisSkin.SkinEngine();
            se.SkinAllForm = true;//所有窗体均应用此皮肤
            se.SkinFile = "skin/MacOS.ssk";
        }
        private void bt1_Click(object sender, EventArgs e)
        {
            if (tb1.Text == "" || tb2.Text == "" || tb1.Text == null || tb2.Text==null)
            {
                MessageBox.Show("小时分钟都要写");
                return;
            }
            Process myProcess = new System.Diagnostics.Process();
            myProcess.StartInfo.FileName = "cmd.exe";
            myProcess.StartInfo.UseShellExecute = false;
            myProcess.StartInfo.RedirectStandardInput = true;
            myProcess.StartInfo.RedirectStandardOutput = true;
            myProcess.StartInfo.RedirectStandardError = true;
            myProcess.StartInfo.CreateNoWindow = true;
            myProcess.Start();
            int a = int.Parse(tb1.Text);
            int b = int.Parse(tb2.Text);
            if (a > 23)
            {
                MessageBox.Show("小时不要写这么大好不好<(－︿－)>");
            }
            else if (b > 59)
            {
                MessageBox.Show("分钟不要写这么大好不好￣へ￣");
            }
            else
            {
                string time = a + ":" + b;

                //获取当前时间和结束时间
                string daystr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string dt1 = daystr.Split(new char[] { ' ' })[0];
                string endtime = daystr.Split(new char[] { ' ' })[0] + " " + time;

                //算出毫秒
                DateTime t1 = Convert.ToDateTime(endtime);
                DateTime t2 = Convert.ToDateTime(daystr);
                TimeSpan t3 = t1 - t2;
                double getSeconds = t3.TotalSeconds;

                if (getSeconds > 0)
                {
                    myProcess.StandardInput.WriteLine("shutdown -s -t " + getSeconds.ToString() + "");
                }
                else
                {
                    //重新获取结束时间
                    string[] arr = daystr.Split(new char[] { ' ' })[0].Split(new char[] { '-' });
                    string str = arr[0] + "-" + arr[1] + "-" + (int.Parse(arr[2]) + 1).ToString();
                    endtime = str + " " + time;
                    t1 = Convert.ToDateTime(endtime);
                    t3 = t1 - t2;
                    getSeconds = t3.TotalSeconds;
                    myProcess.StandardInput.WriteLine("shutdown -s -t " + getSeconds.ToString() + "");
                }
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process myProcess = new System.Diagnostics.Process();
            myProcess.StartInfo.FileName = "cmd.exe";
            myProcess.StartInfo.UseShellExecute = false;
            myProcess.StartInfo.RedirectStandardInput = true;
            myProcess.StartInfo.RedirectStandardOutput = true;
            myProcess.StartInfo.RedirectStandardError = true;
            myProcess.StartInfo.CreateNoWindow = true;
            myProcess.Start();
            myProcess.StandardInput.WriteLine("shutdown -a"); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string strDt = DateTime.Now.ToString("t");
            string[] dt1 = strDt.Split(new char[] { ':' });
            int time = int.Parse(dt1[0]);
            time += 1;
            if (time==24)
            {
                time = 0;
            }
            
            tb1.Text = time.ToString();
            tb2.Text = dt1[1];
        }

        private void tb1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 允许输入:数字、退格键(8)、全选(1)、复制(3)、粘贴(22)
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8 &&
            e.KeyChar != 1 && e.KeyChar != 3 && e.KeyChar != 22)
            {
                e.Handled = true;
            }
        }

        private void tb2_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 允许输入:数字、退格键(8)、全选(1)、复制(3)、粘贴(22)
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8 &&
            e.KeyChar != 1 && e.KeyChar != 3 && e.KeyChar != 22)
            {
                e.Handled = true;
            }
        }
    }
}
