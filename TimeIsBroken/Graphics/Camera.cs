using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace ESTD.Graphics
{
	public class Camera
	{
		public Matrix4 Projection;
		public Matrix4 View;

		bool perspective;

		GameWindow gameWindow;

		public bool Perspective {
			get {
				return perspective;
			}
		}

		public bool Orthogonal {
			get {
				return !perspective;
			}
		}

		public float zNear {
			get;
			set;
		}

		public float zFar {
			get;
			set;
		}

		public float Fovy {
			get;
			set;
		}

		public Camera (GameWindow gw)
		{
			zFar = 100.0f;
			zNear = 0.0001f;
			Fovy = (float)Math.PI / 4;
			gameWindow = gw;
			gw.RenderFrame += HandleRenderFrame;
			gw.Resize += HandleResize;
		}

		void HandleResize (object sender, EventArgs e)
		{
			Console.WriteLine ("Resize event");
			if(perspective)
			{
				Create3D (new Vector2 (gameWindow.Width, gameWindow.Height));
			}
			else
			{
				Create2D (new Vector2 (gameWindow.Width, gameWindow.Height));
			}
			GL.Viewport (0, 0, gameWindow.Width, gameWindow.Height);
		}

		void HandleRenderFrame (object sender, FrameEventArgs e)
		{
			// TODO: Add view updating
		}

		public void Create2D(Vector2 size)
		{
			size = size * 0.5f;
			Projection = Matrix4.CreateOrthographicOffCenter (-size.X, size.X, size.Y, -size.Y, -zFar, zFar);
			View = Matrix4.Identity;
			perspective = false;
		}

		public void Create3D(Vector2 size)
		{
			float aspect = size.X / size.Y;
			Projection = Matrix4.CreatePerspectiveFieldOfView (Fovy, aspect, zNear, zFar);
		}
	}
}

