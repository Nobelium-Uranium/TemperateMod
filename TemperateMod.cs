using Terraria;
using Terraria.ModLoader;

namespace TemperateMod
{
	public class TemperateMod : Mod
	{
        public override void Load()
        {
            if (!Main.dedServ)
            {
                AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/GlacialCaverns"), ItemType("GlacialCavernsMusicBox"), TileType("GlacialCavernsMusicBox"));
            }
        }

        public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {
            if (Main.myPlayer == -1 || Main.gameMenu || !Main.LocalPlayer.active)
            {
                return;
            }

            if (Main.LocalPlayer.GetModPlayer<TemperatePlayer>().ZoneGlacier && Main.LocalPlayer.ZoneRockLayerHeight)
            {
                music = GetSoundSlot(SoundType.Music, "Sounds/Music/GlacialCaverns");
                priority = MusicPriority.BiomeHigh;
            }
        }
    }
}