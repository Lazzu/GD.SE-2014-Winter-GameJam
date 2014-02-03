using System;
using ESTD.Graphics.Sprites;

namespace ESTD.ECS.Components
{
	public class SpriteComponent : IComponent
	{
		public Sprite Sprite {
			get;
			set;
		}

		public SpriteComponent ()
		{
		}

		#region IComponent implementation

		public string ComponentType {
			get {
				return "Sprite";
			}
		}

		#endregion
	}
}

