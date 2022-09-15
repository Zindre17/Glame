using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.Windowing;
using static Glame.OpenGlContext;

namespace Glame;

public interface IGame
{
    void Run();
    void Pause();
}

public interface ITile
{

}

public delegate void KeyPressed(Key key);
public delegate void KeyReleased(Key key);

public sealed class Game
{
    public string Title { get; init; } = "Glame";
    public (int Width, int Height) Resolution { get; init; } = (800, 600);
    public int FramePerSecond { get; init; } = 60;

    public Game()
    {
        if (window is not null)
        {
            throw new InvalidOperationException("Only one game can be instantiated.");
        }

        Init();
    }

    public void Run() => window?.Run();

    private static IWindow? window;
    private readonly Renderer renderer = new();
    private readonly VertexArray vertexArray = new();

    private void Init()
    {
        var options = WindowOptions.Default;
        options.Size = new Vector2D<int>(Resolution.Width, Resolution.Height);
        options.Title = Title;
        window = Window.Create(options);

        window.Load += OnLoad;
        window.Render += OnRender;
        window.Closing += OnClose;
        window.Resize += OnResize;
    }

    private void OnResize(Vector2D<int> newScreenSize)
    {
        Gl.Viewport(0, 0, (uint)newScreenSize.X, (uint)newScreenSize.Y);
    }

    private void OnClose()
    {
    }

    private void OnRender(double obj)
    {
        renderer.Clear();
        // renderer.Draw();
    }

    private void OnLoad()
    {
        window!.Center();

        SetGlContext(window!);


    }
}

