using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TemperateMod.Projectiles
{
    class LaserSight : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.aiStyle = 0;
            projectile.extraUpdates = 100;
            projectile.width = projectile.height = 4;
            projectile.ignoreWater = true;
            projectile.hide = true;
            projectile.timeLeft = 600;
            projectile.friendly = true;
        }

        public override bool CanDamage()
        {
            return false;
        }

        public override void AI()
        {
            if (projectile.ai[1] <= 0)
                projectile.ai[1] = 25;
            projectile.ai[1] -= 1;
            if (projectile.ai[1] < 5)
            {
                int Sight = Dust.NewDust(projectile.Center, 0, 0, DustType<Dusts.TracerBeam>(), newColor: Color.Crimson);
                Main.dust[Sight].velocity = projectile.velocity;
                Main.dust[Sight].scale = 1f;
            }
            NPC clearCheck;
            for (int p = 0; p < Main.maxNPCs; p++)
            {
                clearCheck = Main.npc[p];
                if (clearCheck.active && !clearCheck.dontTakeDamage && projectile.Hitbox.Intersects(clearCheck.Hitbox) && !clearCheck.townNPC)
                {
                    projectile.Kill();
                }
            }
            Lighting.AddLight(projectile.position, Color.Crimson.ToVector3() * 0.1f);
        }
    }
}
