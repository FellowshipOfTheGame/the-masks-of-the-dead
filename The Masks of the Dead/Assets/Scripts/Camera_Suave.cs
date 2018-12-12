using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camera_Suave : MonoBehaviour {
    
    [Range(0.0f, 300.0f)]public float min_speed_x;
    [Range(0.0f, 2.0f)]public float min_speed_y;
    float starting_speed_x;//valor inicial da vel max de Y e X
    float starting_speed_y;
        
    GameObject player;
    GameObject o_camera;
    float starting_distance;

	// Use this for initialization
	void Start () {
        o_camera = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player");
        starting_distance = (o_camera.transform.position - (player.transform.position + Vector3.up*1.5f)).magnitude;
        starting_speed_x = gameObject.GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed;
        starting_speed_y = gameObject.GetComponent<CinemachineFreeLook>().m_YAxis.m_MaxSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        float r = 1 - ((o_camera.transform.position - (player.transform.position + Vector3.up*1.5f)).magnitude) / (starting_distance);//Razão entre o quadrado das distancias atuais e finais.

        gameObject.GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed = starting_speed_x - ((starting_speed_x - min_speed_x) * r);
        if(gameObject.GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed > starting_speed_x)
        {
            gameObject.GetComponent<CinemachineFreeLook>().m_XAxis.m_MaxSpeed = starting_speed_x;
        }
        gameObject.GetComponent<CinemachineFreeLook>().m_YAxis.m_MaxSpeed = starting_speed_y - ((starting_speed_y - min_speed_y) * r);
        if (gameObject.GetComponent<CinemachineFreeLook>().m_YAxis.m_MaxSpeed > starting_speed_y)
        {
            gameObject.GetComponent<CinemachineFreeLook>().m_YAxis.m_MaxSpeed = starting_speed_y;
        }
    }
}
