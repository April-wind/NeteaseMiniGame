﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//用来存储物品的所有信息
[CreateAssetMenu(fileName = "New Item", menuName = "Backpack/New Item")]
public class Item : ScriptableObject
{
    //所有物品的基类,包含了物品的id, 名字, 图片, 持有数量, 宽, 高, 描述
    public int id;//id, 从1开始, 0作为"空"的标志
    public Sprite itemImage;//图片
    //public int itemHeld;//持有数量,既然没有堆叠,那就没什么存在意义了
    public int width;//宽
    public int height;//高
    public virtual bool use(){
        Debug.Log("使用了"+id+"号物品");
        return false;
    }//使用效果
}
