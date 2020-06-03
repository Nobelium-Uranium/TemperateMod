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
    }
}
