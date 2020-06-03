using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TemperateMod.Items.Accessories
{
    public abstract class ExclusiveAccessory : ModItem
    {
        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (slot < 10)
            {
                int index = FindDifferentEquippedExclusiveAccessory().index;
                if (index != -1)
                {
                    return slot == index;
                }
            }
            return base.CanEquipAccessory(player, slot);
        }

        public override bool CanRightClick()
        {
            int maxAccessoryIndex = 5 + Main.LocalPlayer.extraAccessorySlots;
            for (int i = 13; i < 13 + maxAccessoryIndex; i++)
            {
                if (Main.LocalPlayer.armor[i].type == item.type) return false;
            }
            if (FindDifferentEquippedExclusiveAccessory().accessory != null)
            {
                return true;
            }
            return base.CanRightClick();
        }

        public override void RightClick(Player player)
        {
            var (index, accessory) = FindDifferentEquippedExclusiveAccessory();
            if (accessory != null)
            {
                Main.LocalPlayer.QuickSpawnClonedItem(accessory);
                Main.LocalPlayer.armor[index] = item.Clone();
            }
        }

        protected (int index, Item accessory) FindDifferentEquippedExclusiveAccessory()
        {
            int maxAccessoryIndex = 5 + Main.LocalPlayer.extraAccessorySlots;
            for (int i = 3; i < 3 + maxAccessoryIndex; i++)
            {
                Item otherAccessory = Main.LocalPlayer.armor[i];
                if (!otherAccessory.IsAir &&
                    !item.IsTheSameAs(otherAccessory) &&
                    otherAccessory.modItem is ExclusiveAccessory)
                {
                    return (i, otherAccessory);
                }
            }
            return (-1, null);
        }
    }

    class LaserSight : ExclusiveAccessory
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Emits an aim-assist tracer for guns\nStealth effectiveness is greatly reduced while laser sight is active");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.value = Item.sellPrice(gold: 5);
            item.rare = ItemRarityID.Pink;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<TemperatePlayer>().LaserSight = true;
        }

        public override void RightClick(Player player)
        {
            string previousItemName = "";
            Item accessory = FindDifferentEquippedExclusiveAccessory().accessory;
            if (accessory != null)
            {
                previousItemName = accessory.Name;
            }
            base.RightClick(player);
        }
    }

    class LaserScope : ExclusiveAccessory
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Emits an aim-assist tracer and increases view range for guns\nStealth effectiveness is greatly reduced while laser sight is active\n<right> to zoom out\n'I'm not a crazed gunman, I'm an assassin'");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.value = Item.sellPrice(gold: 7, silver: 50);
            item.rare = ItemRarityID.Lime;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<TemperatePlayer>().LaserSight = true;
            player.GetModPlayer<TemperatePlayer>().LaserScope = true;
        }

        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddIngredient(ItemID.RifleScope);
            r.AddIngredient(ItemType<LaserSight>());
            r.AddTile(TileID.TinkerersWorkbench);
            r.SetResult(this);
            r.AddRecipe();
        }

        public override void RightClick(Player player)
        {
            string previousItemName = "";
            Item accessory = FindDifferentEquippedExclusiveAccessory().accessory;
            if (accessory != null)
            {
                previousItemName = accessory.Name;
            }
            base.RightClick(player);
        }
    }
}
