using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GenerateurMonstre : MonoBehaviour
{

    const int NB_ENNEMIS_VAGUE1 = 10;
    const int TEMPS_ENTRE_VAGUES = 8;
    const float DELAI_ENTRE_MONSTRE = .3f;

    int NbEnnemisVague_ { get; set; }
    int TempsEntreVagues_ { get; set; }
    bool GenerantVague { get; set; }
    Vector3 Position_ { get; set; }

    List<Ennemi> ListeEnnemis { get; set; }
    
    int nbEnnemisActuel = 0;
    float tempsEcouleEnnemi = 0;
    float tempsEcouleVague = 0;
    int IDEnnemisVague;
    
    public GameObject Prefab;

    void Start()
    {
        NbEnnemisVague_ = NB_ENNEMIS_VAGUE1;
        TempsEntreVagues_ = TEMPS_ENTRE_VAGUES;
        GenerantVague = true;
        Position_ = gameObject.transform.position;
    }

    void Start(int Nb_EnnemisVague1, int TempsEntreVague)
    {
        NbEnnemisVague_ = Nb_EnnemisVague1;
        TempsEntreVagues_ = TempsEntreVague;
    }

    void Update()
    {
        if (!GenerantVague)
        {
            tempsEcouleVague += Time.deltaTime;
            if (tempsEcouleVague >= TEMPS_ENTRE_VAGUES)
            {
                ConfigurerProchaineVague();
                GenerantVague = true;
                tempsEcouleVague = 0;
            }
        }
        else
            GenererVague();
    }


    void GenererVague()
    {
        if (nbEnnemisActuel <= NbEnnemisVague_)
        {
            tempsEcouleEnnemi += Time.deltaTime;
            if (tempsEcouleEnnemi >= DELAI_ENTRE_MONSTRE)
            {
                CreerEnnemi();
                tempsEcouleEnnemi = 0;
                ++nbEnnemisActuel;
            }
        }
        else
        {
            nbEnnemisActuel = 0;
            GenerantVague = false;
        }
    }


    void CreerEnnemi()
    {
        GameObject ennemi = Instantiate(Prefab,Position_, gameObject.transform.rotation) as GameObject;
    }

    void ConfigurerProchaineVague()
    {

    }

}