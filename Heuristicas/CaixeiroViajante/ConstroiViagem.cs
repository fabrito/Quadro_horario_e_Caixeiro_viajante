using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HeuristicaConstrutiva;

namespace CaixeiroViajante
{
    public class ConstroiViagem : HeuristicaConstrutiva.HeuristicaConstrutiva
    {
        public List<IComponente> destPossiveis { get; set; }
        public Viagem rota { get; set; }
        public List<IComponente> destinos { get; set; }
        private static Boolean solucaoIn; // marca se o primeiro componente foi add na lista de destino

        public Viagem Viagen
        {
            get
            {
                return (Viagem)Solucao;
            }
        }


        public ConstroiViagem()
        {
            destPossiveis = new List<IComponente>();
            destinos = new List<IComponente>();
            solucaoIn = false;
        }

        public override List<IComponente> GerarComponentes()
        {           
            return destPossiveis;
        }

        public void AddComponente(IComponente Componente)
        {
            if (Componente == null)
                return;
            Acesso aresta = (Acesso)Componente;
            
            if (!destPossiveis.Contains(Componente))
                destPossiveis.Add(aresta);
        }

        public override IComponente EscolheMelhorComponente(List<IComponente> Componentes)
        {
            Acesso fimRota = (Acesso)Viagen.Componentes.LastOrDefault();// cidade atual
            Acesso temp = new Acesso();
            Acesso menor = new Acesso();
            Boolean cidVisit;

            foreach (Acesso acesso in Componentes)
            {
                // quando não tiver inserido o primeiro componente na solução
                if (solucaoIn == false)
                {
                    foreach (Acesso a in Componentes)
                    {
                        if (menor.km == -1 && a.cidPartida == rota.cidPartida || (a.km < menor.km && a.cidPartida == rota.cidPartida))
                            menor = a;
                    }
                }
                    // depois que inserir o primeiro componente na solução
                else
                {
                    if (acesso.cidPartida == fimRota.cidDestino && acesso.cidDestino != rota.cidPartida)
                    {
                        if (menor.km == -1 || acesso.km < menor.km)
                        {
                            // verifica se cidade ja foi visitada
                            cidVisit = false;
                            foreach (Acesso tmp in Viagen.Componentes)
                            {
                                if (tmp.cidPartida == acesso.cidDestino)
                                {
                                    cidVisit = true;
                                    break;
                                }
                            } // encerra verificação de cidade visitada

                            // se a cidade não foi visitada e a distancia for menor troca o valor a ser retornado 
                            if (cidVisit == false)
                                menor = acesso;
                        }
                    }
                    else if (acesso.cidDestino == rota.cidPartida)
                        temp = acesso;
                }
            }
                // caso não haja outra cidade a ser visitada manda a aresta que encerra a rota.
            if (menor.km == -1 && solucaoIn == true)
                menor = temp;

            solucaoIn = true;    
            return menor;
        }

        public override ISolucao CriaSolucaoVazia()
        {
            rota = new Viagem();
            rota.cidPartida = "Belo Horizonte";
            return rota; 
        }

        public override bool VerificaSolucaoCompleta()
        {
            Acesso fimRota = new Acesso();
            Acesso inRota = new Acesso();          
                
            if (solucaoIn == true)
            {
                fimRota = (Acesso)rota.Componentes.LastOrDefault();// ultimo destino
                if (fimRota.cidDestino == rota.cidPartida)
                    return true;
                else
                    return false;
            }
            else
                return false;            
        }
    }
}
