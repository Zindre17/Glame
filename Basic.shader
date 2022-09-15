#shader vertex
#version 330 core

layout (location = 0) in vec4 position;
layout (location = 1) in float textureSlot;
layout (location = 2) in vec2 textureCoordinates;

out vec2 texCoord;
out float texSlot;

void main()
{
    gl_Position = position;
    texCoord = textureCoordinates;
    texSlot = textureSlot;
}


#shader fragment
#version 330 core

layout (location = 0) out vec4 Color;

in vec2 texCoord;
in float texSlot;

uniform sampler2D textures[10];

void main()
{
    vec4 textureColor = texture(textures[int(texSlot)], texCoord);
    Color = textureColor;
}
