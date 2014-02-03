using System;
using ESTD.ECS.Entities;
using ESTD.ECS.Components;
using OpenTK;

namespace ESTD.ECS.Systems
{
	public class SpriteSystem : ISystem
	{
		public SpriteSystem ()
		{
		}

		public bool Fits(Entity e)
		{
			// Requires Sprite and Position
			return (e.Components.ContainsKey ("Sprite") && (e.Components.ContainsKey ("Position") || e.Components.ContainsKey ("Position2") || e.Components.ContainsKey ("Position3") ));
		}

		public void Update (Entity e, double deltaTime)
		{
			// Do nothing here
		}
		public void Render (Entity e, double deltaTime)
		{
			SpriteComponent sprite = (SpriteComponent)e.Components ["Sprite"];

			Vector3 position = ((PositionComponent)e.Components ["Position"]).Position;

			float rotation = 0;

			if(e.Components.ContainsKey("Rotation"))
			{
				rotation = ((RotationComponent)e.Components ["Rotation"]).Rotation;
			}

			sprite.Sprite.Render (position, rotation);
		}
	}
}

