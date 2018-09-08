using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gerador : MonoBehaviour {

	public GameObject PrefabSpawnar;

	float contadorTempo = 0;
	public float TempoGerarObjeto = 1;

	public LayerMask LayerNaoInstanciarObjeto;
	public float distanciaDeGeracao = 5;
	public float DistanciaJogadorParaGeracao = 20;
	public int NumeroMaximoDeObjetosParaGerar = 5;
	private GameObject jogador;
	private List<GameObject> objetosGerador;

	public Color CorGerador = Color.yellow;


	// Use this for initialization
	void Start () {
		jogador = GameObject.Find ("Jogador");
		objetosGerador = new List<GameObject> ();
	

		for (int i = 0; i < NumeroMaximoDeObjetosParaGerar; i++) {
			StartCoroutine (GerarNovoObjeto(PrefabSpawnar));
		}
		StartCallback ();
	}

	protected virtual void StartCallback(){
		
	}

	// Update is called once per frame
	void Update () {

		var distanciaJogador = Vector3.Distance (transform.position, jogador.transform.position);
		if (distanciaJogador > DistanciaJogadorParaGeracao && 
			objetosGerador.Count < NumeroMaximoDeObjetosParaGerar) {

			contadorTempo += Time.deltaTime;

			if(contadorTempo >= TempoGerarObjeto){

				StartCoroutine(GerarNovoObjeto (PrefabSpawnar));

				contadorTempo = 0;
			}
		}

		UpdateCallback ();
	}

	protected virtual void UpdateCallback(){

	}

	void OnDrawGizmos(){
		Gizmos.color = CorGerador;
		Gizmos.DrawWireSphere (transform.position, distanciaDeGeracao);
	}

	IEnumerator GerarNovoObjeto(GameObject prefabObjeto){
		var posicaoDeCriacao = AleatorizarPosicao ();
		Collider[] colisores = Physics.OverlapSphere (posicaoDeCriacao, 1, LayerNaoInstanciarObjeto);

		while (colisores.Length > 0) {
			posicaoDeCriacao = AleatorizarPosicao ();
			colisores = Physics.OverlapSphere (posicaoDeCriacao, 1, LayerNaoInstanciarObjeto);

			yield return null;
		}
		GameObject objeto = Instantiate (prefabObjeto, posicaoDeCriacao, transform.rotation);
		IGerado objetoGerado = objeto.GetComponent<IGerado> ();


		objetoGerado.SetGerador (this);
		//inimigo.SetVelocidade (5);
		objetosGerador.Add(objeto);

	}

	Vector3 AleatorizarPosicao(){
		var posicao = Random.insideUnitSphere * distanciaDeGeracao;
		posicao += transform.position;
		posicao.y = transform.position.y;

		return posicao;
	}

	public void RemoverObjeto(GameObject objeto){
		objetosGerador.Remove (objeto);
	}

}
