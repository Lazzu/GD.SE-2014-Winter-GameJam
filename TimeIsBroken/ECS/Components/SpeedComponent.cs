using System;

namespace ESTD.ECS.Components
{
	public class SpeedComponent : IComponent
	{
		public float Speed {
			get;
			set;
		}

		public SpeedComponent ()
		{
		}

		#region IComponent implementation

		public string ComponentType {
			get {
				return "Speed";
			}
		}

		#endregion
	}
}

