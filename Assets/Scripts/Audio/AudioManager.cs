using System;
using UnityEngine;

namespace Audio
{
	public class AudioManager : MonoBehaviour
	{
		[SerializeField] private Sound[] sounds;

		public static Action<string, bool> onPlaySound;

		private void Awake()
		{
			onPlaySound += Play;
			
			foreach (var sound in sounds)
			{
				sound.source = gameObject.AddComponent<AudioSource>();
				sound.source.clip = sound.clip;
				sound.source.volume = sound.volume;
				sound.source.pitch = sound.pitch;
				sound.source.playOnAwake = sound.playOnAwake;
				sound.source.loop = sound.loop;
				sound.source.outputAudioMixerGroup = sound.output;
			}
		}

		private void Start() => Play("Level Music", false);

		private void Play(string soundName, bool isOneShot)
		{
			var sound = Array.Find(sounds, s => s.name == soundName);
			
			if (isOneShot)
				sound?.source.PlayOneShot(sound.clip);
			else
				sound?.source.Play();
		}
	}
}
