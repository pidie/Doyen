﻿using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
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