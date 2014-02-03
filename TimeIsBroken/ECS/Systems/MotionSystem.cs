using System;
using OpenTK;
using TimeIsBroken.ECS.Components;
using TimeIsBroken.ECS.Entities;

namespace TimeIsBroken.ECS.Systems
{
	public class MotionSystem : ISystem
	{
		public MotionSystem ()
		{
		}

		#region ISystem implementation

		public bool Fits (Entity e)
		{
			return (e.Components.ContainsKey ("Position") && e.Components.ContainsKey ("Velocity"));
		}

		public void Initialize (Entity e)
		{

		}

		public void Update (Entity e, double deltaTime)
		{
			// Get current position and velocity
			Vector3 pos = ((PositionComponent)e.Components ["Position"]).Position;
			Vector3 vel = ((VelocityComponent)e.Components ["Velocity"]).Velocity;

			// Add velocity to position
			pos += vel * (float)deltaTime;

			// Update position
			((PositionComponent)e.Components ["Position"]).Position = pos;
		}

		public void Render (Entity e, double deltaTime)
		{

		}

		#endregion
	}
}

