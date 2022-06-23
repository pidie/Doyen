using System;
using System.Collections.Generic;
using System.Linq;
using SpawnPoints;
using UnityEngine;
using UserInterface;

namespace Inventory
{
    public class PickUpRadius : MonoBehaviour
    {
        [SerializeField] private float detectionRadius;
        [SerializeField] private List<SpawnNode> nodesInRange = new ();

        private SphereCollider _detectionSphere;
        private SpawnNode _targetNode;

        public static Action itemPickUpRequested;
        public static Action<SpawnNode> nodeCollected;
        public static Action<SpawnNode> nodeRefreshed;

        private void Awake()
        {
            _detectionSphere = GetComponent<SphereCollider>();
            _detectionSphere.radius = detectionRadius;
            _targetNode = null;
            
            itemPickUpRequested += TryPickUpItem;
            nodeCollected += OnNodeCollected;
            nodeRefreshed += OnNodeRefreshed;
        }

        private void Update() => DetermineTargetCollectable();

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("SpawnNode"))
            {
                var node = other.GetComponent<SpawnNode>();
                if (node.hasBeenCollected) return;
                nodesInRange.Add(node);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("SpawnNode"))
            {
                var node = other.GetComponent<SpawnNode>();
                node.Collectable.ToggleOutlineOff();
                nodesInRange.Remove(node);
                if (nodesInRange.Count < 1)
                {
                    _targetNode = null;
                    CheckForHideMessageBox();
                }
            }
        }

        private void OnNodeCollected(SpawnNode node) => nodesInRange.Remove(node);

        private void OnNodeRefreshed(SpawnNode node) => nodesInRange.Add(node);

        public void TryPickUpItem()
        {
            if (_targetNode == null) return;
            
            var item = _targetNode.Collectable;

            if (item != null)
            {
                PlayerInventory.AddItem(item.Data);
                _targetNode.HandlePickUp();
                _targetNode = null;
                CheckForHideMessageBox();
            }
        }

        private void CheckForHideMessageBox()
        {
            if (nodesInRange.Count < 1)
                HUD.OnHideMessageBox();
        }

        private void ShowMessageBox(SpawnNode node)
        {
            _targetNode = node;
            _targetNode.Collectable.ToggleOutlineOn();
            HUD.OnDisplayMessageBox($"{_targetNode.Collectable.Data.name}\nPress {Globals.GetKeyBinding("Interact")} to pick up");
        }

        private void DetermineTargetCollectable()
        {
            if (nodesInRange.Count > 0)
            {
                foreach (var node in nodesInRange.Where(node => node != _targetNode))
                {
                    if (_targetNode == null)
                        ShowMessageBox(node);

                    var currentTargetDistance = Vector3.Distance(transform.position, _targetNode.transform.position);
                    var potentialTargetDistance = Vector3.Distance(transform.position, node.transform.position);

                    if (potentialTargetDistance < currentTargetDistance)
                    {
                        _targetNode.Collectable.ToggleOutlineOff();
                        ShowMessageBox(node);
                    }
                }
            }
        }
    }
}