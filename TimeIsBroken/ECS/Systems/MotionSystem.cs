using System;
using OpenTK;
using ESTD.ECS.Components;
using ESTD.ECS.Entities;

namespace ESTD.ECS.Systems
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

		public void Update (Entity e, double deltaTime)
		{
			// Get current position and velocity
			Vector3 pos = ((PositionComponent)e.Components ["Position"]).Position;
			Vector3 vel = ((VelocityComponent)e.Components ["Velocity"]).Velocity;

			// Add velocity to position
			pos += vel * (float)deltaTime;

			// Update position
			((PositionComponent)e.Components ["Position"]).Position = pos;

			// Check if entity has a rotation modifier
			if(e.Components.ContainsKey("RotationModifier"))
			{
				// Get modifier
				float mod = ((RotationModifierComponent)e.Components ["RotationModifier"]).RotationModifier;

				// Check if modifier is not zero (no modification
				if (mod != 0) {
					// Get current rotation
					float rot = ((RotationComponent)e.Components ["Rotation"]).Rotation;

					// Add modifier in the rotation
					rot += mod * (float)deltaTime;

					// Update rotation
					((RotationComponent)e.Components ["Rotation"]).Rotation = rot;
				}
			}
		}

		public void Render (Entity e, double deltaTime)
		{

		}

		#endregion
	}
}

