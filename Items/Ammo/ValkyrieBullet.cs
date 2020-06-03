using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace TemperateMod.Items.Ammo
{
    public class ValkyrieBullet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Low fixed critical ratio\nReturns ammo on crit and inflicts 3x damage");
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
            item.shoot = ProjectileType<Projectiles.ValkyrieBullet>();
            item.shootSpeed = 16f;
            item.ammo = AmmoID.Bullet;
        }
    }
}
