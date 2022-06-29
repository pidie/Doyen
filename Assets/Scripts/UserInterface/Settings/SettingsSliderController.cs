using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.Settings
{
	public class SettingsSliderController : MonoBehaviour
	{
		[SerializeField] private string channel;
		[SerializeField] private float defaultValue = 0.8f;
		
		private Toggle _toggle => GetComponentInChildren<Toggle>();
		private Slider _slider => GetComponentInChildren<Slider>();

		public string Channel => channel;
		public Toggle Toggle => _toggle;
		public float TempValue { get; set; }

		public Slider Slider => _slider;

		public float Value
		{
			get => Slider == null ? defaultValue : Slider.value;
			set => Slider.value = value;
		}

		private void Awake() => Slider.minValue = 0.0001f;
	}
}