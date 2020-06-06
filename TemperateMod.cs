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
    }
}