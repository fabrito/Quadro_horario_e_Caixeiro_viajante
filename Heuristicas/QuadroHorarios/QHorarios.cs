using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HeuristicaConstrutiva;

namespace QuadroHorarios
{
    public class QHorarios : ISolucao
    {
        public List<IComponente> Componentes
        {
            get;
            set;
            
        }

        public float Avaliacao
        {
            get { return (float)Componentes.Count(); }
        }

        public void AddComponente(IComponente Componente)
        {
            if (!Componentes.Contains(Componente))
                Componentes.Add(Componente);
        }

        public void RemoveComponente(IComponente Componente)
        {
            if (Componentes.Contains(Componente))
                Componentes.Remove(Componente);
        }

        public QHorarios()
        {
            Componentes = new List<IComponente>();
        }
    }
}
