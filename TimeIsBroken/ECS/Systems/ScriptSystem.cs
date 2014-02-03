using System;
using TimeIsBroken.ECS.Components;

namespace TimeIsBroken.ECS.Systems
{
	public class ScriptSystem : ISystem
	{
		public ScriptSystem ()
		{
		}

		#region ISystem implementation

		public bool Fits (TimeIsBroken.ECS.Entities.Entity e)
		{
			return e.Components.ContainsKey ("Script");
		}

		public void Initialize (TimeIsBroken.ECS.Entities.Entity e)
		{
			((ScriptComponent)e.Components ["Script"]).Script.OnSpawn ();
		}

		public void Update (TimeIsBroken.ECS.Entities.Entity e, double deltaTime)
		{
			((ScriptComponent)e.Components ["Script"]).Script.OnUpdate (deltaTime);
		}

		public void Render (TimeIsBroken.ECS.Entities.Entity e, double deltaTime)
		{
			// Nothing to be done here
		}

		#endregion
	}
}

