using UnityEngine;
using System.Collections;

public class Ennemi : MonoBehaviour, IEnnemi {


    //Santé maximal de l'ennemi
	const float VIE_MAX_DEFAUT = 5;
    //Dégat causé par l'ennemi lorsqu'il franchi la deadzone
    const int VAL_DEGAT_DEFAUT = 1;

	public float NbVieMax { get; set; }
	public float NbVieRestante {  get; set; }
	public float vitesse { get; set; }
    public int Degat { get; set; }


    public Ennemi(Vector3 posDepart)
    {
        Start();
        gameObject.transform.position = posDepart;

    }

	// Use this for initialization
	void Start () {
        NbVieMax = VIE_MAX_DEFAUT;
        NbVieRestante = NbVieMax;
        Degat = VAL_DEGAT_DEFAUT;
	}
	
	// Update is called once per frame
	void Update () {
		if (EstVivant ()) {
			Pathfinding();
		}
		else{
			Destroy(gameObject);
		}
	}

	public void RecevoirDommage(float degat)
	{
		NbVieRestante -= degat;
	}


	public bool EstVivant()
	{
		return NbVieRestante >= 0;
	}

	public void Pathfinding(){
        //TODO

        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - .002f, gameObject.transform.position.z);
	}
}
