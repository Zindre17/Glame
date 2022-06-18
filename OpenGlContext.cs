using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace Glame;

public static class OpenGlContext
{
    private static GL? gl;

    public static void SetGlContext(IWindow window)
    {
        gl = GL.GetApi(window);
    }

    public static GL Gl => gl ?? throw new InvalidOperationException("Context not set yet");
}
