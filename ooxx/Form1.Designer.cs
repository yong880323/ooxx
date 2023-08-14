namespace ooxx
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            btnZoom = new Button();
            btnMove = new Button();
            Mid = new Button();
            Big = new Button();
            Small = new Button();
            panel = new Panel();
            button2 = new Button();
            panel.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(354, 88);
            button1.Margin = new Padding(2);
            button1.Name = "button1";
            button1.Size = new Size(73, 23);
            button1.TabIndex = 0;
            button1.Text = "重新開始";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // btnZoom
            // 
            btnZoom.Location = new Point(0, 0);
            btnZoom.Name = "btnZoom";
            btnZoom.Size = new Size(75, 23);
            btnZoom.TabIndex = 0;
            // 
            // btnMove
            // 
            btnMove.Location = new Point(0, 0);
            btnMove.Name = "btnMove";
            btnMove.Size = new Size(75, 23);
            btnMove.TabIndex = 0;
            // 
            // Mid
            // 
            Mid.Location = new Point(0, 38);
            Mid.Name = "Mid";
            Mid.Size = new Size(75, 23);
            Mid.TabIndex = 1;
            Mid.Text = "中";
            Mid.UseVisualStyleBackColor = true;
            Mid.Visible = false;
            Mid.Click += Mid_Click;
            // 
            // Big
            // 
            Big.Location = new Point(0, 9);
            Big.Name = "Big";
            Big.Size = new Size(75, 23);
            Big.TabIndex = 0;
            Big.Text = "大";
            Big.UseVisualStyleBackColor = true;
            Big.Visible = false;
            Big.Click += Big_Click;
            // 
            // Small
            // 
            Small.Location = new Point(0, 67);
            Small.Name = "Small";
            Small.Size = new Size(75, 23);
            Small.TabIndex = 2;
            Small.Text = "小";
            Small.UseVisualStyleBackColor = true;
            Small.Visible = false;
            Small.Click += Small_Click;
            // 
            // panel
            // 
            panel.Controls.Add(Big);
            panel.Controls.Add(Small);
            panel.Controls.Add(Mid);
            panel.Location = new Point(352, 145);
            panel.Name = "panel";
            panel.Size = new Size(75, 92);
            panel.TabIndex = 3;
            panel.Visible = false;
            // 
            // button2
            // 
            button2.Location = new Point(354, 33);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 4;
            button2.Text = "遊戲規則";
            button2.UseVisualStyleBackColor = true;
            button2.Click += Button2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(453, 354);
            Controls.Add(button2);
            Controls.Add(panel);
            Controls.Add(button1);
            Margin = new Padding(2);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            panel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button btnZoom;
        private Button btnMove;
        private Button Mid;
        private Button Big;
        private Button Small;
        private Panel panel;
        private Button button2;
    }
}