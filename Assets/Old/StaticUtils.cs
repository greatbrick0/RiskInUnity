/*
using OpenTK.Graphics.OpenGL4;
using StbImageSharp;

namespace INFR2100U;

public class StaticUtils
{
    private static readonly string MainDirectory =
        Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent + "\\";

    public static readonly string TextureDirectory = MainDirectory + @"Graphics\Textures\";
    public static readonly string ShaderDirectory = MainDirectory + @"Graphics\Shaders\";

    static StaticUtils()
    {
        StbImage.stbi_set_flip_vertically_on_load(1);
    }
    
    public static void CheckError(string stage)
    {
        ErrorCode errorCode = GL.GetError();
        if (errorCode != ErrorCode.NoError)
        {
            Console.WriteLine($"OpenGL Error ({stage}): {errorCode}");
        }
    }
}
*/