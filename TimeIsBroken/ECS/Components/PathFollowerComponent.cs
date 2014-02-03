using System;
using OpenTK;

namespace TimeIsBroken.ECS.Components
{
	public class PathFollowerComponent : IComponent
	{
		Random random;

		int targetIndex;
		public int TargetIndex {
			get {
				return targetIndex;
			}
			set {
				targetIndex = value;

			}
		}

		Vector3 offset;

		public Vector3 TargetPoint {
			get {
				return Vector3.Zero;//GameLevelManager.Current.Path [TargetIndex] + offset;
			}
		}

		public int PathID {
			get;
			set;
		}

		public PathFollowerComponent ()
		{
			random = new Random ();
			offset = new Vector3 ((float)(random.NextDouble () - 0.5) * 40, (float)(random.NextDouble () - 0.5) * 40, 0);
		}

		#region IComponent implementation

		public string ComponentType {
			get {
				return "PathFollower";
			}
		}


		public IComponent Clone ()
		{
			return new PathFollowerComponent () {
				TargetIndex = targetIndex,
				PathID = PathID
			};
		}
		#endregion
	}
}

