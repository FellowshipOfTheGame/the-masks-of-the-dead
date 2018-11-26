using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  

public class Start_Game : MonoBehaviour {

    Button start_button;
    public string first_level;

	void Start () {
        start_button = GameObject.Find("Main Camera/Canvas/Button").GetComponent<Button>();
        start_button.onClick.AddListener(Btn_Click);
	}

    void Btn_Click()
    {
        SceneManager.LoadScene(first_level);
    }

}
