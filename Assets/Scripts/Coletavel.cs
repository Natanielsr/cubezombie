using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Coletavel : MonoBehaviour {

	public AudioClip SomColetar;

	void OnTriggerEnter(Collider objetoDeColisao){
		if (objetoDeColisao.tag == "Jogador") {
			var jogador = objetoDeColisao.GetComponent<ControlaJogador> ();
			AcaoColetavel (jogador);
			ControlaAudio.instancia.PlayOneShot (SomColetar);
			Destroy (gameObject);
		}
	}

	protected abstract void AcaoColetavel (ControlaJogador jogador);
}
