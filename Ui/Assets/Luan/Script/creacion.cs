using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class creacion : MonoBehaviour
{

    public LevelManagementData data;
    public RecolectableObstaculos imgs;
    public GameObject recolectable;
    public GameObject obstaculo;
    public GameObject laser;
    public GameObject casilla;
    public GameObject reflect;
    public GameObject switche;
    public GameObject puertaStatic;
    public GameObject puertaStay;



    private Sprite[] imgRecolect;
    private Sprite[] imgObs;
    private Vector2[,] vectores;
    private int[,] matriz;

    private void Awake()
    {
        vectores = data.getVectors();
        imgObs = imgs.Obstaculos;

        if (gameObject.scene.name.Contains("1"))
        {
        matriz = data.getmatriz(1);

        } else if (gameObject.scene.name.Contains("2"))
        {
            matriz = data.getmatriz(2);

        }
        else if (gameObject.scene.name.Contains("3"))
        {
            matriz = data.getmatriz(3);

        }
        else if (gameObject.scene.name.Contains("4"))
        {
            matriz = data.getmatriz(4);

        }
        else
        {
            matriz = data.getmatriz(0);

        }


        data.contarRecolectables(matriz);


    }


    // Use this for initialization
    void Start()
    {

       // data.creacion();

        for (int i = 0; i < data.alto; i++)
        {

            for (int j = 0; j < data.ancho; j++)
            {

                GameObject casillaTemp = Instantiate(casilla, LevelManagementData.vector(vectores[i, j]), Quaternion.identity);
                casillaTemp.GetComponent<Transform>().parent = GetComponentsInChildren<Transform>()[0];



                if (matriz[i, j] == 2) // obstaculo
                {
                    GameObject obstaculoTemp = Instantiate(obstaculo, LevelManagementData.vector(vectores[i, j]), Quaternion.identity);
                    obstaculoTemp.GetComponent<Transform>().parent = GetComponentsInChildren<Transform>()[1];
                    obstaculoTemp.GetComponent<SpriteRenderer>().sprite = (imgObs[(int)Random.Range(0, 2)]);

                }
                else if (matriz[i, j] == 3) // recolectables
                {
                    int random = Random.Range(0, 3);
                    GameObject recolectableTemp = Instantiate(recolectable, LevelManagementData.vector(vectores[i, j]), Quaternion.identity);
                    recolectableTemp.GetComponent<Transform>().SetParent(transform.Find("Recolectables").transform);
                    recolectableTemp.GetComponent<recolectable>().puntaje = data.getPuntos()[random];


                }
                else if (matriz[i, j] == 5) // laser
                {
                    GameObject laserTemp = Instantiate(laser, LevelManagementData.vector(vectores[i, j]), Quaternion.identity);
                    laserTemp.GetComponent<Transform>().parent = GetComponentsInChildren<Transform>()[1];


                }
                else if (matriz[i, j] == 6) // reflector
                {
                    GameObject reflectorTemp = Instantiate(reflect, LevelManagementData.vector(vectores[i, j]), Quaternion.identity);
                    reflectorTemp.GetComponent<Transform>().parent = GetComponentsInChildren<Transform>()[1];


                }
                else if (matriz[i, j] == 7) // puerta
                {
                    GameObject puertaTemp = Instantiate(puertaStatic, LevelManagementData.vector(vectores[i, j]), Quaternion.identity);
                    puertaTemp.GetComponent<Transform>().parent = GetComponentsInChildren<Transform>()[1];


                }
                else if (matriz[i, j] == 8) //switche
                {
                    


                }

            }
        }




    }
}
