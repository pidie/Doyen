using Inventory;
using UnityEngine;
using UserInterface;

namespace Spawners
{
	public class CurrencyNode : SpawnNode
	{
		private void Awake() => IsAutoCollected = true;

		private void Update() => transform.Rotate(Vector3.up, -200f * Time.deltaTime);

		public override void HandlePickUp()
		{
			PickUpRadius.onNodeCollected(this);
			InventoryCurrencyUpdater.onGainMoney(Collectable.Data.worth);
			Destroy(gameObject);
		}
	}
}