using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour {

    public string start_menu;

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameObject.Find("Canvas/Button").GetComponent<Button>().onClick.AddListener(Back_Menu);
	}

    void Back_Menu()
    {
        SceneManager.LoadScene(start_menu, LoadSceneMode.Single);
    }
}
