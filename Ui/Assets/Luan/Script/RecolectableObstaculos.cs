using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="enemigos y recoletables")]
public class RecolectableObstaculos : ScriptableObject {

    public Sprite[] Recolectables;
    public Sprite[] Obstaculos;

    private void OnEnable()
    {
        
    
        for (int i = 0; i < Obstaculos.Length; i++)
        {
            Obstaculos[i] = Resources.Load<Sprite>("Images/Obstacles/Obs" +"0"+(i+1));
        }
    }

}
