using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public Transform PosicaoInicial;
    public LayerMask Barreira_mask;
    public Vector2 Tamanho_gride;
    public float Raio_no;
    public float Distancia;

    No[,] gride;
    public List<No> Trilha;

    float Diametro_no;
    int TamanhoGride_x, TamanhoGride_y;

    private void Start()
    {
        Diametro_no = Raio_no * 2;
        TamanhoGride_x = Mathf.RoundToInt(Tamanho_gride.x / Diametro_no);
        TamanhoGride_y = Mathf.RoundToInt(Tamanho_gride.y / Diametro_no);
        CreateGrid();
    }

    void CreateGrid()
    {
        gride = new No[TamanhoGride_x, TamanhoGride_y];
        Vector3 bottomLeft = transform.position - Vector3.right * Tamanho_gride.x / 2 - Vector3.forward * Tamanho_gride.y / 2;
        for(int x = 0; x < TamanhoGride_x; x++)
        {
            for (int y = 0; y < TamanhoGride_y; y++)
            {
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * Diametro_no + Raio_no) + Vector3.forward * (y * Diametro_no + Raio_no);
                bool Obstrucao = true;

                if (Physics.CheckSphere(worldPoint, Raio_no, Barreira_mask))
                {
                    Obstrucao = false;
                }

                gride[x, y] = new No(Obstrucao, worldPoint, x, y);
            }
        }
    }

    public No PosicaoNoMundo(Vector3 arg_PosicaoMundo)
    {
        float ponto_x = ((arg_PosicaoMundo.x + Tamanho_gride.x / 2) / Tamanho_gride.x);
        float ponto_y = ((arg_PosicaoMundo.z + Tamanho_gride.y / 2) / Tamanho_gride.y);

        ponto_x = Mathf.Clamp01(ponto_x);
        ponto_y = Mathf.Clamp01(ponto_y);

        int x = Mathf.RoundToInt((TamanhoGride_x - 1) * ponto_x);
        int y = Mathf.RoundToInt((TamanhoGride_y - 1) * ponto_y);

        return gride[x, y];
    }

    public List<No> GetNosVizinhos (No arg_No)
    {

        List<No> NosVizinhos = new List<No> ();
        int xCheck;
        int yCheck;

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }

                xCheck = arg_No.X_Gride + x;
                yCheck = arg_No.Y_Gride + y;

                if (xCheck >= 0 && xCheck < TamanhoGride_x && yCheck >= 0 && yCheck < TamanhoGride_y)
                {
                    NosVizinhos.Add(gride[xCheck, yCheck]);
                }

            }
        }

        return NosVizinhos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(Tamanho_gride.x, 1, Tamanho_gride.y));
        if (gride != null)
        {
            foreach (No n in gride)
            {
                if (n.Obstruido)
                {
                    Gizmos.color = Color.white;
                }
                else
                {
                    Gizmos.color = Color.yellow;
                }

                if (n.Visitado)
                {
                    Gizmos.color = Color.blue;
                }

                if (Trilha != null)
                {
                    if (Trilha.Contains(n))
                    {
                        Gizmos.color = Color.red;
                    }
                }

                Gizmos.DrawCube(n.Posicao, Vector3.one * (Diametro_no - Distancia));
            }
        }
    }

}
