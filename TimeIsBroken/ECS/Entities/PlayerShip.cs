using System;
using TimeIsBroken.ECS.Components;
using TimeIsBroken.Graphics.Sprites;
using OpenTK;
using TimeIsBroken.Graphics.Textures;
using TimeIsBroken.Scripts;

namespace TimeIsBroken.ECS.Entities
{
	public class PlayerShip : Entity
	{
		public Sprite CurrentProjectileSprite {
			get;
			set;
		}

		public float HitPoints {
			get {
				return ((HealthComponent)this.Components ["Health"]).Health;
			}
		}

		public PlayerShip (Sprite sprite)
		{
			this.AddComponent(new PositionComponent());
			this.AddComponent(new RotationComponent() { Rotation = 0});
			this.AddComponent(new SpriteComponent() { Sprite = sprite});
			this.AddComponent (new HealthComponent (){ Health = 50 });
			this.AddComponent (new ShieldComponent (){ Shield = 10 });
			this.AddComponent(new PlayerControlledComponent());
			this.AddComponent (new BoundingBoxComponent () {
				Box = new Vector4(
					-sprite.Frames[0].Size.X / 2, 
					-sprite.Frames[0].Size.Y / 2, 
					sprite.Frames[0].Size.X / 2,
					sprite.Frames[0].Size.Y / 2
				),
				Group = "HitsPlayer",
				Type = "Target"
			});
			this.AddComponent (new ScriptComponent () {
				Script = new PlayerShipScript (this)
			});
		}
	}
}

