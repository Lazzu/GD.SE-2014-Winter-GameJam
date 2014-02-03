using System;
using System.Collections.Generic;
using ESTD.ECS.Components;

namespace ESTD.ECS.Entities
{
	public class Entity
	{
		public Dictionary<string, IComponent> Components {
			get;
			protected set;
		}

		public bool Enabled {
			get;
			set;
		}

		public Entity ()
		{
			Components = new Dictionary<string, IComponent> ();
			Enabled = true;
		}

		public void AddComponent(IComponent component)
		{
			// Do nothing if component already exists in this entity
			if (Components.ContainsKey (component.ComponentType))
				return;

			Components.Add (component.ComponentType, component);
		}
	}
}

