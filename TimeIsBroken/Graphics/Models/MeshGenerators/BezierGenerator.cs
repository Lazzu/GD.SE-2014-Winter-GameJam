using System;
using OpenTK;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;

namespace TimeIsBroken.Graphics.Models.MeshGenerators
{
	public class BezierGenerator : IMeshGenerator<TexturedVertices>
	{
		readonly List<Vector4> points;

		public List<Vector4> Points {
			get {
				return points;
			}
		}

		public BezierGenerator ()
		{
			points = new List<Vector4> ();
			resolution = 100;
			Thickness = 100;
		}

		Vector2[] curvePoints;
		int totalPoints;

		public Vector2[] CurvePath {
			get {
				return curvePoints;
			}
		}

		int resolution;
		public int Resolution {
			get {
				return resolution;
			}
			set {
				var tmp = value;
				if (tmp != resolution) {
					resolution = tmp;
					if (resolution < 3)
						resolution = 3;
					Allocate ();
				}
			}
		}

		public int Thickness {
			get;
			set;
		}

		#region IMeshGenerator implementation

		void Allocate ()
		{
			totalPoints = points.Count / 2 * resolution + 1;
			curvePoints = new Vector2[totalPoints];
		}

		public Mesh<TexturedVertices> Generate (Mesh<TexturedVertices> mesh)
		{
			if(totalPoints < 2)
			{
				Allocate ();
			}

			// Calculate curve

			// Allocate all the memory we need for generating the curve

			float res = (float)resolution;
			int r = 0;

			Vector2 p0, p1, p2, p3;

			int index = 0;

			// Generate curve points
			for (int i = 0; i < points.Count - 1; i+=2) {
				p0.X = points [i].X;
				p0.Y = points [i].Y;
				p1.X = points [i].Z;
				p1.Y = points [i].W;
				p2.X = points [i + 1].X;
				p2.Y = points [i + 1].Y;
				p3.X = points [i + 1].Z;
				p3.Y = points [i + 1].W;

				if (i == 0) {
					CalculatePoint (0, ref p0, ref p1, ref p2, ref p3, out curvePoints[index++]);
				}

				for (r = 1; r <= resolution; r++) {
					CalculatePoint (r / res, ref p0, ref p1, ref p2, ref p3, out curvePoints[index++]);
				}
			}

			// Build mesh

			if (mesh.Vertices == null || mesh.Vertices.Length < totalPoints * 2) {
				mesh.Vertices = new TexturedVertices[totalPoints * 2];
			}


			Vector2 direction;
			Vector2 normal;

			float thick = (float)Thickness;
			float totalDistance;
			totalDistance = 0;
			index = 0;
			int j;

			for (j = 0; j < curvePoints.Length-1; j++) {
				direction = curvePoints [j+1] - curvePoints [j];
				totalDistance += direction.LengthFast;

				direction.NormalizeFast ();

				normal.X = direction.Y;
				normal.Y = -direction.X;

				// Faster than assigning whole vector
				mesh.Vertices [index].Position.X = normal.X * 0.5f * thick + curvePoints[j].X;
				mesh.Vertices [index].Position.Y = normal.Y * 0.5f * thick + curvePoints[j].Y;

				// Faster than assigning one component at a time
				mesh.Vertices [index].TexCoords.X = totalDistance * 0.005f;
				mesh.Vertices [index].TexCoords.Y = 0;

				index++;

				mesh.Vertices [index].Position.X = -normal.X * 0.5f * thick + curvePoints[j].X;
				mesh.Vertices [index].Position.Y = -normal.Y * 0.5f * thick + curvePoints[j].Y;

				mesh.Vertices [index].TexCoords.X = totalDistance * 0.005f;
				mesh.Vertices [index].TexCoords.Y = 1;

				index++;
			}

			direction = curvePoints [j-2] - curvePoints [j-1];
			totalDistance += direction.LengthFast;

			direction.NormalizeFast ();

			normal.X = direction.Y;
			normal.Y = -direction.X;

			//mesh.Vertices [index].Position = -normal * 0.5f * (float)Thickness + curvePoints[curvePoints.Count-1];
			mesh.Vertices [index].Position.X = -normal.X * 0.5f * thick + curvePoints[totalPoints-1].X;
			mesh.Vertices [index].Position.Y = -normal.Y * 0.5f * thick + curvePoints[totalPoints-1].Y;
			mesh.Vertices [index].TexCoords.X = totalDistance * 0.005f;
			mesh.Vertices [index].TexCoords.Y = 0;

			index++;

			mesh.Vertices [index].Position.X = normal.X * 0.5f * thick + curvePoints[totalPoints-1].X;
			mesh.Vertices [index].Position.Y = normal.Y * 0.5f * thick + curvePoints[totalPoints-1].Y;
			mesh.Vertices [index].TexCoords.X = totalDistance * 0.005f;
			mesh.Vertices [index].TexCoords.Y = 1;

			index++;

			mesh.VertexCount = index;

			mesh.Upload ();

			return mesh;
		}

		public Mesh<TexturedVertices> Generate ()
		{
			Mesh<TexturedVertices> mesh = new Mesh<TexturedVertices> ();

			mesh.Bind ();
			mesh.DescribeVertices (0, 2, VertexAttribPointerType.Float, false, BlittableValueType.StrideOf (mesh.Vertices), 0);
			mesh.DescribeVertices (1, 2, VertexAttribPointerType.Float, false, BlittableValueType.StrideOf (mesh.Vertices), Vector2.SizeInBytes);
			mesh.UnBind ();

			Generate (mesh);

			return mesh;
		}

		#endregion

		float u, uu, uuu, tt, ttt;

		/// <summary>
		/// Calculates coordinates of a point on cubic bezier curve at position t.
		/// </summary>
		/// <param name="t">T.</param>
		/// <param name="p0">P0.</param>
		/// <param name="v0">V0.</param>
		/// <param name="p1">P1.</param>
		/// <param name="v1">V1.</param>
		/// <param name="p">The end result coordinates.</param>
		public void CalculatePoint (float t, ref Vector2 p0, ref Vector2 v0, ref Vector2 p1, ref Vector2 v1, out Vector2 p)
		{
			u = 1 - t;
			uu = u * u;
			uuu = u * uu;

			tt = t * t;
			ttt = t * tt;

			p.X = uuu * p0.X +
				3 * uu * t * v0.X +
				3 * u * tt * v1.X +
				ttt * p1.X;

			p.Y = uuu * p0.Y +
				3 * uu * t * v0.Y +
				3 * u * tt * v1.Y +
				ttt * p1.Y;
		}
	}
}

