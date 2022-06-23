using System;
using Inventory;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawners
{
	public enum SpawnPointDensity
	{
		Sparse,
		Scattered,
		Average,
		Plentiful,
		Packed
	}
	[ExecuteInEditMode]
	public class Spawner : MonoBehaviour
	{
		[SerializeField] private GameObject itemToSpawn;
		[SerializeField] private GameObject node;
		[SerializeField] private SpawnPointDensity density = SpawnPointDensity.Average;
		[Tooltip("Adjust this value when working with items that use large models.")]
		[SerializeField] private float distanceBetweenNodes = 1f;
		
		private int _spawnerDiameter;
		private Vector3[] _nodePositions;
		private float _densityMultiplier;
		
		private void Awake() => enabled = true;

		private void OnEnable()
		{
			EditorApplication.playModeStateChanged += HandleOnPlaymodeStateChanged;
			Spawn();
		}

		private void OnDisable()
		{
			EditorApplication.playModeStateChanged -= HandleOnPlaymodeStateChanged;
			Despawn();
		}

		private void Spawn()
		{
			_spawnerDiameter = (int) transform.localScale.x;
			SetDensity();
			_nodePositions = new Vector3[(int) Math.Round(_spawnerDiameter * _densityMultiplier)];
			gameObject.name = $"{Globals.TitleCase(itemToSpawn.name)} Spawner";
			PlaceNodes();
		}

		private void Despawn()
		{
			foreach (var child in GetComponentsInChildren<SpawnNode>())
				child.DestroyNode();
		}

		private void HandleOnPlaymodeStateChanged(PlayModeStateChange state)
		{
			if (state is PlayModeStateChange.ExitingPlayMode or PlayModeStateChange.EnteredEditMode)
				enabled = false;
		}

		private void SetDensity()
		{
			_densityMultiplier = density switch
			{
				SpawnPointDensity.Sparse => 0.25f,
				SpawnPointDensity.Scattered => 0.5f,
				SpawnPointDensity.Average => 1f,
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
				// todo : figure out why the inclusion of the layer mask causes the editor to crash
				// if (!Physics.Raycast(nodeLocation + new Vector3(0, -1, 0), Vector3.up, out var hit, _spawnerDiameter + 1, LayerMask.NameToLayer("Ground")))
				// 	if (!Physics.Raycast(nodeLocation + new Vector3(0, 1, 0), Vector3.down, out hit, _spawnerDiameter + 1, LayerMask.NameToLayer("Ground")))
				// 	{
				// 		i--;
				// 		continue;
				// 	}
				if (!Physics.Raycast(nodeLocation + new Vector3(0, -1, 0), Vector3.up, out var hit, _spawnerDiameter + 1))
					if (!Physics.Raycast(nodeLocation + new Vector3(0, 1, 0), Vector3.down, out hit, _spawnerDiameter + 1))
					{
						i--;
						continue;
					}

				nodeLocation = new Vector3(nodeLocation.x, hit.point.y, nodeLocation.z);

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
				var n = newNode.GetComponent(typeof(SpawnNode)) as SpawnNode;
				if (n == null) throw new NullReferenceException($"No derivative of SpawnNode was found on {gameObject.name}! Was a node type properly passed in?");
				n.item = itemToSpawn;
				n.SpawnItem();
				var scaling = node.transform.localScale.x / _spawnerDiameter;
				newNode.transform.localScale = new Vector3(scaling, scaling,scaling);
				newNode.name = $"{Globals.TitleCase(itemToSpawn.name)} Node {count}";
				count++;
			}
		}

		#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			var radius = transform.localScale.x * 0.5f;
			var iconPath = AssetDatabase.GetAssetPath(itemToSpawn.GetComponent<Collectable>().Data.icon);

			Gizmos.color = Color.cyan;
			Gizmos.DrawWireSphere(transform.position, radius);
			Gizmos.DrawIcon(transform.position, iconPath);
		}

		private void OnDrawGizmosSelected()
		{			
			var radius = transform.localScale.x * 0.5f;
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(transform.position, radius);
		}
		#endif
	}
}