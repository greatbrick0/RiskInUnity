/*
using OpenTK.Graphics.OpenGL4;
using StbImageSharp;

namespace INFR2100U.Graphics;

public class Texture
{
    public readonly int Handle;

    public Texture(string filePath)
    {
        Handle = GL.GenTexture();
        Use();

        // === LOAD IMAGE === //
        using Stream stream = File.OpenRead(StaticUtils.TextureDirectory + filePath);
        ImageResult img = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, img.Width, img.Height, 0, PixelFormat.Rgba,
            PixelType.UnsignedByte, img.Data);
        
        // === FILTERING AND WRAPPING === //
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
        
        // === MIPMAPS === //
        GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
    }

    public void Use(TextureUnit unit = TextureUnit.Texture0)
    {
        GL.ActiveTexture(unit);
        GL.BindTexture(TextureTarget.Texture2D, Handle);
    }
}
*/