using System;

namespace ESTD.ECS.Components
{
	public class RotationComponent : IComponent
	{
		public float Rotation {
			get;
			set;
		}

		public RotationComponent ()
		{
		}

		#region IComponent implementation

		public string ComponentType {
			get {
				return "Rotation";
			}
		}

		#endregion
	}
}

