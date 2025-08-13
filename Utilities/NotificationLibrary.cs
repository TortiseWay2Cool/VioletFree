using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static VioletFree.Utilities.Variables;
using static VioletFree.Menu.Optimizations;
using HarmonyLib;
using Photon.Pun;

namespace VioletFree.Utilities
{
    internal class NotificationLib : MonoBehaviour
    {
        private GameObject HUDObj, HUDObj2, MainCamera;
        private Text notificationText;
        private static readonly Dictionary<string, float> notificationTimestamps = new Dictionary<string, float>();
        private static NotificationLib instance;
        private const float NotificationDelay = 1f;
        private bool hasInitialized;

        private readonly List<GameObject> trackedObjects = new List<GameObject>();
        private Material notificationMaterial;

        public static string PreviousNotification { get; private set; }
        public static bool IsEnabled { get; set; } = true;

        public static NotificationLib Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<NotificationLib>() ?? new GameObject("NotificationLib").AddComponent<NotificationLib>();
                }
                return instance;
            }
        }

        public void Init()
        {
            if (hasInitialized) return;

            MainCamera = GameObject.Find("Main Camera");
            if (MainCamera == null)
            {
                return;
            }

            HUDObj2 = CreateAndTrackHUDObject("HUD_Notification_Parent");
            HUDObj2.transform.position = MainCamera.transform.position + new Vector3(-1.5f, 0f, -4.5f);

            HUDObj = CreateAndTrackHUDObject("HUD_Notification", HUDObj2.transform);

            var canvas = HUDObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvas.worldCamera = MainCamera.GetComponent<Camera>();

            var canvasScaler = HUDObj.AddComponent<CanvasScaler>();
            canvasScaler.dynamicPixelsPerUnit = 10;

            HUDObj.AddComponent<GraphicRaycaster>();

            var rectTransform = HUDObj.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(5f, 5f);
            rectTransform.localScale = Vector3.one;
            rectTransform.localPosition = new Vector3(0f, 0f, 1.6f);
            rectTransform.rotation = Quaternion.Euler(0f, -270f, 0f);

            notificationText = CreateTextElement("NotificationText", HUDObj, new Vector3(-1.2f, -0.9f, -0.22f), new Vector2(260f, 70f), 7);
            notificationText.font = ResourceLoader.ArialFont;
            notificationText.fontStyle = FontStyle.BoldAndItalic;

            notificationMaterial = new Material(Shader.Find("GUI/Text Shader"));
            notificationText.material = notificationMaterial;

            hasInitialized = true;
        }

        private GameObject CreateAndTrackHUDObject(string name, Transform parent = null)
        {
            var obj = new GameObject(name);
            if (parent != null)
            {
                obj.transform.parent = parent;
            }
            trackedObjects.Add(obj);
            return obj;
        }

        private Text CreateTextElement(string name, GameObject parent, Vector3 position, Vector2 size, int fontSize)
        {
            var textObject = new GameObject(name);
            textObject.transform.parent = parent.transform;

            var text = textObject.AddComponent<Text>();
            text.fontSize = fontSize;
            text.alignment = TextAnchor.MiddleCenter;
            text.rectTransform.sizeDelta = size;
            text.rectTransform.localScale = new Vector3(0.01f, 0.01f, 1f);
            text.rectTransform.localPosition = position;

            trackedObjects.Add(textObject);
            return text;
        }

        public void UpdateNotifications()
        {
            if (!hasInitialized) Init();

            if (HUDObj2 != null && MainCamera != null)
            {
                HUDObj2.transform.SetPositionAndRotation(MainCamera.transform.position, MainCamera.transform.rotation);
            }

            ProcessExpiredNotifications();
        }

        private void ProcessExpiredNotifications()
        {
            float now = Time.time;

            foreach (var notification in notificationTimestamps.Keys.Where(notification => now - notificationTimestamps[notification] > NotificationDelay).ToList())
            {
                notificationTimestamps.Remove(notification);
                UpdateNotificationText();
            }
        }

        private void UpdateNotificationText()
        {
            if (notificationText != null)
            {
                notificationText.text = string.Join(Environment.NewLine, notificationTimestamps.Keys);
            }
        }

        public static void SendNotification(string content)
        {
            if (!toggleNotifications) return;
            if (!IsEnabled || string.IsNullOrEmpty(content) || Instance.notificationText == null || content == PreviousNotification)
            {
                return;
            }

            notificationTimestamps[content] = Time.time;
            PreviousNotification = content;
            Instance.UpdateNotificationText();
        }

        public static void ClearAllNotifications()
        {
            notificationTimestamps.Clear();
            Instance.UpdateNotificationText();
        }
    }
}