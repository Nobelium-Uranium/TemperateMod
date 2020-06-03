using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TemperateMod.Items.Accessories
{
    class RestorationRelic : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Relic of Restoration");
            Tooltip.SetDefault("Increases maximum mana by 20" +
                "\nIncreased life and mana regeneration" +
                "\nReduces the cooldown of healing potions by 25%" +
                "\nMassively increased life regeneration after 10 seconds of not taking damage" +
                "\nGrants slow but constant mana regeneration");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 22;
            item.accessory = true;
            item.value = Item.sellPrice(gold: 5);
            item.rare = ItemRarityID.LightPurple;
            item.lifeRegen = 1;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<TemperatePlayer>().RestorationRelic = true;
        }

        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddIngredient(ItemID.CharmofMyths);
            r.AddIngredient(ItemID.ManaRegenerationBand);
            r.AddTile(TileID.TinkerersWorkbench);
            r.SetResult(this);
            r.AddRecipe();
        }
    }
}
