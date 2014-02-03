using System;

namespace ESTD.Graphics.Models.MeshGenerators
{
	public interface IMeshGenerator<T> where T : struct
	{
		Mesh<T> Generate();
		Mesh<T> Generate(Mesh<T> mesh);
	}
}

