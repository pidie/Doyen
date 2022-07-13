using System;
using PlayerInput;
using Serialization;
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

        public static Action<string> onDisplayMessageBox;
        public static Action onHideMessageBox;
        public static Action onToggleInventoryPanel;
        public static Action onToggleMainMenu;

        private void OnEnable()
        {
            onDisplayMessageBox += DisplayMessageBox;
            onHideMessageBox += HideMessageBox;
            onToggleInventoryPanel += ToggleInventoryPanel;
            onToggleMainMenu += ToggleMainMenu;
            SceneLoader.onAssignReferences += AssignReferences;
            SceneLoader.onSetUIElements += SetUIElements;
        }

        private void OnDisable()
        {
            onDisplayMessageBox -= DisplayMessageBox;
            onHideMessageBox -= HideMessageBox;
            onToggleInventoryPanel -= ToggleInventoryPanel;
            onToggleMainMenu -= ToggleMainMenu;
            SceneLoader.onAssignReferences -= AssignReferences;
            SceneLoader.onSetUIElements -= SetUIElements;
        }

        private void AssignReferences()
        {
            messageBoxPanel = GameObject.Find("MessageBoxPanel");
            messageBox = messageBoxPanel.GetComponentInChildren<TMP_Text>();
            inventoryPanel = GameObject.Find("InventoryPanel");
            mainMenu = GameObject.Find("MainMenu");
        }

        private void SetUIElements()
        {
            messageBoxPanel.SetActive(false);
            inventoryPanel.SetActive(false);
            mainMenu.SetActive(false);
        }

        public void ToggleInventoryPanel()
        {
            Audio.AudioManager.onMuffleMusic(!inventoryPanel.activeSelf);
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
            TooltipManager.OnDestroyTooltip();

            if (inventoryPanel.activeSelf)
                PlayerInputController.menusActive++;
            else
                PlayerInputController.menusActive--;
        }

        public void ToggleMainMenu()
        {
            Audio.AudioManager.onMuffleMusic(!mainMenu.activeSelf);
            mainMenu.SetActive(!mainMenu.activeSelf);
            
            if (inventoryPanel.activeSelf)
                ToggleInventoryPanel();

            if (mainMenu.activeSelf)
                PlayerInputController.menusActive++;
            else
                PlayerInputController.menusActive--;
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