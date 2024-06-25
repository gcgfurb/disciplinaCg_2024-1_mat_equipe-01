﻿#define CG_Gizmo  // debugar gráfico.
#define CG_OpenGL // render OpenGL.
// #define CG_DirectX // render DirectX.
// #define CG_Privado // código do professor.

using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using System;
using OpenTK.Mathematics;
using System.Collections.Generic;


//FIXME: padrão Singleton

namespace gcgcg
{
  public class Mundo : GameWindow
  {
    Objeto mundo;
    private char rotuloNovo = '?';
    private Objeto objetoSelecionado = null;

    private readonly float[] _sruEixos =
    {
      -0.5f,  0.0f,  0.0f, /* X- */      0.5f,  0.0f,  0.0f, /* X+ */
       0.0f, -0.5f,  0.0f, /* Y- */      0.0f,  0.5f,  0.0f, /* Y+ */
       0.0f,  0.0f, -0.5f, /* Z- */      0.0f,  0.0f,  0.5f  /* Z+ */
    };

      private readonly float[] _vertices = {
        // Position         Texture coordinates
        -0.3005f, -0.3005f,  0.3005f, 0.0f, 0.0f, // front
          0.3005f, -0.3005f,  0.3005f, 1.0f, 0.0f,
          0.3005f,  0.3005f,  0.3005f, 1.0f, 1.0f,
        -0.3005f,  0.3005f,  0.3005f, 0.0f, 1.0f,
      //   -0.3005f, -0.3005f, -0.3005f, 0.0f, 0.0f, // back
      //   0.3005f, -0.3005f, -0.3005f, 1.0f, 0.0f,
      //   0.3005f,  0.3005f, -0.3005f, 1.0f, 1.0f,
      //   -0.3005f,  0.3005f, -0.3005f, 0.0f, 1.0f,
      //   -0.3005f, 0.3005f,  0.3005f, 0.0f, 0.0f, // top
      //   0.3005f, 0.3005f,  -0.3005f, 1.0f, 1.0f,
      //   0.3005f, 0.3005f,  0.3005f, 1.0f, 0.0f,
      //   -0.3005f, 0.3005f, -0.3005f, 0.0f, 1.0f,
      //   0.3005f, -0.3005f,  -0.3005f, 0.0f, 0.0f, // bottom
      //   -0.3005f, -0.3005f,  0.3005f, 1.0f, 1.0f,
      //   -0.3005f, -0.3005f,  -0.3005f, 1.0f, 0.0f,
      //   0.3005f, -0.3005f, 0.3005f, 0.0f, 1.0f,
      //   0.3005f, -0.3005f, -0.3005f, 0.0f, 0.0f, //right
      //   0.3005f, 0.3005f, 0.3005f, 1.0f, 1.0f,
      //   0.3005f, -0.3005f, 0.3005f, 1.0f, 0.0f,
      //   0.3005f, 0.3005f, -0.3005f, 0.0f, 1.0f,
      //  -0.3005f, -0.3005f, 0.3005f, 0.0f, 0.0f, //left
      //  -0.3005f, 0.3005f, -0.3005f, 1.0f, 1.0f,
      //  -0.3005f, -0.3005f, -0.3005f, 1.0f, 0.0f,
      //  -0.3005f, 0.3005f, 0.3005f, 0.0f, 1.0f,
    };
      private readonly float[] _vertices2 = {
        // Position         Texture coordinates
        -0.3005f, -0.3005f, -0.3005f, 0.0f, 0.0f, // back
        0.3005f, -0.3005f, -0.3005f, 1.0f, 0.0f,
        0.3005f,  0.3005f, -0.3005f, 1.0f, 1.0f,
        -0.3005f,  0.3005f, -0.3005f, 0.0f, 1.0f
      };
      private readonly float[] _vertices3 = {
        // Position         Texture coordinates
        -0.3005f, 0.3005f,  0.3005f, 0.0f, 0.0f, // top
        0.3005f, 0.3005f,  -0.3005f, 1.0f, 1.0f,
        0.3005f, 0.3005f,  0.3005f, 1.0f, 0.0f,
        -0.3005f, 0.3005f, -0.3005f, 0.0f, 1.0f
      };

      private readonly float[] _vertices4 = {
        // Position         Texture coordinates
        0.3005f, -0.3005f,  -0.3005f, 0.0f, 0.0f, // bottom
        -0.3005f, -0.3005f,  0.3005f, 1.0f, 1.0f,
        -0.3005f, -0.3005f,  -0.3005f, 1.0f, 0.0f,
        0.3005f, -0.3005f, 0.3005f, 0.0f, 1.0f
      };

      private readonly float[] _vertices5 = {
        // Position         Texture coordinates
        0.3005f, -0.3005f, -0.3005f, 0.0f, 0.0f, //right
        0.3005f, 0.3005f, 0.3005f, 1.0f, 1.0f,
        0.3005f, -0.3005f, 0.3005f, 1.0f, 0.0f,
        0.3005f, 0.3005f, -0.3005f, 0.0f, 1.0f
      };

