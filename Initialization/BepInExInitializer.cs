using BepInEx;
using HarmonyLib;
using static VioletFree.Initialization.PluginInfo;

namespace VioletFree.Initialization
{
    [BepInPlugin(menuGUID, menuName, menuVersion)]
    public class BepInExInitializer : BaseUnityPlugin
    {
        public static BepInEx.Logging.ManualLogSource LoggerInstance;

        void Awake()
        {
            LoggerInstance = Logger;
            new Harmony(menuGUID).PatchAll();
        }
    }
}
