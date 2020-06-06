using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace TemperateMod.Items.Placeable.MusicBox
{
    class GlacialCavernsMusicBox : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Music Box (Glacial Caverns)");
        }

        public override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTurn = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.autoReuse = true;
            item.consumable = true;
            item.createTile = TileType<Tiles.MusicBox.GlacialCavernsMusicBox>();
            item.width = 24;
            item.height = 24;
            item.rare = ItemRarityID.Orange;
            item.value = 100000;
            item.accessory = true;
        }
    }
}
