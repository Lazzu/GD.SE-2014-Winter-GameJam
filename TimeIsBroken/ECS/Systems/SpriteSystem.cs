using System;
using TimeIsBroken.ECS.Entities;
using TimeIsBroken.ECS.Components;
using OpenTK;
using TimeIsBroken.Graphics.Sprites;

namespace TimeIsBroken.ECS.Systems
{
	public class SpriteSystem : ISystem
	{
		public SpriteSystem ()
		{
		}

		public bool Fits(Entity e)
		{
			// Requires Sprite and Position
			return (e.Components.ContainsKey ("Sprite") && e.Components.ContainsKey ("Position"));
		}

		public void Initialize (Entity e)
		{

		}

		public void Update (Entity e, double deltaTime)
		{
			Sprite sprite = ((SpriteComponent)e.Components ["Sprite"]).Sprite;
			sprite.Update (deltaTime);
		}
		public void Render (Entity e, double deltaTime)
		{
			Sprite sprite = ((SpriteComponent)e.Components ["Sprite"]).Sprite;

			Vector3 position = ((PositionComponent)e.Components ["Position"]).Position;

			float rotation = 0;

			if(e.Components.ContainsKey("Rotation"))
			{
				rotation = ((RotationComponent)e.Components ["Rotation"]).Rotation;
			}

 			sprite.Render (position, rotation);
		}
	}
}

