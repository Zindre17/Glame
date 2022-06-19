#shader vertex
#version 330 core

layout (location = 0) in vec4 position;
layout (location = 1) in vec2 textureCoordinates;

out vec2 texCoord;

void main()
{
    gl_Position = position;
    texCoord = textureCoordinates;
}


#shader fragment
#version 330 core

layout (location = 0) out vec4 Color;

in vec2 texCoord;

uniform sampler2D textureSlot;

void main()
{
    vec4 textureColor = texture(textureSlot, texCoord);
    Color = textureColor;
}
