using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Camera backpackCamera;
    public GameObject backpack;
    void Update()
    {
        Test();
    }

    void Test()
    {
        //背包格子增加
        if (Input.GetKeyDown(KeyCode.Z))
        {
            BackpackManager.instance.backpack.GridGeneration();
            BackpackManager.RefreshItem();
        }
        //放入随机物品
        if (Input.GetKeyDown(KeyCode.X))
        {
            BackpackManager.AddItem(Random.Range(1, 5));
        }
        //背包格子减少
        if (Input.GetKeyDown(KeyCode.C))
        {
            BackpackManager.RemoveItem();
        }
        //显示/隐藏背包
        if (Input.GetKeyDown(KeyCode.I))
        {
            backpack.SetActive(!backpack.activeSelf);
        }
    }
}
