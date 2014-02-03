using System;
using System.Runtime.InteropServices;
using OpenTK;

namespace ESTD.Graphics.Models
{
	[StructLayout(LayoutKind.Sequential)]
	public struct ColoredVertices
	{
		public Vector2 Position;
		public Vector4 Color;
	}
}

