using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//Class controlling the game

public class Player : MonoBehaviour
{

    const int ARGENT_DEPART = 1000;
    const int ARGENT_MAX = 100000;
    const int NB_VIES_DEPART = 20;


    //Attributs pour la création d'une nouvelle tour
    Ray ray;
    RaycastHit hit;
    Vector3 PosCurseur;
    //Will be instansiate by unity
    public GameObject prefab;



    int Score { get; set; }
    int Argent { get; set; }
    public int NbVieRestantes { get; protected set; }
    public Map MapCourante { get; protected set; }

    List<GameObject> ListTours { get; set; }

    // Use this for initialization
    void Start()
    {
        NbVieRestantes = NB_VIES_DEPART;
        Argent = ARGENT_DEPART;
        ListTours = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

        //Vérification du clic du joueur afin de potentiellement placer une tour
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        PosCurseur = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       // if(Physics.Raycast(ray, out hit))
        //{
            if(Input.GetMouseButtonDown(0))
            {
                if (!VerifierCollision(new Vector2(PosCurseur.x, PosCurseur.y)))
                {
                    GameObject nTour = Instantiate(prefab, new Vector3(PosCurseur.x, PosCurseur.y, 0), Quaternion.identity) as GameObject;

                    ListTours.Add(nTour);

                }

            }
      //  }
    }

    private bool VerifierCollision(Vector2 pos)
    {
        bool EnCollision = false;
        
        //On débute par vérifier si l'on entre en collision avec l'ensemble des colliders de tous les tours  (incluant la zone de tir)
        foreach (GameObject tour in ListTours)
        {
            //Si on détecte une collsion
            if (tour.collider2D.OverlapPoint(pos))
            {
                //On parcours les enfants afin de trouver la ou les bases de la tour
                foreach (Transform child in tour.transform)
                {
                    if (child.collider2D.bounds.Intersects(new Bounds(new Vector3(pos.x, pos.y, 0), tour.collider2D.bounds.size)))
                    {
                        EnCollision = true;
                        print("Collision détecté avec le placement d'une tour.");
                        return EnCollision;
                    }
                }
            }
        }

        return EnCollision;
    }

    void PerdreUneVie()
    {
        --NbVieRestantes;
    }
}