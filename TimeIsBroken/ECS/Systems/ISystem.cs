using System;
using ESTD.ECS.Entities;

namespace ESTD.ECS.Systems
{
	public interface ISystem
	{

		bool Fits(Entity e);
		void Update(Entity e, double deltaTime);
		void Render (Entity e, double deltaTime);

	}
}

