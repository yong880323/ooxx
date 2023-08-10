using System;
using System.Windows.Forms;
namespace ooxx
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 引入共用參數
        /// </summary>
        private readonly GBVA gbva = new();
        public Form1()
        {
            InitializeComponent();
        }

        #region 繪製九宮格
        /// <summary>
        /// 重寫OnPaint方法 繪製九宮格
        /// </summary>
        /// <param name="e">繪圖相關參數</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // 獲取一個 Graphics 對象，用於繪圖
            Graphics GPS = e.Graphics;
            //創建一個紅色 寬度是2個單位 的畫筆對象
            Pen MyPen = new(Color.Red, 2f);
            #region 繪圖

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    //繪製直條線
                    GPS.DrawLine(MyPen, gbva.X_left + j * 100, gbva.Y_top,
                                        gbva.X_left + j * 100, gbva.Y_top * 7);
                }
                //繪製橫條線
                GPS.DrawLine(MyPen, gbva.X_left, gbva.Y_top + i * 100,
                                    gbva.X_left * 7, gbva.Y_top + i * 100);
            }

            #endregion

        }
        #endregion

        #region 產生九宮格按鈕

        private void Form1_Load(object sender, EventArgs e)
        {
            InitBtn();
        }

        /// <summary>
        /// 九宮格順序排列產生按鈕並顯示到畫面
        /// </summary>
        private void InitBtn()
        {
            //產生10個按鈕陣列
            for (int i = 0; i < 10; i++)
            {
                gbva.ArrOX[i] = "";//格子被占用
                gbva.ArrBtn[i] = new PictureBox
                {
                    BackColor = SystemColors.Window,
                    ForeColor = Color.Red,

                };
            }
            //BTN照九宮格順序排列
            for (int i = 1; i < 4; i++)
            {
                for (int j = 1; j < 4; j++)
                {
                    int arr_Idx = ((i - 1) * 3) + j;

                    gbva.ArrBtn[arr_Idx].Size = new Size(gbva.X_width, gbva.Y_hight);

                    gbva.ArrBtn[arr_Idx].Text = arr_Idx.ToString();//暫看不出來
                    gbva.ArrBtn[arr_Idx].Name = arr_Idx.ToString();
                    gbva.ArrBtn[arr_Idx].Click += new EventHandler(ArrBtn_Click);//點擊觸發
                    gbva.ArrBtn[arr_Idx].MouseEnter += new EventHandler(PictureBox_MouseEnter);//滑鼠靠近觸發
                    gbva.ArrBtn[arr_Idx].MouseLeave += new EventHandler(PictureBox_MouseLeave);//滑鼠離開觸發
                    int loc_x = gbva.X_left + ((i - 1) * 100) + 2;//橫向
                    int loc_y = gbva.Y_top + ((j - 1) * 100) + 2;//直向
                    gbva.ArrBtn[arr_Idx].Location = new Point(loc_x, loc_y);
                    Controls.Add(gbva.ArrBtn[arr_Idx]);//新增到畫面
                }
            }
        }

        #endregion


        #region 按鈕點擊後顯示O或X

        /// <summary>
        /// 點擊後顯示O或X以及下次換人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ArrBtn_Click(object sender, EventArgs e)
        {
            PictureBox tmp = (PictureBox)sender;//抓取本身位置
            tmp.Enabled = true;//是否還能點擊

            int thisIdx = Convert.ToInt16(tmp.Name);

            //tmp.Text = OX[idx]; // 顯示輸入 O 或 X
            TextToImage(tmp, gbva.OX[gbva.idx]);
            gbva.ArrOX[thisIdx] = gbva.OX[gbva.idx];

            CheckLine(gbva.OX[gbva.idx]);

            gbva.idx = (gbva.idx + 1) % 2; // 設定下次為 O 或 X
            if (gbva.IniWin) ClearBtn();
        }

        #endregion

        #region 字串OX變成圖案顯示
        /// <summary>
        /// 字串OX變成圖案顯示
        /// </summary>
        /// <param name="pictureBox1"></param>
        /// <param name="str_OX"></param>
        private void TextToImage(PictureBox pictureBox1, string str_OX)
        {
            Color BackColor = Color.Transparent;
            String FontName = "Times New Roman";
            int FontSize = 50;//圖形大小

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

        #endregion

        #region PictureBox事件

        /// <summary>
        /// 滑鼠靠近時觸發
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox_MouseEnter(object sender, EventArgs e)
        {
            PictureBox tmp = (PictureBox)sender;
            tmp.BackColor = Color.Red;
        }
        /// <summary>
        /// 滑鼠離開時觸發
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox_MouseLeave(object sender, EventArgs e)
        {
            PictureBox tmp = (PictureBox)sender;
            tmp.BackColor = Color.White;
        }
        #endregion

        #region Bingo連線

        /// <summary>
        /// 確認是否連線
        /// </summary>
        /// <param name="thisIdx"></param>
        /// <param name="str_OX">目前是圈或叉</param>
        private void CheckLine(string str_OX)
        {
            string[,] arrLine = new string[,] {  { "1", "4", "7" }, { "2", "5", "8" }, { "3", "6", "9" },
                                                 { "1", "2", "3" }, { "4", "5", "6" }, { "7", "8", "9" },
                                                 { "1", "5", "9" }, { "3", "5", "7" } };

            for (int i = 0; i < 8; i++)
            {
                int cnt = 0;
                for (int j = 0; j < 3; j++)
                {
                    if (str_OX == gbva.ArrOX[Convert.ToInt16(arrLine[i, j])])
                    {
                        cnt++;
                    }
                }
                if (cnt == 3)
                {
                    if (str_OX == "o")
                    {
                        MessageBox.Show("Ｏ 贏了");
                        gbva.IniWin = true;
                    }
                    else
                    {
                        MessageBox.Show("Ｘ 贏了");
                        gbva.IniWin = true;
                    }
                }
            }

        }

        #endregion

        #region 重新開始

        /// <summary>
        /// 重新開始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, EventArgs e)
        {
            ClearBtn();
        }

        /// <summary>
        /// 重新開始還原預設
        /// </summary>
        private void ClearBtn()
        {
            for (int i = 0; i < 10; i++)
            {
                gbva.ArrBtn[i].Image = null;
                gbva.ArrOX[i] = null;
            }
            gbva.idx = 0;
            gbva.IniWin=false;
        } 
        #endregion
    }
}