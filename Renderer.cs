using Silk.NET.OpenGL;
using static Glame.OpenGlContext;

namespace Glame;

public class Renderer
{
    public void Clear()
    {
        Gl.Clear(ClearBufferMask.ColorBufferBit);
    }

    unsafe public void Draw(VertexArray va, IndexBuffer ib, Shaders shaders)
    {
        va.Bind();
        ib.Bind();
        shaders.Bind();
        Gl.DrawElements(GLEnum.Triangles, ib.Count, GLEnum.UnsignedInt, null);
    }
}
