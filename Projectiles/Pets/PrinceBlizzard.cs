using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TemperateMod.Projectiles.Pets
{
    class PrinceBlizzard : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Prince Blizzard");
            Main.projFrames[projectile.type] = 1;
            Main.projPet[projectile.type] = true;
        }

        public override void SetDefaults()
        {
            projectile.netImportant = true;
            projectile.width = projectile.height = 42;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
        }

        public override void AI()
        {
            UpdatePet();
            Player player = Main.player[projectile.owner];
            float num = 4f;
            float num6 = 0.08f;
            float scaleFactor = 0.1f;
            Vector2 value = new Vector2(0, -64f);
            Vector2 vector = player.MountedCenter + value;
            float num8 = Vector2.Distance(projectile.Center, vector);
            if (num8 > 1000f)
            {
                projectile.Center = player.Center + value;
            }
            Vector2 vector2 = vector - projectile.Center;
            if (num8 < num)
            {
                projectile.velocity *= 0.25f;
            }
            if (vector2 != Vector2.Zero)
            {
                if (vector2.Length() < num * 0.5f)
                {
                    projectile.velocity = vector2;
                }
                else
                {
                    projectile.velocity = vector2 * scaleFactor;
                }
            }
            if (projectile.velocity.Length() > 6f)
            {
                float num9 = projectile.velocity.X * num6 + projectile.velocity.Y * (float)projectile.spriteDirection * 0.02f;
                if (Math.Abs(projectile.rotation - num9) >= 3.14159274f)
                {
                    if (num9 < projectile.rotation)
                    {
                        projectile.rotation -= 6.28318548f;
                    }
                    else
                    {
                        projectile.rotation += 6.28318548f;
                    }
                }
                float num10 = 12f;
                projectile.rotation = (projectile.rotation * (num10 - 1f) + num9) / num10;
            }
            else
            {
                if (projectile.rotation > 3.14159274f)
                {
                    projectile.rotation -= 6.28318548f;
                }
                if (projectile.rotation > -0.005f && projectile.rotation < 0.005f)
                {
                    projectile.rotation = 0f;
                }
                else
                {
                    projectile.rotation *= 0.96f;
                }
            }
            Lighting.AddLight(projectile.Center, Color.Cyan.ToVector3() * 0.5f);
        }

        private void UpdatePet()
        {
            Player player = Main.player[projectile.owner];
            TemperatePlayer modPlayer = player.GetModPlayer<TemperatePlayer>();
            if (player.dead)
            {
                modPlayer.PrinceBlizzard = false;
                projectile.Kill();
            }
            if (modPlayer.PrinceBlizzard)
            {
                projectile.timeLeft = 2;
            }
            projectile.tileCollide = false;
        }
    }
}
