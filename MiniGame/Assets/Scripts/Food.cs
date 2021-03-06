﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Food", menuName = "Backpack/New Food")]
public class Food : Item
{
    //补充的饥饿值
    public int deltaHungryValue;
    public override bool use()
    {
        BackpackManager.instance.gridNum += deltaHungryValue;
        for(int i = 0; i < deltaHungryValue;i++){
            BackpackManager.instance.backpack.GridGeneration();
        }
        return true;
        //LemmingSumControl._Instance.lemmingNumTrue += deltaHungryValue;
    }
}
