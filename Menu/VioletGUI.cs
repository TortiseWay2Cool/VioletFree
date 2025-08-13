/*
    using GorillaNetworking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using VioletFree.Mods.Movement;
using VioletFree.Mods.Overpowered;
using VioletFree.Mods.Player;
using VioletFree.Mods.Room;
using VioletFree.Mods.Settings;
using VioletFree.Mods.Spammers;
using VioletFree.Mods.Vissual;
using VioletFree.Utilities;
using VioletFree.Menu;
using static VioletFree.Menu.ButtonHandler;
using static VioletFree.Menu.Main;
using static VioletFree.Mods.Settings.Settings;
using static VioletFree.Mods.Movement.Movement;
using static VioletFree.Mods.Overpowered.Master;
using static VioletFree.Mods.Overpowered.Overpowered;
using static VioletFree.Mods.Player.Advantage;
using static VioletFree.Mods.Player.Player;
using static VioletFree.Mods.Room.Room;
using static VioletFree.Mods.Spammers.CS;
using static VioletFree.Mods.Spammers.Hoverboard;
using static VioletFree.Mods.Spammers.Projectiles;
using static VioletFree.Mods.Spammers.Water;
using static VioletFree.Mods.Vissual.Fun;
using static VioletFree.Mods.Vissual.Networked;
using static VioletFree.Mods.Stone.StoneBase;
using static VioletFree.Mods.Stone.StoneConfig_Config;
using static VioletFree.Mods.Vissual.Visuals;
using static VioletFree.Utilities.Variables;
using VioletFree.Mods.Stone;

namespace VioletFree.Menu
{
    internal class VioletGUI
    {
        public class VioletGui : MonoBehaviour
        {
            Rect windowRectangle = new Rect(100, 20, 900, 500);
            bool isGuiEnabled = false;

            Texture2D windowBackgroundT2D;
            Texture2D tabAreaT2D;
            Texture2D tabButtonT2D;
            Texture2D tabButtonHoverT2D;
            Texture2D featureToggleT2D;
            Texture2D featureToggleHoverT2D;
            Texture2D featureToggleBarT2D;
            Texture2D featureToggleBoxOffT2D;
            Texture2D featureToggleBoxOnT2D;
            Texture2D watermarkT2D;
            Texture2D shadowT2D;

            Vector2 watermarkVec2;
            Texture2D gradientBorderT2D;
            float gradientTime = 0f;
            GUIStyle tabButtonStyle;
            GUIStyle featureToggleStyle;
            GUIStyle featureDescLabelStyle;

            int selectedTab = 0;
            private Vector2 scrollPosition = Vector2.zero;
            private int lastSelectedTab = -1;

            class Tab
            {
                public string Name;
                public int ID;

                public Tab(string name, int Id)
                {
                    Name = name;
                    ID = Id;
                }
            }
            List<Tab> tabs = new List<Tab>();

            class ButtonInfo
            {
                public string Name;
                public string Description;
                public Action OnClick;
                public int TabID;
                public bool IsRunning = false;
                public Coroutine RunningCoroutine = null;

                public ButtonInfo(string name, string description, Action onClick, int tabID)
                {
                    Name = name;
                    Description = description;
                    OnClick = onClick;
                    TabID = tabID;
                }
            }
            List<ButtonInfo> buttons = new List<ButtonInfo>();

            void Start()
            {
                // Violet-based color scheme
                Color violetBase = new Color32(138, 43, 226, 245); // Vibrant violet, semi-transparent
                Color violetDark = new Color32(106, 13, 173, 255); // Darker violet for tab area
                Color violetLight = new Color32(186, 85, 211, 255); // Lighter violet for hover effects
                Color accentGreen = new Color32(50, 205, 50, 255); // Vibrant green for active toggles
                Color accentBlue = new Color32(70, 130, 180, 255); // Steel blue for gradients and bars

                windowBackgroundT2D = MakeRoundedTexture(900, 500, violetBase, 10);
                shadowT2D = MakeRoundedTexture(900, 500, new Color(0, 0, 0, 0.3f), 10); // Subtle shadow
                tabAreaT2D = MakeRoundedTexture(140, 500, violetDark, 10, leftOnly: true);
                tabButtonT2D = MakeRoundedTexture(130, 35, violetBase, 5);
                tabButtonHoverT2D = MakeRoundedTexture(130, 35, violetLight, 5);
                featureToggleT2D = MakeRoundedTexture(230, 70, violetDark, 8);
                featureToggleHoverT2D = MakeRoundedTexture(230, 70, violetLight, 8);
                featureToggleBarT2D = MakeSolidColorTexture(accentBlue);
                featureToggleBoxOnT2D = MakeRoundedTexture(18, 18, accentGreen, 3);
                featureToggleBoxOffT2D = MakeRoundedTexture(18, 18, new Color32(169, 169, 169, 255), 3); // Light gray
                gradientBorderT2D = CreateGradientTexture(900, 5, GetColorFromTime(gradientTime), GetColorFromTime(gradientTime + 1f));

                // Home Category
                AddTab("Home", 0);
                AddTab("Settings", 1);
                AddTab("Room", 2);
                AddTab("Safety", 3);
                AddTab("Movement", 4);
                AddTab("Player", 5);
                AddTab("Visuals", 6);
                AddTab("Fun", 7);
                AddTab("Projectiles", 8);
                AddTab("Overpowered", 9);
                AddTab("Master", 10);
                AddTab("SoundBoard", 11);
                AddTab("Networking", 12);
                AddTab("Temu Room System [ADMIN]", 13);
                AddTab("Menu Settings", 14);
                AddTab("Movement Settings", 15);
                AddTab("Advantage Settings", 16);
                AddTab("Stone Networking", 17);

                // Settings Category
                AddButton("Menu", "", () => ChangePage(Category.MSettings), 1);
                AddButton("Movement", "", () => ChangePage(Category.MOSettings), 1);
                AddButton("Advantage", "", () => ChangePage(Category.ASettings), 1);

                // Menu Settings Category
                AddButton("Change Menu Theme", "", () => ChangeTheme(), 14);
                AddButton("Change Background Color", "", () => ChangeBackgroundColor(), 14);
                AddButton("Right-Handed Menu", "", () => ToggleRightHandedMenu(true), 14, () => ToggleRightHandedMenu(false));
                AddButton("Toggle Disconnect Button", "", () => toggledisconnectButton = true, 14, () => toggledisconnectButton = false);
                AddButton("Long Menu", "", () => LongMenu(true), 14, () => LongMenu(false));
                AddButton("Wide Menu", "", () => WideMenu(true), 14, () => WideMenu(false));
                AddButton("Outline", "", () => OutlineEnabled = true, 14, () => OutlineEnabled = false);
                AddButton("Toggle Notifications", "", () => toggleNotifications = true, 14, () => toggleNotifications = false);

                // Room Category
                AddButton("Disconnect", "", () => Disconect(), 2);
                AddButton("Join Room Violet", "", () => JoinRoom("violet"), 2);
                AddButton("Join Room Cha554", "", () => JoinRoom("cha554"), 2);
                AddButton("Join Room Mod", "", () => JoinRoom("mod"), 2);
                AddButton("Join Room Mods", "", () => JoinRoom("mods"), 2);

                // Movement Settings Category
                AddButton("Change Fly Speed", "", () => ChangeFlySpeed(), 15);

                // Advantage Settings Category
                AddButton("Change Tag Aura Range", "", () => ChangeTagAuraDistance(), 16);
                AddButton("Change Tag Reach Range", "", () => ChangeTagTouchDistance(), 16);

                // Safety Category
                AddButton("Anti Report", "", () => Saftey.AntiReport(), 3);
                AddButton("Flush RPCs", "", () => Saftey.RemoveRPCS(), 3);

                // Fun Category
                AddButton("Spawn Small Black Hole", "", () => BlackHole.CreateBlackHole(1f, false), 7);
                AddButton("Spawn Medium Black Hole", "", () => BlackHole.CreateBlackHole(3f, false), 7);
                AddButton("Spawn Large Black Hole", "", () => BlackHole.CreateBlackHole(7f, false), 7);
                AddButton("Spawn Massive Black Hole", "", () => BlackHole.CreateBlackHole(20f, false), 7);
                AddButton("Spawn Black Hole V2", "", () => BlackHole.CreateBlackHoleV2(), 7);
                AddButton("Spawn Growing Black Hole", "", () => BlackHole.CreateBlackHole(0.01f, true), 7);

                // Movement Category
                AddButton("WASD Fly", "", () => WASDFly(), 4);
                AddButton("Platforms", "", () => Platforms(), 4);
                AddButton("Sticky Platforms", "", () => StickyPlatforms(), 4);
                AddButton("Invis Platform", "", () => InvisPlatforms(), 4);
                AddButton("Frozone", "", () => Frozone(), 4);
                AddButton("No Clip", "", () => NoClip(), 4);
                AddButton("Fly", "", () => Fly(), 4);
                AddButton("Hand Fly", "", () => HandFly(), 4);
                AddButton("Sling Shot", "", () => SlingShot(), 4);
                AddButton("Trigger Sling Shot", "", () => TriggerSlingShot(), 4);
                AddButton("JetPack", "", () => jetpackmod(), 4);
                AddButton("Up And Down", "", () => UpAndDown(), 4);
                AddButton("Fwd And Back", "", () => ForwardsAndBackwards(), 4);
                AddButton("Left And Right", "", () => LeftAndRight(), 4);
                AddButton("No Clip Fly", "", () => NoclipFly(), 4);
                AddButton("Trigger Fly", "", () => triggerFly(), 4);
                AddButton("Mosa Speed Boost", "", () => MosaSpeedBoost(), 4);
                AddButton("Speed Boost", "", () => SpeedBoost(), 4);
                AddButton("Iron Monke", "", () => IronMonke(), 4);
                AddButton("Toggle Speed Boost", "", () => ToggleSpeedBoost(), 4);
                AddButton("Low Gravity", "", () => LowGravity(), 4);
                AddButton("Toggle Low Gravity", "", () => ToggleLowGravity(), 4);
                AddButton("Zero Gravity", "", () => ZeroGravity(), 4);
                AddButton("High Gravity", "", () => HighGravity(), 4);
                AddButton("TP To Stump", "", () => TPToStump(), 4);
                AddButton("TP To Tutorial", "", () => TPToTutorial(), 4);
                AddButton("Double Jump", "", () => DoubleJump(), 4);
                AddButton("Dash", "", () => Dash(), 4);
                AddButton("Fast Dash", "", () => StrongDash(), 4);
                AddButton("Right Dash", "", () => RightDash(), 4);
                AddButton("Left Dash", "", () => LeftDash(), 4);
                AddButton("Back Dash", "", () => BackDash(), 4);
                AddButton("Hand Dash", "", () => HandDash(), 4);

                // Player Category
                AddButton("Instant Tag All", "", () => TagAll(), 5);
                AddButton("Instant Tag Gun", "", () => TagGun(), 5);
                AddButton("Instant Tag Aura", "", () => TagAura(), 5);
                AddButton("Instant Tag Reach", "", () => TagReach(), 5);
                AddButton("Tag Self", "", () => TagSelf(), 5);
                AddButton("Strobe [Stump]", "", () => Strobe(), 5);
                AddButton("RGB Monke [Stump]", "", () => RGB(), 5);
                AddButton("SpawnBarrel", "", () => SpawnBarrel(), 5);
                AddButton("Copy MoveMent Gun", "", () => CopyMovementGun(), 5);
                AddButton("No Tag On Join", "", () => NoTagOnJoin(), 5);
                AddButton("Ghost", "", () => GhostMonke(), 5);
                AddButton("Invis", "", () => InvisibleMonke(), 5);
                AddButton("Hand Rig", "", () => HandRig(), 5);
                AddButton("Look At Gun", "", () => LookAtGun(), 5);
                AddButton("Look At Closest", "", () => LookAtClosest(), 5);
                AddButton("Max Quest Score", "", () => MaxQuestScore(), 5);
                AddButton("Spaz Quest Score", "", () => SpazQuestScore(), 5);
                AddButton("69 Quest Score", "", () => NiceQuestScore(), 5);
                AddButton("No Name", "", () => NoName(), 5);
                AddButton("Nword Name", "", () => Nword(), 5);
                AddButton("KKK Name", "", () => KKKName(), 5);
                AddButton("Tp Gun", "", () => TPGun(), 5);
                AddButton("Rig Gun", "", () => RigGun(), 5);
                AddButton("Scare Closest", "", () => ScareClosest(), 5);
                AddButton("Scare Gun", "", () => ScareGun(), 5);
                AddButton("Orbit Gun", "", () => OrbitGun(), 5);

                // Visuals Category
                AddButton("Distance ESP", "", () => DistanceESP(), 6);
                AddButton("Name tags", "", () => Nametags(), 6);
                AddButton("Corner ESP", "", () => CornerESP(), 6);
                AddButton("FPS Checker", "", () => FPSESP(), 6);
                AddButton("Headset Checker", "", () => HeadsetChecker(), 6);
                AddButton("Tracers", "", () => Tracers(), 6);
                AddButton("Infection Tracers", "", () => InfectionTracers(), 6);
                AddButton("Prism ESP", "", () => PrismESP(), 6);
                AddButton("Quest Check", "", () => QuestCheck(), 6);
                AddButton("Snake ESP", "", () => SnakeESP(), 6);
                AddButton("2d Box ESP", "", () => BoxESP2(), 6);
                AddButton("3d Box ESP", "", () => BoxESP1(), 6);
                AddButton("Sphere ESP", "", () => SphereESP(), 6);
                AddButton("Spaz Box ESP", "", () => SpazESP(), 6);
                AddButton("Circle Frame ESP", "", () => CircleFrameIDP(), 6);

                // Projectiles Category
                AddButton("Current Projectile Type", "", () => ChangeProjectileType(), 8);
                AddButton("Current Projectile Velocity", "", () => ChangeProjectileSpeed(), 8);
                AddButton("Current Projectile Color", "", () => ChangeProjectileColor(), 8);
                AddButton("Launch Projectile", "", () => LaunchProjectile(), 8);
                AddButton("Grab Projectile", "", () => GrabProjectile(), 8);
                AddButton("Projectile Orbit", "", () => ProjectileOrbit(), 8);
                AddButton("Grab Projectile Item", "", () => GrabProjectileItem(), 8);
                AddButton("Projectile Item Orbit", "", () => ProjectileItemOrbit(), 8);
                AddButton("Pee", "", () => Pee(), 8);
                AddButton("Cum", "", () => Projectiles.Cum(), 8);
                AddButton("Period Blood", "", () => PeriodBlood(), 8);
                AddButton("Shit", "", () => Shit(), 8);
                AddButton("Wet Shit", "", () => WetShit(), 8);
                AddButton("Splash Hands", "", () => SplashHands(), 8);
                AddButton("Splash Orbit", "", () => SplashOrbit(), 8);
                AddButton("Splash Body", "", () => SplashBody(), 8);
                AddButton("Cum (Water)", "", () => Water.Cum(), 8);
                AddButton("Splash Aura", "", () => SplashAura(), 8);
                AddButton("Tiny Splash Hands", "", () => TinySplashHands(), 8);
                AddButton("Tiny Splash Orbit", "", () => TinySplashOrbit(), 8);
                AddButton("Tiny Splash Body", "", () => TinySplashBody(), 8);
                AddButton("Tiny Cum", "", () => TinyCum(), 8);
                AddButton("Tiny Splash Aura", "", () => TinySplashAura(), 8);
                AddButton("Mixed Splash Aura", "", () => MixedSplashAura(), 8);

                // Overpowered Category
                AddButton("Current Lag Power", "", () => ChangeLagPower(), 9);
                AddButton("Lag Gun", "", () => Lag(1), 9);
                AddButton("Lag All", "", () => Lag(0), 9);
                AddButton("Lag On Touch", "", () => Lag(2), 9);
                AddButton("Lag On They Touch", "", () => Lag(3), 9);
                AddButton("Lag Aura", "", () => Lag(4), 9);
                AddButton("Lag On They Aura", "", () => Lag(5), 9);
                AddButton("Destroy Gun", "", () => DestroyGun(), 9);
                AddButton("Destroy All", "", () => DestroyAll(), 9);
                AddButton("Spaz Ropes", "", () => SpazRopes(), 9);
                AddButton("Up Ropes", "", () => UpRopes(), 9);
                AddButton("Kick All In Stump", "", () => KickAllInStump(), 9);
                AddButton("Quit App All [GUARDIAN]", "", () => QuitAppAll(), 9);
                AddButton("Quit App Gun [GUARDIAN]", "", () => QuitAppGun(), 9);
                AddButton("Grab All [GUARDIAN]", "", () => GrabAll(), 9);
                AddButton("Grab Gun [GUARDIAN]", "", () => GrabGun(), 9);
                AddButton("RGB Board", "", () => RGBBoard(), 9);
                AddButton("Launch HoverBoard", "", () => HoverBoardLauncher(), 9);
                AddButton("Orbit HoverBoard", "", () => OrbitHoverBoard(), 9);
                AddButton("Elevator Kick Gun [M]", "", () => ElevatorTpGun(), 9);
                AddButton("Elevator Kick All [M]", "", () => ElevatorTpAll(), 9);

                // Master Category
                AddButton("Rock Monke Gamemode [SLOW]", "", () => RockMonkeGamemode(), 10);
                AddButton("Lava Monkey Gamemode [SLOW]", "", () => InfectionMonkeGamemode(), 10);
                AddButton("Slow Gun", "", () => SlowGun(), 10);
                AddButton("Slow All", "", () => SlowAll(), 10);
                AddButton("Vibrate Gun", "", () => SlowGun(), 10);
                AddButton("Vibrate All", "", () => SlowAll(), 10);
                AddButton("Rose Toy Self", "", () => RoseToySelf(), 10);
                AddButton("Tag All", "", () => TagAllMaster(), 10);
                AddButton("Tag Gun", "", () => TagGunMaster(), 10);
                AddButton("Tag Touch", "", () => TagReachMaster(), 10);
                AddButton("Tag Aura", "", () => TagAuraMaster(), 10);
                AddButton("Tag Self", "", () => TagSelfMaster(), 10);
                AddButton("Untag All", "", () => UnTagAllMaster(), 10);
                AddButton("Untag Gun", "", () => UnTagGunMaster(), 10);
                AddButton("Untag Touch", "", () => UnTagReachMaster(), 10);
                AddButton("Untag Aura", "", () => UnTagAuraMaster(), 10);
                AddButton("Untag Self", "", () => UnTagSelfMaster(), 10);
                AddButton("Stop Gamemode", "", () => StopGamemode(), 10);
                AddButton("Start Gamemode", "", () => StopGamemode(), 10);
                AddButton("Restart Gamemode", "", () => RestartGamemode(), 10);
                AddButton("Spaz Gamemode", "", () => SpazGamemode(), 10);

                // SoundBoard Category
                AddButton("Play Alarm", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/alarm!!!-made-with-Voicemod.mp3", "alarm!!!-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play Amogus", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/amogus-made-with-Voicemod.mp3", "amogus-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play Augh", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/augh-made-with-Voicemod.mp3", "augh-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play Ben No", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/ben-no-made-with-Voicemod.mp3", "ben-no-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play Bruh", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/bruh-made-with-Voicemod.mp3", "bruh-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play Chipi Chipi Chapa Chapa", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/chipi-chipi-chapa-chapa-made-with-Voicemod.mp3", "chipi-chipi-chapa-chapa-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play Clapping", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/clapping-made-with-Voicemod.mp3", "clapping-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play Czy To Freddy Fazbear", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/czy-to-freddy-fazbear-made-with-Voicemod.mp3", "czy-to-freddy-fazbear-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play Discord In", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/discord-in-made-with-Voicemod.mp3", "discord-in-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play Emotional Damage", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/emotional-damage-made-with-Voicemod.mp3", "emotional-damage-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play Erm What The Sigma", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/erm-what-the-sigma-made-with-Voicemod.mp3", "erm-what-the-sigma-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play Fart", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/fart-made-with-Voicemod.mp3", "fart-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play FBI Open Up", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/fbi-open-up!-(sound-effect)-made-with-Voicemod.mp3", "fbi-open-up!-(sound-effect)-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play Get Out", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/get-out-made-with-Voicemod.mp3", "get-out-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play Giga Chad", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/giga-chad-made-with-Voicemod.mp3", "giga-chad-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play Hawk Tuah", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/hawk-tuah_SRaUp2L.mp3", "hawk-tuah_SRaUp2L.mp3"), false), 11);
                AddButton("Play Holy Moly", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/holy-moly-made-with-Voicemod.mp3", "holy-moly-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play Lego Yoda Death", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/lego-yoda-death-made-with-Voicemod.mp3", "lego-yoda-death-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play Mario Jump", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/mario-jump-made-with-Voicemod.mp3", "mario-jump-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play Metal Pipe Falling", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/metal-pipe-falling-sound-effect-made-with-Voicemod.mp3", "metal-pipe-falling-sound-effect-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play Monkey", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/monkey-made-with-Voicemod.mp3", "monkey-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play Oi Oi Oi", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/oi-oi-oi-made-with-Voicemod.mp3", "oi-oi-oi-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play Rick Roll", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/rick-roll-made-with-Voicemod.mp3", "rick-roll-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play Rizz Sound Effect", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/rizz-made-with-Voicemod.mp3", "rizz-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play Roblox Bye", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/roblox-bye-made-with-Voicemod.mp3", "roblox-bye-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play Spongebob", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/spongebob-made-with-Voicemod.mp3", "spongebob-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play Sus", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/sus-made-with-Voicemod.mp3", "sus-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play Taco Bell Bell", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/taco-bell-bell-made-with-Voicemod.mp3", "taco-bell-bell-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play Two Hours Later", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/two-hours-later-made-with-Voicemod.mp3", "two-hours-later-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play Uhh No", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/uhh-no-made-with-Voicemod.mp3", "uhh-no-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play Uwu", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/uwu-made-with-Voicemod.mp3", "uwu-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play Vine Boom", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/vine-boom-made-with-Voicemod.mp3", "vine-boom-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play What The Sigma", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/what-the-sigma-made-with-Voicemod.mp3", "what-the-sigma-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play Why Are You Gay", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/why-are-you-gay-made-with-Voicemod.mp3", "why-are-you-gay-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play Womp Womp", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/womp-womp-made-with-Voicemod.mp3", "womp-womp-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play Yipee", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/yipee-made-with-Voicemod.mp3", "yipee-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play daisy-bell-slowed", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/GhostSounds/main/daisy-bell-slowed-made-with-Voicemod.mp3", "daisy-bell-slowed-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play daisy09-gorilla-tag", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/GhostSounds/main/daisy09-gorilla-tag-made-with-Voicemod.mp3", "daisy09-gorilla-tag-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play distorted-run-rabbit-run", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/GhostSounds/main/distorted-run-rabbit-run-made-with-Voicemod.mp3", "distorted-run-rabbit-run-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play footsteps-sound-effect", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/GhostSounds/main/footsteps-sound-effect-made-with-Voicemod.mp3", "footsteps-sound-effect-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play j3vu-message", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/GhostSounds/main/j3vu-message-made-with-Voicemod.mp3", "j3vu-message-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play pbbv-warningbot", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/GhostSounds/main/pbbv-warningbot-made-with-Voicemod.mp3", "pbbv-warningbot-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play run-rabbit-run", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/GhostSounds/main/run-rabbit-run-made-with-Voicemod.mp3", "run-rabbit-run-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play run-run-run", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/GhostSounds/main/run-run-run-made-with-Voicemod.mp3", "run-run-run-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play t774-bells", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/GhostSounds/main/t774-bells-made-with-Voicemod.mp3", "t774-bells-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play t774-speech-1", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/GhostSounds/main/t774-speech-made-with-Voicemod (1).mp3", "t774-speech-made-with-Voicemod (1).mp3"), false), 11);
                AddButton("Play t774-speech-2", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/GhostSounds/main/t774-speech-made-with-Voicemod.mp3", "t774-speech-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play tip-toe-warning-bot", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/GhostSounds/main/tip-toe-warning-bot-made-with-Voicemod.mp3", "tip-toe-warning-bot-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play virus-twin1-sound", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/GhostSounds/main/virus-twin1-sound-made-with-Voicemod.mp3", "virus-twin1-sound-made-with-Voicemod.mp3"), false), 11);
                AddButton("Play wi-crash-sound", "", () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/GhostSounds/main/wi-crash-sound-(better-&-louder)-made-with-Voicemod.mp3", "wi-crash-sound-(better-&-louder)-made-with-Voicemod.mp3"), false), 11);

                // Networking Category
                AddButton("Launch SlingShot Networked [BROKEN] [CS]", "", () => Networked.LaunchSlingShotNetworked(), 12);
                AddButton("Launch WaterBalloons [BROKEN] [CS]", "", () => Networked.WaterBaloons(), 12);
                AddButton("Launch IceCream [BROKEN] [CS]", "", () => Networked.IceCream(), 12);
                AddButton("Launch DeadShot [BROKEN] [CS]", "", () => Networked.DeadShot(), 12);
                AddButton("Launch Present V1 [BROKEN] [CS]", "", () => Networked.PresentV1(), 12);
                AddButton("Launch Firework [BROKEN] [CS]", "", () => Networked.Firework(), 12);
                AddButton("Launch Cloud Slingshot [BROKEN] [CS]", "", () => Networked.CloundSlingshot(), 12);
                AddButton("Launch InVisible [BROKEN] [CS]", "", () => Networked.InVisible(), 12);
                AddButton("Launch Firework V2 [BROKEN] [CS]", "", () => Networked.FireWorkV2(), 12);
                AddButton("Launch Heart Slingshot [BROKEN] [CS]", "", () => Networked.HeartSlingShot(), 12);
                AddButton("Launch FishFood [BROKEN] [CS]", "", () => Networked.FishFood(), 12);
                AddButton("Launch Snowball [BROKEN] [CS]", "", () => Networked.Snowball(), 12);

                // Stone Networking Category
                AddButton("Vibrate Gun", "", () => StoneConfig_Config.GunEvent("Vibrate"), 17);
                AddButton("Vibrate All", "", () => StoneConfig_Config.EventAll("Vibrate"), 17);
                AddButton("Slow Gun", "", () => StoneConfig_Config.GunEvent("Slow"), 17);
                AddButton("Slow All", "", () => StoneConfig_Config.EventAll("Slow"), 17);
                AddButton("Kick Gun", "", () => StoneConfig_Config.GunEvent("Kick"), 17);
                AddButton("Kick All", "", () => StoneConfig_Config.EventAll("Kick"), 17);
                AddButton("Fling Gun", "", () => StoneConfig_Config.GunEvent("Fling"), 17);
                AddButton("Fling All", "", () => StoneConfig_Config.EventAll("Fling"), 17);
                AddButton("Stutter Gun", "", () => StoneConfig_Config.GunEvent("Stutter"), 17);
                AddButton("Stutter All", "", () => StoneConfig_Config.EventAll("Stutter"), 17);
                AddButton("Bring Gun", "", () => StoneConfig_Config.GunEvent("Bring"), 17);
                AddButton("Bring All", "", () => StoneConfig_Config.EventAll("Bring"), 17);
                AddButton("BreakMovement Gun", "", () => StoneConfig_Config.GunEvent("BreakMovemet"), 17);
                AddButton("BreakMovement All", "", () => StoneConfig_Config.EventAll("BreakMovemet"), 17);
                AddButton("Message Gun", "", () => StoneConfig_Config.GunEvent("Message"), 17);
                AddButton("Message All", "", () => StoneConfig_Config.EventAll("Message"), 17);
                AddButton("Grab", "", () => StoneConfig_Config.Grab(), 17);
                AddButton("ScaleDown Gun", "", () => StoneConfig_Config.GunEvent("ScaleDown"), 17);
                AddButton("ScaleDown All", "", () => StoneConfig_Config.EventAll("ScaleDown"), 17);
                AddButton("ScaleUp Gun", "", () => StoneConfig_Config.GunEvent("ScaleUp"), 17);
                AddButton("ScaleUp All", "", () => StoneConfig_Config.EventAll("ScaleUp"), 17);
                AddButton("ScaleReset Gun", "", () => StoneConfig_Config.GunEvent("ScaleReset"), 17);
                AddButton("ScaleReset All", "", () => StoneConfig_Config.EventAll("ScaleReset"), 17);
                AddButton("Low Gravity Gun", "", () => StoneConfig_Config.GunEvent("LowGrav"), 17);
                AddButton("Low Gravity All", "", () => StoneConfig_Config.EventAll("LowGrav"), 17);
                AddButton("No Gravity Gun", "", () => StoneConfig_Config.GunEvent("NoGrav"), 17);
                AddButton("No Gravity All", "", () => StoneConfig_Config.EventAll("NoGrav"), 17);
                AddButton("High Gravity Gun", "", () => StoneConfig_Config.GunEvent("HighGrav"), 17);
                AddButton("High Gravity All", "", () => StoneConfig_Config.EventAll("HighGrav"), 17);
            }

            void AddTab(string name, int ID) => tabs.Add(new Tab(name, ID));

            void AddButton(string name, string description, Action onClick, int tabID, Action offClick = null)
            {
                var button = new ButtonInfo(name, description, onClick, tabID);
                if (offClick != null)
                {
                    button.OnClick = () =>
                    {
                        if (!button.IsRunning)
                            onClick?.Invoke();
                        else
                            offClick?.Invoke();
                    };
                }
                buttons.Add(button);
            }

            void DrawButton(Rect position, ButtonInfo buttonInfo)
            {
                featureToggleStyle.normal.textColor = buttonInfo.IsRunning ? new Color32(50, 205, 50, 255) : new Color32(255, 105, 180, 255); // Green when enabled, hot pink when disabled
                featureToggleStyle.hover.textColor = Color.white;
                if (GUI.Button(position, buttonInfo.Name, featureToggleStyle))
                {
                    buttonInfo.IsRunning = !buttonInfo.IsRunning;

                    if (buttonInfo.IsRunning)
                    {
                        if (buttonInfo.RunningCoroutine == null)
                            buttonInfo.RunningCoroutine = StartCoroutine(ButtonActionLoop(buttonInfo));
                    }
                    else
                    {
                        if (buttonInfo.RunningCoroutine != null)
                        {
                            StopCoroutine(buttonInfo.RunningCoroutine);
                            buttonInfo.RunningCoroutine = null;
                        }
                    }
                }

                GUI.DrawTexture(new Rect(position.x, position.y + 30, position.width, 2), featureToggleBarT2D);
                GUI.DrawTexture(new Rect(position.x + 8, position.y + 34, 20, 20), buttonInfo.IsRunning ? featureToggleBoxOnT2D : featureToggleBoxOffT2D);

                GUI.Label(new Rect(position.x + 34, position.y + 34, 100, 24), "Toggle", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.UpperLeft, fontSize = 12, normal = { textColor = new Color32(200, 200, 200, 255) } });
                GUI.Label(new Rect(position.x + 8, position.y + 5, position.width - 10, position.height - 10), buttonInfo.Description, featureDescLabelStyle);
            }

            IEnumerator ButtonActionLoop(ButtonInfo buttonInfo)
            {
                while (buttonInfo.IsRunning)
                {
                    buttonInfo.OnClick?.Invoke();
                    yield return null;
                }
            }

            void MainGUI(int windowID)
            {
                tabButtonStyle = new GUIStyle(GUI.skin.button)
                {
                    fontStyle = FontStyle.Bold,
                    fontSize = 16,
                    normal = { background = tabButtonT2D, textColor = Color.white },
                    hover = { background = tabButtonHoverT2D, textColor = Color.white },
                    active = { background = tabButtonT2D },
                    alignment = TextAnchor.MiddleCenter
                };
                GUI.DrawTexture(new Rect(0, 0, windowRectangle.width, 5), gradientBorderT2D);
                featureToggleStyle = new GUIStyle(GUI.skin.button)
                {
                    fontStyle = FontStyle.Bold,
                    fontSize = 16,
                    normal = { background = featureToggleT2D, textColor = Color.white },
                    hover = { background = featureToggleHoverT2D },
                    active = { background = featureToggleT2D },
                    alignment = TextAnchor.UpperLeft,
                    padding = new RectOffset(10, 10, 5, 5)
                };

                featureDescLabelStyle = new GUIStyle(GUI.skin.label)
                {
                    alignment = TextAnchor.LowerLeft,
                    fontSize = 12,
                    normal = { textColor = new Color32(200, 200, 200, 255) }
                };

                GUI.DrawTexture(new Rect(0, 0, 140, 500), tabAreaT2D);
                GUI.Label(new Rect(0, 10, 140, 40), "Violet", new GUIStyle(GUI.skin.label) { fontSize = 24, alignment = TextAnchor.UpperCenter, normal = { textColor = Color.white } });

                for (int i = 0; i < tabs.Count; i++)
                {
                    if (GUI.Button(new Rect(5, 60 + i * 40, 130, 35), tabs[i].Name, tabButtonStyle))
                        selectedTab = i;
                }

                if (selectedTab != lastSelectedTab)
                {
                    scrollPosition.y = 0;
                    lastSelectedTab = selectedTab;
                }

                int buttonCount = buttons.Count(b => b.TabID == selectedTab);
                int rows = (buttonCount + 2) / 3;
                float contentHeight = 20 + rows * 80 + 20;

                scrollPosition = GUI.BeginScrollView(new Rect(140, 0, 760, 500), scrollPosition, new Rect(0, 0, 740, contentHeight));

                int buttonIndex = 0;
                foreach (var button in buttons.Where(b => b.TabID == selectedTab))
                {
                    DrawButton(new Rect(20 + (buttonIndex % 3) * 245, 20 + (buttonIndex / 3) * 80, 230, 70), button);
                    buttonIndex++;
                }

                GUI.EndScrollView();

                GUI.DrawTexture(new Rect(0, 450, 140, 3), featureToggleBarT2D);

                GUI.DragWindow();
            }

            void Update()
            {
                gradientTime += Time.deltaTime;
                gradientBorderT2D = CreateGradientTexture(900, 5, GetColorFromTime(gradientTime), GetColorFromTime(gradientTime + 1f));

                watermarkT2D = MakeRoundedTexture((int)watermarkVec2.x * 2, (int)watermarkVec2.y * 2, new Color(0, 0, 0, 0.5f), 5, bottomOnly: true);

                if (Keyboard.current.rightShiftKey.wasReleasedThisFrame)
                    isGuiEnabled = !isGuiEnabled;
            }

            void OnGUI()
            {
                GUIStyle windowStyle = new GUIStyle(GUI.skin.window)
                {
                    fontStyle = FontStyle.Bold,
                    normal = { background = windowBackgroundT2D },
                    onNormal = { background = windowBackgroundT2D },
                    onActive = { background = windowBackgroundT2D },
                };

               
                Color color = Color.Lerp(new Color(0.54f, 0.17f, 0.89f), new Color(0.73f, 0.33f, 0.83f), Mathf.PingPong(Time.time * 0.5f, 1)); 
                GUI.color = color;

                GUI.DrawTexture(new Rect(windowRectangle.x + 5, windowRectangle.y + 5, windowRectangle.width, windowRectangle.height), shadowT2D);
                windowRectangle = GUI.Window(1684115495, windowRectangle, MainGUI, "", windowStyle);
            }

            static Texture2D MakeSolidColorTexture(Color color, int width = 1, int height = 1)
            {
                Texture2D texture = new Texture2D(width, height);
                Color[] pixels = new Color[width * height];
                for (int i = 0; i < pixels.Length; i++) pixels[i] = color;
                texture.SetPixels(pixels);
                texture.Apply();
                return texture;
            }

            Color GetColorFromTime(float time)
            {
                float r = Mathf.PingPong(time * 0.15f, 0.5f) + 0.4f;
                float g = Mathf.PingPong(time * 0.15f + 0.3f, 0.3f) + 0.1f;
                float b = Mathf.PingPong(time * 0.15f + 0.6f, 0.5f) + 0.4f;
                return new Color(r, g, b);
            }

            static Texture2D CreateGradientTexture(int width, int height, Color startColor, Color endColor)
            {
                Texture2D texture = new Texture2D(width, height);
                for (int x = 0; x < width; x++)
                {
                    Color color = Color.Lerp(startColor, endColor, (float)x / width);
                    for (int y = 0; y < height; y++)
                    {
                        texture.SetPixel(x, y, color);
                    }
                }
                texture.Apply();
                return texture;
            }

            static Texture2D MakeRoundedTexture(int width, int height, Color color, int borderRadius, bool leftOnly = false, bool bottomOnly = false)
            {
                Texture2D texture = new Texture2D(width, height);
                Color[] pixels = new Color[width * height];
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        bool isCorner;

                        if (leftOnly)
                        {
                            isCorner = (x < borderRadius && y < borderRadius && (x - borderRadius) * (x - borderRadius) + (y - borderRadius) * (y - borderRadius) > borderRadius * borderRadius) ||
                                       (x < borderRadius && y > height - borderRadius - 1 && (x - borderRadius) * (x - borderRadius) + (y - (height - borderRadius - 1)) * (y - (height - borderRadius - 1)) > borderRadius * borderRadius);
                        }
                        else if (bottomOnly)
                        {
                            isCorner = (x < borderRadius && y < borderRadius && (x - borderRadius) * (x - borderRadius) + (y - borderRadius) * (y - borderRadius) > borderRadius * borderRadius) ||
                                       (x > width - borderRadius - 1 && y < borderRadius && (x - (width - borderRadius - 1)) * (x - (width - borderRadius - 1)) + (y - borderRadius) * (y - borderRadius) > borderRadius * borderRadius);
                        }
                        else
                        {
                            isCorner = (x < borderRadius && y < borderRadius && (x - borderRadius) * (x - borderRadius) + (y - borderRadius) * (y - borderRadius) > borderRadius * borderRadius) ||
                                       (x > width - borderRadius - 1 && y < borderRadius && (x - (width - borderRadius - 1)) * (x - (width - borderRadius - 1)) + (y - borderRadius) * (y - borderRadius) > borderRadius * borderRadius) ||
                                       (x < borderRadius && y > height - borderRadius - 1 && (x - borderRadius) * (x - borderRadius) + (y - (height - borderRadius - 1)) * (y - (height - borderRadius - 1)) > borderRadius * borderRadius) ||
                                       (x > width - borderRadius - 1 && y > height - borderRadius - 1 && (x - (width - borderRadius - 1)) * (x - (width - borderRadius - 1)) + (y - (height - borderRadius - 1)) * (y - (height - borderRadius - 1)) > borderRadius * borderRadius);
                        }

                        pixels[x + y * width] = isCorner ? new Color(0, 0, 0, 0) : color;
                    }
                }

                texture.SetPixels(pixels);
                texture.Apply();
                return texture;
            }
        }
    }
}*/

