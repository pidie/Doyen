using System;
using TMPro;
using UnityEngine;

namespace UserInterface
{
	public class TooltipManager : MonoBehaviour
	{
		private Tooltip _activeTooltip;

		public static Action<string, string, Tooltip, Transform> OnCreateTooltip;
		public static Action OnDestroyTooltip;

		public static Vector2 Offset { get; private set; }

		private void OnEnable()
		{
			OnCreateTooltip += CreateTooltip;
			OnDestroyTooltip += DestroyTooltip;
		}

		private void OnDisable()
		{
			OnCreateTooltip -= CreateTooltip;
			OnDestroyTooltip -= DestroyTooltip;
		}

		public void CreateTooltip(string title, string contents, Tooltip tooltip, Transform parent)
		{
			_activeTooltip = tooltip;
			var activeTooltipTitle = _activeTooltip.reference.title;
			var activeTooltipContents = _activeTooltip.reference.contents;
			var activeTooltipWindow = _activeTooltip.reference.window;

			activeTooltipTitle.text = title;
			activeTooltipContents.text = contents;
			activeTooltipWindow.sizeDelta = new Vector2(activeTooltipContents.preferredWidth > 300 ? 300 : activeTooltipContents.preferredWidth,
				activeTooltipTitle.preferredHeight + activeTooltipContents.preferredHeight);

			activeTooltipContents.rectTransform.sizeDelta = new Vector2(activeTooltipWindow.sizeDelta.x,
				activeTooltipContents.preferredHeight);

			activeTooltipTitle.rectTransform.anchoredPosition =
				new Vector2(activeTooltipTitle.rectTransform.anchoredPosition.x, 0);
			activeTooltipContents.rectTransform.anchoredPosition =
				new Vector2(activeTooltipContents.rectTransform.anchoredPosition.x, 0);

			_activeTooltip = Instantiate(_activeTooltip, parent);
			
			var tooltipSizeDelta = activeTooltipWindow.sizeDelta;
			Offset = new Vector2(tooltipSizeDelta.x / 2, tooltipSizeDelta.y / 2);
			_activeTooltip.transform.position = (Vector2) Input.mousePosition + Offset;
			
			_activeTooltip.gameObject.AddComponent<TooltipMouseFollow>();
		}

		public void DestroyTooltip()
		{
			if (_activeTooltip != null)
			{
				Destroy(_activeTooltip.gameObject);
				_activeTooltip = null;
			}
		}
	}

	[Serializable]
	public struct TooltipReference
	{
		public TextMeshProUGUI title;
		public TextMeshProUGUI contents;
		public RectTransform window;

		public TooltipReference(TextMeshProUGUI title, TextMeshProUGUI contents, RectTransform window)
		{
			this.title = title;
			this.contents = contents;
			this.window = window;
		}

		public TooltipReference(TMP_Text title, TMP_Text contents, RectTransform window)
		{
			this.title = (TextMeshProUGUI) title;
			this.contents = (TextMeshProUGUI) contents;
			this.window = window;
		}
	}
}