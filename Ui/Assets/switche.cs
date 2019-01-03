using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switche : MonoBehaviour {

   public  SpriteRenderer on;
     public SpriteRenderer off;
    public GameObject puerta;

    private bool puertaActive = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (name.Contains("SwitchStatic"))
        {
        on.gameObject.SetActive(!on.gameObject.activeSelf);
        off.gameObject.SetActive(!off.gameObject.activeSelf);
            puertaActive = !puertaActive;

        }

        if (name.Contains("SwitchStay"))
        {
            on.gameObject.SetActive(true);
            off.gameObject.SetActive(false);
            puertaActive = true;
        }
       
        puerta.GetComponent<puerta>().ReceiveTrigger(ref collision);
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (name.Contains("SwitchStay"))
        {
            on.gameObject.SetActive(false);
            off.gameObject.SetActive(true);
            puertaActive = false;

        }
    }

    public bool getActive() {
        return puertaActive;
    }

    public void setActive(bool active) {
        this.puertaActive = active;
    }

    

    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
