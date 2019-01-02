using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DoozyUI;

public class CountDownTimer : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI uiText;



    public static CountDownTimer instance = null;

    public LevelManagementData data;

    public float startTimer;
    private bool canCount = true;
   

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

    }

    private void Start()
    {



    }
    private void Update()
    {

        
        

        if (startTimer < 1)
        {
            uiText.text = null;
            data.setInit(true);
            StopCoroutine(ContadorMed());
            //GameObject.Destroy(GameObject.Find("Background"));

        }
        else
        {
            StartCoroutine(ContadorMed());

        }

    }

    private IEnumerator ContadorMed()
    {
        //if (startTimer >= 0.0f && canCount)
        //{
        //}
            startTimer -= Time.deltaTime;
            uiText.text = startTimer.ToString("F0");
            yield return new WaitForSeconds(1);
    }

    public float getStartTimer() {
        return startTimer;
    }
}
