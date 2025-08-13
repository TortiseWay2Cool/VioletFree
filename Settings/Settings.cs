using GorillaTag;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using VioletFree.Menu;
using VioletFree.Mods.Spammers;
using VioletFree.Utilities;
using static HandRayController;

namespace VioletFree.Mods.Settings
{
    class Settings
    {

        public static int FlySpeed = 9;
        public static int CurrantFlySpeedCycle = 1;

        public static void ChangeFlySpeed()
        {
            CurrantFlySpeedCycle++;
            if (CurrantFlySpeedCycle > 5)
            {
                CurrantFlySpeedCycle = 1;
            }

            switch (CurrantFlySpeedCycle)
            {
                case 2:
                    FlySpeed = 30;
                    NotificationLib.SendNotification("Currant Speed: " + FlySpeed);
                    break;
                case 3:
                    FlySpeed = 50;
                    NotificationLib.SendNotification("Currant Speed: " + FlySpeed);
                    break;
                case 4:
                    FlySpeed = 70;
                    NotificationLib.SendNotification("Currant Speed: " + FlySpeed);
                    break;
                case 5:
                    FlySpeed = 7;
                    NotificationLib.SendNotification("Currant Speed: " + FlySpeed);
                    break;
                default:
                    FlySpeed = 9;
                    NotificationLib.SendNotification("Currant Speed: " + FlySpeed);
                    break;
            }
        }

        public static float TagDistance = 3;
        public static int TagDistanceCycle = 1;

        public static void ChangeTagAuraDistance()
        {
            TagDistanceCycle++;
            if (TagDistanceCycle > 5)
            {
                TagDistanceCycle = 1;
            }

            switch (TagDistanceCycle)
            {
                case 2:
                    TagDistance = 1;
                    NotificationLib.SendNotification("Currant Distance: [CLOSE]");
                    break;
                case 3:
                    TagDistance = 2;
                    NotificationLib.SendNotification("Currant Distance: [MEDIUM]");
                    break;
                case 4:
                    TagDistance = 4;
                    NotificationLib.SendNotification("Currant Distance: [FAR]");
                    break;
                case 5:
                    TagDistance = 5;
                    NotificationLib.SendNotification("Currant Distance: [INSANE]");
                    break;
                default:
                    TagDistance = 5.5f;
                    NotificationLib.SendNotification("Currant Distance: [MAX]");
                    break;
            }
        }

        public static float TagHandDistance = 1;
        public static int TagHandDistanceCycle = 1;

