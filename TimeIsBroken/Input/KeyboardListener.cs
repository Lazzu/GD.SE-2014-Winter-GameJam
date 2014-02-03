using System;
using OpenTK;
using OpenTK.Input;

namespace TimeIsBroken.Input
{
	public class KeyboardListener
	{

		public bool LeftDown {
			get;
			protected set;
		}

		public bool LeftPushed {
			get;
			protected set;
		}

		public bool RightDown {
			get;
			protected set;
		}

		public bool RightPushed {
			get;
			protected set;
		}

		public bool UpDown {
			get;
			protected set;
		}

		public bool UpPushed {
			get;
			protected set;
		}

		public bool DownDown {
			get;
			protected set;
		}

		public bool DownPushed {
			get;
			protected set;
		}

		public bool FireDown {
			get;
			protected set;
		}

		public bool FirePushed {
			get;
			protected set;
		}

		GameWindow gameWindow;

		public KeyboardListener (GameWindow gw)
		{
			gw.UpdateFrame += HandleUpdateFrame;
			gameWindow = gw;
		}

		void HandleUpdateFrame (object sender, FrameEventArgs e)
		{
			LeftPushed = false;
			if(gameWindow.Keyboard[InputSettings.Current.LeftKey])
			{
				if(!LeftDown)
				{
					LeftPushed = true;
				}
				LeftDown = true;
			}
			else
			{
				LeftDown = false;
			}

			RightPushed = false;
			if(gameWindow.Keyboard[InputSettings.Current.RightKey])
			{
				if(!RightDown)
				{
					RightPushed = true;
				}
				RightDown = true;
			}
			else
			{
				RightDown = false;
			}

			UpPushed = false;
			if(gameWindow.Keyboard[InputSettings.Current.UpKey])
			{

				if(!UpDown)
				{
					UpPushed = true;
				}
				UpDown = true;
			}
			else
			{
				UpDown = false;
			}

			DownPushed = false;
			if(gameWindow.Keyboard[InputSettings.Current.DownKey])
			{
				if(!DownDown)
				{
					DownPushed = true;
				}
				DownDown = true;
			}
			else
			{
				DownDown = false;
			}

			FirePushed = false;
			if(gameWindow.Keyboard[InputSettings.Current.FireKey])
			{
				if(!FireDown)
				{
					FirePushed = true;
				}
				FireDown = true;
			}
			else
			{
				FireDown = false;
			}
		}

	}
}

