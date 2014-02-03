using System;
using System.Collections.Generic;
using OpenTK;

namespace TimeIsBroken.TimeMechanics
{
	public static class FrameRecorder
	{
		static Queue<EntityFrameData> PastFrames = new Queue<EntityFrameData>();

		public static int MaxFrames {
			get;
			set;
		}

		public static void Initialize (GameWindow gw)
		{
			MaxFrames = 1000 / 60 * 2;

			gw.RenderFrame += HandleRenderFrame;
		}

		static void HandleRenderFrame (object sender, FrameEventArgs e)
		{
			PastFrames.Enqueue (new EntityFrameData());

			if (PastFrames.Count > MaxFrames)
				PastFrames.Dequeue ();

		}
	}
}

