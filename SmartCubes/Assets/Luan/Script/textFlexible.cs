using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode()]
[RequireComponent(typeof(Text))]
public class textFlexible : MonoBehaviour {

    public LevelManagementData data;
    public int yuknow;


    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();           
    }
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

        switch (yuknow)
        {
            case 1:
                text.text = data.getTurnos().ToString();
                break;

            case 2:
                text.text = data.tiempo();
                break;
            case 3:
                text.text = LevelManagementData.pjPuntos.ToString();
                break;
        }
    }
}
