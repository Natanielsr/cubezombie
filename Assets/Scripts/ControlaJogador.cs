using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaJogador : ControlaPersonagem{

	Vector3 direcao;
	public LayerMask MascaraChao;
	public AudioClip SomDeDano;
	private ControlaArma controlaArma;

	private int quantidadeDeBalas = 0;

	void Start(){
		controlaArma = GetComponent<ControlaArma> ();
		quantidadeDeBalas = 15;

		scriptControlaInterface.AtualizarQuantidadeDeBalas (controlaArma.QuantidadeDeBalas, this.quantidadeDeBalas);
	}

	// Update is called once per frame
	void Update () {

		if(Input.GetButtonDown("Fire1")){
			if (controlaArma.QuantidadeDeBalas > 0)
				controlaArma.Atirar ();
			else {
				controlaArma.Atirar ();
				quantidadeDeBalas = controlaArma.RecarregarArma (quantidadeDeBalas);
			}
			
			scriptControlaInterface.AtualizarQuantidadeDeBalas (controlaArma.QuantidadeDeBalas, this.quantidadeDeBalas);
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			quantidadeDeBalas = controlaArma.RecarregarArma (quantidadeDeBalas);
			scriptControlaInterface.AtualizarQuantidadeDeBalas (controlaArma.QuantidadeDeBalas, this.quantidadeDeBalas);
		}



	}

	void FixedUpdate(){

		float eixoX = Input.GetAxis ("Horizontal");
		float eixoZ = Input.GetAxis ("Vertical");

		direcao = new Vector3 (eixoX, 0, eixoZ);

		Movimentar (direcao, meuStatus.Velocidade);
		MovimentarAnimacao (direcao.magnitude);

		RotacaoJogador (MascaraChao);

	}

	public void RotacaoJogador(LayerMask mascaraChao){
		Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
		Debug.DrawRay (raio.origin, raio.direction*100, Color.red);

		RaycastHit impacto;

		if (Physics.Raycast (raio, out impacto, 100, mascaraChao)) {
			Vector3 posicaoMiraJogador = impacto.point - transform.position;

			posicaoMiraJogador.y = transform.position.y;

			Rotacionar (posicaoMiraJogador);
		}

	}

	protected override void TomarDanoCallback(int dano){
		
		scriptControlaInterface.AtualizarSliderVidaJogador ();
		ControlaAudio.instancia.PlayOneShot (SomDeDano);
	}

	public override void  Morrer(){
		base.Morrer ();
		scriptControlaInterface.GameOver ();
	}

	protected override void  CurarVidaCallback(){

		scriptControlaInterface.AtualizarSliderVidaJogador ();
	}

	public void AdicionarBalas(int qtdBalas){
		this.quantidadeDeBalas += qtdBalas;
		scriptControlaInterface.AtualizarQuantidadeDeBalas (controlaArma.QuantidadeDeBalas, this.quantidadeDeBalas);
	}

}
