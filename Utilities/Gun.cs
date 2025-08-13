using BepInEx;
using ExitGames.Client.Photon;
using Pathfinding;
using Photon.Pun;
using Photon.Realtime;
using POpusCodec.Enums;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using VioletFree.Utilities;
using VioletFree.Menu;

namespace GunTemplateee
{
    public class GunTemplate : MonoBehaviour
    {
        public static readonly Color purple = new Color(0.5f, 0f, 0.5f);
        public static int LineCurve = 500;
        public const float PointerScale = 0.4f;
        public const float PulseSpeed = 3f;
        public const float PulseAmplitude = 0.04f;
        public static readonly Color Violet = new Color(0.56f, 0.0f, 1.0f);
        public static GameObject spherepointer;
        public static GameObject lineRen;
        public static LineRenderer lineRenderer;
        public static Color lineRendererColor1 = Color.Lerp(ColorLib.Black, ColorLib.Violet, Mathf.PingPong(Time.time, 2f));
        public static Color lineRendererColor2 = Color.Lerp(ColorLib.Violet, ColorLib.Black, Mathf.PingPong(Time.time, 2f));
        public static VRRig lockedPlayer;
        public static Vector3 lr;
        public static readonly Color32 PointerColor = Violet;
        public static bool lineEnabled = true;

        public static void StartVrGun(Action action, bool LockOn)
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                Physics.Raycast(GorillaTagger.Instance.rightHandTransform.position, -GorillaTagger.Instance.rightHandTransform.up, out nray, float.MaxValue);
                if (GunTemplate.spherepointer == null)
                {
                    GunTemplate.spherepointer = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    GunTemplate.spherepointer.AddComponent<Renderer>();
                    GunTemplate.spherepointer.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    GunTemplate.spherepointer.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                    GameObject.Destroy(GunTemplate.spherepointer.GetComponent<BoxCollider>());
                    GameObject.Destroy(GunTemplate.spherepointer.GetComponent<Rigidbody>());
                    GameObject.Destroy(GunTemplate.spherepointer.GetComponent<Collider>());
                    GunTemplate.lr = GorillaTagger.Instance.offlineVRRig.rightHandTransform.position;
                    GunTemplate.spherepointer.AddComponent<GunTemplate>().StartCoroutine(GunTemplate.PulsePointer(GunTemplate.spherepointer));
                }

                if (GunTemplate.lockedPlayer == null)
                {
                    GunTemplate.spherepointer.transform.position = GunTemplate.nray.point;
                    GunTemplate.spherepointer.GetComponent<Renderer>().material.color = ColorLib.Violet;
                }
                else
                {
                    GunTemplate.spherepointer.transform.position = GunTemplate.lockedPlayer.transform.position;
                }

                GunTemplate.lr = Vector3.Lerp(GunTemplate.lr, (GorillaTagger.Instance.rightHandTransform.position + GunTemplate.spherepointer.transform.position) / 2f, Time.deltaTime * 6f);
                GameObject gameObject = new GameObject("Line");
                LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
                lineRenderer.startWidth = 0.02f;
                lineRenderer.endWidth = 0.02f;
                lineRenderer.startColor = lineRendererColor2;
                lineRenderer.endColor = lineRendererColor1;
                lineRenderer.useWorldSpace = true;
                lineRenderer.material = new Material(Shader.Find("GUI/Text Shader"));
                gameObject.AddComponent<GunTemplate>().StartCoroutine(GunTemplate.StartCurvyLineRenderer(lineRenderer, GorillaTagger.Instance.rightHandTransform.position, GunTemplate.lr, GunTemplate.spherepointer.transform.position));
                GameObject.Destroy(lineRenderer, Time.deltaTime);


