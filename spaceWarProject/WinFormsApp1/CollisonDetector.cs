using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace spaceWar
{
    public class CollisionDetector
    {
        public static bool CheckCollision(Point obj1Position, Size obj1Size, Point obj2Position, Size obj2Size)
        {
            Rectangle rect1 = new Rectangle(obj1Position, obj1Size);
            Rectangle rect2 = new Rectangle(obj2Position, obj2Size);
            return rect1.IntersectsWith(rect2);
        }
        private static Size GetEnemySize(Enemy enemy)
    {
        if (enemy is BossEnemy)
        {
            return new Size(600, 600);
        }
        else if (enemy is StrongEnemy)
        {
            return new Size(400, 400);
        }
        else if (enemy is FastEnemy)
        {
            return new Size(300, 270);
        }
        else // BasicEnemy
        {
            return new Size(300, 270);
        }
    }
        public static void CheckBulletCollisions(List<Bullet> bullets, Spaceship player, List<Enemy> enemies, ref int score)
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                Bullet bullet = bullets[i];

                // Check player bullets against enemies
                if (bullet.IsPlayerBullet)
                {
                    for (int j = enemies.Count - 1; j >= 0; j--)
                    {
                        Enemy enemy = enemies[j];
                        if (CheckCollision(bullet.Position, new Size(5, 10),
                                           enemy.Position, GetEnemySize(enemy)))
                        {
                            enemy.TakeDamage(bullet.Damage);
                            bullet.OnHit();
                            bullets.RemoveAt(i);

                            if (enemy.Health <= 0)
                            {
                                enemy.Destroy();  // Destroy metodunu çağır
                                score += enemy.Points;  // Puanı ekle
                                enemies.RemoveAt(j);
                                break;
                            }
                        }
                    }
                }
                // Check enemy bullets against player
                else
                {
                    if (CheckCollision(bullet.Position, new Size(5, 10),
                                       player.Position, new Size(40, 40)))
                    {
                        player.TakeDamage(bullet.Damage);
                        bullet.OnHit();
                        bullets.RemoveAt(i);
                    }
                }
            }
        }

    }
}