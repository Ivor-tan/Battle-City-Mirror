using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    private GameObject[] playerStartPoint;
    void Start()
    {
        playerStartPoint = GameObject.FindGameObjectsWithTag("PlayerStartPoint");
    }
 
}
