using System;
using TimeIsBroken.Graphics.Sprites;
using OpenTK;
using TimeIsBroken.ECS.Components;

namespace TimeIsBroken.ECS.Entities
{
	public class Explosion : Entity
	{
		public Explosion (Sprite sprite, Vector3 position, double life)
		{
			this.AddComponent (new PositionComponent () { Position = position });
			// Do not use the same sprite, instead create new one using the stuff from the sprite.
			// This prevents all explosions having the same current frame.
			this.AddComponent (new SpriteComponent () { 
				Sprite = new Sprite () {
					Frames = sprite.Frames,
					Texture = sprite.Texture,
					Scale = sprite.Scale,
					CurrentFrame = 0,
					BlendingMode = sprite.BlendingMode
				}
			});
			this.AddComponent (new LifeSpanComponent () { Life = life });
		}
	}
}

