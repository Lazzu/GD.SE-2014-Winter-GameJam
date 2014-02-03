using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace ESTD.Graphics.Textures
{
	public class Texture
	{
		public int GLName {
			get;
			protected set;
		}

		public string Name {
			get;
			protected set;
		}

		public string File {
			get;
			protected set;
		}

		public Vector2 Size {
			get;
			set;
		}

		public float Width {
			get {
				return Size.X;
			}
		}

		public float Height {
			get {
				return Size.Y;
			}
		}

		public Texture (int id, string file, string name, Vector2 size)
		{
			GLName = id;
			Name = name;
			File = file;
			Size = size;
		}

		public void Bind ()
		{
			GL.BindTexture (TextureTarget.Texture2D, GLName);
		}

		public void UnBind ()
		{
			GL.BindTexture (TextureTarget.Texture2D, 0);
		}

		public override string ToString ()
		{
			return string.Format ("[Texture: GLName={0}, Name={1}, File={2}, Size={3}, Width={4}, Height={5}]", GLName, Name, File, Size, Width, Height);
		}
	}
}

