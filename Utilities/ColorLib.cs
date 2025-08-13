using System.Net;
using System;
using UnityEngine;
using static VioletFree.Menu.Optimizations;
using UnityEngine.Networking;
using System.Collections;

namespace VioletFree.Utilities
{
    public class ColorLib
    {
        #region Non-Transparent Colors

        // Reds
        public static Color32 Red = new Color32(255, 0, 0, 255);
        public static Color32 DarkRed = new Color32(180, 0, 0, 255);
        public static Color32 Salmon = new Color32(250, 128, 114, 255);
        public static Color32 WineRed = new Color32(123, 0, 0, 255);
        public static Color32 IndianRed = new Color32(205, 92, 92, 255);
        public static Color32 Crimson = new Color32(220, 20, 60, 255);
        public static Color32 FireBrick = new Color32(178, 34, 34, 255);
        public static Color32 Coral = new Color32(255, 127, 80, 255);
        public static Color32 Tomato = new Color32(255, 99, 71, 255);
        public static Color32 Maroon = new Color32(128, 0, 0, 255);

        // Greens
        public static Color32 Green = new Color32(0, 255, 0, 255);
        public static Color32 Lime = new Color32(0, 128, 0, 255);
        public static Color32 DarkGreen = new Color32(0, 100, 0, 255);
        public static Color32 Olive = new Color32(128, 128, 0, 255);
        public static Color32 ForestGreen = new Color32(34, 139, 34, 255);
        public static Color32 SeaGreen = new Color32(46, 139, 87, 255);
        public static Color32 MediumSeaGreen = new Color32(60, 179, 113, 255);
        public static Color32 Aquamarine = new Color32(127, 255, 212, 255);
        public static Color32 MediumAquamarine = new Color32(102, 205, 170, 255);
        public static Color32 DarkSeaGreen = new Color32(143, 188, 143, 255);

        // Blues
        public static Color32 Blue = new Color32(0, 0, 255, 255);
        public static Color32 Navy = new Color32(0, 0, 128, 255);
        public static Color32 DarkBlue = new Color32(0, 0, 160, 255);
        public static Color32 RoyalBlue = new Color32(65, 105, 225, 255);
        public static Color32 DodgerBlue = new Color32(30, 144, 255, 255);
        public static Color32 DarkDodgerBlue = new Color32(8, 90, 177, 255);
        public static Color32 DeepSkyBlue = new Color32(0, 191, 255, 255);
        public static Color32 SkyBlue = new Color32(135, 206, 235, 255);
        public static Color32 SteelBlue = new Color32(70, 130, 180, 255);
        public static Color32 Cyan = new Color32(0, 255, 255, 255);

        // Yellows
        public static Color32 Yellow = new Color32(255, 255, 0, 255);
        public static Color32 Gold = new Color32(255, 215, 0, 255);
        public static Color32 LightYellow = new Color32(255, 255, 224, 255);
        public static Color32 LemonChiffon = new Color32(255, 250, 205, 255);
        public static Color32 Khaki = new Color32(240, 230, 140, 255);
        public static Color32 PaleGoldenrod = new Color32(238, 232, 170, 255);
        public static Color32 LightGoldenrodYellow = new Color32(250, 250, 210, 255);

        // Oranges
        public static Color32 Orange = new Color32(255, 165, 0, 255);
        public static Color32 DarkOrange = new Color32(255, 140, 0, 255);
        public static Color32 RedOrange = new Color32(255, 69, 0, 255);
        public static Color32 PeachPuff = new Color32(255, 218, 185, 255);
        public static Color32 DarkGoldenrod = new Color32(184, 134, 11, 255);
        public static Color32 Peru = new Color32(205, 133, 63, 255);
        public static Color32 OrangeRed = new Color32(255, 69, 0, 255);

        // Purples
        public static Color32 Magenta = new Color32(255, 0, 255, 255);
        public static Color32 Purple = new Color32(123, 3, 252, 255);
        public static Color32 Lavender = new Color32(230, 230, 250, 255);
        public static Color32 Plum = new Color32(221, 160, 221, 255);
        public static Color32 Indigo = new Color32(75, 0, 130, 255);
        public static Color32 MediumOrchid = new Color32(186, 85, 211, 255);
        public static Color32 SlateBlue = new Color32(106, 90, 205, 255);
        public static Color32 DarkSlateBlue = new Color32(72, 61, 139, 255);

        // Pinks
        public static Color32 Pink = new Color32(255, 192, 203, 255);
        public static Color32 LightSalmon = new Color32(255, 160, 122, 255);
        public static Color32 DarkSalmon = new Color32(233, 150, 122, 255);
        public static Color32 LightCoral = new Color32(240, 128, 128, 255);
        public static Color32 MistyRose = new Color32(255, 228, 225, 255);
        public static Color32 HotPink = new Color32(255, 105, 180, 255);
        public static Color32 DeepPink = new Color32(255, 20, 147, 255);

