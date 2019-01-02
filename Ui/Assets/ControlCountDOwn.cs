using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlCountDOwn : MonoBehaviour {


    private TextMeshProUGUI control;
	// Use this for initialization
	void Start () {
        control = GetComponentInChildren<CountDownTimer>().GetComponent<TextMeshProUGUI>();
	}
	
	// Update is called once per frame
	void Update () {
       
	}
}
