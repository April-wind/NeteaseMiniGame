using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Camera backpackCamera;
    public GameObject backpackObj;
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
            BackpackManager.instance.gridNum++;
        }
        // //放入随机物品
        // if (Input.GetKeyDown(KeyCode.X))
        // {
        //     BackpackManager.AddItem(5);
        // }
        // //背包格子减少
        if (Input.GetKeyDown(KeyCode.C))
        {
            BackpackManager.RemoveGrid();
            BackpackManager.instance.gridNum--;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            //BackpackManager.AddItem(6);
            BackpackManager.AddItem(Random.Range(0,5));
        }
        //显示/隐藏背包
        if (Input.GetKeyDown(KeyCode.I))
        {
            backpackObj.SetActive(!backpackObj.activeSelf);
        }
    }
}
