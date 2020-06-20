using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TemperateMod.Projectiles
{
    class AvaliManipulatorHoldout : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("A.C.D.E.");
        }
        public override void SetDefaults()
        {
            projectile.width = projectile.height = 20;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
        }
        public override bool CanDamage()
        {
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            Vector2 rrp = player.RotatedRelativePoint(player.MountedCenter, true);
            UpdatePlayerVisuals(player, rrp);
            if (projectile.owner == Main.myPlayer)
            {
                UpdateAim(rrp, player.HeldItem.shootSpeed);
                if (!player.channel || player.noBuilding || player.noItems || player.CCed)
                {
                    player.reuseDelay = player.itemTime;
                    player.itemTime = player.itemAnimation = 0;
                    projectile.Kill();
                }
            }
            projectile.timeLeft = 2;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (projectile.direction == -1)
            {
                spriteEffects = SpriteEffects.FlipVertically;
            }
            Texture2D texture = Main.projectileTexture[projectile.type];
            int frameHeight = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int startY = frameHeight * projectile.frame;
            Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);
            Vector2 origin = sourceRectangle.Size() / 2f;
            origin.X = (float)(projectile.spriteDirection == 1 ? sourceRectangle.Width - 20 : 20);

            Color drawColor = projectile.GetAlpha(lightColor);
            Main.spriteBatch.Draw(texture,
                projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY),
                sourceRectangle, drawColor, projectile.rotation, origin, projectile.scale, spriteEffects, 0f);

            return false;
        }
        private void UpdatePlayerVisuals(Player player, Vector2 playerHandPos)
        {
            projectile.Center = playerHandPos;
            projectile.rotation = projectile.velocity.ToRotation();
            //projectile.spriteDirection = projectile.direction;
            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            player.itemRotation = (projectile.velocity * projectile.direction).ToRotation();
        }
        private void UpdateAim(Vector2 source, float speed)
        {
            Vector2 aim = Vector2.Normalize(Main.MouseWorld - source);
            if (aim.HasNaNs())
            {
                aim = -Vector2.UnitY;
            }
            aim *= speed;
            if (aim != projectile.velocity)
            {
                projectile.netUpdate = true;
            }
            projectile.velocity = aim;
        }
    }
}
