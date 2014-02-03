using System;

namespace TimeIsBroken.ECS.Components
{
	public interface IComponent
	{
		string ComponentType {
			get;
		}

		IComponent Clone ();
	}
}

