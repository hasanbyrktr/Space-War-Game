using spaceWar;
using System.Collections.Generic;

public class SpaceWarGame
{
    public int PanelWidth { get; private set; }
    public int PanelHeight { get; private set; }

    public Spaceship player;
    public List<Enemy> enemies;
    public List<Bullet> allBullets;
    public List<Bullet> Bullets => allBullets;
    private int currentScore;
    private int currentLevel;
    private bool isGameOver;
    public string playerName { get; set; }
    public int Score { get { return currentScore; } }
    public bool IsGameOver 
    { 
        get { return isGameOver; } 
        private set { isGameOver = value; }  // private set ekleyelim
    }
    public bool IsWinner { get; private set; }

    private int enemySpawnDelay = 3000; // Başlangıçta 3 saniye bekleme
    private DateTime lastSpawnTime;
    private Queue<EnemyType> enemySpawnQueue;
    private int maxCycles = 3;
    private int currentCycle = 1;

    private enum EnemyType
    {
        Basic,
        Fast,
        Strong,
        Boss
    }

    private Scoreboard scoreboard;

    public SpaceWarGame()
    {
        scoreboard = new Scoreboard("highscores.txt");
        // Başlangıçta varsayılan değerler
        PanelWidth = 1920;  // Tipik Full HD genişlik
        PanelHeight = 1080; // Tipik Full HD yükseklik
        StartGame();
    }

    public Scoreboard Scoreboard
    {
        get { return scoreboard; }
    }

    public void SetDimensions(int width, int height)
    {
        PanelWidth = width;
        PanelHeight = height;
        UpdatePositions();
    }

    private void UpdatePositions()
    {
        if (player != null)
        {
            // Oyuncuyu sağ tarafta, dikey olarak ortala
            player.Position = new Point(
                PanelWidth - 150,
                PanelHeight / 2 - 30
            );
        }
    }

    public void StartGame()
    {
        currentScore = 0;
        currentLevel = 1;
        currentCycle = 1;
        lastSpawnTime = DateTime.Now;
        
        player = new Spaceship(new Point(
            PanelWidth - 150,
            PanelHeight / 2 - 30
        ));

        enemies = new List<Enemy>();
        allBullets = new List<Bullet>();
        isGameOver = false;
        enemySpawnQueue = new Queue<EnemyType>();
        InitializeSpawnQueue();
    }

    private void InitializeSpawnQueue()
    {
        enemySpawnQueue.Clear();
        
        // 3 Basic düşman ekle
        for (int i = 0; i < 3; i++)
            enemySpawnQueue.Enqueue(EnemyType.Basic);
        
        // 2 Fast düşman ekle
        for (int i = 0; i < 2; i++)
            enemySpawnQueue.Enqueue(EnemyType.Fast);
        
        // 2 Strong düşman ekle
        for (int i = 0; i < 2; i++)
            enemySpawnQueue.Enqueue(EnemyType.Strong);
        
        // 1 Boss düşman ekle
        enemySpawnQueue.Enqueue(EnemyType.Boss);
    }

    public void UpdateGame()
    {
        if (isGameOver) return;

        // Düşmanları güncelle
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            enemies[i].Move(player.Position);
            enemies[i].Attack(player);

            if (enemies[i].Position.X > PanelWidth)
            {
                enemies.RemoveAt(i);
                continue;
            }
        }

        // Mermileri güncelle
        for (int i = allBullets.Count - 1; i >= 0; i--)
        {
            allBullets[i].Move();

            if (allBullets[i].Position.X < 0 || 
                allBullets[i].Position.X > PanelWidth ||
                allBullets[i].Position.Y < 0 || 
                allBullets[i].Position.Y > PanelHeight)
            {
                allBullets.RemoveAt(i);
            }
        }

        CheckCollisions();

