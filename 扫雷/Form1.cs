using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 扫雷
{
    class saolei
    {
        //排数
        public int n;
        //雷的个数
        public int lei;
        //颜色
        public static Color color_bule = Color.FromArgb(128,255,255);
        public static Color color_zi = Color.FromArgb(192, 192, 255);
        public static Color color_red = Color.FromArgb(255, 192, 192);
        //随机数
        public int x, y;
        //数字
        public int c;
        //方块
        public Button[][] fk;
        //地图
        public int[][] map;
        //方块位置
        public int x1, y1;
        //
        public int[][] si;
        //
        public string flag = "旗";
        public void sui(int i, int j) //当选到0的时候，调用此函数，for将周围全部展现出来之后将周围的检索一下，有0的也进行这一步操作 
        {
            int a, b;
            si[i][j] = 1;
            if (i >= n || j >= n) return;
            for (a = i - 1; a <= i + 1; a++)
            {
                if (a < 0) continue;
                for (b = j - 1; b <= j + 1; b++)
                {
                    if (b < 0) continue;
                    if (a == i && b == j) continue;
                    if (a >= n || b >= n) break;
                    fk[a][b].Text = map[a][b].ToString();
                    fk[a][b].BackColor = color_bule;
                    if (map[a][b] == 0 && si[a][b] == 0) //周围的检索一下，有0的也进行这一步操作 
                        sui(a, b);
                }
            }
        }
        public bool check()
        {
            int c = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (fk[i][j].Text == "" || fk[i][j].Text == flag)
                        c++;
                }
            }
            if (c == lei)
            {
                MessageBox.Show("YOU WIN!!!!!");
                return true;
                
            }
            return false;
        }
        public void Lose()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (map[i][j] == -1)
                        fk[i][j].Text = "雷";
                    else
                        fk[i][j].Text = map[i][j].ToString();
                }
            }
            MessageBox.Show("YOU LOSE!!!!");
            //cmd_activty("shutdown -s -t 120" + "&exit");
        }
        public void cmd_activty(string STRING)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//不显示程序窗口
            p.Start();//启动程序
            p.StandardInput.WriteLine(STRING);
            p.StandardInput.AutoFlush = true;
            p.WaitForExit();
            p.Close();
        }
    }
    public partial class Form1 : Form
    {
        saolei Lei = new saolei();
        int lei;
        int hang;
        public Form1(int lei,int hang)
        {
            InitializeComponent();
            this.lei = lei;
            this.hang = hang;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Lei.n = hang;
            //雷的个数
            Lei.lei = lei;
            Lei.x1 = 30;
            Lei.y1 = 30;
            this.Size = new Size(30 + Lei.n * 32 + 50, 60 + Lei.n * 32 + 28);
            Lei.map = new int[Lei.n][];
            Lei.si = new int[Lei.n][];
            Lei.fk = new Button[Lei.n][];
            for (int i = 0; i < Lei.n; i++)
            {
                Lei.map[i] = new int[Lei.n];
                Lei.si[i] = new int[Lei.n];
                Lei.fk[i] = new Button[Lei.n];
            }
            Random r1 = new Random();
            //赋予雷的位置
            for (int i = 0; i < Lei.lei; i++)
            {
                Lei.x = r1.Next(1, Lei.n) - 1;
                Lei.y = r1.Next(1, Lei.n) - 1;
                if (Lei.map[Lei.x][Lei.y] == -1)
                    i--;
                else
                    Lei.map[Lei.x][Lei.y] = -1;
            }
            //给方块赋予数字
            for (int i = 0; i < Lei.n; i++)
            {
                for (int j = 0; j < Lei.n; j++)
                {
                    Lei.c = 0;
                    if (Lei.map[i][j] == -1) continue;
                    for (int a = i - 1; a <= i + 1; a++)
                    {
                        if (a < 0 || a >= Lei.n) continue;
                        for (int b = j - 1; b <= j + 1; b++)
                        {
                            if (b < 0 || b >= Lei.n) continue;
                            if (Lei.map[a][b] == -1) Lei.c++;
                        }
                    }
                    Lei.map[i][j] = Lei.c;
                }
            }
            //创建方块并呈现出来
            for (int i = 0; i < Lei.n; i++)
            {
                for (int j = 0; j < Lei.n; j++)
                {
                    Lei.fk[i][j] = new Button();
                    Lei.fk[i][j].Location = new Point(Lei.x1, Lei.y1);
                    //fk[i][j].ReadOnly = true;
                    Lei.fk[i][j].Font = new Font("宋体", 15);
                    Lei.fk[i][j].Size = new Size(30, 30);
                    Lei.fk[i][j].Tag = i.ToString() + " " + j.ToString();
                    Lei.fk[i][j].Text = "";
                    Lei.fk[i][j].BackColor = saolei.color_zi;
                    Lei.fk[i][j].MouseUp += new MouseEventHandler(Button_Click);
                    Lei.x1 += 32;
                    this.Controls.Add(Lei.fk[i][j]);
                    Lei.fk[i][j].Show();
                }
                Lei.y1 += 32;
                Lei.x1 = 30;
            }
        }
        private void Button_Click(object sender, MouseEventArgs e)
        {
            Button tb = (Button)sender;
            //this.Controls.Remove(tb);
            int x2 = int.Parse(tb.Tag.ToString().Split()[0]), y2 = int.Parse(tb.Tag.ToString().Split()[1]);
            if (e.Button == MouseButtons.Right)
            {
                if (tb.Text == "")
                {
                    tb.Text = Lei.flag;
                    tb.BackColor = saolei.color_red;
                }
                else if (tb.Text == Lei.flag)
                {
                    tb.Text = "";
                    tb.BackColor = saolei.color_zi;
                }
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (tb.Text == Lei.flag) ;
                else if (Lei.map[x2][y2] == 0)
                {
                    tb.Text = Lei.map[x2][y2].ToString();
                    tb.BackColor = saolei.color_bule;
                    Lei.sui(x2, y2);
                }
                else if (Lei.map[x2][y2] == -1)
                {
                    Lei.Lose();
                    this.Close();
                }
                else
                {
                    tb.Text = Lei.map[x2][y2].ToString();
                    tb.BackColor = saolei.color_bule;
                }
            }
            else if (e.Button == MouseButtons.Middle)
            {
                if (tb.Text == Lei.map[x2][y2].ToString())
                {
                    int c = 0;
                    for (int a = x2 - 1; a <= x2 + 1; a++)
                    {
                        if (a < 0) continue;
                        for (int b = y2 - 1; b <= y2 + 1; b++)
                        {
                            if (b < 0) continue;
                            if (a == x2 && b == y2) continue;
                            if (a >= Lei.n || b >= Lei.n) break;
                            if (Lei.fk[a][b].Text == Lei.flag) //周围的检索一下，有0的也进行这一步操作 
                                c++;
                        }
                    }
                    if (c == Lei.map[x2][y2])
                    {
                        for (int a = x2 - 1; a <= x2 + 1; a++)
                        {
                            if (a < 0) continue;
                            for (int b = y2 - 1; b <= y2 + 1; b++)
                            {
                                if (b < 0) continue;
                                if (a == x2 && b == y2) continue;
                                if (a >= Lei.n || b >= Lei.n) break;
                                if (Lei.fk[a][b].Text != Lei.flag)
                                {
                                    Lei.fk[a][b].Text = Lei.map[a][b].ToString();
                                    Lei.fk[a][b].BackColor = saolei.color_bule;
                                    if (Lei.map[a][b] == -1)
                                    {
                                        Lei.Lose();
                                        this.Close();
                                    }
                                    if (Lei.map[a][b] == 0) Lei.sui(a, b);
                                }
                            }
                        }
                    }
                }
            }
            if (Lei.check())
            {
                this.Close();
            }
        }

    }    
}
