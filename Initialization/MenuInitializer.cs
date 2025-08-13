using Console;
using HarmonyLib;
using System;
using UnityEngine;
using VioletFree.Menu;
using VioletFree.Mods.Spammers;
using VioletFree.Mods.Stone;
using VioletFree.Mods.Vissual;
using VioletFree.Utilities;
//using static VioletFree.Menu.VioletGUI;

namespace VioletFree.Initialization
{
    [HarmonyPatch(typeof(GorillaLocomotion.GTPlayer), "LateUpdate")]
    internal class MenuInitializer
    {
        private static GameObject menuObject = null;

        static void Postfix()
        {
            if (menuObject != null && GameObject.Find(PluginInfo.menuName) != null) return;

            if (menuObject != null)
            {
                Debug.LogWarning($"{PluginInfo.menuName} was unexpectedly destroyed. Reinitializing...");
            }

            try
            {
                menuObject = new GameObject(PluginInfo.menuName);
                Debug.Log($"Initializing {PluginInfo.menuName}...");
                menuObject.AddComponent<Networked>();
                menuObject.AddComponent<Main>();
                menuObject.AddComponent<Plugin>();
                menuObject.AddComponent<CoroutineManager>();
                menuObject.AddComponent<NotificationLib>();
                menuObject.AddComponent<StoneBase>();
                menuObject.AddComponent<VioletFree.Menu.Gui>();
                GameObject.DontDestroyOnLoad(menuObject);
                Debug.Log($"{PluginInfo.menuName} successfully initialized.");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to initialize {PluginInfo.menuName}: {ex.Message}\n{ex.StackTrace}");
            }
        }
    }
}
