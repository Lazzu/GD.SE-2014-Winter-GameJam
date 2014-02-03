using System;
using TimeIsBroken.ECS.Entities;
using TimeIsBroken.ECS.Components;
using TimeIsBroken.ECS;
using OpenTK;

namespace TimeIsBroken.Scripts
{
	public class PlayerShipScript : ScriptedEntity
	{
		public PlayerShipScript (Entity e) : base(e)
		{
		}

		#region IScript implementation

		public override void OnCollision (Entity entity)
		{

			if(entity.Components.ContainsKey("Damage"))
			{
				float damage = ((DamageComponent)entity.Components ["Damage"]).Damage;

				OnDamaged (damage);

				if(((HealthComponent)Entity.Components ["Health"]).Health <= 0)
				{
					OnDeath ();
				}

			}

			if(entity.Components.ContainsKey("Script"))
			{
				((ScriptComponent)entity.Components ["Script"]).Script.OnDeath ();
			}



		}

		public override void OnSpawn ()
		{

		}

		public override void OnDeath ()
		{
			Vector3 position = ((PositionComponent)Entity.Components ["Position"]).Position;

			ECSEngine.AddEntity (new MultiExplosionEntity (position, 10));

			ECSEngine.RemoveEntity (Entity);
		}

		public override void OnDamaged (float damage)
		{
			((HealthComponent)Entity.Components ["Health"]).Health -= damage;
		}

		public override IScript Clone ()
		{
			return new PlayerShipScript (Entity);
		}

		#endregion
	}
}

