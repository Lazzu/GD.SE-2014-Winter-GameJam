using System;

namespace TimeIsBroken
{
	public static class GameTimer
	{
		static double currentTime;
		public static double CurrentTime {
			get {
				return currentTime;
			}
		}

		static double deltaDirection = 1;
		static double deltaTime;
		public static double Delta {
			get {
				return deltaTime * deltaDirection;
			}
		}

		static double nextGlitchTime;
		public static double TimeUntilNextGlitch {
			get {
				return nextGlitchTime - currentTime;
			}
		}

		public static double GlitchFrequency {
			get;
			set;
		}

		public static double GlitchDelay {
			get;
			set;
		}

		public static void Update (double delta)
		{
			currentTime += delta;
			deltaTime = delta;

			if(TimeUntilNextGlitch <= 0)
			{
				deltaDirection = -deltaDirection;

				if (deltaDirection < 0) {
					nextGlitchTime += GlitchDelay;
				} else {
					nextGlitchTime += GlitchFrequency;
				}

			}
		}
	}
}

