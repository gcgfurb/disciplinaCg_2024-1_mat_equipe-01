#define CG_Debug

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using CG_Biblioteca;
using OpenTK.Graphics.OpenGL4;

namespace gcgcg
{
  internal class Circulo : Objeto
  {
    public Circulo(Objeto _paiRef, ref char _rotulo) : this(_paiRef, ref _rotulo, 0.2, 0.2) {
      
    }

    public Circulo(Objeto _paiRef, ref char _rotulo, double raio, double desloc) : base(_paiRef, ref _rotulo)
    {
      PrimitivaTipo = PrimitiveType.LineLoop;
      PrimitivaTamanho = 2;

      double ang = 360 / 72;
      double add = ang;
      
      for(int i = 0; i < 72; i++) {
        PontosAdicionar(new Ponto4D(Matematica.GerarPtosCirculo(ang, raio).X+desloc, 
                                         Matematica.GerarPtosCirculo(ang, raio).Y+desloc));
        ang += add;
      }

      Atualizar();
    }

    public void deslocaCirculo(double deslocX, double deslocY) {
      List<Ponto4D> novosPontos = [];

      for(int i = 0; i < pontosLista.Count; i++) {
        novosPontos.Add(new Ponto4D(pontosLista[i].X+deslocX, pontosLista[i].Y+deslocY));
      }

      pontosLista = [];
      
      for(int i = 0; i < novosPontos.Count; i++) {
        PontosAdicionar(novosPontos[i]);
      }
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
