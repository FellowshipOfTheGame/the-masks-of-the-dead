using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AnyButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.anyKey){
			//this.transform.parent.gameObject.SetActive(false);
			SceneManager.LoadScene("Game 8 - New Design");
		}
	}
}
