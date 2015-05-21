using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


	public interface ITourAttaque : ITour
    {
        float Portee { get; }
        float Dommage { get;  }
        float CadenceTir { get; }
        float Cout { get; }
        bool PossedeCible { get;  }

        int Niveau { get; set; }

        //fonction qui est appelé lorsqu'on amélirore une tour
        void Améliorer();

	}

