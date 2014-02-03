using System;

namespace TimeIsBroken.ECS.Components
{
	public class DamageComponent : IComponent
	{
		public float Damage {
			get;
			set;
		}

		#region IComponent implementation

		public string ComponentType {
			get {
				return "Damage";
			}
		}


		public IComponent Clone ()
		{
			return new DamageComponent (){
				Damage = Damage
			};
		}
		#endregion
	}
}

