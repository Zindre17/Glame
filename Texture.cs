using Silk.NET.OpenGL;
using static Glame.OpenGlContext;

namespace Glame;

public class Texture : IDisposable
{
    private readonly uint id;
    private readonly GLEnum target = GLEnum.Texture2D;

    unsafe public Texture(byte[] textureData, uint width, uint height)
    {
        id = Gl.GenTexture();
        Bind();

        Gl.TexParameterI(target, GLEnum.TextureMinFilter, (int)GLEnum.Nearest);
        Gl.TexParameterI(target, GLEnum.TextureMagFilter, (int)GLEnum.Nearest);
        Gl.TexParameterI(target, GLEnum.TextureWrapS, (int)GLEnum.ClampToEdge);
        Gl.TexParameterI(target, GLEnum.TextureWrapT, (int)GLEnum.ClampToEdge);

        fixed (void* data = textureData)
        {
            Gl.TexImage2D(target, 0, (int)GLEnum.Rgba8, width, height, 0, GLEnum.Rgba, GLEnum.UnsignedByte, data);
        }
    }

    public void Bind(uint slot = 0)
    {
        Gl.ActiveTexture((GLEnum)((int)GLEnum.Texture0 + slot));
        Gl.BindTexture(target, id);
    }

    public void Unbind()
    {
        Gl.BindTexture(target, 0);
    }

    public void Dispose()
    {
        Gl.DeleteTexture(id);
    }
}
