using System;

namespace TimeIsBroken.ECS.Components
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


		public IComponent Clone ()
		{
			return new SpeedComponent (){
				Speed = Speed
			};
		}
		#endregion
	}
}

