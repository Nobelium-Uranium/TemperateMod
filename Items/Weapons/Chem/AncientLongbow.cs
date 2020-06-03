using TemperateMod.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TemperateMod.Items.Weapons.Chem
{
    class AncientLongbow : ModItem
    {
        private bool ArrowLoaded;
        private bool Using;
        private bool FullyCharged;
        private int ChargeAmount;
        private Vector2 RefPosition;
        private Vector2 RefVelocity;
        private Vector2 MuzOffset;
        private int RefDamage;
        private float RefKnockback;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault(
                "Hold attack to charge, can only be fired when fully charged" +
                "\nConverts arrows into ancient arrows that fly straight and explode" +
                "\nAncient arrows will also obliterate weak non-boss enemies, destroying their loot" +
                "\nIncreased damage against strong foes, especially against celestial beings" +
                "\n'You are the light, our light, that must shine upon this world again...'");
        }

        public override void SetDefaults()
        {
            item.ranged = true;
            item.noMelee = true;
            item.width = 60;
            item.height = 30;
            item.damage = 3000;
            item.crit = 11;
            item.useTime = 1;
            item.useAnimation = 30;
            item.channel = true;
            item.shoot = ProjectileType<AncientArrow>();
            item.shootSpeed = 10f;
            item.useAmmo = AmmoID.Arrow;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 0;
            item.value = Item.sellPrice(gold: 75);
            item.rare = ItemRarityID.Cyan;
            item.autoReuse = true;
        }

        public override bool ConsumeAmmo(Player player)
        {
            return FullyCharged && !ArrowLoaded;
        }

        public override void OnConsumeAmmo(Player player)
        {
            ArrowLoaded = true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            RefPosition = position; //Need to store these as they will be important later
            RefVelocity = new Vector2(speedX, speedY);
            MuzOffset = RefPosition + Vector2.Normalize(RefVelocity) * 35f;
            RefDamage = damage;
            RefKnockback = knockBack;
            return false; //Returning false because shooting the projectile is being done completely elsewhere
        }

        public override void HoldItem(Player player)
        {
            if (player.channel)
            {
                Using = true;
                if (ChargeAmount == 5)
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/AncientLongbow/AncientBowCharge"), player.position);
                if (ChargeAmount >= 5)
                {
                    Vector2 perturbedSpeed = RefVelocity.RotatedByRandom(MathHelper.ToRadians(10)) * 1.5f;
                    float scale = 1f - (Main.rand.NextFloat() * 0.3f);
                    perturbedSpeed = perturbedSpeed * scale;
                    int Explosion = Dust.NewDust(MuzOffset, 0, 0, DustID.AncientLight, newColor: Color.Cyan);
                    Main.dust[Explosion].velocity = -perturbedSpeed * ChargeAmount / 60 + player.velocity;
                    Main.dust[Explosion].noGravity = true;
                }
                if (ChargeAmount == 60)
                {
                    if (!FullyCharged)
                    {
                        Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/AncientLongbow/AncientBowReady"), player.position);
                        for (int i = 0; i < 10; i++)
                        {
                            int Explosion = Dust.NewDust(MuzOffset, 0, 0, DustID.AncientLight, newColor: Color.Cyan);
                            Main.dust[Explosion].velocity *= 5;
                            Main.dust[Explosion].noGravity = true;
                        }
                        FullyCharged = true;
                    }
                    if (Main.rand.NextBool())
                    {
                        int Explosion = Dust.NewDust(MuzOffset, 0, 0, DustID.AncientLight, newColor: Color.Cyan);
                        Main.dust[Explosion].velocity *= 3;
                        Main.dust[Explosion].noGravity = true;
                    }
                }
                else
                    ChargeAmount += 1;
            }
            else
            {
                if (Using)
                {
                    player.itemTime = item.useAnimation;
                    player.itemAnimation = item.useAnimation;
                    if (FullyCharged)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            Vector2 perturbedSpeed = RefVelocity.RotatedByRandom(MathHelper.ToRadians(10)) * 2.5f;
                            float scale = 1f - (Main.rand.NextFloat() * 0.3f);
                            perturbedSpeed = perturbedSpeed * scale;
                            int Explosion = Dust.NewDust(RefPosition, 0, 0, DustID.AncientLight, newColor: Color.Cyan, Scale: 1.5f);
                            Main.dust[Explosion].velocity += perturbedSpeed;
                            Main.dust[Explosion].noGravity = true;
                        }
                        Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/AncientLongbow/AncientBowShot").WithVolume(0.75f), player.position);
                        Projectile.NewProjectile(RefPosition, RefVelocity, ProjectileType<AncientArrow>(), RefDamage, RefKnockback, player.whoAmI);
                    }
                    Using = false;
                }
                ChargeAmount = 0;
                FullyCharged = false;
                ArrowLoaded = false;
            }
        }

        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddIngredient(ItemID.PulseBow);
            r.AddIngredient(ItemID.Phantasm);
            r.AddIngredient(ItemID.LunarBar, 100);
            r.AddTile(TileID.LunarCraftingStation);
            r.SetResult(this);
            r.AddRecipe();
        }
    }
}
