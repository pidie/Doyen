using System.Collections;
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

        private bool _runDetection;

        public static int NumberOfCollectablesInRange { get; private set; }

        public Collider[] hits;

        private void Awake() => _runDetection = true;

        private void Update()
        {
            if (_runDetection)
                DetectCollectables();
            else
                StartCoroutine(DetectionCooldown());

            if (Input.GetKeyDown(KeyCode.E))
            {
                TryPickUpItem();
            }
        }

        private void UpdateCollectablesInRangeCount() => NumberOfCollectablesInRange = collectablesInRange.Count;

        private void DetectCollectables()
        {
            hits = new Collider[20];
            Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, hits);

            var collectables = new List<Collectable>();

            foreach (var hit in hits)
                if (hit.GetComponent<Collectable>() != null)
                    collectables.Add(hit.GetComponent<Collectable>());

            collectablesInRange = collectables.Count > 0 ? collectables : collectablesInRange;

            _runDetection = false;
        }

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

        private IEnumerator DetectionCooldown()
        {
            yield return new WaitForSeconds(0.1f);
            _runDetection = true;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color32(100, 255, 100, 100);
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
    }
}