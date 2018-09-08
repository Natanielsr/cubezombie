

public class Tempo  {
	public int Minutos {
		get;
		private set;
	}
	public int Segundos {
		get;
		private set;
	}

	public Tempo(int Minutos, int Segundos){
		this.Minutos = Minutos;
		this.Segundos = Segundos;
	}

}
