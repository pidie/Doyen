using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class AutoPickUpRadius : MonoBehaviour
    {
        [SerializeField] private List<GameObject> autocollectablesInRange;

        private void OnTriggerEnter(Collider other)
        {
            var col = other.GetComponent<Collectable>();
            
            if (col == null) return;
            
            if (col.IsAutocollected)
                autocollectablesInRange.Add(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            var col = other.GetComponent<Collectable>();
            
            if (col == null) return;
            
            if (col.IsAutocollected)
                autocollectablesInRange.Remove(other.gameObject);
        }
    }
}
