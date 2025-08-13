using ExitGames.Client.Photon;
using Oculus.Platform;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;
using VioletFree.Utilities;

namespace VioletFree.Mods.Settings
{
    class Saftey
    {
        public static void AntiReport()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (vrrig != GorillaTagger.Instance.offlineVRRig)
                    {
                        Vector3 rHand = vrrig.rightHandTransform.position;
                        Vector3 lHand = vrrig.leftHandTransform.position;
                        rHand = vrrig.rightHandTransform.position + vrrig.rightHandTransform.forward * 0.125f;
                        lHand = vrrig.leftHandTransform.position + vrrig.leftHandTransform.forward * 0.125f;
                        float range = 0.6f;
                        foreach (GorillaPlayerScoreboardLine gorillaPlayerScoreboardLine in GorillaScoreboardTotalUpdater.allScoreboardLines)
                        {
                            if (gorillaPlayerScoreboardLine.linePlayer == NetworkSystem.Instance.LocalPlayer)
                            {
                                Vector3 reportButton = gorillaPlayerScoreboardLine.reportButton.gameObject.transform.position + new Vector3(0f, 0.001f, 0.0004f);
                                if (Vector3.Distance(reportButton, vrrig.leftHandTransform.position) < range)
                                {
                                    NotificationLib.SendNotification("<color=red>Anti-Report:</color> : " + vrrig.playerText1.text + " Attempted to Report You");
                                    PhotonNetwork.Disconnect();
                                }
                                if (Vector3.Distance(reportButton, vrrig.rightHandTransform.position) < range)
                                {
                                    NotificationLib.SendNotification("<color=red>Anti-Report</color> : " + vrrig.playerText1.text + " Attempted to <color=red>Report</color> You");
                                    PhotonNetwork.Disconnect();
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void KawaiiRPC()
        {
            Hashtable yuh = new Hashtable();
            yuh[0] = GorillaTagger.Instance.myVRRig.ViewID;
            PhotonNetwork.NetworkingClient.OpRaiseEvent(200, yuh,  new RaiseEventOptions
            {
                CachingOption = EventCaching.RemoveFromRoomCache,
                TargetActors = new int[]
                {
                    PhotonNetwork.LocalPlayer.ActorNumber
                }
            }, SendOptions.SendReliable);
        }
        public static void RemoveRPCS()
        {
            KawaiiRPC();
        }
    }
}
