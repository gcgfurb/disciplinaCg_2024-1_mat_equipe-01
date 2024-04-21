#define CG_Debug

using System;
using System.Collections.Generic;
using System.Security.Principal;
using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;

namespace gcgcg
{
  internal class Spline : Objeto
  {
    private List<Ponto4D> spline;

    private int iMin = 1;

    private int iMax = 10;

    public Spline(Objeto paiRef, ref char _rotulo, Ponto4D pto1, 
                  Ponto4D pto2, Ponto4D pto3, Ponto4D pto4) : base(paiRef, ref _rotulo)
    {
      PrimitivaTipo = PrimitiveType.LineStrip;
      PrimitivaTamanho = 15;

      spline = calculaSpline(pto1, pto2, pto3, pto4);

      for(int i = 0; i < spline.Count; i++) {
        PontosAdicionar(spline[i]);
      }

      Atualizar();
    }

    public void diminuiPontos(Ponto4D pto1, Ponto4D pto2, Ponto4D pto3, Ponto4D pto4) {
      if(iMin <= 5 && iMax >= 5) {
        pontosLista = [];
        iMin++;
        iMax--;

        mudaPontos(pto1, pto2, pto3, pto4);
      }
    }

    public void aumentaPontos(Ponto4D pto1, Ponto4D pto2, Ponto4D pto3, Ponto4D pto4) {
      if(iMin > 1 && iMax < 10) {
        iMin--;
        iMax++;

        mudaPontos(pto1, pto2, pto3, pto4);
      }
    }

    public void mudaPontos(Ponto4D pto1, Ponto4D pto2, Ponto4D pto3, Ponto4D pto4) {
      pontosLista = [];

      spline = calculaSpline(pto1, pto2, pto3, pto4);

      for(int i = 0; i < spline.Count; i++) {
        PontosAdicionar(spline[i]);
      }
    }

    private List<Ponto4D> calculaSpline(Ponto4D pto1, Ponto4D pto2, Ponto4D pto3, Ponto4D pto4){
        List<Ponto4D> spline = [pto4];

        for(int i = iMin; i < iMax; i++) {
          double t = i / 10.0;

          double AB_X = pto4.X + (pto3.X - pto4.X) * t;
          double AB_Y = pto4.Y + (pto3.Y - pto4.Y) * t;
          double BC_X = pto3.X + (pto2.X - pto3.X) * t;
          double BC_Y = pto3.Y + (pto2.Y - pto3.Y) * t;
          double CD_X = pto2.X + (pto1.X - pto2.X) * t;
          double CD_Y = pto2.Y + (pto1.Y - pto2.Y) * t;

          double ABC_X = AB_X + (BC_X - AB_X) * t;
          double ABC_Y = AB_Y + (BC_Y - AB_Y) * t;
          double BCD_X = BC_X + (CD_X - BC_X) * t;
          double BCD_Y = BC_Y + (CD_Y - BC_Y) * t;

          double ABCD_X = ABC_X + (BCD_X - ABC_X) * t;
          double ABCD_Y = ABC_Y + (BCD_Y - ABC_Y) * t;

          spline.Add(new Ponto4D(ABCD_X, ABCD_Y));
        }

        spline.Add(pto1);

        return spline;
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
