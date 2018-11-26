using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour {

    bool ispaused = false;

    GameObject pause_ui;
    ActionScript actionscript;

    private void Resume()
    {
        pause_ui.SetActive(false);
        ispaused = false;
        Time.timeScale = 1;
        actionscript.enabled = true;
    }

    private void Start()
    {
        actionscript = GameObject.Find("Camera").GetComponent<ActionScript>();
        pause_ui = GameObject.Find("Camera/Canvas/Pause");
        GameObject.Find("Camera/Canvas/Pause/Buttons/Resume").GetComponent<Button>().onClick.AddListener(Resume);
        GameObject.Find("Camera/Canvas/Pause/Buttons/Options").GetComponent<Button>().onClick.AddListener(Resume);
        GameObject.Find("Camera/Canvas/Pause/Buttons/Exit").GetComponent<Button>().onClick.AddListener(Resume);
        pause_ui.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
		if(Input.GetButtonDown("Pause"))
        {
            if(ispaused)
            {
                pause_ui.SetActive(false);
                ispaused = false;
                Time.timeScale = 1;
                actionscript.enabled = true;
            }else
            {
                pause_ui.SetActive(true);
                ispaused = true;
                Time.timeScale = 0;
                actionscript.enabled = false;
            }
        }
	}
}
