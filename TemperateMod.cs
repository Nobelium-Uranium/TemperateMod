using System;
using System.Diagnostics;
using Terraria.ModLoader;
using Terraria;
using ReLogic.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Collections.Generic;
using Terraria.UI;

namespace TemperateMod
{
	public class TemperateMod : Mod
	{
        private static int UI_ScreenAnchorX = Terraria.Main.screenWidth - 800;
        private static int UIDisplay_ManaPerStar;

        public override void Load()
        {
            On.Terraria.Main.DrawInterface_Resources_Mana += NewDrawMana;
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

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int index = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Ruler"));

            layers.Insert(index, new LegacyGameInterfaceLayer("TemperateMod: Avali Manipulator", delegate
            {
                Player player = Main.LocalPlayer;

                if (player.HeldItem.type == ModContent.ItemType<Items.Tools.Chem.AvaliManipulator>())
                    DrawAvaliManipulator(player);

                return true;
            }, InterfaceScaleType.Game));
        }

        public void DrawAvaliManipulator(Player player)
        {
            float distance = Vector2.Distance(player.Center, Main.MouseWorld);

            if (distance < 320 && player.active && !player.dead)
            {
                Texture2D texture = GetTexture("UI/MatterManipulator");
                Point uiPositionPoint = Main.MouseWorld.ToTileCoordinates();
                Vector2 uiPosition = uiPositionPoint.ToVector2();
                Main.spriteBatch.Draw(texture, uiPosition.ToWorldCoordinates() - Main.screenPosition, null, Color.White * 0.5f, 0f, texture.Size() / 2, 1f, SpriteEffects.None, 0f);
            }
        }


