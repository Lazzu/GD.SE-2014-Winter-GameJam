using System;
using OpenTK.Graphics.OpenGL;
using System.Diagnostics;

namespace TimeIsBroken.Utilities
{
	public static class ErrorChecking
	{
		public static int CheckGLErrors ()
		{
			int errCount = 0;
			for(var currError = GL.GetError(); currError != ErrorCode.NoError; currError = GL.GetError())
			{
				// get call stack
				StackTrace stackTrace = new StackTrace();

				var frame = stackTrace.GetFrame (1);

				Console.WriteLine ("GL Error: {0}() : {1}", frame.GetMethod().Name, currError);
				++errCount;
			}

			return errCount;
		}
	}
}

