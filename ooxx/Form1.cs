using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

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

                    int loc_x = gbva.X_left + ((i - 1) * 100) + 2;//��V
                    int loc_y = gbva.Y_top + ((j - 1) * 100) + 2;//���V


                    int Mloc_x = gbva.X_left + ((i - 1) * 100) + 2;//��V
                    int Mloc_y = gbva.Y_top + ((j - 1) * 100) + 25;//���V

                    gbva.ArrBtn[arr_Idx] = CreateBtn("0", arr_Idx.ToString(), loc_x, loc_y);
                    gbva.MoveBtn[arr_Idx] = CreateMoveBtn("����", arr_Idx.ToString(), Mloc_x, Mloc_y);

                    Controls.AddRange(new Control[] { gbva.MoveBtn[arr_Idx], gbva.ArrBtn[arr_Idx] });//�s�W��e��
                }
            }
        }

        /// <summary>
        /// PictureBox�إ�
        /// </summary>
        /// <param name="text">���h</param>
        /// <param name="name">�Ʀr</param>
        /// <param name="x">��V</param>
        /// <param name="y">���V</param>
        /// <returns>�^�ǫ��s�˦�</returns>
        private PictureBox CreateBtn(string text,string name, int x,int y) 
        {
            
            PictureBox button = new PictureBox();
            button.Size = new Size(gbva.X_width, gbva.Y_hight);
            button.Text = text;
            button.Name = name;
            button.Location = new Point(x, y);
            button.Click += new EventHandler(ArrBtn_Click);//�I��Ĳ�o
            button.MouseEnter += new EventHandler(PictureBox_MouseEnter);//�ƹ��a��Ĳ�o
            button.MouseLeave += new EventHandler(PictureBox_MouseLeave);//�ƹ����}Ĳ�o
            button.BackColor = Color.White;

            return button;
        }

        /// <summary>
        /// ���ʪ�PictureBox�إ�
        /// </summary>
        /// <param name="text">���h</param>
        /// <param name="name">�Ʀr</param>
        /// <param name="x">��V</param>
        /// <param name="y">���V</param>
        /// <returns>�^�ǫ��s�˦�</returns>
        private PictureBox CreateMoveBtn(string text, string name, int x, int y)
        {

            PictureBox button = new PictureBox();
            button.Size = new Size(80, 45);
            button.Text = text;
            button.Name = name;
            button.Location = new Point(x, y);
            TextToImage(button, "����", "4");
            button.Visible = false;
            button.Click += new EventHandler(ArrMove_Click);//�I��Ĳ�o
            button.BackColor = Color.White;

            return button;
        }
        #endregion


        #region ���s�I�������O��X

        /// <summary>
        /// ��ܧP�_�j�p�Ϊ̲��ʿﶵ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ArrBtn_Click(object sender, EventArgs e)
        {
            PictureBox tmp = (PictureBox)sender;//���������m
            tmp.Enabled = true;//�O�_�ٯ��I��

            int thisIdx = Convert.ToInt16(tmp.Name);

            //tmp.Text = OX[idx]; // ��ܿ�J O �� X
            TextToImage(tmp, gbva.OX[gbva.idx]);
            gbva.ArrOX[thisIdx] = gbva.OX[gbva.idx];

            CheckLine(gbva.OX[gbva.idx]);

            gbva.idx = (gbva.idx + 1) % 2; // �]�w�U���� O �� X
            if (gbva.IniWin) ClearBtn();
        }
        #endregion

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
                bitmap = new Bitmap(pictureBox1.Image);
            else
                bitmap = new Bitmap(gbva.X_width, gbva.Y_hight);
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
                gbva.Levenl[i] = 0;
                gbva.ArrBtn[i].Image = null;
                gbva.ArrOX[i] = null;
            }
            gbva.idx = 0;
            gbva.IniWin=false;
        } 
        #endregion
>>>>>>>>> Temporary merge branch 2
    }
}