using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlaInterface : MonoBehaviour {

	private ControlaJogador scriptControlaJogador;
	public Slider SliderVidaJogador;

	public GameObject PainelGameOver;

	public Text TextoTempoSobrevivencia;
	public Text TextoTempoSobrevivenciaMaximo;
	private float tempoMaximoSobrevivencia;
	private int quantidadeDeZumbisMortos;
	public Text TextoQuantidadeDeZumbisMortos;
	public Text TextoQuantidadeDeBalas;

	// Use this for initialization
	void Start () {
		scriptControlaJogador = GameObject.FindWithTag ("Jogador")
			.GetComponent<ControlaJogador> ();

		SliderVidaJogador.maxValue = scriptControlaJogador.VidaInicial();
		SliderVidaJogador.value = scriptControlaJogador.VidaInicial ();
		PainelGameOver.SetActive (false);
		Time.timeScale = 1;

		tempoMaximoSobrevivencia = PlayerPrefs.GetFloat ("tempoMaximoSobrevivencia");
		TextoQuantidadeDeZumbisMortos.text = "x" + 0;
	}


	public void AtualizarSliderVidaJogador(){
		SliderVidaJogador.value = scriptControlaJogador.Vida();
	}

	public void GameOver(){
		
		var timeFloat = Time.timeSinceLevelLoad;
		PainelGameOver.SetActive (true);
		Time.timeScale = 0;

		var tempo = converteTempo (timeFloat);

		TextoTempoSobrevivencia.text = "Você sobreviveu por "+tempo.Minutos+"min e "+tempo.Segundos+"seg";

		AjustarPontuacaoMaxima (timeFloat);
	}

	public void Reiniciar(){
		SceneManager.LoadScene ("game");
	}

	void AjustarPontuacaoMaxima(float tempoSobrevivido){
		if (tempoSobrevivido > tempoMaximoSobrevivencia) {
			tempoMaximoSobrevivencia = tempoSobrevivido;

			PlayerPrefs.SetFloat ("tempoMaximoSobrevivencia", tempoMaximoSobrevivencia);
		}

		var tempo = converteTempo (tempoMaximoSobrevivencia);

		TextoTempoSobrevivenciaMaximo.text = string.Format ("Seu melhor tempo é {0}min e {1}seg", tempo.Minutos, tempo.Segundos);
	}

	public void AtualizarQuantidadeDeZumbisMortos(){
		quantidadeDeZumbisMortos++;
		TextoQuantidadeDeZumbisMortos.text = "x" + quantidadeDeZumbisMortos;
	}

	Tempo converteTempo(float tempo){
		int minutos = (int)( tempo / 60);
		int segundos = (int)(tempo % 60);

		return new Tempo (minutos, segundos);
	}

	public void AtualizarQuantidadeDeBalas(int balasArma, int balasTotal){
		TextoQuantidadeDeBalas.text = string.Format ("{0} | {1}", balasArma, balasTotal);
	}


}



