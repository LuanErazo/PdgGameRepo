using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pj : MonoBehaviour {


    public LevelManagementData data;
    public int idM;
    public float speed;
    public float speedX;


    private bool Bmove;


    private int mH, mV;

    private void Awake()
    {
            
    }

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        mH = (int)Input.GetAxisRaw("Horizontal");
        mV = (int)Input.GetAxisRaw("Vertical");



        if (Input.GetButtonDown("Horizontal") | Input.GetButtonDown("Vertical")) {
            Bmove = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("recolect"))
        {
            LevelManagementData.addPuntos(collision.GetComponent<recolectable>().puntaje);
            data.recolectables--;
            Destroy(collision.gameObject);
        }
    }

    public int getmH() {
        return mH;
    }

    public int getmV() {
        return mV;
    }

    public bool getBmove() {
        return Bmove;
    }

    public void setBmove(bool Bmove)
    {
        this.Bmove = Bmove;
    }



}
