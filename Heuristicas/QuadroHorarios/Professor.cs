using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HeuristicaConstrutiva;

namespace QuadroHorarios
{
    public class Professor
    {
        public string nome { get; set; }
        public List<Horario> restricoes { get; set; }

        public Professor()
        {
            restricoes = new List<Horario>();
        }

        public Professor(string n)
        {
            restricoes = new List<Horario>();
            nome = n;
        }
        
        public void AddRestrincoes(Horario h)
        {
            if (restricoes != null)
                restricoes.Add(h);
        }

        public void RemoveRestrincoes(Horario h)
        {
            if (restricoes != null)
                restricoes.Remove(h);
        }
    }
}
