using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Bullect : PoolMonoBehaviour
{
    public BullctType burrctType;
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * 4, Space.Self);
    }

    [ServerCallback]
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Contains("Wall"))
        {
            DestorySelf();
        }
        if (other.name.Contains("Enemy") && burrctType == BullctType.Player)
        {
            DestorySelf();
            EventHelper.CallDestroyObj(other.gameObject);
            EnemyManager.Instance.currentEnemySum--;
        }
        if (other.name.Contains("Player") && burrctType == BullctType.Enemy)
        {
            DestorySelf();
            NetworkIdentity id = other.GetComponent<NetworkIdentity>();
            Debug.Log("OnTriggerEnter2D =======>" + id.connectionToClient);
            Debug.Log("isServer =======>" + isServer);
            if (isServer)
            {
                other.GetComponent<PlayerContorl>().OnActtacked(2);
            }
            // test(id.connectionToClient);
            TargetPlayerAttacked(id.connectionToClient, 10);
            // TargetHealed(id.netId);
        }
    }

    [TargetRpc]
    public void TargetPlayerAttacked(NetworkConnection target, int damage)
    {
        // 这将出现在对手的客户端，而不是攻击玩家的客户端
        Debug.Log("PlayerAttacked=======>" + damage);
    }

    // [TargetRpc]
    // public void TargetHealed(uint amount)
    // {
    //     // 没有 NetworkConnection 参数，所以它转到所有者
    //     Debug.Log("TargetHealed=======>" + amount);
    // }

    private void DestorySelf()
    {
        EventHelper.CallCreateMapObj(MapObjType.Explode, transform.position);
        EventHelper.CallDestroyObj(gameObject);
    }
}
