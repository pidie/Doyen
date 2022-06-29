using System;
using UnityEngine;

namespace UserInterface.Settings
{
    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject settingsMenu;

        public static bool atMainMenu = true;
        public static Action onBackStep;

        private void OnEnable() => onBackStep += BackStep;

        private void OnDisable() => onBackStep -= BackStep;

        public void ShowSettingsMenu()
        {
            atMainMenu = false;
            mainMenu.SetActive(false);
            settingsMenu.SetActive(true);
        }

        public void ReturnToMainMenu()
        {
            atMainMenu = true;
            mainMenu.SetActive(true);
            settingsMenu.SetActive(false);
        }

        private void BackStep()
        {
            ReturnToMainMenu();
        }
    }
}
