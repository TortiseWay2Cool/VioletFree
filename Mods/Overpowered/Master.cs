using ExitGames.Client.Photon;
using GorillaGameModes;
using GorillaTag.Cosmetics.Summer;
using GunTemplateee;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VioletFree.Utilities;
using static OVRPlugin;

namespace VioletFree.Mods.Overpowered
{
    class Master : MonoBehaviour
    {

        public static void SlowGun()
        {
            GunTemplate.StartBothGuns(() =>
            {
                if (PhotonNetwork.MasterClient != RigManager.GetPlayerFromVRRig(GorillaTagger.Instance.offlineVRRig))
                    return;

                if (Time.time > Variables.Delay)
                {
                    Variables.Delay = Time.time + 0.1f;
                    RoomSystem.SendStatusEffectToPlayer(RoomSystem.StatusEffects.SetSlowedTime, RigManager.GetNetPlayerFromVRRig(GunTemplate.lockedPlayer));
                }
            }, true);
        }

        public static void SlowAll()
        {
            if (Time.time > Variables.Delay)
            {
                if (PhotonNetwork.MasterClient != RigManager.GetPlayerFromVRRig(GorillaTagger.Instance.offlineVRRig))
                    return;

                Variables.Delay = Time.time + 0.1f;
                RoomSystem.SendStatusEffectAll(RoomSystem.StatusEffects.SetSlowedTime);
            }
        }

        public static void VibrateGun()
        {
            GunTemplate.StartBothGuns(() =>
            {
                if (PhotonNetwork.MasterClient != RigManager.GetPlayerFromVRRig(GorillaTagger.Instance.offlineVRRig))
                    return;

                if (Time.time > Variables.Delay)
                {
                    Variables.Delay = Time.time + 0.1f;
                    RoomSystem.SendStatusEffectToPlayer(RoomSystem.StatusEffects.JoinedTaggedTime, RigManager.GetNetPlayerFromVRRig(GunTemplate.lockedPlayer));
                }
            }, true);
        }

        public static void VibrateAll()
        {
            if (Time.time > Variables.Delay)
            {
                if (PhotonNetwork.MasterClient != RigManager.GetPlayerFromVRRig(GorillaTagger.Instance.offlineVRRig))
                    return;

                Variables.Delay = Time.time + 0.1f;
                RoomSystem.SendStatusEffectAll(RoomSystem.StatusEffects.JoinedTaggedTime);
            }
        }

        public static void RoseToySelf()
        {
            if (Time.time > Variables.Delay)
            {
                if (PhotonNetwork.MasterClient != RigManager.GetPlayerFromVRRig(GorillaTagger.Instance.offlineVRRig))
                    return;

                Variables.Delay = Time.time + 0.1f;
                RoomSystem.SendStatusEffectToPlayer(RoomSystem.StatusEffects.JoinedTaggedTime, PhotonNetwork.LocalPlayer);
            }
        }
        public static void TagPlayerMaster(Photon.Realtime.Player plr, bool infect)
        {
            if (PhotonNetwork.MasterClient != RigManager.GetPlayerFromVRRig(GorillaTagger.Instance.offlineVRRig))
                return;

            GorillaTagManager infection = GorillaTagManager.instance.gameObject.GetComponent<GorillaTagManager>();
            if (infect)
            {
                infection.currentInfected.Add(plr);
                infection.AddInfectedPlayer(plr, true);
            }
            else
            {
                if (!infection.isCurrentlyTag)
                {
                    infection.currentInfected.Remove(plr);
                }
                else
                {
                    infection.currentIt = null;
                }
            }
        }

        public static void TagGunMaster()
        {
            GunTemplate.StartBothGuns(() =>
            {
                TagPlayerMaster(RigManager.GetPlayerFromVRRig(GunTemplate.lockedPlayer), true);
            }, true);
        }

        public static void TagAllMaster()
        {
            foreach (Photon.Realtime.Player plr in PhotonNetwork.PlayerList)
            {
                TagPlayerMaster(plr, true);
            }
        }

        public static void TagSelfMaster()
        {
            TagPlayerMaster(PhotonNetwork.LocalPlayer, true);
        }

        public static void TagAuraMaster()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {

                if (rig != GorillaTagger.Instance.offlineVRRig && (Vector3.Distance(GorillaTagger.Instance.leftHandTransform.position, rig.headMesh.transform.position) < 4f || Vector3.Distance(GorillaTagger.Instance.rightHandTransform.position, rig.headMesh.transform.position) < 4f))
                {
                    TagPlayerMaster(RigManager.GetPlayerFromVRRig(rig), true);
                }
            }
        }

