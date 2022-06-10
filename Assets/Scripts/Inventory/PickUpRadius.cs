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

        public static Action itemPickUpRequested;

        private void Awake()
        {
            _detectionSphere = GetComponent<SphereCollider>();
            _detectionSphere.radius = detectionRadius;
            
            itemPickUpRequested += TryPickUpItem;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable"))
            {
                collectablesInRange.Add(other.GetComponent<Collectable>());
                HUD.DisplayMessageBox($"{collectablesInRange.LastOrDefault().Data.name}\nPress {Globals.GetKeyBinding("Interact")} to pick up");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Collectable"))
            {
                collectablesInRange.Remove(other.GetComponent<Collectable>());
                if (collectablesInRange.Count < 1)
                    HUD.HideMessageBox();
            }
        }
        
        public void TryPickUpItem()
        {
            var item = collectablesInRange.LastOrDefault();

            if (item != null)
            {
                PlayerInventory.AddItem(item.Data);
                Destroy(item.gameObject);
                collectablesInRange.Remove(item);
            }
        }
    }
}


