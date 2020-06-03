using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using TemperateMod;
using TemperateMod.Projectiles;

namespace TemperateMod.Items.Ammo
{
    public class ValkyrieBulletI : ModItem
    {
        public override void SetDefaults()
        {
            item.consumable = true;
            item.height = 12;
            item.width = 12;
            item.ranged = true;
            item.maxStack = 999;
            item.ammo = AmmoID.Bullet;
            item.shoot = ProjectileType<ValkyrieBullet>();
            item.shootSpeed = 10;
            item.crit = 15;
        }
        public override void OnConsumeAmmo(Player player)
        {
            //
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Valkyrie Bullet");
        }
    }
}
