using System;
using OpenTK;
using System.Collections.Generic;
using TimeIsBroken.Graphics.Textures;
using OpenTK.Graphics.OpenGL;
using System.IO;

namespace TimeIsBroken.Graphics.Shaders
{
	public class Shader
	{
		public int GLName {
			get;
			protected set;
		}

		public ShaderType ShaderType {
			get;
			set;
		}

		public Shader (ShaderType type)
		{
			ShaderType = type;
			GLName = GL.CreateShader (type);
		}

		public Shader (ShaderType type, string file) : this(type)
		{
			Load (file);
		}

		public void LoadSource(string source, string name)
		{
			GL.ShaderSource (GLName, source);
			GL.CompileShader (GLName);

			string info;

			GL.GetShaderInfoLog (GLName, out info);

			if(!string.IsNullOrEmpty(info))
			{
				Console.WriteLine ("Shader {0} Info Log", name);
				Console.WriteLine ("---------------");
				Console.WriteLine (info);
			}
		}

		public void Load (string file)
		{
			string source;

			using(FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				using(StreamReader stream = new StreamReader(fileStream))
				{
					source = stream.ReadToEnd ();
				}
			}

			LoadSource (source, file);
		}
	}
}