      private readonly float[] _vertices6 = {
        // Position         Texture coordinates
       -0.3005f, -0.3005f, 0.3005f, 0.0f, 0.0f, //left
       -0.3005f, 0.3005f, -0.3005f, 1.0f, 1.0f,
       -0.3005f, -0.3005f, -0.3005f, 1.0f, 0.0f,
       -0.3005f, 0.3005f, 0.3005f, 0.0f, 1.0f
      };

    private readonly uint[] _indices =
    {
        1, 2, 3,
        0, 1, 3,
        // 5, 6, 7,
        // 4, 5, 7,
        // 11, 8, 9,
        // 8, 10, 9,
        // 15, 12, 13,
        // 12, 14, 13,
        // 17, 16, 18,
        // 19, 16, 17,
        // 21, 20, 22,
        // 23, 20, 21
    };

    private readonly uint[] _indices2 =
    {
        1, 2, 3,
        0, 1, 3
    };

    private readonly uint[] _indices3 =
    {
        3, 0, 1,
        0, 2, 1
    };
    private readonly uint[] _indices4 =
    {
        3, 0, 1,
        0, 2, 1
    };
    private readonly uint[] _indices5 =
    {
        1, 0, 2,
        3, 0, 1
    };
    private readonly uint[] _indices6 =
    {
        1, 0, 2,
        3, 0, 1
    };
    private int _vertexBufferObject_sruEixos;
    private int _vertexArrayObject_sruEixos;

    private float xAtual;
    private float xAnterior;
    double anguloX;
    private float yAtual;
    private float yAnterior;
    double anguloY;

    private Shader _shader;
    private Shader _shader2;
    private Shader _shader3;
    private Shader _shader4;
    private Shader _shader5;
    private Shader _shader6;
    private Shader _shaderBranca;
    private Shader _shaderVermelha;
    private Shader _shaderVerde;
    private Shader _shaderAzul;
    private Shader _shaderCiano;
    private Shader _shaderMagenta;
    private Shader _shaderAmarela;
    private Texture _texture;
    private Texture _texture2;
    private Texture _texture3;
    private Texture _texture4;
    private Texture _texture5;
    private Texture _texture6;
    private int _vertexBufferObject_texture;
    private int _vertexArrayObject_texture;
    private int _elementBufferObject_texture;
    private int _vertexBufferObject_texture2;
    private int _vertexArrayObject_texture2;
    private int _elementBufferObject_texture2;
    private int _vertexBufferObject_texture3;
    private int _vertexArrayObject_texture3;
    private int _elementBufferObject_texture3;
    private int _vertexBufferObject_texture4;
    private int _vertexArrayObject_texture4;
    private int _elementBufferObject_texture4;
    private int _vertexBufferObject_texture5;
    private int _vertexArrayObject_texture5;
    private int _elementBufferObject_texture5;
    private int _vertexBufferObject_texture6;
    private int _vertexArrayObject_texture6;
    private int _elementBufferObject_texture6;

    private readonly Vector3 _lightPos = new Vector3(1.2f, 1.0f, 2.0f);
    private int _vaoModel;
    private Shader _lightingShader;

    private bool luz1;
    private bool luz2;
    private bool luz3;
    private bool luz4;
    private bool luz5;
    private bool luz6;

    private List<Ponto4D> verticesCubo;

    private Camera _camera;

    public Mundo(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
           : base(gameWindowSettings, nativeWindowSettings)
    {
      mundo = new Objeto(null, ref rotuloNovo);
    }

    private void Diretivas()
    {
#if DEBUG
      Console.WriteLine("Debug version");
#endif      
#if RELEASE
    Console.WriteLine("Release version");
#endif      
#if CG_Gizmo      
      Console.WriteLine("#define CG_Gizmo  // debugar gráfico.");
#endif
#if CG_OpenGL      
      Console.WriteLine("#define CG_OpenGL // render OpenGL.");
#endif
#if CG_DirectX      
      Console.WriteLine("#define CG_DirectX // render DirectX.");
#endif
#if CG_Privado      
      Console.WriteLine("#define CG_Privado // código do professor.");
#endif
      Console.WriteLine("__________________________________ \n");
    }

    protected override void OnLoad()
    {
      base.OnLoad();

      Diretivas();

      GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);

      GL.Enable(EnableCap.DepthTest);       // Ativar teste de profundidade
      // GL.Enable(EnableCap.CullFace);     // Desenha os dois lados da face
      // GL.FrontFace(FrontFaceDirection.Cw);
      // GL.CullFace(CullFaceMode.FrontAndBack);

      #region Cores
      _shaderBranca = new Shader("Shaders/shader.vert", "Shaders/shaderBranca.frag");
      _shaderVermelha = new Shader("Shaders/shader.vert", "Shaders/shaderVermelha.frag");
      _shaderVerde = new Shader("Shaders/shader.vert", "Shaders/shaderVerde.frag");
      _shaderAzul = new Shader("Shaders/shader.vert", "Shaders/shaderAzul.frag");
      _shaderCiano = new Shader("Shaders/shader.vert", "Shaders/shaderCiano.frag");
      _shaderMagenta = new Shader("Shaders/shader.vert", "Shaders/shaderMagenta.frag");
      _shaderAmarela = new Shader("Shaders/shader.vert", "Shaders/shaderAmarela.frag");
      #endregion

      #region Eixos: SRU  
      _vertexBufferObject_sruEixos = GL.GenBuffer();
      GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject_sruEixos);
      GL.BufferData(BufferTarget.ArrayBuffer, _sruEixos.Length * sizeof(float), _sruEixos, BufferUsageHint.StaticDraw);
      _vertexArrayObject_sruEixos = GL.GenVertexArray();
      GL.BindVertexArray(_vertexArrayObject_sruEixos);
      GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
      GL.EnableVertexAttribArray(0);
      #endregion

