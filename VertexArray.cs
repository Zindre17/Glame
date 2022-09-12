using Silk.NET.OpenGL;
using static Glame.OpenGlContext;

namespace Glame;

public class VertexArray : IDisposable
{
    private readonly uint id;

    public VertexArray()
    {
        id = Gl.GenVertexArray();
        Bind();
    }

    unsafe public void AddBuffer<TBuffer, TLayout>(VertexBuffer<TBuffer> buffer, VertexLayout<TLayout> layout)
        where TBuffer : unmanaged
        where TLayout : unmanaged
    {
        Bind();
        buffer.Bind();

        var stride = layout.Stride;
        var glType = GetGlType(typeof(TLayout));

        var index = 0u;
        var offset = 0u;
        foreach (var attribute in layout.Attributes)
        {
            Gl.EnableVertexAttribArray(index);
            Gl.VertexAttribPointer(
                index++,
                attribute.Count,
                glType,
                attribute.Normalized,
                stride,
                (void*)offset
            );

            offset += (uint)(attribute.Count * sizeof(TLayout));
        }
    }

    public void Bind()
    {
        Gl.BindVertexArray(id);
    }

    public void Unbind()
    {
        Gl.BindVertexArray(0);
    }

    public void Dispose()
    {
        Gl.DeleteVertexArray(id);
    }

    private static GLEnum GetGlType(Type type)
    {
        if (type == typeof(float))
        {
            return GLEnum.Float;
        }
        else if (type == typeof(int))
        {
            return GLEnum.Int;
        }
        else if (type == typeof(uint))
        {
            return GLEnum.UnsignedInt;
        }
        else if (type == typeof(byte))
        {
            return GLEnum.Byte;
        }
        else
        {
            throw new ArgumentException("Unsupported type");
        }
    }
}
