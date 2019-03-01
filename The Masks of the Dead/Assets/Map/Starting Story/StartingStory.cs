using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartingStory : MonoBehaviour {

    public float textSpeed = 1.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        print(gameObject.transform.position.y);
		if(gameObject.transform.position.y <= -300)
        {
            gameObject.transform.Translate(Vector3.up * textSpeed * Time.deltaTime);
        }else
        {
            SceneManager.LoadScene("Game 8 - New Design", LoadSceneMode.Single);
        }
	}
}
