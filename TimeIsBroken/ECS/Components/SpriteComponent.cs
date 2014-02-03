using System;
using TimeIsBroken.Graphics.Sprites;

namespace TimeIsBroken.ECS.Components
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


		public IComponent Clone ()
		{
			return new SpriteComponent (){
				Sprite = Sprite
			};
		}
		#endregion
	}
}

