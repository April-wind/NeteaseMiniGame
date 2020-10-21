using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackpackManager : MonoBehaviour
{
    public static BackpackManager instance;

    public Backpack backpack;//存储层的背包
    public Inventory myInventory;//物品列表,按ID取Item
    public GameObject slotGrid;//一堆格子,UI意义上的"背包"
    public GameObject emptySlot;
    public List<GameObject> slots = new List<GameObject>();//格子列表
    public int gridNum;//格子个数

    //单例
    void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    //初始化
    void Start()
    {
        backpack = new Backpack(9);
        gridNum = 9;
        //LemmingSumControl._Instance.lemmingNumTrue = gridNum;
        RefreshItem();
    }

    public static void RefreshItem()
    {
        int tmp = 0;
        for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            if (instance.slotGrid.transform.childCount == 0)
                break;
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
            instance.slots.Clear();
        }
        for (int i = 0; i < instance.backpack.data.GetLength(0); i++)
        {
            for (int j = 0; j < instance.backpack.data.GetLength(1); j++)
            {
                instance.slots.Add(Instantiate(instance.emptySlot));
                if (instance.backpack.data[i, j] == -1)
                {
                    instance.slots[tmp].transform.SetParent(instance.slotGrid.transform);//设置父物体
                    instance.slots[tmp].GetComponent<Slot>().SetUpSlot(null);

                }
                else
                {
                    instance.slots[tmp].transform.SetParent(instance.slotGrid.transform);//设置父物体
                    int x = instance.backpack.storeData[i, j].x;
                    int y = instance.backpack.storeData[i, j].y;
                    if (i == x && j == y)
                    {
                        instance.slots[tmp].GetComponent<Slot>().available = true;
                        instance.slots[tmp].GetComponent<Slot>().SetUpSlot(instance.myInventory.itemList[instance.backpack.data[i, j]]);//同步图片等物品信息
                    }
                    else
                        instance.slots[tmp].GetComponent<Slot>().SetUpSlot(null);
                }
                tmp++;
            }
        }
    }

    //测试用
    public static void AddItem(int id)
    {
        instance.backpack.PutIn(instance.myInventory.itemList[id]);
        RefreshItem();
    }
    public static void RemoveItem()
    {
        instance.backpack.GridReduction();
        RefreshItem();
    }
    public static void UseItem(int x, int y)
    {
        //instance.myInventory.itemList[instance.backpack.data[x,y]].use();
        instance.backpack.ItemReduction(x, y);
        RefreshItem();
    }
}
