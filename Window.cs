using Silk.NET.Input;
using Silk.NET.Maths;
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

    private static readonly float[] vertices2 = new float[16]{
        -0.5f, -0.5f, 0.0f, 1.0f,
        -0.5f, 0.5f, 0.0f, 0.0f,
        0.5f, 0.5f, 1.0f, 0.0f,
        0.5f, -0.5f, 1.0f, 1.0f
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
    private static VertexArray vao2 = null!;
    private static VertexArray va3 = null!;
    private static VertexArray va4 = null!;
    private static VertexBuffer<TexturedVertex> vb3 = null!;
    private static VertexBuffer<TexturedVertex> vb4 = null!;
    private static VertexBuffer<float> vb = null!;
    private static VertexBuffer<float> vb2 = null!;
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
        window.Center();
        var input = window.CreateInput();
        foreach (var keyboard in input.Keyboards)
        {
            keyboard.KeyDown += OnKeyPressed;
        }

        SetGlContext(window);

        vao = new();
        vao2 = new();

        vb = new(vertices);
        vb2 = new(vertices2);

        var layout = new VertexLayout<float>(2, 2);
        var layout2 = new VertexLayout<float>(2, 2);

        vao.AddBuffer(vb, layout);
        vao2.AddBuffer(vb2, layout2);

        var quad = new Quad(new(-1, -1), 2, 2, 0, new(), 1, 1);
        va3 = new();
        vb3 = new(quad.Vertices);
        va3.AddBuffer(vb3, quad.Layout);

        var quad2 = new Quad(new(-0.5f, -0.5f), 1, 1, 0, new(1, 0), -1, 1);
        va4 = new();
        vb4 = new(QuadBatcher.Batch(quad, quad2));
        va4.AddBuffer(vb4, quad2.Layout);

        ib = new(QuadBatcher.GetIndices(2));

        shaders = new("Basic.shader");
        shaders.Bind();
        // shaders.SetUniform("textureSlot", 0);

        texture = new(textureData, 2, 2);
        texture.Bind();

        vao.Unbind();
        vb.Unbind();
        vao2.Unbind();
        vb2.Unbind();
        va3.Unbind();
        vb3.Unbind();
        va4.Unbind();
        vb4.Unbind();
    }

    private static void OnKeyPressed(IKeyboard arg1, Key arg2, int arg3)
    {

    }

    private static unsafe void OnRender(double obj)
    {
        renderer.Clear();

        // renderer.Draw(vao, ib, shaders);
        // renderer.Draw(vao2, ib, shaders);
        // renderer.Draw(va3, ib, shaders);
        renderer.Draw(va4, ib, shaders);
    }

    private static void OnClose()
    {
        vao.Dispose();
        vao2.Dispose();
        va3.Dispose();
        va4.Dispose();
        vb.Dispose();
        vb2.Dispose();
        vb3.Dispose();
        vb4.Dispose();
        ib.Dispose();
        shaders.Dispose();
    }

}
