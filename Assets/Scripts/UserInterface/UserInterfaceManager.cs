using System;
using Audio;
using UnityEngine;

namespace UserInterface
{
    [Serializable]
    public enum ButtonClickSound
    {
        Click01 = 1,
        Click02 = 2,
        Click03 = 3,
        Click04 = 4
    }
    public class UserInterfaceManager : MonoBehaviour
    {
        public void PlayButtonClick(string soundName) => AudioManager.onPlayOneShot(soundName);

        private string Click(ButtonClickSound sound)
        {
            var str = sound switch
            {
                ButtonClickSound.Click01 => "click01",
                ButtonClickSound.Click02 => "click02",
                ButtonClickSound.Click03 => "click03",
                ButtonClickSound.Click04 => "click04",
                _ => throw new ArgumentOutOfRangeException(nameof(sound), sound, "No click sound assigned")
            };

            return str;
        }
    }
}
