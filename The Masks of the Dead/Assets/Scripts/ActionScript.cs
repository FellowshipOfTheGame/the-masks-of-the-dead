using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ActionScript : MonoBehaviour {

    [Range(0.0f, 10.0f)] public float action_distance = 1.0f;
    //GameObject questText;

    private bool startedQuest = false;
    private int ropes=0, squeegees=0, tapes=0;

    public Text questText;

    public string next_scene;

    GameObject icon;
    Sprite icon_use;
    Sprite icon_open;
    Sprite icon_close;
    Sprite icon_pick;
    bool showing_icon = false;
    bool quest_completed = false;

    void Action()
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
                        if (Verify_quest(ropes, tapes, squeegees))
                        {
                            quest_completed = true;
                            questText.text = "Use esses itens para consertar o arpão e fugir!";
                        }
                    }else if(object_gotten.name == "Squeegee" && squeegees<2){
                        squeegees++;
                        questText.text = "Colete os itens para consertar o harpão:\nCorda "+ropes+"/5\nFita "+tapes+"/5\nRodo "+squeegees+"/2";
                        Destroy(object_gotten);
                        if (Verify_quest(ropes, tapes, squeegees))
                        {
                            quest_completed = true;
                            questText.text = "Use esses itens para consertar o arpão e fugir!";
                        }
                    }
                    else if(object_gotten.name == "Tape" && tapes<5){
                        tapes++;
                        questText.text = "Colete os itens para consertar o harpão:\nCorda "+ropes+"/5\nFita "+tapes+"/5\nRodo "+squeegees+"/2";
                        Destroy(object_gotten);
                        if (Verify_quest(ropes, tapes, squeegees))
                        {
                            quest_completed = true;
                            questText.text = "Use esses itens para consertar o arpão e fugir!";
                        }
                    }
                }
            }else if(object_gotten.tag == "Harpoon"){
                if(quest_completed)
                {
                    SceneManager.LoadScene(next_scene, LoadSceneMode.Single);
                }
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
        icon = GameObject.Find("Camera/Canvas/Action_Icon");
        icon.SetActive(false);
        icon_open = Resources.Load<Sprite>("Sprites/Press 'E' to Open");
        icon_close = Resources.Load<Sprite>("Sprites/Press 'E' to Close");
        icon_pick = Resources.Load<Sprite>( "Sprites/Press 'E' to Pick");
        icon_use = Resources.Load<Sprite>("Sprites/Press 'E' to Use");
        //questText = transform.GetChild(1).gameObject.transform.GetChild(1).gameObject;
    }
	
	// Update is called once per frame
	void Update () {

        GameObject o_hit;//objeto acertado pelo RayCast;
        Vector3 direction = Quaternion.Euler(gameObject.transform.eulerAngles) * Vector3.forward;//Direção do RayCast
        RaycastHit hit;//RayCast hit
        DoorScript door_script;//Variável que armazena o script de uma porta, se o player mirar para alguma.

        if (Input.GetButtonDown("Action"))
        {
            Action();
        }
        
        if (Physics.Raycast(gameObject.transform.position, direction, out hit, action_distance))
        {
            if(!showing_icon)
            {
                icon.SetActive(true);
                showing_icon = true;
            }

            o_hit = hit.transform.gameObject;
            if(o_hit.tag == "Door")
            {
                door_script = o_hit.transform.gameObject.GetComponent<DoorScript>();
                if(door_script.isopenned == 1)
                {
                     icon.GetComponent<Image>().sprite = icon_close;
                }else
                {
                    icon.GetComponent<Image>().sprite = icon_open;
                }
                if(door_script.opening != 0)
                {
                    icon.SetActive(false);
                    showing_icon = false;
                }
            }else if(o_hit.tag == "Item" && startedQuest)
            {
                icon.GetComponent<Image>().sprite = icon_pick;
            }else if(o_hit.tag == "Harpoon")
            {
                icon.GetComponent<Image>().sprite = icon_use;
            }else
            {
                icon.SetActive(false);
                showing_icon = false;
            }
        }
        else if(showing_icon)
        {
            icon.SetActive(false);
            showing_icon = false;
        }
    }

    bool Verify_quest(int ropes, int tapes, int squeegees)
    {
        if(ropes == 5 && tapes == 5 && squeegees == 2)
        {
            return true;
        }else
        {
            return false;
        }
    }
}
