using System.Collections;
using System.Collections.Generic;
using System;

public class Individuo<T> {

    public T[] Genes { get; private set; }
    public float Fitness { get; private set; }

    private Random random;
    private Func<T> GeraGeneAleatorio;
    private int pontos;
    private int nosPercorridos;
    Func<int, float> FuncaoFitness;

    public Individuo(int tamanho, Random random, Func<T> GeraGeneAleatorio, Func<int, float> FuncaoFitness, bool Inicializa = true)
    {
        Genes = new T[tamanho];
        this.random = random;
        this.GeraGeneAleatorio = GeraGeneAleatorio;
        this.FuncaoFitness = FuncaoFitness;
        pontos = 0;
        nosPercorridos = 0;

        if (Inicializa)
        {
            for (int i = 0; i < Genes.Length; i++)
            {
                Genes[i] = GeraGeneAleatorio();
            }
        }
    }

    public float CalculoFitness(int indice)
    {
        Fitness = FuncaoFitness(indice);
        return Fitness;
    }

    public Individuo<T> Crossover(Individuo<T> SegundoPai)
    {
        Individuo<T> Filho = new Individuo<T>(Genes.Length, random, GeraGeneAleatorio, FuncaoFitness, Inicializa: false);

        for (int i = 0; i < Genes.Length; i++)
        {
            Filho.Genes[i] = random.NextDouble() < 0.5 ? Genes[i] : SegundoPai.Genes[i];
        }

        return Filho;
    }

    public void Mutacao(float Taxa)
    {
        for(int i = 0; i < Genes.Length; i++)
        {
            if(random.NextDouble() < Taxa)
            {
                Genes[i] = GeraGeneAleatorio();
            }
        }
    }

    public void Pontua(int pontos)
    {
        pontos += pontos;
    }

    public void PercorreuNos(int num_nos)
    {
        nosPercorridos += num_nos;
    }

    public int getPontos()
    {
        return pontos;
    }

    public int getTamanhoCaminho()
    {
        return nosPercorridos;
    }
}