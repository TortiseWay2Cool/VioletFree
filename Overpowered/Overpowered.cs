using ExitGames.Client.Photon;
using Fusion;
using GorillaLocomotion.Gameplay;
using GorillaNetworking;
using GorillaTag.Cosmetics.Summer;
using GorillaTagScripts;
using GunTemplateee;
using Photon.Pun;
using Photon.Realtime;
using POpusCodec.Enums;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UIElements;
using VioletFree.Menu;
using VioletFree.Mods.Settings;
using VioletFree.Utilities;
using Random = UnityEngine.Random;

namespace VioletFree.Mods.Overpowered
{
    class Overpowered : MonoBehaviour
    {
        public static PhotonView photonView;
        public static void SerilizeAction(VRRig plr, Action action)
        {
            GorillaTagger.Instance.offlineVRRig.enabled = false;
            GorillaTagger.Instance.offlineVRRig.transform.SetPositionAndRotation(plr.transform.position, plr.transform.rotation);
            PhotonNetwork.SendAllOutgoingCommands();
            PhotonNetwork.RunViewUpdate();
            photonView = GameObject.Find("Player Objects/RigCache/Network Parent/GameMode(Clone)").GetPhotonView();
            action();
            GorillaTagger.Instance.offlineVRRig.enabled = true;
            PhotonNetwork.SendAllOutgoingCommands();
            PhotonNetwork.RunViewUpdate();
            typeof(PhotonView).GetMethod("OnSerialize", BindingFlags.NonPublic | BindingFlags.Instance)?.Invoke(photonView, new object[] { null, null });

            PhotonNetwork.NetworkingClient.LoadBalancingPeer.SendAcksOnly();
        }
        public static bool SendOpperation = true;
        public static int LagPower = 0;
        public static float LagDelay = 0;
        public static int LagPowerCycle = 1;

        public static void ChangeLagPower()
        {
            LagPowerCycle++;
            if (LagPowerCycle > 6)
            {
                LagPowerCycle = 1;
            }

            switch (LagPowerCycle)
            {

                case 1:
                    LagPower = 140;
                    LagDelay = 0.5f;
                    SendOpperation = true;
                    ButtonHandler.ChangeButtonText("Current Lag Power", "Current Lag Power [<color=blue>Very Weak</color>]");
                    break;
                case 2:
                    LagPower = 170;
                    LagDelay = 0.5f;
                    ButtonHandler.ChangeButtonText("Current Lag Power", "Current Lag Power [<color=blue>Medium</color>]");
                    break;
                case 3:
                    LagPower = 200;
                    LagDelay = 0.5f;
                    ButtonHandler.ChangeButtonText("Current Lag Power", "Current Lag Power [<color=blue>Strong</color>]");
                    break;
                case 4:
                    LagPower = 240;
                    LagDelay = 0.5f;
                    ButtonHandler.ChangeButtonText("Current Lag Power", "Current Lag Power [<color=blue>Strongest</color>]");
                    break;
                case 5:
                    LagPower = 600;
                    LagDelay = 2.5f;
                    ButtonHandler.ChangeButtonText("Current Lag Power", "Current Lag Power [<color=blue>Lag Spike</color>]");
                    break ;
                case 6:
                    LagPower = 240;
                    LagDelay = 0.5f;
                    SendOpperation = false;
                    ButtonHandler.ChangeButtonText("Current Lag Power", "Current Lag Power [<color=blue>Subtle</color>]");
                    break;
            }
        }

