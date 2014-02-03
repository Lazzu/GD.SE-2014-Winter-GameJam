using System;
using TimeIsBroken.ECS.Entities;

namespace TimeIsBroken.Scripts
{
	public interface IScript
	{
		Entity Entity {
			get;
		}
		void OnUpdate(double deltaTime);
		void OnCollision (Entity e);
		void OnSpawn();
		void OnDeath();
		void OnDamaged(float damage);
		IScript Clone();
	}
}

