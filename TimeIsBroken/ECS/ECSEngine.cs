using System;
using System.Collections.Generic;
using TimeIsBroken.ECS.Systems;
using TimeIsBroken.ECS.Entities;
using OpenTK;

namespace TimeIsBroken.ECS
{
	public static class ECSEngine
	{
		static readonly Dictionary<ISystem, List<Entity>> entities = new Dictionary<ISystem, List<Entity>> ();

		static readonly List<Entity> removeQueue = new List<Entity> ();

		public static readonly List<Entity> AllEntities = new List<Entity> ();

		static readonly HashSet<string> collisionTypes = new HashSet<string>();
		static readonly Dictionary<string, List<Entity>> collisionGroups = new Dictionary<string, List<Entity>> ();

		public static int EntityCount {
			get;
			set;
		}

		public static void Initialize(GameWindow gw)
		{
			EntityCount = 0;
			gw.UpdateFrame += HandleUpdateFrame;
			gw.RenderFrame += HandleRenderFrame;
		}

		public static void AddSystem(ISystem s)
		{
			if (entities.ContainsKey (s))
				return;

			entities.Add (s, new List<Entity> ());
		}

		public static void AddCollisionGroup (string group)
		{
			collisionTypes.Add (group);

			if (!collisionGroups.ContainsKey (group))
				collisionGroups.Add (group, new List<Entity> ());
		}

		public static void AddEntity(Entity e)
		{
			bool added = false;

			// Go through all systems and add the entity to them if it fits
			foreach (var item in entities) {
				ISystem system = item.Key;

				if(system.Fits(e))
				{
					system.Initialize (e);
					entities [system].Add (e);
					added = true;
				}
			}

			// Check if the entity belongs in some collision group
			foreach (var collisionGroup in collisionTypes) {
				if(e.Components.ContainsKey(collisionGroup))
				{
					// It does! Add it to the collision group
					collisionGroups [collisionGroup].Add (e);

					added = true;
				}
			}

			// Check if the entity was added in any system or in any collision group
			if(added)
			{
				AllEntities.Add (e);
				EntityCount += 1;
			}
		}

		public static void RemoveEntity(Entity e)
		{
			lock (removeQueue) {
				removeQueue.Add (e);
			}
		}

		public static List<Entity> GetCollidables(string group)
		{
			if(collisionGroups.ContainsKey(group))
			{
				return collisionGroups [group];
			}
			return null;
		}

		static void HandleUpdateFrame (object sender, FrameEventArgs e)
		{
			// Update all entities using the systems the entities belong to
			foreach (var item in entities) {
				ISystem system = item.Key;
				var entitylist = item.Value;
				for (int i = 0; i < entitylist.Count; i++) {
					var entity = entitylist [i];
					if (entity.Enabled) {
						system.Update (entity, GameTimer.Delta);
					}
				}
				// No going through collision groups here, because that's what the systems are for.
			}

			// Remove the entities marked to be removed
			foreach (var removed in removeQueue) {
				bool removedone = false;
				// Remove from systems
				foreach (var item in entities) {
					ISystem system = item.Key;
					if (entities [system].Remove (removed)) 
					{
						removedone = true;
					}
				}
				// Remove from collision groups
				foreach (var item in collisionGroups) {
					string group = item.Key;
					if (collisionGroups [group].Remove (removed)) 
					{
						removedone = true;
					}
				}
				// If the entity was removed from somewhere, subtract the count
				if(removedone)
				{
					AllEntities.Remove (removed);
					EntityCount -= 1;
				}
			}
			removeQueue.Clear ();
		}

		static void HandleRenderFrame (object sender, FrameEventArgs e)
		{
			foreach (var item in entities) {
				ISystem system = item.Key;
				var entitylist = item.Value;

				for (int i = 0; i < entitylist.Count; i++) {
					var entity = entitylist [i];
					if (entity.Enabled) {
						system.Render (entity, e.Time);
					}
				}

			}
		}
	}
}

