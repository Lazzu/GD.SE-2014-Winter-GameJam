using System;
using OpenTK;
using System.Collections.Generic;
using TimeIsBroken.Graphics.Textures;
using OpenTK.Graphics.OpenGL;

namespace TimeIsBroken.Graphics.Shaders
{
	class ShaderProgram
	{
		Dictionary<string, int> uniforms = new Dictionary<string, int>();

		public int GLName {
			get;
			protected set;
		}

		public List<Shader> Shaders {
			get;
			protected set;
		}

		public ShaderProgram ()
		{
			GLName = GL.CreateProgram ();
			Shaders = new List<Shader> ();
		}

		public void AttachShader(Shader shader)
		{
			GL.AttachShader (GLName, shader.GLName);
			Shaders.Add (shader);
		}

		public void Link ()
		{
			GL.LinkProgram (GLName);
		}

		public void Use ()
		{
			GL.UseProgram (GLName);
		}

		public void Disable ()
		{
			GL.UseProgram (0);
		}

		public void FindUniform(string uniform)
		{
			if(!uniforms.ContainsKey(uniform))
			{
				int location = GL.GetUniformLocation (GLName, uniform);
				uniforms.Add (uniform, location);
			}
		}

		#region SendUniform methods
		/*public void SendUniform(string uniform, double data)
		{
			FindUniform (uniform);
			GL.Uniform1(uniforms[uniform], data);
		}*/
		public void SendUniform(string uniform, float data)
		{
			FindUniform (uniform);
			GL.Uniform1(uniforms[uniform], data);
		}
		public void SendUniform(string uniform, int data)
		{
			FindUniform (uniform);
			GL.Uniform1(uniforms[uniform], data);
		}
		public void SendUniform(string uniform, uint data)
		{
			FindUniform (uniform);
			GL.Uniform1(uniforms[uniform], data);
		}
		public void SendUniform(string uniform, short data)
		{
			FindUniform (uniform);
			GL.Uniform1(uniforms[uniform], data);
		}
		public void SendUniform(string uniform, byte data)
		{
			FindUniform (uniform);
			GL.Uniform1(uniforms[uniform], data);
		}
		public void SendUniform(string uniform, ref Vector2 data)
		{
			FindUniform (uniform);
			GL.Uniform2(uniforms[uniform], ref data);
		}
		public void SendUniform(string uniform, ref Vector3 data)
		{
			FindUniform (uniform);
			GL.Uniform3(uniforms[uniform], ref data);
		}
		public void SendUniform(string uniform, ref Vector4 data)
		{
			FindUniform (uniform);
			GL.Uniform4(uniforms[uniform], ref data);
		}
		public void SendUniform(string uniform, ref Matrix4 data)
		{
			FindUniform (uniform);
			GL.UniformMatrix4(uniforms[uniform], false, ref data);
		}
		public void SendUniform(string uniform, bool normalize, ref Matrix4 data)
		{
			FindUniform (uniform);
			GL.UniformMatrix4(uniforms[uniform], normalize, ref data);
		}
		public void SendUniform(string uniform, float[] data)
		{
			FindUniform (uniform);
			int u = uniforms [uniform];

			for(int i = 0; i < data.Length; i++)
				GL.Uniform1(u + i, data[i]);
		}
		/*public void SendUniform(string uniform, double[] data)
		{
			FindUniform (uniform);
			int u = uniforms [uniform];

			for(int i = 0; i < data.Length; i++)
				GL.Uniform1(u + i, data[i]);
		}*/
		public void SendUniform(string uniform, int[] data)
		{
			FindUniform (uniform);
			int u = uniforms [uniform];

			for(int i = 0; i < data.Length; i++)
				GL.Uniform1(u + i, data[i]);
		}
		public void SendUniform(string uniform, uint[] data)
		{
			FindUniform (uniform);
			int u = uniforms [uniform];

			for(int i = 0; i < data.Length; i++)
				GL.Uniform1(u + i, data[i]);
		}
		public void SendUniform(string uniform, short[] data)
		{
			FindUniform (uniform);
			int u = uniforms [uniform];

			for(int i = 0; i < data.Length; i++)
				GL.Uniform1(u + i, data[i]);
		}
		public void SendUniform(string uniform, byte[] data)
		{
			FindUniform (uniform);
			int u = uniforms [uniform];

			for(int i = 0; i < data.Length; i++)
				GL.Uniform1(u + i, data[i]);
		}
		/*public void SendUniformBlock<T> (string uniform, int size, T[] data, BufferUsageHint hint) where T : struct
		{
			GL.BindBuffer(BufferTarget.UniformBuffer, uniformBuffers[uniform]);
			GL.BufferData (BufferTarget.UniformBuffer, new IntPtr(size), data, hint);
			GL.BindBuffer(BufferTarget.UniformBuffer, 0);
		}*/
		// TODO: Implement all GL uniform methods
		#endregion

	}
}

