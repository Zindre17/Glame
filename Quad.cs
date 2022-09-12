namespace Glame;

public class Quad
{
    private readonly TexturedVertex[] vertices = new TexturedVertex[4];

    public Quad(
        Position xy,
        float width,
        float height,
        int textureSlot,
        Position texturePosition,
        float textureWidth,
        float textureHeight
    )
    {
        // bottom left
        vertices[0] = new(xy, textureSlot, texturePosition);

        // bottom right
        vertices[1] = vertices[0].Translate(width, 0, textureWidth, 0);

        // top right
        vertices[2] = vertices[0].Translate(width, height, textureWidth, textureHeight);

        // top left
        vertices[3] = vertices[0].Translate(0, height, 0, textureHeight);
    }

    public TexturedVertex[] Vertices => vertices;

    public VertexLayout<float> Layout => new(2, 1, 2);
}

public struct TexturedVertex
{
    public TexturedVertex(Position position, float textureSlot, Position texturePosition)
        => (Position, TextureSlot, TexturePosition) = (position, textureSlot, texturePosition);

    public Position Position { get; init; }
    public float TextureSlot { get; init; }
    public Position TexturePosition { get; init; }

    public TexturedVertex Translate(float x, float y, float textureX, float textureY) =>
        this with
        {
            Position = Position.Translate(x, y),
            TexturePosition = TexturePosition.Translate(textureX, textureY)
        };
}

public struct Position
{
    public Position() : this(0, 0) { }
    public Position(float x, float y) => (X, Y) = (x, y);

    public float X { get; init; }
    public float Y { get; init; }

    public Position Translate(float x, float y) => new(X + x, Y + y);
}
