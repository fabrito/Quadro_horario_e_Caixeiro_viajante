using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HeuristicaConstrutiva;

namespace QuadroHorarios
{
    public class Alocacao : IComponente
    {
        public Professor professor { get; set; }
        public Horario horario { get; set; }

        public Alocacao()
        {
            professor = new Professor();
            horario = new Horario();
        }

        public Alocacao(Professor p, Horario h)
        {
            professor = p;
            horario = h;
        }
        public object Valor
        {
            get { throw new NotImplementedException(); }
        }
    }
}
