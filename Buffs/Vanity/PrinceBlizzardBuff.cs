using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TemperateMod.Buffs.Vanity
{
    class PrinceBlizzardBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Prince Blizzard");
            Description.SetDefault("His judgement may be cold but his heart is not");
            Main.buffNoTimeDisplay[Type] = true;
            Main.lightPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 18000;
            player.GetModPlayer<TemperatePlayer>().PrinceBlizzard = true;
            bool petProjectileNotSpawned = player.ownedProjectileCounts[ProjectileType<Projectiles.Pets.PrinceBlizzard>()] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ProjectileType<Projectiles.Pets.PrinceBlizzard>(), 0, 0f, player.whoAmI, 0f, 0f);
            }
        }
    }
}
