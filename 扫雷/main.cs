using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 扫雷
{
    public partial class main : Form
    {

        Boolean falg = true;

        public main()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                textBox2.Text = (int.Parse(textBox1.Text) * 3).ToString();
            }
            catch (Exception)
            {
                textBox2.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && textBox1.Text != "")
            {
                int hang = int.Parse(textBox1.Text);
                if (hang <= 30 && hang >= 10)
                {
                    int lei = int.Parse(textBox2.Text);
                    if (lei > hang * hang)
                    {
                        MessageBox.Show("雷数大于行数列数", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    //new form1 打开扫雷
                    this.Hide();
                    if (DialogResult.Cancel == new Form1(lei, hang).ShowDialog())
                    {
                        this.Visible = true;
                    }
                    //this.Close();
                    //this.Visible = false;
                }
                else
                {
                    MessageBox.Show("行数列数不符合要求", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("不正规的行数列数", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (falg == true)
            {
                textBox2.ReadOnly = false;
            }
            else
            {
                textBox2.ReadOnly = false;
            }
        }
    }
}
