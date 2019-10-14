using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour {

    Grid gride;
    public Transform PosInicial;
    public Transform PosFinal;

    private void Awake()
    {
        gride = GetComponent<Grid>();
    }

    private void Update()
    {
        EncontraCaminho(PosInicial.position, PosFinal.position);
    }

    void EncontraCaminho (Vector3 arg_PosInicial, Vector3 arg_PosFinal)
    {
        No No_inicial = gride.PosicaoNoMundo(arg_PosInicial);
        No No_final = gride.PosicaoNoMundo(arg_PosFinal);

        List<No> Lista_aberta = new List<No> ();
        HashSet<No> Lista_fechada = new HashSet<No>();

        Lista_aberta.Add(No_inicial);

        while (Lista_aberta.Count > 0)
        {
            No No_atual = Lista_aberta[0];
            for (int i = 1; i < Lista_aberta.Count; i++)
            {
                if (Lista_aberta[i].fCusto <= No_atual.fCusto && Lista_aberta[i].hCusto < No_atual.hCusto)
                {
                    No_atual = Lista_aberta[i];
                }
            }
            Lista_aberta.Remove(No_atual);
            Lista_fechada.Add(No_atual);

            if (No_atual == No_final)
            {
                CalcularTrajeto(No_inicial, No_final);
            }

            foreach (No Vizinho in gride.GetNosVizinhos(No_atual))
            {
                if(!Vizinho.Obstruido || Lista_fechada.Contains(Vizinho))
                {
                    continue;
                }

                int Custo = No_atual.gCusto + DistanciaManhattan(No_atual, Vizinho);

                if(Custo < Vizinho.fCusto || !Lista_aberta.Contains(Vizinho))
                {
                    Vizinho.gCusto = Custo;
                    Vizinho.hCusto = DistanciaManhattan(Vizinho, No_final);
                    Vizinho.Pai = No_atual;

                    if (!Lista_aberta.Contains(Vizinho))
                    {
                        Lista_aberta.Add(Vizinho);
                    }
                }
            }
        }
    }

    void CalcularTrajeto (No arg_NoInicial, No arg_NoFinal)
    {
        List<No> Trajeto = new List<No>();
        No No_atual = arg_NoFinal;

        while (No_atual != arg_NoInicial)
        {
            Trajeto.Add(No_atual);
            No_atual = No_atual.Pai;
        }

        Trajeto.Reverse();

        gride.Trilha = Trajeto;
    }

    int DistanciaManhattan(No arg_Noa, No arg_NoB)
    {
        int ix = Mathf.Abs(arg_Noa.X_Gride - arg_NoB.X_Gride);
        int iy = Mathf.Abs(arg_Noa.Y_Gride - arg_NoB.Y_Gride);

        return ix + iy;
    }
}
