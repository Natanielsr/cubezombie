using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorZumbis : Gerador {

	public int MaximoZumbis = 25;
	private float contatorDeAumentoDificuldade;
	public float tempoProximoAumentoDeDificuldade;

	protected override void StartCallback() {
		contatorDeAumentoDificuldade = tempoProximoAumentoDeDificuldade;
	}

	// Update is called once per frame
	protected override void UpdateCallback() {
		

		if (NumeroMaximoDeObjetosParaGerar <= MaximoZumbis) {
			if (Time.timeSinceLevelLoad > contatorDeAumentoDificuldade) {
				NumeroMaximoDeObjetosParaGerar++;
				contatorDeAumentoDificuldade = Time.timeSinceLevelLoad + tempoProximoAumentoDeDificuldade;
			}
		}
	}


}
