using System;
using System.Collections.Generic;

public class AG<T> {
    public List<Individuo<T>> Populacao { get; private set; }
    public int Geracao { get; private set; }
    public float MelhorFitness { get; private set; }
    public T[] MelhorGene { get; private set; }

    public float TaxaMutacao;

    private Random random;
    private float SomaFitness;

    public AG(int tamanhoPopulacao, int individuoTamanho, Random random, Func<T> GeraGeneAleatorio, Func<int, float> FuncaoFitness, float taxaMutacao = 0.01f)
    {
        Geracao = 1;
        TaxaMutacao = taxaMutacao;
        Populacao = new List<Individuo<T>>();
        this.random = random;

        MelhorGene = new T[individuoTamanho];

        for(int i = 0; i < tamanhoPopulacao; i++)
        {
            Populacao.Add(new Individuo<T>(individuoTamanho, random, GeraGeneAleatorio, FuncaoFitness, Inicializa: true));
        }
    }

    public void NovaGeracao()
    {
        if(Populacao.Count <= 0)
        {
            return;
        }

        CalculaFitness();

        List<Individuo<T>> novaPopulacao = new List<Individuo<T>>();

        for(int i = 0; i < Populacao.Count; i++)
        {
            Individuo<T> Pai1 = EscolhePai();
            Individuo<T> Pai2 = EscolhePai();

            Individuo<T> Filho = Pai1.Crossover(Pai2);

            Filho.Mutacao(TaxaMutacao);

            novaPopulacao.Add(Filho);
        }

        Populacao = novaPopulacao;
        Geracao++;
    }

    public void CalculaFitness()
    {
        SomaFitness = 0;
        Individuo<T> Melhor = Populacao[0];

        for(int i = 0; i < Populacao.Count; i++)
        {
            SomaFitness += Populacao[i].CalculoFitness(i);

            if(Populacao[i].Fitness > Melhor.Fitness)
            {
                Melhor = Populacao[i];
            }
        }

        MelhorFitness = Melhor.Fitness;
        Melhor.Genes.CopyTo(MelhorGene, 0);
    }

    private Individuo<T> EscolhePai()
    {
        double Aleatoriedade = random.NextDouble() * SomaFitness;

        for (int i = 0; i < Populacao.Count; i++)
        {
            if(Aleatoriedade < Populacao[i].Fitness)
            {
                return Populacao[i];
            }

            Aleatoriedade -= Populacao[i].Fitness;
        }

        return Populacao[random.Next(Populacao.Count - 1)];
    }
}