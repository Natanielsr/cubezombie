using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour {

	public int VidaInicial = 100;
	private int vida;
	public float Velocidade = 5;
	public float PorcentagemCritico = 5;
	public float dano = 10;

	// Use this for initialization
	void Awake () {
		vida = VidaInicial;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int GetVidaAtual(){
		return this.vida;
	}

	public void TomarDano(int dano){
		this.vida -= dano;
	}

	public void Curar(int quantidadeDeCura){

		this.vida += quantidadeDeCura;
		if (this.vida > this.VidaInicial)
			this.vida = this.VidaInicial;
	}

}
