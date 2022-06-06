using TMPro;
using UnityEngine;

namespace UserInterface
{
    public class HUD : MonoBehaviour
    {
        private static GameObject _messageBoxPanel;
        private static TMP_Text _messageBox;

        private void Awake()
        {
            _messageBoxPanel = GameObject.Find("MessageBoxPanel");
            _messageBox = _messageBoxPanel.GetComponentInChildren<TMP_Text>();
            _messageBoxPanel.SetActive(false);
        }

        public static void DisplayMessageBox(string message = "")
        {
            _messageBoxPanel.SetActive(true);
            _messageBox.text = message;
            PlayerBehavior.CanInteract = true;
        }

        public static void HideMessageBox()
        {
            _messageBoxPanel.SetActive(false);
            PlayerBehavior.CanInteract = false;
        }
    }
}
