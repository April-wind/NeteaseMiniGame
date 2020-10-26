using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Bridge", menuName = "Backpack/New Bridge")]
public class Bridge : Item
{
    public override bool use()
    {
        PutBridge t = GameObject.Find("Lemmings").GetComponent<PutBridge>();
        if(t.canPutBridge){
            t.bridge.GetComponent<SpriteRenderer>().enabled = true;
            return true;
        }
        return false;
    }
}
