using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puerta : MonoBehaviour {

    public LevelManagementData data;
    public switche switche;
    public int id;

    private Animator anim;
    private int abrir = Animator.StringToHash("abrir");
    private int cerrar = Animator.StringToHash("cerrar");

    private int[,] matriz;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        id = data.darId();
        if (gameObject.scene.name.Contains("1"))
        {
            matriz = data.getmatriz(1);

        }
        else if (gameObject.scene.name.Contains("2"))
        {
            matriz = data.getmatriz(2);

        }
        else if (gameObject.scene.name.Contains("3"))
        {
            matriz = data.getmatriz(3);

        }
        else if (gameObject.scene.name.Contains("4"))
        {
            matriz = data.getmatriz(4);

        }
        else
        {
            matriz = data.getmatriz(0);

        }
    }

    public void ReceiveTrigger(ref Collider2D col)
    {
        OnTriggerEnter2D(col);
        OnTriggerExit2D(col);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (switche.name.Contains("SwitchStatic"))
        {
            int[] posPuerta = data.getPuerta(id);
            if (switche.getActive())
            {

                matriz[posPuerta[0], posPuerta[1]] = 0;
                anim.SetBool(abrir, true);

                anim.SetBool(cerrar, false);
            }
            else
            {
                matriz[posPuerta[0], posPuerta[1]] = 7;

                anim.SetBool(abrir, false);
                anim.SetBool(cerrar, true);
            }

        }



    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        data.setMatriz(matriz);
    }


    // Update is called once per frame
    void Update()
    {
	}
}
