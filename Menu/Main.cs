using BepInEx;
using HarmonyLib;
using Photon.Pun;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Valve.VR;
using VioletFree.Menu;
using VioletFree.Mods;
using VioletFree.Mods.Settings;
using VioletFree.Mods.Stone;
using VioletFree.Mods.Vissual;
using VioletFree.Utilities;
using static Mono.Security.X509.X520;
using static VioletFree.Initialization.PluginInfo;
using static VioletFree.Menu.ButtonHandler;
using static VioletFree.Menu.Optimizations;
using static VioletFree.Utilities.ColorLib;
using static VioletFree.Utilities.Variables;
using Object = UnityEngine.Object;

namespace VioletFree.Menu
{
    [HarmonyPatch(typeof(GorillaLocomotion.GTPlayer), "LateUpdate")]
    public class Main : MonoBehaviour
    {
        public void Awake()
        {
            Settings.ChangeProjectileType();
            ResourceLoader.LoadResources();
            taggerInstance = GorillaTagger.Instance;
            playerInstance = GorillaLocomotion.GTPlayer.Instance;
            pollerInstance = ControllerInputPoller.instance;
            thirdPersonCamera = GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera");
            cm = GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera/CM vcam1");
        }

        [HarmonyPrefix]
        public static void Prefix()
        {
            try
            {
                Destroy(clickerObj);
                if (playerInstance == null || taggerInstance == null)
                {
                    return;
                }

                foreach (ButtonHandler.Button bt in ModButtons.buttons)
                {
                    try
                    {
                        if (bt.Enabled && bt.onEnable != null)
                        {
                            bt.onEnable.Invoke();
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }

                if (NotificationLib.Instance != null)
                {
                    try
                    {
                        NotificationLib.Instance.UpdateNotifications();
                    }
                    catch (Exception ex)
                    {
                    }
                }

                if (UnityInput.Current.GetKeyDown(PCMenuKey))
                {
                    PCMenuOpen = !PCMenuOpen;
                }
                HandleMenuInteraction();
            }
            catch (NullReferenceException ex)
            {
            }
            catch (Exception ex)
            {
            }
        }

        public static void HandleMenuInteraction()
        {
            try
            {
                    if (PCMenuOpen && !InMenuCondition && !pollerInstance.leftControllerPrimaryButton && !pollerInstance.rightControllerPrimaryButton && !menuOpen)
                {
                    InPcCondition = true;
                    cm?.SetActive(false);

                    if (menuObj == null)
                    {
                        Draw();
                        AddButtonClicker(thirdPersonCamera?.transform);
                    }
                    else
                    {
                        AddButtonClicker(thirdPersonCamera?.transform);
                        PositionMenuForKeyboard();

                        if (Mouse.current.leftButton.isPressed)
                        {
                            Ray ray = thirdPersonCamera.GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());
                            if (Physics.Raycast(ray, out RaycastHit hit))
                            {
                                BtnCollider btnCollider = hit.collider?.GetComponent<BtnCollider>();
                                if (btnCollider != null && clickerObj != null)
                                {
                                    btnCollider.OnTriggerEnter(clickerObj.GetComponent<Collider>());
                                }
                            }
                        }
                        else if (clickerObj != null)
                        {
                            Optimizations.DestroyObject(ref clickerObj);
                        }
                    }
                }
                else if (menuObj != null && InPcCondition)
                {
                    InPcCondition = false;
                    CleanupMenu(0);
                    cm?.SetActive(true);
                }

                openMenu = rightHandedMenu ? pollerInstance.rightControllerSecondaryButton : pollerInstance.leftControllerSecondaryButton;

                if (openMenu && !InPcCondition)
                {
                    InMenuCondition = true;
                    if (menuObj == null)
                    {
                        Draw();
                        AddButtonClicker(rightHandedMenu ? playerInstance.leftControllerTransform : playerInstance.rightControllerTransform);
                    }
                    else
                    {
                        PositionMenuForHand();
                    }
                }
                else if (menuObj != null && InMenuCondition)
                {
                    InMenuCondition = false;
                    CleanupMenu(0f);
                }
            }
            catch (Exception ex)
            {
            }
        }
        public static void Draw()
        {
            if (menuObj != null)
            {
                ClearMenuObjects();
                return;
            }

            CreateMenuObject();
            CreateBackground();
            CreateMenuCanvasAndTitle();
            AddTitleAndFPSCounter();
            AddReturnButton();
            AddDisconnectButton();
            AddPageButton(">");
            AddPageButton("<");

            ButtonsPerPage = VioletFree.Mods.Settings.Settings.LongMenuEnabled ? 99 : 9;

            ButtonPool.ResetPool();
            var pageToDraw = GetButtonInfoByPage(currentPage).Skip(currentCategoryPage * ButtonsPerPage).Take(ButtonsPerPage).ToArray();
            for (int i = 0; i < pageToDraw.Length; i++)
            {
                AddModButtons(i * 0.09f, pageToDraw[i]);
            }
        }

        private static void CreateMenuObject()
        {
            menuObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Destroy(menuObj.GetComponent<Rigidbody>());
            Destroy(menuObj.GetComponent<BoxCollider>());
            Destroy(menuObj.GetComponent<Renderer>());
            menuObj.name = "menu";
            menuObj.transform.localScale = new Vector3(0.1f, Settings.WideMenuEnabled ? 0.8f : 0.3f, 0.3825f);
        }

        public static Color32 color = new Color32(65, 105, 225, 50);

        private static void CreateBackground()
        {
            background = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Object.Destroy(background.GetComponent<Rigidbody>());
            Object.Destroy(background.GetComponent<BoxCollider>());
            if (OutlineEnabled) Outline(background, Color.black);
            background.GetComponent<MeshRenderer>().material.color = Settings.CurrentBackgroundColor;
            background.transform.parent = menuObj.transform;
            background.transform.rotation = Quaternion.identity;
            Variables.background.transform.localScale = new Vector3(0.1f, 1.28f, Settings.LongMenuEnabled ? 10.39f : 1.25f);
            background.name = "menucolor";
            background.transform.position = new Vector3(0.05f, 0f, 0f);

        }

        private static void CreateMenuCanvasAndTitle()
        {
            canvasObj = new GameObject();
            canvasObj.transform.parent = menuObj.transform;
            canvasObj.name = "canvas";
            Canvas canvas = canvasObj.AddComponent<Canvas>();
            CanvasScaler canvasScale = canvasObj.AddComponent<CanvasScaler>();
            canvasObj.AddComponent<GraphicRaycaster>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvasScale.dynamicPixelsPerUnit = 1000;

            GameObject titleObj = new GameObject();
            titleObj.transform.parent = canvasObj.transform;
            titleObj.transform.localScale = new Vector3(0.875f, 0.875f, 1f);
            title = titleObj.AddComponent<UnityEngine.UI.Text>();
            title.font = ResourceLoader.ArialFont;
            title.fontStyle = FontStyle.BoldAndItalic;
            title.color = Variables.MainTitleColor;
            title.fontSize = 10;
            title.alignment = TextAnchor.MiddleCenter;
            title.resizeTextForBestFit = true;
            title.resizeTextMinSize = 0;
            RectTransform titleTransform = title.GetComponent<RectTransform>();
            titleTransform.localPosition = Vector3.zero;
            titleTransform.position = new Vector3(0.057f, 0f, .21f);
            titleTransform.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            titleTransform.sizeDelta = new Vector2(0.35f, 0.06f);
        }

        public static void Outline(GameObject obj, Color clr)
        {
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
            UnityEngine.Object.Destroy(gameObject.GetComponent<BoxCollider>());
            gameObject.transform.parent = obj.transform;
            gameObject.transform.rotation = Quaternion.identity;
            gameObject.transform.localPosition = obj.transform.localPosition;
            gameObject.transform.localScale = obj.transform.localScale + new Vector3(-0.01f, 0.0145f, 0.0145f);
            //gameObject.AddComponent<AnimatedGradient>();
            gameObject.GetComponent<Renderer>().material.color = OutlineEnabled ? clr : Color.clear;
        }

        public static void AddDisconnectButton()
        {
            if (toggledisconnectButton)
            {
                // Disconnect Button
                disconnectButton = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Destroy(disconnectButton.GetComponent<Rigidbody>());
                Outline(disconnectButton, ColorLib.Grey);
                disconnectButton.GetComponent<BoxCollider>().isTrigger = true;
                disconnectButton.transform.parent = menuObj.transform;
                disconnectButton.transform.rotation = Quaternion.identity;
                disconnectButton.transform.localScale = new Vector3(0.09f, 1.14f, 0.08f);
                disconnectButton.transform.localPosition = new Vector3(0.56f, 0f, 0.42f);
                disconnectButton.AddComponent<BtnCollider>().clickedButton = new ButtonHandler.Button("DisconnectButton", Category.Home, false, false, null, null);
                disconnectButton.GetComponent<Renderer>().material.color = DisconnecyColor;

                Text discontext = new GameObject { transform = { parent = canvasObj.transform } }.AddComponent<Text>();
                discontext.text = "Disconnect";
                discontext.font = ResourceLoader.ArialFont;
                discontext.fontStyle = FontStyle.Bold;
                discontext.color = White;
                discontext.alignment = TextAnchor.MiddleCenter;
                discontext.resizeTextForBestFit = true;
                discontext.resizeTextMinSize = 0;
                RectTransform rectt = discontext.GetComponent<RectTransform>();
                rectt.localScale = new Vector3(0.9f, 0.9f, 0.9f);
                rectt.sizeDelta = new Vector2(0.13f, 0.023f);
                rectt.localPosition = new Vector3(0.066f, 0f, 0.1585f);
                rectt.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            }
        }

        public static void AddTitleAndFPSCounter()
        {
            if (Time.time - lastFPSTime >= 0.1f)
            {
                fps = Mathf.CeilToInt(1f / Time.smoothDeltaTime);
                lastFPSTime = Time.time;
            }

            title.text = $"Violet Free\nFPS: {fps} | Version: 7";
        }

        public static void AddModButtons(float offset, ButtonHandler.Button button)
        {
            ModButton = ButtonPool.GetButton();
            Rigidbody btnRigidbody = ModButton.GetComponent<Rigidbody>();
            if (btnRigidbody != null)
            {
                Destroy(btnRigidbody);
            }
            BoxCollider btnCollider = ModButton.GetComponent<BoxCollider>();
            if (btnCollider != null)
            {
                btnCollider.isTrigger = true;
            }

            ModButton.transform.SetParent(menuObj.transform, false);
            ModButton.transform.rotation = Quaternion.identity;
            ModButton.transform.localScale = new Vector3(0.026f, 1.14f, 0.08f);
            ModButton.transform.localPosition = new Vector3(0.57f, 0f, 0.31f - offset);
            BtnCollider btnColScript = ModButton.GetComponent<BtnCollider>() ?? ModButton.AddComponent<BtnCollider>();
            btnColScript.clickedButton = button;

            GameObject titleObj = TextPool.GetTextObject();
            titleObj.transform.SetParent(canvasObj.transform, false);
            titleObj.transform.localScale = new Vector3(0.95f, 0.95f, 1f);
            UnityEngine.UI.Text title = titleObj.GetComponent<UnityEngine.UI.Text>();
            title.text = button.buttonText;
            title.font = ResourceLoader.ArialFont;
            title.fontStyle = FontStyle.Bold;
            title.color = Variables.PageButtonsTextColor;
            RectTransform titleTransform = title.GetComponent<RectTransform>();
            titleTransform.localPosition = new Vector3(.0591f, 0, .12f - offset / 2.6f);
            titleTransform.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            titleTransform.sizeDelta = new Vector2(0.2f, 0.021f);

            Renderer btnRenderer = ModButton.GetComponent<Renderer>();
            if (btnRenderer != null)
            {
                btnRenderer.material.color = button.Enabled ? ButtonColorOn : ButtonColorOff;
            }
        }

        public static void AddPageButton(string button)
        {
            GameObject PageButtons = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Destroy(PageButtons.GetComponent<Rigidbody>());
            PageButtons.GetComponent<BoxCollider>().isTrigger = true;
            Outline(PageButtons, ColorLib.Grey);
            PageButtons.transform.parent = menuObj.transform;
            PageButtons.transform.rotation = Quaternion.identity;
            PageButtons.transform.localScale = new Vector3(0.02f, 0.36f, 0.09f);
            PageButtons.transform.localPosition = new Vector3(0.58f, button.Contains("<") ? 0.37f : -0.37f, -0.55f);
            PageButtons.GetComponent<Renderer>().material.color = ButtonColorOff;
            PageButtons.AddComponent<BtnCollider>().clickedButton = new ButtonHandler.Button(button, Category.Home, false, false, null, null);
            GameObject titleObj = new GameObject();
            titleObj.transform.parent = canvasObj.transform;
            titleObj.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
            UnityEngine.UI.Text title = titleObj.AddComponent<UnityEngine.UI.Text>();
            title.font = ResourceLoader.ArialFont;
            title.color = PageButtonsTextColor;
            title.fontSize = 4;
            title.fontStyle = FontStyle.Normal;
            title.alignment = TextAnchor.MiddleCenter;
            title.resizeTextForBestFit = true;
            title.resizeTextMinSize = 0;
            RectTransform titleTransform = title.GetComponent<RectTransform>();
            titleTransform.localPosition = Vector3.zero;
            titleTransform.sizeDelta = new Vector2(0.25f, 0.035f);
            title.text = button.Contains("<") ? "<" : ">";
            titleTransform.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            titleTransform.position = new Vector3(.06f, button.Contains("<") ? 0.11f : -0.11f, -0.208f);

        }

        public static void AddReturnButton()
        {
            GameObject backToStartButton = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Destroy(backToStartButton.GetComponent<Rigidbody>());
            Outline(backToStartButton, ColorLib.Grey);
            backToStartButton.GetComponent<BoxCollider>().isTrigger = true;
            backToStartButton.transform.parent = menuObj.transform;
            backToStartButton.transform.rotation = Quaternion.identity;
            backToStartButton.transform.localScale = new Vector3(0.02f, 0.29f, 0.09f);
            backToStartButton.transform.localPosition = new Vector3(0.58f, 0f, -0.55f);
            backToStartButton.AddComponent<BtnCollider>().clickedButton = new ButtonHandler.Button("ReturnButton", Category.Home, false, false, null, null);
            backToStartButton.GetComponent<Renderer>().material.color = ButtonColorOff;

            GameObject titleObj = new GameObject();
            titleObj.transform.parent = canvasObj.transform;
            titleObj.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
            UnityEngine.UI.Text title = titleObj.AddComponent<UnityEngine.UI.Text>();
            title.font = ResourceLoader.ArialFont;
            title.fontStyle = FontStyle.Bold;
            title.text = "Return";
            title.color = PageButtonsTextColor;
            title.fontSize = 3;
            title.alignment = TextAnchor.MiddleCenter;
            title.resizeTextForBestFit = true;
            title.resizeTextMinSize = 0;
            RectTransform titleTransform = title.GetComponent<RectTransform>();
            titleTransform.localPosition = Vector3.zero;
            titleTransform.sizeDelta = new Vector2(0.2f, 0.02f);
            titleTransform.localPosition = new Vector3(.06f, 0f, -0.208f);
            titleTransform.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            Outline(backToStartButton, ColorLib.LightGrey);
        }

        public static void AddButtonClicker(Transform parentTransform)
        {
            if (clickerObj == null)
            {
                clickerObj = new GameObject("buttonclicker");
                BoxCollider clickerCollider = clickerObj.AddComponent<BoxCollider>();
                if (clickerCollider != null)
                {
                    clickerCollider.isTrigger = true;
                }
                MeshFilter meshFilter = clickerObj.AddComponent<MeshFilter>();
                if (meshFilter != null)
                {
                    meshFilter.mesh = Resources.GetBuiltinResource<Mesh>("Sphere.fbx");
                }
                Renderer clickerRenderer = clickerObj.AddComponent<MeshRenderer>();
                if (clickerRenderer != null)
                {
                    clickerRenderer.material.color = DeepVioletTransparent;
                    clickerRenderer.material.shader = Shader.Find("GUI/Text Shader");
                }
                if (parentTransform != null)
                {
                    clickerObj.transform.parent = parentTransform;
                    clickerObj.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
                    clickerObj.transform.localPosition = new Vector3(0f, -0.1f, 0f);
                }
            }
        }
        
        private static void PositionMenuForHand()
        {
            if (rightHandedMenu)
            {
                menuObj.transform.position = playerInstance.rightControllerTransform.position;
                Vector3 rotation = playerInstance.rightControllerTransform.rotation.eulerAngles;
                rotation += new Vector3(0f, 0f, 180f);
                menuObj.transform.rotation = Quaternion.Euler(rotation);
                AddButtonClicker(playerInstance.leftControllerTransform);
            }
            else
            {
                AddButtonClicker(playerInstance.rightControllerTransform);
                menuObj.transform.position = playerInstance.leftControllerTransform.position;
                menuObj.transform.rotation = playerInstance.leftControllerTransform.rotation;
            }
        }

        private static void PositionMenuForKeyboard()
        {
            if (thirdPersonCamera != null)
            {
                thirdPersonCamera.transform.position = new Vector3(-65.8373f, 21.6568f, -80.9763f);
                thirdPersonCamera.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                menuObj.transform.SetParent(thirdPersonCamera.transform, true);

                Vector3 headPosition = thirdPersonCamera.transform.position;
                Quaternion headRotation = thirdPersonCamera.transform.rotation;
                float offsetDistance = 0.65f;
                Vector3 offsetPosition = headPosition + headRotation * Vector3.forward * offsetDistance;
                menuObj.transform.position = offsetPosition;

                Vector3 directionToHead = headPosition - menuObj.transform.position;
                menuObj.transform.rotation = Quaternion.LookRotation(directionToHead, Vector3.up);
                menuObj.transform.Rotate(Vector3.up, -90.0f);
                menuObj.transform.Rotate(Vector3.right, -90.0f);
            }
        }
    }
}