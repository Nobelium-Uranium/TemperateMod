using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TemperateMod.Dusts
{
    class TracerBeam : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.alpha = 2;
            dust.noGravity = true;
            dust.frame = new Rectangle(0, 0, 4, 4);
            dust.position -= new Vector2(2, 2);
        }
        public override bool Update(Dust dust)
        {
            dust.alpha -= 1;
            if (dust.alpha <= 0)
            {
                dust.active = false;
            }
            return false;
        }
        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            return Color.White;
        }
    }
}
