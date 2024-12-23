using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceWar
{
    public class FastEnemy : Enemy
    {
        private Point playerLastPosition;

        public FastEnemy(Point spawnLocation)
            : base(spawnLocation, health: 20, speed: 8, damage: 5, points: 75)
        {
            attackInterval = 40; // Orta hızda ateş
            playerLastPosition = spawnLocation;
        }
        public override void Attack(Spaceship target)
        {
            attackCooldown++;
            if (attackCooldown >= attackInterval)
            {
                Bullet newBullet = new Bullet(Position, 15, Damage, false);
                game?.AddBullet(newBullet);
                attackCooldown = 0;
            }
        }
        public override void Move(Point playerPosition)
        {
            // Hızlı düşman sürekli oyuncuyu takip eder
            playerLastPosition = playerPosition;
            Position = CalculateMovementTowardsPlayer(playerLastPosition);
        }
        public override void Destroy()
        {
            base.Destroy();
            // Hızlı patlama efekti ve 75 puan
            Points = 75;
        }
    }

}

