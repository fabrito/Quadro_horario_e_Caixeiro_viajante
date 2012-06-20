using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HeuristicaMelhoria;

namespace CaixeiroViajante
{
    class MelhoraViagem : HeuristicaMelhoria.HeuristicaMelhoria
    {
        public override List<HeuristicaConstrutiva.ISolucao> GerarVizinhanca()
        {
            throw new NotImplementedException();
        }

        public override HeuristicaConstrutiva.ISolucao EscolheMelhorVizinho(List<HeuristicaConstrutiva.ISolucao> vizinhos)
        {
            throw new NotImplementedException();
        }

        public override HeuristicaConstrutiva.ISolucao CriaSolucaoInicial()
        {
            throw new NotImplementedException();
        }

        public override bool VerificaCondicaoParada()
        {
            throw new NotImplementedException();
        }
    }
}
