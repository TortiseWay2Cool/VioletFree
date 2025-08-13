using ExitGames.Client.Photon;
using Meta.WitAi.Json;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.Networking;
using VioletFree.Mods.Spammers;
using VioletFree.Utilities;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace VioletFree.Mods.Vissual
{
    class Networked : MonoBehaviour
    {
        public static void SendWeb(string message)
        {

        }

        

        public static void LaunchSlingShotNetworked()
        {
            if (PhotonNetwork.InRoom)
            {
                if (ControllerInputPoller.instance.rightGrab)
                {
                    Projectiles.CSProjectile(-820530352, GorillaTagger.Instance.offlineVRRig.rightHandTransform.up * 20, GorillaTagger.Instance.offlineVRRig.rightHandTransform.position, GorillaTagger.Instance.offlineVRRig.rightHandTransform.rotation, ColorLib.Red);
                }

                else
                {
                    ExitGames.Client.Photon.Hashtable table = Photon.Pun.PhotonNetwork.LocalPlayer.CustomProperties;
                    table.Add("Shooting", true);
                    Photon.Pun.PhotonNetwork.LocalPlayer.CustomProperties.Remove(table);
                }
            }
        }

        public static void WaterBaloons()
        {
            if (PhotonNetwork.InRoom && ControllerInputPoller.instance.rightGrab)
            {
                Projectiles.CSProjectile(-1674517839, GorillaTagger.Instance.offlineVRRig.rightHandTransform.up * 20,
                    GorillaTagger.Instance.offlineVRRig.rightHandTransform.position,
                    GorillaTagger.Instance.offlineVRRig.rightHandTransform.rotation, ColorLib.Red);
            }
            else
            {
                ExitGames.Client.Photon.Hashtable table = Photon.Pun.PhotonNetwork.LocalPlayer.CustomProperties;
                table.Add("Shooting", true);
                Photon.Pun.PhotonNetwork.LocalPlayer.CustomProperties.Remove(table);
            }
        }

        public static void IceCream()
        {
            if (PhotonNetwork.InRoom && ControllerInputPoller.instance.rightGrab)
            {
                Projectiles.CSProjectile(-1852289989, GorillaTagger.Instance.offlineVRRig.rightHandTransform.up * 20,
                    GorillaTagger.Instance.offlineVRRig.rightHandTransform.position,
                    GorillaTagger.Instance.offlineVRRig.rightHandTransform.rotation, ColorLib.Red);
            }
            else
            {
                ExitGames.Client.Photon.Hashtable table = Photon.Pun.PhotonNetwork.LocalPlayer.CustomProperties;
                table.Add("Shooting", true);
                Photon.Pun.PhotonNetwork.LocalPlayer.CustomProperties.Remove(table);
            }
        }


        public static void DeadShot()
        {
            if (PhotonNetwork.InRoom && ControllerInputPoller.instance.rightGrab)
            {
                Projectiles.CSProjectile(693334698, GorillaTagger.Instance.offlineVRRig.rightHandTransform.up * 20,
                    GorillaTagger.Instance.offlineVRRig.rightHandTransform.position,
                    GorillaTagger.Instance.offlineVRRig.rightHandTransform.rotation, ColorLib.Red);
            }
            else
            {
                ExitGames.Client.Photon.Hashtable table = Photon.Pun.PhotonNetwork.LocalPlayer.CustomProperties;
                table.Add("Shooting", true);
                Photon.Pun.PhotonNetwork.LocalPlayer.CustomProperties.Remove(table);
            }
        }

        public static void PresentV1()
        {
            if (PhotonNetwork.InRoom && ControllerInputPoller.instance.rightGrab)
            {
                Projectiles.CSProjectile(-160604350, GorillaTagger.Instance.offlineVRRig.rightHandTransform.up * 20,
                    GorillaTagger.Instance.offlineVRRig.rightHandTransform.position,
                    GorillaTagger.Instance.offlineVRRig.rightHandTransform.rotation, ColorLib.Red);
            }
            else
            {
                ExitGames.Client.Photon.Hashtable table = Photon.Pun.PhotonNetwork.LocalPlayer.CustomProperties;
                table.Add("Shooting", true);
                Photon.Pun.PhotonNetwork.LocalPlayer.CustomProperties.Remove(table);
            }
        }

        public static void Firework()
        {
            if (PhotonNetwork.InRoom && ControllerInputPoller.instance.rightGrab)
            {
                Projectiles.CSProjectile(2043643338, GorillaTagger.Instance.offlineVRRig.rightHandTransform.up * 20,
                    GorillaTagger.Instance.offlineVRRig.rightHandTransform.position,
                    GorillaTagger.Instance.offlineVRRig.rightHandTransform.rotation, ColorLib.Red);
            }
            else
            {
                ExitGames.Client.Photon.Hashtable table = Photon.Pun.PhotonNetwork.LocalPlayer.CustomProperties;
                table.Add("Shooting", true);
                Photon.Pun.PhotonNetwork.LocalPlayer.CustomProperties.Remove(table);
            }
        }

        public static void CloundSlingshot()
        {
            if (PhotonNetwork.InRoom && ControllerInputPoller.instance.rightGrab)
            {
                Projectiles.CSProjectile(1511318966,
                    GorillaTagger.Instance.offlineVRRig.rightHandTransform.up * 20,
                    GorillaTagger.Instance.offlineVRRig.rightHandTransform.position,
                    GorillaTagger.Instance.offlineVRRig.rightHandTransform.rotation,
                    ColorLib.Red);
            }
            else
            {
                ExitGames.Client.Photon.Hashtable table = Photon.Pun.PhotonNetwork.LocalPlayer.CustomProperties;
                table.Add("Shooting", true);
                Photon.Pun.PhotonNetwork.LocalPlayer.CustomProperties.Remove(table);
            }
        }

        public static void InVisible()
        {
            if (PhotonNetwork.InRoom && ControllerInputPoller.instance.rightGrab)
            {
                Projectiles.CSProjectile(1345490170,
                    GorillaTagger.Instance.offlineVRRig.rightHandTransform.up * 20,
                    GorillaTagger.Instance.offlineVRRig.rightHandTransform.position,
                    GorillaTagger.Instance.offlineVRRig.rightHandTransform.rotation,
                    ColorLib.Red);
            }
            else
            {
                ExitGames.Client.Photon.Hashtable table = Photon.Pun.PhotonNetwork.LocalPlayer.CustomProperties;
                table.Add("Shooting", true);
                Photon.Pun.PhotonNetwork.LocalPlayer.CustomProperties.Remove(table);
            }
        }

        public static void FireWorkV2()
        {
            if (PhotonNetwork.InRoom && ControllerInputPoller.instance.rightGrab)
            {
                Projectiles.CSProjectile(343786844,
                    GorillaTagger.Instance.offlineVRRig.rightHandTransform.up * 20,
                    GorillaTagger.Instance.offlineVRRig.rightHandTransform.position,
                    GorillaTagger.Instance.offlineVRRig.rightHandTransform.rotation,
                    ColorLib.Red);
            }
            else
            {
                ExitGames.Client.Photon.Hashtable table = Photon.Pun.PhotonNetwork.LocalPlayer.CustomProperties;
                table.Add("Shooting", true);
                Photon.Pun.PhotonNetwork.LocalPlayer.CustomProperties.Remove(table);
            }
        }

        public static void HeartSlingShot()
        {
            if (PhotonNetwork.InRoom && ControllerInputPoller.instance.rightGrab)
            {
                Projectiles.CSProjectile(825718363,
                    GorillaTagger.Instance.offlineVRRig.rightHandTransform.up * 20,
                    GorillaTagger.Instance.offlineVRRig.rightHandTransform.position,
                    GorillaTagger.Instance.offlineVRRig.rightHandTransform.rotation,
                    ColorLib.Red);
            }
            else
            {
                ExitGames.Client.Photon.Hashtable table = Photon.Pun.PhotonNetwork.LocalPlayer.CustomProperties;
                table.Add("Shooting", true);
                Photon.Pun.PhotonNetwork.LocalPlayer.CustomProperties.Remove(table);
            }
        }

        public static void FishFood()
        {
            if (PhotonNetwork.InRoom && ControllerInputPoller.instance.rightGrab)
            {
                Projectiles.CSProjectile(-1509512060,
                    GorillaTagger.Instance.offlineVRRig.rightHandTransform.up * 20,
                    GorillaTagger.Instance.offlineVRRig.rightHandTransform.position,
                    GorillaTagger.Instance.offlineVRRig.rightHandTransform.rotation,
                    ColorLib.Red);
            }
            else
            {
                ExitGames.Client.Photon.Hashtable table = Photon.Pun.PhotonNetwork.LocalPlayer.CustomProperties;
                table.Add("Shooting", true);
                Photon.Pun.PhotonNetwork.LocalPlayer.CustomProperties.Remove(table);
            }
        }

        public static void Snowball()
        {
            if (PhotonNetwork.InRoom && ControllerInputPoller.instance.rightGrab)
            {
                Projectiles.CSProjectile(-716425086,
                    GorillaTagger.Instance.offlineVRRig.rightHandTransform.up * 20,
                    GorillaTagger.Instance.offlineVRRig.rightHandTransform.position,
                    GorillaTagger.Instance.offlineVRRig.rightHandTransform.rotation,
                    ColorLib.Red);
            }
            else 
            {
                ExitGames.Client.Photon.Hashtable table = Photon.Pun.PhotonNetwork.LocalPlayer.CustomProperties;
                table.Add("Shooting", true);
                Photon.Pun.PhotonNetwork.LocalPlayer.CustomProperties.Remove(table);
            }
        }

        public static bool Running = false;

        public static void ProjectileEvent(int Hash, Vector3 velocity, Vector3 position, Quaternion rotation, Color32 color)
        {
            if (PhotonNetwork.InRoom)
            {
                PhotonNetwork.NetworkingClient.OpRaiseEvent(44, new Hashtable
                {
                    { 0, Hash },
                    { 1, velocity },
                    { 2, position }, 
                    { 3, rotation }, 
                    { 4, color },
                }, new RaiseEventOptions { Receivers = ReceiverGroup.All }, SendOptions.SendReliable);
                ExitGames.Client.Photon.Hashtable table = Photon.Pun.PhotonNetwork.LocalPlayer.CustomProperties;
                table.Add("Shooting", true);
                Photon.Pun.PhotonNetwork.LocalPlayer.SetCustomProperties(table);
            }
        }

        public static void OnEvent(EventData photonEvent)
        {
            if (photonEvent.Code == 44)
            {
                if (PhotonNetwork.InRoom)
                {
                    if (photonEvent.CustomData is Hashtable data)
                    {
                        /*int ProjectHash = (int)data[0];
                        Vector3 Velocity = (Vector3)data[1];
                        Vector3 Position = (Vector3)data[2];
                        Quaternion Rotation = (Quaternion)data[3];
                        Color32 color = (Color32)data[4];
                        Photon.Realtime.Player player = PhotonNetwork.CurrentRoom.GetPlayer(photonEvent.sender);
                        VRRig vrrig = RigManager.GetVRRigFromPlayer(player);

                        if (player.CustomProperties.ContainsKey("Shooting"))
                        {
                            ObjectPools.instance.Instantiate((int)data[0]).SetActive(true);
                            Projectiles.CSProjectile(ProjectHash, Velocity, Position, Rotation, color);
                        }
                        else { ObjectPools.instance.Instantiate((int)data[0]).SetActive(false); }
                            
                        */
                    }
                }
            }
        }

        private void Start()
        {
            SendWeb($"{PhotonNetwork.LocalPlayer.NickName} has loaded into the game with Violet");
            PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
            ExitGames.Client.Photon.Hashtable table = Photon.Pun.PhotonNetwork.LocalPlayer.CustomProperties;
            table.Add("VioletFreeUser", true);
            Photon.Pun.PhotonNetwork.LocalPlayer.SetCustomProperties(table);
            Settings.Settings.ChangeProjectileColor();
            Settings.Settings.ChangeProjectileType();
            Settings.Settings.ChangeProjectileSpeed();
            Overpowered.Overpowered.ChangeLagPower();
        }
        private static int i = 0;

    }
}