      // #region Objeto: polígono qualquer  
      // List<Ponto4D> pontosPoligonoBandeira = new List<Ponto4D>();
      // pontosPoligonoBandeira.Add(new Ponto4D(0.25, 0.25));
      // pontosPoligonoBandeira.Add(new Ponto4D(0.75, 0.25));
      // pontosPoligonoBandeira.Add(new Ponto4D(0.75, 0.75));
      // pontosPoligonoBandeira.Add(new Ponto4D(0.50, 0.50));
      // pontosPoligonoBandeira.Add(new Ponto4D(0.25, 0.75));
      // objetoSelecionado = new Poligono(mundo, ref rotuloNovo, pontosPoligonoBandeira);
      // #endregion
      // #region declara um objeto filho ao polígono qualquer
      // List<Ponto4D> pontosPoligonoTriangulo = new List<Ponto4D>();
      // pontosPoligonoTriangulo.Add(new Ponto4D(0.50, 0.50));
      // pontosPoligonoTriangulo.Add(new Ponto4D(0.75, 0.75));
      // pontosPoligonoTriangulo.Add(new Ponto4D(0.25, 0.75));
      // objetoSelecionado = new Poligono(objetoSelecionado, ref rotuloNovo, pontosPoligonoTriangulo);
      // objetoSelecionado.PrimitivaTipo = PrimitiveType.Triangles;
      // #endregion

      // #region Objeto: polígono quadrado
      // List<Ponto4D> pontosPoligonoQuadrado = new List<Ponto4D>();
      // pontosPoligonoQuadrado.Add(new Ponto4D(-0.25, 0.25, 0.1));
      // pontosPoligonoQuadrado.Add(new Ponto4D(-0.75, 0.25, 0.1));
      // pontosPoligonoQuadrado.Add(new Ponto4D(-0.75, 0.75, 0.1));
      // pontosPoligonoQuadrado.Add(new Ponto4D(-0.25, 0.75, 0.1));
      // objetoSelecionado = new Poligono(mundo, ref rotuloNovo, pontosPoligonoQuadrado);
      // objetoSelecionado.PrimitivaTipo = PrimitiveType.TriangleFan;
      // #endregion

      #region Objeto: Cubo
      objetoSelecionado = new Cubo(mundo, ref rotuloNovo);
      objetoSelecionado.shaderCor = _shaderVermelha;
      objetoSelecionado.PrimitivaTipo = PrimitiveType.TriangleFan;
      #endregion

      #region Front Texture
      verticesCubo = objetoSelecionado.getListaPontos();
      GL.Enable(EnableCap.Texture2D);
      _vertexArrayObject_texture = GL.GenVertexArray();
      GL.BindVertexArray(_vertexArrayObject_texture);

      _vertexBufferObject_texture = GL.GenBuffer();
      GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject_texture);
      GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

