using System;
using ESTD.Graphics.Textures;
using OpenTK;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;

namespace ESTD.Graphics.Sprites
{
	public class Sprite
	{
		public Texture Texture {
			get;
			set;
		}

		public List<SpriteFrame> Frames {
			get;
			set;
		}

		public int CurrentFrame {
			get;
			set;
		}

		public float Scale {
			get;
			set;
		}

		double frameTime = 0;

		public Sprite ()
		{
			Frames = new List<SpriteFrame> ();
			CurrentFrame = 0;
			Scale = 1;
		}

		public Sprite (ICollection<SpriteFrame> frames) : this()
		{
			Frames.AddRange (frames);
		}

		public void Update(double deltaTime)
		{
			// If no frames or only one, nothing to do here.
			if (Frames.Count <= 1)
				return;

			// How long current frame has been on the screen?
			frameTime += deltaTime;

			// Get the current frame delay
			double currentDelay = Frames [CurrentFrame].Delay;

			// Check if the frame should be changed
			if(frameTime >= currentDelay)
			{
				frameTime -= currentDelay;
				CurrentFrame++;
			}

			// Check if we have reached the last frame and need to start over from the beginning
			if(CurrentFrame == Frames.Count)
			{
				CurrentFrame = 0;
			}
		}

		public void Render(Vector2 position, float rotation)
		{
			SpriteRenderer.RenderSprite (this, new Vector3(position.X, position.Y, 0), rotation);
		}

		public void Render(Vector3 position, float rotation)
		{
			SpriteRenderer.RenderSprite (this, position, rotation);
		}

	}
}

