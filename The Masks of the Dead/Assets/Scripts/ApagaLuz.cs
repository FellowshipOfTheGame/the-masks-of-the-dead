using UnityEngine;

public class ApagaLuz : MonoBehaviour {

	[SerializeField] public Animator anima;
	public GameObject zumbi;

	public void FadeOut(){
		anima.SetTrigger("FadeOut");
	}

	public void OnFadeComplete(){
		print("Inahi");
		zumbi.GetComponent<Zombie>().respawn2();
	}
}
