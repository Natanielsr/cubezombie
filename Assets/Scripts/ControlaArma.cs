using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaArma : MonoBehaviour {

	public GameObject Bala;
	public GameObject CanoDaArma;
	public AudioClip SomDoTiro;
	public AudioClip SomRecargaArma;
	public AudioClip SomSemBala;
	public float TempoDeDisparo = 0.5f;
	public float TempoRecargaArma = 1;
	private float contagemDisparo;

	public int TamanhoPente = 15;
	public int QuantidadeDeBalas { get; private set;}

	bool podeAtirar;
	bool carregando;

	// Use this for initialization
	void Start () {
		QuantidadeDeBalas = TamanhoPente;
		podeAtirar = true;
		//contagemDisparo = TempoDeDisparo;
	}
	
	// Update is called once per frame
	/*void Update () {



		if (contagemDisparo >= TempoDeDisparo ) {

			podeAtirar = true;

			if(Input.GetButtonDown("Fire1")){
				
			}

		}
		else
			contagemDisparo += Time.deltaTime;


		
	}*/

	public void Atirar(){
		if (podeAtirar && !carregando && QuantidadeDeBalas > 0) {
			Instantiate (Bala, CanoDaArma.transform.position, CanoDaArma.transform.rotation);
			QuantidadeDeBalas--;
			ControlaAudio.instancia.PlayOneShot (SomDoTiro);
			StartCoroutine (controlaTempoParaAtirar(TempoDeDisparo));
		}
		else if(QuantidadeDeBalas == 0){
			ControlaAudio.instancia.PlayOneShot (SomSemBala);
		}
		
	}

	IEnumerator controlaTempoParaAtirar(float tempo){
		podeAtirar = false;
		yield return new WaitForSeconds (tempo);
		podeAtirar = true;

	}

	public int RecarregarArma(int quantidadeDeBalas){

		if (quantidadeDeBalas == 0)
			return 0;

		if (this.QuantidadeDeBalas == TamanhoPente)
			return quantidadeDeBalas;

		ControlaAudio.instancia.PlayOneShot (SomRecargaArma);
		
		StartCoroutine (tempoCarregando(TempoRecargaArma));
		var totalBalas = this.QuantidadeDeBalas + quantidadeDeBalas;
		if (totalBalas <= TamanhoPente) {
			this.QuantidadeDeBalas = totalBalas;
			return 0;
		} else {
			var restoBalas = totalBalas - TamanhoPente;
			this.QuantidadeDeBalas = TamanhoPente;
			return restoBalas;
		}
	}

	IEnumerator tempoCarregando(float tempo){
		carregando = true;
		yield return new WaitForSeconds (tempo);
		carregando = false;

	}

}


