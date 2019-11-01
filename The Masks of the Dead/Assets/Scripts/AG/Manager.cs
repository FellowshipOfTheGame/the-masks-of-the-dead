using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    [Header("Parametros geneticos")]
    [SerializeField] int maxHeuristica = 2500;
    [SerializeField] int tamanhoPopulacao = 5;
    [SerializeField] float taxaMutacao = 0.10f;

    private AG<float> ag;
    private System.Random random;

	void Start () {
        random = new System.Random();
        ag = new AG<float>(tamanhoPopulacao, 60 * 25/*Tamanho grid*/, random, GeraHeuristica, FuncaoFitness, taxaMutacao);
	}
	
	void Update () {
        //if todos receberam fitness
        ag.NovaGeracao();
	}

    private float GeraHeuristica()
    {
        float valor = random.Next(maxHeuristica)/100;
        return valor;
    }

    private float FuncaoFitness(int index)
    {
        // Desenvolver lógica específica aqui
        return 0;
    }
}
