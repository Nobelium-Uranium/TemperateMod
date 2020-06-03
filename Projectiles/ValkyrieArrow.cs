using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using TemperateMod;
using static TemperateMod.TemperateProjHelpers;
using TemperateMod.Items.Ammo;
using Microsoft.Xna.Framework.Graphics;

namespace TemperateMod.Projectiles
{
    public class ValkyrieArrow : ModProjectile
    {
        private bool ReturnedAmmo;

        public override void SetDefaults()
        {
            projectile.aiStyle = 0;
            projectile.arrow = true;
            projectile.ranged = true;
            projectile.width = projectile.height = 10;
            projectile.friendly = true;
            projectile.penetrate = 3;
            projectile.extraUpdates = 1;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Main.rand.NextFloat() > 0.15f)
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
                Main.player[projectile.owner].QuickSpawnItem(ItemType<Items.Ammo.ValkyrieArrow>());
                ReturnedAmmo = true;
            }
        }
    }
}