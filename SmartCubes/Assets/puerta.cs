﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puerta : MonoBehaviour {

    public LevelManagementData data;
    public switche switche;
    public int id;

    private Animator anim;
    private int abrir = Animator.StringToHash("abrir");
    private int cerrar = Animator.StringToHash("cerrar");

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        id = data.darId();
    }

    public void ReceiveTrigger(ref Collider2D col)
    {
        OnTriggerEnter2D(col);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (switche.name.Contains("SwitchStatic"))
        {
            int[,] matriz = data.getmatriz();
            int[] posPuerta = data.getPuerta(id);

            if (switche.getActive())
            {
                matriz[posPuerta[0], posPuerta[1]] = 0;
                anim.SetBool(abrir, true);

                anim.SetBool(cerrar, false);
            }
            else
            {
                matriz[posPuerta[0], posPuerta[1]] = 8;

                anim.SetBool(abrir, false);
                anim.SetBool(cerrar, true);
            }

        }



    }

    // Update is called once per frame
    void Update()
    {
	}
}
