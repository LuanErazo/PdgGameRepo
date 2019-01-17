using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;

public class Pj : MonoBehaviour
{


    public LevelManagementData data;
    public Tilemap obstacleTile;
    public GameObject gameover;

    private SerialController serial;

    public int idM;
    public float speed;


    private bool Bmove;
    private int[] movimientos = new int[5];

    [SerializeField]
    private Sprite[] sprites;


    private int mH, mV, arV, arH;
    private bool onCooldown;
    private int llegada;
    private bool send;
    private int movido;
    private Sprite mySprite;
    private bool gameOverB;

    private void Awake()
    {

    }

    // Use this for initialization
    void Start()
    {
        serial = GetComponent<SerialController>();
    }

    // Update is called once per frame
    void Update()
    {

        //We do nothing if the player is still moving.
        if (Bmove) return;

        mH = (int)Input.GetAxisRaw("Horizontal");
        mV = (int)Input.GetAxisRaw("Vertical");

        if (mH != 0)
            mV = 0;


        if (!gameOver())
        {
        //If there's a direction, we are trying to move.
        if (mH != 0 || mV != 0)
        {
                if (mV >0)
                {
                    mySprite = sprites[0];
                }
                else if(mV < 0)
                {
                    mySprite = sprites[1];

                }

                if (mH > 0)
                {
                    mySprite = sprites[3];
                }
                else if(mH <0)
                {
                    mySprite = sprites[2];

                }
                GetComponent<SpriteRenderer>().sprite = mySprite;
                Move(mH, mV);
        }

        if (Input.GetButtonDown("test"))
        {
            movimientoIndividualTIle(0, .1f);
        }

        if (send)
        {   

            StartCoroutine(movimientoArdu());
            llegada = 0;
            send = false;
                data.turnos++;
        }

        }

        comunicacion();
    }

    //A method that handle doors : Return true if you can move on the tile, false otherwise. 
    //If the door can be opened, opens it.
    //TO ADD : Levered Doors.
    private bool doorCheck(Vector2 targetCell)
    {
        Collider2D coll = whatsThere(targetCell);

        //No obstacle, we can walk there
        if (coll == null)
            return true;

        //If there's a golden door in front of the character
        if (coll.tag == "Puerta")
        {
            switche switchee = coll.transform.parent.GetComponentInChildren<switche>();

            //If the door is closed and we can open it
            if (switchee.puertaActive)
            {
                return true;
            }
            else  //If it's closed.
                return false;
        }
        else if (coll.tag == "laser")
        {
            return false;
        }
        ////if there's a levered door in front of the character.
        //else if (coll.tag == "LeveredDoor")
        //{
        //    Debug.Log("LeveredDoor detected!");
        //    LeveredDoor door = coll.gameObject.GetComponent<LeveredDoor>();
        //    //If the door is open
        //    if (door.isOpen)
        //        return true;
        //    //If the door is close.
        //    else
        //        return false;
        //}
        //else if (coll.tag == "Lever")
        //{

        //    //Click sound !
        //    if (AudioManager.getInstance() != null)
        //    {
        //        AudioManager.getInstance().Find("leverClick").source.Play();
        //        AudioManager.getInstance().Find("blocked").source.mute = true;
        //    }
        //    Lever lever = coll.gameObject.GetComponent<Lever>();
        //    lever.operate();
        //    //We operate the lever, but can't move there, so we return false;
        //    return false;
        //}
        else
            return true;
    }


    public Collider2D whatsThere(Vector2 targetPos)
    {
        RaycastHit2D hit;
        hit = Physics2D.Linecast(targetPos, targetPos);
        return hit.collider;
    }

    private IEnumerator movimientoArdu() {
        int cont = 0;
        float time = .4f;
        while (cont <5)
        {
            movimientoIndividualTIle(cont,time);
            yield return new WaitForSeconds(time);
            cont++;
        }
    }


