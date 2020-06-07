using TemperateMod.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TemperateMod.Items.Weapons.Chem
{
    class RelicBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault(
                "Converts arrows into relic arrows that explode" +
                "\nRelic arrows will also obliterate very weak non-boss enemies" +
                "\n'A fine bow crafted from celestial technology'");
        }

        public override void SetDefaults()
        {
            item.ranged = true;
            item.noMelee = true;
            item.width = 20;
            item.height = 30;
            item.damage = 300;
            item.knockBack = 4f;
            item.useTime = 35;
            item.useAnimation = 35;
            item.UseSound = SoundID.Item102;
            item.channel = true;
            item.shoot = ProjectileType<RelicArrow>();
            item.shootSpeed = 4f;
            item.useAmmo = AmmoID.Arrow;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.value = Item.sellPrice(gold: 5);
            item.rare = ItemRarityID.Cyan;
            item.autoReuse = true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            type = ProjectileType<RelicArrow>();
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
    }
}
