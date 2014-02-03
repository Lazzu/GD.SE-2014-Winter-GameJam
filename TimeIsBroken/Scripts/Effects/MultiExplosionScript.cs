using System;
using TimeIsBroken.ECS.Entities;
using TimeIsBroken.ECS;
using TimeIsBroken.ECS.Components;
using TimeIsBroken.Graphics.Sprites;
using OpenTK;

namespace TimeIsBroken.Scripts.Effects
{
	public class MultiExplosionScript : ScriptedEntity
	{
		public MultiExplosionScript (Entity entity) : base (entity)
		{

		}

		double explosionTimer = 0;
		Random r = new Random();

		public override void OnUpdate (double deltaTime)
		{
			explosionTimer += deltaTime;

			if(explosionTimer >= 0.15)
			{
				explosionTimer -= 0.15;

				Sprite sprite = Game.Sprites.Sprites["Explosions.1"];
				Vector3 position = ((PositionComponent)Entity.Components ["Position"]).Position;

				position.X += (float)(r.NextDouble () - 0.5) * sprite.Frames [0].Size.X;
				position.Y += (float)(r.NextDouble () - 0.5) * sprite.Frames [0].Size.Y;

				ECSEngine.AddEntity (new Explosion (sprite, position, sprite.TotalAnimationTime));

			}

		}


	}
}

