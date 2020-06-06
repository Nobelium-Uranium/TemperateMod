using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TemperateMod.Items.Placeable.GlacialCaverns
{
    class PermafrostBlock : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Resistant to infection\n'Just don't put your tongue on it'");
        }

        public override void SetDefaults()
        {
            item.width = 12;
            item.height = 12;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.rare = ItemRarityID.Blue;
            item.consumable = true;
            item.createTile = TileType<Tiles.GlacialCaverns.PermafrostTile>();
        }
    }
}
