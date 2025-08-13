using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using VioletFree.Mods.Settings;
using VioletFree.Utilities;

namespace VioletFree.Mods.Spammers
{
    class Water
    {
        public static void SplashHands()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                if (Time.time > Variables.Delay)
                {
                    GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlaySplashEffect", RpcTarget.All, new object[] { GorillaTagger.Instance.rightHandTransform.position, GorillaTagger.Instance.rightHandTransform.rotation, 6f, 150f, true, false });

                    Saftey.KawaiiRPC();
                    Variables.Delay = Time.time + 0.07f;
                }
            }
            if (ControllerInputPoller.instance.leftGrab)
            {
                if (Time.time > Variables.Delay)
                {
                    GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlaySplashEffect", RpcTarget.All, new object[] { GorillaTagger.Instance.leftHandTransform.position, GorillaTagger.Instance.leftHandTransform.rotation, 6f, 150f, true, false });

                    Saftey.KawaiiRPC();
                    Variables.Delay = Time.time + 0.07f;
                }
            }
        }

        public static void SplashBody()
        {
            if (Time.time > Variables.Delay)
            {
                GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlaySplashEffect", RpcTarget.All, new object[] { GorillaTagger.Instance.transform.position, GorillaTagger.Instance.rightHandTransform.rotation, 6f, 150f, true, true });
                Saftey.KawaiiRPC();
                Variables.Delay = Time.time + 0.07f;
            }
        }

        public static void SplashOrbit()
        {
            if (Time.time > Variables.Delay)
            {
                GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlaySplashEffect", RpcTarget.All, new object[] { GorillaTagger.Instance.transform.position + new Vector3(MathF.Cos(240f + ((float)Time.frameCount / 30)), 1, MathF.Sin(240f + ((float)Time.frameCount / 30))), GorillaTagger.Instance.rightHandTransform.rotation, 6f, 150f, true, true });
                Saftey.KawaiiRPC();
                Variables.Delay = Time.time + 0.07f;
            }
        }

        public static void Cum()
        {
            if (Time.time > Variables.Delay)
            {
                GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlaySplashEffect", RpcTarget.All, new object[] { GorillaTagger.Instance.transform.position + new Vector3(0,-0.5f,0), GorillaTagger.Instance.rightHandTransform.rotation, 6f, 150f, true, true });
                Saftey.KawaiiRPC();
                Variables.Delay = Time.time + 0.07f;
            }
        }

        public static void TinySplashBody()
        {
            if (Time.time > Variables.Delay)
            {
                GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlaySplashEffect", RpcTarget.All, new object[] { GorillaTagger.Instance.transform.position, GorillaTagger.Instance.rightHandTransform.rotation, 6f, 150f, false, true });
                Saftey.KawaiiRPC();
                Variables.Delay = Time.time + 0.07f;
            }
        }

        public static void TinySplashOrbit()
        {
            if (Time.time > Variables.Delay)
            {
                GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlaySplashEffect", RpcTarget.All, new object[] { GorillaTagger.Instance.transform.position + new Vector3(MathF.Cos(240f + ((float)Time.frameCount / 30)), 1, MathF.Sin(240f + ((float)Time.frameCount / 30))), GorillaTagger.Instance.rightHandTransform.rotation, 6f, 150f, false, true });
                Saftey.KawaiiRPC();
                Variables.Delay = Time.time + 0.07f;
            }
        }

        public static void TinyCum()
        {
            if (Time.time > Variables.Delay)
            {
                GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlaySplashEffect", RpcTarget.All, new object[] { GorillaTagger.Instance.transform.position + new Vector3(0, -0.5f, 0), GorillaTagger.Instance.rightHandTransform.rotation, 6f, 150f, false, true });
                Saftey.KawaiiRPC();
                Variables.Delay = Time.time + 0.07f;
            }
        }

        public static void TinySplashHands()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                if (Time.time > Variables.Delay)
                {
                    GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlaySplashEffect", RpcTarget.All, new object[] { GorillaTagger.Instance.rightHandTransform.position, GorillaTagger.Instance.rightHandTransform.rotation, 6f, 150f, false, false });
                    Saftey.KawaiiRPC();
                    Variables.Delay = Time.time + 0.07f;
                }
            }
            if (ControllerInputPoller.instance.leftGrab)
            {
                if (Time.time > Variables.Delay)
                {
                    GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlaySplashEffect", RpcTarget.All, new object[] { GorillaTagger.Instance.leftHandTransform.position, GorillaTagger.Instance.leftHandTransform.rotation, 6f, 150f, false, false });
                    Saftey.KawaiiRPC();
                    Variables.Delay = Time.time + 0.07f;
                }
            }
        }

        public static bool randsplash = false;
        public static void SplashAura()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                if (Time.time > Variables.Delay)
                {
                    GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlaySplashEffect", RpcTarget.All, new object[] { GorillaTagger.Instance.rightHandTransform.position + new Vector3(UnityEngine.Random.Range(-1.5f, 1.5f), UnityEngine.Random.Range(-1.5f, 1.5f), UnityEngine.Random.Range(-1.5f, 1.5f)), GorillaTagger.Instance.rightHandTransform.rotation, 6f, 150f, true, false });
                    Saftey.KawaiiRPC();
                    Variables.Delay = Time.time + 0.07f;
                }
            }
        }

        public static void TinySplashAura()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                if (Time.time > Variables.Delay)
                {
                    GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlaySplashEffect", RpcTarget.All, new object[] { GorillaTagger.Instance.rightHandTransform.position + new Vector3(UnityEngine.Random.Range(-1.5f, 1.5f), UnityEngine.Random.Range(-1.5f, 1.5f), UnityEngine.Random.Range(-1.5f, 1.5f)), GorillaTagger.Instance.rightHandTransform.rotation, 6f, 150f, false, false });
                    Saftey.KawaiiRPC();
                    Variables.Delay = Time.time + 0.07f;
                }
            }
        }

        public static void MixedSplashAura()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                if (Time.time > Variables.Delay)
                {
                    randsplash = !randsplash;
                    GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlaySplashEffect", RpcTarget.All, new object[] { GorillaTagger.Instance.rightHandTransform.position + new Vector3(UnityEngine.Random.Range(-1.5f, 1.5f), UnityEngine.Random.Range(-1.5f, 1.5f), UnityEngine.Random.Range(-1.5f, 1.5f)), GorillaTagger.Instance.rightHandTransform.rotation, 6f, 150f, randsplash, true });
                    Saftey.KawaiiRPC();
                    Variables.Delay = Time.time + 0.07f;
                }
            }
        }
    }
}
