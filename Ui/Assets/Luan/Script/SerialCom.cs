using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

public class SerialCom : MonoBehaviour
{

    SerialPort stream;

    private int inicioTime, time;
    private String numero;
    private int n;

    public int XPosMaster, YPosMaster;

    private int[] posiciones, rotacion;


   private int[,] lecturaPosRota;

  





    // Use this for initialization
    void Start()
    {

        XPosMaster = 10;
        YPosMaster = 30;
        posiciones = new int[20];
        rotacion = new int[4];
        stream = new SerialPort("COM7", 9600);
        stream.ReadTimeout = 50;
        stream.Open();

    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(stream.ReadLine());
        
        StartCoroutine(AsynchronousReadFromArduino
        ((string s) => Debug.Log(s),     // Callback
        () => Debug.LogError("Error!"), // Error callback
        1000f                          // Timeout (milliseconds)
        ));


    }

    private void enviarArduino(String s) {
        stream.WriteLine(s);
        stream.BaseStream.Flush();    

    }

    public void callbackFun(String s)
    {
        if (s.Equals("esperoR"))
        {
            enviarArduino("inicio");
        }

        if (s.Equals("necesito Ubicacion")) {
            
         //   enviarArduino("necesitoUbicacion "+ XPosMaster.ToString() + " " + YPosMaster.ToString());
            enviarArduino("necesitoUbicacion");
        }

            Debug.Log("ME LLLEGO:: " + s);

       
    }

    public IEnumerator AsynchronousReadFromArduino(Action<string> callback, Action fail = null, float timeout = float.PositiveInfinity)
    {
        DateTime initialTime = DateTime.Now;
        DateTime nowTime;
        TimeSpan diff = default(TimeSpan);

        string dataString = null;

        do
        {
            try
            {
                dataString = stream.ReadLine();
            }
            catch (TimeoutException)
            {
                dataString = null;
            }

            if (dataString != null)
            {
                callback(dataString);

                yield break; // Terminates the Coroutine
            }
            else
                yield return null; // Wait for next frame

            nowTime = DateTime.Now;
            diff = nowTime - initialTime;

        } while (diff.Milliseconds < timeout);

        if (fail != null)
            fail();
        yield return null;
    }
}