        public static void ChangeTagTouchDistance()
        {
            TagHandDistanceCycle++;
            if (TagHandDistanceCycle > 5)
            {
                TagHandDistanceCycle = 1;
            }

            switch (TagHandDistanceCycle)
            {
                case 2:
                    TagHandDistance = 0.2f;
                    NotificationLib.SendNotification("Currant Distance: [CLOSE]");
                    break;
                case 3:
                    TagHandDistance = 0.5f;
                    NotificationLib.SendNotification("Currant Distance: [MEDIUM]");
                    break;
                case 4:
                    TagHandDistance = 1;
                    NotificationLib.SendNotification("Currant Distance: [FAR]");
                    break;
                case 5:
                    TagHandDistance = 2;
                    NotificationLib.SendNotification("Currant Distance: [INSANE]");
                    break;
                default:
                    TagHandDistance = 4f;
                    NotificationLib.SendNotification("Currant Distance: [MAX]");
                    break;
            }
        }
        public static string ProjectileType = "";
        public static string ProjectileCode = "";
        public static int CurrantPRojectileCycle = 1;
        public static int projectileNumber = 2;
        public static void ChangeProjectileType()
        {
            CurrantPRojectileCycle++;
            if (CurrantPRojectileCycle > 15)
            {
                CurrantPRojectileCycle = 1;
            }

            switch (CurrantPRojectileCycle)
            {
                case 1:
                    projectileNumber = 2;
                    ProjectileType = "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/WaterBalloonRightAnchor(Clone)";
                    ProjectileCode = "LMAEY. RIGHT.";
                    ButtonHandler.ChangeButtonText("Current Projectile Type", "Current Projectile Type [<color=blue>Water Balloon</color>]");
                    break;
                case 2:
                    projectileNumber = 3;
                    ProjectileType = "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/VotingRockAnchor_RIGHT(Clone)";
                    ProjectileCode = "LMAMT. RIGHT.";
                    ButtonHandler.ChangeButtonText("Current Projectile Type", "Current Projectile Type [<color=blue>Voting Rock</color>]");
                    break;
                case 3:
                    projectileNumber = 4;
                    ProjectileType = "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/BucketGiftFunctionalAnchor_Right(Clone)";
                    ProjectileCode = "LMAHR. RIGHT.";
                    ButtonHandler.ChangeButtonText("Current Projectile Type", "Current Projectile Type [<color=blue>Gift</color>]");
                    break;
                case 4:
                    projectileNumber = 5;
                    ProjectileType = "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/ScienceCandyRightAnchor(Clone)";
                    ProjectileCode = "LMAIF. RIGHT.";
                    ButtonHandler.ChangeButtonText("Current Projectile Type", "Current Projectile Type [<color=blue>Mento</color>]");
                    break;
                case 5:
                    projectileNumber = 6;
                    ProjectileType = "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/FishFoodRightAnchor(Clone)";
                    ProjectileCode = "LMAIP. RIGHT.";
                    ButtonHandler.ChangeButtonText("Current Projectile Type", "Current Projectile Type [<color=blue>FishFood</color>]");
                    break;
                case 6:
                    projectileNumber = 7;
                    ProjectileType = "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/TrickTreatFunctionalAnchorRIGHT Variant(Clone)";
                    ProjectileCode = "LMAMO. RIGHT.";
                    ButtonHandler.ChangeButtonText("Current Projectile Type", "Current Projectile Type [<color=blue>Candy</color>]");
                    break;
                case 7:
                    projectileNumber = 8;
                    ProjectileType = "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/LavaRockAnchor(Clone)";
                    ProjectileCode = "LMAGE. RIGHT.";
                    ButtonHandler.ChangeButtonText("Current Projectile Type", "Current Projectile Type [<color=blue>Lava Rock</color>]");
                    break;
                case 8:
                    projectileNumber = 9;
                    ProjectileType = "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/AppleRightAnchor(Clone)";
                    ProjectileCode = "LMAMV.";
                    ButtonHandler.ChangeButtonText("Current Projectile Type", "Current Projectile Type [<color=blue>Apple</color>]");
                    break;
                case 9:
                    projectileNumber = 10;
                    ProjectileType = "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/BookRightAnchor(Clone)";
                    ProjectileCode = "LMAQA. RIGHT.";
                    ButtonHandler.ChangeButtonText("Current Projectile Type", "Current Projectile Type [<color=blue>Book</color>]");
                    break;
                case 10:
                    projectileNumber = 11;
                    ProjectileType = "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/CoinRightAnchor(Clone)";
                    ProjectileCode = "LMAQC.";
                    ButtonHandler.ChangeButtonText("Current Projectile Type", "Current Projectile Type [<color=blue>Coin</color>]");
                    break;
                case 11:
                    projectileNumber = 12;
                    ProjectileType = "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/EggRightHand_Anchor Variant(Clone)";
                    ProjectileCode = "LMAPS. RIGHT.";
                    ButtonHandler.ChangeButtonText("Current Projectile Type", "Current Projectile Type [<color=blue>Egg</color>]");
                    break;
                case 12:
                    projectileNumber = 13;
                    ProjectileType = "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/IceCreamRightAnchor(Clone)";
                    ProjectileCode = "LMARA. LEFT.";
                    ButtonHandler.ChangeButtonText("Current Projectile Type", "Current Projectile Type [<color=blue>Ice Cream</color>]");
                    break;
                case 13:
                    projectileNumber = 14;
                    ProjectileType = "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/HotDogRightAnchor(Clone)";
                    ProjectileCode = "LMARC.";
                    ButtonHandler.ChangeButtonText("Current Projectile Type", "Current Projectile Type [<color=blue>HotDog</color>]");
                    break;
                case 14:
                    projectileNumber = 15;
                    ProjectileType = "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/Fireworks_Anchor Variant_Right Hand(Clone)";
                    ProjectileCode = "LMAQU. LEFT.";
                    ButtonHandler.ChangeButtonText("Current Projectile Type", "Current Projectile Type [<color=blue>Firework</color>]");
                    break;
                case 15:
                    projectileNumber = 0;
                    ProjectileType = "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/GrowingSnowballRightAnchor(Clone)";
                    ProjectileCode = "LMACF. RIGHT.";
                    ButtonHandler.ChangeButtonText("Current Projectile Type", "Current Projectile Type [<color=blue>Snowball</color>]");
                    break;
            }
        }
        public static int ProjectileSpeed = 15;
        public static int CurrantProjectileSpeedCycle = 1;