using BepInEx;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using VioletFree.Menu;
using VioletFree.Utilities;

namespace VioletFree.Menu
{
    internal class Gui : MonoBehaviour
    {
        public static void ToggleArrayList(bool show)
        {
            ArrayListShown = show;
        }

        public void Update()
        {
            if (UnityInput.Current.GetKeyDown(KeyCode.Insert))
            {
                GUIShown = !GUIShown;
            }
        }

        public void OnGUI()
        {
            buttonTexture = CreateTexture(buttonColor);
            buttonHoverTexture = CreateTexture(buttonHoverColor);
            buttonClickTexture = CreateTexture(buttonEnabledColor);
            guiBackgroundTexture = CreateTexture(guiBackGroundColor);
            containerTexture = CreateTexture(containerColor);
            versionTexture = CreateTexture(containerColor);
            timeTexture = CreateTexture(containerColor);
            updatesTexture = CreateTexture(containerColor);
            arrayListTexture = CreateTexture(ColorLib.DeepVioletTransparent, 17, 17);

            GUIStyle labelStyle = new GUIStyle(GUI.skin.box)
            {
                fontSize = 32,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                wordWrap = false,
                normal = { background = arrayListTexture, textColor = colorMaterial.color }
            };

            string menuText = string.Concat(
                NameOfMenu,
                " v",
                VersionOfMenu,
                " | FPS : ",
                Mathf.Ceil(1f / Time.unscaledDeltaTime).ToString()
            );
            GUI.Label(
                new Rect(1100f, 10f, labelStyle.CalcSize(new GUIContent(menuText)).x + 13.5f, 40.5f),
                menuText,
                labelStyle
            );

            if (ArrayListShown)
            {
                IOrderedEnumerable<ButtonHandler.Button> orderedButtons = from b in ModButtons.buttons
                                                                          orderby b.buttonText.Length descending
                                                                          select b;
                GUIStyle buttonLabelStyle = new GUIStyle(GUI.skin.box)
                {
                    fontSize = 25,
                    fontStyle = FontStyle.Bold,
                    alignment = TextAnchor.MiddleCenter,

                    wordWrap = false,
                    normal = { background = arrayListTexture, textColor = Color.white }
                };

                foreach (ButtonHandler.Button button in orderedButtons)
                {
                    if (button.Enabled)
                    {
                        GUILayout.BeginHorizontal();
                        GUILayout.Space(5.5f);
                        GUILayout.BeginVertical();
                        GUILayout.Space(5.5f);
                        GUILayout.Label(
                            button.buttonText,
                            buttonLabelStyle,
                            GUILayout.Width(buttonLabelStyle.CalcSize(new GUIContent(button.buttonText)).x + 23.5f),
                            GUILayout.Height(33.5f)
                        );
                        GUILayout.EndVertical();
                        GUILayout.EndHorizontal();
                    }
                }
            }
        }

    
        private GUIStyle CreateLabelStyle(Color color, int fontSize, FontStyle fontStyle, TextAnchor textAnchor)
        {
            return new GUIStyle(GUI.skin.label)
            {
                normal = { textColor = color },
                fontSize = fontSize,
                fontStyle = fontStyle,
                alignment = textAnchor
            };
        }

