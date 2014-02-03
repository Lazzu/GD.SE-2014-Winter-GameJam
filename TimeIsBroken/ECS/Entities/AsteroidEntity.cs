using System;
using TimeIsBroken.ECS.Components;
using OpenTK;
using TimeIsBroken.Graphics.Sprites;
using TimeIsBroken.Scripts.Enemies;

namespace TimeIsBroken.ECS.Entities
{
	public class AsteroidEntity : Entity
	{
		public AsteroidEntity (Sprite sprite, Vector3 position,  Vector3 velocity, float rotation, float scale)
		{
			this.AddComponent(new PositionComponent() { Position = position});
			this.AddComponent(new RotationComponent() { Rotation = rotation});
			this.AddComponent(new VelocityComponent() { Velocity = velocity});
			this.AddComponent(new HealthComponent() {Health = 10});
			this.AddComponent (new DamageComponent () { Damage = 10 * scale });
			this.AddComponent(new ProjectileComponent());
			this.AddComponent (new SpriteComponent () { 
				Sprite = new Sprite () {
					Frames = sprite.Frames,
					Texture = sprite.Texture,
					CurrentFrame = 0,
					BlendingMode = sprite.BlendingMode,
					Scale = scale
				}
			});
			this.AddComponent (new BoundingBoxComponent () {
				Box = new Vector4(
					-sprite.Frames[0].Size.X * 0.5f * scale, 
					-sprite.Frames[0].Size.Y * 0.5f * scale, 
					sprite.Frames[0].Size.X * 0.5f * scale,
					sprite.Frames[0].Size.Y * 0.5f * scale
				),
				Group = "HitsEnemies",
				Type = "Target"
			});
			this.AddComponent (new BoundingBoxComponent () {
				Box = new Vector4(
					-sprite.Frames[0].Size.X * 0.5f * scale, 
					-sprite.Frames[0].Size.Y * 0.5f * scale, 
					sprite.Frames[0].Size.X * 0.5f * scale,
					sprite.Frames[0].Size.Y * 0.5f * scale
				),
				Group = "HitsPlayer",
				Type = "Projectile"
			});
			this.AddComponent (new ScriptComponent () {
				Script = new AsteroidEnemy (this)
			});
		}
	}
}

