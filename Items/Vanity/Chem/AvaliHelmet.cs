using Terraria.ID;
using Terraria.ModLoader;

namespace TemperateMod.Items.Vanity.Chem
{
    [AutoloadEquip(EquipType.Head)]
    class AvaliHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chem's Avalispec Helmet");
            Tooltip.SetDefault("'Great for impersonating devs!'");
        }
        public override void SetDefaults()
        {
            item.width = 15;
            item.height = 13;
            item.rare = ItemRarityID.Cyan;
            item.vanity = true;
        }
        public override bool DrawHead()
        {
            return false;
        }
    }
}
