#version 440 core

layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 UVs;

out vec2 UV0;

void main()
{
    gl_Position = vec4(aPos, 1);

    UV0 = UVs;
}