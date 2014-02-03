using System;
using OpenTK;

namespace TimeIsBroken.ECS.Components
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


		public IComponent Clone ()
		{
			return new VelocityComponent (){
				Velocity = Velocity
			};
		}
		#endregion


	}
}

