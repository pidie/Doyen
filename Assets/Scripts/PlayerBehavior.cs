using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public static bool CanInteract;

    public static void Interact()
    {
        if (!CanInteract) return;
        
        
        // InventoryManager.Instance.onCollectablePickedUp.Invoke(collectable);
        // collectable.gameObject.SetActive(false);
    }
}
