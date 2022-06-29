using Inventory;
using UnityEngine;
using UserInterface;

namespace PlayerInput
{
	public class PlayerInputController : MonoBehaviour
	{
		public static bool menuActive;
		private void Update()
		{
			if (Input.anyKey)
			{
				if (Input.GetKeyDown(Globals.GetKeyBinding("Inventory")))
					HUD.OnToggleInventoryPanel();
				if (Input.GetKeyDown(Globals.GetKeyBinding("Interact")))
					PickUpRadius.itemPickUpRequested.Invoke();
				if (Input.GetKeyDown(Globals.GetKeyBinding("MainMenu")))
				{
					menuActive = !menuActive;
					HUD.OnToggleMainMenu();
				}
			}
		}
	}
}
