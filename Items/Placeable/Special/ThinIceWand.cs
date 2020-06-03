using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace TemperateMod.Items.Placeable.Special
{
    class ThinIceWand : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Places thin ice");
        }

        public override void SetDefaults()
        {
            item.width = item.height = 20;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.createTile = TileID.BreakableIce;
            item.rare = ItemRarityID.Blue;
            item.value = Item.sellPrice(silver: 25);
        }
    }
}
