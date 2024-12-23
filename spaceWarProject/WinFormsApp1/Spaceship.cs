using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using spaceWar;
using System.Media;
using System.Windows.Forms;


public class Spaceship
{
    public int Health { get; private set; }
    public int Damage { get; private set; }
    public int Speed { get; private set; }
    private Point position;
    public Point Position
    {
        get { return position; }
        set { position = value; }  // set accessor'ı ekledik
    }
    public List<Bullet> Bullets { get; private set; }
    private SoundPlayer shootSound;

    public Spaceship(Point startPosition)
    {
        Health = 100;
        Damage = 10;
        Speed = 20;
        Position = startPosition;
        Bullets = new List<Bullet>();
    }

    public void Move(Keys direction)
{
    switch (direction)
    {
        case Keys.Left:
            Position = new Point(Position.X - Speed, Position.Y);
            break;
        case Keys.Right:
            Position = new Point(Position.X + Speed, Position.Y);
            break;
        case Keys.Up:
            Position = new Point(Position.X, Position.Y - Speed);
            break;
        case Keys.Down:
            Position = new Point(Position.X, Position.Y + Speed);
            break;
    }
}

    public void Shoot()
    {
        Bullet newBullet = new Bullet(
            new Point(Position.X, Position.Y),
            speed: 30,
            damage: Damage,
            isPlayerBullet: true
        );
        Bullets.Add(newBullet);
        
       
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            // Game over logic
        }
    }
}