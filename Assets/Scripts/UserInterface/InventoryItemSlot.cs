using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    public class InventoryItemSlot : MonoBehaviour
    {
        private GameObject _activeTooltip;
        
        public RawImage icon;
        public TMP_Text quantityText;
        public string itemName;
    }
}