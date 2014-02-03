using System;
using TimeIsBroken.Input;
using OpenTK;
using TimeIsBroken.ECS.Components;
using TimeIsBroken.ECS.Entities;
using TimeIsBroken.Graphics.Sprites;

namespace TimeIsBroken.ECS.Systems
{
	public class PlayerControlsSystem : ISystem
	{
		KeyboardListener keyboardListener;

		public float PlayerMoveSpeed {
			get;
			set;
		}

		public Vector2 MinBounds {
			get;
			set;
		}

		public Vector2 MaxBounds {
			get;
			set;
		}

		double lastShootTime = 0;
		public double ShootTimeSpan {
			get;
			set;
		}

		public PlayerControlsSystem (KeyboardListener keyboard)
		{
			keyboardListener = keyboard;
			PlayerMoveSpeed = 500;
			ShootTimeSpan = 0.05;
		}

		#region ISystem implementation

		public bool Fits (TimeIsBroken.ECS.Entities.Entity e)
		{
			return e.Components.ContainsKey ("PlayerControlled") && 
				e.Components.ContainsKey ("Position");
		}

		public void Initialize (Entity e)
		{

		}

		public void Update (TimeIsBroken.ECS.Entities.Entity e, double deltaTime)
		{
			Vector3 positionChanges = Vector3.Zero;

			// If moving left
			if(keyboardListener.LeftDown)
			{
				positionChanges.X += (float)deltaTime * -PlayerMoveSpeed;
			}

			// If moving right
			if(keyboardListener.RightDown)
			{
				positionChanges.X += (float)deltaTime * PlayerMoveSpeed;
			}

			// If moving up
			if(keyboardListener.UpDown)
			{
				positionChanges.Y += (float)deltaTime * -PlayerMoveSpeed;
			}

			// if moving down
			if(keyboardListener.DownDown)
			{
				positionChanges.Y += (float)deltaTime * PlayerMoveSpeed;
			}

			// Fetch position from entity
			Vector3 position = ((PositionComponent)e.Components ["Position"]).Position;

			// Apply position changes
			position += positionChanges;

			// Check for bounds
			if (position.X < MinBounds.X)
				position.X = MinBounds.X;

			if (position.Y < MinBounds.Y)
				position.Y = MinBounds.Y;

			if (position.X > MaxBounds.X)
				position.X = MaxBounds.X;

			if (position.Y > MaxBounds.Y)
				position.Y = MaxBounds.Y;

			// Update the position to entity
			((PositionComponent)e.Components ["Position"]).Position = position;

			lastShootTime += deltaTime;

			if(lastShootTime > ShootTimeSpan)
			{
				lastShootTime -= ShootTimeSpan;

				if(keyboardListener.FireDown)
				{
					position += new Vector3 (0, -30, 0);
					ECSEngine.AddEntity (
						new ProjectileEntity (
							2, position, 
							new Sprite(){
								Texture = Game.Sprites.Sprites["Projectile.BlueProjectile"].Texture,
								Frames = Game.Sprites.Sprites["Projectile.BlueProjectile"].Frames,
								Scale = Game.Sprites.Sprites["Projectile.BlueProjectile"].Scale,
								BlendingMode = Game.Sprites.Sprites["Projectile.BlueProjectile"].BlendingMode,
							}, 
							300, 
							3,
							"HitsEnemies"
						)
					);
				}

			}


		}

		public void Render (TimeIsBroken.ECS.Entities.Entity e, double deltaTime)
		{
			// Do nothing here
		}

		#endregion
	}
}

