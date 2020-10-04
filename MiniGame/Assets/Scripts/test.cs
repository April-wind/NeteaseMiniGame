using System.Collections;
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
            Debug.Log("test");
            for (int i = 0; i < BackpackManager.instance.backpack.data.GetLength(0); i++)
                for (int j = 0; j < BackpackManager.instance.backpack.data.GetLength(1); j++)
                {
                    Debug.Log(BackpackManager.instance.backpack.data[i, j]);
                }

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