        // Yeni düşman spawn kontrolü
        if ((DateTime.Now - lastSpawnTime).TotalMilliseconds >= enemySpawnDelay)
        {
            SpawnNextEnemy();
            lastSpawnTime = DateTime.Now;
        }
    }

    private void SpawnNextEnemy()
    {
        if (enemySpawnQueue.Count == 0)
        {
            if (currentCycle >= maxCycles)
            {
                WinGame();
                return;
            }
            InitializeSpawnQueue();
            currentCycle++;
        }

        Random rand = new Random();
        Point spawnLocation = new Point(
            -150,
            rand.Next(50, PanelHeight - 100)
        );

        Enemy newEnemy = null; // Başlangıçta null olarak tanımla

        EnemyType nextEnemyType = enemySpawnQueue.Dequeue();

        switch (nextEnemyType)
        {
            case EnemyType.Basic:
                newEnemy = new BasicEnemy(spawnLocation);
                enemySpawnDelay = 3000; // Basic düşmanlar arası 3 saniye
                break;
            case EnemyType.Fast:
                newEnemy = new FastEnemy(spawnLocation);
                enemySpawnDelay = 4000; // Fast düşmanlar arası 4 saniye
                break;
            case EnemyType.Strong:
                newEnemy = new StrongEnemy(spawnLocation);
                enemySpawnDelay = 5000; // Strong düşmanlar arası 5 saniye
                break;
            case EnemyType.Boss:
                newEnemy = new BossEnemy(spawnLocation);
                enemySpawnDelay = 6000; // Boss'tan sonra daha uzun bekleme
                break;
            default:
                throw new InvalidOperationException("Bilinmeyen düşman türü");
        }

        if (newEnemy != null)
        {
            newEnemy.SetGame(this);
            enemies.Add(newEnemy);
        }
    }

    public void HandleInput(Keys key)
    {
        if (isGameOver) return;

        switch (key)
        {
            case Keys.Up:
                if (player.Position.Y > 0)
                    player.Move(key);
                break;
            case Keys.Down:
                if (player.Position.Y < PanelHeight - 100)
                    player.Move(key);
                break;
            case Keys.Left:
                if (player.Position.X > PanelWidth / 2)
                    player.Move(key);
                break;
            case Keys.Right:
                if (player.Position.X < PanelWidth - 150)
                    player.Move(key);
                break;
            case Keys.Space:
                player.Shoot();
                allBullets.AddRange(player.Bullets);
                player.Bullets.Clear();
                break;
        }
    }

    private void CheckCollisions()
    {
        // Oyuncu-Düşman çarpışmaları
        foreach (var enemy in enemies.ToList())
        {
            if (CollisionDetector.CheckCollision(
                player.Position, new Size(40, 40),
                enemy.Position, new Size(30, 30)))
            {
                player.TakeDamage(enemy.Damage);
                enemy.TakeDamage(player.Damage);

                if (player.Health <= 0)
                {
                    EndGame();
                    return;
                }

                if (enemy.Health <= 0)
                {
                    currentScore += enemy.Points;
                    enemies.Remove(enemy);
                }
            }
        }

        // Mermi çarpışmaları
        CollisionDetector.CheckBulletCollisions(allBullets, player, enemies, ref currentScore);

        // Oyuncu sağlığı kontrol
        if (player.Health <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        IsGameOver = true;
        IsWinner = false;
        SaveHighScore();
    }

    public void WinGame()
    {
        IsGameOver = true;
        IsWinner = true;
        // Skoru kaydet
        if (scoreboard != null)
        {
            scoreboard.AddScore(playerName, Score);
        }
    }

    public void GameOver()
    {
        IsGameOver = true;
        IsWinner = false;
        // Skoru kaydet
        if (scoreboard != null)
        {
            scoreboard.AddScore(playerName, Score);
        }
    }

    public void AddBullet(Bullet bullet)
    {
        allBullets.Add(bullet);
    }

    private void SaveHighScore()
    {
        scoreboard.AddScore(playerName, currentScore);
    }

    // Skor artırma metodu
    public void AddScore(int points)
    {
        if (points > 0) // Negatif skor eklemeyi engelle
        {
            currentScore += points;
        }
    }
}