using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class giveRecolectables : MonoBehaviour {

    public LevelManagementData data;

    private void Awake()
    {
        data.setRecolectables(transform.childCount);
        
    }

}
