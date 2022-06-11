using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    public class InventoryItemSlot : MonoBehaviour
    {
        [SerializeField] private GameObject UITooltip;

        private GameObject _activeTooltip;
        
        public RawImage icon;
        public TMP_Text quantityText;
        public string itemName;
    }
}