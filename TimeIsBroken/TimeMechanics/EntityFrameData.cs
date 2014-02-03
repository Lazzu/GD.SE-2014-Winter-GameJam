using System;
using System.Collections.Generic;
using TimeIsBroken.ECS.Entities;
using TimeIsBroken.ECS.Components;

namespace TimeIsBroken.TimeMechanics
{
	class EntityFrameData
	{
		public Entity Entity {
			get;
			set;
		}

		public Dictionary<string, IComponent> Components {
			get;
			set;
		}
	}
}