        public static void ChangeProjectileSpeed()
        {
            CurrantProjectileSpeedCycle++;
            if (CurrantProjectileSpeedCycle > 7)
            {
                CurrantProjectileSpeedCycle = 1;
            }

            switch (CurrantProjectileSpeedCycle)
            {
                case 2:
                    ProjectileSpeed = 15;
                    ButtonHandler.ChangeButtonText("Current Projectile Velocity", "Current Projectile Velocity [<color=blue>Slow</color>]");
                    break;
                case 3:
                    ProjectileSpeed = 30;
                    ButtonHandler.ChangeButtonText("Current Projectile Velocity", "Current Projectile Velocity [<color=blue>Medium</color>]");
                    break;
                case 4:
                    ProjectileSpeed = 50;
                    ButtonHandler.ChangeButtonText("Current Projectile Velocity", "Current Projectile Velocity [<color=blue>Fast</color>]");
                    break;
                case 5:
                    ProjectileSpeed = 100;
                    ButtonHandler.ChangeButtonText("Current Projectile Velocity", "Current Projectile Velocity [<color=blue>Very Fast</color>]");
                    break;
                case 6:
                    ProjectileSpeed = 800;
                    ButtonHandler.ChangeButtonText("Current Projectile Velocity", "Current Projectile Velocity [<color=blue>Instant</color>]");
                    break;
                case 7:
                    ProjectileSpeed = 6;
                    ButtonHandler.ChangeButtonText("Current Projectile Velocity", "Current Projectile Velocity [<color=blue>Very Slow</color>]");
                    break;
                default: // Case 1
                    ProjectileSpeed = 10;
                    ButtonHandler.ChangeButtonText("Current Projectile Velocity", "Current Projectile Velocity [<color=blue>Pretty Slow</color>]");
                    break;
            }
        }

        public static Color ProjectileColor = Color.red;
        public static int ProjectileColorCycle = 1;

        public static void ChangeProjectileColor()
        {
            ProjectileColorCycle++;
            if (ProjectileColorCycle > 12)
            {
                ProjectileColorCycle = 1;
            }

            switch (ProjectileColorCycle)
            {
                case 1:
                    ProjectileColor = Color.red;
                    ButtonHandler.ChangeButtonText("Current Projectile Color", "Current Projectile Color [<color=blue>Red</color>]");
                    break;
                case 2:
                    ProjectileColor = Color.blue;
                    ButtonHandler.ChangeButtonText("Current Projectile Color", "Current Projectile Color [<color=blue>Blue</color>]");
                    break;
                case 3:
                    ProjectileColor = Color.green;
                    ButtonHandler.ChangeButtonText("Current Projectile Color", "Current Projectile Color [<color=blue>Green</color>]");
                    break;
                case 4:
                    ProjectileColor = Color.yellow;
                    ButtonHandler.ChangeButtonText("Current Projectile Color", "Current Projectile Color [<color=blue>Yellow</color>]");
                    break;
                case 5:
                    ProjectileColor = Color.cyan;
                    ButtonHandler.ChangeButtonText("Current Projectile Color", "Current Projectile Color [<color=blue>Cyan</color>]");
                    break;
                case 6:
                    ProjectileColor = Color.magenta;
                    ButtonHandler.ChangeButtonText("Current Projectile Color", "Current Projectile Color [<color=blue>Magenta</color>]");
                    break;
                case 7:
                    ProjectileColor = Color.white;
                    ButtonHandler.ChangeButtonText("Current Projectile Color", "Current Projectile Color [<color=blue>White</color>]");
                    break;
                case 8:
                    ProjectileColor = new Color(1f, 0.5f, 0f); // Orange
                    ButtonHandler.ChangeButtonText("Current Projectile Color", "Current Projectile Color [<color=blue>Orange</color>]");
                    break;
                case 9:
                    ProjectileColor = new Color(0.5f, 0f, 0.5f); // Purple
                    ButtonHandler.ChangeButtonText("Current Projectile Color", "Current Projectile Color [<color=blue>Purple</color>]");
                    break;
                case 10:
                    ProjectileColor = new Color(1f, 0.65f, 0.8f); // Pink
                    ButtonHandler.ChangeButtonText("Current Projectile Color", "Current Projectile Color [<color=blue>Pink</color>]");
                    break;
                case 11:
                    ProjectileColor = new Color(0.5f, 0.5f, 0.5f); // Gray
                    ButtonHandler.ChangeButtonText("Current Projectile Color", "Current Projectile Color [<color=blue>Gray</color>]");
                    break;
                case 12:
                    ProjectileColor = new Color(0f, 0.5f, 0.5f); // Teal
                    ButtonHandler.ChangeButtonText("Current Projectile Color", "Current Projectile Color [<color=blue>Teal</color>]");
                    break;
            }
        }

