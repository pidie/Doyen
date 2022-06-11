using System;
using TMPro;
using UnityEngine;

namespace UserInterface
{
	public class TooltipManager : MonoBehaviour
	{
		public TextMeshProUGUI tooltipText;
		public RectTransform tooltipWindow;

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
			tooltipText = tooltip.text;
			tooltipWindow = tooltip.window;
			
			tooltipText.text = message;
			tooltipWindow.sizeDelta = new Vector2(tooltipText.preferredWidth > 200 ? 200 : tooltipText.preferredWidth,
				tooltipText.preferredHeight);

			tooltipWindow.gameObject.SetActive(true);

			Offset = tooltipWindow.sizeDelta.x / 2;
			tooltipWindow.transform.position = new Vector2(mousePosition.x + Offset, mousePosition.y);
		}

		private void HideTooltip()
		{
			tooltipText.text = default;
			tooltipWindow.gameObject.SetActive(false);
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