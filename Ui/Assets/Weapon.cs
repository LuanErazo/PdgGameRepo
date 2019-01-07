using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weapon : MonoBehaviour
{
    public LevelManagementData data;
    public Transform firepoint;
    public GameObject animateEffect;
    public LineRenderer line;
    public float x;
    public float y;
    private int lines;
    public int lenghtRay = 20;


    // Update is called once per frame
    void Update()
    {
            if (data.disparo) {
            StartCoroutine(shoot());
            }
        if (Input.GetButtonDown("Fire2"))
        {
        }

    }

    IEnumerator shoot()

    {


        reflectionRay(firepoint.position, firepoint.up, lenghtRay);

        yield return new WaitForSeconds(.8f);
        clean();
    }


    public void reflectionRay(Vector2 pos, Vector2 direction, int reflections)
    {

        if (reflections == 0)
        {
            return;
        }

        LineRenderer line = Instantiate(this.line, transform.position, Quaternion.identity);
        line.transform.SetParent(this.transform);

        Vector2 startingPos = pos;

        Ray rayT = new Ray(pos,direction);
        RaycastHit2D hit = Physics2D.Raycast(pos, direction,reflections);

 

        
        if (hit)
        {
                pos = hit.point;

            if (hit.transform.tag == "reflector")
            {
                StartCoroutine(reflejoReflector(pos,direction,hit,reflections));
            }

            if (hit.transform.tag == "Player")
            {
                Debug.Log("entro");
                if (data.turnos >0)
                {
                data.turnos--;

                }
                else
                {
                    data.turnos = 0;
                }
            }


        }
        else
        {
            pos += direction * lenghtRay;
        }
        lines++;
        line.SetPosition(0, startingPos);
        line.SetPosition(1, pos);
        line.enabled = true;

    }
    IEnumerator reflejoReflector(Vector2 pos, Vector2 direction, RaycastHit2D hit, int reflections) {
        direction = Vector2.Reflect(direction, hit.normal);
        //reflector = hitInfo.transform.GetComponent<Reflector>();
        reflectionRay(pos, direction, reflections - 1);
        yield return new WaitForSeconds(.5f);

    }

    private void clean()
    {

        for (int i = 0; i < lines; i++)
        {
        Destroy(GetComponentsInChildren<LineRenderer>()[i+1].gameObject);

        }
        lines = 0;
    }



}
