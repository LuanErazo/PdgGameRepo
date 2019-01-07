﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeedShowValue : MonoBehaviour
{


    public Text valueText;
    public float value;
    private GameObject slider;
    private Timer timer;
    public static int val;


    static public float sliderValue;

    private void Awake()
    {
        timer = GameObject.FindObjectOfType<Timer>();

    }

    // Use this for initialization
    public void Start()
    {
        valueText = GetComponent<Text>();
       // slider = GameObject.Find("TimeSlider").GetComponent<Slider>().value;
    }

    // Update is called once per frame
    public void TextUpdate(float value)
    {
        val = (int)value;
        valueText.text = Mathf.RoundToInt(value).ToString();
        Debug.Log(valueText.text);
    }


    public static int EnvioNum()
    {
        Debug.Log("SpeedValue:"+" "+val);

        return val;
    }


}
