#define CG_Debug

using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;

namespace gcgcg
{
  internal class SrPalito : Objeto
  {

    public SrPalito(Objeto _paiRef, ref char _rotulo) : this(_paiRef, ref _rotulo, new Ponto4D(0.0, 0.0, 0.0))
    {

    }

    public SrPalito(Objeto _paiRef, ref char _rotulo, Ponto4D inicio) : base(_paiRef, ref _rotulo)
    {
      PrimitivaTipo = PrimitiveType.Lines;
      PrimitivaTamanho = 1;

      base.PontosAdicionar(inicio);
      base.PontosAdicionar(new Ponto4D(0.5, 0.5, 0.0));

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
      retorno = "__ Objeto Sr Palito _ Tipo: " + PrimitivaTipo + " _ Tamanho: " + PrimitivaTamanho + "\n";
      retorno += base.ImprimeToString();
      return (retorno);
    }
#endif

  }
}
