using Terraria;
using Terraria.ModLoader;

namespace TemperateMod
{
    public class TemperateProjHelpers : GlobalProjectile
    {
        public static float AddGravity(Projectile projectile, float strength)
        {
            return projectile.velocity.Y += strength;
        }
    }
}
