using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Zombie : MonoBehaviour {

	public GameObject player;
	public GameObject camera;
	public GameObject cameraDead;

    public float cLowerDist = 1.4f;
    public float cBiggerDist = 1.8f;
    public float sLowerDist = 1.6f;
    public float sBiggerDist = 2.5f;

	private float lowerDist;
	private float biggerDist;

	public Material green;
	public Material yellow;
	public Material red;

    [Tooltip("Os pontos para os quais os zumbis vao andar, na ordem que sao colocados. (apos o ultimo, ele retorna a posicao inicial.")]
    public Vector3[] destination;
    [Tooltip("O tempo que o zumbi fica parado em cada ponto de destino (deve ter um tempo para cada ponto)")]
    public int[] waiting_time;
    bool is_waiting = true;
    float time_waiting = 0.0f;
    Vector3 starting_location;//Onde o zumbi inicia.
    Vector3 destiny;//O próximo destino do zumbi.
    int i = 0;//Posição do destino no vetor direction
    Vector3 direction;//Direção que o zumbi anda para chegar no destino.
    [Range(0.0f, 10.0f)] public float speed;

	private GameObject body;
	private string state;

	public bool isDead = false;

    public AudioClip grunhido;
    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		body = transform.GetChild(0).gameObject;
		body.GetComponent<Renderer>().material = green;
		state = "green";
        starting_location = gameObject.transform.position;
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if(player.GetComponent<ThirdPersonCharacter>().m_Crouching)
        {
            lowerDist = cLowerDist;
            biggerDist = cBiggerDist;
        }else
        {
            lowerDist = sLowerDist;
            biggerDist = sBiggerDist;
        }

        if(state == "yellow")
        {
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }else
        {
            if(audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

		howNear();

		if(isDead){
			if(Input.GetKeyDown(KeyCode.Return)){
				player.transform.SetPositionAndRotation(new Vector3(3.4f, 0f, -8.35f), Quaternion.Euler(new Vector3(0,90,0)));
                camera.SetActive(true);
                cameraDead.SetActive(false);
				/*camera.GetComponent<Camera>().enabled = true;
				cameraDead.GetComponent<Camera>().enabled = false;*/
				cameraDead.transform.position = new Vector3(-10, 7, 10);
				isDead = false;
			}
		}
        
        //movimentação do zombie
        if(destination.Length != 0)
        {
            if (is_waiting)
            {
                wait(i);
                if(!is_waiting)
                {
                    if(i < destination.Length)
                    {
                        destiny = destination[i];
                        i++;
                    }
                    else
                    {
                        i = 0;
                        destiny = starting_location;
                    }
                    time_waiting = 0;
                    direction = (destiny - gameObject.transform.position).normalized;
                }
            }else
            {
                gameObject.transform.Translate(direction * speed * Time.deltaTime);
                if ((gameObject.transform.position - destiny).sqrMagnitude <= (direction * speed * Time.deltaTime).sqrMagnitude)
                {

                    gameObject.transform.position = destiny;
                    is_waiting = true;
                }
            }
        }
	}

    void wait(int i)
    {
        time_waiting += Time.deltaTime;
        if(time_waiting >= waiting_time[i])
        {
            is_waiting = false;
        }
    }

	void howNear(){
        Vector3 playerPos = player.transform.position;
        Vector3 myPos = transform.position;
		float dist = Vector3.Distance(myPos, playerPos);

		//Debug.Log(dist);

        if(dist < lowerDist){
            if(state != "red"){
				body.GetComponent<Renderer>().material = red;
				state = "red";
				respawn();
				//Debug.Log("dead");
			}
        } else if(dist < biggerDist){
			if(state != "yellow"){
				body.GetComponent<Renderer>().material = yellow;
				state = "yellow";
				//Debug.Log("zombie is noticing the player");
			}
		} else {
			if(state != "green"){
				body.GetComponent<Renderer>().material = green;
				state = "green";
				//Debug.Log("not dead anymore");
			}
		}
    }

	void respawn(){
		cameraDead.transform.position = new Vector3(player.transform.position.x, cameraDead.transform.position.y, player.transform.position.z);
        cameraDead.SetActive(true);
        camera.SetActive(false);
        /*
        cameraDead.GetComponent<Camera>().enabled = true;
		camera.GetComponent<Camera>().enabled = false;*/
		player.transform.SetPositionAndRotation(new Vector3(-27f, 0f, -8.35f), Quaternion.Euler(new Vector3(0,90,0)));
		isDead = true;
	}
}
