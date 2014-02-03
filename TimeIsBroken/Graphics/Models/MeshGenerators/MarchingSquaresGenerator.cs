using System;
using OpenTK;

namespace ESTD.Graphics.Models.MeshGenerators
{
	public class MarchingSquaresGenerator
	{
		public Vector2 Resolution {
			get;
			set;
		}

		public Vector2[] Circles {
			get;
			set;
		}

		public MarchingSquaresGenerator ()
		{
		}
	}
}

