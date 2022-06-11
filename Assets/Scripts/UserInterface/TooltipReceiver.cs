using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UserInterface
{
    public class TooltipReceiver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public string message;
        
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

        private void ShowMessage()
        {
            TooltipManager.OnMouseShowMessage(message, Input.mousePosition);
        }

        private IEnumerator MessageDelay()
        {
            yield return new WaitForSeconds(0.5f);
            ShowMessage();
        }
    }
}
