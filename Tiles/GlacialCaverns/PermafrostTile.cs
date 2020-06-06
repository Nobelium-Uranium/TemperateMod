using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TemperateMod.Tiles.GlacialCaverns
{
    class PermafrostTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            dustType = DustID.Ice;
            drop = ItemType<Items.Placeable.GlacialCaverns.PermafrostBlock>();
            AddMapEntry(new Color(99, 152, 192));
            mineResist = 2.5f;
            minPick = 110;
        }

        public override bool KillSound(int i, int j)
        {
            Main.PlaySound(SoundID.Item50, i * 16, j * 16);
            return false;
        }
    }
}
