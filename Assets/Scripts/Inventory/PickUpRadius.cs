using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UserInterface;

namespace Inventory
{
    public class PickUpRadius : MonoBehaviour
    {
        [SerializeField] private float detectionRadius;
        [SerializeField] private List<Collectable> collectablesInRange = new ();

        private SphereCollider _detectionSphere;
        private Collectable _targetCollectable;

        public static Action itemPickUpRequested;

        private void Awake()
        {
            _detectionSphere = GetComponent<SphereCollider>();
            _detectionSphere.radius = detectionRadius;
            _targetCollectable = null;
            
            itemPickUpRequested += TryPickUpItem;
        }

        private void Update() => DetermineTargetCollectable();

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable"))
            {
                var collectable = other.GetComponent<Collectable>();
                collectablesInRange.Add(collectable);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Collectable"))
            {
                var collectable = other.GetComponent<Collectable>();
                collectable.ToggleOutlineOff();
                collectablesInRange.Remove(collectable);
                if (collectablesInRange.Count < 1)
                {
                    _targetCollectable = null;
                    CheckForHideMessageBox();
                }
            }
        }
        
        public void TryPickUpItem()
        {
            var item = _targetCollectable;

            if (item != null)
            {
                PlayerInventory.AddItem(item.Data);
                Destroy(item.gameObject);
                collectablesInRange.Remove(item);
                _targetCollectable = null;
                CheckForHideMessageBox();
            }
        }

        private void CheckForHideMessageBox()
        {
            if (collectablesInRange.Count < 1)
                HUD.OnHideMessageBox();
        }

        private void ShowMessageBox(Collectable collectable)
        {
            _targetCollectable = collectable;
            _targetCollectable.ToggleOutlineOn();
            HUD.OnDisplayMessageBox($"{collectable.Data.name}\nPress {Globals.GetKeyBinding("Interact")} to pick up");
        }

        private void DetermineTargetCollectable()
        {
            if (collectablesInRange.Count > 0)
            {
                foreach (var collectable in collectablesInRange.Where(collectable => collectable != _targetCollectable))
                {
                    if (_targetCollectable == null)
                        ShowMessageBox(collectable);

                    var currentTargetDistance = Vector3.Distance(transform.position, _targetCollectable.transform.position);
                    var potentialTargetDistance = Vector3.Distance(transform.position, collectable.transform.position);

                    if (potentialTargetDistance < currentTargetDistance)
                    {
                        _targetCollectable.ToggleOutlineOff();
                        ShowMessageBox(collectable);
                    }
                }
            }
        }
    }
}