using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Backpack/New Inventory")]
public class Inventory : ScriptableObject
{
    //物品列表,记得首项留空
    public List<Item> itemList = new List<Item>();
}
