/*
using OpenTK.Graphics.OpenGL4;

namespace INFR2100U.Graphics;

public class Shader : IDisposable
{
    private readonly int Handle;
    protected bool isDisposed;

    public Shader(string vertPath, string fragPath)
    {
        // === INIT SHADERS === //
        int vertHandle = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertHandle, File.ReadAllText(StaticUtils.ShaderDirectory + vertPath));
        
        int fragHandle = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragHandle, File.ReadAllText(StaticUtils.ShaderDirectory + fragPath));
        
        // === COMPILE VERTEX SHADER === //
        GL.CompileShader(vertHandle);
        GL.GetShader(vertHandle, ShaderParameter.CompileStatus, out int successState);
        if (successState == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error in vertex shader: " + GL.GetShaderInfoLog(vertHandle));
            Console.ForegroundColor = ConsoleColor.White;
        }
        
        // === COMPILE FRAGMENT SHADER === //
        GL.CompileShader(fragHandle);
        GL.GetShader(fragHandle, ShaderParameter.CompileStatus, out successState);
        if (successState == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error in fragment shader: " + GL.GetShaderInfoLog(fragHandle));
            Console.ForegroundColor = ConsoleColor.White;
        }
        
        // === LINK SHADERS === //
        Handle = GL.CreateProgram();
        GL.AttachShader(Handle, vertHandle);
        GL.AttachShader(Handle, fragHandle);
        
        // === LINK PROGRAM === //
        GL.LinkProgram(Handle);
        GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out successState);
        if (successState == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error in linking program: " + GL.GetProgramInfoLog(Handle));
            Console.ForegroundColor = ConsoleColor.White;
        }
        
        // === DETACH SHADERS === //
        GL.DetachShader(Handle, fragHandle);
        GL.DetachShader(Handle, vertHandle);
        GL.DeleteShader(fragHandle);
        GL.DeleteShader(vertHandle);
    }

    // === HELPER FUNCTIONS === //
    public void Use()
    {
        GL.UseProgram(Handle);
    }

    public int GetAttribLocation(string attribName)
    {
        return GL.GetAttribLocation(Handle, attribName);
    }
    
    public int GetUniformLocation(string attribName)
    {
        return GL.GetUniformLocation(Handle, attribName);
    }
    
    // === GARBAGE COLLECTION === //
    ~Shader()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("MEMORY LEAK OCCURED!");
        Console.ForegroundColor = ConsoleColor.White;
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this); // DO NOT call deconstructor
    }

    protected virtual void Dispose(bool state)
    {
        if (state)
        {
            GL.DeleteProgram(Handle);
            isDisposed = true;
        }
    }
}
*/