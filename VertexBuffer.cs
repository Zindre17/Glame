using Silk.NET.OpenGL;
using static Glame.OpenGlContext;

namespace Glame;

public class VertexBuffer<T> : IDisposable where T : unmanaged
{
    private readonly uint id;
    private const GLEnum target = GLEnum.ArrayBuffer;

    public VertexBuffer(T[]? data = null)
    {
        id = Gl.CreateBuffer();
        Bind();

        if (data is not null)
        {
            FeedData(data);
        }
    }

    unsafe public void FeedData(Span<T> data)
    {
        fixed (void* dataPointer = data)
        {
            Gl.BufferData(target, (nuint)(sizeof(T) * data.Length), dataPointer, GLEnum.StaticDraw);
        }
    }

    public void Bind()
    {
        Gl.BindBuffer(target, id);
    }

    public void Unbind()
    {
        Gl.BindBuffer(target, 0);
    }

    public void Dispose()
    {
        Gl.DeleteBuffers(1, id);
    }
}
