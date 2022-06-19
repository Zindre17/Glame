using Silk.NET.OpenGL;

namespace Glame;

public class IndexBuffer : Buffer<uint>
{
    unsafe public IndexBuffer(uint[]? data = null)
        : base(GLEnum.ElementArrayBuffer, data) { }

    override public void FeedData(Span<uint> data)
    {
        Count = (uint)data.Length;
        base.FeedData(data);
    }

    public uint Count { get; private set; }
}
