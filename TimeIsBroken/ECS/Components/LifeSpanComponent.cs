using System;

namespace TimeIsBroken.ECS.Components
{
	public class LifeSpanComponent : IComponent
	{
		public double Life {
			get;
			set;
		}

		#region IComponent implementation

		public string ComponentType {
			get {
				return "LifeSpan";
			}
		}


		public IComponent Clone ()
		{
			return new LifeSpanComponent (){
				Life = Life
			};
		}
		#endregion


	}
}

