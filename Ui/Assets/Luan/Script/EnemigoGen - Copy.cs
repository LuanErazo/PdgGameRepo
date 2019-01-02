﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class EnemigoGen : MonoBehaviour {

    protected Sprite mySprite;

    protected void Awake()
    {
        mySprite = GetComponent<SpriteRenderer>().sprite;
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		

	}


    public void setmySprite(Sprite mySprite) {
        this.mySprite = mySprite;
    }

}
