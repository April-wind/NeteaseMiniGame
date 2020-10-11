using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Camera backpackCamera;
    public GameObject backpackCanvas;
    void Update()
    {
        Test();
    }

    void Test()
    {
        //背包格子增加
        if (Input.GetKeyDown(KeyCode.A))
        {
            BackpackManager.instance.backpack.GridGeneration();
            BackpackManager.RefreshItem();
        }
        //放入随机物品
        if (Input.GetKeyDown(KeyCode.S))
        {
            BackpackManager.AddItem(Random.Range(1, 5));
        }
        //背包格子减少
        if (Input.GetKeyDown(KeyCode.D))
        {
            BackpackManager.RemoveItem();
        }
        //显示/隐藏背包
        if (Input.GetKeyDown(KeyCode.I))
        {
            backpackCanvas.SetActive(!backpackCanvas.activeSelf);
        }
        //背包界面缩小
        if (Input.GetKeyDown(KeyCode.Z))
        {

        }
        //背包界面放大
        if (Input.GetKeyDown(KeyCode.C))
        {

        }
    }
}
