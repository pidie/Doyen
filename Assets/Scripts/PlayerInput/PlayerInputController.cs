using Inventory;
using UnityEngine;
using UserInterface;
using UserInterface.Settings;

namespace PlayerInput
{
	public class PlayerInputController : MonoBehaviour
	{
		public static int menusActive;
		private void Update()
		{
			if (Input.anyKey)
			{
				if (Input.GetKeyDown(Globals.GetKeyBinding("Inventory")))
					HUD.onToggleInventoryPanel.Invoke();
				if (Input.GetKeyDown(Globals.GetKeyBinding("Interact")))
					PickUpRadius.itemPickUpRequested.Invoke();
				if (Input.GetKeyDown(Globals.GetKeyBinding("MainMenu")))
				{
					if (SettingsMenu.atMainMenu)
						HUD.onToggleMainMenu.Invoke();
					else
						SettingsMenu.onBackStep?.Invoke();
				}
			}
		}
	}
}
