using System;
using TimeIsBroken.ECS.Components;
using OpenTK;
using TimeIsBroken.Graphics.Sprites;

namespace TimeIsBroken.ECS.Entities
{
	public class SpriteEntity : Entity
	{
		public SpriteEntity (Vector3 position, Vector3 velocity, float rotation, Sprite sprite)
		{
			this.AddComponent(new PositionComponent() { Position = position});
			this.AddComponent(new RotationComponent() { Rotation = 0});
			this.AddComponent(new SpeedComponent() { Speed = 50});
			this.AddComponent(new PathFollowerComponent() { TargetIndex = 0});
			this.AddComponent(new SpriteComponent() { Sprite = sprite});
		}
	}
}