        public static void Lag(int index)
        {
            switch (index)
            {
                case 0:// all
                    if (Time.time > Variables.Delay)
                    {
                        Variables.Delay = Time.time + LagDelay;
                        for (int i = 0; i < LagPower; i++)
                        {
                            PhotonNetwork.RPC(FriendshipGroupDetection.Instance.photonView, "NotifyPartyMerging", RpcTarget.Others, true, new object[1]);
                        }
                        if (SendOpperation) PhotonNetwork.NetworkingClient.LoadBalancingPeer.SendOutgoingCommands();
                    }
                    if (Time.time > Variables.Delay)
                    {
                        Variables.Delay = Time.time + 0.5f;
                        Saftey.KawaiiRPC();
                    }
                    break;


                case 1: // locked Player
                    GunTemplate.StartBothGuns(() =>
                    {
                        if (Time.time > Variables.Delay)
                        {
                            Variables.Delay = Time.time + LagDelay;

                            for (int i = 0; i < LagPower; i++)
                            {
                                PhotonNetwork.RPC(FriendshipGroupDetection.Instance.photonView, "NotifyPartyMerging", RigManager.GetPlayerFromVRRig(GunTemplate.lockedPlayer), true, new object[1]); 

                            }
                            if (SendOpperation) PhotonNetwork.NetworkingClient.LoadBalancingPeer.SendOutgoingCommands();
                        }
                        if (Time.time > Variables.Delay)
                        {
                            Variables.Delay = Time.time + 0.5f;
                            Saftey.KawaiiRPC();

                        }
                    }, true);
                    break;


                case 2: // On Touch
                    GameObject yuh = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    yuh.transform.position = GorillaTagger.Instance.leftHandTransform.position;
                    yuh.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;
                    yuh.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    yuh.GetComponent<Renderer>().material.shader = ColorLib.guiShader;
                    yuh.GetComponent<Renderer>().material.color = ColorLib.DeepVioletTransparent;
                    Destroy(yuh.GetComponent<Collider>());

                    GameObject yuh2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    yuh2.transform.position = GorillaTagger.Instance.rightHandTransform.position;
                    yuh2.transform.rotation = GorillaTagger.Instance.rightHandTransform.rotation;
                    yuh2.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    Destroy(yuh2.GetComponent<Collider>());
                    yuh2.GetComponent<Renderer>().material.shader = ColorLib.guiShader;
                    yuh2.GetComponent<Renderer>().material.color = ColorLib.DeepVioletTransparent;
                    Destroy(yuh, Time.deltaTime);
                    Destroy(yuh2, Time.deltaTime);
                    foreach (VRRig rig in GorillaParent.instance.vrrigs)
                    {

                        if (rig != GorillaTagger.Instance.offlineVRRig && (Vector3.Distance(GorillaTagger.Instance.leftHandTransform.position, rig.headMesh.transform.position) < 0.9f || Vector3.Distance(GorillaTagger.Instance.rightHandTransform.position, rig.headMesh.transform.position) < 0.9f))
                        {
                            if (Time.time > Variables.Delay)
                            {
                                Variables.Delay = Time.time + LagDelay;

                                for (int i = 0; i < LagPower; i++)
                                {
                                    PhotonNetwork.RPC(FriendshipGroupDetection.Instance.photonView, "NotifyPartyMerging", RigManager.GetPlayerFromVRRig(rig), true, new object[1]);
                                }
                                if (SendOpperation) PhotonNetwork.NetworkingClient.LoadBalancingPeer.SendOutgoingCommands();
                            }
                            if (Time.time > Variables.Delay)
                            {
                                Variables.Delay = Time.time + 0.5f;
                                Saftey.KawaiiRPC();
                            }
                        }
                    }
                    break;


                case 3: // On They Touch
                    foreach (VRRig rig in GorillaParent.instance.vrrigs)
                    {
                        GameObject LHand = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        LHand.transform.position = rig.leftHandTransform.position;
                        LHand.transform.rotation = rig.leftHandTransform.rotation;
                        LHand.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                        LHand.GetComponent<Renderer>().material.shader = ColorLib.guiShader;
                        LHand.GetComponent<Renderer>().material.color = ColorLib.DeepVioletTransparent;
                        Destroy(LHand.GetComponent<Collider>());

                        GameObject Rhand = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        Rhand.transform.position = rig.rightHandTransform.position;
                        Rhand.transform.rotation = rig.rightHandTransform.rotation;
                        Rhand.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                        Destroy(Rhand.GetComponent<Collider>());
                        Rhand.GetComponent<Renderer>().material.shader = ColorLib.guiShader;
                        Rhand.GetComponent<Renderer>().material.color = ColorLib.DeepVioletTransparent;
                        Destroy(LHand, Time.deltaTime);
                        Destroy(Rhand, Time.deltaTime);
                        if (rig != GorillaTagger.Instance.offlineVRRig && (Vector3.Distance(rig.leftHandTransform.position, GorillaTagger.Instance.offlineVRRig.headMesh.transform.position) < 0.9f || Vector3.Distance(rig.rightHandTransform.position, GorillaTagger.Instance.offlineVRRig.headMesh.transform.position) < 0.9f))
                        {
                            if (Time.time > Variables.Delay)
                            {
                                Variables.Delay = Time.time + LagDelay;

                                for (int i = 0; i < LagPower; i++)
                                {
                                    PhotonNetwork.RPC(FriendshipGroupDetection.Instance.photonView, "NotifyPartyMerging", RigManager.GetPlayerFromVRRig(rig), true, new object[1]);
                                }
                                if (SendOpperation) PhotonNetwork.NetworkingClient.LoadBalancingPeer.SendOutgoingCommands();
                            }
                            if (Time.time > Variables.Delay)
                            {
                                Variables.Delay = Time.time + 0.5f;
                                Saftey.KawaiiRPC();

                            }
                        }
                    }
                    break;


                case 4: // Your Aura
                    foreach (VRRig rig in GorillaParent.instance.vrrigs)
                    {
                        if (rig != GorillaTagger.Instance.offlineVRRig && (Vector3.Distance(GorillaTagger.Instance.leftHandTransform.position, rig.headMesh.transform.position) < 4f || Vector3.Distance(GorillaTagger.Instance.rightHandTransform.position, rig.headMesh.transform.position) < 4f))
                        {
                            if (Time.time > Variables.Delay)
                            {
                                Variables.Delay = Time.time + LagDelay;

                                for (int i = 0; i < LagPower; i++)
                                {
                                    PhotonNetwork.RPC(FriendshipGroupDetection.Instance.photonView, "NotifyPartyMerging", RigManager.GetPlayerFromVRRig(rig), true, new object[1]); 
                                }
                                if (SendOpperation) PhotonNetwork.NetworkingClient.LoadBalancingPeer.SendOutgoingCommands();
                            }
                            if (Time.time > Variables.Delay)
                            {
                                Variables.Delay = Time.time + 0.5f;
                                Saftey.KawaiiRPC();

                            }
                        }
                    }
                    break;


                case 5:// There Aura
                    foreach (VRRig rig in GorillaParent.instance.vrrigs)
                    {
                        if (rig != GorillaTagger.Instance.offlineVRRig && (Vector3.Distance(rig.leftHandTransform.position, GorillaTagger.Instance.offlineVRRig.headMesh.transform.position) < 4f || Vector3.Distance(rig.rightHandTransform.position, GorillaTagger.Instance.offlineVRRig.headMesh.transform.position) < 4f))
                        {
                            if (Time.time > Variables.Delay)
                            {
                                Variables.Delay = Time.time + LagDelay;

                                for (int i = 0; i < LagPower; i++)
                                {
                                    PhotonNetwork.RPC(FriendshipGroupDetection.Instance.photonView, "NotifyPartyMerging", RigManager.GetPlayerFromVRRig(rig), true, new object[1]); 
                                }
                                if (SendOpperation) PhotonNetwork.NetworkingClient.LoadBalancingPeer.SendOutgoingCommands();
                            }
                            if (Time.time > Variables.Delay)
                            {
                                Variables.Delay = Time.time + 0.5f;
                                Saftey.KawaiiRPC();

                            }
                        }
                    }
                    break;
            }
        }