    private void movimientoIndividualTIle(int movido, float time)
    {
        int mH = 0;
        int mV = 0;
        Vector2 startCell = transform.position;

        if (movimientos[movido] == 2) // arrriba
        {
            mV = 1;
            mySprite = sprites[0];

        }
        else if (movimientos[movido] == 3) // abajo
        {
            mV = -1;
            mySprite = sprites[1];
        }

        if (movimientos[movido] == 5) //derecha
        {
            mH = 1;
            mySprite = sprites[2];

        }
        else if (movimientos[movido] == 4) // izquierdaa
        {
            mH = -1;
            mySprite = sprites[3];

        }

        GetComponent<SpriteRenderer>().sprite = mySprite;
        int multiplicador = 2; // este numero represena que tan grande es la celda
        Vector2 targetCell = startCell + new Vector2(mH * multiplicador, mV * multiplicador);

        bool hasObstacleTile = getCell(obstacleTile, targetCell) != null; //if target Tile has an obstacle

        if (!hasObstacleTile)
        {
            if (doorCheck(targetCell))
            {
                SmoothMovementVoid(targetCell, time);
            }
            else
                StartCoroutine(BlockedMovement(targetCell));
        }
    

        data.disparo = !data.disparo;
    }


    private void SmoothMovementVoid(Vector3 end, float time)
    {

        Bmove = true;

        transform.DOMove(end, time);

        Bmove = false;

    }

    private IEnumerator SmoothMovement(Vector3 end)
    {
        //while (isMoving) yield return null;

        Bmove = true;


        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
        float inverseMoveTime = 1 / speed;

        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, end, inverseMoveTime * Time.deltaTime);
            transform.position = newPosition;
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            yield return  null;
        }


        Bmove = false;

    }

    //Blocked animation
    private IEnumerator BlockedMovement(Vector3 end)
    {
        //while (isMoving) yield return null;

        Bmove = true;


        //if (AudioManager.getInstance() != null)
        //    AudioManager.getInstance().Find("blocked").source.Play();

        Vector3 originalPos = transform.position;

        end = transform.position + ((end - transform.position) / 3);
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
        float inverseMoveTime = (1 / (speed * 2));

        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, end, inverseMoveTime * Time.deltaTime);
            transform.position = newPosition;
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            yield return null;
        }

        sqrRemainingDistance = (transform.position - originalPos).sqrMagnitude;
        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, originalPos, inverseMoveTime * Time.deltaTime);
            transform.position = newPosition;
            sqrRemainingDistance = (transform.position - originalPos).sqrMagnitude;

            yield return null;
        }

        ////The lever disable the sound so its doesn't overlap with this one, so it blocked has been muted, we restore it.
        //if (AudioManager.getInstance() != null && AudioManager.getInstance().Find("blocked").source.mute)
        //{
        //    AudioManager.getInstance().Find("blocked").source.Stop();
        //    AudioManager.getInstance().Find("blocked").source.mute = false;
        //}
        Bmove = false;
    }
    //----------------------------------------------------------------------------------------------Movimiento------------------
    private void Move(int xDir, int yDir)
    {
        Vector2 startCell = transform.position;
        int multiplicador = 2; // este numero represena que tan grande es la celda
        Vector2 targetCell = startCell + new Vector2(xDir * multiplicador, yDir * multiplicador);

        bool hasObstacleTile = getCell(obstacleTile, targetCell) != null; //if target Tile has an obstacle

        if (!hasObstacleTile)
        {
            if (doorCheck(targetCell))
            {
                StartCoroutine(SmoothMovement(targetCell));
            }
            else
                StartCoroutine(BlockedMovement(targetCell));

        }
        else
            StartCoroutine(BlockedMovement(targetCell));


        data.disparo = !data.disparo;

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(BlockedMovement(collision.transform.position));

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("recolect"))
        {
            LevelManagementData.addPuntos(collision.GetComponent<recolectable>().puntaje);
            data.recolectables--;
            Destroy(collision.gameObject);
        }
    }


    private void comunicacion()
    {

        if (Input.GetButtonDown("envio"))
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
            if (message.ToString().Length == 1)
            {
                if (llegada < 5)
                {
                    movimientos[llegada] = int.Parse(message);
                    llegada++;
                }
                if (llegada == movimientos.Length)
                    send = true;


            }


        }

    }


    private bool gameOver()
    {
        if (data.tiempo().Equals("00:00") || data.getRecolectables() <= 0)
        {
            gameover.SetActive(true);
            gameOverB = true;
        }
        else gameOverB = false;
        return gameOverB;
    }

    public void setGameOver(bool gameover) {
        this.gameOverB = gameover;
    }

    public int getmH()
    {
        return mH;
    }

    public int getmV()
    {
        return mV;
    }

    public bool getBmove()
    {
        return Bmove;
    }

    public void setBmove(bool Bmove)
    {
        this.Bmove = Bmove;
    }

    private TileBase getCell(Tilemap tilemap, Vector2 cellWorldPos)
    {
        return tilemap.GetTile(tilemap.WorldToCell(cellWorldPos));
    }



}
