using UnityEngine;
using DG.Tweening;
using System.Collections;

public class ControlMove : MonoBehaviour
{

    public LevelManagementData data;



    public GameObject pj;
    public GameObject recolectables;
    public GameObject Obstaculo;
    public GameObject gameover;

    private Vector2[,] vectores;
    private int[,] matriz;

    private int Mpos;




    private int x;
    private int y;
    private int[] movimientos;
    private SerialController serial;

    private Pj personaje;
    private int llegada;
    private bool mover;


    private void Awake()
    {
        movimientos = new int[5];
        vectores = data.getVectors();
        matriz = data.getmatriz();
        x = data.x;
        y = data.y;
    }

    // Use this for initialization
    void Start()
    {

        serial = GetComponent<SerialController>();
        pj.transform.position = vector(vectores[y, x]);
        personaje = pj.GetComponent<Pj>();

        serial.SendSerialMessage("A");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver() == false)
        {
            moverInput();
            if (llegada >= 5)
            {
                mover = true;
            }
            
            if (mover)
            {
                StartCoroutine(movimientoArdu());
                llegada = 0;
                mover = false;
            }


        }
        else
        {
        }

        comunicacion();
    }

    private void moverInput()
    {

        if (personaje.getBmove())
        {

            //matriz = data.getmatriz();

            if (y + 1 < data.alto && personaje.getmV() == 1)
            {
                if (matriz[y + 1, x] == 0 || matriz[y + 1, x] == 3)
                {
                    y += 1;
                    matriz[y, x] = 1;
                    matriz[y - 1, x] = 0;

                }
            }
            else if (y - 1 > -1 && personaje.getmV() == -1)
            {
                if (matriz[y - 1, x] == 0 || matriz[y - 1, x] == 3)
                {

                    y -= 1;
                    matriz[y, x] = 1;
                    matriz[y + 1, x] = 0;
                }
            }

            if (x + 1 < data.ancho && personaje.getmH() == 1)
            {
                if (matriz[y, x + 1] == 0 || matriz[y, x + 1] == 3)
                {
                    x += 1;
                    matriz[y, x] = 1;
                    matriz[y, x - 1] = 0;

                }
            }
            else if (x - 1 > -1 && personaje.getmH() == -1)
            {
                if (matriz[y, x - 1] == 0 || matriz[y, x - 1] == 3)
                {

                    x -= 1;
                    matriz[y, x] = 1;
                    matriz[y, x + 1] = 0;
                }
            }

            personaje.transform.DOMove(vector(vectores[y, x]), personaje.speed);
            data.disparo = !data.disparo;
            personaje.setBmove(false);
        
        }
    }

    private IEnumerator movimientoArdu()
    {

        for (int i = 0; i < movimientos.Length; i++)
        {

            if (y + 1 < data.alto && movimientos[i] == 2)
            {
                if (matriz[y + 1, x] == 0 || matriz[y + 1, x] == 3)
                {
                    y += 1;
                    matriz[y, x] = 1;
                    matriz[y - 1, x] = 0;

                }
            }
            else if (y - 1 > -1 && movimientos[i] == 3)
            {
                if (matriz[y - 1, x] == 0 || matriz[y - 1, x] == 3)
                {

                    y -= 1;
                    matriz[y, x] = 1;
                    matriz[y + 1, x] = 0;
                }
            }

            if (x + 1 < data.ancho && movimientos[i] == 4)
            {
                if (matriz[y, x + 1] == 0 || matriz[y, x + 1] == 3)
                {
                    x += 1;
                    matriz[y, x] = 1;
                    matriz[y, x - 1] = 0;

                }
            }
            else if (x - 1 > -1 && movimientos[i] == 5)
            {
                if (matriz[y, x - 1] == 0 || matriz[y, x - 1] == 3)
                {

                    x -= 1;
                    matriz[y, x] = 1;
                    matriz[y, x + 1] = 0;
                }
            }

            personaje.transform.DOMove(vector(vectores[y, x]), personaje.speed);
            data.disparo = !data.disparo;
            yield return new WaitForSeconds(.5f);
        }




    }

    private bool gameOver()
    {
        if (data.tiempo().Equals("00:00") || data.getRecolectables() <=0 || data.turnos <=0) {
            gameover.SetActive(true);

            return true;
        }
        return false;
    }



    private void comunicacion()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            serial.SendSerialMessage("A");
        }

        //---------------------------------------------------------------------
        // Receive data
        //---------------------------------------------------------------------

        string message = serial.ReadSerialMessage();

        if (message == null)
            return;


        // Check if the message is plain data or a connect/disconnect event.
        if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))
            Debug.Log("Connection established");
        else if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED))
            Debug.Log("Connection attempt failed or disconnection detected");
        else
        {
            Debug.Log("Message arrived: " + message);
            if (llegada < 5)
            {
                Debug.Log("LLegada: " + llegada);
                movimientos[llegada] = int.Parse(message);
                llegada++;
            }

            
        }

    }
    private Vector2 vector(Vector2 vectorin)
    {
        return new Vector2(vectorin.x, vectorin.y);
    }
}
