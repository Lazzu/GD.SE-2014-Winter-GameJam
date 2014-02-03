using System;
using System.Runtime.InteropServices;
using OpenTK;

namespace TimeIsBroken.Graphics.Sprites
{
	[StructLayout(LayoutKind.Sequential)]
	public struct SpriteRenderingData
	{
		public Vector3 Position;
		public Vector2 Size;
		public Vector4 Texcoord;
		public float Rotation;
		public SpriteBlendingMode BlendMode;
		public static int SizeInBytes {
			get {
				return (Vector2.SizeInBytes * 3) + sizeof(float);
			}
		}
	}
}

