using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TemperateMod.Items.Tools.Chem
{
    class AvaliManipulator : ModItem
    {
        private int PickSpeed;
        private int PickPower;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chem's A.C.D.E.");
            Tooltip.SetDefault("'Apparatus for Construction and Destruction of Elements'" +
                "\n'Or simply, in layman's terms, a Matter Manipulator'");
        }
        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 32;
            item.noMelee = true;
            item.noUseGraphic = true;
            PickSpeed = 15;
            item.useTime = item.useAnimation = PickSpeed;
            item.rare = ItemRarityID.Cyan;
            item.autoReuse = true;
            item.channel = true;
            item.shoot = ProjectileType<Projectiles.AvaliManipulatorHoldout>();
            item.shootSpeed = 7.5f;
            item.useStyle = ItemUseStyleID.HoldingOut;
            PickPower = 35;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 Offset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + Offset, 0, 0))
            {
                position += Offset;
            }
            return player.ownedProjectileCounts[ProjectileType<Projectiles.AvaliManipulatorHoldout>()] <= 0;
        }
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = mod.GetTexture("Items/Tools/Chem/AvaliManipulatorIcon");
            spriteBatch.Draw(texture, position, null, Color.White, 0, origin, scale / 2, SpriteEffects.None, 0f);
            return false;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.noBuilding || player.noItems || player.CCed)
                return false;
            float distance = Vector2.Distance(player.Center, Main.MouseWorld);
            if (distance > 320)
            {
                player.channel = false;
                return false;
            }
            int i = (int)(Main.MouseWorld.X / 16);
            int j = (int)(Main.MouseWorld.Y / 16);
            int tileX, tileY;
            for (int y = -1; y <= 1; y++)
            {
                for (int x = -1; x <= 1; x++)
                {
                    tileX = i + x;
                    tileY = j + y;
                    if (!Main.tile[tileX, tileY].active() || !WorldGen.InWorld(tileX, tileY, 0)) continue;
                    player.PickTile(tileX, tileY, PickPower);
                }
            }
            return true;
        }
        public override void UpdateInventory(Player player)
        {
            if (NPC.downedMoonlord)
            {
                PickSpeed = 10;
                PickPower = 225;
            }
            else if (NPC.downedGolemBoss)
            {
                PickSpeed = 15;
                PickPower = 210;
            }
            else if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
            {
                PickSpeed = 15;
                PickPower = 200;
            }
            else if (NPC.downedMechBossAny)
            {
                PickSpeed = 20;
                PickPower = 150;
            }
            else if (Main.hardMode)
            {
                PickSpeed = 20;
                PickPower = 100;
            }
            else if (NPC.downedBoss2)
            {
                PickSpeed = 25;
                PickPower = 65;
            }
            else if (NPC.downedBoss1)
            {
                PickSpeed = 30;
                PickPower = 55;
            }
            else
            {
                PickSpeed = 30;
                PickPower = 35;
            }
            item.useTime = item.useAnimation = PickSpeed;
        }
    }
}
