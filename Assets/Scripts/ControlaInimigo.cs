using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum StatusInimigo{
	VAGANDO,
	SEGUINDO,
	ERRANDO_ATAQUE,
	ATACANDO,
	MORTO
}


public abstract class ControlaInimigo : ControlaPersonagem, IGerado {

	protected GameObject jogador;
	float distanciaJogador;

	public AudioClip SomDeMorte;
	public AudioClip SomDanoZumbi;
	protected Vector3 direcao;
	protected Vector3 posicaoAleatoria;

	private float contadorVagar;
	private float tempoEntrePosicoesAleatorias = 4;
	private Gerador geradorDeZumbis;
	public float PorcentagemGerarKitMedico = 0.1f;
	public GameObject KitMedicoPrefab;
	public float AlcanceDoAtaque = 3f;
	public float DistanciaInicioAtaque = 2;

	protected bool Atacando = false;
	protected bool PertoJogador = false;
	protected bool Morto = false;
	protected bool AchouJogador = false;

	StatusInimigo statusAtualInimgo;

	public GameObject ParticulaSanguePrefab;

	// Use this for initialization
	void Start () {
		jogador = GameObject.FindWithTag ("Jogador");

		AleatorizarZumbi ();

		StartCallback ();


	}

	protected virtual void StartCallback(){
		
	}
	
	// Update is called once per frame
	void Update () {

		if(meuAnimator.GetCurrentAnimatorStateInfo(0).IsName("Zumbi_Andar"))
			meuAnimator.speed = meuStatus.Velocidade / 2 ;
		else
			meuAnimator.speed = 1;
	}

	protected void ControlaStatus ()
	{
		distanciaJogador = Vector3.Distance (transform.position, jogador.transform.position);

		statusAtualInimgo = AlteraStatus ();
		switch(statusAtualInimgo){
		case StatusInimigo.VAGANDO:
			Vagar ();
		
			//meuRigidbody.constraints = RigidbodyConstraints.FreezePositionY;
			break;

		case StatusInimigo.SEGUINDO:
			Perseguir ();
			break;

		case StatusInimigo.ERRANDO_ATAQUE:
			PertoJogador = false;
			direcao = jogador.transform.position - transform.position;

			Rotacionar (direcao);
			//meuRigidbody.constraints = RigidbodyConstraints.FreezeAll;
			break;

		case StatusInimigo.ATACANDO:
			direcao = jogador.transform.position - transform.position;
			PertoJogador = true;
			Atacando = true;
			AtacarAnimacao (Atacando);
			Rotacionar (direcao);

			//meuRigidbody.constraints = RigidbodyConstraints.FreezeAll;
			break;

		case StatusInimigo.MORTO:
			//meuRigidbody.constraints = RigidbodyConstraints.FreezePositionY;
			if(distanciaJogador > 32)
				Destroy (gameObject);
			break;
		default :
			break;
		}
	}

	protected virtual void Perseguir (){
		direcao = jogador.transform.position - transform.position;
		Movimentar (direcao, meuStatus.Velocidade);

		MovimentarAnimacao (direcao.magnitude);
		Rotacionar (direcao);
	}

	protected StatusInimigo AlteraStatus (){

		if (Morto) {
			return StatusInimigo.MORTO;
		}

		if (distanciaJogador > 15) {
			return StatusInimigo.VAGANDO;

		} else if (distanciaJogador > AlcanceDoAtaque) {
			
			if (Atacando)
				return StatusInimigo.ERRANDO_ATAQUE;

			return StatusInimigo.SEGUINDO;

		} else if (distanciaJogador < DistanciaInicioAtaque) {
			return StatusInimigo.ATACANDO;
		} 
		else {
			return StatusInimigo.SEGUINDO;
		}

	}

	protected void Vagar(){

		contadorVagar -= Time.deltaTime;
		if(contadorVagar <= 0){
			posicaoAleatoria = AleatorizarPosicao ();
			contadorVagar += tempoEntrePosicoesAleatorias + Random.Range(-1f, 1f);
		}

		MoverParaPosicaoAleatoria ();

	}

	protected virtual void MoverParaPosicaoAleatoria(){
		if (Vector3.Distance (transform.position, posicaoAleatoria) >= 0.1f) {
			direcao = posicaoAleatoria - transform.position;
			Movimentar (direcao, meuStatus.Velocidade);
		}

		MovimentarAnimacao (direcao.magnitude);
		Rotacionar (direcao);
	}

	Vector3 AleatorizarPosicao(){
		var posicao = Random.insideUnitSphere * 10;
		posicao += transform.position;
		posicao.y = transform.position.y;

		return posicao;
	}

	void AtacaJogador(){

		if (PertoJogador) {
			var ctrl_jogador = jogador.GetComponent<ControlaJogador> ();

			var danoGerado = CalculaDano ();

			ctrl_jogador.TomarDano (danoGerado);
		}

		Atacando = false;
		AtacarAnimacao (Atacando);

	}

	void AleatorizarZumbi(){
		int geraTipoZumbi = Random.Range(2, transform.childCount);
		var objRender = transform.GetChild (geraTipoZumbi).gameObject;

		objRender.SetActive(true);
	}

	public void SetGerador(Gerador gerador){
		this.geradorDeZumbis = gerador;
	}

	public override void TomarDano(int dano){
		base.TomarDano (dano);
		ControlaAudio.instancia.PlayOneShot (SomDanoZumbi);
	}

	public override void Morrer(){
		base.Morrer ();

		MorrerAnimacao ();
		Morto = true;

		GetComponent<Collider> ().enabled = false;

		scriptControlaInterface.AtualizarQuantidadeDeZumbisMortos ();
		ControlaAudio.instancia.PlayOneShot (SomDeMorte);
		VerificarGeracaoKitMedico (PorcentagemGerarKitMedico);

		if (geradorDeZumbis != null)
			RemoverObjeto ();

		Destroy (gameObject, 10);
	}

	public void RemoverObjeto(){
		geradorDeZumbis.RemoverObjeto (gameObject);
	}

	void VerificarGeracaoKitMedico(float porcentagemGeracao){
		if(Random.value <= porcentagemGeracao){
			Instantiate (KitMedicoPrefab, transform.position, Quaternion.identity);
		}
	}

	public void ParticulaSangue(Vector3 posicao, Quaternion rotacao){
		var obj = Instantiate (ParticulaSanguePrefab, posicao, rotacao);
		Destroy (obj, 1);
	}
}