        public static void TagReachMaster()
        {
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
                    TagPlayerMaster(RigManager.GetPlayerFromVRRig(rig), true);
                }
            }
        }

        public static void UnTagGunMaster()
        {
            GunTemplate.StartBothGuns(() =>
            {
                TagPlayerMaster(RigManager.GetPlayerFromVRRig(GunTemplate.lockedPlayer), false);
            }, true);
        }

        public static void UnTagAllMaster()
        {
            foreach (Photon.Realtime.Player plr in PhotonNetwork.PlayerList)
            {
                TagPlayerMaster(plr, false);
            }
        }

        public static void UnTagSelfMaster()
        {
            TagPlayerMaster(PhotonNetwork.LocalPlayer, false);
        }

        public static void UnTagAuraMaster()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {

                if (rig != GorillaTagger.Instance.offlineVRRig && (Vector3.Distance(GorillaTagger.Instance.leftHandTransform.position, rig.headMesh.transform.position) < 4f || Vector3.Distance(GorillaTagger.Instance.rightHandTransform.position, rig.headMesh.transform.position) < 4f))
                {
                    TagPlayerMaster(RigManager.GetPlayerFromVRRig(rig), false);
                }
            }
        }

        public static void UnTagReachMaster()
        {
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
                    TagPlayerMaster(RigManager.GetPlayerFromVRRig(rig), false);
                }
            }
        }

        public static void StopGamemode()
        {
            if (PhotonNetwork.MasterClient != RigManager.GetPlayerFromVRRig(GorillaTagger.Instance.offlineVRRig))
                return;

            GorillaGameManager infect = GorillaGameManager.instance.gameObject.GetComponent<GorillaGameManager>();
            infect.StopPlaying();
            infect.Reset();
        }

        public static void RestartGamemode()
        {
            if (PhotonNetwork.MasterClient != RigManager.GetPlayerFromVRRig(GorillaTagger.Instance.offlineVRRig))
                return;

            GorillaGameManager infect = GorillaGameManager.instance.gameObject.GetComponent<GorillaGameManager>();

            infect.StopPlaying();
            infect.Reset();
            infect.StartPlaying();
        }

        public static void startgamemode()
        {
            if (PhotonNetwork.MasterClient != RigManager.GetPlayerFromVRRig(GorillaTagger.Instance.offlineVRRig))
                return;

            GorillaGameManager yu = GorillaGameManager.instance.gameObject.GetComponent<GorillaGameManager>();
            yu.StartPlaying();
        }

        public static async void SpazGamemode()
        {
            if (PhotonNetwork.MasterClient != RigManager.GetPlayerFromVRRig(GorillaTagger.Instance.offlineVRRig))
                return;

            if (ControllerInputPoller.instance.rightGrab)
            {
                GorillaGameManager infect = GorillaGameManager.instance.gameObject.GetComponent<GorillaGameManager>();
                infect.StopPlaying();
                infect.Reset();
                await Task.Delay(400);
                infect.StartPlaying();
                
            }
        }

        public static void RockMonkeGamemode()
        {
            if (PhotonNetwork.MasterClient != RigManager.GetPlayerFromVRRig(GorillaTagger.Instance.offlineVRRig))
                return;

            GorillaTagManager pluh = GorillaTagManager.instance.gameObject.GetComponent<GorillaTagManager>();
            pluh.infectedModeThreshold = 0;
        }

        public static void InfectionMonkeGamemode()
        {
            if (PhotonNetwork.MasterClient != RigManager.GetPlayerFromVRRig(GorillaTagger.Instance.offlineVRRig))
                return;

            GorillaTagManager pluh = GorillaTagManager.instance.gameObject.GetComponent<GorillaTagManager>();
            pluh.infectedModeThreshold = 99;
        }

        public enum GhostReactorEntities
        {
            EnemyChaser = 1534537551,
            EnemyChaserArmored = 48354877,
            EnemyRanged = -1716223397,
            EnemyRangedArmored = -144412770,
            EnemyPhantom = -509453360,
            EnemyPest = -2080328249,
            ToolFlash = 225241881,
            CollectibleCore = 166197108,
            ToolClub = -175001459,
            ToolCollector = 1989693521,
            ToolRevive = 1165678479,
            BarrierSpectral = 1517967647,
            CollectibleFlower = -20887423,
            CollectibleCoreFlowerVariant = -298662477,
            EnergyCostGate = -735557236,
            IdBadge = 931983585,
            WallLight01 = -1086813702,
            BreakableCrate = 373410214,
            ToolLantern = 1115277044,
            BreakableBarrel = -1724683316,
            ToolShieldGun = -1495476618,
            CustomMapsAIAgent = -562623891
        }
        public static void CreateEntity(Master.GhostReactorEntities entity)
        {
            if (ControllerInputPoller.instance.rightGrab && PhotonNetwork.IsMasterClient)
            {
                if (Time.time > Variables.Delay)
                {
                    Variables.Delay = Time.time + 0.1f;
                    int netId = (int)entity;
                    GhostReactorManager.instance.gameEntityManager.photonView.RPC("CreateItemRPC", RpcTarget.All, new object[]
                    {
                    new int[] { netId },
                    new int[] { (int)GTZone.ghostReactor },
                    new int[] {  netId},
                    new long[] { BitPackUtils.PackWorldPosForNetwork(GorillaTagger.Instance.rightHandTransform.transform.position) },
                    new int[] { BitPackUtils.PackQuaternionForNetwork(GorillaTagger.Instance.rightHandTransform.rotation) },
                    new long[] { 0L }
                    });

                }
            }
        }
    }
}
