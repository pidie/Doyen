using System;
using TMPro;
using UnityEngine;

namespace UserInterface
{
	public class TooltipManager : MonoBehaviour
	{
		public TextMeshProUGUI tooltipText;
		public RectTransform tooltipWindow;

		public static Action<string, Vector2> OnMouseShowMessage;
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

		private void ShowTooltip(string message, Vector2 mousePosition)
		{
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
}