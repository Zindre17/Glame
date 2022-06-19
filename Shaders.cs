using System.Text;
using Silk.NET.OpenGL;
using static Glame.OpenGlContext;

namespace Glame;
public class Shaders : IDisposable
{
    readonly uint program;
    private readonly Dictionary<string, int> uniformLocations = new();

    public Shaders(string filepath)
    {
        var shaders = FindShaders(filepath);

        program = Gl.CreateProgram();
        var vert = Gl.CreateShader(ShaderType.VertexShader);
        var frag = Gl.CreateShader(ShaderType.FragmentShader);
        Gl.ShaderSource(vert, shaders[0]);
        Gl.ShaderSource(frag, shaders[1]);
        Gl.CompileShader(vert);
        Gl.CompileShader(frag);

        var info = Gl.GetShaderInfoLog(vert);
        if (!string.IsNullOrEmpty(info))
        {
            Console.WriteLine(info);
        }
        info = Gl.GetShaderInfoLog(frag);
        if (!string.IsNullOrEmpty(info))
        {
            Console.WriteLine(info);
        }

        Gl.AttachShader(program, vert);
        Gl.AttachShader(program, frag);
        Gl.LinkProgram(program);

        Gl.GetProgram(program, GLEnum.LinkStatus, out var status);
        if (status is 0)
        {
            Console.WriteLine(Gl.GetProgramInfoLog(program));
        }

        // Cleanup of temporary resources
        Gl.DetachShader(program, vert);
        Gl.DetachShader(program, frag);
        Gl.DeleteShader(vert);
        Gl.DeleteShader(frag);
    }

    public void SetUniform(string name, int value)
    {
        Gl.Uniform1(GetUniformLocation(name), value);
    }

    private int GetUniformLocation(string name)
    {
        if (!uniformLocations.TryGetValue(name, out var location))
        {
            location = Gl.GetUniformLocation(program, name);
            if (location is -1)
            {
                throw new Exception("Did not find uniform");
            }
            uniformLocations[name] = location;
        };
        return location;
    }


    public void Bind()
    {
        Gl.UseProgram(program);
    }

    public void Unbind()
    {
        Gl.UseProgram(0);
    }

    public void Dispose()
    {
        Gl.DeleteProgram(program);
    }

    private const string ShaderStart = "#shader";
    private const string VertexShader = "vertex";
    private const string FragmentShader = "fragment";

    private static string[] FindShaders(string filepath)
    {
        var sourceBuilders = new StringBuilder[2] { new(), new() };
        var mode = -1;

        var file = File.ReadLines(filepath);
        foreach (var line in file)
        {
            if (line.Contains(ShaderStart))
            {
                if (line.Contains(VertexShader))
                {
                    mode = 0;
                }
                else if (line.Contains(FragmentShader))
                {
                    mode = 1;
                }
            }
            else if (mode > -1)
            {
                sourceBuilders[mode].Append(line);
                sourceBuilders[mode].Append(Environment.NewLine);
            }
        }

        return sourceBuilders.Select(b => b.ToString()).ToArray();
    }
}
