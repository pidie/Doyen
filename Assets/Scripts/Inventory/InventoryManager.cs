using UnityEngine;
using UnityEngine.Events;

namespace Inventory
{
	public class InventoryManager : MonoBehaviour
	{
		public static InventoryManager Instance;
		public UnityEvent<Collectable> onCollectablePickedUp;
		public int Money { get; set; }
		public PlayerInventory inventory;

		private void Awake()
		{
			if (Instance == null)
				Instance = this;
			else
				Destroy(this);
		}
	}
}