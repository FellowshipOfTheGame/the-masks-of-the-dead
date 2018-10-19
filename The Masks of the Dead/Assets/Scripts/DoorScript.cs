using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

    [Range(0.0f, 500.0f)] public float door_speed;//Velocidade que a porta abre.
    [Range(0.0f, 180.0f)] public float max_rotation;//O quando que a porta roda ao abrir.
    public int isopenned = 0; //Se 0, está fechada, se 1, está aberta para o sentido anti-horário, se -1, para o sentido horário.
    int opening = 0; //Se 0, a porta está parada, se 1, está girando no sentido anti-horário, se -1, sentido horário e se 2, está fechando.
    float total_rotation = 0.0f;//O total que rodou durante a animação.

    Vector3 starting_rotation;//Rotação em Eulers
    Vector3 open_rotation;//Guarda a rotação de quando a porta está aberta.
    GameObject player;
    Vector3 door_rotation;//Vetor que inicia na origem e aponta para a ponta da porta.

    private void Start()
    {
        starting_rotation = gameObject.transform.eulerAngles;
        player = GameObject.FindGameObjectWithTag("Player");

        door_rotation = Quaternion.Euler(0, gameObject.transform.eulerAngles.y, 0) * (-Vector3.right);
    }

    private void Update()
    {
        if (opening != 0)
        {
            open_animation(opening);
        }
    }

    public void opendoor()
    {
        if (opening != 0)
            return;
        //Multiplicação vetorial para identificar de qual lado da porta o player se localiza (se < 0, está à direita).
        Vector3 player_location = player.transform.position - gameObject.transform.position;
        float y_result = ((door_rotation.z) * (player_location.x)) - ((door_rotation.x) * (player_location.z));

        if (isopenned != 0)
        {
            open_rotation = gameObject.transform.eulerAngles;
            opening = 2;
        }
        else
        {
            if (y_result >= 0)
            {
                opening = 1;
            }
            else
            {
                opening = -1;
            }
        }
    }

    public void open_animation(int side)
    {
        if(opening == 2)
        {
            gameObject.transform.eulerAngles += new Vector3(0, isopenned * door_speed * Time.deltaTime, 0);
            total_rotation += door_speed * Time.deltaTime;
            if(total_rotation >= max_rotation)
            {
                total_rotation = 0.0f;
                gameObject.transform.eulerAngles = starting_rotation;
                opening = 0;
                isopenned = 0;
            }
        }
        else
        {
            gameObject.transform.eulerAngles += new Vector3(0, -opening * door_speed * Time.deltaTime, 0);
            total_rotation += door_speed * Time.deltaTime;
            if(total_rotation >= max_rotation)
            {
                total_rotation = 0.0f;
                gameObject.transform.eulerAngles = new Vector3(starting_rotation.x, starting_rotation.y + (-opening * max_rotation), starting_rotation.z);
                isopenned = opening;
                opening = 0;
            }
        }
    }

}
