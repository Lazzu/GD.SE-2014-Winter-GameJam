using System;
using System.Collections.Generic;
using ESTD.ECS.Systems;
using ESTD.ECS.Entities;
using OpenTK;
using System.Threading.Tasks;
using System.Threading;

namespace ESTD.ECS
{
	public static class ECSEngine
	{
		static readonly Dictionary<ISystem, List<Entity>> entities = new Dictionary<ISystem, List<Entity>> ();

		static readonly List<Entity> removeQueue = new List<Entity> ();

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

		public static void AddEntity(Entity e)
		{
			bool added = false;

			foreach (var item in entities) {
				ISystem system = item.Key;

				if(system.Fits(e))
				{
					entities [system].Add (e);
					added = true;
				}
			}

			if(added)
			{
				EntityCount += 1;
			}
		}

		public static void RemoveEntity(Entity e)
		{
			lock (removeQueue) {
				removeQueue.Add (e);
			}
		}

		static ManualResetEvent[] handles;

		static void HandleUpdateFrame (object sender, FrameEventArgs e)
		{
			/*bool resized = false;
			if(handles == null || handles.Length < entities.Count)
			{
				handles = new ManualResetEvent[(int)(entities.Count * 1.5f) + 50];
				resized = true;
			}

			int index = 0;
			foreach (var item in entities)
			{
				handles[index] = new ManualResetEvent(false);
				var currentHandle = handles[index];
				Action wrappedAction = () => { 
					try { 
						ISystem system = item.Key;
						var entitylist = item.Value;

						for (int i = 0; i < entitylist.Count; i++) {
							var entity = entitylist [i];
							if (entity.Enabled) {
								system.Update (entity, e.Time);
							}
						}
					} 
					finally { currentHandle.Set(); } 
				};
				ThreadPool.QueueUserWorkItem(x => wrappedAction());
				index++;
			}

			if (resized) {
				for (int i = index; i < handles.Length; i++) {
					(handles [i] = new ManualResetEvent (false)).Set ();
				}
			}

			WaitHandle.WaitAll(handles);*/

			foreach (var item in entities) {
				ISystem system = item.Key;
				var entitylist = item.Value;

				for (int i = 0; i < entitylist.Count; i++) {
					var entity = entitylist [i];
					if (entity.Enabled) {
						system.Update (entity, e.Time);
					}
				}
			}

			foreach (var removed in removeQueue) {
				bool removedone = false;
				foreach (var item in entities) {
					ISystem system = item.Key;
					removedone = entities [system].Remove (removed);
				}
				if(removedone)
				{
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

