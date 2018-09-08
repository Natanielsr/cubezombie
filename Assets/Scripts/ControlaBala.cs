using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaBala : MonoBehaviour {

	public float Velocidade = 30;

	float tempoDeVidaBala = 1;
	float contador;
	Rigidbody rigidbodyBala;


	// Use this for initialization
	void Start () {
		rigidbodyBala = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rigidbodyBala.MovePosition 
		(rigidbodyBala.position +
			transform.forward * Velocidade * Time.deltaTime);

	}

	void Update(){
		contador += Time.deltaTime;

		if (contador > tempoDeVidaBala)
			Destroy (gameObject);
	}

	void OnTriggerEnter(Collider objetoDeColisao){
		if (objetoDeColisao.gameObject.tag == "Inimigo") {
			//dar dano no inimigo
			var dano = Random.Range (50, 60);
			var critico = Random.Range (0, 100) < 5;

			if (critico) {
				dano = dano * 2;
				Debug.Log ("Critico " + dano);
			}

			Quaternion rotacaoOpostaBala = Quaternion.LookRotation(-transform.forward);

			ControlaInimigo inimigo = objetoDeColisao.GetComponent<ControlaInimigo> ();
			inimigo.TomarDano(dano);
			inimigo.ParticulaSangue (transform.position, rotacaoOpostaBala);
		}
		

		Destroy (gameObject);
	}
}
