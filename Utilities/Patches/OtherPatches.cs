using HarmonyLib;
using Photon.Pun;
using PlayFab.ClientModels;
using PlayFab.Internal;
using PlayFab;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace VioletFree.Utilities.Patches
{
    public class OtherPatches
    {
        [HarmonyPatch(typeof(GorillaLocomotion.GTPlayer), "AntiTeleportTechnology", MethodType.Normal)]
        public class NoAntiTP
        {
            static bool Prefix()
            {
                return false;
            }
        }

        [HarmonyPatch(typeof(PlayFabClientAPI), "AttributeInstall")]
        internal class NoAttributeInstall
        {
            private static bool Prefix()
            {
                return false;
            }
        }

        [HarmonyPatch(typeof(PlayFabHttp), "InitializeScreenTimeTracker")]
        internal class NoInitializeScreenTimeTracker
        {
            private static bool Prefix()
            {
                return false;
            }
        }

        [HarmonyPatch(typeof(PlayFabDeviceUtil), "GetAdvertIdFromUnity")]
        internal class NoGetAdvertIdFromUnity
        {
            private static bool Prefix()
            {
                return false;
            }
        }

        [HarmonyPatch(typeof(PlayFabDeviceUtil), "DoAttributeInstall")]
        internal class NoDoAttributeInstall
        {
            private static bool Prefix()
            {
                return false;
            }
        }

        [HarmonyPatch(typeof(PlayFabClientInstanceAPI), "ReportDeviceInfo")]
        internal class NoDeviceInfo2
        {
            private static bool Prefix()
            {
                return false;
            }
        }

        [HarmonyPatch(typeof(PlayFabClientAPI), "ReportDeviceInfo")]
        internal class NoDeviceInfo1
        {
            private static bool Prefix()
            {
                return false;
            }
        }

        [HarmonyPatch(typeof(PlayFabDeviceUtil), "SendDeviceInfoToPlayFab")]
        internal class NoDeviceInfo
        {
            private static bool Prefix()
            {
                return false;
            }
        }

        [HarmonyPatch(typeof(PlayFabClientInstanceAPI), "ReportPlayer", MethodType.Normal)]
        public class NoReportPlayer
        {
            static bool Prefix(ReportPlayerClientRequest request, Action<ReportPlayerClientResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
            {
                return false;
            }
        }

        [HarmonyPatch(typeof(PlayFabClientAPI), "ReportPlayer", MethodType.Normal)]
        public class PlayFabReportPatch2
        {
            private static bool Prefix(ReportPlayerClientRequest request, Action<ReportPlayerClientResult> resultCallback, Action<PlayFabError> errorCallback, object customData = null, Dictionary<string, string> extraHeaders = null)
            {
                return false;
            }
        }


        [HarmonyPatch(typeof(GorillaNot), "SendReport")]
        public static class NoSendReport
        {
            private static bool Prefix(string susReason, string susId, string susNick)
            {
                if (susId == PhotonNetwork.LocalPlayer.UserId)
                {
                    Debug.Log("Reported By Anti Cheat : " + susReason);
                    //NotificationLib.SendNotification("<color=blue>Anti-Cheat</color> : Reason: " + susReason);
                }

                return false;
            }
        }

        [HarmonyPatch(typeof(GorillaNot), "CheckReports", MethodType.Enumerator)]
        public class ReportCheck : MonoBehaviourPunCallbacks
        {
            public static bool Prefix()
            {
                return false;
            }
        }

        [HarmonyPatch(typeof(GorillaNot), "LogErrorCount")]
        public class LogErrorCount : MonoBehaviourPunCallbacks
        {
            public static bool Prefix()
            {
                return false;
            }
        }

        [HarmonyPatch(typeof(GorillaNot), "DispatchReport")]
        public class Dispatch : MonoBehaviourPunCallbacks
        {
            public static bool Prefix()
            {
                return false;
            }
        }

        [HarmonyPatch(typeof(GorillaNetworkPublicTestsJoin))]
        [HarmonyPatch("GracePeriod", MethodType.Enumerator)]
        public class NoGracePeriod
        {
            public static bool Prefix()
            {
                return false;
            }
        }

        [HarmonyPatch(typeof(GorillaNetworkPublicTestsJoin))]
        [HarmonyPatch("LateUpdate", MethodType.Normal)]
        public class NoGracePeriod4
        {
            public static bool Prefix()
            {
                return false;
            }
        }

        [HarmonyPatch(typeof(GorillaNetworkPublicTestJoin2))]
        [HarmonyPatch("GracePeriod", MethodType.Enumerator)]
        public class NoGracePeriod3
        {
            public static bool Prefix()
            {
                return false;
            }
        }

        [HarmonyPatch(typeof(GorillaNetworkPublicTestJoin2))]
        [HarmonyPatch("LateUpdate", MethodType.Normal)]
        public class NoGracePeriod2
        {
            public static bool Prefix()
            {
                return false;
            }
        }
    }
}
