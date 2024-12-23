
namespace spaceWar
{
    partial class IntroForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IntroForm));
            welcomeLabel = new Label();
            button1 = new Button();
            label1 = new Label();
            button2 = new Button();
            label2 = new Label();
            playerNameTextBox = new TextBox();
            button3 = new Button();
            SuspendLayout();
            // 
            // welcomeLabel
            // 
            welcomeLabel.AutoSize = true;
            welcomeLabel.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 162);
            welcomeLabel.ForeColor = SystemColors.ButtonFace;
            welcomeLabel.Location = new Point(166, 21);
            welcomeLabel.Name = "welcomeLabel";
            welcomeLabel.Size = new Size(312, 41);
            welcomeLabel.TabIndex = 1;
            welcomeLabel.Text = "CHAOS OF THE SPACE";
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ControlText;
            button1.ForeColor = Color.Firebrick;
            button1.Location = new Point(275, 286);
            button1.Name = "button1";
            button1.Size = new Size(189, 69);
            button1.TabIndex = 2;
            button1.Text = "Start";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label1.ForeColor = SystemColors.ButtonFace;
            label1.Location = new Point(-1, 433);
            label1.Name = "label1";
            label1.Size = new Size(209, 20);
            label1.TabIndex = 3;
            label1.Text = "Hasan Bayraktar 240229090";
            // 
            // button2
            // 
            button2.BackColor = SystemColors.ControlText;
            button2.ForeColor = Color.Firebrick;
            button2.Location = new Point(275, 361);
            button2.Name = "button2";
            button2.Size = new Size(189, 69);
            button2.TabIndex = 4;
            button2.Text = "Quit";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 162);
            label2.ForeColor = SystemColors.ButtonFace;
            label2.Location = new Point(45, 243);
            label2.Name = "label2";
            label2.Size = new Size(185, 41);
            label2.TabIndex = 5;
            label2.Text = " PlayerName";
            // 
            // playerNameTextBox
            // 
            playerNameTextBox.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 162);
            playerNameTextBox.Location = new Point(275, 243);
            playerNameTextBox.Name = "playerNameTextBox";
            playerNameTextBox.Size = new Size(189, 43);
            playerNameTextBox.TabIndex = 6;
            // 
            // button3
            // 
            button3.BackColor = SystemColors.ControlText;
            button3.ForeColor = Color.Firebrick;
            button3.Location = new Point(531, 361);
            button3.Name = "button3";
            button3.Size = new Size(189, 69);
            button3.TabIndex = 7;
            button3.Text = "SCORES";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // IntroForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(732, 453);
            ControlBox = false;
            Controls.Add(button3);
            Controls.Add(welcomeLabel);
            Controls.Add(playerNameTextBox);
            Controls.Add(label2);
            Controls.Add(button2);
            Controls.Add(label1);
            Controls.Add(button1);
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "IntroForm";
            StartPosition = FormStartPosition.CenterScreen;
            TopMost = true;
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion

        private Label welcomeLabel;
        private Button button1;
        private Label label1;
        private Button button2;
        private Label label2;
        private TextBox playerNameTextBox;
        private Button button3;
    }
}
