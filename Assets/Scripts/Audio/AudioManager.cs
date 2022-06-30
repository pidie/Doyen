using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
	public class AudioManager : MonoBehaviour
	{
		[SerializeField] private AudioMixer mixer;
		[SerializeField] private Sound[] sounds;

		private float _tempVolume;

		public static Action<string> onPlaySound;
		public static Action<string> onPlayOneShot;
		public static Action<AudioSource, bool> onPlayFromSource;
		public static Action<bool> onMuffleMusic;
		public static Action<float> onFadeMusic;
		public static Action onNewGame;

		private void Awake()
		{
			onPlaySound += Play;
			onPlayOneShot += PlayOneShot;
			onPlayFromSource += PlayFromSource;
			onMuffleMusic += MuffleMusic;
			onFadeMusic += FadeMusic;
			onNewGame += PlayLevelSounds;

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

		private void PlayLevelSounds()
		{
			Play("Level Music");
			Play("garden birds");
		}

		private void Play(string soundName)
		{
			var sound = Array.Find(sounds, s => s.name == soundName);
			sound?.source.Play();
		}

		private void PlayOneShot(string soundName)
		{
			var sound = Array.Find(sounds, s => s.name == soundName);
			sound?.source.PlayOneShot(sound.clip);
		}

		private void PlayFromSource(AudioSource source, bool isOneShot)
		{
			if (isOneShot)
				source.PlayOneShot(source.clip);
			else
				source.Play();
		}

		private void MuffleMusic(bool muffle)
		{
			const float baseFreq = 22000f;

			if (muffle)
			{
				mixer.SetFloat("MusicLowpassFreq", 750f);

				mixer.GetFloat("MasterVolume", out _tempVolume);
				mixer.SetFloat("MasterVolume", -10f);
			}
			else
			{
				mixer.SetFloat("MusicLowpassFreq", baseFreq);
				mixer.SetFloat("MasterVolume", _tempVolume);
			}
		}

		private void FadeMusic(float time) => StartCoroutine(Fade(time));

		private IEnumerator Fade(float time)
		{
			var sound = Array.Find(sounds, s => s.name == "Level Music");
			print(sound.volume);
			mixer.GetFloat("MusicVolume", out var volume);
			
			while (volume > 0)
			{
				var v = Mathf.Lerp(volume, -80f, Time.deltaTime / time);
				mixer.SetFloat("MusicVolume",volume - v);
			}
			
			sound.source.Stop();
			yield return null;

		}

		public void SetLevel(string channel, float value) => mixer.SetFloat(channel, Mathf.Log10(value) * 20);
	}
}
