using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
	public class PlayerInventory : MonoBehaviour
	{
		public static List<CollectableData> Items = new ();

		public static void AddItem(CollectableData collectableData)
		{
			Items.Add(collectableData);
		}
	}
}