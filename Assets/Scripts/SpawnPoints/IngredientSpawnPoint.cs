using Inventory;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpawnPoints
{
	public class IngredientSpawnPoint : MonoBehaviour
	{
		[SerializeField] private Ingredient ingredient;
		[SerializeField] private GameObject node;
		
		private int _quantity;
		private Vector3[] _plantNodes;

		private void Awake()
		{
			_quantity = (int) transform.localScale.x;
			_plantNodes = new Vector3[_quantity];
		}

		private void Start() => PlantIngredients();

		private void PlantIngredients()
		{
			for (var i = 0; i < _quantity; i++)
			{
				var location = Random.insideUnitSphere * (_quantity * 0.5f);
				location += transform.position;
				if (!Physics.Raycast(location, Vector3.up, out var hit, _quantity))
					Physics.Raycast(location, Vector3.down, out hit, _quantity);

				location = new Vector3(location.x, hit.collider.transform.position.y, location.z);
				_plantNodes[i] = location;

				// foreach (var n in _plantNodes)
				// 	if (Vector3.Distance(n, _plantNodes[i]) < 1)
				// 		i--;
			}

			foreach (var location in _plantNodes)
			{
				var newNode =Instantiate(node, location, Quaternion.identity, transform);
				var scaling = node.transform.localScale.x / _quantity;
				newNode.transform.localScale = new Vector3(scaling, scaling,scaling);
			}
		}
	}

	public class IngredientNode : MonoBehaviour
	{
		public Ingredient ingredient;
		public bool hasBeenCollected;
	}
}