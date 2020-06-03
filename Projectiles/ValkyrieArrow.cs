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
    /// <summary>
    /// ariam wuz here
    /// </summary>
    public class ValkyrieArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.penetrate = 3;
            projectile.damage = 20;
            projectile.arrow = true;
            projectile.aiStyle = -1;
            projectile.knockBack = 5;
            projectile.ranged = true;
            projectile.friendly = true;
            projectile.timeLeft = 180;
            projectile.width = 32;
            projectile.height = 14;
      
            
            //placeholder values - rebalance as you will
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Valkyrie Arrow");
        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + 1.57f;
            projectile.velocity.Y = AddGravity(projectile, 0.2f);
            //Dust.NewDustPerfect(projectile.position, DustID.Fire);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if(crit)
            {
                projectile.damage *= 3;
                Item.NewItem(projectile.position, ItemType<ValkyrieArrowI>());
                projectile.Kill();
            }
        }
        public override void Kill(int timeLeft)
        {
            Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Fire);
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            /**Texture2D texture = mod.GetTexture("Projectiles/ValkyrieArrow_Glowmask");
            Vector2 drawPos = projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY);
            Rectangle frame = new Rectangle(0, 0, texture.Width, texture.Height);
            spriteBatch.Draw(texture, drawPos, frame, Color.White * 0.7f, projectile.rotation, frame.Size()/2, 1f, SpriteEffects.None, 0f);
            **/
            //dunno what to do with this; does weird doppler effect. 
        }
    }
}
/**
 * Ammunition made from the ore found in the Glacial Caverns,
    they can hit up to three enemies and critical hits do 3x damage instead of 2x, but they have a lower critical rate than usual.
    In addition, you will regain a bullet/arrow on crit, this will only happen once.
**/