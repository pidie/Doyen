using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
	public class PlayerInventory : MonoBehaviour
	{
		[SerializeField] private List<CollectableData> window; // useful until Items can be made non-static
		public static List<CollectableData> Items = new ();

		private void Update()
		{
			window = Items;
		}

		public static void AddItem(CollectableData collectableData)
		{
			Items.Add(collectableData);
		}
	}
}