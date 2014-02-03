using System;
using OpenTK;

namespace TimeIsBroken.ECS.Components
{
	public class PositionComponent : IComponent
	{
		public Vector3 Position {
			get;
			set;
		}

		public PositionComponent ()
		{
		}

		#region IComponent implementation

		public string ComponentType {
			get {
				return "Position";
			}
		}

		public IComponent Clone ()
		{
			return new PositionComponent(){
				Position = Position
			};
		}

		#endregion
	}
}

