using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HeuristicaConstrutiva;

namespace QuadroHorarios
{
    public class ConstroiQuadroHorario : HeuristicaConstrutiva.HeuristicaConstrutiva
    {

        public QHorarios quadroHorarios { get; set; }
        public List<Horario> horarios { get; set; }
        public List<Professor> professores { get; set; }
        List<IComponente> alocPoss { get; set; }
        int cont;

        public QHorarios quadro
        {
            get
            {
                return (QHorarios)Solucao;
            }
        }

        public ConstroiQuadroHorario()
        {
            horarios = new List<Horario>();
            professores = new List<Professor>();            
            cont = 0;
            
        }
        
        public override List<IComponente> GerarComponentes()
        {
            alocPoss = new List<IComponente>();
            // gera uma lista de componentes possiveis, ou seja, professores que não tenham restrição ao horario e não esteja com a alocação completa
            for (int i = 0; i < professores.Count(); i++ )
            {
                if (!RestrincaoHorario(professores[i], Solucao.Componentes.Count()) && CargaHorariaIncompleta(professores[i]))
                    alocPoss.Add(new Alocacao(professores[i],horarios[Solucao.Componentes.Count()]));                
            }
                      
            return alocPoss;
        }

        public override IComponente EscolheMelhorComponente(List<IComponente> Componentes)
        {
            Alocacao alocar = new Alocacao();
            int pos = 0;
            int memPos = -1;
            int numRestricoes = -1;

            foreach (Alocacao aloc in Componentes)
            {
                // seleciona na lista a alocação em que o professor tem maior numero de restições a outros horários
                if (aloc.professor.restricoes.Count() > numRestricoes && CargaHorariaIncompleta(aloc.professor))
                {
                    numRestricoes = aloc.professor.restricoes.Count();
                    memPos = pos;
                }
                pos++;
            }

            if (memPos > -1)
                alocar = (Alocacao)Componentes[memPos];
            else
            {
                // chama a função que realoca um professor quando existe professores com alocação incompleta e restrição aos horarios disponiveis
                alocar = Realocacao();
                if (alocar == null)
                    System.Console.Write("Não foi possivel gerar uma solução.\n\n");
            }
            
            return alocar;
        }

        Alocacao Realocacao()
        {
            Alocacao al = new Alocacao();
            List<Professor> cargaInc = new List<Professor>();

            for (int i = 0; i<professores.Count(); i++)
            {
                // adciona na lista professores com a carga horaria incompleta
                if (CargaHorariaIncompleta(professores[i]))
                    cargaInc.Add(professores[i]);
            }
            
            for (int i = 0; i < Solucao.Componentes.Count(); i++ )
            {
                Alocacao aloc = (Alocacao)Solucao.Componentes[i];
                for (int j=0; j<cargaInc.Count(); j++)
                {
                    // tenta trocar um professor alocado por um disponivel na lista de componentes da solução
                    if (!aloc.horario.restHorario.Contains(cargaInc[j]))
                    {
                        Solucao.Componentes.Remove(Solucao.Componentes[i]);
                        Solucao.Componentes.Insert(i, new Alocacao(cargaInc[j],aloc.horario));
                        return (new Alocacao(aloc.professor, horarios[Solucao.Componentes.Count()]));
                    }
                }                
            }
            return al;
        }

        public override ISolucao CriaSolucaoVazia()
        {
            quadroHorarios = new QHorarios();
            return quadroHorarios;
        }

        Boolean RestrincaoHorario(Professor p, int i)
        {
            // verifica se o professor tem restrição ao horario especifico da lista passado pelo indice i
            if (p.restricoes.Contains(horarios[i]))
                return true;
            else    
                return false; ;
        }

        Boolean CargaHorariaIncompleta(Professor p)
        {
            int alocAulas = 0;
            
            // verifica a quantidade de alocação do professor
            foreach (Alocacao aloc in Solucao.Componentes)
            {                
                if (aloc.professor.nome == p.nome)
                    alocAulas++;
            }

            if (alocAulas < 2 )
                return true;
            else
                return false;
        }

        public override bool VerificaSolucaoCompleta()
        {
            if (Solucao.Componentes.Count()<10)
                return false;
            else
                return true;
        }

        public void AddProfessores(Professor p)
        {
            if (!professores.Contains(p))
                professores.Add(p);
        }

        public void RemoveProfessores(Professor p)
        {
            if (professores.Contains(p))
                professores.Remove(p);
        }

        public void AddHorarios(Horario h)
        {
            if (!horarios.Contains(h))
            horarios.Add(h);
            
        }

        public void RemoveHorarios(Horario h)
        {
            if (horarios.Contains(h))
                horarios.Remove(h);
        }

        public void AddRestricoes(Professor prof, Horario hor)
        {
            int i=0;
            while (professores[i].nome != prof.nome)
                i++;

            professores[i].AddRestrincoes(hor);
            i = 0;
            while (horarios[i].ident != hor.ident)
                i++;

            horarios[i].AddRestHorario(prof);
        }
    }
}
