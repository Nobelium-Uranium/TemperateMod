﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TemperateMod
{
    class TemperateItem : GlobalItem
    {
        public override void OpenVanillaBag(string context, Player player, int arg)
        {
            #region Dev Sets
            // Self reminder to do a switch when a second developer set is added
            if (context == "bossBag" && Main.rand.NextBool(20))
            {
                player.QuickSpawnItem(ItemType<Items.Vanity.Chem.AvaliHelmet>());
                player.QuickSpawnItem(ItemType<Items.Vanity.Chem.AvaliShirt>());
                player.QuickSpawnItem(ItemType<Items.Vanity.Chem.AvaliPants>());
                if (Main.hardMode)
                {
                    player.QuickSpawnItem(ItemType<Items.Weapons.Chem.RelicBow>());
                    if (arg == ItemID.PlanteraBossBag && Main.rand.NextBool(10))
                        player.QuickSpawnItem(ItemType<Items.Accessories.Wings.BlossomWings>());
                    else
                        player.QuickSpawnItem(ItemType<Items.Accessories.Wings.AvaliGlider>());
                }
                player.QuickSpawnItem(ItemType<Items.Tools.Chem.AvaliManipulator>());
            }
            #endregion

            if (context == "bossBag")
            {
                if (arg == ItemID.TwinsBossBag)
                {
                    if (Main.rand.NextBool(7))
                        player.QuickSpawnItem(ItemType<Items.Accessories.LaserSight>());
                }
                if (arg == ItemID.PlanteraBossBag)
                {
                    if (Main.rand.NextBool(45))
                        player.QuickSpawnItem(ItemType<Items.Accessories.Wings.BloomWings>());
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddIngredient(ItemID.EnchantedNightcrawler);
            r.AddIngredient(ItemID.ShroomiteBar, 5);
            r.AddIngredient(ItemID.SoulofLight, 2);
            r.AddIngredient(ItemID.SoulofNight, 2);
            r.AddIngredient(ItemID.SoulofFlight, 2);
            r.AddTile(TileID.Bottles);
            r.SetResult(ItemID.TruffleWorm);
            r.AddRecipe();

            r = new ModRecipe(mod);
            r.AddIngredient(ItemID.StoneBlock, 50);
            r.AddIngredient(ItemID.LifeCrystal, 3);
            r.AddTile(TileID.HeavyWorkBench);
            r.SetResult(ItemID.HeartStatue);
            r.AddRecipe();

            r = new ModRecipe(mod);
            r.AddIngredient(ItemID.StoneBlock, 50);
            r.AddIngredient(ItemID.ManaCrystal, 3);
            r.AddTile(TileID.HeavyWorkBench);
            r.SetResult(ItemID.StarStatue);
            r.AddRecipe();
        }
    }
}
