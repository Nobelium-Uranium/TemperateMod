using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TemperateMod
{
    class TemperateItem : GlobalItem
    {
        public override void OpenVanillaBag(string context, Player player, int arg)
        {
            if (context == "bossBag" && arg == ItemID.TwinsBossBag)
            {
                if (Main.rand.NextBool(7))
                    player.QuickSpawnItem(ItemType<Items.Accessories.LaserSight>());
               
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
        }
    }
}
