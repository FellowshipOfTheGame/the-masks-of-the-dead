using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Zombie : MonoBehaviour {

	public GameObject player;
	public GameObject camera;
	public GameObject cameraDead;
    public GameObject pathManager;

    public float cLowerDist = 1.4f;
    public float cBiggerDist = 1.8f;
    public float sLowerDist = 1.6f;
    public float sBiggerDist = 2.5f;

	private float lowerDist;
	private float biggerDist;
    private List<No> Caminho;

	public Material green;
	public Material yellow;
	public Material red;

	[SerializeField] public Animator apaga;

    [Tooltip("Os pontos para os quais os zumbis vao andar, na ordem que sao colocados. (apos o ultimo, ele retorna a posicao inicial.")]
    public Vector3[] destination;
    [Tooltip("O tempo que o zumbi fica parado em cada ponto de destino (deve ter um tempo para cada ponto)")]
    public int[] waiting_time;
    bool is_waiting = true;
    float time_waiting = 0.0f;
    Vector3 starting_location;//Onde o zumbi inicia.
    Vector3 destiny;//O próximo destino do zumbi.
    int i = 0;//Posição do destino no vetor direction
    int waypoint_index = 0; //Índice do vetor de possíveis destinos
    private Vector3 direction;//Direção que o zumbi anda para chegar no destino.
    [Range(0.0f, 10.0f)] public float speed;

    private enum modo
    {
        PATRULHA,
        PERSEGUINDO,
        PROCURANDO
    }

    private modo estado;
    private GameObject body;
	private string state;

	public bool isDead = false;

    public AudioClip grunhido;
    public float maxVolume = 1.0f;
    public float changingSpeed = 1.0f;
    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        estado = modo.PATRULHA;
		Cursor.visible = false;
		body = transform.GetChild(0).gameObject;
		state = "green";
        starting_location = gameObject.transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        //Detecta que distancia será usada (em pé, ou agaichado)
        if (player.GetComponent<ThirdPersonCharacter>().m_Crouching)
        {
            lowerDist = cLowerDist;
            biggerDist = cBiggerDist;
        } else
        {
            lowerDist = sLowerDist;
            biggerDist = sBiggerDist;
        }

        //Ajusta o som do grunhido do zumbi.
        if (state == "yellow")
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            if (audioSource.volume < maxVolume)
            {
                audioSource.volume += changingSpeed * Time.deltaTime;
            }
        } else
        {
            if (audioSource.isPlaying)
            {
                if (audioSource.volume <= 0)
                {
                    audioSource.volume = 0;
                    audioSource.Stop();
                } else
                {
                    audioSource.volume -= changingSpeed * Time.deltaTime;
                }
            }
        }

        howNear();

        if (isDead) {
            if (Input.GetKeyDown(KeyCode.Return)) {
                player.transform.SetPositionAndRotation(new Vector3(-25f, 0f, 0f), Quaternion.Euler(new Vector3(0, 90, 0)));
                camera.SetActive(true);
                cameraDead.GetComponent<AudioListener>().enabled = false;
                camera.GetComponent<AudioListener>().enabled = true;
                cameraDead.SetActive(false);
                /*camera.GetComponent<Camera>().enabled = true;
				cameraDead.GetComponent<Camera>().enabled = false;*/
                cameraDead.transform.position = new Vector3(-10, 7, 10);
                player.GetComponent<Animator>().enabled = true;
                isDead = false;

                this.transform.GetChild(0).gameObject.SetActive(true);
                player.transform.GetChild(0).gameObject.GetComponent<AudioSource>().pitch = 1.0f;
            }
        }

        if (GetComponent<Zombie_sight>().playerInSight)
        {
            if (estado != modo.PERSEGUINDO)
            {
                speed = 4.0f;
                GetComponent<Zombie_Animation>().animSpeed = 7.0f;
                estado = modo.PERSEGUINDO;
                pathManager.GetComponent<Pathfinding>().PosFinal.localPosition = player.transform.position;
            }
        }
        else
        {
            if (estado == modo.PERSEGUINDO)
            {
                if (waypoint_index < destination.Length)
                {
                    pathManager.GetComponent<Pathfinding>().PosFinal.localPosition = destination[waypoint_index];
                    waypoint_index++;
                }
                else
                {
                    waypoint_index = 0;
                    pathManager.GetComponent<Pathfinding>().PosFinal.localPosition = destination[waypoint_index];
                }
            }
            estado = modo.PATRULHA;
            speed = 2.1f;
            GetComponent<Zombie_Animation>().animSpeed = 3.5f;
        }

        if (!pathManager)
        {
            //movimentação do zombie
            if (destination.Length != 0)
            {
                if (is_waiting)
                {
                    wait(i);
                    //this.transform.GetChild (0).gameObject.GetComponent<Animator>().SetBool("Moving", false);
                    if (!is_waiting)
                    {

                        if (i < destination.Length)
                        {
                            destiny = destination[i];
                            i++;
                        }
                        else
                        {
                            i = 0;
                            destiny = destination[i];
                        }
                        time_waiting = 0;
                        direction = (destiny - gameObject.transform.position).normalized;
                    }
                }
                else
                {
                    //this.transform.GetChild (0).gameObject.GetComponent<Animator>().SetBool("Moving", true);
                    gameObject.transform.Translate(direction * speed * Time.deltaTime);
                    if ((gameObject.transform.position - destiny).sqrMagnitude <= (direction * speed * Time.deltaTime).sqrMagnitude)
                    {

                        gameObject.transform.position = destiny;
                        is_waiting = true;
                    }
                }
            }
        }
        else
        {
            //movimentação do zombie
            Caminho = pathManager.GetComponent<Grid>().Trilha;
            if (Caminho.Count != 0)
            {
                if (is_waiting)
                {
                    //wait(0);
                    is_waiting = false;

                    if (!is_waiting)
                    {

                        if (i < Caminho.Count)
                        {
                            destiny = Caminho[i].Posicao;
                            i++;
                        }
                        else
                        {
                            i = 0;
                            destiny = Caminho[i].Posicao;
                        }
                        time_waiting = 0;
                        direction = (destiny - gameObject.transform.position).normalized;
                    }
                }
                else
                {
                    gameObject.transform.Translate(direction * speed * Time.deltaTime);
                    if ((gameObject.transform.position - destiny).sqrMagnitude <= (direction * speed * Time.deltaTime).sqrMagnitude)
                    {

                        gameObject.transform.position = destiny;
                        is_waiting = true;
                    }
                }
            }
            else
            {
                if (waypoint_index < destination.Length)
                {
                    pathManager.GetComponent<Pathfinding>().PosFinal.localPosition = destination[waypoint_index];
                    waypoint_index++;
                }
                else
                {
                    waypoint_index = 0;
                    pathManager.GetComponent<Pathfinding>().PosFinal.localPosition = destination[waypoint_index];
                }
                time_waiting = 0;
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
				state = "red";
				respawn();
				//Debug.Log("dead");
			}
        } else if(dist < biggerDist){
			if(state != "yellow"){
				state = "yellow";
				//Debug.Log("zombie is noticing the player");
			}
		} else {
			if(state != "green"){
				state = "green";
				//Debug.Log("not dead anymore");
			}
		}
    }

	void respawn(){
		FadeOut();
		apaga.GetComponent<ApagaLuz>().zumbi = this.gameObject;
	}

	public void FadeOut(){
		apaga.SetTrigger("FadeOut");
	}

	public void FadeIn(){
		apaga.SetTrigger("FadeIn");
	}

	public void respawn2(){
		FadeIn();
		this.transform.GetChild (0).gameObject.SetActive(false);
		cameraDead.transform.position = new Vector3(player.transform.position.x, cameraDead.transform.position.y, player.transform.position.z);
        cameraDead.SetActive(true);
        cameraDead.GetComponent<AudioListener>().enabled = true;
        camera.GetComponent<AudioListener>().enabled = false;
        camera.SetActive(false);
       
		player.transform.SetPositionAndRotation(new Vector3(-27f, 0f, -8.35f), Quaternion.Euler(new Vector3(0,90,0)));
		player.GetComponent<Animator>().enabled = false;
		player.transform.GetChild (0).gameObject.GetComponent<AudioSource> ().pitch = 0.75f;
		isDead = true;
	}

    public Vector3 getDirection()
    {
        return direction;
    }

}
