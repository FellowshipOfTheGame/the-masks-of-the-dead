using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  

public class Start_Game : MonoBehaviour {

    Button start_button;
    public string first_level;
    AudioSource audioSource;

	void Start () {
        start_button = GameObject.Find("Main Camera/Canvas/Panel/Button").GetComponent<Button>();
        start_button.onClick.AddListener(Btn_Click);

        audioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
	}

    private void Update()
    {
        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    void Btn_Click()
    {
        SceneManager.LoadScene(first_level);
    }

}
