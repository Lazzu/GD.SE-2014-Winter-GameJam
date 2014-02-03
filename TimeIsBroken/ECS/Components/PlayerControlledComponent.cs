using System;

namespace TimeIsBroken.ECS.Components
{
	public class PlayerControlledComponent : IComponent
	{
		#region IComponent implementation

		public string ComponentType {
			get {
				return "PlayerControlled";
			}
		}


		public IComponent Clone ()
		{
			return new PlayerControlledComponent ();
		}
		#endregion

	}
}

