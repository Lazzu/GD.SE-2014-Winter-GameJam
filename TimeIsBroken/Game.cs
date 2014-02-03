using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

using TimeIsBroken.ECS;
using TimeIsBroken.ECS.Components;
using TimeIsBroken.ECS.Entities;
using TimeIsBroken.ECS.Systems;
using TimeIsBroken.Graphics.Sprites;
using TimeIsBroken.Graphics.Textures;
using TimeIsBroken.Graphics;
using TimeIsBroken.Input;
using TimeIsBroken.Content;
using TimeIsBroken.Graphics.Text;
using System.Drawing;
using TimeIsBroken.TimeMechanics;

namespace TimeIsBroken
{
	public class Game : GameWindow
	{
		public Game () : base(1280, 720, OpenTK.Graphics.GraphicsMode.Default, "Time Is Broken", 
			GameWindowFlags.Default, DisplayDevice.Default, 2,0,OpenTK.Graphics.GraphicsContextFlags.Default)
		{
		}

		Texture FPSTextTexture;
		Sprite FPSTextSprite;
		Font fpsFont;

		PlayerShip playerShip;

		TextureManager tManager;

		TextWriter textWriter;

		Camera camera;

		// Spritemanager handles sprite animation
		SpriteManager spriteManager;

		public static SpriteManager Sprites {
			get;
			set;
		}

