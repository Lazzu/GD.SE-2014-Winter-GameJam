using System;
using OpenTK;
using System.Collections.Generic;
using ESTD.Graphics.Textures;
using ESTD.Graphics.Shaders;
using OpenTK.Graphics.OpenGL;
using ESTD.Utilities;

namespace ESTD.Graphics.Sprites
{
	public static class SpriteRenderer
	{
		static readonly Dictionary<Texture, List<SpriteRenderingData>> drawQueue = new Dictionary<Texture, List<SpriteRenderingData>>();
		static readonly Dictionary<Texture, int> drawQueueCounters = new Dictionary<Texture, int>();

		static ShaderProgram shader;

		static int VBO;

		static Vector2[] Vertices = new[] {
			new Vector2(0,0),
			new Vector2(0,1),
			new Vector2(1,0),
			new Vector2(1,1)
		};

		public static void Initialize(GameWindow gw)
		{
			gw.Load += HandleLoad;
		}

		static void HandleLoad (object sender, EventArgs e)
		{
			shader = new ShaderProgram ();
			shader.AttachShader (new Shader (ShaderType.FragmentShader, "./Content/Shaders/Sprite.frag"));
			shader.AttachShader (new Shader (ShaderType.VertexShader, "./Content/Shaders/Sprite.vert"));
			shader.Link ();

			VBO = GL.GenBuffer ();

			GL.BindBuffer (BufferTarget.ArrayBuffer, VBO);

			// Describe the vertices
			GL.EnableVertexAttribArray(0);
			GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, BlittableValueType.StrideOf(Vertices), 0);

			// Send the vertices
			GL.BufferData (BufferTarget.ArrayBuffer, (IntPtr)(Vertices.Length * Vector2.SizeInBytes), Vertices, BufferUsageHint.StaticDraw);

			GL.BindBuffer (BufferTarget.ArrayBuffer, 0);

		}

		public static void RenderSprite(Sprite sprite, Vector3 position, float rotation)
		{
			if(!drawQueue.ContainsKey(sprite.Texture))
			{
				drawQueue.Add (sprite.Texture, new List<SpriteRenderingData> (100));
				drawQueueCounters.Add (sprite.Texture, 0);
			}

			SpriteFrame frame = sprite.Frames [sprite.CurrentFrame];

			Vector2 size = new Vector2(frame.Size.X / sprite.Texture.Size.X, frame.Size.Y / sprite.Texture.Size.Y) + frame.Offset;

			int count = drawQueueCounters [sprite.Texture];

			if(drawQueue[sprite.Texture].Count > count)
			{
				SpriteRenderingData data = drawQueue [sprite.Texture] [count];

				data.Position = position;
				data.Texcoord = new Vector4 (frame.Offset.X, frame.Offset.Y, size.X, size.Y);
				data.Size = frame.Size * sprite.Scale;
				data.Rotation = rotation;

				drawQueue [sprite.Texture] [count] = data;
			}
			else
			{
				drawQueue [sprite.Texture].Add (new SpriteRenderingData () {
					Position = position,
					Texcoord = new Vector4(frame.Offset.X, frame.Offset.Y, size.X, size.Y),
					Size = frame.Size * sprite.Scale,
					Rotation = rotation
				});
			}


			drawQueueCounters [sprite.Texture]++;
		}

		public static void Render(Camera camera)
		{
			shader.Use ();
			GL.Enable (EnableCap.Blend);
			GL.BindBuffer (BufferTarget.ArrayBuffer, VBO);
			foreach (var item in drawQueue) {
				Texture texture = item.Key;
				var list = item.Value;

				texture.Bind ();

				for (int i = 0; i < drawQueueCounters [texture]; i++) {

					SpriteRenderingData data = list [i];

					Matrix4 mvp = 
						Matrix4.CreateScale(data.Size.X, data.Size.Y, 1)
						* Matrix4.CreateRotationZ(data.Rotation)
						* Matrix4.CreateTranslation (data.Position)
						* camera.View
						* camera.Projection
					;

					Vector2 offset = data.Texcoord.Xy;
					Vector2 size = data.Texcoord.Zw;

					shader.SendUniform ("Size", ref size);
					shader.SendUniform ("Offset", ref offset);
					shader.SendUniform ("mMVP", ref mvp);

					ErrorChecking.CheckGLErrors ();

					GL.DrawArrays (PrimitiveType.TriangleStrip, 0, Vertices.Length);

				}

				drawQueueCounters [texture] = 0;

				texture.UnBind ();

			}
			GL.BindBuffer (BufferTarget.ArrayBuffer, 0);
			shader.Disable ();
		}

	}
}

