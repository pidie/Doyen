using UnityEngine;

namespace Spawners
{
	public class CurrencyNode : SpawnNode
	{
		private void Awake() => IsAutoCollected = true;

		private void Update() => transform.Rotate(Vector3.up, -200f * Time.deltaTime);

		public override void HandlePickUp()
		{
			Destroy(gameObject);
		}
	}
}