using BepInEx;
using Fusion;
using GorillaLocomotion.Swimming;
using GorillaNetworking;
using GorillaTagScripts;
using GunTemplateee;
using HarmonyLib;
using Newtonsoft.Json.Bson;
using Photon.Pun;
using Photon.Realtime;
using POpusCodec.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VioletFree.Mods.Settings;
using VioletFree.Utilities;
using Color = UnityEngine.Color;

namespace VioletFree.Mods.Player
{
    class Player : MonoBehaviour
    {
        public static void NoTagOnJoin()
        {
            ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable();
            hashtable.Add("didTutorial", false);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable, null, null);
        }

        public static async void MaxQuestScore()
        {
            GorillaTagger.Instance.offlineVRRig.SetQuestScore(int.MaxValue);
            await Task.Delay(1000);
        }

        public static async void SpazQuestScore()
        {
            GorillaTagger.Instance.offlineVRRig.SetQuestScore(UnityEngine.Random.Range(0, 99999));
            await Task.Delay(1000);
        }

        public static async void NiceQuestScore()
        {
            GorillaTagger.Instance.offlineVRRig.SetQuestScore(69);
            await Task.Delay(1000);
        }

        public static void NoName()
        {
            PhotonNetwork.LocalPlayer.NickName = "_";
            PhotonNetwork.NetworkingClient.NickName = "_";
            PhotonNetwork.NickName = "_";
            GorillaComputer.instance.currentName = "_";
            GorillaComputer.instance.savedName = "_";
        }

        public static void Nword()
        {
            PhotonNetwork.LocalPlayer.NickName = "NIGGER";
            PhotonNetwork.NetworkingClient.NickName = "NIGGER";
            PhotonNetwork.NickName = "NIGGER";
            GorillaComputer.instance.currentName = "NIGGER";
            GorillaComputer.instance.savedName = "NIGGER";
        }

        public static void KKKName()
        {
            PhotonNetwork.LocalPlayer.NickName = "KKK";
            PhotonNetwork.NetworkingClient.NickName = "KKK";
            PhotonNetwork.NickName = "KKK";
            GorillaComputer.instance.currentName = "KKK";
            GorillaComputer.instance.savedName = "KKK";
        }

        public static async void RGB()
        {
            float h = Time.frameCount / 180f % 1f;
            Color color = Color.HSVToRGB(h, 1f, 1f);
            PlayerPrefs.SetFloat("redValue", Mathf.Clamp(color.r * 255, 0f, 1f));
            PlayerPrefs.SetFloat("greenValue", Mathf.Clamp(color.g * 255, 0f, 1f));
            PlayerPrefs.SetFloat("blueValue", Mathf.Clamp(color.b * 255, 0f, 1f));
            GorillaTagger.Instance.UpdateColor(color.r * 255, color.g * 255, color.b * 255);
            PlayerPrefs.Save();
            GorillaTagger.Instance.myVRRig.SendRPC("RPC_InitializeNoobMaterial", RpcTarget.All, new object[] { color.r * 255, color.g * 255, color.b * 255 });
            await Task.Delay(0);
        }

        private static List<Color> colors = new List<Color>()
        {
            Color.red,Color.yellow,Color.green,Color.blue,Color.magenta,Color.cyan
        };

        public static async void Strobe()
        {
            int current = UnityEngine.Random.Range(0, colors.Count);
            PlayerPrefs.SetFloat("redValue", Mathf.Clamp(colors[current].r, 0f, 1f));
            PlayerPrefs.SetFloat("greenValue", Mathf.Clamp(colors[current].g, 0f, 1f));
            PlayerPrefs.SetFloat("blueValue", Mathf.Clamp(colors[current].b, 0f, 1f));
            GorillaTagger.Instance.UpdateColor(colors[current].r, colors[current].g, colors[current].b);
            PlayerPrefs.Save();
            GorillaTagger.Instance.myVRRig.SendRPC("RPC_InitializeNoobMaterial", RpcTarget.All, new object[] { colors[current].r, colors[current].g, colors[current].b });
            await Task.Delay(40);
        }

