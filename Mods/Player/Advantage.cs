using BepInEx;
using Fusion;
using GunTemplateee;
using Oculus.Platform;
using Photon.Pun;
using VioletFree.Mods.Overpowered;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VioletFree.Utilities;
using UnityEngine.Animations.Rigging;
using GorillaGameModes;
using VioletFree.Utilities;

namespace VioletFree.Mods.Player
{
    class Advantage : MonoBehaviour
    {
        public static Dictionary<int, float> lastTaggedTime = new Dictionary<int, float>();
        public static void TagAll()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (!vrrig.mainSkin.material.name.Contains("it") || !vrrig.mainSkin.material.name.Contains("fected"))
                {
                    int targetActorNumber = vrrig.Creator.ActorNumber;
                    Photon.Realtime.Player targetPlayer = PhotonNetwork.CurrentRoom.GetPlayer(targetActorNumber);

                    if (targetPlayer != null && vrrig.Creator.ActorNumber == targetActorNumber)
                    {
                        float currentTime = Time.time;

                        if (!lastTaggedTime.ContainsKey(targetActorNumber) || currentTime - lastTaggedTime[targetActorNumber] >= 0.5f)
                        {
                            Overpowered.Overpowered.SerilizeAction(vrrig, () => Overpowered.Overpowered.photonView.RPC("RPC_ReportTag", RpcTarget.All, new object[] { vrrig.Creator.ActorNumber }));
                            lastTaggedTime[targetActorNumber] = currentTime;
                        }
                    }
                }
            }
        }

        internal static void TagGun()
        {
            GunTemplate.StartBothGuns(() =>
            {
                if (!GunTemplate.lockedPlayer.mainSkin.material.name.Contains("it") || !GunTemplate.lockedPlayer.mainSkin.material.name.Contains("fected"))
                {
                    VRRig rig = GunTemplate.lockedPlayer;
                    int targetActorNumber = rig.Creator.ActorNumber;
                    Photon.Realtime.Player targetPlayer = PhotonNetwork.CurrentRoom.GetPlayer(targetActorNumber);
                    float currentTime = Time.time;

                    if (!lastTaggedTime.ContainsKey(targetActorNumber) || currentTime - lastTaggedTime[targetActorNumber] >= 5f)
                    {
                        Overpowered.Overpowered.SerilizeAction(rig, () => Overpowered.Overpowered.photonView.RPC("RPC_ReportTag", RpcTarget.All, new object[] { targetActorNumber }));
                        lastTaggedTime[targetActorNumber] = currentTime;
                    }
                }
            }, true);
        }

        public static void TagReach()
        {
            GameObject yuh = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            yuh.transform.position = GorillaTagger.Instance.leftHandTransform.position;
            yuh.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;
            yuh.transform.localScale = new Vector3(Settings.Settings.TagHandDistance / 2, Settings.Settings.TagHandDistance / 2, Settings.Settings.TagHandDistance / 2);
            yuh.GetComponent<Renderer>().material.shader = ColorLib.guiShader;
            yuh.GetComponent<Renderer>().material.color = ColorLib.DeepVioletTransparent;
            Destroy(yuh.GetComponent<Collider>());
            GameObject yuh2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            yuh2.transform.position = GorillaTagger.Instance.rightHandTransform.position;
            yuh2.transform.rotation = GorillaTagger.Instance.rightHandTransform.rotation;
            yuh2.transform.localScale = new Vector3(Settings.Settings.TagHandDistance / 2, Settings.Settings.TagHandDistance / 2, Settings.Settings.TagHandDistance / 2);
            Destroy(yuh2.GetComponent<Collider>());
            yuh2.GetComponent<Renderer>().material.shader = ColorLib.guiShader;
            yuh2.GetComponent<Renderer>().material.color = ColorLib.DeepVioletTransparent;
            Destroy(yuh, Time.deltaTime);
            Destroy(yuh2, Time.deltaTime);
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {

                if (rig != GorillaTagger.Instance.offlineVRRig && (Vector3.Distance(GorillaTagger.Instance.leftHandTransform.position, rig.headMesh.transform.position) < Settings.Settings.TagHandDistance || Vector3.Distance(GorillaTagger.Instance.rightHandTransform.position, rig.headMesh.transform.position) < Settings.Settings.TagHandDistance))
                {
                    PhotonView photonView = GameObject.Find("Player Objects/RigCache/Network Parent/GameMode(Clone)")
                        .GetPhotonView(
                    );

                    if (photonView != null)
                    {
                        photonView.RPC("RPC_ReportTag", RpcTarget.All, new object[] { rig.Creator.ActorNumber });
                    }
                }
            }
        }

        public static void TagAura()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {

                if (rig != GorillaTagger.Instance.offlineVRRig && (Vector3.Distance(GorillaTagger.Instance.leftHandTransform.position, rig.headMesh.transform.position) < Settings.Settings.TagDistance || Vector3.Distance(GorillaTagger.Instance.rightHandTransform.position, rig.headMesh.transform.position) < Settings.Settings.TagDistance))
                {
                    PhotonView photonView = GameObject.Find("Player Objects/RigCache/Network Parent/GameMode(Clone)")
                    .GetPhotonView(
                    );

                    if (photonView != null)
                    {
                        photonView.RPC("RPC_ReportTag", RpcTarget.All, new object[] { rig.Creator.ActorNumber });
                    }
                }
            }
        }

        public static async void TagSelf()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (vrrig.mainSkin.material.name.Contains("it") || vrrig.mainSkin.material.name.Contains("fected"))
                    {
                        if (!GorillaTagger.Instance.offlineVRRig.mainSkin.material.name.Contains("it") || !GorillaTagger.Instance.offlineVRRig.mainSkin.material.name.Contains("fected"))
                        {
                            GorillaTagger.Instance.offlineVRRig.enabled = false;
                            GorillaTagger.Instance.myVRRig.transform.position = vrrig.rightHandTransform.position;
                            GorillaTagger.Instance.offlineVRRig.transform.position = vrrig.rightHandTransform.position + new Vector3(UnityEngine.Random.Range(-0.05f, 0.05f), UnityEngine.Random.Range(-0.05f, 0.05f), UnityEngine.Random.Range(-0.05f, 0.05f));
                        }
                        else
                        {
                            GorillaTagger.Instance.offlineVRRig.enabled = true;
                        }

                    }
                }
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }
    }
}

