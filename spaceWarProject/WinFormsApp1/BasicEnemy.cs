using spaceWar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BasicEnemy : Enemy
{
    private Point playerLastPosition;

    public BasicEnemy(Point spawnLocation)
        : base(spawnLocation, health: 40, speed: 5 , damage: 10, points: 25)
    {
        attackInterval = 60; // Daha yavaş ateş
        playerLastPosition = spawnLocation;
    }
    public override void Attack(Spaceship target)
    {
        attackCooldown++;
        if (attackCooldown >= attackInterval)
        {
            Bullet newBullet = new Bullet(Position, 8, Damage, false);
            game?.AddBullet(newBullet);
            attackCooldown = 0;
        }
    }
    public override void Move(Point playerPosition)
    {

        playerLastPosition = playerPosition;
        Position = CalculateMovementTowardsPlayer(playerLastPosition);
    }
    public override void Destroy()
    {
        base.Destroy();
        // Hızlı patlama efekti ve 75 puan
        Points = 25;
    }
}

