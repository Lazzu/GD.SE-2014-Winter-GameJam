using System;
using OpenTK.Audio;
using System.Collections.Generic;
using OpenTK.Audio.OpenAL;

namespace TimeIsBroken.Audio
{
	public static class SoundManager
	{
		static Dictionary<string, SoundEffect> sounds = new Dictionary<string, SoundEffect>();

		public static bool AudioInitialized {
			get;
			set;
		}

		static AudioContext AC;

		public static void Initialize()
		{
			AudioInitialized = false;

			try
			{
				AC = new AudioContext();
			} 
			catch( AudioException e)
			{ 
				// problem with Device or Context, cannot continue
				return;
			}

			AudioInitialized = true;
		}

		public static void LoadSound (string name, string file)
		{
			if (!AudioInitialized)
				return;

			SoundEffect sound = new SoundEffect ();



		}

	}
}

