﻿using System;
using OpenTK;
using ESTD.ECS.Components;
using ESTD.GameLevels;

namespace ESTD.ECS.Systems
{
	public class PathFollowerSystem : ISystem
	{
		float threshold;
		public float Threshold {
			get {
				return threshold;
			}
			set {
				threshold = value;
				if (threshold < 1)
					threshold = 1;
			}
		}

		public PathFollowerSystem ()
		{
			threshold = 2000.0f;
		}

		#region ISystem implementation

		public bool Fits (ESTD.ECS.Entities.Entity e)
		{
			return (
			    e.Components.ContainsKey ("PathFollower")
				&& e.Components.ContainsKey ("Position")
				&& e.Components.ContainsKey ("Rotation")
				&& e.Components.ContainsKey ("Speed")
			);
		}

		public void Update (ESTD.ECS.Entities.Entity e, double deltaTime)
		{
			PathFollowerComponent pfc = (PathFollowerComponent)e.Components ["PathFollower"];

			Vector3 position = ((PositionComponent)e.Components ["Position"]).Position;

			// Vector from position to target
			Vector3 direction = new Vector3(pfc.TargetPoint) - position;

			// Distance from position to target
			float distance = direction.LengthSquared;

			if(distance < threshold)
			{
				pfc.TargetIndex = GameLevelManager.Current.GetNextPointOnPath (pfc.TargetIndex);
				if (pfc.TargetIndex < 0) {
					ECSEngine.RemoveEntity (e);
					return;
				}
			}

			((RotationComponent)e.Components ["Rotation"]).Rotation = (float)Math.Atan2 (direction.Y, direction.X);

			float speed = ((SpeedComponent)e.Components ["Speed"]).Speed;

			direction.NormalizeFast ();

			position += direction * speed * (float)deltaTime;

			((PositionComponent)e.Components ["Position"]).Position = position;
		}

		public void Render (ESTD.ECS.Entities.Entity e, double deltaTime)
		{
			// Nothing to do here
		}

		#endregion
	}
}

