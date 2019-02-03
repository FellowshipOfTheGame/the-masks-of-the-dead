using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour {
	[SerializeField] public AudioClip[] passos;
	private AudioSource m_audio;
	private Animator m_anima;
	// Use this for initialization
	void Start () {
		m_audio = GetComponent<AudioSource>();
		m_anima = GetComponent<Animator>();
	}
	
	// Andar
	private void Step(){
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){
			AudioClip clip = GetRandomClip();
			m_audio.PlayOneShot(clip, volumeScale: 0.5f);
		}
	}

	// Andar agachado
	private void StepCrouch(){
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){
			AudioClip clip = GetRandomClip();
			m_audio.PlayOneShot(clip, volumeScale: 0.25f);
		}
	}

	// Andar para Direita
	private void StepSharpRight(){
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){
			AudioClip clip = GetRandomClip();
			m_audio.PlayOneShot(clip, volumeScale: 0.5f);
		}
	}

	// Andar para Esquerda
	private void StepSharpLeft(){
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){
			AudioClip clip = GetRandomClip();
			m_audio.PlayOneShot(clip, volumeScale: 0.5f);
		}
	}

	// Pega um som de passo aleatório do vetor
	private AudioClip GetRandomClip(){
		return passos[UnityEngine.Random.Range(0, passos.Length)];
	}
}
