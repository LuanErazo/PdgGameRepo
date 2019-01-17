using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class recolectable : MonoBehaviour {

    private Sprite mySprite;
    public int puntaje;

    private Animator anim;

    private void Awake()
    {
      mySprite = GetComponent<SpriteRenderer>().sprite;
        anim = GetComponent<Animator>();
        anim.speed = Random.Range(.3f,.5f);
    }

    // Use this for initialization
    void Start()
    {
    }

  

    // Update is called once per frame
    void Update()
    {
   
      
    }


    public void setmySprite(Sprite mySprite)
    {
        this.mySprite = mySprite;
    }
}
