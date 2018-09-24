using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Zombie : MonoBehaviour {

	public GameObject player;
	
	public float lowerDist = 1f;
	public float biggerDist = 3f;

	public Material green;
	public Material yellow;
	public Material red;

	private GameObject body;
	private string state;

	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		body = transform.GetChild(0).gameObject;
		body.GetComponent<Renderer>().material = green;
		state = "green";
	}
	
	// Update is called once per frame
	void Update () {
		howNear();
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
				//Debug.Log("dead");
			}
        } else if(dist < biggerDist && !player.GetComponent<ThirdPersonCharacter>().m_Crouching){
			if(state != "yellow"){
				body.GetComponent<Renderer>().material = yellow;
				state = "yellow";
				//Debug.Log("dead if not stealth");
			}
		} else {
			if(state != "green"){
				body.GetComponent<Renderer>().material = green;
				state = "green";
				//Debug.Log("not dead anymore");
			}
		}
    }
}
