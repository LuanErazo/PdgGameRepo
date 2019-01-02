using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OCShowValue : MonoBehaviour
{
    public LevelManagementData data;
    public Text valueText;


    private int value;
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        value = (int)slider.value;
      
        valueText.text = value.ToString();

    }


    public void setTurnos()
    {
        value = (int)slider.value;
        data.setTurnos(value);
        valueText.text = value.ToString();     
    }

    public void setRecolectables()
    {
        value = (int)slider.value;
        data.setRecolectables(value);
        valueText.text = value.ToString();
    }

    public void setTiempoxTurno()
    {
        value = (int)slider.value;
        data.setTiempoxTurno(value);
        valueText.text = value.ToString();
    }

    public void setobstaculos()
    {
        value = (int)slider.value;
        data.setobstaculos(value);
        valueText.text = value.ToString();
    }

}