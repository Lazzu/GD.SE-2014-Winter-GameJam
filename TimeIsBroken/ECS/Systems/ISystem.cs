using System;
using TimeIsBroken.ECS.Entities;

namespace TimeIsBroken.ECS.Systems
{
	public interface ISystem
	{

		bool Fits(Entity e);
		void Update(Entity e, double deltaTime);
		void Render (Entity e, double deltaTime);
		void Initialize(Entity e);
	}
}

