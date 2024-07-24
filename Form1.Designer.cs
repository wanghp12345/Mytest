namespace Mytest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            panelAI = new Panel();
            label1 = new Label();
            btnPredict = new Button();
            btnColorTMono = new Button();
            log = new Sunny.UI.UIListBox();
            btnOpen = new Button();
            pictureBox1 = new PictureBox();
            uiPanel1 = new Sunny.UI.UIPanel();
            btnM = new Button();
            pictureBoxM = new PictureBox();
            btnLoadR = new Button();
            pictureBoxR = new PictureBox();
            btnLoadL = new Button();
            pictureBoxL = new PictureBox();
            uiPanel2 = new Sunny.UI.UIPanel();
            pictureBox5 = new PictureBox();
            button1 = new Button();
            pictureBox2 = new PictureBox();
            wImgBox1 = new ImageBox.WImgBox();
            panelAI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            uiPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxM).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxR).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxL).BeginInit();
            uiPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)wImgBox1).BeginInit();
            SuspendLayout();
            // 
            // panelAI
            // 
            panelAI.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelAI.BackColor = Color.SteelBlue;
            panelAI.Controls.Add(label1);
            panelAI.Controls.Add(btnPredict);
            panelAI.Controls.Add(btnColorTMono);
            panelAI.Controls.Add(log);
            panelAI.Controls.Add(btnOpen);
            panelAI.Controls.Add(pictureBox1);
            panelAI.Location = new Point(5, 29);
            panelAI.Margin = new Padding(1);
            panelAI.Name = "panelAI";
            panelAI.Size = new Size(749, 556);
            panelAI.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(20, 529);
            label1.Name = "label1";
            label1.Size = new Size(39, 20);
            label1.TabIndex = 5;
            label1.Text = "0.0.0";
            // 
            // btnPredict
            // 
            btnPredict.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnPredict.Location = new Point(228, 17);
            btnPredict.Name = "btnPredict";
            btnPredict.Size = new Size(88, 30);
            btnPredict.TabIndex = 4;
            btnPredict.Text = "推理";
            btnPredict.UseVisualStyleBackColor = true;
            btnPredict.Click += btnPredict_Click;
            // 
            // btnColorTMono
            // 
            btnColorTMono.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnColorTMono.Location = new Point(119, 17);
            btnColorTMono.Name = "btnColorTMono";
            btnColorTMono.Size = new Size(88, 30);
            btnColorTMono.TabIndex = 3;
            btnColorTMono.Text = "转灰度图";
            btnColorTMono.UseVisualStyleBackColor = true;
            btnColorTMono.Click += btnColorTMono_Click;
            // 
            // log
            // 
            log.Font = new Font("宋体", 7.5F, FontStyle.Regular, GraphicsUnit.Point);
            log.HoverColor = Color.FromArgb(155, 200, 255);
            log.ItemSelectForeColor = Color.White;
            log.Location = new Point(532, 60);
            log.Margin = new Padding(4, 5, 4, 5);
            log.MinimumSize = new Size(1, 1);
            log.Name = "log";
            log.Padding = new Padding(2);
            log.SelectionMode = SelectionMode.MultiExtended;
            log.ShowText = false;
            log.Size = new Size(203, 466);
            log.Sorted = true;
            log.TabIndex = 2;
            log.Text = "uiListBox1";
            // 
            // btnOpen
            // 
            btnOpen.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnOpen.Location = new Point(15, 17);
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new Size(88, 30);
            btnOpen.TabIndex = 1;
            btnOpen.Text = "加载图片";
            btnOpen.UseVisualStyleBackColor = true;
            btnOpen.Click += btnOpen_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Black;
            pictureBox1.Location = new Point(15, 60);
            pictureBox1.Margin = new Padding(1);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(512, 466);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.MouseLeave += pictureBox1_MouseLeave;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            // 
            // uiPanel1
            // 
            uiPanel1.Controls.Add(btnM);
            uiPanel1.Controls.Add(pictureBoxM);
            uiPanel1.Controls.Add(btnLoadR);
            uiPanel1.Controls.Add(pictureBoxR);
            uiPanel1.Controls.Add(btnLoadL);
            uiPanel1.Controls.Add(pictureBoxL);
            uiPanel1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
            uiPanel1.Location = new Point(781, 29);
            uiPanel1.Margin = new Padding(4, 5, 4, 5);
            uiPanel1.MinimumSize = new Size(1, 1);
            uiPanel1.Name = "uiPanel1";
            uiPanel1.Size = new Size(927, 556);
            uiPanel1.TabIndex = 1;
            uiPanel1.Text = "uiPanel1";
            uiPanel1.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // btnM
            // 
            btnM.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnM.Location = new Point(420, 289);
            btnM.Name = "btnM";
            btnM.Size = new Size(98, 30);
            btnM.TabIndex = 6;
            btnM.Text = "合并L+R";
            btnM.UseVisualStyleBackColor = true;
            btnM.Click += btnM_Click;
            // 
            // pictureBoxM
            // 
            pictureBoxM.Location = new Point(19, 321);
            pictureBoxM.Name = "pictureBoxM";
            pictureBoxM.Size = new Size(880, 228);
            pictureBoxM.TabIndex = 5;
            pictureBoxM.TabStop = false;
            // 
            // btnLoadR
            // 
            btnLoadR.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnLoadR.Location = new Point(631, 17);
            btnLoadR.Name = "btnLoadR";
            btnLoadR.Size = new Size(96, 30);
            btnLoadR.TabIndex = 4;
            btnLoadR.Text = "加载图片R";
            btnLoadR.UseVisualStyleBackColor = true;
            btnLoadR.Click += btnLoadR_Click;
            // 
            // pictureBoxR
            // 
            pictureBoxR.Location = new Point(462, 60);
            pictureBoxR.Name = "pictureBoxR";
            pictureBoxR.Size = new Size(437, 228);
            pictureBoxR.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxR.TabIndex = 3;
            pictureBoxR.TabStop = false;
            // 
            // btnLoadL
            // 
            btnLoadL.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnLoadL.Location = new Point(176, 17);
            btnLoadL.Name = "btnLoadL";
            btnLoadL.Size = new Size(98, 30);
            btnLoadL.TabIndex = 2;
            btnLoadL.Text = "加载图片L";
            btnLoadL.UseVisualStyleBackColor = true;
            btnLoadL.Click += btnLoadL_Click;
            // 
            // pictureBoxL
            // 
            pictureBoxL.Location = new Point(19, 60);
            pictureBoxL.Name = "pictureBoxL";
            pictureBoxL.Size = new Size(437, 228);
            pictureBoxL.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxL.TabIndex = 0;
            pictureBoxL.TabStop = false;
            // 
            // uiPanel2
            // 
            uiPanel2.Controls.Add(pictureBox5);
            uiPanel2.Controls.Add(button1);
            uiPanel2.Controls.Add(pictureBox2);
            uiPanel2.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
            uiPanel2.Location = new Point(10, 603);
            uiPanel2.Margin = new Padding(4, 5, 4, 5);
            uiPanel2.MinimumSize = new Size(1, 1);
            uiPanel2.Name = "uiPanel2";
            uiPanel2.Size = new Size(1227, 419);
            uiPanel2.TabIndex = 2;
            uiPanel2.Text = "uiPanel2";
            uiPanel2.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // pictureBox5
            // 
            pictureBox5.Location = new Point(680, 53);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(525, 352);
            pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox5.TabIndex = 4;
            pictureBox5.TabStop = false;
            // 
            // button1
            // 
            button1.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button1.Location = new Point(55, 17);
            button1.Name = "button1";
            button1.Size = new Size(98, 30);
            button1.TabIndex = 3;
            button1.Text = "加载图片L";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(24, 53);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(624, 352);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 0;
            pictureBox2.TabStop = false;
            pictureBox2.Paint += pictureBox2_Paint;
            pictureBox2.MouseDown += pictureBox2_MouseDown;
            pictureBox2.MouseMove += pictureBox2_MouseMove;
            pictureBox2.MouseUp += pictureBox2_MouseUp;
            // 
            // wImgBox1
            // 
            wImgBox1.BackColor = Color.Black;
            wImgBox1.Image = (Image)resources.GetObject("wImgBox1.Image");
            wImgBox1.Location = new Point(1272, 620);
            wImgBox1.Name = "wImgBox1";
            wImgBox1.Size = new Size(325, 251);
            wImgBox1.TabIndex = 3;
            wImgBox1.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoValidate = AutoValidate.Disable;
            BackColor = Color.FromArgb(224, 232, 239);
            ClientSize = new Size(1920, 1031);
            Controls.Add(wImgBox1);
            Controls.Add(uiPanel2);
            Controls.Add(uiPanel1);
            Controls.Add(panelAI);
            EffectBack = Color.WhiteSmoke;
            Font = new Font("Microsoft YaHei UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            ForeColor = Color.Black;
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(6, 5, 6, 5);
            Mobile = CCWin.MobileStyle.None;
            Name = "Form1";
            Radius = 10;
            RoundStyle = CCWin.SkinClass.RoundStyle.Top;
            ShadowWidth = 5;
            ShowBorder = false;
            ShowDrawIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Inlit Technology Company";
            TitleCenter = true;
            TitleColor = Color.GreenYellow;
            TitleSuitColor = true;
            FormClosing += Form1_FormClosing;
            panelAI.ResumeLayout(false);
            panelAI.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            uiPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxM).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxR).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxL).EndInit();
            uiPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)wImgBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelAI;
        private PictureBox pictureBox1;
        private Button btnOpen;
        private Sunny.UI.UIListBox log;
        private Button btnColorTMono;
        private Button btnPredict;
        private Label label1;
        private Sunny.UI.UIPanel uiPanel1;
        private Button btnLoadL;
        private PictureBox pictureBoxL;
        private Button btnLoadR;
        private PictureBox pictureBoxR;
        private Button btnM;
        private PictureBox pictureBoxM;
        private Sunny.UI.UIPanel uiPanel2;
        private PictureBox pictureBox3;
        private Button button3;
        private PictureBox pictureBox4;
        private Button button1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox5;
        private ImageBox.WImgBox wImgBox1;
    }
}
