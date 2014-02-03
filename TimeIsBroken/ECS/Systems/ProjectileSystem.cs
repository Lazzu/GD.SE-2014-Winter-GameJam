using System;

namespace TimeIsBroken.ECS.Systems
{
	public class BoundingGroupSystem : ISystem
	{

		public string BoundingGroup {
			get;
			protected set;
		}

		public BoundingGroupSystem (string group)
		{
			BoundingGroup = group;
		}

		#region ISystem implementation

		public bool Fits (TimeIsBroken.ECS.Entities.Entity e)
		{
			// Take in only projectiles
			return 
				(
					e.Components.ContainsKey (string.Format ("BoundingBox.{0}.Projectile", BoundingGroup))
					|| e.Components.ContainsKey (string.Format ("BoundingSphere.{0}.Projectile", BoundingGroup))
				)
				&& e.Components.ContainsKey ("Velocity") && e.Components.ContainsKey ("Speed")
				&& e.Components.ContainsKey ("Position");
		}

		public void Update (TimeIsBroken.ECS.Entities.Entity e, double deltaTime)
		{

		}

		public void Render (TimeIsBroken.ECS.Entities.Entity e, double deltaTime)
		{

		}

		#endregion
	}
}

