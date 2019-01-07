using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Timer : MonoBehaviour
{
    public Text timerText;
    private float startTime, time = 10f;
    private TimeShowValue show;
    public CountDownTimer tiempo;
    private int valueTime;
    private bool startTimer;
    private float timer;

    public Color32 myColor;

    // Use this for initialization
    void Start()
    {
        startTime = Time.time;
        valueTime = TimeShowValue.EnvioNum();
        timer = valueTime * 60;
        //valueTime = 10;

        StartCoroutine(StartTimer(tiempo.getStartTimer()));

    }

    // Update is called once per frame
    public void Update()
    {


        if (startTimer)
        {
            timer -= Time.deltaTime;            
            float minutes = Mathf.Floor(timer / 60);
            float seconds = (timer % 60);

            print(string.Format("{0}:{1}", minutes, seconds));

            if (minutes <= 0)
            {
                timerText.text = 0 + ":" + 0;

            }
            else
            {
                timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
            }

            /*
            float t = Time.time - startTime;
            int minutos = valueTime - ((int)t / 60);
            float sec = 60 - (t % 60);

            string minutes = minutos.ToString();
            string seconds = sec.ToString("F2");

            if (minutos <= 0)
            {
                timerText.text = 0 + ":" + 0;

            }
            else
            {
                timerText.text = minutes + ":" + seconds;
            }


            if (minutos > 1 && minutos< 2)
            {
                timerText.color = myColor;
            } else if (minutos < 1)
            {
                timerText.color = new Color (255, 255, 0);
            }
            */
        }
        

    }

    IEnumerator StartTimer(float delay)
    {
        yield return new WaitForSeconds(delay);
        startTimer = true;
    }
    
        
    
}

