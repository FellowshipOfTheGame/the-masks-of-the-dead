using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour {

	[SerializeField]
	private float averageOnTime, averageOffTime;
	private bool on;
	[SerializeField]
	private GameObject lightPlane;

	void Start(){
		StartCoroutine(Flickering());
		on = true;
	}

	IEnumerator Flickering(){
		while (true){
			if (on)
				yield return new WaitForSeconds(ExpoRandom(averageOnTime));
			else
				yield return new WaitForSeconds(ExpoRandom(averageOffTime));
			on = !on;
			lightPlane.SetActive(on);
		}
	}

	private float ExpoRandom(float lambda){
		float x = Random.value;
		return -Mathf.Log(1-x)*lambda;
	}

}
