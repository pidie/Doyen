using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UserInterface
{
    public class TooltipReceiver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private InventoryItemSlot _slot;
        private string _message;

        private void Awake() => _slot = GetComponent<InventoryItemSlot>();

        private void OnEnable() => _message = _slot.itemName;

        public void OnPointerEnter(PointerEventData eventData)
        {
            StopAllCoroutines();
            StartCoroutine(MessageDelay());
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            StopAllCoroutines();
            TooltipManager.OnMouseHideMessage();
        }

        private void ShowMessage() => TooltipManager.OnMouseShowMessage(_message, Input.mousePosition);

        private IEnumerator MessageDelay()
        {
            yield return new WaitForSeconds(0.5f);
            ShowMessage();
        }
    }
}