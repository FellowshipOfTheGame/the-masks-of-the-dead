using System.Collections;
using System.Collections.Generic;
using System;

public class Individuo<T> {

    public T[] Genes { get; private set; }
    public float Fitness { get; private set; }

    private Random random;
    private Func<T> GeraGeneAleatorio;
    Func<float, int> FuncaoFitness;

    public Individuo(int tamanho, Random random, Func<T> GeraGeneAleatorio, Func<float, int> FuncaoFitness, bool Inicializa = true)
    {
        Genes = new T[tamanho];
        this.random = random;
        this.GeraGeneAleatorio = GeraGeneAleatorio;
        this.FuncaoFitness = FuncaoFitness;

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
}
