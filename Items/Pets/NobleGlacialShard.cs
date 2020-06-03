using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TemperateMod.Items.Pets
{
    class NobleGlacialShard : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Summons the forgotten prince of the tundra");
        }

        public override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item2;
            item.width = item.height = 20;
            item.useAnimation = item.useTime = 20;
            item.rare = ItemRarityID.Blue;
            item.noMelee = true;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.shoot = ProjectileType<Projectiles.Pets.PrinceBlizzard>();
            item.buffType = BuffType<Buffs.Vanity.PrinceBlizzardBuff>();
        }

        public override void UseStyle(Player player)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(item.buffType, 3600, true);
            }
        }
    }
}
