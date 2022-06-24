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

        public static Action<string> OnDisplayMessageBox;
        public static Action OnHideMessageBox;
        public static Action OnToggleInventoryPanel;

        private void OnEnable()
        {
            OnDisplayMessageBox += DisplayMessageBox;
            OnHideMessageBox += HideMessageBox;
            OnToggleInventoryPanel += ToggleInventoryPanel;
        }

        private void OnDisable()
        {
            OnDisplayMessageBox -= DisplayMessageBox;
            OnHideMessageBox -= HideMessageBox;
            OnToggleInventoryPanel -= ToggleInventoryPanel;
        }

        public void ToggleInventoryPanel()
        {
            Audio.AudioManager.onMuffleMusic(!inventoryPanel.activeSelf);
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
            TooltipManager.OnDestroyTooltip();
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