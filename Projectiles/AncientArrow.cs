using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TemperateMod.Projectiles
{
    class AncientArrow : ModProjectile
    {
        private bool Explode;

        public override void SetDefaults()
        {
            projectile.ranged = true;
            projectile.aiStyle = 1;
            projectile.extraUpdates = 4;
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
            projectile.ai[0] = 0;
            if (!Explode)
            {
                int Explosion = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.AncientLight, newColor: Color.Cyan, Scale: 0.75f);
                Main.dust[Explosion].velocity += -projectile.velocity / 2;
                Main.dust[Explosion].noGravity = true;
            }
            Lighting.AddLight(projectile.position, Color.Cyan.ToVector3() * 0.5f);
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (target.type == NPCID.MoonLordCore || target.type == NPCID.MoonLordHand || target.type == NPCID.MoonLordHead || target.type == NPCID.MoonLordFreeEye || target.type == NPCID.LunarTowerNebula || target.type == NPCID.LunarTowerSolar || target.type == NPCID.LunarTowerStardust || target.type == NPCID.LunarTowerVortex)
                damage *= 2;
            else if (target.boss)
                damage = (int)(damage * 1.5f);
            else if (target.lifeMax < 5000 && !target.boss && target.type != NPCID.TargetDummy && target.type != NPCID.EaterofWorldsHead && target.type != NPCID.EaterofWorldsBody && target.type != NPCID.EaterofWorldsTail && target.type != NPCID.TheDestroyer && target.type != NPCID.TheDestroyerBody && target.type != NPCID.TheDestroyerTail && target.type != NPCID.PrimeCannon && target.type != NPCID.PrimeLaser && target.type != NPCID.PrimeSaw && target.type != NPCID.PrimeVice && target.type != NPCID.GolemFistLeft && target.type != NPCID.GolemFistRight && target.type != NPCID.GolemHead && target.type != NPCID.GolemHeadFree && target.type != NPCID.MartianSaucerCannon && target.type != NPCID.MartianSaucerTurret)
            {
                CombatText.NewText(target.getRect(), Color.Cyan, "Obliterated!", dot: true);
                target.GetGlobalNPC<TemperateNPC>().Obliterated = true;
                target.defense = 0;
                if (damage < target.life)
                    damage += target.life - damage;
            }
            if (projectile.timeLeft > 3)
                projectile.timeLeft = 3;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (!Explode)
            {
                projectile.damage = (int)(projectile.damage * 0.75f);
                projectile.alpha = 255;
                projectile.hide = true;
                projectile.aiStyle = 0;
                projectile.tileCollide = false;
                projectile.velocity = Vector2.Zero;
                projectile.width = projectile.height = 200;
                projectile.Center = projectile.position;
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/AncientLongbow/AncientArrowSpecial"), projectile.position);
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/AncientLongbow/AncientArrowExplosion"), projectile.position);
                for (int i = 0; i < 40; i++)
                {
                    int Explosion = Dust.NewDust(projectile.Center, 0, 0, DustID.AncientLight, newColor: Color.Cyan, Scale: 2f);
                    Main.dust[Explosion].velocity *= 15;
                    Main.dust[Explosion].noGravity = true;
                    Explosion = Dust.NewDust(projectile.Center, 0, 0, DustID.Electric, newColor: Color.Cyan);
                    Main.dust[Explosion].velocity *= 7.5f;
                    Main.dust[Explosion].noGravity = true;
                }
                Explode = true;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.damage /= 2;
            projectile.alpha = 255;
            projectile.hide = true;
            projectile.aiStyle = 0;
            projectile.tileCollide = false;
            projectile.velocity = Vector2.Zero;
            projectile.width = projectile.height = 200;
            projectile.Center = projectile.position;
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/AncientLongbow/AncientArrowExplosion"), projectile.position);
            for (int i = 0; i < 40; i++)
            {
                int Explosion = Dust.NewDust(projectile.Center, 0, 0, DustID.AncientLight, newColor: Color.Cyan, Scale: 2f);
                Main.dust[Explosion].velocity *= 15;
                Main.dust[Explosion].noGravity = true;
                Explosion = Dust.NewDust(projectile.Center, 0, 0, DustID.Electric, newColor: Color.Cyan);
                Main.dust[Explosion].velocity *= 7.5f;
                Main.dust[Explosion].noGravity = true;
            }
            Explode = true;
            if (projectile.timeLeft > 3)
                projectile.timeLeft = 3;
            return false;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * (1f - projectile.alpha / 255f);
        }
    }
}
