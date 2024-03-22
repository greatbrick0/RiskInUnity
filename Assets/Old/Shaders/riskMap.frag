#version 440 core

uniform vec4 color;
uniform sampler2D tex0;

in vec2 UV0;
out vec4 fragColor;

void main()
{
    vec4 colorTex0 = texture(tex0, UV0);
    fragColor = mix(color, colorTex0, colorTex0.a);
}