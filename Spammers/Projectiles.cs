using ExitGames.Client.Photon;
using GorillaTag.Cosmetics.Summer;
using GunTemplateee;
using ModIO.Implementation;
using Oculus.Interaction.Body.Input;
using Photon.Pun;
using Photon.Realtime;
using Photon.Voice;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;
using VioletFree.Menu;
using VioletFree.Mods.Player;
using VioletFree.Mods.Settings;
using VioletFree.Mods.Settings;
using VioletFree.Mods.Vissual;
using VioletFree.Utilities;
using static CrittersRigActorSetup;

namespace VioletFree.Mods.Spammers
{
    class Projectiles : MonoBehaviour
    {
        public static GameObject yuh;
        public static GorillaVelocityEstimator velocitygm;
        public static bool yuhhh;
        public static bool projModsEnabled;
        public static float projectileDelay;
        public static int Size = 1;
        public static bool ProjectileDecider;
        public static bool RGB;
        public static SnowballThrowable GetProjectile()
        {
            return GameObject.Find(VioletFree.Mods.Settings.Settings.ProjectileType).transform.Find(VioletFree.Mods.Settings.Settings.ProjectileCode).GetComponent<SnowballThrowable>();
        }

        public static void LaunchProjectile(Vector3 velocity, Vector3 position, bool Rhand)
        {
            SnowballThrowable projectile = GetProjectile();
            if (!projectile.gameObject.activeSelf) projectile.SetSnowballActiveLocal(true); projectile.transform.SetPositionAndRotation(position, new Quaternion());
            if (Rhand) GorillaTagger.Instance.offlineVRRig.RightThrowableProjectileIndex = VioletFree.Mods.Settings.Settings.projectileNumber;
            else GorillaTagger.Instance.offlineVRRig.LeftThrowableProjectileIndex = VioletFree.Mods.Settings.Settings.projectileNumber;
            GorillaTagger.Instance.offlineVRRig.LeftThrowableProjectileColor = VioletFree.Mods.Settings.Settings.ProjectileColor;
            GorillaTagger.Instance.offlineVRRig.RightThrowableProjectileColor = VioletFree.Mods.Settings.Settings.ProjectileColor;
            if (Time.time > projectileDelay)
            {
                projectileDelay = Time.time + 0.32f;
                object[] parameters = new object[]
                {
                    position,
                    velocity,
                    Rhand ? RoomSystem.ProjectileSource.RightHand : RoomSystem.ProjectileSource.LeftHand,
                    projectile.LaunchSnowballLocal(position, velocity, 1, true, VioletFree.Mods.Settings.Settings.ProjectileColor).myProjectileCount,
                    true,
                    VioletFree.Mods.Settings.Settings.ProjectileColor.r,
                    VioletFree.Mods.Settings.Settings.ProjectileColor.g,
                    VioletFree.Mods.Settings.Settings.ProjectileColor.b,
                    VioletFree.Mods.Settings.Settings.ProjectileColor.a
                };
                RoomSystem.SendEvent(0, parameters, new NetEventOptions { Reciever = NetEventOptions.RecieverTarget.others, }, false);
                Saftey.KawaiiRPC();
            }
        }

        public static void CSProjectile(int Hash,Vector3 velocity, Vector3 position, Quaternion rotation, Color32 color)
        {
            GameObject go = ObjectPools.instance.Instantiate(Hash);
            go.transform.localScale = Vector3.one * 1;
            go.GetComponent<SlingshotProjectile>().Launch(
                position,
                velocity,
                PhotonNetwork.LocalPlayer,
                false,
                false,
                -1,
                1f,
                true,
                color
                );
            Networked.ProjectileEvent(Hash, velocity, position, rotation, color);
        }

