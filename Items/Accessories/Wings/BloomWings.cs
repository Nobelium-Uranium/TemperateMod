﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace TemperateMod.Items.Accessories.Wings
{
    [AutoloadEquip(EquipType.Wings)]
    class BloomWings : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Allows for very long lasting flight\nJust don't get hit\n'Graceful, yet frail'");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 20;
            item.rare = ItemRarityID.Lime;
            item.value = Item.sellPrice(gold: 8);
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.wingTimeMax = 3600;
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.75f;
            ascentWhenRising = 0.15f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 2.5f;
            constantAscend = 0.125f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 7f;
            acceleration *= 1.5f;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<TemperatePlayer>().BloomWings = true;
        }
    }
}
