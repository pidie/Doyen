using System;
using TMPro;
using UnityEngine;

namespace UserInterface
{
	public class TooltipManager : MonoBehaviour
	{
		private TextMeshProUGUI _tooltipText;
		private RectTransform _tooltipWindow;

		public static Action<string, Vector2, Tooltip> OnMouseShowMessage;
		public static Action OnMouseHideMessage;

		public static float Offset { get; private set; }

		private void Awake() => HideTooltip();

		private void OnEnable()
		{
			OnMouseShowMessage += ShowTooltip;
			OnMouseHideMessage += HideTooltip;
		}

		private void OnDisable()
		{
			OnMouseShowMessage -= ShowTooltip;
			OnMouseHideMessage -= HideTooltip;
		}

		private void ShowTooltip(string message, Vector2 mousePosition, Tooltip tooltip)
		{
			_tooltipText = tooltip.text;
			_tooltipWindow = tooltip.window;
			
			_tooltipText.text = message;
			_tooltipWindow.sizeDelta = new Vector2(_tooltipText.preferredWidth > 200 ? 200 : _tooltipText.preferredWidth,
				_tooltipText.preferredHeight);

			_tooltipWindow.gameObject.SetActive(true);

			Offset = _tooltipWindow.sizeDelta.x / 2;
			_tooltipWindow.transform.position = new Vector2(mousePosition.x + Offset, mousePosition.y);
		}

		private void HideTooltip()
		{
			_tooltipText.text = default;
			_tooltipWindow.gameObject.SetActive(false);
		}
	}

	public struct Tooltip
	{
		public TextMeshProUGUI text;
		public RectTransform window;

		public Tooltip(TextMeshProUGUI text, RectTransform window)
		{
			this.text = text;
			this.window = window;
		}

		public Tooltip(TMP_Text text, RectTransform window)
		{
			this.text = (TextMeshProUGUI) text;
			this.window = window;
		}

		public Tooltip(RectTransform window)
		{
			this.window = window;
			this.text = window.GetComponentInChildren<TextMeshProUGUI>();
		}
	}
}