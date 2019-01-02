using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectCounter : MonoBehaviour {


    public Text valueText;
    public float value;
    private GameObject slider;
    public static int val;


    static public float sliderValue;

    private void Awake()
    {
    }

    // Use this for initialization
    public void Start()
    {
        valueText = GetComponent<Text>();
        //   timeSliderGet = GameObject.Find("TimeSlider").GetComponent<Slider>().value;
    }

    // Update is called once per frame
    public void TextUpdate(float value)
    {
        val = (int)value;
        valueText.text = Mathf.RoundToInt(value).ToString();
    }


    public static int EnvioNum()
    {
    

        return val;
    }
}
