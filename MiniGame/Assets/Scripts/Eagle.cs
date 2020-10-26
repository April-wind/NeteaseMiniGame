using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col2D)
    {
        //Debug.Log("猫头鹰，启动！");
        if(col2D.gameObject.tag == "Player"){
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
