using UnityEngine;
using UserInterface;

namespace PlayerInput
{
	public class PlayerInputController : MonoBehaviour
	{
		private void Update()
		{
			if (Input.anyKey)
			{
				if (Input.GetKeyDown(KeyCode.I))
					HUD.ToggleInventoryPanel();
			}
		}
	}
}