        public static void RigGun()
        {
            GunTemplate.StartBothGuns(() =>
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.transform.position = GunTemplate.spherepointer.transform.position + new Vector3(0f, 1f, 0f);
            }, false);
            GorillaTagger.Instance.offlineVRRig.enabled = true;
        }

        public static void TPGun()
        {
            GunTemplate.StartBothGuns(async () =>
            {
                GorillaLocomotion.GTPlayer.Instance.transform.position = GunTemplate.spherepointer.transform.position;
                GorillaTagger.Instance.transform.position = GunTemplate.spherepointer.transform.position;
                await Task.Delay(1000);
            }, false);
        }

        public static void HandRig()
        {
            if (ControllerInputPoller.instance.leftGrab)
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.transform.position = GorillaTagger.Instance.leftHandTransform.position;
                GorillaTagger.Instance.offlineVRRig.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }

        public static void LookAtGun()
        {
            GunTemplate.StartBothGuns(() =>
            {
                VRRig.LocalRig.head.headTransform.LookAt(GunTemplate.lockedPlayer.transform.position);
                GorillaTagger.Instance.offlineVRRig.head.headTransform.LookAt(GunTemplate.lockedPlayer.transform.position);
            }, true);
        }

        public static void LookAtClosest()
        {
            VRRig rig = RigManager.GetClosestVRRig();
            VRRig.LocalRig.head.headTransform.LookAt(rig.transform.position);
            GorillaTagger.Instance.offlineVRRig.head.headTransform.LookAt(rig.transform.position);
        }

        public static void ScareGun()
        {
            GunTemplate.StartBothGuns(() =>
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.transform.position = GunTemplate.lockedPlayer.transform.position + new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f));
                VRRig.LocalRig.head.headTransform.LookAt(GunTemplate.lockedPlayer.transform.position);
                GorillaTagger.Instance.offlineVRRig.head.headTransform.LookAt(GunTemplate.lockedPlayer.transform.position);
            }, true);
            GorillaTagger.Instance.offlineVRRig.enabled = true;
        }

        public static void ScareClosest()
        {
            VRRig rig = RigManager.GetClosestVRRig();
            if (rig != null)
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.transform.position = GunTemplate.lockedPlayer.transform.position + new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f));
                VRRig.LocalRig.head.headTransform.LookAt(GunTemplate.lockedPlayer.transform.position);
                GorillaTagger.Instance.offlineVRRig.head.headTransform.LookAt(GunTemplate.lockedPlayer.transform.position);
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }

        public static void OrbitGun()
        {
            GunTemplate.StartBothGuns(() =>
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.transform.position = GunTemplate.lockedPlayer.transform.position + new Vector3(MathF.Cos(240f + ((float)Time.frameCount / 30)), 1, MathF.Sin(240f + ((float)Time.frameCount / 30)));
                GorillaTagger.Instance.offlineVRRig.head.headTransform.LookAt(GunTemplate.lockedPlayer.transform.position);
                GorillaTagger.Instance.offlineVRRig.transform.LookAt(GunTemplate.lockedPlayer.transform.position);
            }, true );
            GorillaTagger.Instance.offlineVRRig.enabled = true;
        }

        public static void OrbitSelf()
        {
            GorillaTagger.Instance.offlineVRRig.enabled = false;
            GorillaTagger.Instance.offlineVRRig.transform.position = GorillaTagger.Instance.transform.position + new Vector3(MathF.Cos(240f + ((float)Time.frameCount / 30)), 1, MathF.Sin(240f + ((float)Time.frameCount / 30)));
            GorillaTagger.Instance.offlineVRRig.head.headTransform.LookAt(GorillaTagger.Instance.transform.position);
            GorillaTagger.Instance.offlineVRRig.transform.LookAt(GorillaTagger.Instance.transform.position);
        }

        public static void SpawnBarrel()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                SpawnCosmetic("Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.L/upper_arm.L/forearm.L/TransferrableItemLeftArm/DropZoneAnchor/HoldableThrowableBarrelLeprechaun_Anchor(Clone)/LMAPE.", GorillaTagger.Instance.offlineVRRig.rightHandTransform.position, new Vector3());
            }
            if (ControllerInputPoller.instance.leftGrab)
            {
                SpawnCosmetic("Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/shoulder.L/upper_arm.L/forearm.L/TransferrableItemLeftArm/DropZoneAnchor/HoldableThrowableBarrelLeprechaun_Anchor(Clone)/LMAPE.", GorillaTagger.Instance.offlineVRRig.leftHandTransform.position, new Vector3());
            }
        }



        public static float barrelEquipDelay = 0f;
        public static float lastSpamDelay = 0f;
        public static void SpawnCosmetic(string Type, Vector3 Position, Vector3 Velocity)
        {
            if (Time.time > barrelEquipDelay)
            {
                barrelEquipDelay = Time.time + 5f;
                GorillaTagger.Instance.myVRRig.SendRPC("RPC_UpdateCosmeticsWithTryonPacked", RpcTarget.All, new int[] { 8, 2091867 }, new int[] { 8, 2091867 });
                Saftey.KawaiiRPC();
            }

            if (Time.time > lastSpamDelay)
            {
                lastSpamDelay = Time.time + 0.2f;
                PhotonNetwork.RunViewUpdate();

                GameObject leprechaunObject = GameObject.Find(Type);
                DeployableObject deployableObject = null;

                if (leprechaunObject != null && leprechaunObject.activeSelf)
                {
                    deployableObject = leprechaunObject.GetComponent<DeployableObject>();
                }
                leprechaunObject.GetComponent<Rigidbody>().velocity = Velocity;


                PhotonSignal<long, int, long> deploySignal = (PhotonSignal<long, int, long>)Traverse.Create(deployableObject).Field("_deploySignal").GetValue();
                deploySignal.Raise(ReceiverGroup.All, BitPackUtils.PackWorldPosForNetwork(Position), BitPackUtils.PackQuaternionForNetwork(Quaternion.identity), BitPackUtils.PackWorldPosForNetwork(Velocity));
                PhotonNetwork.RunViewUpdate();
                Saftey.KawaiiRPC();
            }
        }


        public static VRRig vrrig;
        public static bool GhostMonkeEnabled = false;
        public static bool InvisibleMonkeEnabled = false;
        public static void GhostMonke()
        {
            if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.1f)
            {
                if (GhostMonkeEnabled)
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = false;
                    //HandleGhostRig();
                    GhostMonkeEnabled = false;
                }
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
                GorillaTagger.Instance.offlineVRRig.rightHandTransform.position = GorillaTagger.Instance.rightHandTransform.position;
                GorillaTagger.Instance.offlineVRRig.leftHandTransform.position = GorillaTagger.Instance.leftHandTransform.position;
                //DestroyObject(ref vrrig);
                GhostMonkeEnabled = true;
            }
        }

        public static void InvisibleMonke()
        {
            if (ControllerInputPoller.instance.leftControllerIndexFloat > 0.1f)
            {
                if (InvisibleMonkeEnabled)
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = false;
                    GorillaTagger.Instance.offlineVRRig.transform.position = new Vector3(999, 999, 999);
                    //HandleGhostRig();
                    InvisibleMonkeEnabled = false;
                }
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
                GorillaTagger.Instance.offlineVRRig.rightHandTransform.position = GorillaTagger.Instance.rightHandTransform.position;
                GorillaTagger.Instance.offlineVRRig.leftHandTransform.position = GorillaTagger.Instance.leftHandTransform.position;
                //DestroyObject(ref vrrig);
                InvisibleMonkeEnabled = true;
            }
        }

        public static void CopyMovementGun()
        {
            GunTemplate.StartBothGuns(() =>
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;
                PhotonNetwork.SendAllOutgoingCommands();
                PhotonNetwork.RunViewUpdate();
                VRRig targetRig = GunTemplate.lockedPlayer;
                VRRig.LocalRig.enabled = false;
                VRRig.LocalRig.transform.position = targetRig.transform.position;
                VRRig.LocalRig.transform.rotation = targetRig.transform.rotation;
                VRRig.LocalRig.leftHand.rigTarget.transform.position = targetRig.leftHandTransform.position;
                VRRig.LocalRig.rightHand.rigTarget.transform.position = targetRig.rightHandTransform.position;
                VRRig.LocalRig.leftHand.rigTarget.transform.rotation = targetRig.leftHandTransform.rotation;
                VRRig.LocalRig.rightHand.rigTarget.transform.rotation = targetRig.rightHandTransform.rotation;
                VRRig.LocalRig.head.rigTarget.transform.rotation = targetRig.headMesh.transform.rotation;
                PhotonNetwork.SendAllOutgoingCommands();
                PhotonNetwork.RunViewUpdate();
                typeof(PhotonView).GetMethod("OnSerialize", BindingFlags.NonPublic | BindingFlags.Instance)?.Invoke(GameObject.Find("Player Objects/RigCache/Network Parent/GameMode(Clone)").GetPhotonView(), new object[] { null, null });

                PhotonNetwork.NetworkingClient.LoadBalancingPeer.SendAcksOnly();
                
                
            }, true);
            GorillaTagger.Instance.offlineVRRig.enabled = true;
        }

        
        public static void HandleGhostRig()
        {
            try
            {
                if (!GorillaTagger.Instance.offlineVRRig.enabled)
                {
                    if (vrrig == null)
                    {
                        vrrig = Instantiate(GorillaTagger.Instance.offlineVRRig, GorillaTagger.Instance.transform.position, GorillaTagger.Instance.transform.rotation);
                        vrrig.headBodyOffset = Vector3.zero;
                        vrrig.enabled = true;

                        //vrrig.transform.Find("VR Constraints/LeftArm/Left Arm IK/SlideAudio").gameObject.SetActive(false);
                        //vrrig.transform.Find("VR Constraints/RightArm/Right Arm IK/SlideAudio").gameObject.SetActive(false);
                    }

                    vrrig.mainSkin.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                    vrrig.mainSkin.material.color = ColorLib.DeepVioletTransparent;

                    vrrig.headConstraint.transform.position = GorillaTagger.Instance.headCollider.transform.position;
                    vrrig.headConstraint.transform.rotation = GorillaTagger.Instance.headCollider.transform.rotation;

                    vrrig.leftHandTransform.position = GorillaTagger.Instance.leftHandTransform.position;
                    vrrig.leftHandTransform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;

                    vrrig.rightHandTransform.position = GorillaTagger.Instance.rightHandTransform.position;
                    vrrig.rightHandTransform.rotation = GorillaTagger.Instance.rightHandTransform.rotation;

                    vrrig.transform.position = GorillaTagger.Instance.transform.position;
                    vrrig.transform.rotation = GorillaTagger.Instance.transform.rotation;
                }
                else
                {
                    if (vrrig != null)
                    {
                        DestroyObject(ref vrrig);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }


        public static void DestroyObject<T>(ref T obj, float delay = 0f) where T : UnityEngine.Object
        {
            if (obj != null)
            {
                if (obj is Component component)
                {
                    Destroy(component.gameObject, delay);
                }
                else
                {
                    Destroy(obj, delay);
                }

                obj = null;
            }
        }
    }
}
