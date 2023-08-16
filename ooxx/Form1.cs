using System;
using System.Drawing;
using System.Windows.Forms;
namespace ooxx
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// �ޤJ�@�ΰѼ�
        /// </summary>
        private readonly GBVA gbva = new();
        public Form1()
        {
            InitializeComponent();
        }

        #region ø�s�E�c��
        /// <summary>
        /// ���gOnPaint��k ø�s�E�c��
        /// </summary>
        /// <param name="e">ø�Ϭ����Ѽ�</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // ����@�� Graphics ��H�A�Ω�ø��
            Graphics GPS = e.Graphics;
            //�Ыؤ@�Ӭ��� �e�׬O2�ӳ�� ���e����H
            Pen MyPen = new(Color.Red, 2f);
            #region ø��

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    //ø�s�����u
                    GPS.DrawLine(MyPen, gbva.X_left + j * 100, gbva.Y_top,
                                        gbva.X_left + j * 100, gbva.Y_top * 7);
                }
                //ø�s����u
                GPS.DrawLine(MyPen, gbva.X_left, gbva.Y_top + i * 100,
                                    gbva.X_left * 7, gbva.Y_top + i * 100);
            }

            #endregion

        }
        #endregion

        #region ���ͤE�c����s

        private void Form1_Load(object sender, EventArgs e)
        {
            InitBtn();
        }

        /// <summary>
        /// �E�c�涶�ǱƦC���ͫ��s����ܨ�e��
        /// </summary>
        private void InitBtn()
        {
            //����10�ӫ��s�}�C
            for (int i = 0; i < 10; i++)
            {
                gbva.ArrOX[i] = "";//��l�Q�e��
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
            //BTN�ӤE�c�涶�ǱƦC
            for (int i = 1; i < 4; i++)
            {
                for (int j = 1; j < 4; j++)
                {
                    int arr_Idx = ((i - 1) * 3) + j;

                    gbva.ArrBtn[arr_Idx].Size = new Size(gbva.X_width, gbva.Y_hight);

                    gbva.ArrBtn[arr_Idx].Text = "0";//�Ȭݤ��X��
                    gbva.ArrBtn[arr_Idx].Name = arr_Idx.ToString();
                    gbva.ArrBtn[arr_Idx].Click += new EventHandler(ArrBtn_Click);//�I��Ĳ�o
                    gbva.ArrBtn[arr_Idx].MouseEnter += new EventHandler(PictureBox_MouseEnter);//�ƹ��a��Ĳ�o
                    gbva.ArrBtn[arr_Idx].MouseLeave += new EventHandler(PictureBox_MouseLeave);//�ƹ����}Ĳ�o
                    int loc_x = gbva.X_left + ((i - 1) * 100) + 2;//��V
                    int loc_y = gbva.Y_top + ((j - 1) * 100) + 2;//���V
                    gbva.ArrBtn[arr_Idx].Location = new Point(loc_x, loc_y);

                    gbva.MoveBtn[arr_Idx].Text = "����";//�Ȭݤ��X��
                    TextToImage(gbva.MoveBtn[arr_Idx], "����", "4");
                    gbva.MoveBtn[arr_Idx].Name = arr_Idx.ToString();
                    gbva.MoveBtn[arr_Idx].Size = new Size(80, 50);
                    gbva.MoveBtn[arr_Idx].Visible = false;
                    /* gbva.MoveBtn[arr_Idx].Click += new EventHandler(ArrBtn_Click);//�I��Ĳ�o
                     gbva.MoveBtn[arr_Idx].MouseEnter += new EventHandler(PictureBox_MouseEnter);//�ƹ��a��Ĳ�o
                     gbva.MoveBtn[arr_Idx].MouseLeave += new EventHandler(PictureBox_MouseLeave);//�ƹ����}Ĳ�o*/
                    int Mloc_x = gbva.X_left + ((i - 1) * 100) + 2;//��V
                    int Mloc_y = gbva.Y_top + ((j - 1) * 100) + 50;//���V
                    gbva.MoveBtn[arr_Idx].Location = new Point(Mloc_x, Mloc_y);
                    Controls.Add(gbva.MoveBtn[arr_Idx]);//�s�W��e��
                    Controls.Add(gbva.ArrBtn[arr_Idx]);//�s�W��e��
                }
            }
        }

        #endregion

        #region ���s�I�������O��X

        /// <summary>
        /// �I�������O��X�H�ΤU�����H
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ArrBtn_Click(object sender, EventArgs e)
        {
            PictureBox tmp = (PictureBox)sender;//���������m
            gbva.ThisIdx = Convert.ToInt16(tmp.Name);
            gbva.OldOX[gbva.ThisIdx] = gbva.ArrOX[gbva.ThisIdx];
            gbva.Oldlevenl[gbva.ThisIdx] = int.Parse(gbva.ArrBtn[gbva.ThisIdx].Text);

            int btnText = int.Parse(gbva.ArrBtn[gbva.ThisIdx].Text);
            int levelIndex = (gbva.ArrOX[gbva.ThisIdx] == "��") ? 3 : 6;

            Big.Visible = btnText < 3 && gbva.Levenl[levelIndex] < 2;
            Mid.Visible = btnText < 2 && gbva.Levenl[levelIndex - 1] < 2;
            Small.Visible = btnText < 1 && gbva.Levenl[levelIndex - 2] < 2;

            panel.Visible = true;

        }

        #endregion

        #region �r��OX�ܦ��Ϯ����
        /// <summary>
        /// �r��OX�ܦ��Ϯ����
        /// </summary>
        /// <param name="pictureBox1"></param>
        /// <param name="str_OX"></param>
        private void TextToImage(PictureBox pictureBox1, string str_OX, string level)
        {
            Color BackColor = Color.Transparent;
            String FontName = "Times New Roman";
            int FontSize = (level == "2") ? 40 :
                           (level == "3") ? 50 : 20;//�ϧΤj�p

            Bitmap bitmap;
            if (pictureBox1.Image != null)
            {
                bitmap = new Bitmap(pictureBox1.Image);
            }
            else
            {
                bitmap = new Bitmap(gbva.X_width, gbva.Y_hight);
            }
            //�]�w�@�ӵe�� graphics
            Graphics graphics = Graphics.FromImage(bitmap);
            //�C��
            Color color = Color.Transparent;
            //�ϧΤj�p
            Font font = new(FontName, FontSize);
            //����
            SolidBrush BrushBackColor = new(BackColor);
            //�e��(���C��)
            Pen BorderPen = new(color);
            //ø�s�x�� (Point=�I���y��)
            Rectangle displayRectangle = new(new Point(0, 0), new Size(Width - 1, Height - 1));
            //�񺡭I��(�C��,�x��)
            graphics.FillRectangle(BrushBackColor, displayRectangle);
            //�e�X��دx��
            graphics.DrawRectangle(BorderPen, displayRectangle);
            //��ܵe�XO��X
            if (str_OX == "��")
                graphics.DrawString(str_OX, font, Brushes.Aqua, 0, 5);
            else
                graphics.DrawString(str_OX, font, Brushes.Magenta, 0, 5);
            pictureBox1.Image = bitmap;

        }

        #endregion

        #region PictureBox�ƥ�

        /// <summary>
        /// �ƹ��a���Ĳ�o
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox_MouseEnter(object sender, EventArgs e)
        {
            PictureBox tmp = (PictureBox)sender;
            tmp.BackColor = Color.Red;
        }
        /// <summary>
        /// �ƹ����}��Ĳ�o
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBox_MouseLeave(object sender, EventArgs e)
        {
            PictureBox tmp = (PictureBox)sender;
            tmp.BackColor = Color.White;
        }
        /// <summary>
        /// �]�w�L�k��J
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
        private void SetPictureBoxEnable(int btn_Idx)
        {
            for (int i = 1; i < 10; i++)
            {
                if (gbva.ArrBtn[i].Text != "3")
                {
                    gbva.ArrBtn[i].Enabled = true;
                    gbva.ArrBtn[i].BackColor = SystemColors.Window;
                }
            }
            if (btn_Idx > 0)
            {
                gbva.ArrBtn[btn_Idx].Enabled = false;
                gbva.ArrBtn[btn_Idx].BackColor = SystemColors.ControlLight;
            }
        }

        #endregion

        #region Bingo�s�u

        /// <summary>
        /// �T�{�O�_�s�u
        /// </summary>
        /// <param name="thisIdx"></param>
        /// <param name="str_OX">�ثe�O��Τe</param>
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
                    if (str_OX == "��")
                    {
                        MessageBox.Show("�� Ĺ�F");
                        gbva.IniWin = true;
                    }
                    else
                    {
                        MessageBox.Show("�� Ĺ�F");
                        gbva.IniWin = true;
                    }
                }
            }

        }

        #endregion

        #region ���s�}�l

        /// <summary>
        /// ���s�}�l
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, EventArgs e)
        {
            ClearBtn();
        }

        /// <summary>
        /// ���s�}�l�٭�w�]
        /// </summary>
        private void ClearBtn()
        {
            for (int i = 0; i < 10; i++)
            {
                gbva.ArrBtn[i].Image = null;
                gbva.ArrOX[i] = null;
                gbva.ArrBtn[i].Text = "0";
            }
            gbva.idx = 0;
            gbva.IniWin = false;
            SetPictureBoxEnable(0);
        }
        #endregion

        #region ���ū��s

        /// <summary>
        /// ���Ťj���s
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Big_Click(object sender, EventArgs e)
        {
            gbva.ArrBtn[gbva.ThisIdx].Text = "3";
            Btn_chick(gbva.ArrLevel[2]);
            Level_Check();
        }

        /// <summary>
        /// ���Ť����s
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Mid_Click(object sender, EventArgs e)
        {
            gbva.ArrBtn[gbva.ThisIdx].Text = "2";
            Btn_chick(gbva.ArrLevel[1]);
            Level_Check();
        }

        /// <summary>
        /// ���Ťp���s
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Small_Click(object sender, EventArgs e)
        {
            gbva.ArrBtn[gbva.ThisIdx].Text = "1";
            Btn_chick(gbva.ArrLevel[0]);
            Level_Check();
        }

        #endregion

        #region ���s�����U�اP�_

        /// <summary>
        /// ���s����U�P�_
        /// </summary>
        /// <param name="level"></param>
        private void Btn_chick(int level)
        {
            //tmp.Text = OX[idx]; // ��ܿ�J O �� X
            gbva.ArrBtn[gbva.ThisIdx].Image = null;
            TextToImage(gbva.ArrBtn[gbva.ThisIdx], gbva.OX[gbva.idx], gbva.ArrBtn[gbva.ThisIdx].Text);

            gbva.ArrOX[gbva.ThisIdx] = gbva.OX[gbva.idx];
            CheckLine(gbva.OX[gbva.idx]);
            gbva.idx = (gbva.idx + 1) % 2; // �]�w�U���� O �� X
            /* if (level == 3)
             {

             }
             else
             {
                 gbva.ArrBtn[gbva.thisIdx].Enabled = true;//�O�_�ٯ��I��

             }*/
            panel.Visible = false;
            if (gbva.IniWin) ClearBtn();
        }

        #endregion

        #region �p�ⵥ�żƶq

        /// <summary>
        /// �p��OX���żƶq
        /// </summary>
        private void Level_Check()
        {
            for (int i = 1; i < 10; i++)
            {
                gbva.Levenl[i] = 0;
                int btnindex = (gbva.ArrOX[i] == "��") ? 0 : 3;
                switch (gbva.ArrBtn[i].Text)
                {
                    case "1":
                        gbva.Levenl[btnindex + 1]++;
                        break;
                    case "2":
                        gbva.Levenl[btnindex + 2]++;
                        break;
                    case "3":
                        gbva.Levenl[btnindex + 3]++;
                        break;

                }
            }
        }

        #endregion
    }
}