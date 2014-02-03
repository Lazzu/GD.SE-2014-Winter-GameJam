using System;

namespace TimeIsBroken.ECS.Components
{
	public class ShieldComponent : IComponent
	{
		public float Shield {
			get;
			set;
		}

		#region IComponent implementation

		public string ComponentType {
			get {
				return "Shield";
			}
		}


		public IComponent Clone ()
		{
			return new ShieldComponent (){
				Shield = Shield
			};
		}
		#endregion
	}
}

