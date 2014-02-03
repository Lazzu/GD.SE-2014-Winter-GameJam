using System;

namespace TimeIsBroken
{
	public class Program
	{
		[STAThread]
		public static void Main(string[] args)
		{

			using(var game = new Game())
			{
				game.Run ();
			}

		}
	}
}

