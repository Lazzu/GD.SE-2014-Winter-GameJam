using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Imaging;

namespace ESTD.Graphics.Textures
{
	public class TextureManager
	{
		readonly Dictionary<string, Texture> textures;

		public TextureManager ()
		{
			textures = new Dictionary<string, Texture> ();
		}

		public Texture GenerateTexture(string name)
		{
			int id = GL.GenTexture ();

			if(id <= 0)
			{
				throw new ApplicationException ("Could not generate texture!");
			}

			Texture t = new Texture (id, "", name, Vector2.Zero);

			// Bind the texture object so it can be used (written in)
			t.Bind ();

			// Set texture parameters for rendering
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

			// Unbind the texture
			t.UnBind ();

			textures.Add (name, t);

			return t;
		}

		/// <summary>
		/// Loads a texture in GPU memory.
		/// </summary>
		/// <returns>The texture object or null on error.</returns>
		/// <param name="name">Texture unique name.</param>
		/// <param name="file">Texture file path.</param>
		public Texture LoadTexture(string name, string file)
		{
			// Check if file does exist
			if(!File.Exists(file))
			{
				Console.WriteLine ("File not found: {0}", file);
				return null;
			}

			// Check if texture has already been loaded
			if(textures.ContainsKey(name))
			{
				return textures [name];
			}

			// Load and return the texture
			using(Bitmap bitmap = new Bitmap(file))
			{
				//GL.PixelStore (PixelStoreParameter.UnpackAlignment, 1);

				// Generate texture GL name
				int id = GL.GenTexture ();

				// Get the size of the bitmap
				Rectangle size = new Rectangle (0, 0, bitmap.Width, bitmap.Height);

				// Create new Texture object
				Texture t = new Texture(id, file, name, new Vector2(size.Right, size.Bottom));

				// Bind the texture object so it can be used (written in)
				t.Bind ();

				// Get the bitmap data
				BitmapData bitmapData = 
					bitmap.LockBits(
						size,
						ImageLockMode.ReadOnly,
						System.Drawing.Imaging.PixelFormat.Format32bppArgb
					);

				//glPixelStorei(GL_UNPACK_ALIGNMENT, 1);



				// Use the bitmap as texture
				GL.TexImage2D (TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bitmapData.Width, bitmapData.Height, 0,
					OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bitmapData.Scan0);

				// Release the bitmap
				bitmap.UnlockBits(bitmapData);

				// Set texture parameters for generating mipmaps
				/*GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

				// Generate mipmaps for the texture
				GL.GenerateMipmap(TextureTarget.Texture2D);*/

				// Set texture parameters for rendering
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

				// Unbind the texture
				t.UnBind ();

				// Add the texture
				textures.Add(name, t);

				Console.WriteLine ("Loaded texture '{1}' with GLName {2} from file {0}", file, name, id);

				return t;
			}

			//return false;
		}

		/// <summary>
		/// Release the specified texture.
		/// </summary>
		/// <param name="texture">Texture.</param>
		public void Release(Texture texture)
		{
			GL.DeleteTexture (texture.GLName);
			textures.Remove (texture.Name);
		}

		/// <summary>
		/// Releases all textures from GPU memory.
		/// </summary>
		public void ReleaseAll()
		{
			foreach (var texture in textures) {
				GL.DeleteTexture (texture.Value.GLName);
			}
			textures.Clear ();
		}
	}
}