        public static void SetTextures()
        {
            if (!TexturesSet)
            {
                GUI.skin.label.richText = true;
                GUI.skin.button.richText = true;
                GUI.skin.window.richText = true;
                GUI.skin.textField.richText = true;
                GUI.skin.box.richText = true;

                GUI.skin.window.border = new RectOffset(5, 5, 5, 5);
                GUI.skin.window.active.background = null;
                GUI.skin.window.normal.background = null;
                GUI.skin.window.hover.background = null;
                GUI.skin.window.focused.background = null;
                GUI.skin.window.onFocused.background = null;
                GUI.skin.window.onActive.background = null;
                GUI.skin.window.onHover.background = null;
                GUI.skin.window.onNormal.background = null;

                GUI.skin.button.active.background = buttonClickTexture;
                GUI.skin.button.normal.background = buttonHoverTexture;
                GUI.skin.button.hover.background = buttonTexture;
                GUI.skin.button.onActive.background = buttonClickTexture;
                GUI.skin.button.onHover.background = buttonHoverTexture;
                GUI.skin.button.onNormal.background = buttonTexture;

                GUI.skin.verticalScrollbarThumb.active.background = containerTexture;
                GUI.skin.verticalScrollbarThumb.normal.background = containerTexture;
                GUI.skin.verticalScrollbarThumb.hover.background = containerTexture;
                GUI.skin.verticalScrollbarThumb.focused.background = containerTexture;
                GUI.skin.verticalScrollbarThumb.onFocused.background = containerTexture;
                GUI.skin.verticalScrollbarThumb.onActive.background = containerTexture;
                GUI.skin.verticalScrollbarThumb.onHover.background = containerTexture;
                GUI.skin.verticalScrollbarThumb.onNormal.background = containerTexture;

                GUI.skin.horizontalScrollbar.active.background = guiBackgroundTexture;
                GUI.skin.horizontalScrollbar.normal.background = guiBackgroundTexture;
                GUI.skin.horizontalScrollbar.hover.background = guiBackgroundTexture;
                GUI.skin.horizontalScrollbar.focused.background = guiBackgroundTexture;
                GUI.skin.horizontalScrollbar.onFocused.background = guiBackgroundTexture;
                GUI.skin.horizontalScrollbar.onActive.background = guiBackgroundTexture;
                GUI.skin.horizontalScrollbar.onHover.background = guiBackgroundTexture;
                GUI.skin.horizontalScrollbar.onNormal.background = guiBackgroundTexture;

                GUI.skin.horizontalSlider.active.background = guiBackgroundTexture;
                GUI.skin.horizontalSlider.normal.background = guiBackgroundTexture;
                GUI.skin.horizontalSlider.hover.background = guiBackgroundTexture;
                GUI.skin.horizontalSlider.focused.background = guiBackgroundTexture;
                GUI.skin.horizontalSlider.onFocused.background = guiBackgroundTexture;
                GUI.skin.horizontalSlider.onActive.background = guiBackgroundTexture;
                GUI.skin.horizontalSlider.onHover.background = guiBackgroundTexture;
                GUI.skin.horizontalSlider.onNormal.background = guiBackgroundTexture;

                GUI.skin.horizontalSliderThumb.active.background = guiBackgroundTexture;
                GUI.skin.horizontalSliderThumb.normal.background = guiBackgroundTexture;
                GUI.skin.horizontalSliderThumb.hover.background = guiBackgroundTexture;
                GUI.skin.horizontalSliderThumb.focused.background = guiBackgroundTexture;
                GUI.skin.horizontalSliderThumb.onFocused.background = guiBackgroundTexture;
                GUI.skin.horizontalSliderThumb.onActive.background = guiBackgroundTexture;
                GUI.skin.horizontalSliderThumb.onHover.background = guiBackgroundTexture;
                GUI.skin.horizontalSliderThumb.onNormal.background = guiBackgroundTexture;

                GUI.skin.horizontalScrollbarThumb.active.background = guiBackgroundTexture;
                GUI.skin.horizontalScrollbarThumb.normal.background = guiBackgroundTexture;
                GUI.skin.horizontalScrollbarThumb.hover.background = guiBackgroundTexture;
                GUI.skin.horizontalScrollbarThumb.focused.background = guiBackgroundTexture;
                GUI.skin.horizontalScrollbarThumb.onFocused.background = guiBackgroundTexture;
                GUI.skin.horizontalScrollbarThumb.onActive.background = guiBackgroundTexture;
                GUI.skin.horizontalScrollbarThumb.onHover.background = guiBackgroundTexture;
                GUI.skin.horizontalScrollbarThumb.onNormal.background = guiBackgroundTexture;

                GUI.skin.verticalScrollbar.border = new RectOffset(0, 0, 0, 0);
                GUI.skin.verticalScrollbar.fixedWidth = 0f;
                GUI.skin.verticalScrollbar.fixedHeight = 0f;
                GUI.skin.verticalScrollbarThumb.fixedHeight = 0f;
                GUI.skin.verticalScrollbarThumb.fixedWidth = 3f;

                GUI.skin.horizontalScrollbar.border = new RectOffset(0, 0, 0, 0);
                GUI.skin.horizontalScrollbar.fixedWidth = 0f;
                GUI.skin.horizontalScrollbar.fixedHeight = 0f;
                GUI.skin.horizontalScrollbarThumb.fixedHeight = 0f;
                GUI.skin.horizontalScrollbarThumb.fixedWidth = 3f;

                TexturesSet = true;
            }
        }