        public static void ElevatorTpGun()
        {
            GunTemplate.StartBothGuns(() =>
            {
                GRElevatorManager._instance.SendRPC("RemoteActivateTeleport", GunTemplate.lockedPlayer.creator.ActorNumber, new object[]
                {
                    GRElevatorManager.ElevatorLocation.Stump,
                    GRElevatorManager.ElevatorLocation.GhostReactor, // You Can Change Where You Want it to Go
                    PhotonNetwork.LocalPlayer.actorNumber
                }); 
            }, true);
        }

        public static void ElevatorTpAll()
        {
            if (Time.time > Variables.Delay)
            {
                Variables.Delay = Time.time + 0.5f;
                GRElevatorManager._instance.SendRPC("RemoteActivateTeleport", RpcTarget.Others, new object[]
                {
                    GRElevatorManager.ElevatorLocation.Stump,
                    GRElevatorManager.ElevatorLocation.GhostReactor, // You Can Change Where You Want it to Go
                    PhotonNetwork.LocalPlayer.actorNumber
                });
            }
        }

        public static void DestroyGun()
        {
            GunTemplate.StartBothGuns(() =>
            {
                PhotonNetwork.OpRemoveCompleteCacheOfPlayer(RigManager.GetPlayerFromVRRig(GunTemplate.lockedPlayer).ActorNumber);
            }, true);
        }

        public static void DestroyAll()
        {
            foreach (Photon.Realtime.Player plr in PhotonNetwork.PlayerList)
            {
                PhotonNetwork.OpRemoveCompleteCacheOfPlayer(RigManager.GetPlayerFromVRRig(GunTemplate.lockedPlayer).ActorNumber);
            }
        }

