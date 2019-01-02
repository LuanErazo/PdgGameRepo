using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casilla : MonoBehaviour {

    public Material material;
    public int Numcasilla;

	// Use this for initialization
	void Start () {

	}

    public void PonerColor(Material material) {
        GetComponent<MeshRenderer>().material = material;
        this.material = material;


    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        print(Numcasilla.ToString());

    }
}
