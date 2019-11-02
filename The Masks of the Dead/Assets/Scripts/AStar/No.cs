using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class No {

    public int X_Gride;
    public int Y_Gride;

    public bool Obstruido;
    public bool Visitado;
    public Vector3 Posicao;

    public int hCusto;
    public int gCusto;
    public int pCusto;

    public No Pai;

    public int fCusto { get { return hCusto + gCusto - pCusto; } }

    public No (bool arg_Obs, Vector3 arg_Pos, int arg_XGride, int arg_YGride)
    {
        Obstruido = arg_Obs;
        Posicao = arg_Pos;
        X_Gride = arg_XGride;
        Y_Gride = arg_YGride;
        pCusto = 0;
        Visitado = false;
    }

}
