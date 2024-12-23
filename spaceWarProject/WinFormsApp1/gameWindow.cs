using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Formats.Asn1.AsnWriter;
using Timer = System.Windows.Forms.Timer;
using System.Media;
using System.Drawing.Text;
namespace spaceWar
{
    public partial class gameWindow : Form
    {
        private SpaceWarGame game;
        private Timer gameTimer = new Timer();
        private Timer animationTimer = new Timer();
        private bool isAnimationComplete = false;
        private bool isGameRunning = false;
        private const int ANIMATION_START_X = 1200;
        private const int TARGET_X = 900;

        private Image playerShipImage;
        private Image basicEnemyImage;
        private Image fastEnemyImage;
        private Image strongEnemyImage;
        private Image bossEnemyImage;
        private SoundPlayer player = new SoundPlayer(@"C:\Users\user\Desktop\spaceWarProject\WinFormsApp1\Resources\gameMusic.wav");
        public gameWindow(SpaceWarGame gameInstance)
        {
            InitializeComponent();
            game = gameInstance;
            LoadImages();
            SetupGame();


        }
        private void PlayMusic()
        {
            player.PlayLooping(); // Müzik döngüsel olarak çalacak
        }
        private void SetupGame()
        {
            PlayMusic();
            // Form ayarları
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.Black;
            this.DoubleBuffered = true;

            // Ekran boyutlarını oyuna bildir
            game.SetDimensions(Screen.PrimaryScreen.Bounds.Width,
                              Screen.PrimaryScreen.Bounds.Height);

            // Timer ayarları
            gameTimer.Interval = 16;  // ~60 FPS
            gameTimer.Tick += GameTimer_Tick;

            // Event handlers
            this.Paint += GameWindow_Paint;
            this.KeyDown += GameWindow_KeyDown;

            // Oyunu başlat
            StartGame();
        }

        private void StartGame()
        {
            isGameRunning = true;
            gameTimer.Start();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (!isGameRunning) return;
            game.UpdateGame();
            this.Invalidate();
        }

        private void GameWindow_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Custom font yükleme
            PrivateFontCollection privateFonts = new PrivateFontCollection();
            privateFonts.AddFontFile(@"Resources\Silkscreen-Regular.ttf");
            Font customFont = new Font(privateFonts.Families[0], 16);

            // Oyuncuyu çiz
            DrawSpaceship(g, game.player.Position);

            // Düşmanları çiz
            foreach (var enemy in game.enemies)
            {
                DrawEnemy(g, enemy);
            }

            // Tüm mermileri çiz
            foreach (var bullet in game.Bullets)
            {
                DrawBullet(g, bullet);
            }

            // Skor ve Health'i custom font ile çiz
            string scoreText = $"SCORE: {game.Score}";
            string healthText = $"HEALTH: {game.player.Health}";

            // Skor sol üstte (custom font ile)
            g.DrawString(scoreText, customFont, Brushes.White, 20, 20);

            // Health sağ üstte (custom font ile)
            SizeF healthSize = g.MeasureString(healthText, customFont);
            float healthX = this.Width - healthSize.Width - 20;
            g.DrawString(healthText, customFont, Brushes.Green, healthX, 20);

            // Oyun bitti mi kontrol et
            if (game.IsGameOver)
            {
                DrawGameOver(g, customFont);
            }

            // Font'u temizle
            customFont.Dispose();
        }

