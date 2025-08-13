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
    public enum Category
    {
        Home,
        Settings,
        MOSettings,
        MSettings,
        PSettings,
        ASettings,
        Saftey,
        Room,
        Player,
        Movement,
        World,
        Visuals,
        CS,
        Projectiles,
        Overpowered,
        Master,
        SoundBoard,
        Networking,
        StoneNetworking

    }

    public class ModButtons
    {
        public static Button[] buttons =
        {
            // Home Category
            new Button("Settings", Category.Home, false, false, () => ChangePage(Category.Settings)),
            new Button("Room", Category.Home, false, false, () => ChangePage(Category.Room)),
            new Button("Saftey", Category.Home, false, false, () => ChangePage(Category.Saftey)),
            new Button("Movement", Category.Home, false, false, () => ChangePage(Category.Movement)),
            new Button("Player", Category.Home, false, false, () => ChangePage(Category.Player)),
            new Button("Visuals", Category.Home, false, false, () => ChangePage(Category.Visuals)),
            new Button("Fun", Category.Home, false, false, () => ChangePage(Category.CS)),
            new Button("Projectiles", Category.Home, false, false, () => ChangePage(Category.Projectiles)),
            new Button("Overpowered", Category.Home, false, false, () => ChangePage(Category.Overpowered)),
            new Button("Master", Category.Home, false, false, () => ChangePage(Category.Master)),
            new Button("SoundBoard", Category.Home, false, false, () => ChangePage(Category.SoundBoard)),
            new Button("Networking", Category.Home, false, false, () => ChangePage(Category.Networking)),
            new Button("Temu Room System [ADMIN]", Category.Home, false, false, () => StoneBase.Access()),


            // Settings Category
            new Button("Menu", Category.Settings, false, false, () => ChangePage(Category.MSettings)),
            new Button("Movement", Category.Settings, false, false, () => ChangePage(Category.MOSettings)),
            new Button("Advantage", Category.Settings, false, false, () => ChangePage(Category.ASettings)),


            //Menu Settings Category
            new Button("Change Menu Theme", Category.MSettings, false, false, () => ChangeTheme()),
            new Button("Change Background Color", Category.MSettings, false, false, () => ChangeBackgroundColor()),
            new Button("Right-Handed Menu", Category.MSettings, true, false, () => ToggleRightHandedMenu(true), () => ToggleRightHandedMenu(false)),
            new Button("Toggle Disconnect Button", Category.MSettings, true, true, () => toggledisconnectButton = true, () => toggledisconnectButton = false),
            new Button("Long Menu", Category.MSettings, true, false, () => LongMenu(true), ()=> LongMenu(false) ),
            new Button("Wide Menu", Category.MSettings, true, false, () =>WideMenu(true), ()=> WideMenu(false)),
            new Button("Outline", Category.MSettings, true, true, () => OutlineEnabled = true, ()=> OutlineEnabled = false),
            new Button("Toggle Notifications", Category.MSettings, true, true, () => toggleNotifications = true, () => toggleNotifications = false),

            
            // Room Category
            new Button("Disconect", Category.Room, false, false, () => Disconect()),
            new Button("Join Room Violet", Category.Room, false, false, () => JoinRoom("violet")),
            new Button("Join Room Cha554", Category.Room, false, false, () => JoinRoom("cha554")),
            new Button("Join Room Mod", Category.Room, false, false, () => JoinRoom("mod")),
            new Button("Join Room Mods", Category.Room, false, false, () => JoinRoom("mods")),
            //new Button("Quit Game", Category.Room, false, false, () => QuitApplication()),




            // Movement Settings Category
            new Button("Change Fly Speed", Category.MOSettings, false, false, () => ChangeFlySpeed()),


            // Advantage Settings Category
            new Button("Change Tag Aura Range", Category.ASettings, false, false, () => ChangeTagAuraDistance()),
            new Button("Change Tag Reach Range", Category.ASettings, false, false, () => ChangeTagTouchDistance()),



            //Saftey Category
            new Button("Anti Report", Category.Saftey, true, false, () => Saftey.AntiReport()),
            new Button("Flush RPCs", Category.Saftey, false, false, () => Saftey.RemoveRPCS()),


            // CS Category
            new Button("Spawn Small Black Hole", Category.CS, true, false, () => BlackHole.CreateBlackHole(1f, false)),
            new Button("Spawn Medium Black Hole", Category.CS, true, false, () => BlackHole.CreateBlackHole(3f, false)),
            new Button("Spawn Large Black Hole", Category.CS, true, false, () => BlackHole.CreateBlackHole(7f, false)),
            new Button("Spawn Massive Black Hole", Category.CS, true, false, () => BlackHole.CreateBlackHole(20f, false)),
            new Button("Spawn Black Hole V2", Category.CS, true, false, () => BlackHole.CreateBlackHoleV2()),
            new Button("Spawn Growing Black Hole", Category.CS, true, false, () => BlackHole.CreateBlackHole(0.01f, true)),

            
            // Movement Category
            new Button("WASD Fly", Category.Movement, true, false, () => WASDFly()),
            new Button("Platforms", Category.Movement, true, false, () => Platforms()),
            new Button("Sticky Platforms", Category.Movement, true, false, () => StickyPlatforms()),
            new Button("Invis Platform", Category.Movement, true, false, () => InvisPlatforms()),
            new Button("Frozone", Category.Movement, true, false, () => Frozone()),
            new Button("Velocity Movement [WEAK]", Category.Movement, true, false, () => Drag(-0.4f), () => Drag(0)),
            new Button("Velocity Movement [MEDIUM]", Category.Movement, true, false, () => Drag(-0.7f), () => Drag(0)),
            new Button("Velocity Movement [STRONG]", Category.Movement, true, false, () => Drag(-1.2f), () => Drag(0)),
            new Button("Velocity Movement [EXTREME]", Category.Movement, true, false, () => Drag(-2.2f), () => Drag(0)),
            new Button("Wall Walk", Category.Movement, true, false, () => WallWalk()),
            new Button("Legit Wall Walk", Category.Movement, true, false, () => LegitimateWallWalk()),
            new Button("No Clip", Category.Movement, true, false, () => NoClip()),
            new Button("Fly", Category.Movement, true, false, () => Fly()),
            new Button("Hand Fly", Category.Movement, true, false, () => HandFly()),
            new Button("Sling Shot", Category.Movement, true, false, () => SlingShot()),
            new Button("Trigger Sling Shot", Category.Movement, true, false, () => TriggerSlingShot()),
            new Button("JetPack", Category.Movement, true, false, () => jetpackmod()),
            new Button("Up And Down", Category.Movement, true, false, () => UpAndDown()),
            new Button("Fwd And Back", Category.Movement, true, false, () => ForwardsAndBackwards()),
            new Button("Left And Right", Category.Movement, true, false, () => LeftAndRight()),
            new Button("No Clip Fly", Category.Movement, true, false, () => NoclipFly()),
            new Button("Trigger Fly", Category.Movement, true, false, () => triggerFly()),
            new Button("Mosa Speed Boost", Category.Movement, true, false, () => MosaSpeedBoost()),
            new Button("Speed Boost", Category.Movement, true, false, () => SpeedBoost()),
            new Button("Iron Monke", Category.Movement, true, false, () => IronMonke()),
            new Button("Toggle Speed Boost", Category.Movement, true, false, () => ToggleSpeedBoost()),
            new Button("Low Gravity", Category.Movement, true, false, () => LowGravity()),
            new Button("Toggle Low Gravity", Category.Movement, true, false, () => ToggleLowGravity()),
            new Button("Zero Gravity", Category.Movement, true, false, () => ZeroGravity()),
            new Button("High Gravity", Category.Movement, true, false, () => HighGravity()),
            new Button("TP To Stump", Category.Movement, true, false, () => TPToStump()),
            new Button("TP To Tutorial", Category.Movement, true, false, () => TPToTutorial()),
            new Button("Double Jump", Category.Movement, true, false, () => DoubleJump()),
            new Button("Dash", Category.Movement, true, false, () => Dash()),
            new Button("Fast Dash", Category.Movement, true, false, () => StrongDash()),
            new Button("Right Dash", Category.Movement, true, false, () => RightDash()),
            new Button("Left Dash", Category.Movement, true, false, () => LeftDash()),
            new Button("Back Dash", Category.Movement, true, false, () => BackDash()),
            new Button("Hand Dash", Category.Movement, true, false, () => HandDash()),


            // Player Category
            new Button("Instant Tag All", Category.Player, true, false, () => TagAll()),
            new Button("Instant Tag Gun", Category.Player, true, false, () => TagGun()),
            new Button("Instant Tag Aura", Category.Player, true, false, () => TagAura()),
            new Button("Instant Tag Reach", Category.Player, true, false, () => TagReach()),
            new Button("Tag Self", Category.Player, true, false, () => TagSelf()),

            new Button("Strobe [Stump]", Category.Player, true, false, () => Strobe()),
            new Button("RGB Monke [Stump]", Category.Player, true, false, () => RGB()),
            new Button("SpawnBarrel", Category.Player, true, false, () => SpawnBarrel()),

            new Button("Copy MoveMent Gun", Category.Player, true, false, () => CopyMovementGun()),
            new Button("No Tag On Join", Category.Player, true, false, () => NoTagOnJoin()),
            new Button("Ghost", Category.Player, true, false, () => GhostMonke()),
            new Button("Invis", Category.Player, true, false, () => InvisibleMonke()),
            new Button("Hand Rig", Category.Player, true, false, () => HandRig()),
            new Button("Look At Gun", Category.Player, true, false, () => LookAtGun()),
            new Button("Look At Closest", Category.Player, true, false, () => LookAtClosest()),
            new Button("Max Quest Score", Category.Player, true, false, () => MaxQuestScore()),
            new Button("Spaz Quest Score", Category.Player, true, false, () => SpazQuestScore()),
            new Button("69 Quest Score", Category.Player, true, false, () => NiceQuestScore()),
            new Button("No Name", Category.Player, true, false, () => NoName()),
            new Button("Nword Name", Category.Player, true, false, () => Nword()),
            new Button("KKK Name", Category.Player, true, false, () => KKKName()),
            new Button("Tp Gun", Category.Player, true, false, () => TPGun()),
            new Button("Rig Gun", Category.Player, true, false, () => RigGun()),
            new Button("Scare Closest", Category.Player, true, false, () => ScareClosest()),
            new Button("Scare Gun", Category.Player, true, false, () => ScareGun()),
            new Button("Orbit Gun", Category.Player, true, false, () => OrbitGun()),
            new Button("Orbit Self", Category.Player, true, false, () => OrbitSelf(), ()=> GorillaTagger.Instance.offlineVRRig.enabled = true),


            //Visuals
            new Button("Distance ESP", Category.Visuals, true, false, () => DistanceESP()),
            new Button("Name tags", Category.Visuals, true, false, () => Nametags()),
            new Button("Corner ESP", Category.Visuals, true, false, () => CornerESP()),
            new Button("FPS Checker", Category.Visuals, true, false, () => FPSESP()),
            new Button("Headset Checker", Category.Visuals, true, false, () => HeadsetChecker()),
            new Button("Tracers", Category.Visuals, true, false, () => Tracers()),
            new Button("Infection Tracers", Category.Visuals, true, false, () => InfectionTracers()),
            new Button("Prism ESP", Category.Visuals, true, false, () => PrismESP()),
            new Button("ESP", Category.Visuals, true, false, () => ESP(), ()=> DisableESP()),
            new Button("Infection ESP", Category.Visuals, true, false, () => InfectionESP(), ()=> DisableESP()),
            new Button("Quest Check", Category.Visuals, true, false, () => QuestCheck()),
            new Button("Snake ESP", Category.Visuals, true, false, () => SnakeESP()),
            new Button("2d Box ESP", Category.Visuals, true, false, () => BoxESP2()),
            new Button("3d Box ESP", Category.Visuals, true, false, () => BoxESP1()),
            new Button("Sphere ESP", Category.Visuals, true, false, () => SphereESP()),
            new Button("Spaz Box ESP", Category.Visuals, true, false, () => SpazESP()),
            new Button("Circle Frame ESP", Category.Visuals, true, false, () => CircleFrameIDP()),

            // Projectiles Category
            new Button("Current Projectile Type []", Category.Projectiles, false, false, () => ChangeProjectileType()),
            new Button("Current Projectile Velocity []", Category.Projectiles, false, false, () => ChangeProjectileSpeed()),
            new Button("Current Projectile Color []", Category.Projectiles, false, false, () => ChangeProjectileColor()),
            new Button("Launch Projectile", Category.Projectiles, true, false, () => LaunchProjectile()),
            new Button("Grab Projectile", Category.Projectiles, true, false, () => GrabProjectile()),
            new Button("Projectile Orbit", Category.Projectiles, true, false, () => ProjectileOrbit()),
            new Button("Grab Projectile Item", Category.Projectiles, true, false, () => GrabProjectileItem()),
            new Button("Projectile Item Orbit", Category.Projectiles, true, false, () => ProjectileItemOrbit()),
            new Button("Pee", Category.Projectiles, true, false, () => Pee()),
            new Button("Cum", Category.Projectiles, true, false, () => Projectiles.Cum()),
            new Button("Period Blood", Category.Projectiles, true, false, () => PeriodBlood()),
            new Button("Shit", Category.Projectiles, true, false, () => Shit()),
            new Button("Wet Shit", Category.Projectiles, true, false, () => WetShit()),
            new Button("Splash Hands", Category.Projectiles, true, false, () => SplashHands()),
            new Button("Splash Orbit", Category.Projectiles, true, false, () => SplashOrbit()),
            new Button("Splash Body", Category.Projectiles, true, false, () => SplashBody()),
            new Button("Cum", Category.Projectiles, true, false, () => Water.Cum()),
            new Button("Splash Aura", Category.Projectiles, true, false, () => SplashAura()),
            new Button("Tiny Splash Hands", Category.Projectiles, true, false, () => TinySplashHands()),
            new Button("Tiby Splash Orbit", Category.Projectiles, true, false, () => TinySplashOrbit()),
            new Button("Tiny Splash Body", Category.Projectiles, true, false, () => TinySplashBody()),
            new Button("Tiny Cum", Category.Projectiles, true, false, () => TinyCum()),
            new Button("Tiny Splash Aura", Category.Projectiles, true, false, () => TinySplashAura()),
            new Button("Mixed Splash Aura", Category.Projectiles, true, false, () => MixedSplashAura()),


            // Overpowered Category
            new Button("Current Lag Power []", Category.Overpowered, false, false, () => ChangeLagPower()),
            new Button("Lag Gun", Category.Overpowered, true, false, () => Lag(1)),
            new Button("Lag All", Category.Overpowered, true, false, () => Lag(0)),
            new Button("Lag On Touch", Category.Overpowered, true, false, () => Lag(2)),
            new Button("Lag On They Touch", Category.Overpowered, true, false, () => Lag(3)),
            new Button("Lag Aura", Category.Overpowered, true, false, () => Lag(4)),
            new Button("Lag On They Aura", Category.Overpowered, true, false, () => Lag(5)),
            new Button("Destroy Gun", Category.Overpowered, true, false, () => DestroyGun()),
            new Button("Destroy All", Category.Overpowered, true, false, () => DestroyAll()),
            new Button("Spaz Ropes", Category.Overpowered, true, false, () => SpazRopes()),
            new Button("Up Ropes", Category.Overpowered, true, false, () => UpRopes()),
            new Button("Snowball MiniGun", Category.Overpowered, true, false, () => SnowballMinigun()),
            new Button("Fling Gun", Category.Overpowered, true, false, () => FlingGun()),
            new Button("Fling Aura", Category.Overpowered, true, false, () => FlingAura()),
            new Button("Fling On Touch", Category.Overpowered, true, false, () => FlingOnYouTouch()),
            new Button("Fling On They Touch", Category.Overpowered, true, false, () => FlingOnTheyTouch()),
            new Button("Kick All In Stump", Category.Overpowered, false, false, () => KickAllInStump()),
            new Button("Quit App All [GUARDIAN]", Category.Overpowered, true, false, () => QuitAppAll()),
            new Button("Quit App App [GUARDIAN]", Category.Overpowered, true, false, () => QuitAppGun()),
            new Button("Grab All [GUARDIAN]", Category.Overpowered, true, false, () => GrabAll()),
            new Button("Grab App [GUARDIAN]", Category.Overpowered, true, false, () => GrabGun()),
            new Button("RGB Board", Category.Overpowered, true, false, () => RGBBoard()),
            new Button("Launch HoverBoard", Category.Overpowered, true, false, () => HoverBoardLauncher()),
            new Button("Orbit HoverBoard", Category.Overpowered, true, false, () => OrbitHoverBoard()),
            new Button("Elevator Kick Gun [M]", Category.Overpowered, true, false, () => ElevatorTpGun()),
            new Button("Elevator Kick All [M]", Category.Overpowered, true, false, () => ElevatorTpAll()),


            // Master Category
            new Button("Rock Monke Gamemode [SLOW]", Category.Master, false, false, () => RockMonkeGamemode()),
            new Button("Lava Monkey Gamemode [SLOW]", Category.Master, false, false, () => InfectionMonkeGamemode()),
            new Button("Slow Gun", Category.Master, false, false, () => SlowGun()),
            new Button("Slow All", Category.Master, true, false, () => SlowAll()),
            new Button("Vibrate Gun", Category.Master, false, false, () => SlowGun()),
            new Button("Vibrate All", Category.Master, true, false, () => SlowAll()),
            new Button("Rose Toy Self", Category.Master, true, false, () => RoseToySelf()),
            new Button("Tag All", Category.Master, false, false, () => TagAllMaster()),
            new Button("Tag Gun", Category.Master, true, false, () => TagGunMaster()),
            new Button("Tag Touch", Category.Master, true, false, () => TagReachMaster()),
            new Button("Tag Aura", Category.Master, true, false, () => TagAuraMaster()),
            new Button("Tag Self", Category.Master, false, false, () => TagSelfMaster()),
            new Button("Untag All", Category.Master, false, false, () => UnTagAllMaster()),
            new Button("Untag Gun", Category.Master, true, false, () => UnTagGunMaster()),
            new Button("Untag Touch", Category.Master, true, false, () => UnTagReachMaster()),
            new Button("Untag Aura", Category.Master, true, false, () => UnTagAuraMaster()),
            new Button("Untag Self", Category.Master, false, false, () => UnTagSelfMaster()),
            new Button("Stop Gamemode", Category.Master, false, false, () => StopGamemode()),
            new Button("Start Gamemode", Category.Master, false, false, () => StopGamemode()),
            new Button("Restart Gamemode", Category.Master, false, false, () => RestartGamemode()),
            new Button("Spaz Gamemode", Category.Master, true, false, () => SpazGamemode()),
            new Button("Spawn Enemy Chaser [Reactor] [<color=red>M</color>]", Category.Master, true, false, () => CreateEntity(GhostReactorEntities.EnemyChaser)),
            new Button("Spawn Enemy Chaser Armored [Reactor] [<color=red>M</color>]", Category.Master, true, false, () => CreateEntity(GhostReactorEntities.EnemyChaserArmored)),
            new Button("Spawn Enemy Ranged [Reactor] [<color=red>M</color>]", Category.Master, true, false, () => CreateEntity(GhostReactorEntities.EnemyRanged)),
            new Button("Spawn Enemy Ranged Armored [Reactor] [<color=red>M</color>]", Category.Master, true, false, () => CreateEntity(GhostReactorEntities.EnemyRangedArmored)),
            new Button("Spawn Enemy Phantom [Reactor] [<color=red>M</color>]", Category.Master, true, false, () => CreateEntity(GhostReactorEntities.EnemyPhantom)),
            new Button("Spawn Enemy Pest [Reactor] [<color=red>M</color>]", Category.Master, true, false, () => CreateEntity(GhostReactorEntities.EnemyPest)),
            new Button("Spawn Tool Flash [Reactor] [<color=red>M</color>]", Category.Master, true, false, () => CreateEntity(GhostReactorEntities.ToolFlash)),
            new Button("Spawn Collectible Core [Reactor] [<color=red>M</color>]", Category.Master, true, false, () => CreateEntity(GhostReactorEntities.CollectibleCore)),
            new Button("Spawn Tool Club [Reactor] [<color=red>M</color>]", Category.Master, true, false, () => CreateEntity(GhostReactorEntities.ToolClub)),
            new Button("Spawn Tool Collector [Reactor] [<color=red>M</color>]", Category.Master, true, false, () => CreateEntity(GhostReactorEntities.ToolCollector)),
            new Button("Spawn Tool Revive [Reactor] [<color=red>M</color>]", Category.Master, true, false, () => CreateEntity(GhostReactorEntities.ToolRevive)),
            new Button("Spawn Barrier Spectral [Reactor] [<color=red>M</color>]", Category.Master, true, false, () => CreateEntity(GhostReactorEntities.BarrierSpectral)),
            new Button("Spawn Collectible Flower [Reactor] [<color=red>M</color>]", Category.Master, true, false, () => CreateEntity(GhostReactorEntities.CollectibleFlower)),
            new Button("Spawn Collectible Core Flower Variant [Reactor] [<color=red>M</color>]", Category.Master, true, false, () => CreateEntity(GhostReactorEntities.CollectibleCoreFlowerVariant)),
            new Button("Spawn Energy Cost Gate [Reactor] [<color=red>M</color>]", Category.Master, true, false, () => CreateEntity(GhostReactorEntities.EnergyCostGate)),
            new Button("Spawn Id Badge [Reactor] [<color=red>M</color>]", Category.Master, true, false, () => CreateEntity(GhostReactorEntities.IdBadge)),
            new Button("Spawn Wall Light [Reactor] [<color=red>M</color>]", Category.Master, true, false, () => CreateEntity(GhostReactorEntities.WallLight01)),
            new Button("Spawn Breakable Crate [Reactor] [<color=red>M</color>]", Category.Master, true, false, () => CreateEntity(GhostReactorEntities.BreakableCrate)),
            new Button("Spawn Tool Lantern [Reactor] [<color=red>M</color>]", Category.Master, true, false, () => CreateEntity(GhostReactorEntities.ToolLantern)),
            new Button("Spawn Breakable Barrel [Reactor] [<color=red>M</color>]", Category.Master, true, false, () => CreateEntity(GhostReactorEntities.BreakableBarrel)),
            new Button("Spawn Tool Shield Gun [Reactor] [<color=red>M</color>]", Category.Master, true, false, () => CreateEntity(GhostReactorEntities.ToolShieldGun)),

            // SoundBoard Category
            new Button("Play Alarm", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/alarm!!!-made-with-Voicemod.mp3", "alarm!!!-made-with-Voicemod.mp3"), false)),
            new Button("Play Amogus", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/amogus-made-with-Voicemod.mp3", "amogus-made-with-Voicemod.mp3"), false)),
            new Button("Play Augh", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/augh-made-with-Voicemod.mp3", "augh-made-with-Voicemod.mp3"), false)),
            new Button("Play Ben No", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/ben-no-made-with-Voicemod.mp3", "ben-no-made-with-Voicemod.mp3"), false)),
            new Button("Play Bruh", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/bruh-made-with-Voicemod.mp3", "bruh-made-with-Voicemod.mp3"), false)),
            new Button("Play Chipi Chipi Chapa Chapa", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/chipi-chipi-chapa-chapa-made-with-Voicemod.mp3", "chipi-chipi-chapa-chapa-made-with-Voicemod.mp3"), false)),
            new Button("Play Clapping", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/clapping-made-with-Voicemod.mp3", "clapping-made-with-Voicemod.mp3"), false)),
            new Button("Play Czy To Freddy Fazbear", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/czy-to-freddy-fazbear-made-with-Voicemod.mp3", "czy-to-freddy-fazbear-made-with-Voicemod.mp3"), false)),
            new Button("Play Discord In", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/discord-in-made-with-Voicemod.mp3", "discord-in-made-with-Voicemod.mp3"), false)),
            new Button("Play Emotional Damage", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/emotional-damage-made-with-Voicemod.mp3", "emotional-damage-made-with-Voicemod.mp3"), false)),
            new Button("Play Erm What The Sigma", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/erm-what-the-sigma-made-with-Voicemod.mp3", "erm-what-the-sigma-made-with-Voicemod.mp3"), false)),
            new Button("Play Fart", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/fart-made-with-Voicemod.mp3", "fart-made-with-Voicemod.mp3"), false)),
            new Button("Play FBI Open Up", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/fbi-open-up!-(sound-effect)-made-with-Voicemod.mp3", "fbi-open-up!-(sound-effect)-made-with-Voicemod.mp3"), false)),
            new Button("Play Get Out", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/get-out-made-with-Voicemod.mp3", "get-out-made-with-Voicemod.mp3"), false)),
            new Button("Play Giga Chad", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/giga-chad-made-with-Voicemod.mp3", "giga-chad-made-with-Voicemod.mp3"), false)),
            new Button("Play Hawk Tuah", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/hawk-tuah_SRaUp2L.mp3", "hawk-tuah_SRaUp2L.mp3"), false)),
            new Button("Play Holy Moly", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/holy-moly-made-with-Voicemod.mp3", "holy-moly-made-with-Voicemod.mp3"), false)),
            new Button("Play Lego Yoda Death", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/lego-yoda-death-made-with-Voicemod.mp3", "lego-yoda-death-made-with-Voicemod.mp3"), false)),
            new Button("Play Mario Jump", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/mario-jump-made-with-Voicemod.mp3", "mario-jump-made-with-Voicemod.mp3"), false)),
            new Button("Play Metal Pipe Falling", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/metal-pipe-falling-sound-effect-made-with-Voicemod.mp3", "metal-pipe-falling-sound-effect-made-with-Voicemod.mp3"), false)),
            new Button("Play Monkey", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/monkey-made-with-Voicemod.mp3", "monkey-made-with-Voicemod.mp3"), false)),
            new Button("Play Oi Oi Oi", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/oi-oi-oi-made-with-Voicemod.mp3", "oi-oi-oi-made-with-Voicemod.mp3"), false)),
            new Button("Play Rick Roll", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/rick-roll-made-with-Voicemod.mp3", "rick-roll-made-with-Voicemod.mp3"), false)),
            new Button("Play Rizz Sound Effect", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/rizz-made-with-Voicemod.mp3", "rizz-made-with-Voicemod.mp3"), false)),
            new Button("Play Roblox Bye", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/roblox-bye-made-with-Voicemod.mp3", "roblox-bye-made-with-Voicemod.mp3"), false)),
            new Button("Play Spongebob", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/spongebob-made-with-Voicemod.mp3", "spongebob-made-with-Voicemod.mp3"), false)),
            new Button("Play Sus", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/sus-made-with-Voicemod.mp3", "sus-made-with-Voicemod.mp3"), false)),
            new Button("Play Taco Bell Bell", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/taco-bell-bell-made-with-Voicemod.mp3", "taco-bell-bell-made-with-Voicemod.mp3"), false)),
            new Button("Play Two Hours Later", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/two-hours-later-made-with-Voicemod.mp3", "two-hours-later-made-with-Voicemod.mp3"), false)),
            new Button("Play Uhh No", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/uhh-no-made-with-Voicemod.mp3", "uhh-no-made-with-Voicemod.mp3"), false)),
            new Button("Play Uwu", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/uwu-made-with-Voicemod.mp3", "uwu-made-with-Voicemod.mp3"), false)),
            new Button("Play Vine Boom", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/vine-boom-made-with-Voicemod.mp3", "vine-boom-made-with-Voicemod.mp3"), false)),
            new Button("Play What The Sigma", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/what-the-sigma-made-with-Voicemod.mp3", "what-the-sigma-made-with-Voicemod.mp3"), false)),
            new Button("Play Why Are You Gay", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/why-are-you-gay-made-with-Voicemod.mp3", "why-are-you-gay-made-with-Voicemod.mp3"), false)),
            new Button("Play Womp Womp", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/womp-womp-made-with-Voicemod.mp3", "womp-womp-made-with-Voicemod.mp3"), false)),
            new Button("Play Yipee", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/SoundBoard/main/yipee-made-with-Voicemod.mp3", "yipee-made-with-Voicemod.mp3"), false)),
            new Button("Play daisy-bell-slowed", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/GhostSounds/main/daisy-bell-slowed-made-with-Voicemod.mp3", "daisy-bell-slowed-made-with-Voicemod.mp3"), false)),
            new Button("Play daisy09-gorilla-tag", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/GhostSounds/main/daisy09-gorilla-tag-made-with-Voicemod.mp3", "daisy09-gorilla-tag-made-with-Voicemod.mp3"), false)),
            new Button("Play distorted-run-rabbit-run", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/GhostSounds/main/distorted-run-rabbit-run-made-with-Voicemod.mp3", "distorted-run-rabbit-run-made-with-Voicemod.mp3"), false)),
            new Button("Play footsteps-sound-effect", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/GhostSounds/main/footsteps-sound-effect-made-with-Voicemod.mp3", "footsteps-sound-effect-made-with-Voicemod.mp3"), false)),
            new Button("Play j3vu-message", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/GhostSounds/main/j3vu-message-made-with-Voicemod.mp3", "j3vu-message-made-with-Voicemod.mp3"), false)),
            new Button("Play pbbv-warningbot", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/GhostSounds/main/pbbv-warningbot-made-with-Voicemod.mp3", "pbbv-warningbot-made-with-Voicemod.mp3"), false)),
            new Button("Play run-rabbit-run", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/GhostSounds/main/run-rabbit-run-made-with-Voicemod.mp3", "run-rabbit-run-made-with-Voicemod.mp3"), false)),
            new Button("Play run-run-run", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/GhostSounds/main/run-run-run-made-with-Voicemod.mp3", "run-run-run-made-with-Voicemod.mp3"), false)),
            new Button("Play t774-bells", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/GhostSounds/main/t774-bells-made-with-Voicemod.mp3", "t774-bells-made-with-Voicemod.mp3"), false)),
            new Button("Play t774-speech-1", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/GhostSounds/main/t774-speech-made-with-Voicemod (1).mp3", "t774-speech-made-with-Voicemod (1).mp3"), false)),
            new Button("Play t774-speech-2", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/GhostSounds/main/t774-speech-made-with-Voicemod.mp3", "t774-speech-made-with-Voicemod.mp3"), false)),
            new Button("Play tip-toe-warning-bot", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/GhostSounds/main/tip-toe-warning-bot-made-with-Voicemod.mp3", "tip-toe-warning-bot-made-with-Voicemod.mp3"), false)),
            new Button("Play virus-twin1-sound", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/GhostSounds/main/virus-twin1-sound-made-with-Voicemod.mp3", "virus-twin1-sound-made-with-Voicemod.mp3"), false)),
            new Button("Play wi-crash-sound", Category.SoundBoard, false, false, () => AudioManager.PlaySoundThroughMicrophone(AudioManager.LoadSoundFromURL("https://raw.githubusercontent.com/TortiseWay2Cool/GhostSounds/main/wi-crash-sound-(better-&-louder)-made-with-Voicemod.mp3", "wi-crash-sound-(better-&-louder)-made-with-Voicemod.mp3"), false)),


            // Networking Category
            new Button("Launch SlingShot Networked [BROKEN] [CS]", Category.Networking, true, false, () => Networked.LaunchSlingShotNetworked()),
            new Button("Launch WaterBalloons [BROKEN] [CS]", Category.Networking, true, false, () => Networked.WaterBaloons()),
            new Button("Launch IceCream [BROKEN] [CS]", Category.Networking, true, false, () => Networked.IceCream()),
            new Button("Launch DeadShot [BROKEN] [CS]", Category.Networking, true, false, () => Networked.DeadShot()),
            new Button("Launch Present V1 [BROKEN] [CS]", Category.Networking, true, false, () => Networked.PresentV1()),
            new Button("Launch Firework [BROKEN] [CS]", Category.Networking, true, false, () => Networked.Firework()),
            new Button("Launch Cloud Slingshot [BROKEN] [CS]", Category.Networking, true, false, () => Networked.CloundSlingshot()),
            new Button("Launch InVisible [BROKEN] [CS]", Category.Networking, true, false, () => Networked.InVisible()),
            new Button("Launch Firework V2 [BROKEN] [CS]", Category.Networking, true, false, () => Networked.FireWorkV2()),
            new Button("Launch Heart Slingshot [BROKEN] [CS]", Category.Networking, true, false, () => Networked.HeartSlingShot()),
            new Button("Launch FishFood [BROKEN] [CS]", Category.Networking, true, false, () => Networked.FishFood()),
            new Button("Launch Snowball [BROKEN] [CS]", Category.Networking, true, false, () => Networked.Snowball()),


            // Stone
            new Button("Vibrate Gun", Category.StoneNetworking, true, false, () => StoneConfig_Config.GunEvent("Vibrate")),
            new Button("Vibrate All", Category.StoneNetworking, true, false, () => StoneConfig_Config.EventAll("Vibrate")),
            new Button("Slow Gun", Category.StoneNetworking, true, false, () => StoneConfig_Config.GunEvent("Slow")),
            new Button("Slow All", Category.StoneNetworking, true, false, () => StoneConfig_Config.EventAll("Slow")),
            new Button("Kick Gun", Category.StoneNetworking, true, false, () => StoneConfig_Config.GunEvent("Kick")),
            new Button("Kick All", Category.StoneNetworking, true, false, () => StoneConfig_Config.EventAll("Kick")),
            new Button("Fling Gun", Category.StoneNetworking, true, false, () => StoneConfig_Config.GunEvent("Fling")),
            new Button("Fling All", Category.StoneNetworking, true, false, () => StoneConfig_Config.EventAll("Fling")),
            new Button("Stutter Gun", Category.StoneNetworking, true, false, () => StoneConfig_Config.GunEvent("Stutter")),
            new Button("Stutter All", Category.StoneNetworking, true, false, () => StoneConfig_Config.EventAll("Stutter")),
            new Button("Bring Gun", Category.StoneNetworking, true, false, () => StoneConfig_Config.GunEvent("Bring")),
            new Button("Bring All", Category.StoneNetworking, true, false, () => StoneConfig_Config.EventAll("Bring")),
            new Button("BreakMovement Gun", Category.StoneNetworking, true, false, () => StoneConfig_Config.GunEvent("BreakMovemet")),
            new Button("BreakMovement All", Category.StoneNetworking, true, false, () => StoneConfig_Config.EventAll("BreakMovemet")),
            new Button("Message Gun", Category.StoneNetworking, true, false, () => StoneConfig_Config.GunEvent("Message")),
            new Button("Message All", Category.StoneNetworking, true, false, () => StoneConfig_Config.EventAll("Message")),
            new Button("Grab", Category.StoneNetworking, true, false, () => StoneConfig_Config.Grab()),
            new Button("ScaleDown Gun", Category.StoneNetworking, true, false, () => StoneConfig_Config.GunEvent("ScaleDown")),
            new Button("ScaleDown All", Category.StoneNetworking, true, false, () => StoneConfig_Config.EventAll("ScaleDown")),
            new Button("ScaleUp Gun", Category.StoneNetworking, true, false, () => StoneConfig_Config.GunEvent("ScaleUp")),
            new Button("ScaleUp All", Category.StoneNetworking, true, false, () => StoneConfig_Config.EventAll("ScaleUp")),
            new Button("ScaleReset Gun", Category.StoneNetworking, true, false, () => StoneConfig_Config.GunEvent("ScaleReset")),
            new Button("ScaleReset All", Category.StoneNetworking, true, false, () => StoneConfig_Config.EventAll("ScaleReset")),
            new Button("Low Gravity Gun", Category.StoneNetworking, true, false, () => StoneConfig_Config.GunEvent("LowGrav")),
            new Button("Low Gravity All", Category.StoneNetworking, true, false, () => StoneConfig_Config.EventAll("LowGrav")),
            new Button("No Gravity Gun", Category.StoneNetworking, true, false, () => StoneConfig_Config.GunEvent("NoGrav")),
            new Button("No Gravity All", Category.StoneNetworking, true, false, () => StoneConfig_Config.EventAll("NoGrav")),
            new Button("High Gravity Gun", Category.StoneNetworking, true, false, () => StoneConfig_Config.GunEvent("HighGrav")),
            new Button("High Gravity All", Category.StoneNetworking, true, false, () => StoneConfig_Config.EventAll("HighGrav")),
            new Button("Disable Nametags Gun", Category.StoneNetworking, true, false, () => StoneConfig_Config.GunEvent("DisableNameTags")),
            new Button("Disable Nametags All", Category.StoneNetworking, true, false, () => StoneConfig_Config.EventAll("DisableNameTags")),
            new Button("Enable Nametags Gun", Category.StoneNetworking, true, false, () => StoneConfig_Config.GunEvent("EnableNameTags")),
            new Button("Enable Nametags All", Category.StoneNetworking, true, false, () => StoneConfig_Config.EventAll("EnableNameTags"))
        };
    }
}