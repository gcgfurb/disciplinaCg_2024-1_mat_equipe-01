#define CG_Debug

using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;

namespace gcgcg
{
  internal class Circulo : Objeto
  {
    public Circulo(Objeto _paiRef, ref char _rotulo) : this(_paiRef, ref _rotulo, 0.5) {
      
    }

    public Circulo(Objeto _paiRef, ref char _rotulo, double raio) : base(_paiRef, ref _rotulo)
    {
      PrimitivaTipo = PrimitiveType.Points;
      PrimitivaTamanho = 10;

      double ang = 360 / 72;
      double add = ang;
      
      for(int i = 0; i < 72; i++) {
        base.PontosAdicionar(Matematica.GerarPtosCirculo(ang, raio));
        ang += add;
      }

      Atualizar();
    }

    private void Atualizar()
    {
      base.ObjetoAtualizar();
    }

#if CG_Debug
    public override string ToString()
    {
      string retorno;
      retorno = "__ Objeto Retangulo _ Tipo: " + PrimitivaTipo + " _ Tamanho: " + PrimitivaTamanho + "\n";
      retorno += base.ImprimeToString();
      return (retorno);
    }
#endif

  }
}
