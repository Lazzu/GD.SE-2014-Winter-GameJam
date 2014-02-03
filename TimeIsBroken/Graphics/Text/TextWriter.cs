using System;
using ESTD.Graphics.Textures;
using System.Drawing;
using System.Drawing.Text;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Imaging;

namespace ESTD.Graphics.Text
{
	public class TextWriter
	{

		public void Write(string text, float maxWidth, Texture texture, Font font)
		{
			// Check that we actually have something in the string. Ignore if there is nothing.
			if (text.Length > 0)
			{
				// Variable for getting the text bitmap size
				SizeF size;

				// Split the text to the max width and get the size of the bitmap
				text = SplitToWidth (text, maxWidth, font, out size);

				// Set the text size
				//this.size = new Size ((int)size.Width + 1, (int)size.Height + 1);

				// Create new text bitmap with the size
				Bitmap textBitmap = new Bitmap((int)size.Width + 1, (int)size.Height + 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

				// Get Graphics from the text bitmap
				using(System.Drawing.Graphics gfx = System.Drawing.Graphics.FromImage(textBitmap))
				{
					// Set clear type hinting
					gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
					gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
					gfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

					// Black background
					gfx.Clear (Color.Transparent);

					// Draw the string on the bitmap
					gfx.DrawString(text, font, new SolidBrush(Color.White), new PointF(0,0));
				}

				GrayscaleToAlpha (textBitmap);

				// Get the texture data
				var textureData = textBitmap.LockBits(new Rectangle(0, 0, textBitmap.Width, textBitmap.Height),
					ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

				// Upload the texture data

				// Check if we have actually drawn something
				if (textureData != null) {

					// Bind the texture
					texture.Bind ();

					// Set the filters of the texture
					GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
					GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

					// Upload the texture
					GL.TexImage2D (TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, 
						textureData.Width, textureData.Height, 0,
						OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, textureData.Scan0);

					texture.Size = new OpenTK.Vector2 (textureData.Width, textureData.Height);

					// Unbind the texture
					texture.UnBind ();

					// Unlock the bits that had been locked by the caller of this method.
					textBitmap.UnlockBits (textureData);

					// Set the data ready for GC
					textureData = null;
				}
			}
		}

		void GrayscaleToAlpha(Bitmap image) {
			var lockData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			unsafe {
				// Pointer to the current pixel
				uint* pPixel = (uint*)lockData.Scan0;
				// Pointer value at which we terminate the loop (end of pixel data)
				var pLastPixel = pPixel + image.Width * image.Height;

				while (pPixel < pLastPixel) {
					// Get pixel data
					uint pixelValue = *pPixel;
					// Average RGBA
					uint brightness = ((pixelValue & 0xFF) + ((pixelValue >> 8) & 0xFF) + ((pixelValue >> 16) & 0xFF) + ((pixelValue >> 24) & 0xFF)) / 4;

					// Use brightness for alpha value, leave R, G, and B zero (black)
					pixelValue = 0xFFFFFFFF - ((0xFF - brightness) << 24);
					//pixelValue = 0xFFFFFFFF - pixelValue;

					// Copy back to image
					*pPixel = pixelValue;
					// Next pixel
					pPixel++;
				}

			}
			image.UnlockBits(lockData);
		}

		string SplitToWidth(string text, float maxWidth, Font textFont, out SizeF size)
		{
			size = GetTextSize(text, textFont);

			// Split the text from line breaks and spaces
			string[] words = text.Split (
				new string[]{
					"\n",
					"\r",
					" "
				}, 
				StringSplitOptions.RemoveEmptyEntries
			);

			// temp string for reconstructing the string
			string temp = "";

			// More temp variables :D
			string line = "";
			string lastLine = "";
			bool first = true;

			// Tuo foreach looppi toimii jostain syystä, en tiedä enää miksi. En mä sitä tuollain alunperin suunnitellu mutta tommonen siitä tuli.
			// Siinä on bugi. Jos ikkunan leveys on pienempi kuin ensimmäisen sanan leveys, ensimmäinen sana tulee jostakin syystä kahdesti.
			// TODO: Fiksaa bugi.

			// Loop through all words
			foreach (var word in words) {

				// Add the word on the line some way depending on if it is the first word
				if(first)
					line = word;
				else
					line += " " + word;

				// Get the size (width) of the line
				SizeF linesize = GetTextSize(line, textFont);

				// If line width is less than the max width
				if(linesize.Width < maxWidth)
				{
					// Continue with the line. Set the "last line" as the current line for next round
					lastLine = line;
				}
				else
				{
					// The line would be longer than the maximum allowed

					// Add the word or last round's line line to the temp string
					if (first)
						temp += word + "\n";
					else
						temp += lastLine + "\n";

					// Set the last line and current line as the current word.
					lastLine = word;
					line = word;
				}

				// We are no longer on the first word
				first = false;
			}

			// Add the last line
			temp += line;

			// Overwrite the old string with the new
			text = temp;

			text = text.Replace ("<br>", "\n");

			// Get the current size of the text
			size = GetTextSize(text, textFont);

			// Hurr durr a comment telling you the next line of code returns the text string to the caller. Talking about overcommenting?
			return text;
		}

		System.Drawing.Graphics gfx;
		SizeF GetTextSize (string text, Font font)
		{
			// If there is no Graphics object for getting the text size, create one
			if(gfx == null)
			{
				gfx = System.Drawing.Graphics.FromImage (new Bitmap (1,1));
				gfx.Clear(Color.Transparent);
				gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			}

			SizeF mSize = gfx.MeasureString(text, font);

			return mSize;
		}

	}
}

