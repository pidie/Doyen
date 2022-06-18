using System.Collections.Generic;
using System.Linq;
using Inventory;
using UnityEngine;

namespace UserInterface
{
    public class InventorySlotManager : MonoBehaviour
    {
        private InventoryItemSlot[] _inventoryItemSlots;

        private void Awake() => _inventoryItemSlots = GetComponentsInChildren<InventoryItemSlot>(true);

        private void OnEnable()
        {
            var itemsAdded = new HashSet<IngredientData>();
            var slotModifier = 0;
            
            for (var i = 0; i < PlayerInventory.Items.Count; i++)
            {
                var slot = _inventoryItemSlots[i + slotModifier];
                var item = PlayerInventory.Items[i];
                
                if (itemsAdded.Contains(item.ingredientData))
                {
                    var itemSlot = FindItemSlotByIngredient(item.ingredientData);
                    itemSlot.quantityText.text = (int.Parse(itemSlot.quantityText.text) + 1).ToString();
                    slotModifier--;
                }
                else
                {
                    slot.icon.texture = item.ingredientData.icon;
                    slot.quantityText.text = 1.ToString();
                    slot.itemName = item.ingredientData.name;
                    slot.data = item;
                    slot.gameObject.SetActive(true);
                }

                itemsAdded.Add(item.ingredientData);
            }
        }

        private void OnDisable()
        {
            foreach (var slot in _inventoryItemSlots)
                slot.gameObject.SetActive(false);
        }

        private InventoryItemSlot FindItemSlotByIngredient(IngredientData ingredientData) =>
            _inventoryItemSlots.FirstOrDefault(slot => slot.isActiveAndEnabled && slot.itemName == ingredientData.name);
    }
}
