using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public LevelManagementData data;
    public Button mapSelector;
  
    private int sceneNumber;


    public void Reader()
    {

        switch (data.getNivel())
        {
            // MAPA 1 ------------------------
            case 1:
                GetComponent<SpriteRenderer>().sprite = data.nivels(0);
                break;
            // MAPA 2 ------------------------
            case 2:
               GetComponent<SpriteRenderer>().sprite = data.nivels(1);
                
                break;
            // MAPA 3 ------------------------
            case 3:
                GetComponent<SpriteRenderer>().sprite = data.nivels(2);
                break;
        }
    }
}
