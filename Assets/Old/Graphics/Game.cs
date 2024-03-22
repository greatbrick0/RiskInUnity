/*
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;


namespace INFR2100U.Graphics
{
    public class Game : GameWindow
    {
        private Shader riskMapShader;
        private Texture riskMapTex;
    
        private int vertexBufferObject;
        private int vertexArrayObject;
        private int elementBufferObject;
    
        // Position + UVs
        private readonly float[] verts =
        {
            -1, 1, 0,   0, 1, // Top-left
            -1, -1, 0,  0, 0, // Bottom-left
            1, -1, 0,   1, 0, // Bottom-right
            1, 1, 0,    1, 1  // Top-right
        };
    
        private uint[] indices =
        {
            0, 1, 2,
            0, 2, 3
        };
    
        public Game(int width, int height, string title) : base(GameWindowSettings.Default,
            new NativeWindowSettings() { ClientSize = new Vector2i(width, height), Title = title })
        {
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, e.Width, e.Height);
            base.OnResize(e);
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(0.302f, 0.302f, 0.302f, 1f);
        
            riskMapShader = new Shader("default.vert", "riskMap.frag");
        
            // === TEXTURES === //
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        
            riskMapTex = new Texture("RiskGameMap.png");
            riskMapTex.Use();

            // === INIT VBO === //
            vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * verts.Length, verts, BufferUsageHint.StaticDraw);
        
            // === INIT VAO === //
            vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayObject);
        
            // === INIT EBO === //
            elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(uint) * indices.Length, indices, BufferUsageHint.StaticDraw);

            // === SET ATTRIBUTES === //
            int id = riskMapShader.GetAttribLocation("aPos");
            GL.VertexAttribPointer(id, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            GL.EnableVertexAttribArray(id);

            id = riskMapShader.GetAttribLocation("UVs");
            GL.VertexAttribPointer(id, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(id);

            riskMapShader.Use();
        
            // === SET UNIFORMS === //
            id = riskMapShader.GetUniformLocation("color");
            GL.Uniform4(id, Color4.Aqua);
        
            id = riskMapShader.GetUniformLocation("tex0");
            GL.Uniform1(id, 0);
        }

        protected override void OnUnload()
        {
            // === CLEAN UP === //
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.UseProgram(0);
        
            GL.DeleteBuffer(vertexBufferObject);
            GL.DeleteBuffer(vertexArrayObject);
            GL.DeleteBuffer(elementBufferObject);
        
            riskMapShader.Dispose();
        
            base.OnUnload();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            riskMapTex.Use();
            riskMapShader.Use();
        
            int id = riskMapShader.GetUniformLocation("color");
            GL.Uniform4(id, Color4.Aqua);
        
            GL.BindVertexArray(vertexArrayObject);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            if (KeyboardState.IsKeyDown(Keys.Escape)) Close();
        }
    }
}
*/