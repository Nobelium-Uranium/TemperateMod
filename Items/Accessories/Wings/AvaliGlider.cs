using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace TemperateMod.Items.Accessories.Wings
{
    [AutoloadEquip(EquipType.Wings)]
    class AvaliGlider : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chem's Aero Glider");
            Tooltip.SetDefault("Does NOT allow flight or slow fall\n'Great for impersonating devs!'");
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 20;
            item.rare = ItemRarityID.Cyan;
            item.accessory = true;
            item.vanity = true;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<TemperatePlayer>().VanityWings = true;
        }
        public override bool? PrefixChance(int pre, UnifiedRandom rand)
        {
            return false;
        }
    }
}
