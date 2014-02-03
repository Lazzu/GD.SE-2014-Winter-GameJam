using System;
using System.Collections.Generic;
using TimeIsBroken.ECS;

namespace TimeIsBroken.TimeMechanics
{
	public class GameTimeFrame
	{
		List<EntityFrameData> Entities = new List<EntityFrameData>();

		public GameTimeFrame ()
		{
			foreach (var Entity in ECSEngine.AllEntities) 
			{
				var ef = new EntityFrameData () {
					Entity = Entity
				};

				foreach (var component in Entity.Components) {
					ef.Components.Add (component.Key, component.Value.Clone ());
				}

				Entities.Add(ef);
			}
		}
	}
}

