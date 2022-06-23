using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
	public class PlayerInventory : MonoBehaviour
	{
		[SerializeField] private List<CollectableData> items = new ();

		public int ItemCount => items.Count;
		
		public static Action<CollectableData> OnAddItem;

		private void Awake() => OnAddItem += AddItem;

		public void AddItem(CollectableData collectableData) => items.Add(collectableData);

		public CollectableData GetItem(int index) => items[index];
	}
}