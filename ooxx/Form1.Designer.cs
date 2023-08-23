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
<<<<<<<<< Temporary merge branch 1
            button1.Location = new Point(453, 56);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 0;
            button1.Text = "遊戲開始";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(583, 448);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
=========
            button1.Location = new Point(352, 44);
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
            btnMove.Text = "移動";
            btnMove.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(453, 354);
            Controls.Add(panel1);
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
>>>>>>>>> Temporary merge branch 2
    }
}