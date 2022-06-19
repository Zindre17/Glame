using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using static Glame.OpenGlContext;

namespace Glame;

public class MyWindow
{
    private static IWindow window = null!;

    private static readonly float[] vertices = new float[16]{
        -1f, -1f, 0.0f, 0.0f,
        -1f, 1f, 0.0f, 1.0f,
        1f, 1f, 1.0f, 1.0f,
        1f, -1f, 1.0f, 0.0f
    };

    private static readonly uint[] indices = new uint[6]{
        0, 1, 2,
        0, 2, 3
    };

    private static readonly byte[] textureData = new byte[]{
        0xFF, 0,0xFF, 0xFF,
        0xFF, 0, 0, 0xFF,
        0, 0, 0xFF, 0xFF,
        0, 0xFF, 0, 0xFF
    };

    private static IndexBuffer ib = null!;
    private static Shaders shaders = null!;
    private static VertexArray vao = null!;
    private static VertexBuffer<float> vb = null!;
    private static Texture texture = null!;
    private static readonly Renderer renderer = new();

    public static void Init()
    {
        var options = WindowOptions.Default;
        options.Size = new Vector2D<int>(800, 600);
        options.Title = "Glame";
        window = Window.Create(options);

        window.Load += OnLoad;
        window.Render += OnRender;
        window.Closing += OnClose;
        window.Resize += OnResize;
    }

    private static void OnResize(Vector2D<int> newScreenSize)
    {
        Gl.Viewport(0, 0, (uint)newScreenSize.X, (uint)newScreenSize.Y);
    }

    public static void Run() => window.Run();

    private static unsafe void OnLoad()
    {
        var input = window.CreateInput();
        foreach (var keyboard in input.Keyboards)
        {
            keyboard.KeyDown += OnKeyPressed;
        }

        SetGlContext(window);

        vao = new();
        vb = new(vertices);

        var layout = new VertexLayout<float>(2, 2);

        vao.AddBuffer(vb, layout);

        ib = new(indices);

        shaders = new("Basic.shader");
        shaders.Bind();
        shaders.SetUniform("textureSlot", 0);

        texture = new(textureData, 2, 2);
        texture.Bind();

        vao.Unbind();
        vb.Unbind();
    }

    private static void OnKeyPressed(IKeyboard arg1, Key arg2, int arg3)
    {

    }

    private static unsafe void OnRender(double obj)
    {
        renderer.Clear();

        renderer.Draw(vao, ib, shaders);
    }

    private static void OnClose()
    {
        vao.Dispose();
        vb.Dispose();
        ib.Dispose();
        shaders.Dispose();
    }

}
