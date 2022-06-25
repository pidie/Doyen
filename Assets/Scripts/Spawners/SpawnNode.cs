using Inventory;
using UnityEngine;

namespace Spawners
{
	public abstract class SpawnNode : MonoBehaviour
	{
		[SerializeField] protected float yOffset;
		
		protected GameObject instance;

		public GameObject item;
		public bool hasBeenCollected;
		public bool IsAutoCollected { get; protected set; }

		public Collectable Collectable => instance.GetComponent<Collectable>();

		public void DestroyNode() => DestroyImmediate(gameObject);

		public void SpawnItem()
		{
			var position = new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z);
			instance = Instantiate(item, position, Quaternion.identity, transform);
			instance.transform.localScale *= 4;
		}

		public virtual void HandlePickUp() => Audio.AudioManager.onPlaySound(Collectable.Data.onCollectSound, true);
	}
}