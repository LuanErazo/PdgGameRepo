using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreacionCasillas : MonoBehaviour {

    public int ancho;
    public int alto;
    public GameObject casilla;

    public Material trans;

    public void Crear() {
        int cont = 0;
        for (int i = 0; i < ancho; i++)
        {
            for (int j = 0; j < alto; j++)
            {
               GameObject casillaTemp = Instantiate(casilla, new Vector3(j, i, 0), Quaternion.identity);

                casillaTemp.GetComponent<Casilla>().PonerColor(trans);

                casillaTemp.GetComponent<Casilla>().Numcasilla = cont;

                cont++;

            }
        }
    }

}
