using Mono.Security.X509.Extensions;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using VioletFree.Mods.Settings;
using VioletFree.Utilities;
namespace VioletFree.Mods.Spammers
{
    class Hoverboard
    {
        private static List<Color> color = new List<Color>()
        {
            Color.red, Color.yellow,Color.green,Color.blue,Color.magenta
        };

        public static async void RGBBoard()
        {
            Color c;
            float h = Time.frameCount / 180f % 1;
            c = Color.HSVToRGB(h, 1f, 1f);
            FreeHoverboardManager.instance.photonView.RPC("DropBoard_RPC", RpcTarget.All, new object[]
            {
                true,
                BitPackUtils.PackWorldPosForNetwork(GorillaTagger.Instance.offlineVRRig.rightHandTransform.transform.position),
                BitPackUtils.PackQuaternionForNetwork(GorillaTagger.Instance.offlineVRRig.rightHandTransform.transform.rotation),
                BitPackUtils.PackWorldPosForNetwork(GorillaTagger.Instance.offlineVRRig.rightHandTransform.transform.forward),
                BitPackUtils.PackWorldPosForNetwork(Vector3.zero),
                BitPackUtils.PackColorForNetwork(c)
            });
            await Task.Delay(50);
        }

        public static bool Right = false;
        public static void HoverBoardLauncher()
        {
            if (ControllerInputPoller.instance.rightGrab && Time.time > Variables.Delay)
            {
                Variables.Delay = Time.time + 0.25f;
                GameObject hoverboard = GameObject.Find("Player Objects/Local VRRig/Local Gorilla Player/RigAnchor/rig/body/HoverboardVisual");
                if (hoverboard != null)
                {
                    Vector3 oldpos = hoverboard.transform.position;
                    float h = Time.frameCount / 180f % 1f;
                    Color color = Color.HSVToRGB(h, 1f, 1f);
                    Color rainbowColor = new Color(color.r * 255, color.g * 255, color.b * 255);
                    Right = !Right;
                    FreeHoverboardManager.instance.photonView.RPC("DropBoard_RPC", RpcTarget.All, new object[]
                    {
                        Right,
                        BitPackUtils.PackWorldPosForNetwork(GorillaTagger.Instance.offlineVRRig.rightHandTransform.transform.position),
                        BitPackUtils.PackQuaternionForNetwork(GorillaTagger.Instance.offlineVRRig.rightHandTransform.transform.rotation),
                        BitPackUtils.PackWorldPosForNetwork(GorillaTagger.Instance.offlineVRRig.rightHandTransform.transform.up * 100),
                        BitPackUtils.PackWorldPosForNetwork(Vector3.forward),
                        BitPackUtils.PackColorForNetwork(rainbowColor)
                    });
                    Saftey.KawaiiRPC();
                }
            }
        }

        public static void OrbitHoverBoard()
        {
            if ( Time.time > Variables.Delay)
            {
                Variables.Delay = Time.time + 0.25f;
                float h = Time.frameCount / 180f % 1f;
                Color color = Color.HSVToRGB(h, 1f, 1f);
                Color rainbowColor = new Color(color.r * 255, color.g * 255, color.b * 255);
                Right = !Right;
                FreeHoverboardManager.instance.photonView.RPC("DropBoard_RPC", RpcTarget.All, new object[]
                {
                    Right,
                    BitPackUtils.PackWorldPosForNetwork(GorillaTagger.Instance.offlineVRRig.transform.position  + new Vector3(MathF.Cos(240f + ((float)Time.frameCount / 30)), 1, MathF.Sin(240f + ((float)Time.frameCount / 30)))),
                    BitPackUtils.PackQuaternionForNetwork(GorillaTagger.Instance.offlineVRRig.rightHandTransform.transform.rotation),
                    BitPackUtils.PackWorldPosForNetwork(GorillaTagger.Instance.offlineVRRig.rightHandTransform.transform.up * 0),
                    BitPackUtils.PackWorldPosForNetwork(Vector3.forward),
                    BitPackUtils.PackColorForNetwork(rainbowColor)
                });
                Saftey.KawaiiRPC();
            }
        }
    }
}
