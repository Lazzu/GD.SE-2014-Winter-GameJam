using System;

namespace TimeIsBroken.ECS.Components
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


		public IComponent Clone ()
		{
			return new RotationComponent (){
				Rotation = Rotation
			};
		}
		#endregion
	}
}

