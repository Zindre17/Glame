namespace Glame;

public static class QuadBatcher
{
    private const int QuadSize = 4;
    private const int IndexCount = 6;

    public static TexturedVertex[] Batch(params Quad[] quads)
    {
        var batchedVertices = new TexturedVertex[quads.Length * QuadSize];
        var offset = 0;
        foreach (var quad in quads)
        {
            Array.Copy(quad.Vertices, 0, batchedVertices, offset, QuadSize);
            offset += QuadSize;
        }
        return batchedVertices;
    }

    public static uint[] GetIndices(int quadCount)
    {
        var indices = new uint[IndexCount * quadCount];
        var quadOffset = 0u;
        for (uint i = 0; i < quadCount; i++)
        {
            var indexOffset = i * IndexCount;
            indices[indexOffset + 0] = quadOffset + 0;
            indices[indexOffset + 1] = quadOffset + 1;
            indices[indexOffset + 2] = quadOffset + 2;
            indices[indexOffset + 3] = quadOffset + 2;
            indices[indexOffset + 4] = quadOffset + 3;
            indices[indexOffset + 5] = quadOffset + 0;
            quadOffset += QuadSize;
        }
        return indices;
    }
}
