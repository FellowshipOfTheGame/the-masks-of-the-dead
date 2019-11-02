using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    [Header("Parametros geneticos")]
    [SerializeField] int maxHeuristica = 25;
    [SerializeField] int tamanhoPopulacao = 5;
    [SerializeField] float taxaMutacao = 0.10f;

    private AG<int> ag;
    private System.Random random;
    private bool TodosAvaliados;

	void Awake () {
        random = new System.Random();
        ag = new AG<int>(tamanhoPopulacao, 60 * 25/*Tamanho grid*/, random, GeraHeuristica, FuncaoFitness, taxaMutacao);
        TodosAvaliados = false;
	}
	
	void Update () {
        //if todos receberam fitness
        if (TodosAvaliados)
        {
            print("Evoluindo...");
            ag.NovaGeracao();
            TodosAvaliados = false;
        }
	}

    private int GeraHeuristica()
    {
        int valor = random.Next(maxHeuristica);
        return valor;
    }

    private float FuncaoFitness(int index)
    {
        return (ag.Populacao[index].getPontos()/(ag.Populacao[index].getTamanhoCaminho() != 0 ? ag.Populacao[index].getTamanhoCaminho() : 10000));
    }

    public AG<int> getAG()
    {
        return ag;
    }

    public void avisoAvaliacao()
    {
        this.TodosAvaliados = true;
    }
}
