using System;
using System.Drawing;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

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
                gbva.MoveBtn[i] = new PictureBox
                {
                    BackColor = Color.Yellow,
                    ForeColor = Color.Red,

                };
            }
            //BTN照九宮格順序排列
            for (int i = 1; i < 4; i++)
            {
                for (int j = 1; j < 4; j++)
                {
                    int arr_Idx = ((i - 1) * 3) + j;

                    int loc_x = gbva.X_left + ((i - 1) * 100) + 2;//橫向
                    int loc_y = gbva.Y_top + ((j - 1) * 100) + 2;//直向


                    int Mloc_x = gbva.X_left + ((i - 1) * 100) + 2;//橫向
                    int Mloc_y = gbva.Y_top + ((j - 1) * 100) + 25;//直向

                    gbva.ArrBtn[arr_Idx] = CreateBtn("0", arr_Idx.ToString(), loc_x, loc_y);
                    gbva.MoveBtn[arr_Idx] = CreateMoveBtn("移動", arr_Idx.ToString(), Mloc_x, Mloc_y);

                    Controls.AddRange(new Control[] { gbva.MoveBtn[arr_Idx], gbva.ArrBtn[arr_Idx] });//新增到畫面
                }
            }
        }

        /// <summary>
        /// PictureBox建立
        /// </summary>
        /// <param name="text">階層</param>
        /// <param name="name">數字</param>
        /// <param name="x">橫向</param>
        /// <param name="y">直向</param>
        /// <returns>回傳按鈕樣式</returns>
        private PictureBox CreateBtn(string text, string name, int x, int y)
        {

            PictureBox button = new PictureBox();
            button.Size = new Size(gbva.X_width, gbva.Y_hight);
            button.Text = text;
            button.Name = name;
            button.Location = new Point(x, y);
            button.Click += new EventHandler(ArrBtn_Click);//點擊觸發
            button.MouseEnter += new EventHandler(PictureBox_MouseEnter);//滑鼠靠近觸發
            button.MouseLeave += new EventHandler(PictureBox_MouseLeave);//滑鼠離開觸發
            button.BackColor = Color.White;

            return button;
        }

        /// <summary>
        /// 移動的PictureBox建立
        /// </summary>
        /// <param name="text">階層</param>
        /// <param name="name">數字</param>
        /// <param name="x">橫向</param>
        /// <param name="y">直向</param>
        /// <returns>回傳按鈕樣式</returns>
        private PictureBox CreateMoveBtn(string text, string name, int x, int y)
        {

            PictureBox button = new PictureBox();
            button.Size = new Size(80, 45);
            button.Text = text;
            button.Name = name;
            button.Location = new Point(x, y);
            TextToImage(button, "移動", "4");
            button.Visible = false;
            button.Click += new EventHandler(ArrMove_Click);//點擊觸發
            button.BackColor = Color.White;

            return button;
        }
        #endregion

        #region 顯示判斷大小或者移動選項

        /// <summary>
        /// 顯示判斷大小或者移動選項
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ArrBtn_Click(object sender, EventArgs e)
        {
            PictureBox tmp = (PictureBox)sender;//抓取本身位置
            gbva.ThisIdx = int.TryParse(tmp.Name, out int ThisValue) ? ThisValue : 0;
            int levenText = int.TryParse(gbva.ArrBtn[gbva.ThisIdx].Text, out int parsedValue) ? parsedValue : 0;

            //大中小移動選項
            int levelIndex = (gbva.ArrOX[gbva.ThisIdx] == "Ｏ") ? 3 : 6;
            Big.Visible = levenText < 3 && gbva.Levenl[levelIndex] < 2;
            Mid.Visible = levenText < 2 && gbva.Levenl[levelIndex - 1] < 2;
            Small.Visible = levenText < 1 && gbva.Levenl[levelIndex - 2] < 2;

            if (gbva.ArrBtn[gbva.ThisIdx].Image != null && gbva.ArrOX[gbva.ThisIdx] == gbva.OX[gbva.idx])
                MoveVisible(gbva.ThisIdx);

            panel.Visible = true;

        }

        #region 計算階級數量

        /// <summary>
        /// 計算OX階級數量
        /// </summary>
        private void Level_Check()
        {
            // 初始化階級數量為0
            for (int level = 1; level < 10; level++)
            {
                gbva.Levenl[level] = 0;
            }
            for (int i = 1; i < 10; i++)
            {
                int btnindex = (gbva.ArrOX[i] == "Ｏ") ? 0 : 3;
                int oldindex = (gbva.OldOX[i] == "Ｏ") ? 0 : 3;
                UpdateLevenl(gbva.ArrBtn[i].Text, btnindex);
                UpdateLevenl(gbva.Oldlevenl[i], oldindex);
            }
        }
        /// <summary>
        /// 階級數量累計
        /// </summary>
        /// <param name="text"></param>
        /// <param name="index"></param>
        private void UpdateLevenl(string text, int index)
        {
            switch (text)
            {
                case "1":
                    gbva.Levenl[index + 1]++;
                    break;
                case "2":
                    gbva.Levenl[index + 2]++;
                    break;
                case "3":
                    gbva.Levenl[index + 3]++;
                    break;

            }
        }
        #endregion

        #endregion

        #region 字串OX變成圖案顯示
        /// <summary>
        /// 字串OX變成圖案顯示
        /// </summary>
        /// <param name="pictureBox1"></param>
        /// <param name="str_OX"></param>
        private void TextToImage(PictureBox pictureBox1, string str_OX, string level)
        {
            Color BackColor = Color.Transparent;
            String FontName = "Times New Roman";
            int FontSize = (level == "2") ? 40 :
                           (level == "3") ? 50 : 20;//圖形大小

            Bitmap bitmap;
            if (pictureBox1.Image != null)
                bitmap = new Bitmap(pictureBox1.Image);
            else
                bitmap = new Bitmap(gbva.X_width, gbva.Y_hight);
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

        /// <summary>
        /// 移動按鈕顯示
        /// </summary>
        /// <param name="index"></param>

        private void MoveVisible(int index)
        {
            int indValue = int.TryParse(gbva.ArrBtn[index].Text, out int indexValue) ? indexValue : 0;
            for (int i = 1; i < 10; i++)
            {
                int leven = int.TryParse(gbva.ArrBtn[i].Text, out int ThisValue) ? ThisValue : 0;

                if (leven < indValue)
                    gbva.MoveBtn[i].Visible = true;
            }
            gbva.MoveBtn[index].Visible = false;
            if (index == 0)
                for (int i = 1; i < 10; i++)
                {
                    gbva.MoveBtn[i].Visible = false;
                }
            else
                SetPictureBoxDisable();
        }
        /// <summary>
        /// 設定無法輸入
        /// </summary>
        /// <param name="btn_Idx"></param>
        private void SetPictureBoxDisable()
        {
            for (int i = 1; i < 10; i++)
            {
                gbva.ArrBtn[i].Enabled = false;
                gbva.ArrBtn[i].BackColor = SystemColors.ControlLight;
            }
        }
        /// <summary>
        /// 設定可以輸入
        /// </summary>
        /// <param name="btn_Idx"></param>
        private void SetPictureBoxEnable(int btn_Idx)
        {
            for (int i = 1; i < 10; i++)
            {
                gbva.ArrBtn[i].Enabled = true;
                gbva.ArrBtn[i].BackColor = SystemColors.Window;

            }
            if (btn_Idx > 0)
            {
                gbva.ArrBtn[btn_Idx].Enabled = false;
                gbva.ArrBtn[btn_Idx].BackColor = SystemColors.ControlLight;
            }
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
                    if (str_OX == "Ｏ")
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
                gbva.Levenl[i] = 0;
                gbva.ArrBtn[i].Image = null;
                gbva.ArrOX[i] = "";
                gbva.ArrBtn[i].Text = "0";
                gbva.Oldlevenl[i] = "0";
                gbva.OldOX[i] = "";

            }
            gbva.idx = 0;
            gbva.IniWin = false;
            SetPictureBoxEnable(0);
            MoveVisible(0);
        }
        #endregion

        #region 階級按鈕

        /// <summary>
        /// 階級大按鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Big_Click(object sender, EventArgs e)
        {
            MoveVisible(0);
            gbva.ArrBtn[gbva.ThisIdx].Text = "3";
            Btn_chick();
            Level_Check();
        }

        /// <summary>
        /// 階級中按鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Mid_Click(object sender, EventArgs e)
        {
            MoveVisible(0);
            gbva.ArrBtn[gbva.ThisIdx].Text = "2";
            Btn_chick();
            Level_Check();
        }

        /// <summary>
        /// 階級小按鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Small_Click(object sender, EventArgs e)
        {
            MoveVisible(0);
            gbva.ArrBtn[gbva.ThisIdx].Text = "1";
            Btn_chick();
            Level_Check();
        }

        #endregion

        #region 移動點擊事件

        /// <summary>
        /// 移動存取舊位置以及交換位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ArrMove_Click(object sender, EventArgs e)
        {
            PictureBox tmp = (PictureBox)sender;//抓取本身位置
            gbva.MoveIdx = Convert.ToInt16(tmp.Name);//新的位置
            MoveVisible(0);

            Eatox(gbva.MoveIdx);

            gbva.ArrOX[gbva.MoveIdx] = gbva.OX[gbva.idx];//新的抓舊的OX
            gbva.ArrBtn[gbva.MoveIdx].Text = gbva.ArrBtn[gbva.ThisIdx].Text;//新的抓舊的階層

            Move_chick(int.Parse(gbva.ArrBtn[gbva.ThisIdx].Text));
            if (gbva.OldOX[gbva.ThisIdx] != null)
            {
                gbva.ArrBtn[gbva.ThisIdx].Text = gbva.Oldlevenl[gbva.ThisIdx];//新的移動原本被吃掉的階級變回來
                gbva.ArrOX[gbva.ThisIdx] = gbva.OldOX[gbva.ThisIdx];//新的移動原本被吃掉的OX變回來
                gbva.ArrBtn[gbva.ThisIdx].Image = null;
                TextToImage(gbva.ArrBtn[gbva.ThisIdx], gbva.ArrOX[gbva.ThisIdx], gbva.ArrBtn[gbva.ThisIdx].Text);
                gbva.Oldlevenl[gbva.ThisIdx] = "";
                gbva.OldOX[gbva.ThisIdx] = "";
            }
            SetPictureBoxEnable(0);

            CheckLine(gbva.ArrOX[gbva.MoveIdx]);
        }

        private void Eatox(int idx)
        {
            gbva.OldOX[idx] = gbva.ArrOX[idx];//儲存被吃掉的OX
            gbva.Oldlevenl[idx] = gbva.ArrBtn[idx].Text;//儲存被吃掉的階層
        }

        #endregion

        #region 按鈕顯示OX以及是否連線

        /// <summary>
        /// 顯示OX以及是否連線
        /// </summary>
        /// <param name="level"></param>
        private void Btn_chick()
        {
            //tmp.Text = OX[idx]; // 顯示輸入 O 或 X
            TextToImage(gbva.ArrBtn[gbva.ThisIdx], gbva.OX[gbva.idx], gbva.ArrBtn[gbva.ThisIdx].Text);
            gbva.ArrOX[gbva.ThisIdx] = gbva.OX[gbva.idx];
            CheckLine(gbva.OX[gbva.idx]);

            gbva.idx = (gbva.idx + 1) % 2; // 設定下次為 O 或 X
            panel.Visible = false;
            if (gbva.IniWin) ClearBtn();
        }

        /// <summary>
        /// 顯示新的OX和判斷是否連線
        /// </summary>
        /// <param name="level"></param>
        private void Move_chick(int level)
        {
            //tmp.Text = OX[idx]; // 顯示輸入 O 或 X
            if (gbva.OldOX[gbva.ThisIdx] == null)
            {
                gbva.ArrBtn[gbva.ThisIdx].Image = null;
            }
            gbva.ArrBtn[gbva.MoveIdx].Image = null;
            TextToImage(gbva.ArrBtn[gbva.MoveIdx], gbva.OX[gbva.idx], gbva.ArrBtn[gbva.ThisIdx].Text);

            gbva.idx = (gbva.idx + 1) % 2; // 設定下次為 O 或 X
            gbva.ArrBtn[gbva.ThisIdx].Text = "0";
            gbva.ArrOX[gbva.ThisIdx] = "";
            panel.Visible = false;
            if (gbva.IniWin) ClearBtn();
        }

        #endregion

        #region 遊戲規則

        /// <summary>
        /// 遊戲規則
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Button2_Click(object sender, EventArgs e)
        {
            string gameRules = "規則說明\n\n" +
                "玩家任選一個大中小(各2隻)輪到的人只能有兩個選擇。\n\n" +
                "1.出一隻自己選擇的大小，放進場上的任一個空格中，或直接套到任何一隻比較小的上（可以是自己的），吃掉牠！\n\n" +
                "2.移動你在場上的一隻OX(被吃掉的OX不行)到任一個空格中，或套到另一隻比較小的OX上（可以是自己的）。\n\n" +
                "3.最後，連成一條線者則獲勝。";

            MessageBox.Show(gameRules, "遊戲規則");

        }
        #endregion

    }
}