        public static async void KickAllInStump()
        {
            string previousRoom = PhotonNetwork.CurrentRoom.name;
            GorillaComputer.instance.OnGroupJoinButtonPress(0, GorillaComputer.instance.friendJoinCollider);
            await Task.Delay(8000);
            NotificationLib.SendNotification("Rejoining now");
            while (PhotonNetwork.CurrentRoom != null && PhotonNetwork.CurrentRoom.name != previousRoom)
            {
                PhotonNetworkController.Instance.AttemptToJoinSpecificRoom(previousRoom, GorillaNetworking.JoinType.Solo);
                await Task.Delay(1000);
            }
            
        }

        public static void SpazRopes()
        {
            if (ControllerInputPoller.instance.leftGrab)
            {
                if (Time.time > Variables.Delay + 0.1f)
                {
                    Variables.Delay = Time.time;
                    foreach (GorillaRopeSwing ropes in GameObject.FindObjectsOfType<GorillaRopeSwing>())
                    {
                        RopeSwingManager.instance.photonView.RPC("SetVelocity", RpcTarget.All, ropes.ropeId, 1, new Vector3(UnityEngine.Random.Range(-50f, 50f), UnityEngine.Random.Range(-50f, 50f), UnityEngine.Random.Range(-50f, 50f)), true);
                    }
                }
            }
        }

        public static void UpRopes()
        {
            if (ControllerInputPoller.instance.leftGrab)
            {
                if (Time.time > Variables.Delay + 0.1f)
                {
                    Variables.Delay = Time.time;
                    foreach (GorillaRopeSwing ropes in GameObject.FindObjectsOfType<GorillaRopeSwing>())
                    {
                        RopeSwingManager.instance.photonView.RPC("SetVelocity", RpcTarget.All, ropes.ropeId, 1, new Vector3(0, 100, 0), true);
                    }
                }
            }
        }

        public static void QuitAppGun()
        {
            GunTemplate.StartBothGuns(() =>
            {
                if (Time.time > Variables.Delay + 0.1f)
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = false;
                    GorillaTagger.Instance.offlineVRRig.transform.position = new Vector3(650, 99999, 0);
                    RigManager.GetNetworkViewFromVRRig(GunTemplate.lockedPlayer).SendRPC("GrabbedByPlayer", RpcTarget.Others, new object[] { true, false, false });
                    Saftey.KawaiiRPC();
                    Variables.Delay = Time.time;
                }
            }, true);
            GorillaTagger.Instance.offlineVRRig.enabled = true;
        }

        public static void QuitAppAll()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                foreach (VRRig rig in GorillaParent.instance.vrrigs)
                {
                    if (Time.time > Variables.Delay + 0.1f)
                    {
                        GorillaTagger.Instance.offlineVRRig.enabled = false;
                        GorillaTagger.Instance.offlineVRRig.transform.position = new Vector3(650, 99999, 0);
                        RigManager.GetNetworkViewFromVRRig(rig).SendRPC("GrabbedByPlayer", RpcTarget.Others, new object[] { true, false, false });
                        Saftey.KawaiiRPC();
                        Variables.Delay = Time.time;
                    }
                }
            }
            GorillaTagger.Instance.offlineVRRig.enabled = true;
        }

        public static void GrabGun()
        {
            GunTemplate.StartBothGuns(() =>
            {
                if (Time.time > Variables.Delay + 0.1f)
                {
                    RigManager.GetNetworkViewFromVRRig(GunTemplate.lockedPlayer).SendRPC("GrabbedByPlayer", RpcTarget.Others, new object[] { true, false, false });
                    Saftey.KawaiiRPC();
                    Variables.Delay = Time.time;
                }
            }, true);
         GorillaTagger.Instance.offlineVRRig.enabled = true;
        }

        public static void GrabAll()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                foreach (VRRig rig in GorillaParent.instance.vrrigs)
                {
                    if (Time.time > Variables.Delay + 0.1f)
                    {
                        RigManager.GetNetworkViewFromVRRig(rig).SendRPC("GrabbedByPlayer", RpcTarget.Others, new object[] { true, false, false });
                        Saftey.KawaiiRPC();
                        Variables.Delay = Time.time;
                    }
                }
            }
            GorillaTagger.Instance.offlineVRRig.enabled = true;
        }
    }
}
