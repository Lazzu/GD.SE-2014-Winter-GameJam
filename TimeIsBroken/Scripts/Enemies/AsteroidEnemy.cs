using System;
using TimeIsBroken.ECS;
using TimeIsBroken.ECS.Entities;
using OpenTK;
using TimeIsBroken.ECS.Components;

namespace TimeIsBroken.Scripts.Enemies
{
	public class AsteroidEnemy : IScript
	{
		public AsteroidEnemy (Entity e)
		{
			Entity = e;
		}

		#region IScript implementation

		public void OnUpdate (double deltaTime)
		{
			// No need for updating, asteroids just go straight
		}

		public void OnCollision (Entity e)
		{

		}

		public void OnSpawn ()
		{
			((VelocityComponent)Entity.Components ["Velocity"]).Velocity = new Vector3 (0, 100, 0);
		}

		public void OnDeath ()
		{
			// Remove the asteroid and add explosion
			ECSEngine.RemoveEntity (Entity);
			ECSEngine.AddEntity (
				new Explosion (
					Game.Sprites.Sprites ["Explosions.1"], 
					((PositionComponent)Entity.Components["Position"]).Position, 
					Game.Sprites.Sprites ["Explosions.1"].TotalAnimationTime
				)
			);
		}

		public void OnDamaged (float damage)
		{
			((HealthComponent)Entity.Components ["Health"]).Health -= damage;
		}

		public Entity Entity {
			get;
			protected set;
		}


		public IScript Clone ()
		{
			return new AsteroidEnemy (Entity);
		}
		#endregion
	}
}

