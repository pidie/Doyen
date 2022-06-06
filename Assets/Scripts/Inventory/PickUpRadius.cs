using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventory
{
    public class PickUpRadius : MonoBehaviour
    {
        [SerializeField] private List<Collectable> collectablesInRange;

        public static int NumberOfCollectablesInRange;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                TryPickUpItem();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var col = other.GetComponent<Collectable>();
            
            if (col == null) return;
            
            if (!col.IsAutocollected)
                collectablesInRange.Add(col);

            NumberOfCollectablesInRange++;
        }

        private void OnTriggerExit(Collider other)
        {
            var col = other.GetComponent<Collectable>();
            
            if (col == null) return;
            
            if (!col.IsAutocollected)
                collectablesInRange.Remove(col);

            NumberOfCollectablesInRange--;
        }

        private void TryPickUpItem()
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