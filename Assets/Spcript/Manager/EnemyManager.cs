using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    public int maxEnemySum;//默认10   60s刷新一次
    public int currentEnemySum = 0;
    private GameObject[] enemyBornPoints;

    override
    protected void Awake()
    {
        base.Awake();
        enemyBornPoints = GameObject.FindGameObjectsWithTag("EnemyBornPoint");
    }
    private void Start()
    {

    }

    public override void OnStartServer()
    {
        InvokeRepeating("ReceateEnemy", 60, 60);
        InitEnemy(maxEnemySum);
    }

    [Server]
    private void InitEnemy(int careteCount)
    {
        Debug.Log("isServer ========>" + isServer);
        int startIndex = Random.Range(0, enemyBornPoints.Length - careteCount);
        for (int i = startIndex; i < careteCount + startIndex; i++)
        {
            EventHelper.CallCreateMapObj(MapObjType.Enemy, enemyBornPoints[i].transform.position);
            currentEnemySum++;
        }
    }

    [Server]
    private void ReceateEnemy()
    {
        Debug.Log("currentEnemySum ==========>" + currentEnemySum);
        InitEnemy(maxEnemySum - currentEnemySum);
    }
}
