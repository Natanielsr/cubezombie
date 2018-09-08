using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaCamera : MonoBehaviour {

	public GameObject Jogador;
	Vector3 distCompensar;
	public float VelocidadeCamera = 5;

	// Use this for initialization
	void Start () {
		distCompensar = transform.position - Jogador.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 posicaoParaIr = Jogador.transform.position + distCompensar;
		Vector3 direcao = (posicaoParaIr - transform.position); 

		transform.Translate (direcao * Time.deltaTime);

		//transform.position = posicaoParaIr;
		
	}
}