		protected override void OnLoad (EventArgs e)
		{
			camera = new Camera (this);
			SpriteRenderer.Initialize (this);
			ECSEngine.Initialize (this);
			spriteManager = new SpriteManager (this);
			FrameRecorder.Initialize (this);

			FontManager.Load ("Content/Fonts");

			Sprites = spriteManager;

			camera.Create2D (new Vector2 (Width, Height));

			base.OnLoad (e);

			tManager = new TextureManager ();

			ECSEngine.AddSystem (new MotionSystem ());
			ECSEngine.AddSystem (new SpriteSystem ());
			ECSEngine.AddSystem (new LifeSpanSystem ());
			ECSEngine.AddSystem (new ScriptSystem ());
			ECSEngine.AddSystem (new PlayerControlsSystem (new KeyboardListener(this)){
				MaxBounds = new Vector2(Width / 2, Height / 2),
				MinBounds = new Vector2(-Width / 2,-Height / 2)
			});
			ECSEngine.AddSystem (new ProjectileSystem ("HitsPlayer"));
			ECSEngine.AddSystem (new ProjectileSystem ("HitsEnemies"));

			GameTimer.GlitchDelay = 1;
			GameTimer.GlitchFrequency = 30;

			spriteManager.AddSprite ("Player.Ship", new Sprite (SpriteData.PlayerShip){
				Texture = tManager.LoadTexture("Ship.Player", "Content/Textures/starship.png")
			});
			spriteManager.AddSprite ("Projectile.BlueBall", new Sprite (SpriteData.BlueBallProjectile){
				Texture = tManager.LoadTexture("Explosions", "Content/Textures/explosions.png"),
				BlendingMode = SpriteBlendingMode.Additive
			});
			spriteManager.AddSprite ("Projectile.BlueProjectile", new Sprite (SpriteData.BlueProjectile){
				Texture = tManager.LoadTexture("Explosions", "Content/Textures/explosions.png"),
				BlendingMode = SpriteBlendingMode.Additive
			});
			spriteManager.AddSprite ("Projectile.OrangeProjectile", new Sprite (SpriteData.OrangeProjectile){
				Texture = tManager.LoadTexture("Explosions", "Content/Textures/explosions.png"),
				BlendingMode = SpriteBlendingMode.Additive
			});
			spriteManager.AddSprite ("Projectile.PurpleProjectile", new Sprite (SpriteData.PurpleProjectile){
				Texture = tManager.LoadTexture("Explosions", "Content/Textures/explosions.png"),
				BlendingMode = SpriteBlendingMode.Additive
			});
			spriteManager.AddSprite ("Explosions.1", new Sprite (SpriteData.Explosion1){
				Texture = tManager.LoadTexture("Explosions", "Content/Textures/explosions.png"),
				BlendingMode = SpriteBlendingMode.Additive
			});
			spriteManager.AddSprite ("Asteroids.1", new Sprite (SpriteData.Asteroids1){
				Texture = tManager.LoadTexture("Asteroids1", "Content/Textures/asteroid_strip64.png"),
				BlendingMode = SpriteBlendingMode.Normal
			});

			FPSTextTexture = tManager.GenerateTexture ("FPSText");

			textWriter = new TextWriter ();

			fpsFont = new Font ("Arial", 10);

			textWriter.Write ("Calculating FPS...", Width, FPSTextTexture, fpsFont);

			FPSTextSprite = new Sprite (new SpriteFrame[] {
				new SpriteFrame {Size = FPSTextTexture.Size}
			}){
				Texture = FPSTextTexture,
				BlendingMode = SpriteBlendingMode.Normal
			};

			playerShip = new PlayerShip (spriteManager.Sprites ["Player.Ship"]);

			ECSEngine.AddEntity (playerShip);

			GL.Enable (EnableCap.Blend);
			GL.Disable (EnableCap.DepthTest);

			GL.BlendFunc (BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
			GL.BlendEquation (BlendEquationMode.FuncAdd);

		}

		double totalTime = 0;
		Random r = new Random();

		double asteroidtime = 0;

		protected override void OnUpdateFrame (FrameEventArgs e)
		{
			GameTimer.Update (e.Time);
			totalTime += e.Time;

			/*if(totalTime > 0.1)
			{
				totalTime -= 0.1;

				for (int i = 0; i < 5; i++) {
					ECSEngine.AddEntity (
						new Explosion (
							spriteManager.Sprites ["Explosions.1"], 
							new Vector3 (
								(float)(r.NextDouble () - 0.5) * Width, 
								(float)(r.NextDouble () - 0.5) * Height, 
								5
							), 
							spriteManager.Sprites ["Explosions.1"].TotalAnimationTime
						)
					);
				}
			}*/

			asteroidtime += e.Time;

			if(asteroidtime > 1)
			{
				asteroidtime -= 1;

				ECSEngine.AddEntity (
					new AsteroidEntity (
						spriteManager.Sprites ["Asteroids.1"], 
						new Vector3 (
							(float)(r.NextDouble () - 0.5) * Width, 
							(float)-Height, 
							5
						),
						new Vector3(0, 100, 0),
						(float)(r.NextDouble () - 0.5),
						(float)(r.NextDouble () / 2) + 0.25f
					)
				);

			}

			base.OnUpdateFrame (e);
		}

		double fpsTime = 0;
		int fps = 0;
		protected override void OnRenderFrame (FrameEventArgs e)
		{
			fpsTime += e.Time;

			if(fpsTime > 1)
			{
				fpsTime -= 1;
				textWriter.Write (string.Format("FPS: {0}<br>Seconds until next time glitch: {1}<br>TS: {2} TG: {3} TL: {4}<br>Player HP: {5}", fps, (int)GameTimer.TimeUntilNextGlitch, SpriteRenderer.TotalRendered, SpriteRenderer.TotalTextureGroups, SpriteRenderer.TotalLayers, (int)playerShip.HitPoints), Width, FPSTextTexture, fpsFont);
				FPSTextSprite.Frames [0].Size = FPSTextTexture.Size;
				fps = 0;
			}

			fps++;

			GL.Clear (ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			FPSTextSprite.Render (new Vector3 (10 + FPSTextTexture.Size.X / 2 - Width / 2, 10 + FPSTextTexture.Size.Y / 2 - Height / 2, 10), 0);

			base.OnRenderFrame (e);

			SpriteRenderer.Render (camera);

			SwapBuffers ();
		}
	}
}

