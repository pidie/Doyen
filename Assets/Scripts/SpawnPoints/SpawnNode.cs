using UnityEngine;

namespace SpawnPoints
{
	public abstract class SpawnNode : MonoBehaviour
	{
		public GameObject item;

		public void Destroy() => DestroyImmediate(gameObject);

		public void SpawnItem()
		{
			var go = Instantiate(item, transform.position, Quaternion.identity, transform);
			go.transform.localScale *= 4;
		}
	}
}