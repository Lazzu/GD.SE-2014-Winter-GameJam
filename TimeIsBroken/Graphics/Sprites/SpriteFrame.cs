using System;
using OpenTK;

namespace ESTD.Graphics.Sprites
{
	public class SpriteFrame
	{
		/// <summary>
		/// Gets or sets the frame size on the texture.
		/// </summary>
		/// <value>The size.</value>
		public Vector2 Size {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the frame offset on the original texture.
		/// </summary>
		/// <value>The offset.</value>
		public Vector2 Offset {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the frame delay in seconds.
		/// </summary>
		/// <value>The delay.</value>
		public double Delay {
			get;
			set;
		}
	}
}

