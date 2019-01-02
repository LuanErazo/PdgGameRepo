using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : MonoBehaviour {

    public int x;
    public int y;
    public int rotacion;


    private bool Xx;
    private bool Yy;


    private void Start()
    {
        transform.Rotate(0,0,45);
    }

}
