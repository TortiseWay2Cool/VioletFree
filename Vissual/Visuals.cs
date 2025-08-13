using HarmonyLib;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VioletFree.Utilities;
using Object = UnityEngine.Object;

namespace VioletFree.Mods.Vissual
{
    public class Visuals : MonoBehaviour
    {
        public static void FPSESP()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                GameObject tmpObject = new GameObject("FPSLabel");
                TextMeshPro tmp = tmpObject.AddComponent<TextMeshPro>();
                tmp.text = "FPS: " + vrrig.fps.ToString();
                tmp.font = Resources.Load<TMP_FontAsset>("LiberationSans SDF");
                tmp.fontSize = 3;
                tmp.alignment = TextAlignmentOptions.Center;
                tmp.color = ColorLib.Violet;
                tmpObject.transform.position = vrrig.transform.position + new Vector3(0, 0.7f, 0);
                if (GorillaTagger.Instance != null && GorillaTagger.Instance.headCollider != null)
                {
                    tmpObject.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                    tmpObject.transform.Rotate(0, 180f, 0);
                }
                Destroy(tmpObject, Time.deltaTime);
            }
        }

        public static void DistanceESP()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig == GorillaTagger.Instance.offlineVRRig) continue;
                GameObject tmpObject = new GameObject("DistanceLabel");
                TextMeshPro tmp = tmpObject.AddComponent<TextMeshPro>();
                tmp.text = $"Distance" + $"{Convert.ToInt32(Vector3.Distance(GorillaLocomotion.GTPlayer.Instance.headCollider.transform.position, vrrig.transform.position))}m";
                tmp.font = Resources.Load<TMP_FontAsset>("LiberationSans SDF");
                tmp.fontSize = 3;
                tmp.alignment = TextAlignmentOptions.Center;
                tmp.color = ColorLib.Violet;
                tmpObject.transform.position = vrrig.transform.position + new Vector3(0, 0.7f, 0);
                if (GorillaTagger.Instance != null && GorillaTagger.Instance.headCollider != null)
                {
                    tmpObject.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                    tmpObject.transform.Rotate(0, 180f, 0);
                }
                Destroy(tmpObject, Time.deltaTime);
            }
        }

        public static void Nametags()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig == GorillaTagger.Instance.offlineVRRig) continue;
                GameObject tmpObject = new GameObject("NametagLabel");
                TextMeshPro tmp = tmpObject.AddComponent<TextMeshPro>();
                tmp.text = vrrig.Creator.NickName;
                tmp.font = Resources.Load<TMP_FontAsset>("LiberationSans SDF");
                tmp.fontSize = 3;
                tmp.alignment = TextAlignmentOptions.Center;
                tmp.color = ColorLib.Violet;
                tmpObject.transform.position = vrrig.transform.position + new Vector3(0, 0.7f, 0);
                if (GorillaTagger.Instance != null && GorillaTagger.Instance.headCollider != null)
                {
                    tmpObject.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                    tmpObject.transform.Rotate(0, 180f, 0);
                }
                Destroy(tmpObject, Time.deltaTime);
            }
        }

        public static void HeadsetChecker()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig == GorillaTagger.Instance.offlineVRRig) continue;
                GameObject tmpObject = new GameObject("HeadsetLabel");
                TextMeshPro tmp = tmpObject.AddComponent<TextMeshPro>();
                int fps = Traverse.Create(vrrig).Field("fps").GetValue<int>();
                string quest = fps < 83 ? "Quest 2 / Quest 3s" : fps > 83 && fps < 100 ? "Quest 3 / Quest 3s" : "Steam / PcVr";
                tmp.text = $"Headset: {quest}";
                tmp.font = Resources.Load<TMP_FontAsset>("LiberationSans SDF");
                tmp.fontSize = 3;
                tmp.alignment = TextAlignmentOptions.Center;
                tmp.color = ColorLib.Violet;
                tmpObject.transform.position = vrrig.transform.position + new Vector3(0, 0.7f, 0);
                if (GorillaTagger.Instance != null && GorillaTagger.Instance.headCollider != null)
                {
                    tmpObject.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                    tmpObject.transform.Rotate(0, 180f, 0);
                }
                Destroy(tmpObject, Time.deltaTime);
            }
        }

        static readonly float LineWidth = 0.025f;
        static readonly float Alpha = 0.8f;
        static readonly int Segments = 32;
        static readonly float Radius = 0.6f;
        static Vector3[] HorizontalPoints;
        static Vector3[] VerticalPoints;
        private static readonly Shader lineShader = Shader.Find("GUI/Text Shader");
        static readonly Shader DefaultShader = Shader.Find("GUI/Text Shader") ?? Shader.Find("Unlit/Color");
        public static void CircleFrameIDP()
        {
            static void InitializeCirclePoints()
            {
                if (HorizontalPoints == null)
                {
                    HorizontalPoints = PrecomputeCirclePoints(Segments, Radius, true);
                }
                if (VerticalPoints == null)
                {
                    VerticalPoints = PrecomputeCirclePoints(Segments, Radius, false);
                }
            }

            InitializeCirclePoints();
            VRRig offlineRig = GorillaTagger.Instance.offlineVRRig;

            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig == offlineRig) continue;

                Vector3 position = vrrig.transform.position;
                Color playerColor = vrrig.playerColor;

                // Create horizontal circle
                CreateCircle(position, playerColor, HorizontalPoints, true);

                // Create vertical circle
                CreateCircle(position, playerColor, VerticalPoints, false);
            }

            static Vector3[] PrecomputeCirclePoints(int segments, float radius, bool isHorizontal)
            {
                Vector3[] points = new Vector3[segments + 1];
                for (int i = 0; i <= segments; i++)
                {
                    float angle = i / (float)segments * Mathf.PI * 2;
                    float x = Mathf.Cos(angle) * radius;
                    float yz = Mathf.Sin(angle) * radius;
                    points[i] = isHorizontal ? new Vector3(x, 0, yz) : new Vector3(x, yz, 0);
                }
                return points;
            }

            static void CreateCircle(Vector3 center, Color color, Vector3[] precomputedPoints, bool isHorizontal)
            {
                GameObject circle = new GameObject("CircleWireframe");
                circle.transform.position = center;

                LineRenderer renderer = circle.AddComponent<LineRenderer>();
                Material material = new Material(DefaultShader)
                {
                    color = new Color(color.r, color.g, color.b, Alpha)
                };

                renderer.material = material;
                renderer.startWidth = LineWidth;
                renderer.endWidth = LineWidth;
                renderer.useWorldSpace = true;
                renderer.positionCount = precomputedPoints.Length;

                Vector3[] worldPoints = new Vector3[precomputedPoints.Length];
                for (int i = 0; i < precomputedPoints.Length; i++)
                {
                    worldPoints[i] = center + precomputedPoints[i];
                }

                renderer.SetPositions(worldPoints);

                UnityEngine.Object.Destroy(circle, Time.deltaTime);
            }
        }

        public static void CornerESP()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig == GorillaTagger.Instance.offlineVRRig) continue;
                Color color = vrrig.playerColor;
                Material material = new Material(Shader.Find("GUI/Text Shader"));
                GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                gameObject.transform.position = vrrig.transform.position;
                Object.Destroy(gameObject.GetComponent<Renderer>());
                Object.Destroy(gameObject.GetComponent<BoxCollider>());
                gameObject.transform.localScale = new Vector3(0.02f, 0.05f, 0.01f);
                gameObject.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                gameObject.GetComponent<Renderer>().enabled = false;
                Object.Destroy(gameObject, Time.deltaTime);

                GameObject[] objects = new GameObject[8];
                Vector3[] positions = new Vector3[]
                {
                    vrrig.transform.position + (gameObject.transform.up * 0.35f + gameObject.transform.right * 0.24f),
                    vrrig.transform.position + (gameObject.transform.right * 0.33f + gameObject.transform.up * 0.26f),
                    vrrig.transform.position + (gameObject.transform.up * 0.35f + gameObject.transform.right * -0.24f),
                    vrrig.transform.position + (gameObject.transform.right * -0.33f + gameObject.transform.up * 0.26f),
                    vrrig.transform.position + (gameObject.transform.up * -0.55f + gameObject.transform.right * -0.24f),
                    vrrig.transform.position + (gameObject.transform.right * -0.33f + gameObject.transform.up * -0.46f),
                    vrrig.transform.position + (gameObject.transform.up * -0.55f + gameObject.transform.right * 0.24f),
                    vrrig.transform.position + (gameObject.transform.right * 0.33f + gameObject.transform.up * -0.46f)
                };
                Vector3[] scales = new Vector3[]
                {
                    new Vector3(0.2f, 0.02f, 0.01f), new Vector3(0.02f, 0.2f, 0.01f),
                    new Vector3(0.2f, 0.02f, 0.01f), new Vector3(0.02f, 0.2f, 0.01f),
                    new Vector3(0.2f, 0.02f, 0.01f), new Vector3(0.02f, 0.2f, 0.01f),
                    new Vector3(0.2f, 0.02f, 0.01f), new Vector3(0.02f, 0.2f, 0.01f)
                };
                for (int i = 0; i < 8; i++)
                {
                    objects[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    objects[i].transform.position = positions[i];
                    Object.Destroy(objects[i].GetComponent<BoxCollider>());
                    objects[i].transform.localScale = scales[i];
                    objects[i].transform.rotation = gameObject.transform.rotation;
                    objects[i].GetComponent<Renderer>().material = material;
                    objects[i].GetComponent<Renderer>().material.color = color;
                    Object.Destroy(objects[i], Time.deltaTime);
                }
                Object.Destroy(gameObject);
            }
        }

        public static void QuestCheck()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                gameObject.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
                gameObject.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                UnityEngine.Object.Destroy(gameObject.GetComponent<SphereCollider>());
                gameObject.GetComponent<Renderer>().material.color = vrrig.concatStringOfCosmeticsAllowed.Contains("FIRST LOGIN") ? Color.green : Color.red;
                gameObject.transform.position = vrrig.transform.position + new Vector3(0, 1, 0);
                UnityEngine.Object.Destroy(gameObject, Time.deltaTime);
            }
        }

        public static void BoxESP1()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig == GorillaTagger.Instance.offlineVRRig) continue;
                GameObject espBox = GameObject.CreatePrimitive(PrimitiveType.Cube);
                espBox.transform.position = vrrig.transform.position;
                UnityEngine.Object.Destroy(espBox.GetComponent<BoxCollider>());
                espBox.transform.localScale = new Vector3(0.5f, 1f, 0.5f);
                espBox.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                UnityEngine.Color color = ColorLib.Violet;
                color.a = 1;
                espBox.GetComponent<Renderer>().material.color = color;
                UnityEngine.Object.Destroy(espBox, Time.deltaTime);
            }
        }

        public static void BoxESP2()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig == GorillaTagger.Instance.offlineVRRig) continue;
                GameObject espBox = GameObject.CreatePrimitive(PrimitiveType.Cube);
                espBox.transform.position = vrrig.transform.position;
                UnityEngine.Object.Destroy(espBox.GetComponent<BoxCollider>());
                espBox.transform.localScale = new Vector3(0.5f, 1f, 0f);
                espBox.transform.LookAt(GorillaTagger.Instance.headCollider.transform.position);
                espBox.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                UnityEngine.Color color = ColorLib.Violet;
                color.a = 1;
                espBox.GetComponent<Renderer>().material.color = color;
                UnityEngine.Object.Destroy(espBox, Time.deltaTime);
            }
        }

        public static void SphereESP()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig == GorillaTagger.Instance.offlineVRRig) continue;
                GameObject espBox = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                espBox.transform.position = vrrig.transform.position;
                UnityEngine.Object.Destroy(espBox.GetComponent<BoxCollider>());
                espBox.transform.localScale = new Vector3(0.5f, 1f, 0.5f);
                espBox.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                UnityEngine.Color color = ColorLib.Violet;
                color.a = 1;
                espBox.GetComponent<Renderer>().material.color = color;
                UnityEngine.Object.Destroy(espBox, Time.deltaTime);
            }
        }

        public static void SpazESP()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig == GorillaTagger.Instance.offlineVRRig) continue;
                GameObject espBox = GameObject.CreatePrimitive(PrimitiveType.Cube);
                espBox.transform.position = vrrig.transform.position;
                UnityEngine.Object.Destroy(espBox.GetComponent<BoxCollider>());
                espBox.transform.localScale = new Vector3(0.5f, 1f, 0.5f);
                espBox.transform.rotation = new Quaternion(UnityEngine.Random.Range(-360, 360), UnityEngine.Random.Range(-360, 360), UnityEngine.Random.Range(-360, 360), UnityEngine.Random.Range(-360, 360));
                espBox.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                UnityEngine.Color color = ColorLib.Violet;
                color.a = 1;
                espBox.GetComponent<Renderer>().material.color = color;
                UnityEngine.Object.Destroy(espBox, Time.deltaTime);
            }
        }

        public static void Tracers()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig == GorillaTagger.Instance.offlineVRRig) continue;
                GameObject line = new GameObject("Line");
                LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
                lineRenderer.SetPosition(0, GorillaTagger.Instance.rightHandTransform.position);
                lineRenderer.SetPosition(1, vrrig.transform.position);
                lineRenderer.startWidth = 0.01f;
                lineRenderer.endWidth = 0.01f;
                lineRenderer.startColor = ColorLib.Violet;
                lineRenderer.endColor = ColorLib.Violet;
                lineRenderer.material.shader = Shader.Find("GUI/Text Shader");
                UnityEngine.Object.Destroy(line, Time.deltaTime);
            }
        }

        public static void InfectionTracers()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig == GorillaTagger.Instance.offlineVRRig) continue;
                GameObject line = new GameObject("Line");
                LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
                lineRenderer.SetPosition(0, GorillaTagger.Instance.rightHandTransform.position);
                lineRenderer.SetPosition(1, vrrig.transform.position);
                lineRenderer.startWidth = 0.01f;
                lineRenderer.endWidth = 0.01f;
                if (vrrig.mainSkin.material.name.Contains("it") || vrrig.mainSkin.material.name.Contains("fected"))
                {
                    lineRenderer.startColor = Color.red;
                    lineRenderer.endColor = Color.red;
                }
                else
                {
                    lineRenderer.startColor = Color.green;
                    lineRenderer.endColor = Color.green;
                }
                lineRenderer.material.shader = Shader.Find("GUI/Text Shader");
                GameObject gameObject3 = new GameObject("Line2");
                UnityEngine.Object.Destroy(line, Time.deltaTime);
            }
        }

        public static void PrismESP()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig == GorillaTagger.Instance.offlineVRRig) continue;
                GameObject pyramidWireframe = new GameObject("PyramidWireframe");
                pyramidWireframe.transform.position = vrrig.transform.position;
                LineRenderer lineRenderer = pyramidWireframe.AddComponent<LineRenderer>();
                Shader alwaysVisibleShader = Shader.Find("GUI/Text Shader");
                if (!alwaysVisibleShader) alwaysVisibleShader = Shader.Find("Unlit/Color");
                Material seeThroughMaterial = new Material(alwaysVisibleShader);
                seeThroughMaterial.color = ColorLib.Violet;
                lineRenderer.material = seeThroughMaterial;
                lineRenderer.startWidth = 0.025f;
                lineRenderer.endWidth = 0.025f;
                lineRenderer.positionCount = 16;
                lineRenderer.useWorldSpace = true;
                float bodyWidth = 0.6f;
                float bodyHeight = 1.2f;
                Vector3 base1 = vrrig.transform.position + new Vector3(-bodyWidth / 2, 0f, -bodyWidth / 2);
                Vector3 base2 = vrrig.transform.position + new Vector3(bodyWidth / 2, 0f, -bodyWidth / 2);
                Vector3 base3 = vrrig.transform.position + new Vector3(bodyWidth / 2, 0f, bodyWidth / 2);
                Vector3 base4 = vrrig.transform.position + new Vector3(-bodyWidth / 2, 0f, bodyWidth / 2);
                Vector3 top = vrrig.transform.position + new Vector3(0f, bodyHeight, 0f);
                Vector3[] edges = {
                    base1, base2, base2, base3, base3, base4, base4, base1,
                    base1, top, base2, top, base3, top, base4, top
                };
                lineRenderer.SetPositions(edges);
                GameObject.Destroy(pyramidWireframe, Time.deltaTime);
            }
        }

        public static void ESP()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig == GorillaTagger.Instance.offlineVRRig) continue;
                vrrig.mainSkin.material.shader = Shader.Find("GUI/Text Shader");
                vrrig.mainSkin.material.color = Color.Lerp(ColorLib.DeepVioletTransparent, ColorLib.BlackTransparent, Mathf.PingPong(Time.time, 0.4f));
            }
        }

        public static void InfectionESP()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig == GorillaTagger.Instance.offlineVRRig) continue;
                vrrig.mainSkin.material.shader = Shader.Find("GUI/Text Shader");
                vrrig.mainSkin.material.color = vrrig.mainSkin.material.name.Contains("it") || vrrig.mainSkin.material.name.Contains("fected")
                    ? Color.red
                    : Color.green;
            }
        }

        public static void DisableESP()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig == GorillaTagger.Instance.offlineVRRig) continue;
                vrrig.mainSkin.material.shader = Shader.Find("GorillaTag/UberShader");
            }
        }

        public static void SnakeESP()
        {
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig == GorillaTagger.Instance.offlineVRRig) continue;
                UnityEngine.Color playerColor = ColorLib.Violet;
                GameObject trailObject = new GameObject("PlayerTrail");
                trailObject.transform.position = vrrig.transform.position;
                trailObject.transform.SetParent(vrrig.transform);
                TrailRenderer trailRenderer = trailObject.AddComponent<TrailRenderer>();
                trailRenderer.material = new Material(Shader.Find("Unlit/Color"));
                trailRenderer.material.color = new UnityEngine.Color(playerColor.r, playerColor.g, playerColor.b, 0.5f);
                trailRenderer.time = 2f;
                trailRenderer.startWidth = 0.2f;
                trailRenderer.endWidth = 0f;
                trailRenderer.startColor = playerColor;
                trailRenderer.endColor = Color.black;
                trailRenderer.autodestruct = true;
                GameObject.Destroy(trailObject, trailRenderer.time + 0.5f);
            }
        }
    }
}