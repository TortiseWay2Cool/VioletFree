using VioletFree.Mods;
using VioletFree.Utilities;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static VioletFree    .Utilities.Variables;
using static VioletFree.Menu.Optimizations;
using static VioletFree.Mods.Room.Room;
using System.IO;
using static VioletFree.Menu.Main;
using Valve.VR;
using VioletFree.Menu;


namespace VioletFree.Menu
{
    public class ButtonHandler
    {
        public static void Toggle(Button button)
        {
            switch (button.buttonText)
            {
                case "<":
                    NavigatePage(false);
                    break;
                case ">":
                    NavigatePage(true);
                    break;
                case "DisconnectButton":
                    PhotonNetwork.Disconnect();
                    break;
                case "ReturnButton":
                    ReturnToMainPage();
                    break;
                default:
                    ToggleButton(button);
                    break;
            }
        }

        public static void ChangeButtonText(string CurrentButtonText, string NewText)
        {
            foreach (var button in ModButtons.buttons)
            {
                try
                {
                    if (button.buttonText.Contains(CurrentButtonText))
                    {
                        button.SetText(NewText);
                        RefreshMenu();
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Error while changing text of button '{button.buttonText}': {ex.Message}\nStack Trace: {ex.StackTrace}");
                }
            }
        }

        private static void NavigatePage(bool forward)
        {
            int totalPages = GetTotalPages(currentPage);
            int lastPage = totalPages - 1;

            currentCategoryPage += forward ? 1 : -1;

            if (currentCategoryPage < 0)
            {
                currentCategoryPage = lastPage;
            }
            else if (currentCategoryPage > lastPage)
            {
                currentCategoryPage = 0; 
            }

            Optimizations.RefreshMenu();
        }

        private static void ReturnToMainPage()
        {
            currentPage = Category.Home;
            currentCategoryPage = 0;

            RefreshMenu();
        }

        private static int GetTotalPages(Category page)
        {
            return (GetButtonInfoByPage(page).Count + ButtonsPerPage - 1) / ButtonsPerPage;
        }

        public static void ChangePage(Category page)
        {
            currentCategoryPage = 0;
            currentPage = page;

            RefreshMenu();
        }

        public static void ToggleButton(Button button)
        {
            try
            {
                if (!button.isToggle)
                {
                    button.onEnable?.Invoke();

                    if (button.Page == currentPage)
                    {
                        NotificationLib.SendNotification($"<color=green>Enabled</color> : {button.buttonText}");
                    }
                    else
                    {
                        NotificationLib.SendNotification($"<color=green>Entered Category</color> : {button.buttonText}");
                    }
                }
                else
                {
                    button.Enabled = !button.Enabled;

                    if (button.Enabled)
                    {
                        button.onEnable?.Invoke();
                        NotificationLib.SendNotification($"<color=green>Enabled</color> : {button.buttonText}");
                    }
                    else
                    {
                        button.onDisable?.Invoke();
                        NotificationLib.SendNotification($"<color=red>Disabled</color> : {button.buttonText}");
                    }
                }

                RefreshMenu();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error while toggling button '{button.buttonText}': {ex.Message}\nStack Trace: {ex.StackTrace}");
            }
        }

        public static string newtooltip;
        public class Button
        {
            public string buttonText { get; set; }
            public bool isToggle { get; set; }
            public bool NeedsMaster { get; set; }
            public bool Enabled { get; set; }
            public Action onEnable { get; set; }
            public Action onDisable { get; set; }
            public Category Page { get; set; }
            public static string Tip { get; set; }

            public Button(string label, Category page, bool isToggle, bool isActive, Action onClick, Action onDisable = null, bool doesNeedMaster = false )
            {
                buttonText = label;
                this.isToggle = isToggle;
                Enabled = isActive;
                onEnable = onClick;
                Page = page;
                this.onDisable = onDisable;
                NeedsMaster = doesNeedMaster;
                

            }
            public void SetText(string newText)
            {
                buttonText = newText;
            }
           
        }
        
        public class BtnCollider : MonoBehaviour
        {
            public Button clickedButton;
            public static int clickCooldown = 0;

            public void OnTriggerEnter(Collider collider)
            {
                if (Time.frameCount >= clickCooldown + 25 && collider.gameObject.name == "buttonclicker")
                {
                    transform.localScale = new Vector3(transform.localScale.x / 3, transform.localScale.y, transform.localScale.z);
                    clickCooldown = Time.frameCount;

                    taggerInstance.StartVibration(rightHandedMenu, taggerInstance.tagHapticStrength / 2, taggerInstance.tagHapticDuration / 2);
                    taggerInstance.offlineVRRig.PlayHandTapLocal(8, false, 0.4f);
                    GetComponent<BoxCollider>().enabled = true;
                    Toggle(clickedButton);
                }
            }
        }

        public static List<Button> GetButtonInfoByPage(Category page)
        {
            return ModButtons.buttons.Where(button => button.Page == page).ToList();
        }

        public static void DisableAllMods()
        {
            foreach (ButtonHandler.Button button in ModButtons.buttons)
            {
                if (button.Enabled)
                {
                    button.Enabled = false;
                    button.onDisable?.Invoke();
                }
            }

            RefreshMenu();
        }
    }
}
