using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TemperateMod
{
    class TemperateNPC : GlobalNPC
    {
        public override bool InstancePerEntity { get { return true; } }

        public bool Obliterated;

        public override bool PreNPCLoot(NPC npc)
        {
            if (Obliterated)
                return false;
            return base.PreNPCLoot(npc);
        }

        public override void NPCLoot(NPC npc)
        {
            if (!Main.expertMode)
            {
                if ((npc.type == NPCID.Retinazer && !NPC.AnyNPCs(NPCID.Spazmatism)) || (npc.type == NPCID.Spazmatism && !NPC.AnyNPCs(NPCID.Retinazer)))
                {
                    if (Main.rand.NextBool(10))
                        Item.NewItem(npc.getRect(), ItemType<Items.Accessories.LaserSight>());
                }
            }
        }
    }
}
