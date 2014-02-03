using System;
using OpenTK;

namespace TimeIsBroken.ECS.Components
{
	public class BoundingBoxComponent : IComponent
	{
		public string Group {
			get;
			set;
		}

		public string Type {
			get;
			set;
		}

		public Vector4 Box {
			get;
			set;
		}

		public Vector2 Offset {
			get;
			set;
		}

		#region IComponent implementation

		public string ComponentType {
			get {
				// For example BoundingBox.PlayerHittable.Projectile
				return String.Format("BoundingBox.{0}.{1}", Group, Type);
			}
		}


		public IComponent Clone ()
		{
			return new BoundingBoxComponent (){
				Group = Group,
				Type = Type,
				Box = Box,
				Offset = Offset
			};
		}
		#endregion
	}
}

