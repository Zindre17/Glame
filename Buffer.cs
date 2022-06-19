using Silk.NET.OpenGL;
using static Glame.OpenGlContext;

namespace Glame;

public abstract class Buffer<T> : IDisposable where T : unmanaged
{
    protected uint id;
    protected GLEnum target;

    public Buffer(GLEnum target, T[]? data = null)
    {
        this.target = target;

        id = Gl.CreateBuffer();
        Bind();

        if (data is not null)
        {
            FeedData(data);
        }
    }

    unsafe public virtual void FeedData(Span<T> data)
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
