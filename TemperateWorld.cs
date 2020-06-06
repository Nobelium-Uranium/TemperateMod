using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using static Terraria.ModLoader.ModContent;

namespace TemperateMod
{
    class TemperateWorld : ModWorld
    {
        public static int GlacierTiles;

        // This code doesn't work properly right now
        /*public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int PostSnow = tasks.FindIndex(genpass => genpass.Name.Equals("Slush Check"));
            if (PostSnow != -1)
            {
                tasks.Insert(PostSnow + 1, new PassLegacy("Glacial Caverns", TemperateGlacialCaverns));
            }
        }

        private void TemperateGlacialCaverns(GenerationProgress progress)
        {
            progress.Message = "Flash Freezing Deep Ice Caverns";
            for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 6E-05); k++)
            {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                int y = WorldGen.genRand.Next((int)WorldGen.rockLayer + 125, Main.maxTilesY - 200);
                Tile tile = Framing.GetTileSafely(x, y);
                if (tile.active() && (tile.type == TileID.IceBlock || tile.type == TileID.CorruptIce || tile.type == TileID.FleshIce))
                {
                    WorldGen.TileRunner(x, y, 50, 2500, TileType<Tiles.GlacialCaverns.PermafrostTile>());
                }
            }
        }*/
        
        public override void ResetNearbyTileEffects()
        {
            GlacierTiles = 0;
        }

        public override void TileCountsAvailable(int[] tileCounts)
        {
            GlacierTiles = tileCounts[TileType<Tiles.GlacialCaverns.PermafrostTile>()];
            Main.snowTiles += tileCounts[TileType<Tiles.GlacialCaverns.PermafrostTile>()];
        }
    }
}
