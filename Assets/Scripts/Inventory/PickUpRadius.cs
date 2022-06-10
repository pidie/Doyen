using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UserInterface;

namespace Inventory
{
    public class PickUpRadius : MonoBehaviour
    {
        [SerializeField] private List<Collectable> collectablesInRange = new ();

        public static int NumberOfCollectablesInRange { get; private set; }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                TryPickUpItem();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable"))
                collectablesInRange.Add(other.GetComponent<Collectable>());
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Collectable"))
                collectablesInRange.Remove(other.GetComponent<Collectable>());
        }

        private void UpdateCollectablesInRangeCount() => NumberOfCollectablesInRange = collectablesInRange.Count;

        private void TryPickUpItem()
        {
            var item = collectablesInRange.LastOrDefault();

            if (item != null)
            {
                PlayerInventory.AddItem(item.Data);
                Destroy(item.gameObject);
                collectablesInRange.Remove(item);
                UpdateCollectablesInRangeCount();
                
                if (NumberOfCollectablesInRange < 1)
                    HUD.HideMessageBox();
            }
        }
    }
}