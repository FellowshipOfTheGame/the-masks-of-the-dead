using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_sight : MonoBehaviour {

    public float fielOfVision = 110f;
    public bool playerInSight;
    public Vector3 personalLastSighting;

    private Pathfinding nav;
    private SphereCollider col;
    private GameObject player;
    private Vector3 previousSighting;

	void Awake () {
        //nav = GetComponent<Pathfinding>();
        col = GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        previousSighting = personalLastSighting;
	}

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInSight = false;

            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, GetComponent<Zombie>().getDirection());

            if(angle < fielOfVision * 0.5f)
            {
                RaycastHit Ray;

                if(Physics.Raycast(transform.position, direction.normalized, out Ray, col.radius))
                {
                    if(Ray.collider.gameObject == player)
                    {
                        playerInSight = true;
                        personalLastSighting = player.transform.position;
                    }
                }
            }
        }
    }
}
