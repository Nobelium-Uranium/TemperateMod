using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;

namespace TemperateMod.Projectiles
{
    public class ValkyrieBullet : ModProjectile
    {
        private bool ReturnedAmmo;

        public override void SetDefaults()
        {
            projectile.ranged = true;
            projectile.width = projectile.height = 4;
            projectile.friendly = true;
            projectile.penetrate = 3;
        }

        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Main.rand.NextFloat() > 0.05f)
                crit = false;
            else
                crit = true;
            if (crit)
                damage = (int)(damage * 1.5f);
            Lighting.AddLight(projectile.position, Color.Cyan.ToVector3() * 0.5f);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (crit && !ReturnedAmmo)
            {
                Main.player[projectile.owner].QuickSpawnItem(ItemType<Items.Ammo.ValkyrieBullet>());
                ReturnedAmmo = true;
            }
        }
    }
}
