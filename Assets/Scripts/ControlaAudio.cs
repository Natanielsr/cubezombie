using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaAudio : MonoBehaviour {


	private AudioSource meuAudioSource;
	public static AudioSource instancia;

	// Use this for initialization
	void Awake () {
		meuAudioSource = GetComponent<AudioSource> ();
		instancia = meuAudioSource;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
