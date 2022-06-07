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
            foreach (var slot in _inventoryItemSlots)
            {
                slot.gameObject.SetActive(false);
            }
            var itemsAdded = new HashSet<Ingredient>();
            for (var i = 0; i < PlayerInventory.Items.Count; i++)
            {
                var slot = _inventoryItemSlots[i];
                var item = PlayerInventory.Items[i];

                if (itemsAdded.Contains(item.ingredient))
                {
                    var itemSlot = FindItemSlotByIngredient(item.ingredient);
                    itemSlot.quantityText.text = (int.Parse(itemSlot.quantityText.text) + 1).ToString();
                }
                else
                {
                    slot.icon.texture = item.ingredient.icon;
                    slot.quantityText.text = 1.ToString();
                    slot.itemName = item.ingredient.name;
                    slot.gameObject.SetActive(true);
                }

                itemsAdded.Add(item.ingredient);
            }
        }

        private InventoryItemSlot FindItemSlotByIngredient(Ingredient ingredient) =>
            _inventoryItemSlots.FirstOrDefault(slot => slot.isActiveAndEnabled && slot.itemName == ingredient.name);
    }
}
