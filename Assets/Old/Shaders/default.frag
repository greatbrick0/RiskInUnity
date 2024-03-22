#version 440 core

uniform vec4 color;
uniform sampler2D tex0;
uniform sampler2D tex1;

in vec2 UV0;
out vec4 fragColor;

void main()
{
    vec4 colorTex0 = texture(tex0, UV0);
    vec4 colorTex1 = texture(tex1, UV0);
    
    float threshold = 0.9;
    float average = (colorTex1.r + colorTex1.g + colorTex1.b) / 3.0;
    
    if (average > threshold) colorTex1 = color;

    fragColor = mix(colorTex0, colorTex1, colorTex1.a);
}