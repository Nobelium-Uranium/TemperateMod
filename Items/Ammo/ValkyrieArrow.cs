using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace TemperateMod.Items.Ammo
{
    public class ValkyrieArrow : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Lower critical hit ratio than usual\nReturns ammo on crit and inflicts 3x damage");
        }

        public override void SetDefaults()
        {
            item.damage = 12;
            item.ranged = true;
            item.width = item.height = 8;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 1.5f;
            item.value = Item.sellPrice(copper: 2);
            item.rare = ItemRarityID.Orange;
            item.shoot = ProjectileType<Projectiles.ValkyrieArrow>();
            item.shootSpeed = 10f;
            item.ammo = AmmoID.Arrow;
        }
    }
}
