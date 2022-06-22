using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpawnPoints
{
	public enum SpawnPointDensity
	{
		Sparse,
		Scattered,
		Typical,
		Plentiful,
		Packed
	}
	[ExecuteInEditMode]
	public class IngredientSpawnPoint : MonoBehaviour
	{
		[SerializeField] private GameObject ingredient;
		[SerializeField] private GameObject node;
		[SerializeField] private SpawnPointDensity density = SpawnPointDensity.Typical;
		[SerializeField] private float distanceBetweenNodes = 1f;
		
		private int _spawnerDiameter;
		private Vector3[] _nodePositions;
		private float _densityMultiplier;

		private void OnEnable()
		{
			_spawnerDiameter = (int) transform.localScale.x;
			SetDensity();
			_nodePositions = new Vector3[(int) Math.Round(_spawnerDiameter * _densityMultiplier)];
			gameObject.name = $"{Globals.TitleCase(ingredient.name)} Spawner";
			PlaceNodes();
		}

		private void OnDisable()
		{
			foreach (var child in GetComponentsInChildren<IngredientNode>())
			{
				#if UNITY_EDITOR
				child.Destroy();
				#else
				Destroy(child.gameObject);
				#endif
			}
		}

		private void SetDensity()
		{
			_densityMultiplier = density switch
			{
				SpawnPointDensity.Sparse => 0.25f,
				SpawnPointDensity.Scattered => 0.5f,
				SpawnPointDensity.Typical => 1f,
				SpawnPointDensity.Plentiful => 1.5f,
				SpawnPointDensity.Packed => 2f,
				_ => default
			};
		}
		
		private void PlaceNodes()
		{
			var resetNodePlacement = false;
			for (var i = 0; i < _nodePositions.Length; i++)
			{
				if (resetNodePlacement)
				{
					i = 0;
					Array.Clear(_nodePositions, 0, _nodePositions.Length);
					resetNodePlacement = false;
				}
				
				var nodeLocation = Random.insideUnitSphere * _spawnerDiameter * 0.5f;
				nodeLocation += transform.position;
				if (!Physics.Raycast(nodeLocation + new Vector3(0, -1, 0), Vector3.up, out var hit, _spawnerDiameter + 1))
					if (!Physics.Raycast(nodeLocation + new Vector3(0, 1, 0), Vector3.down, out hit, _spawnerDiameter + 1))
					{
						i--;
						continue;
					}

				nodeLocation = new Vector3(nodeLocation.x, hit.collider.transform.position.y, nodeLocation.z);

				for (var j = i - 1; j >= 0; j--)
				{
					if (Vector3.Distance(nodeLocation, _nodePositions[j]) < distanceBetweenNodes)
					{
						resetNodePlacement = true;
						break;
					}
				}

				if (!resetNodePlacement)
					_nodePositions[i] = nodeLocation;
			}

			var count = 1;
			foreach (var position in _nodePositions)
			{
				var newNode = Instantiate(node, position, Quaternion.identity, transform);
				newNode.GetComponent<MeshRenderer>().enabled = false;
				var n = newNode.AddComponent<IngredientNode>();
				n.ingredient = ingredient;
				n.SpawnIngredient();
				var scaling = node.transform.localScale.x / _spawnerDiameter;
				newNode.transform.localScale = new Vector3(scaling, scaling,scaling);
				newNode.name = $"{Globals.TitleCase(ingredient.name)} Node {count}";
				count++;
			}
		}

		#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			var radius = transform.localScale.x * 0.5f;
			Gizmos.color = Color.cyan;
			Gizmos.DrawWireSphere(transform.position, radius);
		}

		private void OnDrawGizmosSelected()
		{			
			var radius = transform.localScale.x * 0.5f;
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(transform.position, radius);
		}
		#endif
	}

	public class IngredientNode : MonoBehaviour
	{
		public GameObject ingredient;
		public bool hasBeenCollected;

		public void Destroy() => DestroyImmediate(gameObject);

		public void SpawnIngredient()
		{
			var t = Instantiate(ingredient, transform.position, Quaternion.identity, transform);
			t.transform.localScale *= 4;
		}
	}
}