using System;
using OpenTK.Graphics.OpenGL;
using System.Runtime.InteropServices;
using OpenTK;
using ESTD.Utilities;

namespace ESTD.Graphics.Models
{
	public class Mesh<T> where T : struct
	{
		PrimitiveType type;
		public PrimitiveType Type {
			get {
				return type;
			}
			set {
				type = value;
			}
		}

		public bool Indexed {
			get;
			set;
		}

		int vbo;
		public int VertexBufferObject {
			get {
				return vbo;
			}
		}

		int ebo;
		public int ElementBufferObject {
			get {
				return ebo;
			}
		}

		int vao;
		public int VertexArrayObject {
			get {
				return vao;
			}
		}

		T[] vertices;
		public T[] Vertices {
			get {
				return vertices;
			}
			set {
				vertices = value;
				VertexCount = vertices.Length;
				verticesNeedsUploading = true;
			}
		}

		int vertexCount;
		public int VertexCount {
			get {
				return vertexCount;
			}
			set {
				vertexCount = value;
				verticesNeedsUploading = true;
			}
		}

		int[] indices;
		public int[] Indices {
			get {
				return indices;
			}
			set {
				indices = value;
				IndexCount = indices.Length;
				indicesNeedsUploading = true;
			}
		}

		int indexCount;
		public int IndexCount {
			get {
				return indexCount;
			}
			set {
				indexCount = value;
				indicesNeedsUploading = true;
			}
		}

		BufferUsageHint vertexUsageHint;
		public BufferUsageHint VertexUsageHint {
			get {
				return vertexUsageHint;
			}
			set {
				vertexUsageHint = value;
			}
		}

		BufferUsageHint indexUsageHint;
		public BufferUsageHint IndexUsageHint {
			get {
				return indexUsageHint;
			}
			set {
				indexUsageHint = value;
			}
		}

		bool verticesNeedsUploading = true;

		public bool VerticesNeedsUploading {
			get {
				return verticesNeedsUploading;
			}
			set {
				verticesNeedsUploading = value;
			}
		}

		bool indicesNeedsUploading = true;

		public bool IndicesNeedsUploading {
			get {
				return indicesNeedsUploading;
			}
			set {
				indicesNeedsUploading = value;
			}
		}

		public Mesh ()
		{
			Type = PrimitiveType.Points;
			vertexUsageHint = BufferUsageHint.StaticDraw;
			indexUsageHint = BufferUsageHint.StaticDraw;
		}

		public void Bind ()
		{
			if (vao < 1) {
				vao = GL.GenVertexArray ();
				vbo = GL.GenBuffer ();
				if (Indexed) {
					ebo = GL.GenBuffer ();
				}
			}

			GL.BindVertexArray (vao);
			GL.BindBuffer (BufferTarget.ArrayBuffer, vbo);
			if(Indexed) GL.BindBuffer (BufferTarget.ElementArrayBuffer, ebo);
		}

		public void UnBind ()
		{
			GL.BindVertexArray (0);
		}

		public void DescribeVertices(int index, int size, VertexAttribPointerType attribPointerType, bool normalized, int stride, int offset)
		{
			// Set the vbo settings
			GL.EnableVertexAttribArray(index);
			GL.VertexAttribPointer(index, size, attribPointerType, normalized, stride, offset);

			ErrorChecking.CheckGLErrors ();
		}

		public void Draw()
		{
			GL.BindVertexArray (vao);

			if (Indexed) {
				GL.DrawElements (type, IndexCount, DrawElementsType.UnsignedByte, IntPtr.Zero);
			} else {
				GL.DrawArrays (type, 0, VertexCount);
			}

			GL.BindVertexArray (0);

			ErrorChecking.CheckGLErrors ();
		}

		public void Upload ()
		{
			int size = Marshal.SizeOf (typeof(T));

			if (verticesNeedsUploading) {
				GL.BindBuffer (BufferTarget.ArrayBuffer, vbo);
				GL.BufferData (BufferTarget.ArrayBuffer, (IntPtr)(VertexCount * size), vertices, vertexUsageHint);
				GL.BindBuffer (BufferTarget.ArrayBuffer, 0);
			}

			if(Indexed && indicesNeedsUploading) {
				GL.BindBuffer (BufferTarget.ElementArrayBuffer, ebo);
				GL.BufferData (BufferTarget.ElementArrayBuffer, (IntPtr)(IndexCount * sizeof(int)), indices, indexUsageHint);
				GL.BindBuffer (BufferTarget.ElementArrayBuffer, 0);
			}

			indicesNeedsUploading = false;
			verticesNeedsUploading = false;

			ErrorChecking.CheckGLErrors ();
		}


	}
}

