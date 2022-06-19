using Silk.NET.OpenGL;

namespace Glame;

public class VertexBuffer<T> : Buffer<T> where T : unmanaged
{
    public VertexBuffer(T[]? data = null)
        : base(GLEnum.ArrayBuffer, data) { }
}
