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
        }

        private void TemperateGlacialCaverns(GenerationProgress progress)
        {
            progress.Message = "Creating Glacial Caverns";

            for (int i = 0; i < Main.maxTilesX; i++)
            {
                for (int j = 0; j < Main.maxTilesY; j++)
                {
                    Tile tile = Framing.GetTileSafely(i, j);
                    if (tile.active() && tile.type == TileID.IceBlock && j == 150)
                    {
                        if (Main.rand.NextBool())
                            Main.tile[i, j].type = (ushort)TileType<Tiles.GlacialCaverns.PermafrostTile>();
                        if (Main.rand.NextBool(3))
                            Main.tile[i, j - 1].type = (ushort)TileType<Tiles.GlacialCaverns.PermafrostTile>();
                    }
                }
            }

            for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 6E-05); k++)
            {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                int y = WorldGen.genRand.Next((int)WorldGen.rockLayer + 150, Main.maxTilesY);
                Tile tile = Framing.GetTileSafely(x, y);
                if (tile.active() && tile.type == TileID.IceBlock)
                    WorldGen.TileRunner(x, y, WorldGen.genRand.Next(6, 12), WorldGen.genRand.Next(12, 24), TileID.Cobalt); // Cobalt ore is placeholder
            }

            for (int i = 0; i < Main.maxTilesX; i++)
            {
                for (int j = 0; j < Main.maxTilesY; j++)
                {
                    Tile tile = Framing.GetTileSafely(i, j);
                    if (tile.active() && tile.type == TileID.IceBlock && j >= (int)WorldGen.rockLayer + 151)
                    {
                        Main.tile[i, j].type = (ushort)TileType<Tiles.GlacialCaverns.PermafrostTile>();
                    }
                }
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