      _elementBufferObject_texture = GL.GenBuffer();
      GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject_texture);
      GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

      _shader = new Shader("Shaders/shader_texture.vert", "Shaders/shader_texture.frag");
      _shader.Use();

      var vertexLocation = _shader.GetAttribLocation("aPosition");
      GL.EnableVertexAttribArray(vertexLocation);
      GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

      var texCoordLocation = _shader.GetAttribLocation("aTexCoord");
      GL.EnableVertexAttribArray(texCoordLocation);
      GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

      _texture = Texture.LoadFromFile("Resources/skibidi-toilet-guy.jpg");
      _texture.Use(TextureUnit.Texture0);

      Retangulo front = new Retangulo(objetoSelecionado, ref rotuloNovo, new Ponto4D(-0.3, -0.3, 0.3), new Ponto4D(0.3, 0.3, 0.3), false);
      front.shaderCor = _shader;

      #endregion

      #region Back Texture
      GL.Enable(EnableCap.Texture2D);
      _vertexArrayObject_texture2 = GL.GenVertexArray();
      GL.BindVertexArray(_vertexArrayObject_texture2);

      _vertexBufferObject_texture2 = GL.GenBuffer();
      GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject_texture2);
      GL.BufferData(BufferTarget.ArrayBuffer, _vertices2.Length * sizeof(float), _vertices2, BufferUsageHint.StaticDraw);

      _elementBufferObject_texture2 = GL.GenBuffer();
      GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject_texture2);
      GL.BufferData(BufferTarget.ElementArrayBuffer, _indices2.Length * sizeof(uint), _indices2, BufferUsageHint.StaticDraw);

      _shader2 = new Shader("Shaders/shader_texture.vert", "Shaders/shader_texture.frag");
      _shader2.Use();

      var vertexLocation2 = _shader2.GetAttribLocation("aPosition");
      GL.EnableVertexAttribArray(vertexLocation);
      GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

      var texCoordLocation2 = _shader2.GetAttribLocation("aTexCoord");
      GL.EnableVertexAttribArray(texCoordLocation);
      GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

      _texture2 = Texture.LoadFromFile("Resources/skibidi-toilet-guy.jpg");
      _texture2.Use(TextureUnit.Texture0);

      Retangulo back = new Retangulo(objetoSelecionado, ref rotuloNovo, new Ponto4D(-0.3, -0.3, -0.3), new Ponto4D(0.3, 0.3, -0.3), false);
      back.shaderCor = _shader2;
      #endregion

      #region Top Texture
      GL.Enable(EnableCap.Texture2D);
      _vertexArrayObject_texture3 = GL.GenVertexArray();
      GL.BindVertexArray(_vertexArrayObject_texture3);

      _vertexBufferObject_texture3 = GL.GenBuffer();
      GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject_texture3);
      GL.BufferData(BufferTarget.ArrayBuffer, _vertices3.Length * sizeof(float), _vertices3, BufferUsageHint.StaticDraw);

      _elementBufferObject_texture3 = GL.GenBuffer();
      GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject_texture3);
      GL.BufferData(BufferTarget.ElementArrayBuffer, _indices3.Length * sizeof(uint), _indices3, BufferUsageHint.StaticDraw);

      _shader3 = new Shader("Shaders/shader_texture.vert", "Shaders/shader_texture.frag");
      _shader3.Use();

      var vertexLocation3 = _shader3.GetAttribLocation("aPosition");
      GL.EnableVertexAttribArray(vertexLocation);
      GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

      var texCoordLocation3 = _shader3.GetAttribLocation("aTexCoord");
      GL.EnableVertexAttribArray(texCoordLocation);
      GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

      _texture3 = Texture.LoadFromFile("Resources/skibidi-toilet-guy.jpg");
      _texture3.Use(TextureUnit.Texture0);

      Retangulo top = new Retangulo(objetoSelecionado, ref rotuloNovo, new Ponto4D(-0.3, 0.3, -0.3), new Ponto4D(0.3, 0.3, 0.3), false);
      top.shaderCor = _shader3;

      #endregion

      #region Bottom Texture
      GL.Enable(EnableCap.Texture2D);
      _vertexArrayObject_texture4 = GL.GenVertexArray();
      GL.BindVertexArray(_vertexArrayObject_texture4);

      _vertexBufferObject_texture4 = GL.GenBuffer();
      GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject_texture4);
      GL.BufferData(BufferTarget.ArrayBuffer, _vertices4.Length * sizeof(float), _vertices4, BufferUsageHint.StaticDraw);

      _elementBufferObject_texture4 = GL.GenBuffer();
      GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject_texture4);
      GL.BufferData(BufferTarget.ElementArrayBuffer, _indices4.Length * sizeof(uint), _indices4, BufferUsageHint.StaticDraw);

      _shader4 = new Shader("Shaders/shader_texture.vert", "Shaders/shader_texture.frag");
      _shader4.Use();

      var vertexLocation4 = _shader4.GetAttribLocation("aPosition");
      GL.EnableVertexAttribArray(vertexLocation);
      GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

      var texCoordLocation4 = _shader4.GetAttribLocation("aTexCoord");
      GL.EnableVertexAttribArray(texCoordLocation);
      GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

      _texture4 = Texture.LoadFromFile("Resources/skibidi-toilet-guy.jpg");
      _texture4.Use(TextureUnit.Texture0);

      Retangulo bottom = new Retangulo(objetoSelecionado, ref rotuloNovo, new Ponto4D(-0.3, -0.3, -0.3), new Ponto4D(0.3, -0.3, 0.3), false);
      bottom.shaderCor = _shader4;

      #endregion

      #region Right Texture
      GL.Enable(EnableCap.Texture2D);
      _vertexArrayObject_texture5 = GL.GenVertexArray();
      GL.BindVertexArray(_vertexArrayObject_texture5);

      _vertexBufferObject_texture5 = GL.GenBuffer();
      GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject_texture5);
      GL.BufferData(BufferTarget.ArrayBuffer, _vertices5.Length * sizeof(float), _vertices5, BufferUsageHint.StaticDraw);

      _elementBufferObject_texture5 = GL.GenBuffer();
      GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject_texture5);
      GL.BufferData(BufferTarget.ElementArrayBuffer, _indices5.Length * sizeof(uint), _indices5, BufferUsageHint.StaticDraw);

      _shader5 = new Shader("Shaders/shader_texture.vert", "Shaders/shader_texture.frag");
      _shader5.Use();

      var vertexLocation5 = _shader5.GetAttribLocation("aPosition");
      GL.EnableVertexAttribArray(vertexLocation);
      GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

      var texCoordLocation5 = _shader5.GetAttribLocation("aTexCoord");
      GL.EnableVertexAttribArray(texCoordLocation);
      GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

      _texture5 = Texture.LoadFromFile("Resources/skibidi-toilet-guy.jpg");
      _texture5.Use(TextureUnit.Texture0);

      Retangulo right = new Retangulo(objetoSelecionado, ref rotuloNovo, new Ponto4D(0.3, -0.3, -0.3), new Ponto4D(0.3, 0.3, 0.3), true);
      right.shaderCor = _shader5;
      #endregion
    
      #region Left Texture
      GL.Enable(EnableCap.Texture2D);
      _vertexArrayObject_texture6 = GL.GenVertexArray();
      GL.BindVertexArray(_vertexArrayObject_texture6);

      _vertexBufferObject_texture6 = GL.GenBuffer();
      GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject_texture6);
      GL.BufferData(BufferTarget.ArrayBuffer, _vertices6.Length * sizeof(float), _vertices6, BufferUsageHint.StaticDraw);

      _elementBufferObject_texture6 = GL.GenBuffer();
      GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject_texture5);
      GL.BufferData(BufferTarget.ElementArrayBuffer, _indices6.Length * sizeof(uint), _indices6, BufferUsageHint.StaticDraw);

      _shader6 = new Shader("Shaders/shader_texture.vert", "Shaders/shader_texture.frag");
      _shader6.Use();

      var vertexLocation6 = _shader6.GetAttribLocation("aPosition");
      GL.EnableVertexAttribArray(vertexLocation);
      GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

      var texCoordLocation6 = _shader6.GetAttribLocation("aTexCoord");
      GL.EnableVertexAttribArray(texCoordLocation);
      GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

      _texture6 = Texture.LoadFromFile("Resources/skibidi-toilet-guy.jpg");
      _texture6.Use(TextureUnit.Texture0);

      Retangulo left = new Retangulo(objetoSelecionado, ref rotuloNovo, new Ponto4D(-0.3, -0.3, -0.3), new Ponto4D(-0.3, 0.3, 0.3), true);
      left.shaderCor = _shader6;

      #endregion

      #region Objeto: Cubo
      objetoSelecionado = new Cubo(mundo, ref rotuloNovo);
      objetoSelecionado.shaderCor = _shaderVermelha;
      objetoSelecionado.PrimitivaTipo = PrimitiveType.TriangleFan;
      #endregion

      objetoSelecionado.MatrizEscalaXYZ(-0.2, -0.2, -0.2);
      objetoSelecionado.MatrizTranslacaoXYZ(0.8, 0, 0);

      _camera = new Camera(Vector3.UnitZ, Size.X / (float)Size.Y);

      anguloX = -90;
      anguloY = 0;

    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
      base.OnRenderFrame(e);

      GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

      mundo.Desenhar(new Transformacao4D(), _camera);
      if(luz1 != true && luz2 != true && luz3 != true && luz4 != true && luz5 != true && luz6 != true) {
        #region drawFrontTexture
        
        GL.BindVertexArray(_vertexArrayObject_texture);

        _texture.Use(TextureUnit.Texture0);
        _shader.Use();
        GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);

        #endregion

        #region drawBackTexture
        
        GL.BindVertexArray(_vertexArrayObject_texture2);

        _texture2.Use(TextureUnit.Texture0);
        _shader2.Use();
        GL.DrawElements(PrimitiveType.Triangles, _indices2.Length, DrawElementsType.UnsignedInt, 0);

        #endregion

        #region drawTopTexture
        
        GL.BindVertexArray(_vertexArrayObject_texture3);

        _texture3.Use(TextureUnit.Texture0);
        _shader3.Use();
        GL.DrawElements(PrimitiveType.Triangles, _indices3.Length, DrawElementsType.UnsignedInt, 0);

        #endregion

        #region drawBottomTexture
        
        GL.BindVertexArray(_vertexArrayObject_texture4);

        _texture4.Use(TextureUnit.Texture0);
        _shader4.Use();
        GL.DrawElements(PrimitiveType.Triangles, _indices4.Length, DrawElementsType.UnsignedInt, 0);

        #endregion

        #region drawRightTexture
        
        GL.BindVertexArray(_vertexArrayObject_texture5);

        _texture5.Use(TextureUnit.Texture0);
        _shader5.Use();
        GL.DrawElements(PrimitiveType.Triangles, _indices5.Length, DrawElementsType.UnsignedInt, 0);

        #endregion

        #region drawLeftTexture
        
        GL.BindVertexArray(_vertexArrayObject_texture6);

        _texture6.Use(TextureUnit.Texture0);
        _shader6.Use();
        GL.DrawElements(PrimitiveType.Triangles, _indices6.Length, DrawElementsType.UnsignedInt, 0);

        #endregion
      }

      if(luz1) {
        GL.BindVertexArray(_vaoModel);

        _lightingShader.Use();

        _lightingShader.SetMatrix4("model", Matrix4.Identity);
        _lightingShader.SetMatrix4("view", _camera.GetViewMatrix());
        _lightingShader.SetMatrix4("projection", _camera.GetProjectionMatrix());

        _lightingShader.SetVector3("objectColor", new Vector3(1.0f, 0.5f, 0.31f));
        _lightingShader.SetVector3("lightColor", new Vector3(1.0f, 1.0f, 1.0f));
        _lightingShader.SetVector3("lightPos", _lightPos);
        _lightingShader.SetVector3("viewPos", _camera.Position);

        GL.DrawArrays(PrimitiveType.Triangles, 0, 36);

        objetoSelecionado.shaderCor = _lightingShader;
        mundo.GrafocenaBuscaProximo(objetoSelecionado).shaderCor = _lightingShader;
      }

      if(luz2) {
        GL.BindVertexArray(_vaoModel);

        _lightingShader.Use();

        _lightingShader.SetMatrix4("model", Matrix4.Identity);
        _lightingShader.SetMatrix4("view", _camera.GetViewMatrix());
        _lightingShader.SetMatrix4("projection", _camera.GetProjectionMatrix());

        _lightingShader.SetVector3("viewPos", _camera.Position);

        // Here we specify to the shaders what textures they should refer to when we want to get the positions.
        _lightingShader.SetInt("material.diffuse", 0);
        _lightingShader.SetInt("material.specular", 1);
        _lightingShader.SetVector3("material.specular", new Vector3(0.5f, 0.5f, 0.5f));
        _lightingShader.SetFloat("material.shininess", 32.0f);

        _lightingShader.SetVector3("light.position", _lightPos);
        _lightingShader.SetVector3("light.ambient", new Vector3(2.0f));
        _lightingShader.SetVector3("light.diffuse", new Vector3(1f));
        _lightingShader.SetVector3("light.specular", new Vector3(1.0f));

        GL.DrawArrays(PrimitiveType.Triangles, 0, 36);

        objetoSelecionado.shaderCor = _lightingShader;
        mundo.GrafocenaBuscaProximo(objetoSelecionado).shaderCor = _lightingShader;
      }

      if(luz3) {
        GL.BindVertexArray(_vaoModel);

        _lightingShader.SetMatrix4("view", _camera.GetViewMatrix());
        _lightingShader.SetMatrix4("projection", _camera.GetProjectionMatrix());

        _lightingShader.SetVector3("viewPos", _camera.Position);

        _lightingShader.SetInt("material.diffuse", 0);
        _lightingShader.SetInt("material.specular", 1);
        _lightingShader.SetVector3("material.specular", new Vector3(0.5f, 0.5f, 0.5f));
        _lightingShader.SetFloat("material.shininess", 32.0f);

        // Directional light needs a direction, in this example we just use (-0.2, -1.0, -0.3f) as the lights direction
        _lightingShader.SetVector3("light.direction", new Vector3(-0.2f, -1.0f, -0.3f));
        _lightingShader.SetVector3("light.ambient", new Vector3(0.2f));
        _lightingShader.SetVector3("light.diffuse", new Vector3(0.5f));
        _lightingShader.SetVector3("light.specular", new Vector3(1.0f));

        objetoSelecionado.shaderCor = _lightingShader;
        mundo.GrafocenaBuscaProximo(objetoSelecionado).shaderCor = _lightingShader;
      }



