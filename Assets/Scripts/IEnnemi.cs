using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


	public interface IEnnemi
	{
        float NbVieMax { get; set; }
        float NbVieRestante { get; set; }
        float vitesse { get; set; }

		void Pathfinding();
		void RecevoirDommage(float degat);
		bool EstVivant();
	}