        private void NewDrawMana(On.Terraria.Main.orig_DrawInterface_Resources_Mana orig)
        {
            if (Terraria.Main.player[Terraria.Main.myPlayer].statManaMax2 / 10 >= 20)
                UIDisplay_ManaPerStar = Terraria.Main.player[Terraria.Main.myPlayer].statManaMax2 / 10;
            else
                UIDisplay_ManaPerStar = 20;
            if (Terraria.Main.LocalPlayer.ghost || Terraria.Main.player[Terraria.Main.myPlayer].statManaMax2 <= 0)
            {
                return;
            }
            int num18 = Terraria.Main.player[Terraria.Main.myPlayer].statManaMax / 20;
            int num17 = Terraria.Main.player[Terraria.Main.myPlayer].GetModPlayer<TemperatePlayer>().ArcaneCrystals;
            if (num17 < 0)
            {
                num17 = 0;
            }
            if (num17 > 0)
            {
                num18 = Terraria.Main.player[Terraria.Main.myPlayer].statManaMax / (20 + num17 / 2);
            }
            _ = Terraria.Main.player[Terraria.Main.myPlayer].statManaMax2 / 20;
            Microsoft.Xna.Framework.Vector2 vector = Terraria.Main.fontMouseText.MeasureString(Terraria.Lang.inter[2].Value);
            int num8 = 50;
            if (vector.X >= 45f)
            {
                num8 = (int)vector.X + 5;
            }
            DynamicSpriteFontExtensionMethods.DrawString(Terraria.Main.spriteBatch, Terraria.Main.fontMouseText, Terraria.Lang.inter[2].Value, new Microsoft.Xna.Framework.Vector2(800 - num8 + UI_ScreenAnchorX, 6f), new Microsoft.Xna.Framework.Color(Terraria.Main.mouseTextColor, Terraria.Main.mouseTextColor, Terraria.Main.mouseTextColor, Terraria.Main.mouseTextColor), 0f, default(Microsoft.Xna.Framework.Vector2), 1f, SpriteEffects.None, 0f);
            if (UIDisplay_ManaPerStar <= 20)
                for (int i = 1; i < Terraria.Main.player[Terraria.Main.myPlayer].statManaMax2 / UIDisplay_ManaPerStar + 1; i++)
                {
                    int num7 = 255;
                    bool flag = false;
                    float num6 = 1f;
                    if (Terraria.Main.player[Terraria.Main.myPlayer].statMana >= i * UIDisplay_ManaPerStar)
                    {
                        num7 = 255;
                        if (Terraria.Main.player[Terraria.Main.myPlayer].statMana == i * UIDisplay_ManaPerStar)
                        {
                            flag = true;
                        }
                    }
                    else
                    {
                        float num4 = (float)(Terraria.Main.player[Terraria.Main.myPlayer].statMana - (i - 1) * UIDisplay_ManaPerStar) / (float)UIDisplay_ManaPerStar;
                        num7 = (int)(30f + 225f * num4);
                        if (num7 < 30)
                        {
                            num7 = 30;
                        }
                        num6 = num4 / 4f + 0.75f;
                        if ((double)num6 < 0.75)
                        {
                            num6 = 0.75f;
                        }
                        if (num4 > 0f)
                        {
                            flag = true;
                        }
                    }
                    if (flag)
                    {
                        num6 += Terraria.Main.cursorScale - 1f;
                    }
                    int a = (int)((double)(float)num7 * 0.9);
                    if (!Terraria.Main.player[Terraria.Main.myPlayer].ghost)
                    {
                        if (num17 > 0)
                        {
                            num17--;
                            Terraria.Main.spriteBatch.Draw(GetTexture("Items/Consumables/Special/ArcaneOverlay"), new Microsoft.Xna.Framework.Vector2(775 + UI_ScreenAnchorX, (float)(30 + Terraria.Main.manaTexture.Height / 2) + ((float)Terraria.Main.manaTexture.Height - (float)Terraria.Main.manaTexture.Height * num6) / 2f + (float)(28 * (i - 1))), new Microsoft.Xna.Framework.Rectangle(0, 0, Terraria.Main.manaTexture.Width, Terraria.Main.manaTexture.Height), new Microsoft.Xna.Framework.Color(num7, num7, num7, a), 0f, new Vector2(Terraria.Main.manaTexture.Width / 2, Terraria.Main.manaTexture.Height / 2), num6, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0f);
                        }
                        else
                        {
                            Terraria.Main.spriteBatch.Draw(Terraria.Main.manaTexture, new Microsoft.Xna.Framework.Vector2(775 + UI_ScreenAnchorX, (float)(30 + Terraria.Main.manaTexture.Height / 2) + ((float)Terraria.Main.manaTexture.Height - (float)Terraria.Main.manaTexture.Height * num6) / 2f + (float)(28 * (i - 1))), new Microsoft.Xna.Framework.Rectangle(0, 0, Terraria.Main.manaTexture.Width, Terraria.Main.manaTexture.Height), new Microsoft.Xna.Framework.Color(num7, num7, num7, a), 0f, new Vector2(Terraria.Main.manaTexture.Width / 2, Terraria.Main.manaTexture.Height / 2), num6, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0f);
                        }
                    }
                }
            else if (UIDisplay_ManaPerStar > 20)
                for (int i = 1; i < Terraria.Main.player[Terraria.Main.myPlayer].statManaMax2 / UIDisplay_ManaPerStar + 1; i++)
                {
                    int num7 = 255;
                    bool flag = false;
                    float num6 = 1f;
                    if (Terraria.Main.player[Terraria.Main.myPlayer].statMana >= i * UIDisplay_ManaPerStar)
                    {
                        num7 = 255;
                        if (Terraria.Main.player[Terraria.Main.myPlayer].statMana == i * UIDisplay_ManaPerStar)
                        {
                            flag = true;
                        }
                    }
                    else
                    {
                        float num4 = (float)(Terraria.Main.player[Terraria.Main.myPlayer].statMana - (i - 1) * UIDisplay_ManaPerStar) / (float)UIDisplay_ManaPerStar;
                        num7 = (int)(30f + 225f * num4);
                        if (num7 < 30)
                        {
                            num7 = 30;
                        }
                        num6 = num4 / 4f + 0.75f;
                        if ((double)num6 < 0.75)
                        {
                            num6 = 0.75f;
                        }
                        if (num4 > 0f)
                        {
                            flag = true;
                        }
                    }
                    if (flag)
                    {
                        num6 += Terraria.Main.cursorScale - 1f;
                    }
                    int a = (int)((double)(float)num7 * 0.9);
                    if (!Terraria.Main.player[Terraria.Main.myPlayer].ghost)
                    {
                        if (num17 > 0)
                        {
                            num17--;
                            Terraria.Main.spriteBatch.Draw(GetTexture("Items/Consumables/Special/ArcaneOverlay"), new Microsoft.Xna.Framework.Vector2(775 + UI_ScreenAnchorX, (float)(30 + Terraria.Main.manaTexture.Height / 2) + ((float)Terraria.Main.manaTexture.Height - (float)Terraria.Main.manaTexture.Height * num6) / 2f + (float)(28 * (i - 1))), new Microsoft.Xna.Framework.Rectangle(0, 0, Terraria.Main.manaTexture.Width, Terraria.Main.manaTexture.Height), new Microsoft.Xna.Framework.Color(num7, num7, num7, a), 0f, new Vector2(Terraria.Main.manaTexture.Width / 2, Terraria.Main.manaTexture.Height / 2), num6, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0f);
                        }
                        else
                        {
                            Terraria.Main.spriteBatch.Draw(Terraria.Main.manaTexture, new Microsoft.Xna.Framework.Vector2(775 + UI_ScreenAnchorX, (float)(30 + Terraria.Main.manaTexture.Height / 2) + ((float)Terraria.Main.manaTexture.Height - (float)Terraria.Main.manaTexture.Height * num6) / 2f + (float)(28 * (i - 1))), new Microsoft.Xna.Framework.Rectangle(0, 0, Terraria.Main.manaTexture.Width, Terraria.Main.manaTexture.Height), new Microsoft.Xna.Framework.Color(num7, num7, num7, a), 0f, new Vector2(Terraria.Main.manaTexture.Width / 2, Terraria.Main.manaTexture.Height / 2), num6, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0f);
                        }
                    }
                }
        }
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            TemperateModMessageType msgType = (TemperateModMessageType)reader.ReadByte();
            switch (msgType)
            {
                case TemperateModMessageType.SteviesModPlayerSyncPlayer:
                    byte playerNumber = reader.ReadByte();
                    TemperatePlayer temperatePlayer = Terraria.Main.player[playerNumber].GetModPlayer<TemperatePlayer>();
                    int arcaneCrystals = reader.ReadInt32();
                    temperatePlayer.ArcaneCrystals = arcaneCrystals;
                    break;
            }
            base.HandlePacket(reader, whoAmI);
        }
        internal enum TemperateModMessageType : byte
        {
            SteviesModPlayerSyncPlayer
        }
    }
}