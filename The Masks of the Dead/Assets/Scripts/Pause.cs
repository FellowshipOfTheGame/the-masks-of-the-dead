using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour {

    bool ispaused = false;

    GameObject pause_ui;
    ActionScript actionscript;

    private void Exit()
    {
        Application.Quit();
    }

    private void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pause_ui.SetActive(false);
        ispaused = false;
        Time.timeScale = 1;
        actionscript.enabled = true;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        actionscript = GameObject.Find("Camera").GetComponent<ActionScript>();
        pause_ui = GameObject.Find("Camera/Canvas/Pause");
        GameObject.Find("Camera/Canvas/Pause/Buttons/Resume").GetComponent<Button>().onClick.AddListener(Resume);
        GameObject.Find("Camera/Canvas/Pause/Buttons/Exit").GetComponent<Button>().onClick.AddListener(Exit);
        pause_ui.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
		if(Input.GetButtonDown("Pause"))
        {
            if(ispaused)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                pause_ui.SetActive(false);
                ispaused = false;
                Time.timeScale = 1;
                actionscript.enabled = true;
            }else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                pause_ui.SetActive(true);
                ispaused = true;
                Time.timeScale = 0;
                actionscript.enabled = false;
            }
        }
	}
}
