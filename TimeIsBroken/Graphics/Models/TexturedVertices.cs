using System;
using OpenTK;
using System.Runtime.InteropServices;

namespace TimeIsBroken.Graphics.Models
{
	[StructLayout(LayoutKind.Sequential)]
	public struct TexturedVertices
	{
		public Vector2 Position;
		public Vector2 TexCoords;
	}
}

