using BepInEx;
using UnityEngine;

namespace Console
{
    public class Plugin : MonoBehaviour
    {
        // Put this snippet of code in your BaseUnityPlugin
        void Start() =>
            GorillaTagger.OnPlayerSpawned(OnPlayerSpawned);


        void OnPlayerSpawned()
        {
            string ConsoleGUID = $"goldentrophy_Console_{Console.ConsoleVersion}";
            GameObject ConsoleObject = GameObject.Find(ConsoleGUID);

            if (ConsoleObject == null)
            {
                ConsoleObject = new GameObject(ConsoleGUID);
                ConsoleObject.AddComponent<CoroutineManager>();
                ConsoleObject.AddComponent<Console>();
            }

            if (ServerData.ServerDataEnabled)
                ConsoleObject.AddComponent<ServerData>();
        }
    }
}
