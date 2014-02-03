using System;
using TimeIsBroken.Graphics.Sprites;

namespace TimeIsBroken.ECS.Components
{
	public class ProjectileComponent : IComponent
	{
		public bool RemoveOnImpact {
			get;
			set;
		}

		public Sprite ImpactAnimation {
			get;
			set;
		}

		#region IComponent implementation

		public string ComponentType {
			get {
				return "Projectile";
			}
		}


		public IComponent Clone ()
		{
			return new ProjectileComponent (){
				RemoveOnImpact = RemoveOnImpact,
				ImpactAnimation = ImpactAnimation
			};
		}
		#endregion
	}
}

