using System;
using System.Collections.Generic;
using TimeIsBroken.ECS.Entities;
using OpenTK;
using TimeIsBroken.ECS.Components;

namespace TimeIsBroken.ECS.Systems
{
	public class ProjectileSystem : ISystem
	{

		public string ProjectileGroup {
			get;
			protected set;
		}

		public ProjectileSystem (string group)
		{
			ProjectileGroup = group;
			ECSEngine.AddCollisionGroup (string.Format ("BoundingBox.{0}.Projectile", ProjectileGroup));
			ECSEngine.AddCollisionGroup (string.Format ("BoundingBox.{0}.Target", ProjectileGroup));
		}

		#region ISystem implementation

		public bool Fits (Entity e)
		{
			// Take in only projectiles, not their targets
			return 
				(
			    e.Components.ContainsKey (string.Format ("BoundingBox.{0}.Projectile", ProjectileGroup))
			    || e.Components.ContainsKey (string.Format ("BoundingSphere.{0}.Projectile", ProjectileGroup))
				) && e.Components.ContainsKey("Position") && e.Components.ContainsKey("Projectile");
		}

		public void Initialize (Entity e)
		{

		}

		public void Update (Entity e, double deltaTime)
		{
			string targetGroup = string.Format ("BoundingBox.{0}.Target", ProjectileGroup);
			string projectileGroup = string.Format ("BoundingBox.{0}.Projectile", ProjectileGroup);

			List<Entity> targets = ECSEngine.GetCollidables (targetGroup);

			if (targets == null)
				return;

			// Get projectile position and bounding box
			Vector3 projectilePosition = ((PositionComponent)e.Components ["Position"]).Position;
			Vector4 projectileBox = ((BoundingBoxComponent)e.Components [projectileGroup]).Box;
			ProjectileComponent projectileData = (ProjectileComponent)e.Components ["Projectile"];

			// Offset the bounding box with the position
			projectileBox.Xy += projectilePosition.Xy;
			projectileBox.Zw += projectilePosition.Xy;

			// Go thru all targets we got from ECSEngine
			for (int i = 0; i < targets.Count; i++) {
				Entity target = targets [i];

				// Get target bounding box and position
				Vector4 targetBox = ((BoundingBoxComponent)target.Components [targetGroup]).Box;
				Vector3 targetPosition = ((PositionComponent)target.Components ["Position"]).Position;

				// Offset the bounding box with the position
				targetBox.Xy += targetPosition.Xy;
				targetBox.Zw += targetPosition.Xy;

				// Check for AABB collision
				if ( ! // Note the negation here
					(projectileBox.Z < targetBox.X || 
					projectileBox.W < targetBox.Y || 
					projectileBox.X > targetBox.Z || 
					projectileBox.Y > targetBox.W) 
				) {

					// Collision happened

					// If target is scripted, use scripted events instead of hardcoded events
					if(target.Components.ContainsKey("Script"))
					{
						((ScriptComponent)target.Components ["Script"]).Script.OnCollision (e);

						if (e.Components.ContainsKey ("Damage")) {
							((ScriptComponent)target.Components ["Script"]).Script.OnDamaged (((DamageComponent)e.Components ["Damage"]).Damage);

							float health = ((HealthComponent)target.Components ["Health"]).Health;

							if (health <= 0) {
								((ScriptComponent)target.Components ["Script"]).Script.OnDeath ();
								return;
							}
						}

					}
					else if(target.Components.ContainsKey("Health"))
					{
						float health = ((HealthComponent)target.Components ["Health"]).Health;

						if (e.Components.ContainsKey ("Damage")) {

							float damage = ((DamageComponent)e.Components ["Damage"]).Damage;

							health -= damage;

							if (health <= 0) {
								if(e.Components.ContainsKey ("Script"))
								{
									((ScriptComponent)e.Components ["Script"]).Script.OnDeath ();
								}
								ECSEngine.RemoveEntity (target);
								return;
							}
							((HealthComponent)target.Components ["Health"]).Health = health;
						} else {
							ECSEngine.RemoveEntity (target);
						}


					}

					if(projectileData.ImpactAnimation != null)
					{
						// Get impact position and add animation entity
					}

					if(projectileData.RemoveOnImpact)
					{
						ECSEngine.RemoveEntity (e);
					}




				}

			}

		}

		public void Render (Entity e, double deltaTime)
		{

		}

		#endregion
	}
}

