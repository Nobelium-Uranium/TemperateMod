using System;
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
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int PostSnow = tasks.FindIndex(genpass => genpass.Name.Equals("Slush Check"));
            tasks.Insert(PostSnow + 1, new PassLegacy("Glacial Caverns", TemperateGlacialCaverns));
            int PostShinies = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
            tasks.Insert(PostShinies + 1, new PassLegacy("Glacier Ores", TemperateGlacierOres));
        }

        private void TemperateGlacialCaverns(GenerationProgress progress)
        {
            progress.Message = "Creating Glacial Caverns";
            for (int i = 0; i < Main.maxTilesX; i++)
            {
                for (int j = 0; j < Main.maxTilesY; j++)
                {
                    int top = (int)WorldGen.rockLayer + 125;
                    Tile tile = Framing.GetTileSafely(i, j);
                    if (tile.active() && tile.type == TileID.IceBlock && j >= top && Main.rand.Next(0, 21 - Math.Min(j - top, 20)) == 0)
                    {
                        Main.tile[i, j].type = (ushort)TileType<Tiles.GlacialCaverns.PermafrostTile>();
                    }
                }
            }
        }

        private void TemperateGlacierOres(GenerationProgress progress)
        {
            progress.Message = "Hiding Frozen Treasure";
            for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 6E-05); k++)
            {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                int y = WorldGen.genRand.Next((int)WorldGen.rockLayer + 125, Main.maxTilesY);
                Tile tile = Framing.GetTileSafely(x, y);
                if (tile.active() && tile.type == TileType<Tiles.GlacialCaverns.PermafrostTile>())
                    WorldGen.TileRunner(x, y, WorldGen.genRand.Next(5, 10), WorldGen.genRand.Next(25, 50), TileID.Cobalt); // Cobalt ore is placeholder
            }
        }

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
