using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceWar
{
    public class BossEnemy : Enemy
    {
        private Point playerLastPosition;

        public BossEnemy(Point spawnLocation)
            : base(spawnLocation, health: 500, speed: 3, damage: 50, points: 1000)
        {
            attackInterval = 100;
            playerLastPosition = spawnLocation;
        }
        public override void Attack(Spaceship target)
        {
            attackCooldown++;
            if (attackCooldown >= attackInterval)
            {
                Bullet newBullet = new Bullet(Position, 20, Damage, false);
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
            // Hızlı patlama efekti ve 1000 puan
            Points = 1000;
        }
    }
}
