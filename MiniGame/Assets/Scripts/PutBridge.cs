using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutBridge : MonoBehaviour
{
    public bool canPutBridge;
    public GameObject bridge;
    void Start()
    {
        canPutBridge = false;
    }

    void OnCollisionEnter2D(Collision2D col2D)
    {
        if(col2D.gameObject.tag == "Bridge"){
            canPutBridge = true;
            bridge = col2D.gameObject;
        }
    }
    void OnCollisionExit2D(Collision2D col2D)
    {
        if(col2D.gameObject.tag == "Bridge"){
            canPutBridge = false;
        }
    }
}
