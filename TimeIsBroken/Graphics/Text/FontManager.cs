using System;
using System.Drawing.Text;
using System.Drawing;
using System.IO;

namespace ESTD.Graphics.Text
{
	public static class FontManager
	{
		static readonly PrivateFontCollection fontCollection = new PrivateFontCollection();

		public static FontFamily[] FontFamilies
		{
			get
			{
				return fontCollection.Families;
			}
		}

		public static void Load(string path)
		{
			if (File.Exists (path)) 
			{
				string ext = Path.GetExtension (path);

				// Supports ttf and otf fonts
				if(ext == ".ttf" || ext == ".otf") 
				{
					Console.WriteLine ("Found font: {0}", path);
					fontCollection.AddFontFile (path);
				}
			}
			else if(Directory.Exists(path)) 
			{
				// Recursively load the found path
				foreach (var file in Directory.GetFileSystemEntries (path)) 
				{
					Load (file);
				}
			}
		}

		public static FontFamily Get(string family)
		{
			foreach (FontFamily font in FontFamilies)
			{
				if (font.Name.ToLower().Equals(family.ToLower()))
				{
					Console.WriteLine ("Got font {0}", family);
					return font;
				}
			}
			return FontFamily.GenericMonospace;
		}
	}
}

