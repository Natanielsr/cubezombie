using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitMedico : Coletavel {

	public int quantidadeDeCura = 15;


	void Start(){
		Destroy (gameObject, 10);
	}

	protected override void AcaoColetavel (ControlaJogador jogador)
	{
		jogador.CurarVida (quantidadeDeCura);
	}

}
