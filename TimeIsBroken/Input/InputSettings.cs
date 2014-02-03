using System;
using OpenTK.Input;

namespace TimeIsBroken.Input
{
	public class InputSettings
	{
		public Key UpKey {
			get;
			set;
		}

		public Key DownKey {
			get;
			set;
		}

		public Key LeftKey {
			get;
			set;
		}

		public Key RightKey {
			get;
			set;
		}

		public Key FireKey {
			get;
			set;
		}
			
		public static InputSettings Default = new InputSettings () {
			UpKey = Key.Up,
			DownKey = Key.Down,
			LeftKey = Key.Left,
			RightKey = Key.Right,
			FireKey = Key.Space
		};
		public static InputSettings Current = Default;
	}
}

