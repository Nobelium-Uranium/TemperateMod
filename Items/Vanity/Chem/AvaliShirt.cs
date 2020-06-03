using Terraria.ID;
using Terraria.ModLoader;

namespace TemperateMod.Items.Vanity.Chem
{
    [AutoloadEquip(EquipType.Body)]
    class AvaliShirt : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chem's Avalispec Breastplate");
            Tooltip.SetDefault("'Great for impersonating devs!'");
        }
        public override void SetDefaults()
        {
            item.width = 15;
            item.height = 13;
            item.rare = ItemRarityID.Cyan;
            item.vanity = true;
        }
        public override bool DrawBody()
        {
            return false;
        }
    }
}
