using System;
using System.Collections.Generic;
using OpenTK;

namespace ESTD.Graphics.Sprites
{
	public class SpriteManager
	{
		Dictionary<string, Sprite> sprites;

		public Dictionary<string, Sprite> Sprites {
			get {
				return sprites;
			}
		}

		public SpriteManager ()
		{
			sprites = new Dictionary<string, Sprite> ();
		}

		public SpriteManager (GameWindow gw) : this()
		{
			gw.UpdateFrame += HandleUpdateFrame;
		}

		public void AddSprite(string name, Sprite sprite)
		{
			if (sprites.ContainsKey (name))
				throw new ApplicationException ("Sprites must have unique names, and two instances of one sprite must be added with different names.");

			sprites.Add (name, sprite);
		}

		public void RemoveSprite(string name)
		{
			sprites.Remove (name);
		}

		void HandleUpdateFrame (object sender, FrameEventArgs e)
		{
			Update (e.Time);
		}

		public void Update(double deltaTime)
		{
			foreach (var sprite in sprites) {

				sprite.Value.Update (deltaTime);

			}
		}


	}
}

