using System;

namespace TimeIsBroken.ECS.Components
{
	public class HealthComponent : IComponent
	{
		public float Health {
			get;
			set;
		}

		#region IComponent implementation

		public string ComponentType {
			get {
				return "Health";
			}
		}


		public IComponent Clone ()
		{
			return new HealthComponent (){
				Health = Health
			};
		}
		#endregion
	}
}

