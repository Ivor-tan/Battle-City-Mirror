using UnityEngine;
using EasyPoolKit;
using Mirror;
using System;

public class MapObjManager : Singleton<MapObjManager>
{
    [Header("暴炸效果")]
    public GameObject explodeObj;
    [Header("子弹")]
    public GameObject bullectObj;
    [Header("敌人")]
    public GameObject enemyPerfab;

    private NetworkIdentity identity;

    override
    protected void Awake()
    {
        identity = GetComponent<NetworkIdentity>();
    }
    private void OnEnable()
    {
        EventHelper.CreateMapObj += OnCreateMapObj;
        EventHelper.CreateButtect += OnCreateBullect;
        EventHelper.DestroyObj += OnDestroyObj;
        EventHelper.DestroyObDelay += OnDestroyObjDelay;
    }

    private void OnDisable()
    {
        EventHelper.CreateMapObj -= OnCreateMapObj;
        EventHelper.CreateButtect -= OnCreateBullect;
        EventHelper.DestroyObj -= OnDestroyObj;
        EventHelper.DestroyObDelay -= OnDestroyObjDelay;
    }

    [Server]
    private void OnDestroyObj(GameObject obj)
    {
        //两个方法好像都行
        Destroy(obj);
        // NetworkServer.Destroy(obj);
    }

    [Server]
    private void OnDestroyObjDelay(GameObject obj, float delay)
    {
        Destroy(obj, delay);
        // NetworkServer.Destroy(obj);
    }

    [Server]
    private void OnCreateMapObj(MapObjType type, Vector3 position)
    {
        // Debug.Log("OnCreateMapObj  MapObjManager     isServer=====>" + isServer);
        GameObject newObj = null;
        switch (type)
        {
            case MapObjType.Explode:
                // newObj = SimpleGOPoolKit.Instance.SimpleSpawn(explodeObj);
                newObj = Instantiate(explodeObj);
                break;
            case MapObjType.Enemy:
                // newObj = SimpleGOPoolKit.Instance.SimpleSpawn(explodeObj);
                newObj = Instantiate(enemyPerfab);
                break;

        }
        if (newObj != null)
        {
            newObj.transform.position = position;
        }
        NetworkServer.Spawn(newObj);
        
    }

    [Server]
    private void OnCreateBullect(BullctType type, Vector3 position, Vector3 dir)
    {
        // GameObject newObj = SimpleGOPoolKit.Instance.SimpleSpawn(bullectObj);
        GameObject newObj = Instantiate(bullectObj);
        newObj.transform.position = position;
        newObj.transform.up = dir;
        Bullect bullect = newObj.GetComponent<Bullect>();
        bullect.burrctType = type;
        NetworkServer.Spawn(newObj);
        // Debug.Log("OnCreateBullect ============>");
    }
}
