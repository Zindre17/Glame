
using System.Collections.ObjectModel;

namespace Glame;

public class Sprite
{
    public Sprite(Texture texture, Position position, Size size)
    {
        Texture = texture;
        Position = position;
        Size = size;
        sprites.Add(this);
    }

    private static List<Sprite> sprites = new();

    internal static ReadOnlyCollection<Sprite> AllVisible => new(sprites.Where(s => !s.IsHidden).ToArray());

    public Texture Texture { get; set; }
    public Position Position { get; set; }
    public Size Size { get; set; }
    public bool IsHidden { get; set; }

    public void Translate(float x, float y)
        => (Position) = (Position.Translate(x, y));

    public void Trash()
    {
        sprites.Remove(this);
    }

}

public struct Size
{
    public Size(float height, float width) => (Height, Width) = (height, width);
    public float Height { get; init; }
    public float Width { get; init; }

    public Size Scale(float factor)
    {
        return new Size(Height * factor, Width * factor);
    }
}