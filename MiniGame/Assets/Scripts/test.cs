﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{


    void Update()
    {
        Test();
    }

    void Test()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            BackpackManager.instance.backpack.GridGeneration();
            BackpackManager.RefreshItem();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            BackpackManager.AddItem(Random.Range(1, 5));
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            BackpackManager.RemoveItem();
        }
    }
}
