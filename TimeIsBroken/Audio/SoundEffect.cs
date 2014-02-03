using System;
using OpenTK.Audio.OpenAL;

namespace TimeIsBroken.Audio
{
	public class SoundEffect
	{
		int buffer, source;

		public int Buffer {
			get {
				return buffer;
			}
		}

		public int Source {
			get {
				return source;
			}
		}

		public bool Looping {
			get;
			set;
		}

		public SoundEffect ()
		{
			buffer = AL.GenBuffer ();
			source = AL.GenSource ();

			AL.Source( source, ALSourcei.Buffer, buffer );
		}

		public void SetGain (float gain)
		{
			AL.Source( source, ALSourcef.Gain, gain );
		}

		public void Play ()
		{
			AL.SourcePlay( source );
			AL.Source( source, ALSourceb.Looping, Looping );
		}

		public void Stop ()
		{
			AL.SourceStop (source);
		}
	}
}

