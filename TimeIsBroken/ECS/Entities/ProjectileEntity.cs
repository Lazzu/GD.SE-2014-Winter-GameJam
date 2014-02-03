using System;
using TimeIsBroken.ECS.Components;
using OpenTK;
using TimeIsBroken.Graphics.Sprites;

namespace TimeIsBroken.ECS.Entities
{
	public class ProjectileEntity : Entity
	{
		public ProjectileEntity (float damage, Vector3 position, Sprite sprite, float speed, double life, string group)
		{
			this.AddComponent (new DamageComponent (){ Damage = damage });
			this.AddComponent (new PositionComponent (){ Position = position });
			this.AddComponent(new DirectionComponent());
			this.AddComponent(new VelocityComponent() { Velocity = new Vector3(0,-speed,0)});
			this.AddComponent(new SpriteComponent() { Sprite = sprite});
			this.AddComponent(new LifeSpanComponent() { Life = life});
			this.AddComponent(new ProjectileComponent(){
				RemoveOnImpact = true
			});
			this.AddComponent (new BoundingBoxComponent () {
				Box = new Vector4(
					-sprite.Frames[0].Size.X / 2, 
					-sprite.Frames[0].Size.Y / 2, 
					sprite.Frames[0].Size.X / 2,
					sprite.Frames[0].Size.Y / 2
				),
				Group = group,
				Type = "Projectile"
			});
		}
	}
}

