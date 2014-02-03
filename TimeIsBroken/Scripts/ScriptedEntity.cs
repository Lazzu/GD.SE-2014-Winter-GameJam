using System;
using TimeIsBroken.ECS.Entities;

namespace TimeIsBroken.Scripts
{
	public class ScriptedEntity : IScript
	{


		public ScriptedEntity (Entity entity)
		{
			Entity = entity;
		}

		#region IScript implementation

		public virtual void OnUpdate (double deltaTime)
		{

		}

		public virtual void OnCollision (TimeIsBroken.ECS.Entities.Entity e)
		{

		}

		public virtual void OnSpawn ()
		{

		}

		public virtual void OnDeath ()
		{

		}

		public virtual void OnDamaged (float damage)
		{

		}

		public virtual IScript Clone ()
		{
			throw new NotImplementedException ();
		}

		public Entity Entity {
			get;
			protected set;
		}

		#endregion
	}
}

