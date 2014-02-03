using System;
using OpenTK;
using System.Collections.Generic;
using TimeIsBroken.Graphics.Textures;
using TimeIsBroken.Graphics.Shaders;
using OpenTK.Graphics.OpenGL;
using TimeIsBroken.Utilities;

namespace TimeIsBroken.Graphics.Sprites
{
	public static class SpriteRenderer
	{
		// Layers for sorting sprites from bottom to top
		static readonly HashSet<int> availableLayers = new HashSet<int>();
		static readonly Dictionary<int, SpriteDrawLayer> layers = new Dictionary<int, SpriteDrawLayer> ();

		public static int TotalRendered {
			get;
			set;
		}

		public static int TotalLayers {
			get;
			set;
		}

		public static int TotalTextureGroups {
			get;
			set;
		}

		static ShaderProgram shader;

		static int VBO;

		static Vector2[] Vertices = new[] {
			new Vector2(0,0),
			new Vector2(0,1),
			new Vector2(1,0),
			new Vector2(1,1)
		};

		public static int MinLayer {
			get;
			set;
		}

		public static int MaxLayer {
			get;
			set;
		}

		public static void Initialize(GameWindow gw)
		{
			gw.Load += HandleLoad;
			MinLayer = -10;
			MaxLayer = 10;
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
			// Get layer by using the Z of the sprite position
			int layer = (int)position.Z;

			// Check layer depth restrictions
			if (layer < MinLayer || layer > MaxLayer)
				return;

			// If the layer does not yet exist, add it
			if (!availableLayers.Contains (layer)) {
				availableLayers.Add (layer);
				layers.Add (layer, new SpriteDrawLayer ());
			}

			// Get the drawing queue from the layer
			var drawQueue = layers [layer].DrawQueue;
			var drawQueueCounters = layers [layer].DrawQueueCounters;

			// Check if queue contains the texture we want to draw
			if(!drawQueue.ContainsKey(sprite.Texture))
			{
				// Add the new texture to the queue
				drawQueue.Add (sprite.Texture, new List<SpriteRenderingData> (100));
				drawQueueCounters.Add (sprite.Texture, 0);
			}

			// Get the current sprite frame
			SpriteFrame frame = sprite.Frames [sprite.CurrentFrame];

			// Calculate sprite offset and size in texture coordinates
			Vector2 offset = frame.Offset;
			offset.X /= sprite.Texture.Size.X;
			offset.Y /= sprite.Texture.Size.Y;

			Vector2 size = frame.Size;
			size.X /= sprite.Texture.Size.X;
			size.Y /= sprite.Texture.Size.Y;

			// Get current sprite count in the queue
			int count = drawQueueCounters [sprite.Texture];

			// Check if pool has room
			if(drawQueue[sprite.Texture].Count > count)
			{
				// Pool has room, set the sprite details
				SpriteRenderingData data = drawQueue [sprite.Texture] [count];

				data.Position = position;
				data.Texcoord = new Vector4 (offset.X, offset.Y, size.X, size.Y);
				data.Size = frame.Size * sprite.Scale;
				data.Rotation = rotation;
				data.BlendMode = sprite.BlendingMode;

				drawQueue [sprite.Texture] [count] = data;
			}
			else
			{
				// No room in pool. Add new sprite in the pool
				drawQueue [sprite.Texture].Add (new SpriteRenderingData () {
					Position = position,
					Texcoord = new Vector4(frame.Offset.X, frame.Offset.Y, size.X, size.Y),
					Size = frame.Size * sprite.Scale,
					Rotation = rotation,
					BlendMode = sprite.BlendingMode
				});
			}

			// Increment the counter
			drawQueueCounters [sprite.Texture]++;


		}

		public static void Render(Camera camera)
		{
			TotalRendered = 0;
			TotalLayers = 0;
			TotalTextureGroups = 0;

			// Use the sprite shader
			shader.Use ();

			// Make sure blending is enabled
			GL.Enable (EnableCap.Blend);

			// Bind the sprite VBO
			GL.BindBuffer (BufferTarget.ArrayBuffer, VBO);

			// Go through all layers
			for(int layer = MinLayer; layer < MaxLayer+1; layer++) {

				// Check if current layer exists
				if (!availableLayers.Contains (layer))
					continue;

				TotalLayers++;

				// Get the draw queue from the layer
				var drawQueue = layers [layer].DrawQueue;
				var drawQueueCounters = layers [layer].DrawQueueCounters;

				// Go through the queues
				foreach (var item in drawQueue) {
					Texture texture = item.Key;
					var list = item.Value;

					TotalTextureGroups++;

					texture.Bind ();

					// Draw all sprites in the queue
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

						switch (data.BlendMode) {
						case SpriteBlendingMode.Additive:
							GL.BlendFunc (BlendingFactorSrc.One, BlendingFactorDest.One);
							break;
						default:
							GL.BlendFunc (BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
							break;
						}

						ErrorChecking.CheckGLErrors ();

						GL.DrawArrays (PrimitiveType.TriangleStrip, 0, Vertices.Length);

						TotalRendered++;

					}

					drawQueueCounters [texture] = 0;

					texture.UnBind ();

				}

			}

			GL.BindBuffer (BufferTarget.ArrayBuffer, 0);
			shader.Disable ();
		}

	}
}

