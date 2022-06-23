using Inventory;
using UnityEngine;

namespace SpawnPoints
{
	public abstract class SpawnNode : MonoBehaviour
	{
		public GameObject item;
		protected GameObject instance;
		public bool hasBeenCollected;

		public Collectable Collectable => instance.GetComponent<Collectable>();

		public void Destroy() => DestroyImmediate(gameObject);

		public void SpawnItem()
		{
			instance = Instantiate(item, transform.position, Quaternion.identity, transform);
			instance.transform.localScale *= 4;
		}
		
		public virtual void HandlePickUp() { }
	}
}