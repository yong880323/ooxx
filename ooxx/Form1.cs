using System;
using System.Windows.Forms;
namespace ooxx
{
    public partial class Form1 : Form
    {
        private readonly GBVA gbva = new();
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            Graphics GPS = CreateGraphics();
            Pen MyPen = new(Color.Red, 2f);
            for (int i = 0; i < 4; i++)
            { 
                for (int j = 0; j < 4; j++)
                {
                   GPS.DrawLine(MyPen, gbva.X_left + j * 100, gbva.Y_top, 
                                       gbva.X_left + j * 100, gbva.Y_top * 7);
                }
                GPS.DrawLine(MyPen, gbva.X_left, gbva.Y_top + i * 100, 
                                    gbva.X_left * 7, gbva.Y_top + i * 100);
            }
        }

        private void InitBtn()
        {
            for (int i = 1; i < 4; i++)
            {
                for (int j = 1; j < 4; j++)
                {
                    int arr_Idx = ((i-1)*3)  + j;

                    gbva.ArrBtn[arr_Idx].Size = new Size(gbva.X_width, gbva.Y_hight);

                    gbva.ArrBtn[arr_Idx].Text = arr_Idx.ToString();//暫看不出來
                    gbva.ArrBtn[arr_Idx].Name = arr_Idx.ToString();
                    gbva.ArrBtn[arr_Idx].Click += new EventHandler(arrBtn_Click);

                    int loc_x = gbva.X_left + ((i - 1) * 100)+2;
                    int loc_y = gbva.Y_top + ((j - 1) * 100)+2;
                    gbva.ArrBtn[arr_Idx].Location = new Point(loc_x, loc_y);
                    Controls.Add(gbva.ArrBtn[arr_Idx]);
                }
            }
        
        }

        private void arrBtn_Click(object sender, EventArgs e)
        {
            PictureBox tmp = (PictureBox)sender;
            tmp.Enabled = false;

            //tmp.Text = OX[idx]; // 顯示輸入 O 或 X
            TextToImage(tmp, gbva.OX[gbva.idx]);

            gbva.idx = (gbva.idx + 1) % 2; // 設定下次為 O 或 X
        }

        private void TextToImage(PictureBox pictureBox1, string str_OX)
        {
            Color BackColor = Color.Transparent;
            String FontName = "Times New Roman";
            int FontSize = 50;

            Bitmap bitmap;
            if (pictureBox1.Image != null)
            {
                bitmap = new Bitmap(pictureBox1.Image);
            }
            else
            {
                bitmap = new Bitmap(gbva.X_width, gbva.Y_hight);
            }
            //設定一個畫布 graphics
            Graphics graphics = Graphics.FromImage(bitmap);
            //顏色
            Color color = Color.Transparent;
            //圖形大小
            Font font = new(FontName, FontSize);
            //筆刷
            SolidBrush BrushBackColor = new(BackColor);
            //畫筆(的顏色)
            Pen BorderPen = new(color);
            //繪製矩形 (Point=點的座標)
            Rectangle displayRectangle = new(new Point(0, 0), new Size(Width - 1, Height - 1));
            //填滿背景(顏色,矩形)
            graphics.FillRectangle(BrushBackColor, displayRectangle);
            //畫出邊框矩形
            graphics.DrawRectangle(BorderPen, displayRectangle);
            //顯示畫出O或X
            if (str_OX == "Ｏ")
                graphics.DrawString(str_OX, font, Brushes.Aqua, 0, 5);
            else
                graphics.DrawString(str_OX, font, Brushes.Magenta, 0, 5);
            pictureBox1.Image = bitmap;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++) {
                gbva.ArrBtn[i] = new PictureBox
                {
                    BackColor = SystemColors.Window,
                    ForeColor = Color.Red
                };
            }

            for (int i = 0; i < 10; i++)
            {
                gbva.ArrOX[i] = "";
            }
            InitBtn();
        }
    }
}