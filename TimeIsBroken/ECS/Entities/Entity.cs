using System;
using System.Collections.Generic;
using TimeIsBroken.ECS.Components;

namespace TimeIsBroken.ECS.Entities
{
	public class Entity
	{
		public Guid ID {
			get;
			private set;
		}

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
			ID = Guid.NewGuid ();
		}

		public void AddComponent(IComponent component)
		{
			// Do nothing if component already exists in this entity
			if (Components.ContainsKey (component.ComponentType))
				return;

			Components.Add (component.ComponentType, component);
		}

		public override int GetHashCode ()
		{
			return ID.GetHashCode ();
		}

		public Entity Clone()
		{
			Dictionary<string, IComponent> comp = new Dictionary<string, IComponent> ();

			foreach (var component in Components) {
				comp.Add (component.Key, component.Value.Clone ());
			}

			return new Entity () {
				ID = ID,
				Components = comp,
				Enabled = Enabled
			};
		}
	}
}