        public static void ToggleRightHandedMenu(bool enabled)
        {
            Variables.rightHandedMenu = enabled;
            Optimizations.RefreshMenu();
        }

        public static bool LongMenuEnabled = false;
        public static void LongMenu(bool Enabled)
        {
            if (Enabled)
            {
                LongMenuEnabled = true;
                Optimizations.RefreshMenu();
            }
            else
            {
                LongMenuEnabled = false;
                Optimizations.RefreshMenu();
            }
        }

        public static bool WideMenuEnabled = false;
        public static void WideMenu(bool Enabled)
        {
            if (Enabled)
            {
                WideMenuEnabled = true;
                Optimizations.RefreshMenu();
            }
            else
            {
                WideMenuEnabled = false;
                Optimizations.RefreshMenu();
            }
        }


        public static int CurrentThemeCycle = 1;
        public static Color32 CurrentBackgroundColor = ColorLib.Violet;

        public static void ChangeBackgroundColor()
        {
            CurrentThemeCycle++;
            if (CurrentThemeCycle > 10)
            {
                CurrentThemeCycle = 1;
            }

            switch (CurrentThemeCycle)
            {
                case 1:
                    CurrentBackgroundColor = ColorLib.DeepVioletTransparent;
                    NotificationLib.SendNotification("Current Theme: Violet");
                    break;
                case 2:
                    CurrentBackgroundColor = ColorLib.RedTransparent;
                    NotificationLib.SendNotification("Current Theme: Red");
                    break;
                case 3:
                    CurrentBackgroundColor = ColorLib.GreenTransparent;
                    NotificationLib.SendNotification("Current Theme: Green ");
                    break;
                case 4:
                    CurrentBackgroundColor = ColorLib.BlueTransparent;
                    NotificationLib.SendNotification("Current Theme: Blue");
                    break;
                case 5:
                    CurrentBackgroundColor = ColorLib.YellowTransparent;
                    NotificationLib.SendNotification("Current Theme: Yellow ");
                    break;
                case 6:
                    CurrentBackgroundColor = ColorLib.OrangeTransparent;
                    NotificationLib.SendNotification("Current Theme: Orange ");
                    break;
                case 7:
                    CurrentBackgroundColor = ColorLib.PurpleTransparent;
                    NotificationLib.SendNotification("Current Theme: Purple ");
                    break;
                case 8:
                    CurrentBackgroundColor = ColorLib.PinkTransparent;
                    NotificationLib.SendNotification("Current Theme: Pink ");
                    break;
                case 9:
                    CurrentBackgroundColor = ColorLib.CyanTransparent;
                    NotificationLib.SendNotification("Current Theme: Cyan ");
                    break;
                case 10:
                    CurrentBackgroundColor = ColorLib.BlackTransparent;
                    NotificationLib.SendNotification("Current Theme: Black ");
                    break;
                default:
                    CurrentBackgroundColor = ColorLib.DeepVioletTransparent;
                    NotificationLib.SendNotification("Current Theme: Violet ");
                    break;
            }
            Optimizations.RefreshMenu();
            Optimizations.CleanupMenu(0f);
        }

