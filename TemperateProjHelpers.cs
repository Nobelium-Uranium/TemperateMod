using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using TemperateMod;

namespace TemperateMod
{
    public class TemperateProjHelpers : GlobalProjectile
    {
        public static float AddGravity(Projectile projectile, float wantedGravityStrength)
        {
            return projectile.velocity.Y += wantedGravityStrength;
        }
    }
}