        // Browns
        public static Color32 Brown = new Color32(139, 69, 19, 255);
        public static Color32 RosyBrown = new Color32(188, 143, 143, 255);
        public static Color32 SaddleBrown = new Color32(139, 69, 19, 255);
        public static Color32 Sienna = new Color32(160, 82, 45, 255);
        public static Color32 Chocolate = new Color32(210, 105, 30, 255);
        public static Color32 SandyBrown = new Color32(244, 164, 96, 255);
        public static Color32 BurlyWood = new Color32(222, 184, 135, 255);
        public static Color32 Tan = new Color32(210, 180, 140, 255);

        // Whites
        public static Color32 White = new Color32(255, 255, 255, 255);
        public static Color32 Linen = new Color32(250, 240, 230, 255);
        public static Color32 OldLace = new Color32(253, 245, 230, 255);
        public static Color32 SeaShell = new Color32(255, 245, 238, 255);
        public static Color32 MintCream = new Color32(245, 255, 250, 255);

        // Blacks and Grays
        public static Color32 Black = new Color32(0, 0, 0, 255);
        public static Color32 Grey = new Color32(128, 128, 128, 255);
        public static Color32 LightGrey = new Color32(192, 192, 192, 255);
        public static Color32 DarkGrey = new Color32(80, 80, 80, 255);
        public static Color32 DarkerGrey = new Color32(40, 40, 40, 255);
        #endregion

        #region Transparent Colors

        // Reds
        public static Color32 RedTransparent = new Color32(255, 0, 0, 80);
        public static Color32 DarkRedTransparent = new Color32(180, 0, 0, 80);
        public static Color32 SalmonTransparent = new Color32(250, 128, 114, 80);
        public static Color32 IndianRedTransparent = new Color32(205, 92, 92, 80);
        public static Color32 CrimsonTransparent = new Color32(220, 20, 60, 80); 
        public static Color32 WineRedTransparent = new Color32(123, 0, 0, 80);
        public static Color32 FireBrickTransparent = new Color32(178, 34, 34, 80);
        public static Color32 CoralTransparent = new Color32(255, 127, 80, 80);
        public static Color32 TomatoTransparent = new Color32(255, 99, 71, 80);
        public static Color32 MaroonTransparent = new Color32(128, 0, 0, 80);

        // Greens
        public static Color32 GreenTransparent = new Color32(0, 255, 0, 80);
        public static Color32 LimeTransparent = new Color32(0, 128, 0, 80);
        public static Color32 DarkGreenTransparent = new Color32(0, 100, 0, 80);
        public static Color32 OliveTransparent = new Color32(128, 128, 0, 80);
        public static Color32 ForestGreenTransparent = new Color32(34, 139, 34, 80);
        public static Color32 SeaGreenTransparent = new Color32(46, 139, 87, 80);
        public static Color32 MediumSeaGreenTransparent = new Color32(60, 179, 113, 80);
        public static Color32 AquamarineTransparent = new Color32(127, 255, 212, 80);
        public static Color32 MediumAquamarineTransparent = new Color32(102, 205, 170, 80);
        public static Color32 DarkSeaGreenTransparent = new Color32(143, 188, 143, 80);

        // Blues
        public static Color32 BlueTransparent = new Color32(0, 0, 255, 80);
        public static Color32 NavyTransparent = new Color32(0, 0, 128, 80);
        public static Color32 DarkBlueTransparent = new Color32(0, 0, 139, 80);
        public static Color32 RoyalBlueTransparent = new Color32(65, 105, 225, 80);
        public static Color32 DodgerBlueTransparent = new Color32(30, 144, 255, 80);
        public static Color32 DarkDodgerBlueTransparent = new Color32(8, 90, 177, 80);
        public static Color32 DeepSkyBlueTransparent = new Color32(0, 191, 255, 80);
        public static Color32 SkyBlueTransparent = new Color32(135, 206, 235, 80);
        public static Color32 SteelBlueTransparent = new Color32(70, 130, 180, 80);
        public static Color32 CyanTransparent = new Color32(0, 255, 255, 80);

        // Yellows
        public static Color32 YellowTransparent = new Color32(255, 255, 0, 80);
        public static Color32 GoldTransparent = new Color32(255, 215, 0, 80);
        public static Color32 LightYellowTransparent = new Color32(255, 255, 224, 80);
        public static Color32 LemonChiffonTransparent = new Color32(255, 250, 205, 80);
        public static Color32 KhakiTransparent = new Color32(240, 230, 140, 80);
        public static Color32 PaleGoldenrodTransparent = new Color32(238, 232, 170, 80);
        public static Color32 LightGoldenrodYellowTransparent = new Color32(250, 250, 210, 80);

