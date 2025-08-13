using BepInEx;
using GorillaExtensions;
using GorillaLocomotion;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using VioletFree.Menu;
using VioletFree.Utilities;
using Object = UnityEngine.Object;

namespace VioletFree.Mods.Movement
{
    class Movement
    {
        public static GameObject RP1;
        public static GameObject LP1;
        public static GameObject RP2;
        public static GameObject LP2;
        public static GameObject RP3;
        public static GameObject LP3;
        public static GameObject RP4;
        public static GameObject LP4;
        public static GameObject RP5;
        public static GameObject LP5;
        public static bool RPA2;
        public static bool LPA2;
        public static bool RPA3;
        public static bool LPA3;
        public static bool RPA4;
        public static bool LPA4;
        public static bool RPA5;
        public static bool LPA5;
        public static GameObject RP;
        public static GameObject LP;
        public static bool RPA;
        public static bool LPA;

        public static void Platforms()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                if (!RPA)
                {
                    RP = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    RP.GetComponent<Renderer>().material.shader = Shader.Find("UI/Default");
                    RP.GetComponent<Renderer>().material.color = Settings.Settings.CurrentBackgroundColor;
                    RP.transform.rotation = GorillaLocomotion.GTPlayer.Instance.rightControllerTransform.rotation;
                    RP.transform.localScale = new Vector3(0.01f, 0.3f, 0.4f);
                    //Main.Outline(RP, Color.black);
                    RP.transform.position = GorillaTagger.Instance.rightHandTransform.position - Vector3.up * 0.045f;
                    RPA = true;
                }
            }
            else
            {
                UnityEngine.Object.Destroy(RP);
                RPA = false;
            }
            if (ControllerInputPoller.instance.leftGrab)
            {
                if (!LPA)
                {
                    LP = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    LP.GetComponent<Renderer>().material.shader = Shader.Find("UI/Default");
                    LP.GetComponent<Renderer>().material.color = Settings.Settings.CurrentBackgroundColor;
                    LP.transform.rotation = GorillaLocomotion.GTPlayer.Instance.leftControllerTransform.rotation;
                    LP.transform.localScale = new Vector3(0.01f, 0.3f, 0.4f);
                    // Main.Outline(LP, Color.black);
                    LP.transform.position = GorillaTagger.Instance.leftHandTransform.position - Vector3.up * 0.045f;
                    LPA = true;
                }
            }
            else
            {
                UnityEngine.Object.Destroy(LP);
                LPA = false;
            }
        }

        public static void StickyPlatforms()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                if (!RPA2)
                {
                    RP1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    RP1.GetComponent<Renderer>().material.shader = Shader.Find("UI/Default");
                    RP1.GetComponent<Renderer>().material.color = Settings.Settings.CurrentBackgroundColor;
                    RP1.transform.rotation = GorillaLocomotion.GTPlayer.Instance.rightControllerTransform.rotation;
                    RP1.transform.localScale = new Vector3(0.01f, 0.3f, 0.4f);
                    //Main.Outline(RP1, Color.black);
                    RP1.transform.position = GorillaTagger.Instance.rightHandTransform.position - Vector3.up * 0.028f;
                    RP2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    RP2.GetComponent<Renderer>().material.shader = Shader.Find("UI/Default");
                    RP2.GetComponent<Renderer>().material.color = Settings.Settings.CurrentBackgroundColor;
                    RP2.transform.rotation = GorillaLocomotion.GTPlayer.Instance.rightControllerTransform.rotation;
                    RP2.transform.localScale = new Vector3(0.01f, 0.3f, 0.4f);
                    //Main.Outline(RP2, Color.black);
                    RP2.transform.position = GorillaTagger.Instance.rightHandTransform.position + Vector3.up * 0.028f;
                    RPA2 = true;
                }
            }
            else
            {
                GameObject.Destroy(RP1);
                GameObject.Destroy(RP2);
                RPA2 = false;
            }
            if (ControllerInputPoller.instance.leftGrab)
            {
                if (!LPA2)
                {
                    LP1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    LP1.GetComponent<Renderer>().material.shader = Shader.Find("UI/Default");
                    LP1.GetComponent<Renderer>().material.color = Settings.Settings.CurrentBackgroundColor;
                    LP1.transform.rotation = GorillaLocomotion.GTPlayer.Instance.leftControllerTransform.rotation;
                    LP1.transform.localScale = new Vector3(0.01f, 0.3f, 0.4f);
                    //Main.Outline(LP1, Color.black);
                    LP1.transform.position = GorillaTagger.Instance.leftHandTransform.position - Vector3.up * 0.028f;
                    LP2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    LP2.GetComponent<Renderer>().material.shader = Shader.Find("UI/Default");
                    LP2.GetComponent<Renderer>().material.color = Settings.Settings.CurrentBackgroundColor;
                    LP2.transform.rotation = GorillaLocomotion.GTPlayer.Instance.leftControllerTransform.rotation;
                    LP2.transform.localScale = new Vector3(0.01f, 0.3f, 0.4f);
                    //Main.Outline(LP2, Color.black);
                    LP2.transform.position = GorillaTagger.Instance.leftHandTransform.position + Vector3.up * 0.028f;
                    LPA2 = true;
                }
            }
            else
            {
                GameObject.Destroy(LP1);
                GameObject.Destroy(LP2);
                LPA2 = false;
            }
        }
        

        public static void InvisPlatforms()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                if (!RPA3)
                {
                    RP3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    RP3.GetComponent<Renderer>().material.shader = Shader.Find("UI/Default");
                    RP3.GetComponent<Renderer>().material.color = Color.clear;
                    RP3.transform.rotation = GorillaLocomotion.GTPlayer.Instance.rightControllerTransform.rotation;
                    RP3.transform.localScale = new Vector3(0.01f, 0.3f, 0.4f);
                    RP3.transform.position = GorillaTagger.Instance.rightHandTransform.position - Vector3.up * 0.035f;
                    RPA3 = true;
                }
            }
            else
            {
                UnityEngine.Object.Destroy(RP3);
                RPA3 = false;
            }
            if (ControllerInputPoller.instance.leftGrab)
            {
                if (!LPA3)
                {
                    LP3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    LP3.GetComponent<Renderer>().material.shader = Shader.Find("UI/Default");
                    LP3.GetComponent<Renderer>().material.color = Color.clear;
                    LP3.transform.rotation = GorillaLocomotion.GTPlayer.Instance.leftControllerTransform.rotation;
                    LP3.transform.localScale = new Vector3(0.01f, 0.3f, 0.4f);
                    LP3.transform.position = GorillaTagger.Instance.leftHandTransform.position - Vector3.up * 0.035f;
                    LPA3 = true;
                }
            }
            else
            {
                UnityEngine.Object.Destroy(LP3);
                LPA3 = false;
            }
        }

        public static void Frozone()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {

                RP5 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                RP5.GetComponent<Renderer>().material.shader = Shader.Find("UI/Default");
                RP5.GetComponent<Renderer>().material.color = Settings.Settings.CurrentBackgroundColor;
                RP5.AddComponent<GorillaSurfaceOverride>().overrideIndex = 61;
                RP5.transform.rotation = GorillaLocomotion.GTPlayer.Instance.rightControllerTransform.rotation;
                RP5.transform.localScale = new Vector3(0.01f, 0.3f, 0.4f);
                RP5.transform.position = GorillaTagger.Instance.rightHandTransform.position - Vector3.up * 0.045f;
                RPA5 = true;
                UnityEngine.Object.Destroy(RP5, 5f);
            }

            if (ControllerInputPoller.instance.leftGrab)
            {

                LP5 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                LP5.GetComponent<Renderer>().material.shader = Shader.Find("UI/Default");
                LP5.GetComponent<Renderer>().material.color = Settings.Settings.CurrentBackgroundColor;
                LP5.AddComponent<GorillaSurfaceOverride>().overrideIndex = 61;
                LP5.transform.rotation = GorillaLocomotion.GTPlayer.Instance.leftControllerTransform.rotation;
                LP5.transform.localScale = new Vector3(0.01f, 0.3f, 0.4f);
                LP5.transform.position = GorillaTagger.Instance.leftHandTransform.position - Vector3.up * 0.045f;
                LPA5 = true;
                UnityEngine.Object.Destroy(LP5, 5f);
            }
        }

        public static void Drag(float Amount)
        {
            GorillaTagger.Instance.rigidbody.drag = Amount;
        }

        public static void NoClip()
        {
            if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.1f)
            {
                foreach (MeshCollider mesh in Resources.FindObjectsOfTypeAll<MeshCollider>())
                {
                    mesh.enabled = false;
                }
            }
            else
            {
                foreach (MeshCollider mesh in Resources.FindObjectsOfTypeAll<MeshCollider>())
                {
                    mesh.enabled = true;
                }
            }
        }

        public static void Fly()
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                GorillaLocomotion.GTPlayer.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
                GorillaLocomotion.GTPlayer.Instance.transform.position += GorillaLocomotion.GTPlayer.Instance.headCollider.transform.forward * Settings.Settings.FlySpeed * Time.deltaTime;

            }
        }

        public static void HandFly()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                GorillaLocomotion.GTPlayer.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
                GorillaLocomotion.GTPlayer.Instance.transform.position += GorillaLocomotion.GTPlayer.Instance.rightControllerTransform.transform.forward * Settings.Settings.FlySpeed * Time.deltaTime;
            }
        }

        public static void NoclipFly()
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                foreach (MeshCollider mesh in Resources.FindObjectsOfTypeAll<MeshCollider>())
                {
                    mesh.enabled = false;
                }
                GorillaLocomotion.GTPlayer.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
                GorillaLocomotion.GTPlayer.Instance.transform.position += GorillaLocomotion.GTPlayer.Instance.headCollider.transform.forward * Settings.Settings.FlySpeed * Time.deltaTime;
            }
            else
            {
                foreach (MeshCollider mesh in Resources.FindObjectsOfTypeAll<MeshCollider>())
                {
                    mesh.enabled = true;
                }
            }
        }

        public static void triggerFly()
        {
            if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.5f)
            {
                GorillaLocomotion.GTPlayer.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
                GorillaLocomotion.GTPlayer.Instance.transform.position += GorillaLocomotion.GTPlayer.Instance.headCollider.transform.forward * Settings.Settings.FlySpeed * Time.deltaTime;
            }
        }

        public static void SlingShot()
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                GorillaTagger.Instance.rigidbody.velocity += GorillaTagger.Instance.offlineVRRig.headMesh.transform.forward * Settings.Settings.FlySpeed * Time.deltaTime;
            }
        }

        public static void TriggerSlingShot()
        {
            if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.5f)
            {
                GorillaTagger.Instance.rigidbody.velocity += GorillaTagger.Instance.offlineVRRig.headMesh.transform.forward * Settings.Settings.FlySpeed * Time.deltaTime;

            }
        }

        private static Vector3 wallContactPoint;
        private static Vector3 wallContactNormal;

        public static void WallWalk()
        {
            if (GTPlayer.Instance.IsHandTouching(true) || GTPlayer.Instance.IsHandTouching(false))
            {
                var hitInfo = GTPlayer.Instance.lastHitInfoHand;
                wallContactPoint = hitInfo.point;
                wallContactNormal = hitInfo.normal;
            }

            if (wallContactPoint != Vector3.zero && ControllerInputPoller.instance.rightGrab || ControllerInputPoller.instance.leftGrab)
            {
                var rb = GorillaTagger.Instance.rigidbody;
                rb.AddForce(-wallContactNormal * 4.6f, ForceMode.Acceleration);
                ZeroGravity();
            }
        }

        public static void LegitimateWallWalk() // Thanks to ii for letting me use his code
        {
            float range = 0.2f;
            float power = -2f;

            if (ControllerInputPoller.instance.leftGrab)
            {
                RaycastHit ray = GTPlayer.Instance.lastHitInfoHand;

                if (Physics.Raycast(GorillaTagger.Instance.leftHandTransform.position, -ray.normal, out var Ray, range, GTPlayer.Instance.locomotionEnabledLayers))
                    GorillaTagger.Instance.rigidbody.AddForce(Ray.normal * power, ForceMode.Acceleration);
            }

            if (ControllerInputPoller.instance.rightGrab)
            {
                RaycastHit ray = GTPlayer.Instance.lastHitInfoHand;

                if (Physics.Raycast(GorillaTagger.Instance.rightHandTransform.position, -ray.normal, out var Ray, range, GTPlayer.Instance.locomotionEnabledLayers))
                    GorillaTagger.Instance.rigidbody.AddForce(Ray.normal * power, ForceMode.Acceleration);
            }
        }

        public static void MosaSpeedBoost()
        {
            GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 8.5f;
            GorillaLocomotion.GTPlayer.Instance.jumpMultiplier = 1.25f;
        }

        public static void SpeedBoost()
        {
            GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 10f;
            GorillaLocomotion.GTPlayer.Instance.jumpMultiplier = 2.2f;
        }


        private static ParticleSystem leftParticleSystem = null;
        private static ParticleSystem rightParticleSystem = null;

        public static void IronMonke()
        {
            if (ControllerInputPoller.instance.leftGrab)
            {
                GorillaTagger.Instance.rigidbody.AddForce(Settings.Settings.FlySpeed * -GorillaTagger.Instance.leftHandTransform.right, ForceMode.Acceleration);
                GorillaTagger.Instance.StartVibration(true, GorillaTagger.Instance.tapHapticStrength / 50f * GorillaLocomotion.GTPlayer.Instance.bodyCollider.attachedRigidbody.velocity.magnitude, GorillaTagger.Instance.tapHapticDuration);

                if (leftParticleSystem == null || !leftParticleSystem.gameObject.activeInHierarchy)
                {
                    leftParticleSystem = new GameObject("LeftIronParticle").AddComponent<ParticleSystem>();
                    var ps = leftParticleSystem;
                    ps.transform.position = GorillaLocomotion.GTPlayer.Instance.leftControllerTransform.position;
                    ps.transform.rotation = GorillaLocomotion.GTPlayer.Instance.leftControllerTransform.rotation;

                    var main = ps.main;
                    main.startSize = 0.3f;
                    main.startSpeed = 31f;
                    main.startLifetime = 0.4f;
                    main.startColor = new ParticleSystem.MinMaxGradient(ColorLib.RedOrange);
                    main.maxParticles = 40000;

                    var emission = ps.emission;
                    emission.rateOverTime = 500;

                    var shape = ps.shape;
                    shape.shapeType = ParticleSystemShapeType.Cone;
                    shape.angle = 20;
                    shape.radius = 0.3f;

                    var renderer = ps.GetComponent<Renderer>();
                    renderer.material.shader = Shader.Find("GorillaTag/UberShader");
                    renderer.material.color = ColorLib.RedOrange;

                    ps.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                    ps.Play();
                }
                else
                {
                    leftParticleSystem.transform.position = GorillaLocomotion.GTPlayer.Instance.leftControllerTransform.position;
                    leftParticleSystem.transform.rotation = GorillaLocomotion.GTPlayer.Instance.leftControllerTransform.rotation * Quaternion.Euler(180, 270, 90);
                }
            }
            else if (leftParticleSystem != null)
            {
                leftParticleSystem.Stop();
                UnityEngine.Object.Destroy(leftParticleSystem.gameObject, 3f);
                leftParticleSystem = null;
            }

            if (ControllerInputPoller.instance.rightGrab)
            {
                GorillaTagger.Instance.rigidbody.AddForce(-Settings.Settings.FlySpeed * -GorillaTagger.Instance.rightHandTransform.right, ForceMode.Acceleration);
                GorillaTagger.Instance.StartVibration(true, GorillaTagger.Instance.tapHapticStrength / 50f * GorillaLocomotion.GTPlayer.Instance.bodyCollider.attachedRigidbody.velocity.magnitude, GorillaTagger.Instance.tapHapticDuration);

                if (rightParticleSystem == null || !rightParticleSystem.gameObject.activeInHierarchy)
                {
                    rightParticleSystem = new GameObject("RightIronParticle").AddComponent<ParticleSystem>();
                    var ps = rightParticleSystem;
                    ps.transform.position = GorillaLocomotion.GTPlayer.Instance.rightControllerTransform.position;
                    ps.transform.rotation = GorillaLocomotion.GTPlayer.Instance.rightControllerTransform.rotation;

                    var main = ps.main;
                    main.startSize = 0.3f;
                    main.startSpeed = 31f;
                    main.startLifetime = 0.4f;
                    main.startColor = new ParticleSystem.MinMaxGradient(ColorLib.RedOrange);
                    main.maxParticles = 40000;

                    var emission = ps.emission;
                    emission.rateOverTime = 500;

                    var shape = ps.shape;
                    shape.shapeType = ParticleSystemShapeType.Cone;
                    shape.angle = 20;
                    shape.radius = 0.44f;

                    var renderer = ps.GetComponent<Renderer>();
                    renderer.material.shader = Shader.Find("GorillaTag/UberShader");
                    renderer.material.color = ColorLib.RedOrange;

                    ps.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                    ps.Play();
                }
                else
                {
                    rightParticleSystem.transform.position = GorillaLocomotion.GTPlayer.Instance.rightControllerTransform.position;
                    rightParticleSystem.transform.rotation = GorillaLocomotion.GTPlayer.Instance.rightControllerTransform.rotation * Quaternion.Euler(180, -270, 90);
                }
            }
            else if (rightParticleSystem != null)
            {
                rightParticleSystem.Stop();
                UnityEngine.Object.Destroy(rightParticleSystem.gameObject, 3f);
                rightParticleSystem = null;
            }
        }

        public static void ToggleSpeedBoost()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 10f;
                GorillaLocomotion.GTPlayer.Instance.jumpMultiplier = 1.9f;
            }
            else
            {
                GorillaLocomotion.GTPlayer.Instance.maxJumpSpeed = 8.5f;
                GorillaLocomotion.GTPlayer.Instance.jumpMultiplier = 1.1f;
            }
        }

        public static void NoSlippyWalls()
        {
            GTPlayer.Instance.isLeftHandSliding = false;
            GTPlayer.Instance.isRightHandSliding = false;
        }

        public static void LowGravity()
        {
            GorillaLocomotion.GTPlayer.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.up * (Time.deltaTime * (6.66f / Time.deltaTime)), ForceMode.Acceleration);
        }
        public static void ToggleLowGravity()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                GorillaLocomotion.GTPlayer.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.up * (Time.deltaTime * (6.66f / Time.deltaTime)), ForceMode.Acceleration);
            }
        }
        public static void ZeroGravity()
        {
            GorillaLocomotion.GTPlayer.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.up * (Time.deltaTime * (9.81f / Time.deltaTime)), ForceMode.Acceleration);
        }
        public static void HighGravity()
        {
            GorillaLocomotion.GTPlayer.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.down * (Time.deltaTime * (7.77f / Time.deltaTime)), ForceMode.Acceleration);
        }

        public static void UpAndDown()
        {
            if ((ControllerInputPoller.instance.rightControllerIndexFloat > 0.5f) || ControllerInputPoller.instance.rightGrab)
    {
                ZeroGravity();
            }
            if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.5f)
            {
                GorillaLocomotion.GTPlayer.Instance.GetComponent<Rigidbody>().velocity += Vector3.up * Time.deltaTime * Settings.Settings.FlySpeed * 3f;
            }

            if (ControllerInputPoller.instance.rightGrab)
            {
                GorillaLocomotion.GTPlayer.Instance.GetComponent<Rigidbody>().velocity += Vector3.up * Time.deltaTime * Settings.Settings.FlySpeed * -3f;
            }
        }

        public static void LeftAndRight()
        {
            if ((ControllerInputPoller.instance.rightControllerIndexFloat > 0.5f) || ControllerInputPoller.instance.rightGrab)
                {
                    ZeroGravity();
                }
                if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.5f)
                {
                    GorillaLocomotion.GTPlayer.Instance.GetComponent<Rigidbody>().velocity += GorillaTagger.Instance.bodyCollider.transform.right * Time.deltaTime * Settings.Settings.FlySpeed * -3f;
                }

                if (ControllerInputPoller.instance.rightGrab)
                {
                    GorillaLocomotion.GTPlayer.Instance.GetComponent<Rigidbody>().velocity += GorillaTagger.Instance.bodyCollider.transform.right * Time.deltaTime * Settings.Settings.FlySpeed * 3f;
                }
        }

        public static void ForwardsAndBackwards()
        {
            if ((ControllerInputPoller.instance.rightControllerIndexFloat > 0.5f) || ControllerInputPoller.instance.rightGrab)
            {
                ZeroGravity();
            }
            if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.5f)
            {
                GorillaLocomotion.GTPlayer.Instance.GetComponent<Rigidbody>().velocity += GorillaTagger.Instance.bodyCollider.transform.forward * Time.deltaTime * Settings.Settings.FlySpeed * 3f;
            }

            if (ControllerInputPoller.instance.rightGrab)
            {
                GorillaLocomotion.GTPlayer.Instance.GetComponent<Rigidbody>().velocity += GorillaTagger.Instance.bodyCollider.transform.forward * Time.deltaTime * Settings.Settings.FlySpeed * -3f;
            }
        }

        public static void TPToStump()
        {
            if (GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom").activeSelf == true)
            {
                if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.1f)
                {
                    foreach (MeshCollider mesh in Resources.FindObjectsOfTypeAll<MeshCollider>())
                    {
                        mesh.enabled = false;
                    }
                    GorillaLocomotion.GTPlayer.Instance.transform.position = new Vector3(-66.4848f, 11.8871f, -82.6619f);
                }
                else
                {
                    foreach (MeshCollider mesh in Resources.FindObjectsOfTypeAll<MeshCollider>())
                    {
                        mesh.enabled = true;
                    }
                }
            }
        }

        public static void TPToTutorial()
        {
            if (GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom").activeSelf == true)
            {
                if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.1f)
                {
                    foreach (MeshCollider mesh in Resources.FindObjectsOfTypeAll<MeshCollider>())
                    {
                        mesh.enabled = false;
                    }
                    GorillaLocomotion.GTPlayer.Instance.transform.position = new Vector3(-82.5915f, 36.2326f, -67.6132f);
                }
                else
                {
                    foreach (MeshCollider mesh in Resources.FindObjectsOfTypeAll<MeshCollider>())
                    {
                        mesh.enabled = true;
                    }
                }
            }
        }

        public static void jetpackmod()
        {
            if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.1f)
            {
                GorillaTagger.Instance.rigidbody.velocity += GorillaTagger.Instance.headCollider.gameObject.transform.up * Settings.Settings.FlySpeed * Time.deltaTime;
            }
            if (ControllerInputPoller.instance.rightGrab)
            {
                GorillaTagger.Instance.rigidbody.velocity += GorillaTagger.Instance.bodyCollider.gameObject.transform.forward * Settings.Settings.FlySpeed * Time.deltaTime;
            }
        }

        public static void Dash()
        {
            if (ControllerInputPoller.instance.leftControllerPrimaryButton)
            {
                GorillaLocomotion.GTPlayer.Instance.ApplyKnockback(GorillaTagger.Instance.headCollider.transform.forward, 7f, true);
            }
        }
        public static void HandDash()
        {
            if (ControllerInputPoller.instance.leftControllerPrimaryButton)
            {
                GorillaLocomotion.GTPlayer.Instance.ApplyKnockback(GorillaTagger.Instance.rightHandTransform.forward, 7f, true);
            }
        }

        public static void LeftDash()
        {
            if (ControllerInputPoller.instance.leftGrab)
            {
                GorillaLocomotion.GTPlayer.Instance.ApplyKnockback(-GorillaTagger.Instance.headCollider.transform.right, 7f, true);

            }
        }

        public static void RightDash()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                GorillaLocomotion.GTPlayer.Instance.ApplyKnockback(GorillaTagger.Instance.headCollider.transform.right, 7f, true);

            }
        }

        public static void BackDash()
        {
            if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.1f)
            {
                GorillaLocomotion.GTPlayer.Instance.ApplyKnockback(-GorillaTagger.Instance.headCollider.transform.forward, 7f, true);
            }
        }

        public static void DoubleJump()
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                GorillaLocomotion.GTPlayer.Instance.ApplyKnockback(GorillaTagger.Instance.transform.up, 7f, true);
            }
        }

        public static void StrongDash()
        {
            if (ControllerInputPoller.instance.leftControllerPrimaryButton)
            {
                GorillaLocomotion.GTPlayer.Instance.ApplyKnockback(GorillaTagger.Instance.headCollider.transform.forward, 14f, true);
            }
        }

        private static Vector3 oldMousePos;

        private static float upwardTimer = 0f;
        private const float upwardInterval = 1f;  // seconds
        private const float upwardVelocityAmount = 0.07f; // velocity boost every interval
        private static bool spacePressedLastFrame = false;

        public static void WASDFly()
        {
            float speed = 7f;
            Transform camTransform = Camera.main.transform;
            var rb = GorillaTagger.Instance.rigidbody;

            rb.useGravity = false;

            if (UnityInput.Current.GetKey(KeyCode.LeftShift))
                speed *= 2.5f;

            Vector3 horizontalMove = Vector3.zero;
            Vector3 verticalMove = Vector3.zero;

            if (UnityInput.Current.GetKey(KeyCode.W) || UnityInput.Current.GetKey(KeyCode.UpArrow))
                horizontalMove += camTransform.forward;
            if (UnityInput.Current.GetKey(KeyCode.S) || UnityInput.Current.GetKey(KeyCode.DownArrow))
                horizontalMove -= camTransform.forward;
            if (UnityInput.Current.GetKey(KeyCode.A) || UnityInput.Current.GetKey(KeyCode.LeftArrow))
                horizontalMove -= camTransform.right;
            if (UnityInput.Current.GetKey(KeyCode.D) || UnityInput.Current.GetKey(KeyCode.RightArrow))
                horizontalMove += camTransform.right;

            if (UnityInput.Current.GetKey(KeyCode.LeftControl))
                verticalMove -= camTransform.up;

            if (horizontalMove != Vector3.zero)
                horizontalMove = horizontalMove.normalized * speed;

            verticalMove = verticalMove * speed;

            Vector3 currentVel = rb.velocity;
            Vector3 newVelocity = horizontalMove + verticalMove;

            newVelocity.y = currentVel.y;

            upwardTimer += Time.deltaTime;

            if (upwardTimer >= upwardInterval)
            {
                newVelocity.y += upwardVelocityAmount;
                upwardTimer = 0f;
            }

            bool spacePressed = UnityInput.Current.GetKey(KeyCode.Space);
            if (spacePressed && !spacePressedLastFrame)
            {
                newVelocity.y = 3f;
            }
            spacePressedLastFrame = spacePressed;

            rb.velocity = newVelocity;

            if (UnityInput.Current.GetMouseButton(1))
            {
                Vector3 mouseDelta = UnityInput.Current.mousePosition - oldMousePos;
                float pitch = camTransform.localEulerAngles.x - mouseDelta.y * 0.3f;
                float yaw = camTransform.localEulerAngles.y + mouseDelta.x * 0.3f;
                camTransform.localEulerAngles = new Vector3(pitch, yaw, 0f);
            }

            oldMousePos = UnityInput.Current.mousePosition;
        }
    }
}
