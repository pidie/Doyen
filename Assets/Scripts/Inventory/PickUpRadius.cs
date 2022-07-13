using System;
using System.Collections.Generic;
using System.Linq;
using Spawners;
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
        public static Action<SpawnNode> onNodeCollected;
        public static Action<SpawnNode> onNodeRefreshed;

        private void Awake()
        {
            _detectionSphere = GetComponent<SphereCollider>();
            _detectionSphere.radius = detectionRadius;
            _targetNode = null;
            
            itemPickUpRequested += TryPickUpItem;
            onNodeCollected += NodeCollected;
            onNodeRefreshed += NodeRefreshed;
        }

        private void Update() => DetermineTargetCollectable();

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("SpawnNode"))
                AddNode(other.GetComponent<SpawnNode>());
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

        private void AddNode(SpawnNode node)
        {
            if (node.hasBeenCollected) return;
            if (node.IsAutoCollected)
            {
                _targetNode = node;
                TryPickUpItem();
            }
            else
                nodesInRange.Add(node);
        }

        private void NodeCollected(SpawnNode node) => nodesInRange.Remove(node);

        private void NodeRefreshed(SpawnNode node)
        {
            // SphereCollider radius is measured in local space, not world space - this is temporary while the player is scaled up to match the environment
            var radiusScaled = detectionRadius * 2.5f;
            if (Vector3.Distance(transform.position, node.transform.position) <= radiusScaled)
                AddNode(node);
        }

        public void TryPickUpItem()
        {
            if (_targetNode == null) return;
            
            var item = _targetNode.Collectable;

            if (item != null)
            {
                // currency shouldn't be added to the player's inventory
                if (_targetNode.GetType() != typeof(CurrencyNode))
                    InventoryManager.Instance.inventory.AddItem(item.Data);
                
                _targetNode.HandlePickUp();
                _targetNode = null;
                CheckForHideMessageBox();
            }
        }

        // todo : this should be handled by the UI
        private void CheckForHideMessageBox()
        {
            if (nodesInRange.Count < 1)
                HUD.onHideMessageBox();
        }

        // todo : this should be handled by the UI
        private void ShowMessageBox(SpawnNode node)
        {
            _targetNode = node;
            _targetNode.Collectable.ToggleOutlineOn();
            HUD.onDisplayMessageBox($"{_targetNode.Collectable.Data.name}\nPress {Globals.GetKeyBinding("Interact")} to pick up");
        }
        
        private void DetermineTargetCollectable()
        {
            if (nodesInRange.Count > 0)
            {
                foreach (var node in nodesInRange.Where(node => node != _targetNode))
                {
                    if (_targetNode == null)
                        ShowMessageBox(node);

                    // calculate the distance between the current target node and the node in our loop
                    var currentTargetDistance = Vector3.Distance(transform.position, _targetNode.transform.position);
                    var potentialTargetDistance = Vector3.Distance(transform.position, node.transform.position);

                    // if the new node is closer, set that node to be the new target
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