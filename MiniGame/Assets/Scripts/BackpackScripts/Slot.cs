﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item slotItem;//获取到Item
    public Image slotImage;//Item的图片
    public GameObject itemInSlot;
    //public float transparency;//透明度
    public bool available = true;

    public void SetUpSlot(Item item)
    {
        //无物体设为空格, 不显示itemInSlot
        if (item == null)
        {
            itemInSlot.SetActive(false);
            return;
        }
        itemInSlot.GetComponent<ItemOnDrag>().enabled = available;
        //有物品则做赋值
        slotImage.sprite = item.itemImage;
        //图片显示为原大小
        itemInSlot.GetComponent<RectTransform>().sizeDelta = new Vector2(item.width * 100, item.height * 100);
        // itemInSlot.transform.SetParent(transform.parent.parent.parent);
        // itemInSlot.transform.position = transform.position;
    }
}