                if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.5f)
                {
                    if (LockOn)
                    {
                        if (GunTemplate.lockedPlayer == null)
                        {
                            GunTemplate.lockedPlayer = GunTemplate.nray.collider.GetComponentInParent<VRRig>();
                        }
                        if (GunTemplate.lockedPlayer != null)
                        {
                            GunTemplate.spherepointer.transform.position = GunTemplate.lockedPlayer.transform.position;
                            action();
                        }
                    }
                    else
                    {
                        action();
                    }
                }
                else if (GunTemplate.lockedPlayer != null)
                {
                    GunTemplate.lockedPlayer = null;
                }
            }
            else if (GunTemplate.spherepointer != null)
            {
                GameObject.Destroy(GunTemplate.spherepointer);
                GunTemplate.spherepointer = null;
                GunTemplate.lockedPlayer = null;
            }
        }




        /*public static Vector3 CalculateBezierPoint(Vector3 start, Vector3 mid, Vector3 end, float t)
        {
            return Mathf.Pow(1 - t, 2) * start + 2 * (1 - t) * t * mid + Mathf.Pow(t, 2) * end;
        }*/

        private static void CurveLineRenderer(LineRenderer lineRenderer, Vector3 start, Vector3 mid, Vector3 end)
        {
            lineRenderer.positionCount = LineCurve;

            float springAmplitude = 0.135f; // Controls coil size
            float springFrequency = 18f;  // Controls the number of coils

            for (int i = 0; i < LineCurve; i++)
            {
                float t = (float)i / (LineCurve - 1);

                // Linear interpolation between start and end (replacing Bezier)
                Vector3 basePoint = Vector3.Lerp(start, end, t);

                // Calculate tangent for t + 0.01f using linear interpolation
                float tPlus = t + 0.01f;
                Vector3 nextPoint = Vector3.Lerp(start, end, tPlus);

                Vector3 tangent = (nextPoint - basePoint).normalized;
                Vector3 normal = Vector3.Cross(tangent, Vector3.up).normalized;
                Vector3 binormal = Vector3.Cross(tangent, normal).normalized;

                float angle = i * Mathf.PI * springFrequency / LineCurve;
                Vector3 coilOffset = normal * Mathf.Sin(angle) * springAmplitude;

                lineRenderer.SetPosition(i, basePoint + coilOffset);
            }
        }

        public static IEnumerator StartCurvyLineRenderer(LineRenderer lineRenderer, Vector3 start, Vector3 mid, Vector3 end)
        {
            while (true)
            {
                CurveLineRenderer(lineRenderer, start, mid, end);
                yield return null;
            }
        }

        private static IEnumerator PulsePointer(GameObject pointer)
        {
            Vector3 originalScale = pointer.transform.localScale;
            while (true)
            {
                float scaleFactor = 1 + Mathf.Sin(Time.time * PulseSpeed) * PulseAmplitude;
                pointer.transform.localScale = originalScale * scaleFactor;
                yield return null;
            }
        }
        public static RaycastHit nray;
        public static void StartPcGun(Action action, bool LockOn)
        {
            Ray ray = GameObject.Find("Shoulder Camera").activeSelf ? GameObject.Find("Shoulder Camera").GetComponent<Camera>().ScreenPointToRay(UnityInput.Current.mousePosition) : GorillaTagger.Instance.mainCamera.GetComponent<Camera>().ScreenPointToRay(UnityInput.Current.mousePosition);

            if (Mouse.current.rightButton.isPressed)
            {
                if (Physics.Raycast(ray.origin, ray.direction, out nray, float.PositiveInfinity, -32777) && spherepointer == null)
                {
                    if (spherepointer == null)
                    {
                        spherepointer = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        spherepointer.AddComponent<Renderer>();
                        spherepointer.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                        spherepointer.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                        GameObject.Destroy(spherepointer.GetComponent<BoxCollider>());
                        GameObject.Destroy(spherepointer.GetComponent<Rigidbody>());
                        GameObject.Destroy(spherepointer.GetComponent<Collider>());
                        lr = GorillaTagger.Instance.offlineVRRig.rightHandTransform.position;
                    }
                }
                if (lockedPlayer == null)
                {
                    spherepointer.transform.position = nray.point;
                    spherepointer.GetComponent<Renderer>().material.color = ColorLib.Violet;
                }
                else
                {
                    spherepointer.transform.position = lockedPlayer.transform.position;
                }
                lr = Vector3.Lerp(lr, (GorillaTagger.Instance.rightHandTransform.position + spherepointer.transform.position) / 2f, Time.deltaTime * 6f);
                GameObject gameObject = new GameObject("Linee");
                LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
                lineRenderer.startWidth = 0.03f;
                lineRenderer.endWidth = 0.03f;
                lineRenderer.startColor = lineRendererColor2;
                lineRenderer.endColor = lineRendererColor1;
                lineRenderer.useWorldSpace = true;

                lineRenderer.material = new Material(Shader.Find("GUI/Text Shader"));
                gameObject.AddComponent<GunTemplate>().StartCoroutine(StartCurvyLineRenderer(lineRenderer, GorillaTagger.Instance.rightHandTransform.position, lr, spherepointer.transform.position));
                GameObject.Destroy(lineRenderer, Time.deltaTime);
                if (Mouse.current.leftButton.isPressed)
                {
                    lineRenderer.startColor = lineRendererColor2;
                    lineRenderer.endColor = lineRendererColor1;
                    spherepointer.GetComponent<Renderer>().material.color = ColorLib.Black;
                    if (LockOn)
                    {
                        if (lockedPlayer == null)
                        {
                            lockedPlayer = nray.collider.GetComponentInParent<VRRig>();
                        }
                        if (lockedPlayer != null)
                        {
                            spherepointer.transform.position = lockedPlayer.transform.position;
                            action();
                        }
                        return;
                    }
                    action();
                    return;
                }
                else if (lockedPlayer != null)
                {
                    lockedPlayer = null;
                    return;
                }
            }
            else if (spherepointer != null)
            {
                GameObject.Destroy(spherepointer);
                spherepointer = null;
                lockedPlayer = null;
            }
        }


        public static void StartBothGuns(Action action, bool locko)
        {
            if (XRSettings.isDeviceActive)
            {
                StartVrGun(action, locko);
            }
            if (!XRSettings.isDeviceActive)
            {
                StartPcGun(action, locko);

            }
        }

        public static void GunExample()
        {
            GunTemplate.StartBothGuns(() =>
            {

            }, true);
        }
    }
}