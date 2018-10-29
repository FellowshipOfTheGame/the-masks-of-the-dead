using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionArea : MonoBehaviour {

    [Range(0.0f, 5.0f)] public float action_distance = 1.0f;
    GameObject outlined_object = null;
    //GameObject questText;

    private bool startedQuest = false;
    private int ropes=0, squeegees=0, tapes=0;

    public Text questText;

    /*EST� AQUI PARA TESTES E SER� REMOVIDO*/
    // public Material test_material;
    // Material previous_material;
    /*EST� AQUI PARA TESTES E SER� REMOVIDO*/

    void open()
    {
        Vector3 direction = Vector3.forward;
        GameObject object_gotten;
        RaycastHit hit;

        direction = Quaternion.Euler(gameObject.transform.eulerAngles) * direction;

        //print(gameObject.transform.eulerAngles);
        if (Physics.Raycast(gameObject.transform.position, direction, out hit, action_distance))
        {
            //Debug.Log(hit.transform.gameObject);
            object_gotten = hit.transform.gameObject;

            if(object_gotten.tag == "Door")
            {
                object_gotten.GetComponent<DoorScript>().opendoor();
            }else if(object_gotten.tag == "Item")
            {
                if(startedQuest){
                    if(object_gotten.name == "Rope" && ropes<5){
                        ropes++;
                        questText.text = "Colete os itens para consertar o harpão:\nCorda "+ropes+"/5\nFita "+tapes+"/5\nRodo "+squeegees+"/2";
                        Destroy(object_gotten);
                    }else if(object_gotten.name == "Squeegee" && squeegees<2){
                        squeegees++;
                        questText.text = "Colete os itens para consertar o harpão:\nCorda "+ropes+"/5\nFita "+tapes+"/5\nRodo "+squeegees+"/2";
                        Destroy(object_gotten);
                    }else if(object_gotten.name == "Tape" && tapes<5){
                        tapes++;
                        questText.text = "Colete os itens para consertar o harpão:\nCorda "+ropes+"/5\nFita "+tapes+"/5\nRodo "+squeegees+"/2";
                        Destroy(object_gotten);
                    }
                }
            }else if(object_gotten.tag == "Harpoon"){
                if(!startedQuest){
                    Debug.Log("Startou a quest!");
                    startedQuest = true;
                    questText.text = "Colete os itens para consertar o harpão:\nCorda 0/5\nFita 0/5\nRodo 0/2";
                }
            }
        }
    }

	// Use this for initialization
	void Start () {
		//questText = transform.GetChild(1).gameObject.transform.GetChild(1).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Action"))
        {
            open();
        }

        /* Vector3 direction = Vector3.forward;
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
        } */
    }
}
