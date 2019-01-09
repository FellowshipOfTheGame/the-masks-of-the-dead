using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Animation : MonoBehaviour {

    public float turning_speed = 100.0f;

    private Vector3 previousPosition;
    private Vector3 currentPosition;
    private GameObject zombieModel;
    private Animator anim;

	// Use this for initialization
	void Start () {
        zombieModel = gameObject.transform.GetChild(0).gameObject;

        anim = zombieModel.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        Animate();
	}

    private void Animate()
    {
        currentPosition = zombieModel.transform.position;
        Vector3 direction = currentPosition - previousPosition;
        if (direction != Vector3.zero)
        {
            zombieModel.transform.rotation = Quaternion.LookRotation(direction);
            anim.SetBool("Moving", true);
        }else
        {
            anim.SetBool("Moving", false);
        }
        previousPosition = currentPosition;
    }
}
