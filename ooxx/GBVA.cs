using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ooxx
{
    public class GBVA
    {
        public string[] ArrOX { get; set; }
        public PictureBox[] ArrBtn { get; set; }

        public int X_width { get; set; }
        public int Y_hight { get; set; }
        public int X_left { get; set; }
        public int Y_top { get; set; }
        public string[] OX { get; } = { "O", "X" };
        public int idx { get; set; } = 0;
        public GBVA()
        {
            ArrOX = new string[10];// 紀錄那些格子被占用
            ArrBtn = new PictureBox[10];// 紀錄井字 
            X_width = 97; // x 寬度
            Y_hight = 97; // y 寬度
            X_left = 50;  // 左上角 x
            Y_top = 50;   // 左上角 y
        }
    }
}
