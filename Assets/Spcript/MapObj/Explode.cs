using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : PoolMonoBehaviour
{
    public float explodeTime = 1.5f;
    private void Start()
    {
        if (isServer)
        {
            EventHelper.CallDestroyObDelay(gameObject, explodeTime);
        }
    }

}