        public static void EnableAllProjs()
        {
            string[] array = new string[]
            {
                "Player Objects/Local VRRig/Local Gorilla Player/Holdables/GrowingSnowballRightAnchor(Clone)/LMACF. RIGHT.",
                "Player Objects/Local VRRig/Local Gorilla Player/Holdables/GrowingSnowballLeftAnchor(Clone)/LMACF. LEFT.",
                "Player Objects/Local VRRig/Local Gorilla Player/Holdables/GrowingSnowballRightAnchor(Clone)",
                "Player Objects/Local VRRig/Local Gorilla Player/Holdables/WaterBalloonRightAnchor(Clone)/LMAEY. RIGHT.",
                "Player Objects/Local VRRig/Local Gorilla Player/Holdables/TrickTreatFunctionalAnchorRIGHT Variant(Clone)/LMAMO. RIGHT.",
                "Player Objects/Local VRRig/Local Gorilla Player/Holdables/AppleRightAnchor(Clone)/LMAMV.",
                "Player Objects/Local VRRig/Local Gorilla Player/Holdables/ScienceCandyRightAnchor(Clone)/LMAIF. RIGHT.",
                "Player Objects/Local VRRig/Local Gorilla Player/Holdables/BucketGiftFunctionalAnchor_Right(Clone)/LMAHR. RIGHT.",
                "Player Objects/Local VRRig/Local Gorilla Player/Holdables/VotingRockAnchor_RIGHT(Clone)/LMAMT. RIGHT.",
                "Player Objects/Local VRRig/Local Gorilla Player/Holdables/FishFoodRightAnchor(Clone)/LMAIP. RIGHT.",
                "Player Objects/Local VRRig/Local Gorilla Player/Holdables/GrowingSnowballRightAnchor(Clone)/LMACF. RIGHT.",
                "Player Objects/Local VRRig/Local Gorilla Player/Holdables/GrowingSnowballRightAnchor(Clone)/LMAGD. LEFT.",
                "Player Objects/Local VRRig/Local Gorilla Player/Holdables/BookRightAnchor(Clone)/LMAQA. RIGHT.",
                "Player Objects/Local VRRig/Local Gorilla Player/Holdables/CoinRightAnchor(Clone)/LMAQC.",
                "Player Objects/Local VRRig/Local Gorilla Player/Holdables/EggRightHand_Anchor Variant(Clone)/LMAPS. RIGHT.",
                "Player Objects/Local VRRig/Local Gorilla Player/Holdables/IceCreamRightAnchor(Clone)/LMARA. LEFT.",
                "Player Objects/Local VRRig/Local Gorilla Player/Holdables/HotDogRightAnchor(Clone)/LMARC.",
                "Player Objects/Local VRRig/Local Gorilla Player/Holdables/Fireworks_Anchor Variant_Right Hand(Clone)/LMAQU. LEFT.",
                "Player Objects/Local VRRig/Local Gorilla Player/Holdables/LavaRockAnchor(Clone)/LMAGE. RIGHT.",

            };
            string[] array2 = new string[]
            {
                "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/GrowingSnowballRightAnchor(Clone)/LMACF. RIGHT.",
                "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/FireworkMortarRightAnchor(Clone)/LMAEW. RIGHT.",
                "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/WaterBalloonRightAnchor(Clone)/LMAEY. RIGHT.",
                "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/TrickTreatFunctionalAnchorRIGHT Variant(Clone)/LMAMO. RIGHT.",
                "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/AppleRightAnchor(Clone)/LMAMV.",
                "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/ScienceCandyRightAnchor(Clone)/LMAIF. RIGHT.",
                "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/BucketGiftFunctionalAnchor_Right(Clone)/LMAHR. RIGHT.",
                "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/VotingRockAnchor_RIGHT(Clone)/LMAMT. RIGHT.",
                "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/FishFoodRightAnchor(Clone)/LMAIP. RIGHT.",
                "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/LavaRockAnchor(Clone)/LMAGD. LEFT.",
                "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/BookRightAnchor(Clone)/LMAQA. RIGHT.",
                "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/CoinRightAnchor(Clone)/LMAQC.",
                "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/EggRightHand_Anchor Variant(Clone)/LMAPS. RIGHT.",
                "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/IceCreamRightAnchor(Clone)/LMARA. LEFT.",
                "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/HotDogRightAnchor(Clone)/LMARC.",
                "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/Fireworks_Anchor Variant_Right Hand(Clone)/LMAQU. LEFT.",
                "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/LavaRockAnchor(Clone)/LMAGE. RIGHT.",

            };
            foreach (string text in array)
            {
                GameObject gameObject = GameObject.Find(text);
                if (gameObject != null)
                {
                    gameObject.SetActive(true);
                }
            }
            foreach (string text2 in array2)
            {
                GameObject gameObject2 = GameObject.Find(text2);
                if (gameObject2 != null)
                {
                    gameObject2.SetActive(false);
                }
            }
            projModsEnabled = true;
        }

