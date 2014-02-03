using System;
using System.Collections.Generic;
using TimeIsBroken.Graphics.Textures;

namespace TimeIsBroken.Graphics.Sprites
{
	public class SpriteDrawLayer
	{
		public readonly Dictionary<Texture, List<SpriteRenderingData>> DrawQueue = new Dictionary<Texture, List<SpriteRenderingData>>();
		public readonly Dictionary<Texture, int> DrawQueueCounters = new Dictionary<Texture, int>();
	}
}

