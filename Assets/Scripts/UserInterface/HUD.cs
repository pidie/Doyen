using System;
using TMPro;
using UnityEngine;

namespace UserInterface
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private GameObject messageBoxPanel;
        [SerializeField] private TMP_Text messageBox;
        
        private static GameObject _inventoryPanel;

        public static Action<string> OnDisplayMessageBox;
        public static Action OnHideMessageBox;

        private void Awake()
        {
            _inventoryPanel = GameObject.Find("InventoryPanel");
            _inventoryPanel.SetActive(false);
            
            messageBoxPanel.SetActive(false);
        }

        private void OnEnable()
        {
            OnDisplayMessageBox += DisplayMessageBox;
            OnHideMessageBox += HideMessageBox;
        }

        private void OnDisable()
        {
            OnDisplayMessageBox -= DisplayMessageBox;
            OnHideMessageBox -= HideMessageBox;
        }

        public static void ToggleInventoryPanel() => _inventoryPanel.SetActive(!_inventoryPanel.activeSelf);

        public void DisplayMessageBox(string message = "")
        {
            messageBoxPanel.SetActive(true);
            messageBox.text = message;
        }

        [ContextMenu("Hide Message Box")]
        public void HideMessageBox() => messageBoxPanel.SetActive(false);
    }
}