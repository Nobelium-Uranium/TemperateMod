using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TemperateMod.Projectiles
{
    public class ValkyrieArrow : ModProjectile
    {
        private bool ReturnedAmmo;

        public override void SetDefaults()
        {
            projectile.aiStyle = 1;
            projectile.arrow = true;
            projectile.ranged = true;
            projectile.width = projectile.height = 10;
            projectile.friendly = true;
            projectile.penetrate = 3;
        }

        public override void AI()
        {
            Lighting.AddLight(projectile.position, Color.Cyan.ToVector3() * 0.5f);
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (crit && Main.rand.NextBool())
                crit = false;
            if (crit)
                damage = (int)(damage * 1.5f);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (crit && !ReturnedAmmo)
            {
                Main.player[projectile.owner].QuickSpawnItem(ItemType<Items.Ammo.ValkyrieArrow>());
                ReturnedAmmo = true;
            }
        }

        public override void Kill(int timeLeft)
        {
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.Dig, projectile.position);
        }
    }
}