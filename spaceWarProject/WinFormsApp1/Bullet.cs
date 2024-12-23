
namespace spaceWar
{
    public class Bullet
    {
        public int Speed { get; private set; }
        public int Damage { get; private set; }
        public bool IsPlayerBullet { get; private set; }
        public Point Position { get; private set; }

        public Bullet(Point position, int speed, int damage, bool isPlayerBullet)
        {
            Position = position;
            Speed = speed;
            Damage = damage;
            IsPlayerBullet = isPlayerBullet;
        }

        public void Move()
        {
            int xOffset = IsPlayerBullet ? -Speed : Speed;  // Oyuncu sağda olduğu için mermiler sola gider
            Position = new Point(Position.X + xOffset, Position.Y);
        }

        public void OnHit()
        {
            // Bullet is destroyed after hitting a target
        }
    }

}
