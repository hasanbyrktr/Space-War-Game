// Enemy abstract sınıfı
using spaceWar;

public abstract class Enemy
{
    protected SpaceWarGame game;  // Oyun referansı
    public Point Position { get; protected set; }
    public int Health { get; protected set; }
    public int Speed { get; protected set; }
    public int Damage { get; protected set; }
    public int Points { get; protected set; }
    public bool IsDestroyed { get; protected set; }
    public List<Bullet> Bullets { get; protected set; } = new List<Bullet>();
    protected int attackCooldown = 0;
    protected int attackInterval;

    public Enemy(Point spawnLocation, int health, int speed, int damage, int points)
    {
        Position = spawnLocation;
        Health = health;
        Speed = speed;
        Damage = damage;
        Points = points;
        Bullets = new List<Bullet>();
    }
    public void SetGame(SpaceWarGame gameInstance)
    {
        game = gameInstance;
    }
    public abstract void Move(Point playerPosition); 
    public abstract void Attack(Spaceship target);

    public virtual void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            Destroy();
        }
    }

    public virtual void Destroy()
    {
        IsDestroyed = true;
    }

    protected Point CalculateMovementTowardsPlayer(Point playerPosition)
    {
        double dx = playerPosition.X - Position.X;
        double dy = playerPosition.Y - Position.Y;
        double distance = Math.Sqrt(dx * dx + dy * dy);

        if (distance > 0)
        {
            dx = dx / distance * Speed;
            dy = dy / distance * Speed;
        }

        return new Point(
            Position.X + (int)dx,
            Position.Y + (int)dy
        );
    }
}