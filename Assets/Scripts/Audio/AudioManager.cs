using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
	public class AudioManager : MonoBehaviour
	{
		[SerializeField] private Sound[] sounds;
		private AudioSource _audioSource;

		public static Action<string> OnPlaySound;

		private void Awake()
		{
			OnPlaySound += Play;
			
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

		private void Start() => Play("Level Music");

		private void Play(string clipName)
		{
			var s = Array.Find(sounds, sound => sound.name == clipName);
			s?.source.Play();
		}
	}

	[Serializable]
	public class Sound
	{
		public string name;
		public AudioClip clip;
		[Range(0f, 1f)]
		public float volume;
		[Range(0.1f, 3f)]
		public float pitch;
		[HideInInspector]
		public AudioSource source;

		public bool playOnAwake;
		public bool loop;
		public AudioMixerGroup output;
	}
}
