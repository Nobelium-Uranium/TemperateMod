using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;

namespace TemperateMod.Items.Consumables.Special
{
    class ArcaneCrystal : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Permanently increases maximum mana by 10");
        }
        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 24;
            item.maxStack = 99;
            item.useAnimation = 30;
            item.useTime = 30;
            item.value = Item.sellPrice(silver: 5);
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.UseSound = SoundID.Item29;
            item.rare = ItemRarityID.Lime;
            item.consumable = true;
            base.SetDefaults();
        }
        public override bool CanUseItem(Player player)
        {
            return player.statManaMax == 200 && player.GetModPlayer<TemperatePlayer>().ArcaneCrystals < 10;
        }
        public override bool UseItem(Player player)
        {
            if (player.itemAnimation > 0 && player.statManaMax >= 200 && player.statManaMax < 300 && player.itemTime == 0)
            {
                player.itemTime = PlayerHooks.TotalUseTime(item.useTime, player, item);
                player.statManaMax2 += 10;
                player.GetModPlayer<TemperatePlayer>().ArcaneCrystals += 1;
                player.statMana += 10;
                if (Main.myPlayer == player.whoAmI)
                    player.ManaEffect(10);
                AchievementsHelper.HandleSpecialEvent(player, AchievementHelperID.Special.ConsumeStar);
            }
            return false;
        }
    }
}
