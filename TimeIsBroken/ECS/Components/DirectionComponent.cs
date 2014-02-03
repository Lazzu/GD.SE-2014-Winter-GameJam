using System;
using OpenTK;

namespace ESTD.ECS.Components
{
	public class DirectionComponent : IComponent
	{
		public Vector3 Direction {
			get;
			set;
		}

		public DirectionComponent ()
		{
		}

		#region IComponent implementation

		public string ComponentType {
			get {
				return "Direction";
			}
		}

		#endregion
	}
}

