using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ControlaChefe : ControlaInimigo {

	private NavMeshAgent agente;
	public GameObject ObjArma;
	public Slider SliderVidaChefe;
	public Image ImagemSlider;
	public Color CorDaVidaMaxima, CorDaVidaMinima;

	// Use this for initialization
	protected override void StartCallback () {
		agente = GetComponent<NavMeshAgent> ();
		agente.speed = meuStatus.Velocidade;
		SliderVidaChefe.maxValue = meuStatus.VidaInicial;
		SliderVidaChefe.value = meuStatus.VidaInicial;
		ImagemSlider.color = CorDaVidaMaxima;
		AleatorizarArma ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		ControlaStatus ();
	}

	protected override void Perseguir ()
	{
		agente.SetDestination (jogador.transform.position);
		MovimentarAnimacao (agente.velocity.magnitude);
	}

	protected override void MoverParaPosicaoAleatoria(){
		if (Vector3.Distance (transform.position, posicaoAleatoria) >= 1f) {
			agente.SetDestination (posicaoAleatoria);
		}

		MovimentarAnimacao (agente.velocity.magnitude);
	}

	public override void Morrer ()
	{
		base.Morrer ();
		SliderVidaChefe.gameObject.SetActive(false);
		agente.enabled = false;
	}

	void AleatorizarArma(){
		int geraTipoArma = Random.Range(0, ObjArma.transform.childCount);
		var objRender = ObjArma.transform.GetChild (geraTipoArma).gameObject;
		objRender.SetActive(true);
	}

	public override void TomarDano (int dano)
	{
		base.TomarDano (dano);
		AtualizarInterface ();
	}

	void AtualizarInterface(){
		SliderVidaChefe.value = meuStatus.GetVidaAtual ();
		float porcentagemDaVida = (float)meuStatus.GetVidaAtual () / meuStatus.VidaInicial;
		Color corDaVida = Color.Lerp( CorDaVidaMinima, CorDaVidaMaxima, porcentagemDaVida);
		ImagemSlider.color = corDaVida;
	}
}