#if CG_Gizmo      
      Gizmo_Sru3D();
#endif
      SwapBuffers();
    }

      public double converteValorPonto(float coordenada, bool eixo){
      // coordenada vem um valor entre 0 e 800, tem que converter pra -1 até 1
      // eixo true = x, eixo false = y
      float convertido;
      float inc = 0.0025f;
      if (eixo){
        convertido = -1+(inc*coordenada);
      } else {
        convertido = +1-(inc*coordenada);
      }
      return convertido;
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
      base.OnUpdateFrame(e);

      objetoSelecionado.MatrizRotacao(0.05);

      // ☞ 396c2670-8ce0-4aff-86da-0f58cd8dcfdc   TODO: forma otimizada para teclado.
      #region Teclado
      var input = KeyboardState;
      if (input.IsKeyDown(Keys.Escape))
        Close();
      if (input.IsKeyPressed(Keys.G))
        mundo.GrafocenaImprimir("");
      if (input.IsKeyPressed(Keys.P) && objetoSelecionado != null)
        System.Console.WriteLine(objetoSelecionado.ToString());
      if (input.IsKeyPressed(Keys.M) && objetoSelecionado != null)
        objetoSelecionado.MatrizImprimir();
      if (input.IsKeyPressed(Keys.I) && objetoSelecionado != null)
        objetoSelecionado.MatrizAtribuirIdentidade();
      if (input.IsKeyPressed(Keys.Left) && objetoSelecionado != null)
        objetoSelecionado.MatrizTranslacaoXYZ(-0.05, 0, 0);
      if (input.IsKeyPressed(Keys.Right) && objetoSelecionado != null)
        objetoSelecionado.MatrizTranslacaoXYZ(0.05, 0, 0);
      if (input.IsKeyPressed(Keys.Up) && objetoSelecionado != null)
        objetoSelecionado.MatrizTranslacaoXYZ(0, 0.05, 0);
      if (input.IsKeyPressed(Keys.Down) && objetoSelecionado != null)
        objetoSelecionado.MatrizTranslacaoXYZ(0, -0.05, 0);
      if (input.IsKeyPressed(Keys.O) && objetoSelecionado != null)
        objetoSelecionado.MatrizTranslacaoXYZ(0, 0, 0.05);
      if (input.IsKeyPressed(Keys.L) && objetoSelecionado != null)
        objetoSelecionado.MatrizTranslacaoXYZ(0, 0, -0.05);
      if (input.IsKeyPressed(Keys.PageUp) && objetoSelecionado != null)
        objetoSelecionado.MatrizEscalaXYZ(2, 2, 2);
      if (input.IsKeyPressed(Keys.PageDown) && objetoSelecionado != null)
        objetoSelecionado.MatrizEscalaXYZ(0.5, 0.5, 0.5);
      if (input.IsKeyPressed(Keys.Home) && objetoSelecionado != null)
        objetoSelecionado.MatrizEscalaXYZBBox(0.5, 0.5, 0.5);
      if (input.IsKeyPressed(Keys.End) && objetoSelecionado != null)
        objetoSelecionado.MatrizEscalaXYZBBox(2, 2, 2);
      if (input.IsKeyPressed(Keys.D1)) {
        _lightingShader = new Shader("Shaders/shader.vert", "Shaders/lighting.frag");

        {
          _vaoModel = GL.GenVertexArray();
          GL.BindVertexArray(_vaoModel);

          var positionLocation = _lightingShader.GetAttribLocation("aPos");
          GL.EnableVertexAttribArray(positionLocation);
          // Remember to change the stride as we now have 6 floats per vertex
          GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);

          // We now need to define the layout of the normal so the shader can use it
          var normalLocation = _lightingShader.GetAttribLocation("aNormal");
          GL.EnableVertexAttribArray(normalLocation);
          GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
        }

        luz1 = true;
        luz2 = false;
        luz3 = false;
        luz4 = false;
        luz5 = false;
        luz6 = false;
      }
      if (input.IsKeyPressed(Keys.D2)) {
        _lightingShader = new Shader("Shaders/shader.vert", "Shaders/lighting2.frag");

        {
          _vaoModel = GL.GenVertexArray();
          GL.BindVertexArray(_vaoModel);

          // All of the vertex attributes have been updated to now have a stride of 8 float sizes.
          var positionLocation = _lightingShader.GetAttribLocation("aPos");
          GL.EnableVertexAttribArray(positionLocation);
          GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);

          var normalLocation = _lightingShader.GetAttribLocation("aNormal");
          GL.EnableVertexAttribArray(normalLocation);
          GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));

          // The texture coords have now been added too, remember we only have 2 coordinates as the texture is 2d,
          // so the size parameter should only be 2 for the texture coordinates.
          var texCoordLocation = _lightingShader.GetAttribLocation("aTexCoords");
          GL.EnableVertexAttribArray(texCoordLocation);
          GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));
        }

        luz1 = false;
        luz2 = true;
        luz3 = false;
        luz4 = false;
        luz5 = false;
        luz6 = false;
      }
      if (input.IsKeyPressed(Keys.D3)) {
        _lightingShader = new Shader("Shaders/shader.vert", "Shaders/lighting3.frag");

        {
          _vaoModel = GL.GenVertexArray();
          GL.BindVertexArray(_vaoModel);

          var positionLocation = _lightingShader.GetAttribLocation("aPos");
          GL.EnableVertexAttribArray(positionLocation);
          GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);

          var normalLocation = _lightingShader.GetAttribLocation("aNormal");
          GL.EnableVertexAttribArray(normalLocation);
          GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));

          var texCoordLocation = _lightingShader.GetAttribLocation("aTexCoords");
          GL.EnableVertexAttribArray(texCoordLocation);
          GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));
        }

        luz1 = false;
        luz2 = false;
        luz3 = true;
        luz4 = false;
        luz5 = false;
        luz6 = false;
      }
      if (input.IsKeyPressed(Keys.D4)) {

        luz1 = false;
        luz2 = false;
        luz3 = false;
        luz4 = true;
        luz5 = false;
        luz6 = false;
      }
      if (input.IsKeyPressed(Keys.D5)) {

        luz1 = false;
        luz2 = false;
        luz3 = false;
        luz4 = false;
        luz5 = true;
        luz6 = false;
      }
      if (input.IsKeyPressed(Keys.D6)) {

        luz1 = false;
        luz2 = false;
        luz3 = false;
        luz4 = false;
        luz5 = false;
        luz6 = true;
      }
      if (input.IsKeyPressed(Keys.D0)) {
        luz1 = false;
        luz2 = false;
        luz3 = false;
        luz4 = false;
        luz5 = false;
        luz6 = false;
      }

      const float cameraSpeed = 1.5f;
      if (input.IsKeyDown(Keys.Z))
        _camera.Position = Vector3.UnitZ;
      if (input.IsKeyDown(Keys.W))
        _camera.Position += _camera.Front * cameraSpeed * (float)e.Time; // Forward
      if (input.IsKeyDown(Keys.S))
        _camera.Position -= _camera.Front * cameraSpeed * (float)e.Time; // Backwards
      if (input.IsKeyDown(Keys.A))
        _camera.Position -= _camera.Right * cameraSpeed * (float)e.Time; // Left
      if (input.IsKeyDown(Keys.D))
        _camera.Position += _camera.Right * cameraSpeed * (float)e.Time; // Right
      if (input.IsKeyDown(Keys.RightShift))
        _camera.Position += _camera.Up * cameraSpeed * (float)e.Time; // Up
      if (input.IsKeyDown(Keys.LeftShift))
        _camera.Position -= _camera.Up * cameraSpeed * (float)e.Time; // Down
      if (input.IsKeyDown(Keys.H))
        _camera.Pitch += -0.5f;
      if (input.IsKeyDown(Keys.Y))
        _camera.Pitch += 0.5f;
      if (input.IsKeyDown(Keys.G))
        _camera.Yaw += -0.5f;
      if (input.IsKeyDown(Keys.J))
        _camera.Yaw += 0.5f;
      #endregion

      #region  Mouse

      if (MouseState.IsButtonPressed(MouseButton.Left))
      {
        System.Console.WriteLine("MouseState.IsButtonPressed(MouseButton.Left)");
        System.Console.WriteLine("__ Valores do Espaço de Tela");
        System.Console.WriteLine("Vector2 mousePosition: " + MousePosition);
        // System.Console.WriteLine("X: " + MouseState.X +" | PreviousX: "+ MouseState.PreviousX);
        System.Console.WriteLine("Vector2i windowSize: " + Size);

        xAnterior = MouseState.X;
        yAnterior = MouseState.Y;
      }
      if (MouseState.IsButtonDown(MouseButton.Left))
      {
        xAtual = MouseState.X;
        yAtual = MouseState.Y;
        // Anterior < Atual = gira Anti-horário ("pra direita/pra cima")
        // Anterior > Atual = gira horário ("pra esquerda/pra baixo")

        // vai pegar um angulo entre 0 e 360°
        // multiplicando por 0.45f = 800/360 (regra de 3)
        // dividindo isso por 100 pq se n girava o cubo muito rapido
        float incrementoX = (xAnterior - xAtual)*0.0045f;
        float incrementoY = (yAnterior - yAtual)*0.0045f;
        anguloX += incrementoX;
        anguloY += incrementoY;
        // _camera.AtualizarCamera(anguloX, incrementoX, anguloY, incrementoY);
      }
      if (MouseState.IsButtonDown(MouseButton.Right) && objetoSelecionado != null)
      {
        System.Console.WriteLine("MouseState.IsButtonDown(MouseButton.Right)");

        int janelaLargura = Size.X;
        int janelaAltura = Size.Y;
        Ponto4D mousePonto = new Ponto4D(MousePosition.X, MousePosition.Y);
        Ponto4D sruPonto = Utilitario.NDC_TelaSRU(janelaLargura, janelaAltura, mousePonto);

        objetoSelecionado.PontosAlterar(sruPonto, 0);
      }
      if (MouseState.IsButtonReleased(MouseButton.Right))
      {
        System.Console.WriteLine("MouseState.IsButtonReleased(MouseButton.Right)");
      }

      #endregion

    }

    protected override void OnResize(ResizeEventArgs e)
    {
      base.OnResize(e);

      GL.Viewport(0, 0, Size.X, Size.Y);
    }

    protected override void OnUnload()
    {
      mundo.OnUnload();

      GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
      GL.BindVertexArray(0);
      GL.UseProgram(0);

      GL.DeleteBuffer(_vertexBufferObject_sruEixos);
      GL.DeleteVertexArray(_vertexArrayObject_sruEixos);

      GL.DeleteProgram(_shaderBranca.Handle);
      GL.DeleteProgram(_shaderVermelha.Handle);
      GL.DeleteProgram(_shaderVerde.Handle);
      GL.DeleteProgram(_shaderAzul.Handle);
      GL.DeleteProgram(_shaderCiano.Handle);
      GL.DeleteProgram(_shaderMagenta.Handle);
      GL.DeleteProgram(_shaderAmarela.Handle);

      base.OnUnload();
    }

