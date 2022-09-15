using Silk.NET.OpenGL;
using static Glame.OpenGlContext;

namespace Glame;

public abstract class Buffer<T> : IDisposable where T : unmanaged
{
    protected uint id;
    protected GLEnum target;

    unsafe public Buffer(GLEnum target, T[]? data = null)
    {
        this.target = target;

        id = Gl.CreateBuffer();
        Bind();

        if (data is not null)
        {
            FeedData(data);
        }
    }

    private nuint currentLength = 0;

    unsafe public virtual void FeedData(Span<T> data, nint offset = 0)
    {
        var lengthOfData = (nuint)(sizeof(T) * data.Length);
        fixed (void* dataPointer = data)
        {
            if (((nuint)offset) + lengthOfData < currentLength)
            {
                Gl.BufferSubData(target, offset, lengthOfData, dataPointer);
            }
            else
            {
                Gl.BufferData(target, lengthOfData, dataPointer, GLEnum.StaticDraw);
            }
        }
        currentLength = lengthOfData;
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