        private void DrawGameOver(Graphics g, Font customFont)
        {
            string gameOverText;
            Brush textColor;

            if (game.IsWinner)
            {
                gameOverText = "TEBRIKLER!\nOYUNU KAZANDINIZ!\n" +
                              $"FINAL SCORE: {game.Score}";
                textColor = Brushes.Gold;
            }
            else
            {
                gameOverText = "GAME OVER\n" +
                              $"FINAL SCORE: {game.Score}";
                textColor = Brushes.Red;
            }

            // Arka plan yarı saydam siyah
            using (SolidBrush backgroundBrush = new SolidBrush(Color.FromArgb(128, 0, 0, 0)))
            {
                g.FillRectangle(backgroundBrush, 0, 0, this.Width, this.Height);
            }

            // Yazıyı ortala
            using (Font gameOverFont = new Font(customFont.FontFamily, 32)) // Daha büyük font
            {
                SizeF size = g.MeasureString(gameOverText, gameOverFont);
                float x = (this.Width - size.Width) / 2;
                float y = (this.Height - size.Height) / 2;

                // Gölge efekti
                g.DrawString(gameOverText, gameOverFont, Brushes.Black, x + 2, y + 2);
                g.DrawString(gameOverText, gameOverFont, textColor, x, y);
            }
        }

        private void DrawSpaceship(Graphics g, Point pos)
        {
            int width = 260;
            int height = 230;
            g.DrawImage(playerShipImage,
                pos.X - width / 2,
                pos.Y - height / 2,
                width,
                height);
        }

        private void DrawEnemy(Graphics g, Enemy enemy)
        {
            Image enemyImage;
            int width, height;

            if (enemy is BossEnemy)
            {
                enemyImage = bossEnemyImage;
                width = 400; height = 400;
            }
            else if (enemy is StrongEnemy)
            {
                enemyImage = strongEnemyImage;
                width = 300; height = 270;
            }
            else if (enemy is FastEnemy)
            {
                enemyImage = fastEnemyImage;
                width = 200; height = 170;
            }
            else // BasicEnemy
            {
                enemyImage = basicEnemyImage;
                width = 150; height = 135;
            }

            g.DrawImage(enemyImage,
                enemy.Position.X - width / 2,
                enemy.Position.Y - height / 2,
                width,
                height);
        }

        private void DrawBullet(Graphics g, Bullet bullet)
        {
            int width = 15;   // Mermi genişliği
            int height = 4;   // Mermi yüksekliği
            Brush bulletColor;

            if (bullet.IsPlayerBullet)
            {
                bulletColor = Brushes.Cyan;    // Oyuncu mermileri cyan
            }
            else if (bullet.Damage >= 50)      // Boss mermilerini hasarına göre tanıyalım
            {
                bulletColor = Brushes.Red;     // Boss mermileri kırmızı
                width = 20;                    // Boss mermileri daha uzun
                height = 6;                    // ve daha kalın
            }
            else
            {
                bulletColor = Brushes.Yellow;  // Normal düşman mermileri sarı
            }

            // Mermiyi çiz (dikdörtgen şeklinde)
            g.FillRectangle(bulletColor,
                bullet.Position.X - width / 2,
                bullet.Position.Y - height / 2,
                width,
                height);
        }

        private void GameWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                return;
            }

            if (!game.IsGameOver)
            {
                game.HandleInput(e.KeyCode);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            // Görselleri temizle
            playerShipImage?.Dispose();
            basicEnemyImage?.Dispose();
            fastEnemyImage?.Dispose();
            strongEnemyImage?.Dispose();
            bossEnemyImage?.Dispose();
        }

        private void LoadImages()
        {
            try
            {
                // Resources klasöründen görselleri yükle
                playerShipImage = Properties.Resources.spaceship;
                basicEnemyImage = Properties.Resources.basic;
                fastEnemyImage = Properties.Resources.fast;
                strongEnemyImage = Properties.Resources.strong;
                bossEnemyImage = Properties.Resources.boss;

                // Görsel yüklenemezse hata fırlat
                if (playerShipImage == null || basicEnemyImage == null ||
                    fastEnemyImage == null || strongEnemyImage == null ||
                    bossEnemyImage == null)
                {
                    throw new Exception("Bir veya birden fazla görsel yüklenemedi!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Görseller yüklenirken hata oluştu:\n{ex.Message}",
                               "Hata",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void gameWindow_Load(object sender, EventArgs e)
        {
            PlayMusic();
        }
    }
}