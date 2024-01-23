using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Fire : Base
{
    public GameObject buttectPos;

    public void CreateBullect(BullctType bullctType)
    {
        EventHelper.CallCreateButtect(bullctType, buttectPos.transform.position, transform.up);
    }

}
