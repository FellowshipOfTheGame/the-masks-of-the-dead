using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionArea : MonoBehaviour {

    [Range(0.0f, 3.0f)] public float action_distance = 1.0f;
    GameObject outlined_object = null;

    /*ESTÁ AQUI PARA TESTES E SERÁ REMOVIDO*/
    public Material test_material;
    Material previous_material;
    /*ESTÁ AQUI PARA TESTES E SERÁ REMOVIDO*/

    void open()
    {
        Vector3 direction = Vector3.forward;
        GameObject object_gotten;
        RaycastHit hit;

        direction = Quaternion.Euler(gameObject.transform.eulerAngles) * direction;

        //print(gameObject.transform.eulerAngles);
        if (Physics.Raycast(gameObject.transform.position, direction, out hit, action_distance))
        {
            object_gotten = hit.transform.gameObject;

            if(object_gotten.tag == "Door")
            {
                object_gotten.GetComponent<DoorScript>().opendoor();
            }else if(object_gotten.tag == "Item")
            {
                Destroy(object_gotten);
            }
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Action"))
        {
            open();
        }

        Vector3 direction = Vector3.forward;
        RaycastHit hit;
        if (Physics.Raycast(gameObject.transform.position, direction, out hit, action_distance))
        {
            if(outlined_object != null)
            {
                if(outlined_object.tag == "Item" || outlined_object.tag == "Door")
                    outlined_object.GetComponent<Renderer>().material = previous_material;
            }
            outlined_object = hit.transform.gameObject;
            if (outlined_object.tag == "Item" || outlined_object.tag == "Door")
            {
                previous_material = outlined_object.GetComponent<Renderer>().material;
                outlined_object.GetComponent<Renderer>().material = test_material;
            }
        }else
        {
            if(outlined_object != null && (outlined_object.tag == "Item" || outlined_object.tag == "Door"))
            {
                outlined_object.GetComponent<Renderer>().material = previous_material;
            }
            outlined_object = null;
        }
    }
}
