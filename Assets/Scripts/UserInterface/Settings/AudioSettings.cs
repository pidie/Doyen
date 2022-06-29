using System.Collections.Generic;
using Audio;
using UnityEngine;

namespace UserInterface.Settings
{
    public class AudioSettings : MonoBehaviour
    {
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private List<SettingsSliderController> audioSliders;

        private void Awake()
        {
            foreach (var slider in audioSliders)
            {
                slider.Slider.onValueChanged.AddListener(delegate
                {
                    AdjustLevel(slider);
                });
                AdjustLevel(slider);
                
                slider.Toggle.onValueChanged.AddListener(delegate
                {
                    ToggleLevel(slider);
                });
            }
        }

        private void AdjustLevel(SettingsSliderController controller)
        {
            if (!controller.Toggle.isOn)
                controller.Toggle.isOn = true;
            
            audioManager.SetLevel(controller.Channel, controller.Value);
            controller.Slider.value = controller.Value;
        }

        private void ToggleLevel(SettingsSliderController controller)
        {
            if (controller.Toggle.isOn)
            {
                audioManager.SetLevel(controller.Channel, controller.TempValue);
            }
            else
            {
                controller.TempValue = controller.Value;
                audioManager.SetLevel(controller.Channel, 0.0001f);
            }
        }
    }
}
