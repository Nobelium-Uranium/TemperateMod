using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using TemperateMod;
using Terraria.ID;
using TemperateMod.Items.Ammo;

namespace TemperateMod.Projectiles
{
    /// <summary>
    /// ariam wuz here
    /// </summary>
    public class ValkyrieBullet : ModProjectile
    {
        public override void SetDefaults()
        {
            
            projectile.penetrate = 3;
            projectile.damage = 20;
            projectile.arrow = true;
            projectile.aiStyle = -1;
            projectile.knockBack = 5;
            projectile.friendly = true;
            aiType = ProjectileID.Bullet;
            projectile.penetrate = 3;
            projectile.width = 12;
            projectile.height = 12;
            //placeholder values - rebalance as you will
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Valkyrie Bullet");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 1;
            ProjectileID.Sets.TrailingMode[projectile.type] = 1;
        }
        
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if(Main.rand.NextFloat() <= 0.15f)
            {
                    projectile.damage *= 3;
                    Item.NewItem(projectile.position, ItemType<ValkyrieBulletI>());
                    projectile.Kill();
                //bullets don't crit apparently.
            }
        }
    }
}
