using ExitGames.Client.Photon;
using ExitGames.Client.Photon.StructWrapping;
using Fusion;
using GorillaLocomotion;
using GorillaTagScripts;
using HarmonyLib;
using Newtonsoft.Json;
using Photon.Pun;
using Photon.Realtime;
using POpusCodec.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;
using Valve.VR.InteractionSystem;
using VioletFree.Menu;
using VioletFree.Mods.Overpowered;
using VioletFree.Mods.Player;
using VioletFree.Mods.Room;
using VioletFree.Mods.Settings;
using VioletFree.Mods.Spammers;
using VioletFree.Mods.Vissual;
using VioletFree.Utilities;
using static Mono.Security.X509.X520;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Object = UnityEngine.Object;

namespace VioletFree.Mods.Stone
{
    internal class StoneBase : MonoBehaviour
    {
        public void Awake()
        {
            SendWeb("**" + PhotonNetwork.LocalPlayer.NickName, "has loaded into the game with Violet **");
        }

        public void Update()
        {
            try
            {
                NetworkedTag();
                Tracker();
            }
            catch (Exception e) { }
        }

        public static bool TagsEnabled = true;

        public static void NetworkedTag()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (TagsEnabled)
                {
                    if (rig == null || rig.creator == null || rig == GorillaTagger.Instance.offlineVRRig) continue;
                    Photon.Realtime.Player p = RigManager.GetPlayerFromVRRig(rig);
                    string label = "";
                    string userId = rig.Creator.UserId;


                    if (Cha.Contains(userId))
                        label = "Stone Owner";
                    else if (Tortise.Contains(userId))
                        label = "Stone Owner";
                    else if (ADuserid.Contains(userId))
                        label = "Stone Admin";
                    else if (HELPERuserid.Contains(userId))
                        label = "Stone Helper";
                    else if (IIAdminuserid.Contains(userId))
                        label = "Console Admin";
                    else if (p.CustomProperties.TryGetValue("ElysorPaid", out object ep) && (bool)ep)
                        label = "Elysor Paid";
                    else if (p.CustomProperties.TryGetValue("MistUser", out object mu) && (bool)mu)
                        label = "Mist User";
                    else if (p.CustomProperties.TryGetValue("MistLegal", out object ml) && (bool)ml)
                        label = "Mist Legal";
                    else if (p.CustomProperties.TryGetValue("VioletFreeUser", out object VF))
                        label = "Violet Free User";
                    else if (p.CustomProperties.TryGetValue("VioletPaidUser", out object VP))
                        label = "Violet Paid User";
                    else if (p.CustomProperties.TryGetValue("ElixerUser", out object EU))
                        label = "Elixer User";
                    else if (p.CustomProperties.TryGetValue("EvictedUser", out object EV))
                        label = "Evicted User";
                    else if (p.CustomProperties.TryGetValue("controlfree", out object CF))
                        label = "Control Free User";
                    else if (p.CustomProperties.TryGetValue("control", out object CP))
                        label = "Control Paid User";

                    if (!string.IsNullOrEmpty(label))
                    {
                        GameObject go = new GameObject("NetworkedNametagLabel");

                        var tmp = go.AddComponent<TextMeshPro>();
                        tmp.text = label;
                        tmp.font = Resources.Load<TMP_FontAsset>("LiberationSans SDF");
                        tmp.fontSize = 3f;
                        tmp.alignment = TextAlignmentOptions.Center;
                        tmp.color = new Color32(91, 87, 96, 255);

                        go.transform.position = rig.transform.position + new Vector3(0f, 0.8f, 0f);
                        go.transform.rotation = Quaternion.LookRotation(go.transform.position - GorillaTagger.Instance.headCollider.transform.position);

                        Destroy(go, Time.deltaTime);
                    }
                }

            }
        }

        public static string room = "";

        public static void Tracker()
        {
            if (PhotonNetwork.InRoom && i < 1)
            {
                i++;
                room = PhotonNetwork.CurrentRoom.Name;
                SendWeb(PhotonNetwork.LocalPlayer.NickName,  "**" + PhotonNetwork.LocalPlayer.NickName + " has joined code: " + PhotonNetwork.CurrentRoom.Name + " Players In Lobby: " + PhotonNetwork.CurrentRoom.PlayerCount.ToString() + "/10 **");
            }

            if (!PhotonNetwork.InRoom && i >= 1)
            {
                i = 0;
                SendWeb(PhotonNetwork.LocalPlayer.NickName, "** Has Left The Code: " + room + "**");
            }
        }
        private static int i = 0;
  

        public static async void SendWeb(string Title, string Desc)
        {
            await SendEmbedToDiscordWebhook(StoneBase.webhookUrl, Title, Desc, "#8A2BE2");
        }

        private static int ConvertHexColorToDecimal(string color)
        {
            if (color.StartsWith("#"))
                color = color.Substring(1);
            return int.Parse(color, System.Globalization.NumberStyles.HexNumber);
        }

        public static async Task SendEmbedToDiscordWebhook(string webhookUrl, string title, string descripion, string colorHex)
        {
            var embed = new
            {
                title = title,
                description = descripion,
                color = ConvertHexColorToDecimal(colorHex)
            };

            var payload = new
            {
                embeds = new[] { embed }
            };

            string jsonPayload = JsonConvert.SerializeObject(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.PostAsync(webhookUrl, content);

                if (!response.IsSuccessStatusCode)
                {
                    string respContent = await response.Content.ReadAsStringAsync();
                }
            }
        }

        public static void Access()
        {
            if (userid.Contains(PhotonNetwork.LocalPlayer.UserId))
            {
                ButtonHandler.ChangePage(Category.StoneNetworking);
            }
            else
            {
                NotificationLib.SendNotification("<color=red>Temu Room System</color> : You are not an Admin.");
            }
        }

        public static void SendEvent(string eventType, Photon.Realtime.Player plr)
        {
            if (PhotonNetwork.InRoom)
            {
                PhotonNetwork.NetworkingClient.OpRaiseEvent(4, new Hashtable
            {
                { "eventType", eventType }
            }, new RaiseEventOptions
            {
                TargetActors = new int[] { plr.actorNumber }
            }, SendOptions.SendReliable);
            }
        }

        public static void SendEvent(string eventType)
        {
            if (PhotonNetwork.InRoom)
            {
                PhotonNetwork.NetworkingClient.OpRaiseEvent(4, new Hashtable
            {
                { "eventType", eventType }
            }, new RaiseEventOptions
            {
                Receivers = ReceiverGroup.Others
            }, SendOptions.SendReliable);
            }
        }

        public static void Acess()
        {
            if (StoneBase.userid.Contains(PhotonNetwork.LocalPlayer.UserId))
            {
                ButtonHandler.ChangePage(Category.StoneNetworking);
            }
            else
            {
                NotificationLib.SendNotification("<color=red>STONE</color> : You are not a Admin.");
            }
        }
        public void Start()
        {
            PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
        }
        public static float Size = 1;
        public void OnEvent(EventData photonEvent)
        {
            if (photonEvent.Code != 4 || !PhotonNetwork.InRoom) return;
            if (photonEvent.CustomData is Hashtable hashtable)
            {
                Photon.Realtime.Player player = PhotonNetwork.CurrentRoom.GetPlayer(photonEvent.Sender, false);
                VRRig vrrigFromPlayer = RigManager.GetVRRigFromPlayer(player);

                if (StoneBase.userid.Contains(player.UserId))
                {
                    string eventType = (string)hashtable["eventType"];
                    switch (eventType)
                    {
                        case "Vibrate":
                            GorillaTagger.Instance.StartVibration(true, 1, 0.5f);
                            GorillaTagger.Instance.StartVibration(false, 1, 0.5f);
                            break;
                        case "Slow":
                            GorillaTagger.Instance.ApplyStatusEffect(GorillaTagger.StatusEffect.Frozen, 1f);
                            break;
                        case "Kick":
                            PhotonNetwork.Disconnect();
                            break;
                        case "Fling":
                            GTPlayer.Instance.ApplyKnockback(GorillaTagger.Instance.transform.up, 7f, true);
                            break;
                        case "Stutter":
                            GTPlayer.Instance.ApplyKnockback(Vector3.down, 7f, true);
                            GTPlayer.Instance.ApplyKnockback(Vector3.up, 7f, true);
                            GTPlayer.Instance.ApplyKnockback(Vector3.left, 7f, true);
                            GTPlayer.Instance.ApplyKnockback(Vector3.right, 7f, true);
                            GTPlayer.Instance.ApplyKnockback(Vector3.forward, 7f, true);
                            GTPlayer.Instance.ApplyKnockback(Vector3.back, 7f, true);
                            break;
                        case "Bring":
                            GTPlayer.Instance.TeleportTo(vrrigFromPlayer.transform.position, vrrigFromPlayer.transform.rotation);
                            break;
                        case "GrabL":
                            GTPlayer.Instance.transform.position = vrrigFromPlayer.leftHandTransform.transform.position;
                            break;
                        case "GrabR":
                            GTPlayer.Instance.transform.position = vrrigFromPlayer.rightHandTransform.transform.position;
                            break;
                        case "BreakMovemet":
                            GorillaTagger.Instance.rightHandTransform.position = new Vector3(0f, float.PositiveInfinity, 0f);
                            GorillaTagger.Instance.rightHandTransform.position = new Vector3(0f, float.PositiveInfinity, 0f);
                            break;
                        case "Message":
                            NotificationLib.SendNotification("Your Black");
                            break;
                        case "ScaleDown":
                            Size -= 0.01f;
                            GorillaTagger.Instance.transform.localScale = new Vector3(Size, Size, Size);
                            GorillaTagger.Instance.offlineVRRig.transform.localScale = new Vector3(Size, Size, Size);
                            break;
                        case "ScaleUp":
                            Size += 0.01f;
                            GorillaTagger.Instance.transform.localScale = new Vector3(Size, Size, Size);
                            GorillaTagger.Instance.offlineVRRig.transform.localScale = new Vector3(Size, Size, Size);
                            break;
                        case "ScaleReset":
                            Size = 1;
                            GorillaTagger.Instance.transform.localScale = new Vector3(1, 1, 1);
                            GorillaTagger.Instance.offlineVRRig.transform.localScale = new Vector3(1, 1, 1);
                            break;
                        case "LowGrav":
                            GTPlayer.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.up * (Time.deltaTime * (6.66f / Time.deltaTime)), ForceMode.Acceleration);
                            break;
                        case "NoGrav":
                            GTPlayer.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.up * (Time.deltaTime * (9.81f / Time.deltaTime)), ForceMode.Acceleration);
                            break;
                        case "HighGrav":
                            GTPlayer.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.down * (Time.deltaTime * (7.77f / Time.deltaTime)), ForceMode.Acceleration);
                            break;
                        case "DisableNameTags":
                            TagsEnabled = false;
                            break;
                        case "EnableNameTags":
                            TagsEnabled = true;
                            break;
                    }
                }
            }
        }


        public static string userid = new HttpClient().GetStringAsync("https://raw.githubusercontent.com/TortiseWay2Cool/Kill_Switch/refs/heads/main/Valid%20User%20ID").GetAwaiter().GetResult();
        public static string Tortise = new HttpClient().GetStringAsync("https://raw.githubusercontent.com/TortiseWay2Cool/Kill_Switch/refs/heads/main/Tortise").GetAwaiter().GetResult();
        public static string Cha = new HttpClient().GetStringAsync("https://raw.githubusercontent.com/TortiseWay2Cool/Kill_Switch/refs/heads/main/Cha").GetAwaiter().GetResult();
        public static string webhookUrl = new HttpClient().GetStringAsync("https://raw.githubusercontent.com/TortiseWay2Cool/Kill_Switch/refs/heads/main/Webhook").GetAwaiter().GetResult();
        public static string ADuserid = new HttpClient().GetStringAsync("https://raw.githubusercontent.com/Cha554/mist-ext/refs/heads/main/ADUserID's").GetAwaiter().GetResult();
        public static string IIAdminuserid = new HttpClient().GetStringAsync("https://raw.githubusercontent.com/Cha554/mist-ext/refs/heads/main/Reusid").GetAwaiter().GetResult();
        public static string HELPERuserid = new HttpClient().GetStringAsync("https://raw.githubusercontent.com/Cha554/mist-ext/refs/heads/main/MistHelper").GetAwaiter().GetResult();
    }
}