        public static Texture2D CreateTexture(Color color, int width = 30, int height = 30)
        {
            Texture2D texture = new Texture2D(width, height);
            Color[] pixels = new Color[width * height];
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = color;
            }
            texture.SetPixels(pixels);
            texture.Apply();
            return texture;
        }


        public static void DoTexture(Rect rect, Texture2D texture, float borderRadius)
        {
            GUI.DrawTexture(
                rect,
                texture,
                ScaleMode.StretchToFill,
                false,
                0f,
                GUI.color,
                Vector4.zero,
                new Vector4(0f, 0f, 0f, 0f)
            );
        }

        public Rect guiRect = new Rect(400f, 200f, 900f, 550f);
        public static bool ArrayListShown = true;
        public static bool GUIShown = true;
        public static bool TexturesSet = false;
        public static string NameOfMenu = "Violet Free";
        public static string VersionOfMenu = string.Format("{0}", "7.0"[0]);
        public Color32 buttonColor = ColorLib.DarkGrey;
        public Color32 buttonHoverColor = new Color32(48, 48, 48, byte.MaxValue);
        public Color32 buttonEnabledColor = new Color32(35, 35, 35, byte.MaxValue);
        public Color32 guiBackGroundColor = new Color32(18, 18, 18, byte.MaxValue);
        public Color32 containerColor = ColorLib.Violet;
        public Vector2 Scrolling = Vector2.zero;
        public static Texture2D buttonTexture = new Texture2D(2, 2);
        public static Texture2D buttonHoverTexture = new Texture2D(2, 2);
        public static Texture2D buttonClickTexture = new Texture2D(2, 2);
        public static Texture2D guiBackgroundTexture = new Texture2D(2, 2);
        public static Texture2D containerTexture = new Texture2D(2, 2);
        public static Texture2D arrayListTexture = new Texture2D(2, 2);
        public static Texture2D versionTexture = new Texture2D(2, 2);
        public static Texture2D timeTexture = new Texture2D(2, 2);
        public static Texture2D updatesTexture = new Texture2D(2, 2);
        public static Rect pageButtonRect;
        public static Material colorMaterial = new Material(Shader.Find("GUI/Text Shader"))
        {
            color = Color.Lerp(Color.gray * 1.3f, Color.grey * 1.6f, Mathf.PingPong(Time.time, 1.5f))
        };
    }
}