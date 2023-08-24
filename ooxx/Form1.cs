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
        private PictureBox CreateBtn(string text, string name, int x, int y)
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

        #region ��ܧP�_�j�p�Ϊ̲��ʿﶵ

        /// <summary>
        /// ��ܧP�_�j�p�Ϊ̲��ʿﶵ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ArrBtn_Click(object sender, EventArgs e)
        {
            PictureBox tmp = (PictureBox)sender;//���������m
            gbva.ThisIdx = int.TryParse(tmp.Name, out int ThisValue) ? ThisValue : 0;
            int levenText = int.TryParse(gbva.ArrBtn[gbva.ThisIdx].Text, out int parsedValue) ? parsedValue : 0;

            //�j���p���ʿﶵ
            int levelIndex = (gbva.ArrOX[gbva.ThisIdx] == "��") ? 3 : 6;
            Big.Visible = levenText < 3 && gbva.Levenl[levelIndex] < 2;
            Mid.Visible = levenText < 2 && gbva.Levenl[levelIndex - 1] < 2;
            Small.Visible = levenText < 1 && gbva.Levenl[levelIndex - 2] < 2;

            if (gbva.ArrBtn[gbva.ThisIdx].Image != null && gbva.ArrOX[gbva.ThisIdx] == gbva.OX[gbva.idx])
                MoveVisible(gbva.ThisIdx);

            panel.Visible = true;

        }

        #region �p�ⶥ�żƶq

        /// <summary>
        /// �p��OX���żƶq
        /// </summary>
        private void Level_Check()
        {
            // ��l�ƶ��żƶq��0
            for (int level = 1; level < 10; level++)
            {
                gbva.Levenl[level] = 0;
            }
            for (int i = 1; i < 10; i++)
            {
                int btnindex = (gbva.ArrOX[i] == "��") ? 0 : 3;
                int oldindex = (gbva.OldOX[i] == "��") ? 0 : 3;
                UpdateLevenl(gbva.ArrBtn[i].Text, btnindex);
                UpdateLevenl(gbva.Oldlevenl[i], oldindex);
            }
        }
        /// <summary>
        /// ���żƶq�֭p
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

        /// <summary>
        /// ���ʫ��s���
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
        /// <summary>
        /// �]�w�i�H��J
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

        #region ���ū��s

        /// <summary>
        /// ���Ťj���s
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
        /// ���Ť����s
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
        /// ���Ťp���s
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

        #region �����I���ƥ�

        /// <summary>
        /// ���ʦs���¦�m�H�Υ洫��m
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ArrMove_Click(object sender, EventArgs e)
        {
            PictureBox tmp = (PictureBox)sender;//���������m
            gbva.MoveIdx = Convert.ToInt16(tmp.Name);//�s����m
            MoveVisible(0);

            Eatox(gbva.MoveIdx);

            gbva.ArrOX[gbva.MoveIdx] = gbva.OX[gbva.idx];//�s�����ª�OX
            gbva.ArrBtn[gbva.MoveIdx].Text = gbva.ArrBtn[gbva.ThisIdx].Text;//�s�����ª����h

            Move_chick(int.Parse(gbva.ArrBtn[gbva.ThisIdx].Text));
            if (gbva.OldOX[gbva.ThisIdx] != null)
            {
                gbva.ArrBtn[gbva.ThisIdx].Text = gbva.Oldlevenl[gbva.ThisIdx];//�s�����ʭ쥻�Q�Y���������ܦ^��
                gbva.ArrOX[gbva.ThisIdx] = gbva.OldOX[gbva.ThisIdx];//�s�����ʭ쥻�Q�Y����OX�ܦ^��
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
            gbva.OldOX[idx] = gbva.ArrOX[idx];//�x�s�Q�Y����OX
            gbva.Oldlevenl[idx] = gbva.ArrBtn[idx].Text;//�x�s�Q�Y�������h
        }

        #endregion

        #region ���s���OX�H�άO�_�s�u

        /// <summary>
        /// ���OX�H�άO�_�s�u
        /// </summary>
        /// <param name="level"></param>
        private void Btn_chick()
        {
            //tmp.Text = OX[idx]; // ��ܿ�J O �� X
            TextToImage(gbva.ArrBtn[gbva.ThisIdx], gbva.OX[gbva.idx], gbva.ArrBtn[gbva.ThisIdx].Text);
            gbva.ArrOX[gbva.ThisIdx] = gbva.OX[gbva.idx];
            CheckLine(gbva.OX[gbva.idx]);

            gbva.idx = (gbva.idx + 1) % 2; // �]�w�U���� O �� X
            panel.Visible = false;
            if (gbva.IniWin) ClearBtn();
        }

        /// <summary>
        /// ��ܷs��OX�M�P�_�O�_�s�u
        /// </summary>
        /// <param name="level"></param>
        private void Move_chick(int level)
        {
            //tmp.Text = OX[idx]; // ��ܿ�J O �� X
            if (gbva.OldOX[gbva.ThisIdx] == null)
            {
                gbva.ArrBtn[gbva.ThisIdx].Image = null;
            }
            gbva.ArrBtn[gbva.MoveIdx].Image = null;
            TextToImage(gbva.ArrBtn[gbva.MoveIdx], gbva.OX[gbva.idx], gbva.ArrBtn[gbva.ThisIdx].Text);

            gbva.idx = (gbva.idx + 1) % 2; // �]�w�U���� O �� X
            gbva.ArrBtn[gbva.ThisIdx].Text = "0";
            gbva.ArrOX[gbva.ThisIdx] = "";
            panel.Visible = false;
            if (gbva.IniWin) ClearBtn();
        }

        #endregion

        #region �C���W�h

        /// <summary>
        /// �C���W�h
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void Button2_Click(object sender, EventArgs e)
        {
            string gameRules = "�W�h����\n\n" +
                "���a����@�Ӥj���p(�U2��)���쪺�H�u�঳��ӿ�ܡC\n\n" +
                "1.�X�@���ۤv��ܪ��j�p�A��i���W�����@�ӪŮ椤�A�Ϊ����M�����@������p���W�]�i�H�O�ۤv���^�A�Y���e�I\n\n" +
                "2.���ʧA�b���W���@��OX(�Q�Y����OX����)����@�ӪŮ椤�A�ήM��t�@������p��OX�W�]�i�H�O�ۤv���^�C\n\n" +
                "3.�̫�A�s���@���u�̫h��ӡC";

            MessageBox.Show(gameRules, "�C���W�h");

        }
        #endregion

    }
}