#if CG_Gizmo
    private void Gizmo_Sru3D()
    {
#if CG_OpenGL && !CG_DirectX
      var model = Matrix4.Identity;
      GL.BindVertexArray(_vertexArrayObject_sruEixos);
      // EixoX
      _shaderVermelha.SetMatrix4("model", model);
      _shaderVermelha.SetMatrix4("view", _camera.GetViewMatrix());
      _shaderVermelha.SetMatrix4("projection", _camera.GetProjectionMatrix());
      _shaderVermelha.Use();
      GL.DrawArrays(PrimitiveType.Lines, 0, 2);
      // EixoY
      _shaderVerde.SetMatrix4("model", model);
      _shaderVerde.SetMatrix4("view", _camera.GetViewMatrix());
      _shaderVerde.SetMatrix4("projection", _camera.GetProjectionMatrix());
      _shaderVerde.Use();
      GL.DrawArrays(PrimitiveType.Lines, 2, 2);
      // EixoZ
      _shaderAzul.SetMatrix4("model", model);
      _shaderAzul.SetMatrix4("view", _camera.GetViewMatrix());
      _shaderAzul.SetMatrix4("projection", _camera.GetProjectionMatrix());
      _shaderAzul.Use();
      GL.DrawArrays(PrimitiveType.Lines, 4, 2);

#elif CG_DirectX && !CG_OpenGL
      Console.WriteLine(" .. Coloque aqui o seu código em DirectX");
#elif (CG_DirectX && CG_OpenGL) || (!CG_DirectX && !CG_OpenGL)
      Console.WriteLine(" .. ERRO de Render - escolha OpenGL ou DirectX !!");
#endif
    }
#endif    

  }
}
