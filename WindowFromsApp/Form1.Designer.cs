using System.Drawing;
using System.Windows.Forms;

namespace WindowFromsApp
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
            UploadedPicture = new PictureBox();
            ModifiedPicture = new PictureBox();
            button2 = new Button();
            button1 = new Button();
            SaveButton = new Button();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton3 = new RadioButton();
            radioButton4 = new RadioButton();
            radioButton5 = new RadioButton();
            radioButton6 = new RadioButton();
            radioButton7 = new RadioButton();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            ((System.ComponentModel.ISupportInitialize)UploadedPicture).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ModifiedPicture).BeginInit();
            SuspendLayout();
            // 
            // UploadedPicture
            // 
            UploadedPicture.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            UploadedPicture.BackColor = Color.White;
            UploadedPicture.Location = new Point(50, 50);
            UploadedPicture.Name = "UploadedPicture";
            UploadedPicture.Size = new Size(650, 550);
            UploadedPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            UploadedPicture.TabIndex = 0;
            UploadedPicture.TabStop = false;
            // 
            // ModifiedPicture
            // 
            ModifiedPicture.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ModifiedPicture.BackColor = Color.White;
            ModifiedPicture.Location = new Point(785, 50);
            ModifiedPicture.Name = "ModifiedPicture";
            ModifiedPicture.Size = new Size(650, 550);
            ModifiedPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            ModifiedPicture.TabIndex = 1;
            ModifiedPicture.TabStop = false;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button2.Location = new Point(218, 640);
            button2.Name = "button2";
            button2.Size = new Size(200, 80);
            button2.TabIndex = 3;
            button2.Text = "Upload Image";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom;
            button1.Location = new Point(645, 640);
            button1.Name = "button1";
            button1.Size = new Size(200, 80);
            button1.TabIndex = 4;
            button1.Text = "Apply Bloom Effect";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // SaveButton
            // 
            SaveButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            SaveButton.Location = new Point(1019, 640);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(200, 80);
            SaveButton.TabIndex = 5;
            SaveButton.Text = "Save";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += Save_Click;
            // 
            // radioButton1
            // 
            radioButton1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            radioButton1.AutoSize = true;
            radioButton1.BackColor = SystemColors.ActiveBorder;
            radioButton1.Font = new Font("Segoe UI", 19F, FontStyle.Regular, GraphicsUnit.Point);
            radioButton1.Location = new Point(283, 773);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(14, 13);
            radioButton1.TabIndex = 6;
            radioButton1.TabStop = true;
            radioButton1.UseVisualStyleBackColor = false;
            radioButton1.CheckedChanged += radioButton1_CheckedChanged;
            // 
            // radioButton2
            // 
            radioButton2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            radioButton2.AutoSize = true;
            radioButton2.Font = new Font("Segoe UI", 19F, FontStyle.Regular, GraphicsUnit.Point);
            radioButton2.Location = new Point(364, 773);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(14, 13);
            radioButton2.TabIndex = 7;
            radioButton2.TabStop = true;
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += radioButton2_CheckedChanged;
            // 
            // radioButton3
            // 
            radioButton3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(453, 772);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(14, 13);
            radioButton3.TabIndex = 8;
            radioButton3.TabStop = true;
            radioButton3.UseVisualStyleBackColor = true;
            radioButton3.CheckedChanged += radioButton3_CheckedChanged;
            // 
            // radioButton4
            // 
            radioButton4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            radioButton4.AutoSize = true;
            radioButton4.Location = new Point(532, 772);
            radioButton4.Name = "radioButton4";
            radioButton4.Size = new Size(14, 13);
            radioButton4.TabIndex = 9;
            radioButton4.TabStop = true;
            radioButton4.UseVisualStyleBackColor = true;
            radioButton4.CheckedChanged += radioButton4_CheckedChanged;
            // 
            // radioButton5
            // 
            radioButton5.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            radioButton5.AutoSize = true;
            radioButton5.Location = new Point(606, 772);
            radioButton5.Name = "radioButton5";
            radioButton5.Size = new Size(14, 13);
            radioButton5.TabIndex = 10;
            radioButton5.TabStop = true;
            radioButton5.UseVisualStyleBackColor = true;
            radioButton5.CheckedChanged += radioButton5_CheckedChanged;
            // 
            // radioButton6
            // 
            radioButton6.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            radioButton6.AutoSize = true;
            radioButton6.Location = new Point(700, 772);
            radioButton6.Name = "radioButton6";
            radioButton6.Size = new Size(14, 13);
            radioButton6.TabIndex = 11;
            radioButton6.TabStop = true;
            radioButton6.UseVisualStyleBackColor = true;
            radioButton6.CheckedChanged += radioButton6_CheckedChanged;
            // 
            // radioButton7
            // 
            radioButton7.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            radioButton7.AutoSize = true;
            radioButton7.Location = new Point(785, 772);
            radioButton7.Name = "radioButton7";
            radioButton7.Size = new Size(14, 13);
            radioButton7.TabIndex = 12;
            radioButton7.TabStop = true;
            radioButton7.UseVisualStyleBackColor = true;
            radioButton7.CheckedChanged += radioButton7_CheckedChanged;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label1.AutoSize = true;
            label1.BackColor = SystemColors.AppWorkspace;
            label1.Font = new Font("Segoe UI", 19F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(298, 756);
            label1.Name = "label1";
            label1.Size = new Size(29, 36);
            label1.TabIndex = 13;
            label1.Text = "1";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 19F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(379, 756);
            label2.Name = "label2";
            label2.Size = new Size(29, 36);
            label2.TabIndex = 14;
            label2.Text = "2";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 19F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.ButtonHighlight;
            label3.Location = new Point(470, 756);
            label3.Name = "label3";
            label3.Size = new Size(29, 36);
            label3.TabIndex = 15;
            label3.Text = "4";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 19F, FontStyle.Regular, GraphicsUnit.Point);
            label4.ForeColor = SystemColors.ButtonHighlight;
            label4.Location = new Point(553, 756);
            label4.Name = "label4";
            label4.Size = new Size(29, 36);
            label4.TabIndex = 16;
            label4.Text = "8";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label5.AutoSize = true;
            label5.BackColor = SystemColors.AppWorkspace;
            label5.Font = new Font("Segoe UI", 19F, FontStyle.Regular, GraphicsUnit.Point);
            label5.ForeColor = SystemColors.ButtonHighlight;
            label5.Location = new Point(623, 756);
            label5.Name = "label5";
            label5.Size = new Size(43, 36);
            label5.TabIndex = 17;
            label5.Text = "16";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 19F, FontStyle.Regular, GraphicsUnit.Point);
            label6.ForeColor = SystemColors.ButtonHighlight;
            label6.Location = new Point(716, 756);
            label6.Name = "label6";
            label6.Size = new Size(43, 36);
            label6.TabIndex = 18;
            label6.Text = "32";
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 19F, FontStyle.Regular, GraphicsUnit.Point);
            label7.ForeColor = SystemColors.ButtonHighlight;
            label7.Location = new Point(806, 756);
            label7.Name = "label7";
            label7.Size = new Size(43, 36);
            label7.TabIndex = 19;
            label7.Text = "64";
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 19F, FontStyle.Regular, GraphicsUnit.Point);
            label8.ForeColor = SystemColors.ButtonHighlight;
            label8.Location = new Point(39, 756);
            label8.Name = "label8";
            label8.Size = new Size(238, 36);
            label8.TabIndex = 20;
            label8.Text = "Number of threads:";
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 19F, FontStyle.Regular, GraphicsUnit.Point);
            label9.ForeColor = SystemColors.ButtonHighlight;
            label9.Location = new Point(39, 816);
            label9.Name = "label9";
            label9.Size = new Size(320, 36);
            label9.TabIndex = 21;
            label9.Text = "Average time per iteration:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(364, 0);
            label10.Name = "label10";
            label10.Size = new Size(44, 15);
            label10.TabIndex = 22;
            label10.Text = "label10";
            // 
            // Form1
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = SystemColors.AppWorkspace;
            ClientSize = new Size(1484, 861);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(radioButton7);
            Controls.Add(radioButton6);
            Controls.Add(radioButton5);
            Controls.Add(radioButton4);
            Controls.Add(radioButton3);
            Controls.Add(radioButton2);
            Controls.Add(radioButton1);
            Controls.Add(SaveButton);
            Controls.Add(button1);
            Controls.Add(button2);
            Controls.Add(ModifiedPicture);
            Controls.Add(UploadedPicture);
            Name = "Form1";
            Text = "Bloom Effect";
            ((System.ComponentModel.ISupportInitialize)UploadedPicture).EndInit();
            ((System.ComponentModel.ISupportInitialize)ModifiedPicture).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox UploadedPicture;
        private PictureBox ModifiedPicture;
        private Button button2;
        private Button button1;
        private Button SaveButton;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private RadioButton radioButton3;
        private RadioButton radioButton4;
        private RadioButton radioButton5;
        private RadioButton radioButton6;
        private RadioButton radioButton7;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
    }
}