        public static int CurrentTheme = 0;

        public static void ChangeTheme()
        {
            CurrentTheme++;
            if (CurrentTheme > 10)
            {
                CurrentTheme = 0;
            }

            switch (CurrentTheme)
            {
                case 0:
                    CurrentBackgroundColor = Variables.violet;
                    Variables.ButtonColorOff = Color.black;
                    Variables.ButtonColorOn = ColorLib.Indigo;
                    NotificationLib.SendNotification("Current Theme: Violet");
                    break;

                case 1:
                    CurrentBackgroundColor = ColorLib.Red;
                    Variables.ButtonColorOff = Color.black;
                    Variables.ButtonColorOn = ColorLib.Crimson;
                    NotificationLib.SendNotification("Current Theme: Red");
                    break;

                case 2:
                    CurrentBackgroundColor = ColorLib.Green;
                    Variables.ButtonColorOff = Color.black;
                    Variables.ButtonColorOn = ColorLib.Lime;
                    NotificationLib.SendNotification("Current Theme: Green");
                    break;

                case 3:
                    CurrentBackgroundColor = ColorLib.Blue;
                    Variables.ButtonColorOff = Color.black;
                    Variables.ButtonColorOn = ColorLib.Cyan;
                    NotificationLib.SendNotification("Current Theme: Blue");
                    break;

                case 4:
                    CurrentBackgroundColor = ColorLib.Yellow;
                    Variables.ButtonColorOff = Color.black;
                    Variables.ButtonColorOn = ColorLib.Gold;
                    NotificationLib.SendNotification("Current Theme: Yellow");
                    break;

                case 5:
                    CurrentBackgroundColor = new Color(0.1f, 0.1f, 0.1f); 
                    Variables.ButtonColorOff = Color.white;
                    Variables.ButtonColorOn = Color.gray;
                    Variables.TitleTextColor = Color.white;
                    Variables.PageButtonsTextColor = Color.black;
                    NotificationLib.SendNotification("Current Theme: Black & White");
                    break;

                case 6:
                    CurrentBackgroundColor = Color.white;
                    Variables.ButtonColorOff = new Color(0.1f, 0.1f, 0.1f);
                    Variables.ButtonColorOn = Color.gray;
                    Variables.MainTitleColor = Color.black;
                    Variables.PageButtonsTextColor = Color.white;
                    NotificationLib.SendNotification("Current Theme: White & Black");
                    break;

                case 7:
                    CurrentBackgroundColor = ColorLib.Orange;
                    Variables.ButtonColorOff = Color.black;
                    Variables.ButtonColorOn = new Color(1f, 0.6f, 0.2f); 

                    NotificationLib.SendNotification("Current Theme: Orange");
                    break;

                case 8:
                    CurrentBackgroundColor = new Color(64f / 255f, 224f / 255f, 208f / 255f);
                    Variables.ButtonColorOff = Color.black;
                    Variables.ButtonColorOn = new Color(0.25f, 0.88f, 0.82f); 
                    NotificationLib.SendNotification("Current Theme: Turquoise");
                    break;

                case 9:
                    CurrentBackgroundColor = ColorLib.Pink;
                    Variables.ButtonColorOff = Color.black;
                    Variables.ButtonColorOn = new Color(1f, 0.5f, 0.75f);
                    NotificationLib.SendNotification("Current Theme: Pink");
                    break;
                case 10:
                    CurrentBackgroundColor = ColorLib.FireBrickTransparent;
                    CurrentBackgroundColor = ColorLib.FireBrick;
                    Variables.ButtonColorOff = ColorLib.WineRed;
                    Variables.ButtonColorOn = ColorLib.IndianRed;
                    Variables.DisconnecyColor = ColorLib.Crimson;
                    ButtonHandler.ChangeButtonText("Current Menu Theme", "Current Menu Theme [Red]");
                    break;
            }
        }
    }
}