        // Oranges
        public static Color32 OrangeTransparent = new Color32(255, 165, 0, 80);
        public static Color32 DarkOrangeTransparent = new Color32(255, 140, 0, 80);
        public static Color32 RedOrangeTransparent = new Color32(255, 69, 0, 80);
        public static Color32 PeachPuffTransparent = new Color32(255, 218, 185, 80);
        public static Color32 DarkGoldenrodTransparent = new Color32(184, 134, 11, 80);
        public static Color32 PeruTransparent = new Color32(205, 133, 63, 80);
        public static Color32 OrangeRedTransparent = new Color32(255, 69, 0, 80);

        // Purples
        public static Color32 MagentaTransparent = new Color32(255, 0, 255, 80);
        public static Color32 PurpleTransparent = new Color32(123, 3, 252, 80);
        public static Color32 LavenderTransparent = new Color32(230, 230, 250, 80);
        public static Color32 PlumTransparent = new Color32(221, 160, 221, 80);
        public static Color32 IndigoTransparent = new Color32(75, 0, 130, 80);
        public static Color32 MediumOrchidTransparent = new Color32(186, 85, 211, 80);
        public static Color32 SlateBlueTransparent = new Color32(106, 90, 205, 80);
        public static Color32 DarkSlateBlueTransparent = new Color32(72, 61, 139, 80);

        // Pinks
        public static Color32 PinkTransparent = new Color32(255, 192, 203, 80);
        public static Color32 LightSalmonTransparent = new Color32(255, 160, 122, 80);
        public static Color32 DarkSalmonTransparent = new Color32(233, 150, 122, 80);
        public static Color32 LightCoralTransparent = new Color32(240, 128, 128, 80);
        public static Color32 MistyRoseTransparent = new Color32(255, 228, 225, 80);
        public static Color32 HotPinkTransparent = new Color32(255, 105, 180, 80);
        public static Color32 DeepPinkTransparent = new Color32(255, 20, 147, 80);

        // Browns
        public static Color32 BrownTransparent = new Color32(165, 42, 42, 80);
        public static Color32 RosyBrownTransparent = new Color32(188, 143, 143, 80);
        public static Color32 SaddleBrownTransparent = new Color32(139, 69, 19, 80);
        public static Color32 SiennaTransparent = new Color32(160, 82, 45, 80);
        public static Color32 ChocolateTransparent = new Color32(210, 105, 30, 80);
        public static Color32 SandyBrownTransparent = new Color32(244, 164, 96, 80);
        public static Color32 BurlyWoodTransparent = new Color32(222, 184, 135, 80);
        public static Color32 TanTransparent = new Color32(210, 180, 140, 80);

        // Whites
        public static Color32 WhiteTransparent = new Color32(255, 255, 255, 80);
        public static Color32 LightWhiteTransparent = new Color32(255, 255, 255, 10);
        public static Color32 LinenTransparent = new Color32(250, 240, 230, 80);
        public static Color32 OldLaceTransparent = new Color32(253, 245, 230, 80);
        public static Color32 SeaShellTransparent = new Color32(255, 245, 238, 80);
        public static Color32 MintCreamTransparent = new Color32(245, 255, 250, 80);

        // Blacks and Grays
        public static Color32 BlackTransparent = new Color32(0, 0, 0, 80);
        public static Color32 GreyTransparent = new Color32(80, 80, 80, 80);
        public static Color32 LightGreyTransparent = new Color32(192, 192, 192, 80);
        public static Color32 DarkGreyTransparent = new Color32(40, 40, 40, 80);
        public static Color32 DarkerGreyTransparent = new Color32(40, 40, 40, 80);
        #endregion

        public static Shader guiShader = Shader.Find("GUI/Text Shader");
        public static Shader uberShader = Shader.Find("GorillaTag/UberShader");
        public static Color32 DeepVioletTransparent = new Color32(115, 0, 230, 60);
        public static Color Violet = new Color(0.56f, 0.0f, 1.0f);
        public static int CurrentTheme = 0;
        public static Color32[] ThemeArraya = new Color32[]
        {
            new Color(0, 0, 200, 255),    // Blue
            new Color32(0, 0, 0, 255),     // Black
            new Color32(123, 3, 200, 255), // Purple
            new Color32(0, 100, 0, 255),   // Green
            new Color32(200, 0, 0, 255),   // Red
            new Color32(200, 60, 0, 255),  // Orange
        };
    }
}
