using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level Management Data")]
public class LevelManagementData : ScriptableObject {



    private Vector2[,] vectores;

    public int ancho;
    public int alto;

    public static int pjPuntos;
    public int puntajito = 0;
    public bool disparo = false;


    private int[] recolectPuntos = new int[3];

    public int turnos;
    public int recolectables;
    public int tiempoxTurno;
    public int obstaculos;
    public int nivel;
    public int x;
    public int y;

    private int idPuerta;
    private int idpuertaTemp = -1;
   
    public Sprite[] lvls = new Sprite[3];

    private int[,] matrizRef;

    private float timer;
    private bool cambio;

    private int[] puertas;

    //private int[,] matrizRef = new int[5, 8] { { 0, 0, 0, 0, 0, 0, 0, 0}, 
    //                                           { 0, 0, 0, 0, 0, 0, 0, 0 }, 
    //                                           { 0, 0, 0, 0, 0, 0, 0, 0 },
    //                                           { 0, 0, 0, 0, 0, 0, 0, 0 },
    //                                           { 0, 0, 0, 0, 0, 0, 0, 0 } };



    private void OnEnable()
    {
        idpuertaTemp = -1;
        if (recolectables <=0)
        {
            recolectables = 4;
        }
        cambio = false;
        pjPuntos = puntajito;
        matrizRef = new int[alto, ancho];
        vectores = new Vector2[alto, ancho];

        timer = (tiempoxTurno) * 60;

        recolectPuntos[0] = 500;
        recolectPuntos[1] = 1000;
        recolectPuntos[2] = 100;


        matrizRef[y, x] = 1;

        matrizRef[1, 4] = 6;
        matrizRef[y, 4] = 5;

        matrizRef[2, 5] = 8;




        float offset = (float)1.85;
        float initX = (float)-8.275259;
        float initY = (float)-4.8803;
        int num = 0;
        for (int i = 0; i < alto; i++)
        {
            for (int j = 0; j < ancho; j++)
            {
                Vector2 vectorTemp = new Vector2(initX + (j * offset), initY + (i * offset));
                vectores[i, j] = vectorTemp;
                if (matrizRef[i, j] == 8)
                {
                    num++;
                }
            }

            //--------------------------------------Creacion mapa-------------------------
            
        }

        puertas = new int[num*2];
        idPuerta = num;
        num = 0;

        for (int i = 0; i < alto; i++)
        {
            for (int j = 0; j < ancho; j++)
            {
                if (matrizRef[i, j] == 8)
                {
                    ubicacionPuertas(i, j, num);
                    num++;
                }
            }

            //--------------------------------------Creacion mapa-------------------------

        }


    }

    public int darId() {
        Debug.Log(idpuertaTemp);
        idpuertaTemp++;
        Debug.Log(idpuertaTemp);
        return idpuertaTemp;
    }



    public void creacion() {
        int count = 0;
        while (count < obstaculos)
        {

            int yram = Random.Range(0, alto);
            int xram = Random.Range(0, ancho);
            if (matrizRef[yram, xram] == 0)
            {

                matrizRef[yram, xram] = 2;
                count++;
            }


        }

        count = 0;
        while (count < recolectables)
        {

            int yram = Random.Range(0, alto);
            int xram = Random.Range(0, ancho);
            if (matrizRef[yram, xram] == 0)
            {

                matrizRef[yram, xram] = 3;
                count++;
            }

        }
    }


    public string tiempo() {


        string timeShow = "";
        timeShow = tiempoxTurno.ToString("00") + ":00";

        if (cambio)
        {
            timer -= Time.deltaTime;
            float minutes = Mathf.Floor(timer / 60);
            float seconds = Mathf.Floor(timer % 60);
            string seg = seconds.ToString("00");
     
            timeShow = minutes.ToString("00") + ":" + seg;

            if (minutes < 0)
            {
                timeShow = "00:00";
            }
        }
        return timeShow;
    }


    public static void addPuntos(int puntos) {
        pjPuntos += puntos;
    }

    public void setTurnos(int turnos)
    {
        this.turnos = turnos;
    }

    public void setRecolectables(int recolectables)
    {
        this.recolectables = recolectables;
    }

    public void setTiempoxTurno(int TiempoxTurno)
    {
        this.tiempoxTurno = TiempoxTurno;
    }

    public void setobstaculos(int obstaculos)
    {
        this.obstaculos = obstaculos;
    }

    public void setNivel(int nivel)
    {
        this.nivel = nivel;
    }

    private void ubicacionPuertas(int i, int j, int loop) {
        puertas[loop*2] = i;
        puertas[(loop*2) + 1] = j;
    }

    public int[] getPuertas() {
        return puertas;
    }

    public int[] getPuerta(int id) {
        int[] puertaPos = new int[2];
        puertaPos[0] =  puertas[id * 2];
        puertaPos[1] = puertas[(id * 2) + 1];
        return puertaPos;
    }


    public int getTurnos()
    {
        return turnos;
    }

    public void setInit(bool init)
    {
        this.cambio = init;
    }

    public int getRecolectables()
    {
        return recolectables;
    }

    public int getTiempo()
    {
        return tiempoxTurno;
    }
    public int getObstaculos()
    {
        return obstaculos;
    }

    public int getNivel()
    {
        return nivel;
    }

    public Vector2[,] getVectors()
    {
        return vectores;
    }

    public int[,] getmatriz()
    {
        return matrizRef;
    }
    public void setMatriz(int[,] matriz)
    {
        this.matrizRef = matriz;
    }


    public int[] getPuntos()
    {
        return recolectPuntos;
    }

    public Sprite nivels(int index)
    {
        return lvls[index];
    }


    public static Vector2 vector(Vector2 vector) {
        return new Vector2(vector.x, vector.y);
    }

}
