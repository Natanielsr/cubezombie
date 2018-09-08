using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Municao : Coletavel, IGerado {

	private Gerador gerador;

	protected override void AcaoColetavel (ControlaJogador jogador)
	{
		jogador.AdicionarBalas (15);
		RemoverObjeto ();
	}

	public void SetGerador(Gerador gerador){
		this.gerador = gerador;
	}

	public void RemoverObjeto(){
		gerador.RemoverObjeto (gameObject);
	}
}