        public static void LaunchProjectile()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                if (projModsEnabled)
                {
                    LaunchProjectile(GorillaTagger.Instance.offlineVRRig.rightHandTransform.transform.up * VioletFree.Mods.Settings.Settings.ProjectileSpeed, GorillaTagger.Instance.offlineVRRig.rightHandTransform.transform.position, true);
                }
                else
                {
                    EnableAllProjs();
                }
            }
            else if (ControllerInputPoller.instance.leftGrab)
            {
                if (projModsEnabled)
                {
                    LaunchProjectile(GorillaTagger.Instance.offlineVRRig.leftHandTransform.transform.up * VioletFree.Mods.Settings.Settings.ProjectileSpeed, GorillaTagger.Instance.offlineVRRig.leftHandTransform.transform.position, false);
                }
                else
                {
                    EnableAllProjs();
                }
            }
            else
            {
                GetProjectile().SetSnowballActiveLocal(false);
            }
        }
        public static void GrabProjectile()
        {

            if (ControllerInputPoller.instance.rightGrab)
            {
                if (projModsEnabled)
                {
                    LaunchProjectile(new Vector3(), GorillaTagger.Instance.offlineVRRig.rightHandTransform.transform.position, true);
                }
                else
                {
                    EnableAllProjs();
                }
            }
            else
            {
                GetProjectile().SetSnowballActiveLocal(false);
            }

            if (ControllerInputPoller.instance.leftGrab)
            {
                if (projModsEnabled)
                {
                    LaunchProjectile(new Vector3(), GorillaTagger.Instance.offlineVRRig.leftHandTransform.transform.position, false);
                }
                else
                {
                    EnableAllProjs();
                }
            }
            else
            {
                GetProjectile().SetSnowballActiveLocal(false);
            }
        }

        public static void Pee()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                if (projModsEnabled)
                {
                    Settings.Settings.projectileNumber = 0;
                    Settings.Settings.ProjectileType = "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/GrowingSnowballRightAnchor(Clone)";
                    Settings.Settings.ProjectileCode = "LMACF. RIGHT.";
                    Settings.Settings.ProjectileColor = Color.yellow;
                    LaunchProjectile(GorillaTagger.Instance.offlineVRRig.transform.forward * 8, GorillaTagger.Instance.offlineVRRig.transform.position + new Vector3(0, -0.4f, 0), false);
                }
                else
                {
                    EnableAllProjs();
                }
            }
            else
            {
                GetProjectile().SetSnowballActiveLocal(false);
            }
        }

        public static void Cum()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                if (projModsEnabled)
                {
                    Settings.Settings.projectileNumber = 0;
                    Settings.Settings.ProjectileType = "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/GrowingSnowballRightAnchor(Clone)";
                    Settings.Settings.ProjectileCode = "LMACF. RIGHT.";
                    Settings.Settings.ProjectileColor = Color.white;
                    LaunchProjectile(GorillaTagger.Instance.offlineVRRig.transform.forward * 8, GorillaTagger.Instance.offlineVRRig.transform.position + new Vector3(0, -0.4f, 0), false);
                }
                else
                {
                    EnableAllProjs();
                }
            }
            else
            {
                GetProjectile().SetSnowballActiveLocal(false);
            }
        }

        public static void PeriodBlood()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                if (projModsEnabled)
                {
                    Settings.Settings.projectileNumber = 0;
                    Settings.Settings.ProjectileType = "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/GrowingSnowballRightAnchor(Clone)";
                    Settings.Settings.ProjectileCode = "LMACF. RIGHT.";
                    Settings.Settings.ProjectileColor = Color.red;
                    LaunchProjectile(GorillaTagger.Instance.offlineVRRig.transform.forward * 8, GorillaTagger.Instance.offlineVRRig.transform.position + new Vector3(0, -0.4f, 0), false);
                }
                else
                {
                    EnableAllProjs();
                }
            }
            else
            {
                GetProjectile().SetSnowballActiveLocal(false);
            }
        }

        public static void Shit()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                if (projModsEnabled)
                {
                    Settings.Settings.projectileNumber = 0;
                    Settings.Settings.ProjectileType = "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/GrowingSnowballRightAnchor(Clone)";
                    Settings.Settings.ProjectileCode = "LMACF. RIGHT.";
                    Settings.Settings.ProjectileColor = ColorLib.Brown;
                    LaunchProjectile(GorillaTagger.Instance.offlineVRRig.transform.forward * 0, GorillaTagger.Instance.offlineVRRig.transform.position + new Vector3(0, -0.4f, 0), false);
                }
                else
                {
                    EnableAllProjs();
                }
            }
            else
            {
                GetProjectile().SetSnowballActiveLocal(false);
            }
        }

        public static void WetShit()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                if (projModsEnabled)
                {
                    Settings.Settings.projectileNumber = 2;
                    Settings.Settings.ProjectileType = "Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/WaterBalloonRightAnchor(Clone)";
                    Settings.Settings.ProjectileCode = "LMAEY. RIGHT.";
                    Settings.Settings.ProjectileColor = ColorLib.Brown;
                    LaunchProjectile(GorillaTagger.Instance.offlineVRRig.transform.forward * 0, GorillaTagger.Instance.offlineVRRig.transform.position + new Vector3(0, -0.4f, 0), false);
                }
                else
                {
                    EnableAllProjs();
                }
            }
            else
            {
                GetProjectile().SetSnowballActiveLocal(false);
            }
        }
        public static int enabled;
        private static bool wasRightGrabbedLastFrame;
        private static bool wasLeftGrabbedLastFrame;

        public static void GrabProjectileItem()
        {
            bool isRightGrabbed = ControllerInputPoller.instance.rightGrab;
            if (wasRightGrabbedLastFrame && !isRightGrabbed) 
            {
                if (projModsEnabled)
                {
                    enabled = 1;
                    LaunchProjectile(GorillaTagger.Instance.offlineVRRig.rightHandTransform.up * 0, GorillaTagger.Instance.offlineVRRig.rightHandTransform.position, true);
                    GetProjectile().SetSnowballActiveLocal(false);
                }
                else
                {
                    EnableAllProjs();
                }
            }
            else if (isRightGrabbed && projModsEnabled)
            {
                enabled = 0;
                GetProjectile().SetSnowballActiveLocal(true);
                GetProjectile().transform.position = GorillaTagger.Instance.offlineVRRig.rightHandTransform.transform.position;
            }

            bool isLeftGrabbed = ControllerInputPoller.instance.leftGrab;
            if (wasLeftGrabbedLastFrame && !isLeftGrabbed) 
            {
                if (projModsEnabled)
                {
                    enabled = 1;
                    LaunchProjectile(GorillaTagger.Instance.offlineVRRig.leftHandTransform.up * 0, GorillaTagger.Instance.offlineVRRig.leftHandTransform.position, false);
                    GetProjectile().SetSnowballActiveLocal(false);
                }
                else
                {
                    EnableAllProjs();
                }
            }
            else if (isLeftGrabbed && projModsEnabled) 
            {
                enabled = 0;
                GetProjectile().SetSnowballActiveLocal(true);
                GetProjectile().transform.position = GorillaTagger.Instance.offlineVRRig.leftHandTransform.transform.position;
            }
            wasRightGrabbedLastFrame = isRightGrabbed;
            wasLeftGrabbedLastFrame = isLeftGrabbed;
        }

        public static void ProjectileOrbit()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                if (projModsEnabled)
                {
                    LaunchProjectile(new Vector3(), GorillaTagger.Instance.offlineVRRig.rightHandTransform.transform.position + new Vector3(MathF.Cos(240f + ((float)Time.frameCount / 30)), 1, MathF.Sin(240f + ((float)Time.frameCount / 30))), true);
                }
                else
                {
                    EnableAllProjs();
                }
            }
            else
            {
                GetProjectile().SetSnowballActiveLocal(false);
            }
        }

        public static void ProjectileItemOrbit()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                if (projModsEnabled)
                {
                    GetProjectile().SetSnowballActiveLocal(true);
                    GetProjectile().transform.position = GorillaTagger.Instance.offlineVRRig.rightHandTransform.transform.position + new Vector3(MathF.Cos(240f + ((float)Time.frameCount / 30)), 1, MathF.Sin(240f + ((float)Time.frameCount / 30)));
                }
                else
                {
                    EnableAllProjs();
                }
            }
            else
            {
                GetProjectile().SetSnowballActiveLocal(false);
            }
        }

        public static GrowingSnowballThrowable GetSnowballThrowable()
        {
            return GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R/palm.01.R/TransferrableItemRightHand/GrowingSnowballRightAnchor(Clone)").transform.Find("LMACF. RIGHT.").GetComponent<GrowingSnowballThrowable>();
        }

        public static void LaunchBigSnowBall(Vector3 position, Vector3 Vel, RaiseEventOptions yu)
        {
            var snowball = GetSnowballThrowable();
            if (!snowball.gameObject.activeSelf)
            {
                snowball.SetSnowballActiveLocal(true);
                snowball.transform.SetPositionAndRotation(position, new Quaternion());
            }

            PhotonNetwork.RaiseEvent(176, new object[]
            {
                snowball.changeSizeEvent._eventId,
                99
            }, new RaiseEventOptions { Receivers = ReceiverGroup.All }, new SendOptions
            {
                Reliability = false,
            });
            PhotonNetwork.RaiseEvent(176, new object[]
            {
                snowball.snowballThrowEvent._eventId,
                position,
                Vel,
                snowball.LaunchSnowballLocal(position, Vel, 0).myProjectileCount
            }, yu, new SendOptions
            {
                Reliability = false,
            });
            Saftey.KawaiiRPC();
            if (!ControllerInputPoller.instance.rightGrab || GunTemplate.lockedPlayer == null)
            {
                snowball.SetSnowballActiveLocal(false);
            }
        }

        public static void FlingGun()
        {
            GunTemplate.StartBothGuns(() =>
            {
                int targetActorNumber = GunTemplate.lockedPlayer.Creator.ActorNumber;
                Photon.Realtime.Player targetPlayer = PhotonNetwork.CurrentRoom.GetPlayer(targetActorNumber);
                float currentTime = Time.time;

                if (!Advantage.lastTaggedTime.ContainsKey(targetActorNumber) || currentTime - Advantage.lastTaggedTime[targetActorNumber] >= 0.5f)
                {
                    Advantage.lastTaggedTime[targetActorNumber] = currentTime;
                    if (projModsEnabled)
                    {
                        //if (Time.time > Variables.Delay)
                        {
                            //Variables.Delay = Time.time + 0.4f;
                            Overpowered.Overpowered.SerilizeAction(GunTemplate.lockedPlayer, () => LaunchBigSnowBall(GunTemplate.lockedPlayer.transform.position + new Vector3(0, -0.5f, 0), new Vector3(0, -49, 0), new RaiseEventOptions { TargetActors = new int[] { GunTemplate.lockedPlayer.creator.ActorNumber } }));
                        }
                    }
                    else
                    {
                        EnableAllProjs();
                    }
                }
            }, true);
        }

        public static void FlingAura()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig != GorillaTagger.Instance.offlineVRRig && (Vector3.Distance(GorillaTagger.Instance.leftHandTransform.position, rig.headMesh.transform.position) < 4f || Vector3.Distance(GorillaTagger.Instance.rightHandTransform.position, rig.headMesh.transform.position) < 4f))
                {
                    if (projModsEnabled)
                    {
                        if (Time.time > Variables.Delay)
                        {
                            Variables.Delay = Time.time + 0.4f;
                            LaunchBigSnowBall(rig.transform.position + new Vector3(0, -0.5f, 0), new Vector3(0, -49, 0), new RaiseEventOptions { TargetActors = new int[] { rig.creator.ActorNumber } });
                        }
                    }
                    else
                    {
                        EnableAllProjs();
                    }
                }
            }
        }

        public static void FlingOnYouTouch()
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
                    if (projModsEnabled)
                    {
                        if (Time.time > Variables.Delay)
                        {
                            Variables.Delay = Time.time + 0.4f;
                            LaunchBigSnowBall(rig.transform.position + new Vector3(0, -0.5f, 0), new Vector3(0, -49, 0), new RaiseEventOptions { TargetActors = new int[] { rig.creator.ActorNumber } });
                        }
                    }
                    else
                    {
                        EnableAllProjs();
                    }
                }
            }
        }

        public static void FlingOnTheyTouch()
        {
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
                    if (projModsEnabled)
                    {
                        if (Time.time > Variables.Delay)
                        {
                            Variables.Delay = Time.time + 0.4f;
                            LaunchBigSnowBall(GunTemplate.lockedPlayer.transform.position + new Vector3(0, -0.5f, 0), new Vector3(0, -49, 0), new RaiseEventOptions { TargetActors = new int[] { rig.creator.ActorNumber } });
                        }
                    }
                    else
                    {
                        EnableAllProjs();
                    }
                }

            }
        }
        public static void SnowballMinigun()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                if (projModsEnabled)
                {
                    if (Time.time > Variables.Delay)
                    {
                        Variables.Delay = Time.time + 0.18f;
                        LaunchBigSnowBall(GorillaTagger.Instance.rightHandTransform.position, -GorillaTagger.Instance.rightHandTransform.forward * -38, new RaiseEventOptions { Receivers = ReceiverGroup.All });
                    }

                }
                else
                {
                    EnableAllProjs();
                }
            }
        }
    }
}
