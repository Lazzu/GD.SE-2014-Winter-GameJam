using System;
using TimeIsBroken.ECS.Components;
using OpenTK;
using TimeIsBroken.Scripts.Effects;

namespace TimeIsBroken.ECS.Entities
{
	public class MultiExplosionEntity : Entity
	{
		public MultiExplosionEntity (Vector3 position, float life)
		{
			this.AddComponent (new PositionComponent () { Position = position });
			this.AddComponent (new LifeSpanComponent () { Life = life });
			this.AddComponent (new ScriptComponent () { Script = new MultiExplosionScript (this) });
		}
	}
}

