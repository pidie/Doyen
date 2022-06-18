using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UserInterface
{
    public class TooltipReceiver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Tooltip tooltip;
        [SerializeField] private string parentName;

        private Transform _parent;
        private InventoryItemSlot _slot;
        private string _title;
        private string _contents;

        private void Awake()
        {
            _slot = GetComponent<InventoryItemSlot>();
            _parent = GameObject.Find(parentName).transform;
        }

        private void OnEnable()
        {
            _title = _slot.itemName;
            _contents = _slot.data.displayContents;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            StopAllCoroutines();
            StartCoroutine(MessageDelay());
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            StopAllCoroutines();
            TooltipManager.OnDestroyTooltip();
        }

        private void ShowMessage() => TooltipManager.OnCreateTooltip(_title, _contents, tooltip, _parent);

        private IEnumerator MessageDelay()
        {
            yield return new WaitForSeconds(0.5f);
            ShowMessage();
        }
    }
}