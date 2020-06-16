using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using static Terraria.ModLoader.ModContent;

namespace TemperateMod
{
    class TemperatePlayer : ModPlayer
    {
        public int ArcaneCrystals;

        public bool VanityWings;
        public bool BloomWings;

        public bool LaserSight;
        public bool LaserScope;

        public bool RestorationRelic;

        public bool PrinceBlizzard;

        private int TracerTimer;

        private int OldLife;
        private int RelicLifeCounter;
        private int RelicManaCounter;

        public bool ZoneGlacier;

        public override void ResetEffects()
        {
            player.statManaMax2 += ArcaneCrystals * 10;
            VanityWings = false;
            BloomWings = false;
            #region Accessories
            LaserSight = false;
            LaserScope = false;
            RestorationRelic = false;
            #endregion
            #region Buffs
            PrinceBlizzard = false;
            #endregion
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            ModPacket packet = mod.GetPacket();
            packet.Write(ArcaneCrystals);
            packet.Send(toWho, fromWho);
        }

        public override TagCompound Save()
        {
            return new TagCompound
            {
                { "ArcaneCrystals", ArcaneCrystals },
            };
        }

        public override void Load(TagCompound tag)
        {
            ArcaneCrystals = tag.GetInt("ArcaneCrystals");
        }

        public override void UpdateBiomes()
        {
            ZoneGlacier = TemperateWorld.GlacierTiles > 100 && player.ZoneSnow;
        }

        public override bool CustomBiomesMatch(Player other)
        {
            TemperatePlayer modOther = other.GetModPlayer<TemperatePlayer>();
            return ZoneGlacier == modOther.ZoneGlacier;
        }

        public override void CopyCustomBiomesTo(Player other)
        {
            TemperatePlayer modOther = other.GetModPlayer<TemperatePlayer>();
            modOther.ZoneGlacier = ZoneGlacier;
        }

        public override void SendCustomBiomes(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = ZoneGlacier;
            writer.Write(flags);
        }

        public override void ReceiveCustomBiomes(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            ZoneGlacier = flags[0];
        }

        public override Texture2D GetMapBackgroundImage()
        {
            if (ZoneGlacier)
                return mod.GetTexture("GlacialCavernsMapBG");
            return null;
        }

        public override void PreUpdate()
        {
            OldLife = player.statLife;
        }

        public override void PreUpdateBuffs()
        {
            if (ZoneGlacier)
            {
                player.gills = true;
                if (player.wet && !player.lavaWet && !player.honeyWet)
                {
                    player.ClearBuff(BuffID.Chilled);
                    player.ClearBuff(BuffID.Frozen);
                    player.buffImmune[BuffID.Chilled] = true;
                    player.buffImmune[BuffID.Frozen] = true;
                    player.AddBuff(BuffID.Warmth, 2);
                    player.AddBuff(BuffID.Regeneration, 2);
                }
                else
                    player.AddBuff(BuffID.Chilled, 2);
            }
        }

        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            if (BloomWings)
                player.wingTime = 0;
            RelicLifeCounter = 0;
        }

        public override void NaturalLifeRegen(ref float regen)
        {
            if (RestorationRelic && RelicLifeCounter == 600)
                regen *= 50;
        }

        public override void UpdateBadLifeRegen()
        {
            if (player.lifeRegen <= 0)
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
