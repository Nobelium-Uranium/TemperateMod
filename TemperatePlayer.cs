using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TemperateMod
{
    class TemperatePlayer : ModPlayer
    {
        public bool VanityWings;

        public bool LaserSight;
        public bool LaserScope;

        public bool RestorationRelic;

        public bool PrinceBlizzard;

        private int TracerTimer;

        private int OldLife;
        private int RelicLifeCounter;
        private int RelicManaCounter;

        public override void ResetEffects()
        {
            VanityWings = false;
            #region Accessories
            LaserSight = false;
            LaserScope = false;
            RestorationRelic = false;
            #endregion
            #region Buffs
            PrinceBlizzard = false;
            #endregion
        }

        public override void PreUpdate()
        {
            OldLife = player.statLife;
        }

        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            RelicLifeCounter = 0;
        }

        public override void UpdateEquips(ref bool wallSpeedBuff, ref bool tileSpeedBuff, ref bool tileRangeBuff)
        {
            if (VanityWings)
                player.wingsLogic = 0;
            if (LaserSight && (player.HeldItem.useAmmo == AmmoID.Bullet || player.HeldItem.type == ItemID.StakeLauncher))
            {
                if (TracerTimer == 25)
                    TracerTimer = 0;
                TracerTimer += 1;
                Projectile.NewProjectile(player.MountedCenter + player.velocity, Vector2.Normalize(Main.MouseWorld - player.MountedCenter) * player.HeldItem.shootSpeed, ProjectileType<Projectiles.LaserSight>(), 0, 0, player.whoAmI, ai1: TracerTimer);
                if (LaserScope)
                    player.scope = true;
                if (player.aggro < 0)
                    player.aggro /= 4;
            }
            else
                TracerTimer = 0;
            if (RestorationRelic)
            {
                player.statManaMax2 += 20;
                player.manaRegenDelayBonus++;
                player.manaRegenBonus += 25;
                player.pStone = true;
                if (player.statLife >= OldLife && !player.dead)
                {
                    if (RelicLifeCounter < 600)
                        RelicLifeCounter += 1;
                }
                else
                    RelicLifeCounter = 0;
                if (RelicLifeCounter == 600)
                    player.statLife += 1;
                if (RelicManaCounter == 2)
                {
                    player.statMana += 1;
                    RelicManaCounter = 0;
                }
                RelicManaCounter += 1;
            }
            else
            {
                RelicLifeCounter = 0;
                RelicManaCounter = 0;
            }
            //god just use a switch AAAAAAAAAAAAAAAAA
        }
    }
}
