using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public static class EventHelper
{
    public static event Action<MapObjType, Vector3> CreateMapObj;
    public static void CallCreateMapObj(MapObjType mapObjType, Vector3 position)
    {
        CreateMapObj?.Invoke(mapObjType, position);
    }

    public static event Action<BullctType, Vector3, Vector3> CreateButtect;
    public static void CallCreateButtect(BullctType mapObjType, Vector3 position, Vector3 dir)
    {
        CreateButtect?.Invoke(mapObjType, position, dir);
    }

    public static event Action<GameObject> DestroyObj;
    public static void CallDestroyObj(GameObject obj)
    {
        DestroyObj?.Invoke(obj);
    }

    public static event Action<GameObject> PlayerCreated;
    public static void CallPlayerCreated(GameObject obj)
    {
        PlayerCreated?.Invoke(obj);
    }

    public static event Action<GameObject, float> DestroyObDelay;
    public static void CallDestroyObDelay(GameObject obj, float delay)
    {
        DestroyObDelay?.Invoke(obj, delay);
    }

    public static event Action PlayerOnAttacked;
    public static void CallPlayerOnAttacked()
    {
        PlayerOnAttacked?.Invoke();
    }

    public static event Action PlayerOnDead;
    public static void CallPlayerOnDead()
    {
        PlayerOnDead?.Invoke();
    }
}
