using System;
using OpenTK;

namespace ESTD.ECS.Components
{
	public class VelocityComponent : IComponent
	{
		public Vector3 Velocity {
			get;
			set;
		}

		#region IComponent implementation

		public string ComponentType {
			get {
				return "Velocity";
			}
		}

		#endregion


	}
}

