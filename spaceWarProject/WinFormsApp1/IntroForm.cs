using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;
using System.Drawing.Text;
using System.Media;
using System.Windows.Forms;

namespace spaceWar
{
    public partial class IntroForm : Form
    {
        private SpaceWarGame game;

        public IntroForm(SpaceWarGame gameInstance)
        {
            InitializeComponent();
            LoadCustomFont();
            game = gameInstance;
            game.StartGame();
        }

        private void LoadCustomFont()
        {
            PrivateFontCollection privateFonts = new PrivateFontCollection();
            privateFonts.AddFontFile(@"Resources\\Silkscreen-Regular.ttf"); // Font dosyas�n�n do�ru yolda oldu�undan emin olun
            Font customFont = new Font(privateFonts.Families[0], 20);
            Font customFont1 = new Font(privateFonts.Families[0], 14);
            welcomeLabel.Font = customFont; // label1 kontrol�n�n var oldu�undan emin olun
            button1.Font = customFont; // button1 kontrol�n�n var oldu�undan emin olun
            button2.Font = customFont; // button2 kontrol�n�n var oldu�undan emin olun
            label2.Font = customFont1; // label2 kontrol�n�n var oldu�undan emin olun
            playerNameTextBox.Font = customFont1;
            button3.Font = customFont;
        }


        private void GameTimer_Tick(object sender, EventArgs e)
        {
            game.UpdateGame(); // Oyun g�ncelleme i�lemleri
        }

        SoundPlayer player = new SoundPlayer(@"C:\Users\user\Desktop\spaceWarProject\WinFormsApp1\Resources\intro.wav");

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(playerNameTextBox.Text))
            {
                MessageBox.Show("Lütfen bir oyuncu adı girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            game.playerName = playerNameTextBox.Text;

            gameWindow gameForm = new gameWindow(game);  // Direkt gameWindow tipini kullan
            player.Stop();
            gameForm.Show();
            this.Hide();
        }

        private void PlayMusic()
        {
            player.PlayLooping(); // M�zik d�ng�sel olarak �alacak
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PlayMusic();
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // Sabit boyutlu yapar
            this.SizeGripStyle = SizeGripStyle.Hide; // Boyutland�rma tutama��n� gizler
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            score scoreForm = new score(game.Scoreboard);
            scoreForm.Show();
        }
    }
}
