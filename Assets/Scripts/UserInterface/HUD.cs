using System;
using TMPro;
using UnityEngine;

namespace UserInterface
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private GameObject messageBoxPanel;
        [SerializeField] private TMP_Text messageBox;
        [SerializeField] private GameObject inventoryPanel;
        [SerializeField] private GameObject mainMenu;

        public static Action<string> OnDisplayMessageBox;
        public static Action OnHideMessageBox;
        public static Action OnToggleInventoryPanel;
        public static Action OnToggleMainMenu;

        private void OnEnable()
        {
            OnDisplayMessageBox += DisplayMessageBox;
            OnHideMessageBox += HideMessageBox;
            OnToggleInventoryPanel += ToggleInventoryPanel;
            OnToggleMainMenu += ToggleMainMenu;
        }

        private void OnDisable()
        {
            OnDisplayMessageBox -= DisplayMessageBox;
            OnHideMessageBox -= HideMessageBox;
            OnToggleInventoryPanel -= ToggleInventoryPanel;
            OnToggleMainMenu -= ToggleMainMenu;
        }

        public void ToggleInventoryPanel()
        {
            Audio.AudioManager.onMuffleMusic(!inventoryPanel.activeSelf);
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
            TooltipManager.OnDestroyTooltip();
        }

        public void ToggleMainMenu()
        {
            Audio.AudioManager.onMuffleMusic(!mainMenu.activeSelf);
            mainMenu.SetActive(!mainMenu.activeSelf);
        }

        public void DisplayMessageBox(string message = "")
        {
            messageBoxPanel.SetActive(true);
            messageBox.text = message;
        }

        [ContextMenu("Hide Message Box")]
        public void HideMessageBox() => messageBoxPanel.SetActive(false);
    }
}