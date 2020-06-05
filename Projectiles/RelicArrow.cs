using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TemperateMod.Projectiles
{
    class RelicArrow : ModProjectile
    {
        private bool Explode;

        public override void SetDefaults()
        {
            projectile.ranged = true;
            projectile.aiStyle = 1;
            projectile.extraUpdates = 2;
            projectile.width = projectile.height = 10;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.timeLeft = 600;
            projectile.penetrate = -1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
        }

        public override void AI()
        {
            if (projectile.localAI[0] < 60)
            {
                projectile.ai[0] = 0;
                projectile.localAI[0] += 1;
            }
            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            if (!Explode)
            {
                int Explosion = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.AncientLight, newColor: Color.Cyan, Scale: 0.75f);
                Main.dust[Explosion].velocity += -projectile.velocity / 2;
                Main.dust[Explosion].noGravity = true;
            }
            Lighting.AddLight(projectile.position, Color.Cyan.ToVector3() * 0.25f);
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (target.lifeMax < 500 && !target.boss && target.type != NPCID.TargetDummy && target.type != NPCID.EaterofWorldsHead && target.type != NPCID.EaterofWorldsBody && target.type != NPCID.EaterofWorldsTail)
            {
                CombatText.NewText(target.getRect(), Color.Cyan, "Obliterated!", dot: true);
                target.GetGlobalNPC<TemperateNPC>().Obliterated = true;
                target.defense = 0;
                if (damage < target.life)
                    damage += target.life - damage;
            }
            if (projectile.timeLeft > 3)
                projectile.timeLeft = 3;
            if (damage >= target.life)
                hitDirection = 0;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (!Explode)
            {
                projectile.damage /= 2;
                projectile.alpha = 255;
                projectile.hide = true;
                projectile.aiStyle = 0;
                projectile.tileCollide = false;
                projectile.velocity = Vector2.Zero;
                projectile.width = projectile.height = 75;
                projectile.Center = projectile.position;
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/AncientLongbow/RelicArrowSpecial"), projectile.position);
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/AncientLongbow/RelicArrowExplosion"), projectile.position);
                for (int i = 0; i < 10; i++)
                {
                    int Explosion = Dust.NewDust(projectile.Center, 0, 0, DustID.AncientLight, newColor: Color.Cyan, Scale: 2f);
                    Main.dust[Explosion].velocity *= 5.625f;
                    Main.dust[Explosion].noGravity = true;
                    Explosion = Dust.NewDust(projectile.Center, 0, 0, DustID.Electric, newColor: Color.Cyan);
                    Main.dust[Explosion].velocity *= 2.8125f;
                    Main.dust[Explosion].noGravity = true;
                }
                Explode = true;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.damage /= 3;
            projectile.alpha = 255;
            projectile.hide = true;
            projectile.aiStyle = 0;
            projectile.tileCollide = false;
            projectile.velocity = Vector2.Zero;
            projectile.width = projectile.height = 75;
            projectile.Center = projectile.position;
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/AncientLongbow/RelicArrowExplosion"), projectile.position);
            for (int i = 0; i < 10; i++)
            {
                int Explosion = Dust.NewDust(projectile.Center, 0, 0, DustID.AncientLight, newColor: Color.Cyan, Scale: 2f);
                Main.dust[Explosion].velocity *= 5.625f;
                Main.dust[Explosion].noGravity = true;
                Explosion = Dust.NewDust(projectile.Center, 0, 0, DustID.Electric, newColor: Color.Cyan);
                Main.dust[Explosion].velocity *= 2.8125f;
                Main.dust[Explosion].noGravity = true;
            }
            Explode = true;
            if (projectile.timeLeft > 3)
                projectile.timeLeft = 3;
            return false;
        }
    }
}
