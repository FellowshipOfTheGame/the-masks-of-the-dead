using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Zombie : MonoBehaviour {

	public GameObject player;
	public GameObject camera;
	public GameObject cameraDead;
	
	public float lowerDist = 1f;
	public float biggerDist = 3f;

	public Material green;
	public Material yellow;
	public Material red;

    public Vector3[] destination;
    Vector3 starting_location;//Onde o zumbi inicia.
    Vector3 destiny;//O próximo destino do zumbi.
    int i = 0;//Posição do destino no vetor direction
    Vector3 direction;//Direção que o zumbi anda para chegar no destino.
    [Tooltip("Os pontos para os quais os zumbis vão andar, na ordem que são colocados. (após o último, ele retorna à posição inicial.")]
    [Range(0.0f, 1.0f)] public float speed;

	private GameObject body;
	private string state;

	public bool isDead = false;

	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		body = transform.GetChild(0).gameObject;
		body.GetComponent<Renderer>().material = green;
		state = "green";
        if (destination.Length != 0)
        {
            destiny = destination[0];
            starting_location = gameObject.transform.position;
            direction = destiny - starting_location;
            direction = direction.normalized;
            i++;
        }
	}
	
	// Update is called once per frame
	void Update () {
		howNear();

		if(isDead){
			if(Input.GetKeyDown(KeyCode.Return)){
				player.transform.SetPositionAndRotation(new Vector3(3.4f, 0f, -8.35f), Quaternion.Euler(new Vector3(0,90,0)));
				camera.GetComponent<Camera>().enabled = true;
				cameraDead.GetComponent<Camera>().enabled = false;
				cameraDead.transform.position = new Vector3(-10, 7, 10);
				isDead = false;
			}
		}
        
        //movimentação do zombie
        if(destination.Length != 0)
        {
            gameObject.transform.Translate(direction * speed * Time.deltaTime);
            if((gameObject.transform.position - destiny).sqrMagnitude <= (direction * speed * Time.deltaTime).sqrMagnitude)
            {
                gameObject.transform.position = destiny;
                if(i < destination.Length)
                {
                    destiny = destination[i];
                    i++;
                }else
                {
                    i = 0;
                    destiny = starting_location;
                }
                direction = destiny - gameObject.transform.position;
            }
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
        } else if(dist < biggerDist && !player.GetComponent<ThirdPersonCharacter>().m_Crouching){
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
		cameraDead.GetComponent<Camera>().enabled = true;
		camera.GetComponent<Camera>().enabled = false;
		player.transform.SetPositionAndRotation(new Vector3(-27f, 0f, -8.35f), Quaternion.Euler(new Vector3(0,90,0)));
		isDead = true;
	}
}
