using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

	public interface ITour
	{
        float hauteur { get; set; }
        float largeur { get; set; }
        Vector2 Position { get; set; }
	}

