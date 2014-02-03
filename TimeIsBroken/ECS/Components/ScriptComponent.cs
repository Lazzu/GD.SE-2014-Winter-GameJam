using System;
using TimeIsBroken.Scripts;

namespace TimeIsBroken.ECS.Components
{
	public class ScriptComponent : IComponent
	{

		public IScript Script {
			get;
			set;
		}

		#region IComponent implementation

		public string ComponentType {
			get {
				return "Script";
			}
		}


		public IComponent Clone ()
		{
			return new ScriptComponent (){
				Script = Script.Clone()
			};
		}
		#endregion
	}
}

