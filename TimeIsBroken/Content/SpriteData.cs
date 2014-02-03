using System;
using TimeIsBroken.Graphics.Sprites;
using OpenTK;

namespace TimeIsBroken.Content
{
	public static class SpriteData
	{
		public static SpriteFrame[] PlayerShip = new SpriteFrame[] {
			new SpriteFrame() {
				Size = new Vector2(64,64),
				Offset = Vector2.Zero
			}
		};

		public static SpriteFrame[] BlueProjectile = new SpriteFrame[] {
			new SpriteFrame() {
				Size = new Vector2(10,26),
				Offset = new Vector2(210,566)
			}
		};

		public static SpriteFrame[] OrangeProjectile = new SpriteFrame[] {
			new SpriteFrame() {
				Size = new Vector2(10,26),
				Offset = new Vector2(198,566)
			}
		};

		public static SpriteFrame[] PurpleProjectile = new SpriteFrame[] {
			new SpriteFrame() {
				Size = new Vector2(10,26),
				Offset = new Vector2(222,566)
			}
		};

		public static SpriteFrame[] BlueBallProjectile = new SpriteFrame[] {
			new SpriteFrame() {
				Size = new Vector2(12,12),
				Offset = new Vector2(304,580),
				Delay = 0.1
			},
			new SpriteFrame() {
				Size = new Vector2(12,12),
				Offset = new Vector2(304,566),
				Delay = 0.1
			},
			new SpriteFrame() {
				Size = new Vector2(12,12),
				Offset = new Vector2(290,580),
				Delay = 0.1
			},
			new SpriteFrame() {
				Size = new Vector2(12,12),
				Offset = new Vector2(290,566),
				Delay = 0.1
			}
		};

		public static SpriteFrame[] Explosion1 = new SpriteFrame[] {
			new SpriteFrame() {
				Size = new Vector2(64,64),
				Offset = new Vector2(958,774),
				Delay = 0.025
			},
			new SpriteFrame() {
				Size = new Vector2(64,64),
				Offset = new Vector2(892,766),
				Delay = 0.025
			},
			new SpriteFrame() {
				Size = new Vector2(64,64),
				Offset = new Vector2(958,708),
				Delay = 0.025
			},
			new SpriteFrame() {
				Size = new Vector2(64,64),
				Offset = new Vector2(826,760),
				Delay = 0.025
			},
			new SpriteFrame() {
				Size = new Vector2(64,64),
				Offset = new Vector2(892,700),
				Delay = 0.025
			},
			new SpriteFrame() {
				Size = new Vector2(64,64),
				Offset = new Vector2(958,642),
				Delay = 0.025
			},
			new SpriteFrame() {
				Size = new Vector2(64,64),
				Offset = new Vector2(826,694),
				Delay = 0.025
			},
			new SpriteFrame() {
				Size = new Vector2(64,64),
				Offset = new Vector2(892,634),
				Delay = 0.025
			},
			new SpriteFrame() {
				Size = new Vector2(64,64),
				Offset = new Vector2(826,628),
				Delay = 0.025
			},
			new SpriteFrame() {
				Size = new Vector2(64,64),
				Offset = new Vector2(760,708),
				Delay = 0.025
			},
			new SpriteFrame() {
				Size = new Vector2(64,64),
				Offset = new Vector2(760,642),
				Delay = 0.025
			},
			new SpriteFrame() {
				Size = new Vector2(64,64),
				Offset = new Vector2(698,808),
				Delay = 0.025
			},
			new SpriteFrame() {
				Size = new Vector2(64,64),
				Offset = new Vector2(694,708),
				Delay = 0.025
			},
			new SpriteFrame() {
				Size = new Vector2(64,64),
				Offset = new Vector2(694,642),
				Delay = 0.025
			},
			new SpriteFrame() {
				Size = new Vector2(64,64),
				Offset = new Vector2(906,568),
				Delay = 0.025
			},
			new SpriteFrame() {
				Size = new Vector2(64,64),
				Offset = new Vector2(840,562),
				Delay = 0.025
			},
			new SpriteFrame() {
				Size = new Vector2(64,64),
				Offset = new Vector2(774,562),
				Delay = 0.025
			},
			new SpriteFrame() {
				Size = new Vector2(64,64),
				Offset = new Vector2(708,562),
				Delay = 0.025
			},
			new SpriteFrame() {
				Size = new Vector2(64,64),
				Offset = new Vector2(642,938),
				Delay = 0.025
			},
			new SpriteFrame() {
				Size = new Vector2(64,64),
				Offset = new Vector2(632,872),
				Delay = 0.025
			},
		};

		public static SpriteFrame[] Asteroids1 = new SpriteFrame[] {
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 0, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 1, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 2, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 3, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 4, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 5, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 6, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 7, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 8, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 9, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 10, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 11, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 12, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 13, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 14, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 15, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 16, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 17, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 18, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 19, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 20, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 21, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 22, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 23, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 24, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 25, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 26, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 27, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 28, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 29, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 30, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 31, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 32, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 33, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 34, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 35, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 36, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 37, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 38, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 39, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 40, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 41, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 42, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 43, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 44, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 45, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 46, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 47, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 48, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 49, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 50, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 51, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 52, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 53, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 54, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 55, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 56, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 57, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 58, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 59, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 60, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 61, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 62, 0),
				Delay = 0.05
			},
			new SpriteFrame() {
				Size = new Vector2(128,128),
				Offset = new Vector2(8192 / 64 * 63, 0),
				Delay = 0.05
			},
		};
	}
}

