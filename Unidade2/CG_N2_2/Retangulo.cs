#define CG_Debug

using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;

namespace gcgcg
{
  internal class Retangulo : Objeto
  {
    public Retangulo(Objeto _paiRef, ref char _rotulo) : this(_paiRef, ref _rotulo, new Ponto4D(-0.5,-0.5), new Ponto4D(0.5,0.5)) {
      
    }

    public Retangulo(Objeto _paiRef, ref char _rotulo, Ponto4D ptoInfEsq, Ponto4D ptoSupDir) : base(_paiRef, ref _rotulo)
    {
      PrimitivaTipo = PrimitiveType.Points;
      PrimitivaTamanho = 10;

      // Sentido horário
      PontosAdicionar(ptoInfEsq);
      PontosAdicionar(new Ponto4D(ptoSupDir.X, ptoInfEsq.Y));
      PontosAdicionar(ptoSupDir);
      PontosAdicionar(new Ponto4D(ptoInfEsq.X, ptoSupDir.Y));
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
