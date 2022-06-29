using PlayerInput;
using UnityEngine;

namespace UserInterface.Settings
{
    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject settingsMenu;

        public void ShowSettingsMenu()
        {
            PlayerInputController.menuActive = true;
            settingsMenu.SetActive(true);
            mainMenu.SetActive(false);
        }

        public void ReturnToMainMenu()
        {
            mainMenu.SetActive(true);
            settingsMenu.SetActive(false);
        }
    }
}
