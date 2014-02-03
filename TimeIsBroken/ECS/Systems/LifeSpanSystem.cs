using System;
using TimeIsBroken.ECS.Components;

namespace TimeIsBroken.ECS.Systems
{
	public class LifeSpanSystem : ISystem
	{
		public LifeSpanSystem ()
		{
		}

		#region ISystem implementation

		public bool Fits (TimeIsBroken.ECS.Entities.Entity e)
		{
			return e.Components.ContainsKey ("LifeSpan");
		}

		public void Initialize (TimeIsBroken.ECS.Entities.Entity e)
		{

		}

		public void Update (TimeIsBroken.ECS.Entities.Entity e, double deltaTime)
		{
			// Get current life
			double life = ((LifeSpanComponent)e.Components ["LifeSpan"]).Life;

			// Reduce life
			life -= deltaTime;

			// Check if remaining life is equal to or less than zero
			if (life <= 0) {
				ECSEngine.RemoveEntity (e);
				return;
			}

			// Update life
			((LifeSpanComponent)e.Components ["LifeSpan"]).Life = life;
		}

		public void Render (TimeIsBroken.ECS.Entities.Entity e, double deltaTime)
		{

		}

		#endregion
	}
}

