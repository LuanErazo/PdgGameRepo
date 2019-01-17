using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class turnosGame : MonoBehaviour {

    private TextMeshProUGUI myText;
    // Use this for initialization
    void Start()
    {
        myText = GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.transform.parent.gameObject.activeSelf)
        {

            myText.SetText(LevelManagementData.getSturnos().ToString());
        }

    }
}
