using GorillaLocomotion;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Text;
using static UnityEngine.ParticleSystem;
using TMPro;
using UnityEngine;
using VioletFree.Utilities;
using Object = UnityEngine.Object;

namespace VioletFree.Mods.Spammers
{
    class CS
    {
    }

    class BlackHole : MonoBehaviour
    {
        public static Vector3 pos = new Vector3(-63.2589f, 9.4352f, -65.2775f);
        public static ParticleSystem particleSystem = null;
        public static GameObject Black;
        public static Vector3 BlackScale;
        public static GameObject Hole;
        public static Vector3 Holescale;
        public static float b = 0.1f;
        public static TextMeshPro t = null;

        public static void CreateBlackHoleV2()
        {
            Black = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Black.transform.rotation = Quaternion.identity;
            Black.transform.localScale = BlackScale;
            Black.transform.localPosition = pos;
            Destroy(Black.GetComponent<Collider>());
            Black.GetComponent<Renderer>().material.color = ColorLib.DeepVioletTransparent;
            Hole = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            Hole.transform.rotation = Quaternion.identity;
            Hole.transform.localScale = BlackScale - new Vector3(0.3f, 0.3f, 0.3f);
            Hole.transform.localPosition = pos;
            Destroy(Hole.GetComponent<Collider>());
            Hole.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
            Hole.GetComponent<Renderer>().material.color = ColorLib.Black;
            Destroy(Hole, Time.deltaTime);
            Destroy(Black, Time.deltaTime);
            BlackScale += new Vector3(0.005f, 0.005f, 0.005f);

            Rigidbody attachedRigidbody = GTPlayer.Instance.bodyCollider.attachedRigidbody;
            Vector3 BlackHolePos = Black.transform.position;
            if (attachedRigidbody != null)
            {
                Vector3 vector2 = BlackHolePos - GTPlayer.Instance.bodyCollider.transform.position;
                float magnitude = vector2.magnitude;
                float num = Mathf.Clamp(1000f / magnitude, 0f, 10f);
                attachedRigidbody.AddForce(vector2.normalized * num * b * 10 / Vector3.Distance(GTPlayer.Instance.bodyCollider.transform.position, BlackHolePos) * Time.deltaTime, (ForceMode)2);
            }
        }
        public static void CreateBlackHole(float Size, bool isSlowlyGrowing)
        {
            if (!isSlowlyGrowing)
            {
                particleSystem = new GameObject("BlackHoleEffect")
                {
                    transform = { position = pos }
                }.AddComponent<ParticleSystem>();

                ParticleSystem.MainModule main = particleSystem.main;
                main.startColor = new ParticleSystem.MinMaxGradient(new Color(0f, 0f, 0f), new Color(0.1f, 0.1f, 0.1f));

                if (Size > 1)
                {
                    main.startSize = 0.4f * Size / 5;
                }
                else
                {
                    main.startSize = 0.3f;
                }

                main.startRotationYMultiplier = 0.5f;
                main.startSpeed = 0.5f * Size;
                main.startLifetime = 2f;
                main.loop = true;
                main.simulationSpace = (ParticleSystemSimulationSpace)1;
                if ((int)Size > 0)
                {
                    main.maxParticles = 600 * 2 * (int)Size;
                }
                else
                {
                    main.maxParticles = 300;
                }


                ParticleSystemRenderer component = particleSystem.GetComponent<ParticleSystemRenderer>();
                component.material = new Material(Shader.Find("Particles/Standard Unlit"));

                EmissionModule em = particleSystem.emission;
                em.rateOverTime = 30 * Size;

                RotationOverLifetimeModule p = particleSystem.rotationOverLifetime;
                p.enabled = true;
                p.yMultiplier = 5f;

                ParticleSystem.ShapeModule shape = particleSystem.shape;
                shape.shapeType = 0;
                shape.radius = 2.5f * Size;
                shape.randomDirectionAmount = 0.1f;

                RotationOverLifetimeModule rot = particleSystem.rotationOverLifetime;
                rot.z = new ParticleSystem.MinMaxCurve(0.5f, 1f);

                ParticleSystem.VelocityOverLifetimeModule velocityOverLifetime = particleSystem.velocityOverLifetime;
                velocityOverLifetime.x = new ParticleSystem.MinMaxCurve(0f, 0f);
                velocityOverLifetime.y = new ParticleSystem.MinMaxCurve(0f, 0f);
                velocityOverLifetime.z = new ParticleSystem.MinMaxCurve(-1f, -2f);

                particleSystem.Play();

                Object.Destroy(particleSystem, 2f);
                Rigidbody attachedRigidbody = GTPlayer.Instance.bodyCollider.attachedRigidbody;
                Vector3 BlackHolePos = particleSystem.gameObject.transform.position;
                if (attachedRigidbody != null)
                {
                    Vector3 vector2 = BlackHolePos - GTPlayer.Instance.bodyCollider.transform.position;
                    float magnitude = vector2.magnitude;
                    float num = Mathf.Clamp(1000f / magnitude, 0f, 10f);
                    attachedRigidbody.AddForce(vector2.normalized * num * Size * 10 / Vector3.Distance(GTPlayer.Instance.bodyCollider.transform.position, BlackHolePos) * Time.deltaTime, (ForceMode)2);
                }
                Collider[] array = Physics.OverlapSphere(BlackHolePos, 10f);
                foreach (Collider collider in array)
                {
                    Rigidbody component3 = collider.GetComponent<Rigidbody>();
                    if (component3 != null)
                    {
                        Vector3 vector3 = BlackHolePos - collider.transform.position;
                        float magnitude2 = vector3.magnitude;
                        float num2 = Mathf.Clamp(1000f / magnitude2, 0f, 10f);
                        component3.AddForce(vector3.normalized * num2 * Time.deltaTime, (ForceMode)2);
                    }
                }
                if (Vector3.Distance(GTPlayer.Instance.bodyCollider.transform.position, BlackHolePos) < 0.4 * Size)
                {
                    PhotonNetwork.Disconnect();
                }
                GameObject ring = new GameObject("BlackHoleRing");
                ring.transform.position = pos;
                ring.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                ParticleSystem ringSystem = ring.AddComponent<ParticleSystem>();
                ParticleSystem.MainModule ringMain = ringSystem.main;
                int a = UnityEngine.Random.Range(20, 76);
                ringMain.startColor = new Color(a / 255f, a / 255f, a / 255f);
                ringMain.startSize = Size / 4;
                ringMain.startSpeed = 0f;
                ringMain.startLifetime = 2f;
                ringMain.startRotationY = 90f;
                ringMain.loop = true;

                ParticleSystemRenderer component2 = ringSystem.GetComponent<ParticleSystemRenderer>();
                component2.material = new Material(Shader.Find("Particles/Standard Unlit"));

                ParticleSystem.ShapeModule ringShape = ringSystem.shape;
                ringShape.shapeType = ParticleSystemShapeType.Donut;
                ringShape.radius = 5f * Size;
                ringShape.angle = 5f;

                ringSystem.Play();

                GameObject.Destroy(ring, 2);
                GameObject.Destroy(particleSystem.gameObject, 2);



            }
            else if (isSlowlyGrowing)
            {
                particleSystem = new GameObject("BlackHoleEffect")
                {
                    transform = { position = pos }
                }.AddComponent<ParticleSystem>();

                ParticleSystem.MainModule main = particleSystem.main;
                main.startColor = new ParticleSystem.MinMaxGradient(new Color(0f, 0f, 0f), new Color(0.1f, 0.1f, 0.1f));

                if (Size > 1)
                {
                    main.startSize = 0.4f * Size / 5;
                }
                else
                {
                    main.startSize = 0.3f;
                }

                main.startRotationYMultiplier = 0.5f;
                main.startSpeed = 0.5f * b;
                main.startLifetime = 2f;
                main.loop = true;
                main.simulationSpace = (ParticleSystemSimulationSpace)1;
                if ((int)b > 0)
                {
                    main.maxParticles = 150 * 2 * (int)b;
                }
                else
                {
                    main.maxParticles = 75;
                }


                ParticleSystemRenderer component = particleSystem.GetComponent<ParticleSystemRenderer>();
                component.material = new Material(Shader.Find("Particles/Standard Unlit"));

                EmissionModule em = particleSystem.emission;
                em.rateOverTime = 30 * b;

                RotationOverLifetimeModule p = particleSystem.rotationOverLifetime;
                p.enabled = true;
                p.yMultiplier = 5f;

                ParticleSystem.ShapeModule shape = particleSystem.shape;
                shape.shapeType = 0;
                shape.radius = 2.5f * b;
                shape.randomDirectionAmount = 0.1f;

                RotationOverLifetimeModule rot = particleSystem.rotationOverLifetime;
                rot.z = new ParticleSystem.MinMaxCurve(0.5f, 1f);

                ParticleSystem.VelocityOverLifetimeModule velocityOverLifetime = particleSystem.velocityOverLifetime;
                velocityOverLifetime.x = new ParticleSystem.MinMaxCurve(0f, 0f);
                velocityOverLifetime.y = new ParticleSystem.MinMaxCurve(0f, 0f);
                velocityOverLifetime.z = new ParticleSystem.MinMaxCurve(-1f, -2f);

                particleSystem.Play();

                Object.Destroy(particleSystem, 2f);
                Rigidbody attachedRigidbody = GTPlayer.Instance.bodyCollider.attachedRigidbody;
                Vector3 BlackHolePos = particleSystem.gameObject.transform.position;
                if (attachedRigidbody != null)
                {
                    Vector3 vector2 = BlackHolePos - GTPlayer.Instance.bodyCollider.transform.position;
                    float magnitude = vector2.magnitude;
                    float num = Mathf.Clamp(1000f / magnitude, 0f, 10f);
                    attachedRigidbody.AddForce(vector2.normalized * num * b * 10 / Vector3.Distance(GTPlayer.Instance.bodyCollider.transform.position, BlackHolePos) * Time.deltaTime, (ForceMode)2);
                }
                Collider[] array = Physics.OverlapSphere(BlackHolePos, 10f);
                foreach (Collider collider in array)
                {
                    Rigidbody component3 = collider.GetComponent<Rigidbody>();
                    if (component3 != null)
                    {
                        Vector3 vector3 = BlackHolePos - collider.transform.position;
                        float magnitude2 = vector3.magnitude;
                        float num2 = Mathf.Clamp(1000f / magnitude2, 0f, 10f);
                        component3.AddForce(vector3.normalized * num2 * Time.deltaTime, (ForceMode)2);
                    }
                }
                if (Vector3.Distance(GTPlayer.Instance.bodyCollider.transform.position, BlackHolePos) < 0.4 * b)
                {
                    PhotonNetwork.Disconnect();
                }
                GameObject ring = new GameObject("BlackHoleRing");
                ring.transform.position = pos;
                ParticleSystem ringSystem = ring.AddComponent<ParticleSystem>();
                ParticleSystem.MainModule ringMain = ringSystem.main;
                int a = UnityEngine.Random.Range(20, 76);
                ring.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                ringMain.startColor = new Color(a / 255f, a / 255f, a / 255f);
                ringMain.startSize = b / 4;
                ringMain.startSpeed = 0f;
                ringMain.startLifetime = 2f;
                ringMain.startRotationY = 90f;
                ringMain.loop = true;

                ParticleSystemRenderer component2 = ringSystem.GetComponent<ParticleSystemRenderer>();
                component2.material = new Material(Shader.Find("Particles/Standard Unlit"));

                ParticleSystem.ShapeModule ringShape = ringSystem.shape;
                ringShape.shapeType = ParticleSystemShapeType.Donut;
                ringShape.radius = 5f * b;
                ringShape.angle = 5f;

                ringSystem.Play();

                GameObject.Destroy(ring, 2);
                GameObject.Destroy(particleSystem.gameObject, 2);
                int LagCD = 1;
                if (Time.time > LagCD)
                {
                    Variables.Delay = Time.time + 2f;
                    {

                    }
                }
                b += 0.002f * b;
                if (t == null)
                {
                    GameObject textObj = new GameObject("BlackHoleText");
                    t = textObj.AddComponent<TextMeshPro>();
                    t.alignment = TextAlignmentOptions.Center;
                    t.fontSize = 1f;
                    t.color = Color.white;
                    t.fontStyle = FontStyles.Bold;
                    t.transform.rotation = GorillaTagger.Instance.transform.rotation;
                }
                t.text = "Size: " + (Mathf.Round(10 * b) / 10).ToString("F1");

                t.transform.position = GorillaTagger.Instance.rightHandTransform.position + new Vector3(0, 0.12f, 0);
                t.transform.rotation = GorillaTagger.Instance.transform.rotation;
            }
        }
    }
}
