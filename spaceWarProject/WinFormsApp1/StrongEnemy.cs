using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceWar
{
    public class StrongEnemy : Enemy
    {
        private Point playerLastPosition;

        public StrongEnemy(Point spawnLocation)
            : base(spawnLocation, health: 80, speed: 4 , damage: 20, points: 300)
        {
            attackInterval = 80; // Yavaş ateş
            playerLastPosition = spawnLocation;
        }
        public override void Attack(Spaceship target)
        {
            attackCooldown++;
            if (attackCooldown >= attackInterval)
            {
                Bullet newBullet = new Bullet(Position, 10, Damage, false); // Orta hızda mermi
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
            // Hızlı patlama efekti ve 300 puan
            Points = 300;
        }
    }
}
