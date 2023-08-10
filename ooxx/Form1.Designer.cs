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
            panel1 = new Panel();
            btnZoom = new Button();
            btnMove = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(352, 44);
            button1.Margin = new Padding(2);
            button1.Name = "button1";
            button1.Size = new Size(73, 23);
            button1.TabIndex = 0;
            button1.Text = "重新開始";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnZoom);
            panel1.Controls.Add(btnMove);
            panel1.Location = new Point(352, 217);
            panel1.Margin = new Padding(2);
            panel1.Name = "panel1";
            panel1.Size = new Size(73, 84);
            panel1.TabIndex = 1;
            panel1.Visible = false;
            // 
            // btnZoom
            // 
            btnZoom.Location = new Point(9, 45);
            btnZoom.Margin = new Padding(2);
            btnZoom.Name = "btnZoom";
            btnZoom.Size = new Size(51, 23);
            btnZoom.TabIndex = 1;
            btnZoom.Text = "放大";
            btnZoom.UseVisualStyleBackColor = true;
            // 
            // btnMove
            // 
            btnMove.Location = new Point(9, 9);
            btnMove.Margin = new Padding(2);
            btnMove.Name = "btnMove";
            btnMove.Size = new Size(51, 23);
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
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Panel panel1;
        private Button btnZoom;
        private Button btnMove;
    }
}