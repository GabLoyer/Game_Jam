using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class TourDeBase : MonoBehaviour, ITourAttaque
	{
        protected enum EPortee  { N1 = 50, N2 = 100, N3 = 150, N4 = 200 }
        protected enum EDommage { N1 = 50, N2 = 100, N3 = 150, N4 = 200 }
        protected enum ECout { N1 = 50, N2 = 100, N3 = 150, N4 = 200 }
        //Cadence de tir, en Tir par  msseconde
        protected enum ECadenceTir { N1 = 500, N2 = 400, N3 = 300, N4 = 200 }


        public const int NIVEAU_MAX = 4;
		public const float POURCENTAGE_REVENTE = 0.5f;

		const float HAUTEUR_DEFAUT = 1;
		const float LARGEUR_DEFAUT = 1;


        protected EPortee _portee { get; set; }
        public float Portee { get { return (float)_portee; } }

        protected EDommage _dommage { get; set; }
        public float Dommage { get { return (float)_dommage; } }

        protected ECadenceTir _cadenceTir { get; set; }
        public float CadenceTir { get { return (float)_cadenceTir; } }

        protected ECout _cout { get; set; }
        public float Cout { get { return (float)_cout; } }


        public int Niveau { get; set; }

		public Vector2 Position { get; set; }
        public float largeur { get; set; }
        public float hauteur { get; set; }
        public bool PossedeCible { get; set; }


		float tempsEcoules;
        Ennemi Cible { get; set; }

        public int ValeurRevente
        {
            get
            {
                return (int)(Cout * POURCENTAGE_REVENTE);
            }
        }



		public void Start () 
        {
		    _portee = EPortee.N1;
			_dommage = EDommage.N1;
			_cadenceTir = ECadenceTir.N1;
			_cout = ECout.N1;

            Niveau = 0;

			largeur = LARGEUR_DEFAUT;
            hauteur = HAUTEUR_DEFAUT;

		    Position = new Vector2(this.collider2D.bounds.center.x,this.collider2D.bounds.center.y);              
			tempsEcoules = CadenceTir;
		}

		// Update is called once per frame
		void Update () {
			if(Cible != null)
			{
				Tirer ();
			}
		}



        public void Améliorer()
        {            
            if ( Niveau < NIVEAU_MAX)
            {
                ++Niveau;
                ++_cout;
                ++_cadenceTir;
                ++_dommage;
                ++_portee;
            }
        }

		void OnTriggerStay2D(Collider2D obj) 
		{
			if (obj.gameObject.tag == "Ennemi") {
				Cible = obj.gameObject.GetComponent<Ennemi>();
				 
			}
		}

		public void Tirer()
		{
			tempsEcoules += Time.deltaTime;
			Viser (); 
			if(tempsEcoules >= CadenceTir / 1000.0f)
			{
				Cible.RecevoirDommage(Dommage);
				if(!Cible.EstVivant())
				{
					Cible = null;
				}
				tempsEcoules = 0;
			}
		}

	//values for internal use
	private Quaternion _lookRotation;
	private Vector3 _direction;

	void Viser()
	{
		float RotationSpeed = 5f;
		Transform canon = transform.FindChild("Cannon");
		//gameObject.transform.FindChild("Cannon").transform.LookAt(Cible.transform);
		//find the vector pointing from our position to the target
		_direction = (Cible.transform.position - canon.position).normalized;
		//create the rotation we need to be in to look at the target

		/*_lookRotation = Quaternion.LookRotation(_direction);
		_lookRotation.SetLookRotation(_direction, new Vector3( ));

		canon.rotation = Quaternion.Slerp (canon.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
		//canon.rotation = Quaternion.SetLookRotation (_direction, new Vector3 (0, 0, -1));

		//canon.rotation.SetLookRotation (_direction, );
*/
		float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg - 90;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		canon.rotation = Quaternion.Slerp(canon.rotation, q, Time.deltaTime * RotationSpeed);

	}

}

