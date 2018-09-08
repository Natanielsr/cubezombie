using UnityEngine;
using System.Collections;

public abstract class ControlaPersonagem : MonoBehaviour, IMatavel, ICuravel
{
	protected Status meuStatus;
	protected ControlaInterface scriptControlaInterface;
	protected Rigidbody meuRigidbody;
	protected Animator meuAnimator;



	void Awake(){
		meuStatus = GetComponent<Status> ();
		meuRigidbody = GetComponent<Rigidbody> ();
		meuAnimator = GetComponent<Animator> ();
		scriptControlaInterface = GameObject.FindObjectOfType (typeof(ControlaInterface)) as ControlaInterface;

	}

	//TOMAR DANO
	public virtual void TomarDano(int dano){
		
		meuStatus.TomarDano(dano);
		TomarDanoCallback (dano);
		if (meuStatus.GetVidaAtual() <= 0) {
			Morrer ();
		}
	}
	protected virtual void TomarDanoCallback(int dano){
		
	}
	//

	//MORRER
	public virtual void Morrer(){
		
	}
	//

	//CURARVIDA
	public void CurarVida(int quantidadeDeCura){
		meuStatus.Curar (quantidadeDeCura);
		CurarVidaCallback ();
	}
	protected virtual void CurarVidaCallback(){}
	//

	//GET
	public int Vida(){
		return meuStatus.GetVidaAtual ();
	}
	public int VidaInicial(){
		return meuStatus.VidaInicial;
	}

	public void SetVelocidade(float velocidade){
		this.meuStatus.Velocidade = velocidade;
	}
	//

	//MOVIMENTO
	protected void Movimentar(Vector3 direcao, float velocidade){
		meuRigidbody.MovePosition (
			meuRigidbody.position + direcao.normalized * velocidade * Time.deltaTime
		);
	}
	protected void Rotacionar(Vector3 direcao){
		Quaternion novaRotacao = Quaternion.LookRotation (direcao);
		meuRigidbody.MoveRotation (novaRotacao);
	}
	//

	//ANIMACAO
	protected void AtacarAnimacao(bool atacando){
		meuAnimator.SetBool ("Atacando", atacando);
	}
	protected void MovimentarAnimacao(float valorMovimento){
		meuAnimator.SetFloat ("Movendo", valorMovimento);
	}
	protected void MorrerAnimacao(){
		meuAnimator.SetTrigger ("Morrer");
	}
	//

	protected int CalculaDano(){
		var danoGerado = this.meuStatus.dano + Random.Range (-10, 10);

		var critico =  (Random.Range (0, 100) < this.meuStatus.PorcentagemCritico);
		if (critico) {
			danoGerado = danoGerado * 2;
			Debug.Log ("CRITICO!");
		}
		Debug.Log ("Dano "+danoGerado);

		return (int)danoGerado;
	}


}

