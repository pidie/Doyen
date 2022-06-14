using System;
using TMPro;
using UnityEngine;

namespace UserInterface
{
	public class TooltipManager : MonoBehaviour
	{
		private TextMeshProUGUI _tooltipText;
		private RectTransform _tooltipWindow;
		private Tooltip _activeTooltip;

		public static Action<string, Tooltip, Transform> OnCreateTooltip;
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

		public void CreateTooltip(string message, Tooltip tooltip, Transform parent)
		{
			_activeTooltip = tooltip;
			var activeTooltipText = _activeTooltip.reference.text;
			var activeTooltipWindow = _activeTooltip.reference.window;

			activeTooltipText.text = message;
			activeTooltipWindow.sizeDelta = new Vector2(activeTooltipText.preferredWidth > 200 ? 200 : activeTooltipText.preferredWidth,
				activeTooltipText.preferredHeight);

			_activeTooltip = Instantiate(_activeTooltip, parent);

			Offset = new Vector2(activeTooltipWindow.sizeDelta.x / 2, 0);
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
		public TextMeshProUGUI text;
		public RectTransform window;

		public TooltipReference(TextMeshProUGUI text, RectTransform window)
		{
			this.text = text;
			this.window = window;
		}

		public TooltipReference(TMP_Text text, RectTransform window)
		{
			this.text = (TextMeshProUGUI) text;
			this.window = window;
		}

		public TooltipReference(RectTransform window)
		{
			this.window = window;
			text = window.GetComponentInChildren<TextMeshProUGUI>();
		}
	}
}