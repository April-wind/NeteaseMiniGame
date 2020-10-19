using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Item
{
    //补充的饥饿值
    public int deltaHungryValue;
    public override void use()
    {
        BackpackManager.instance.gridNum += deltaHungryValue;
        //LemmingSumControl._Instance.LemmingNumTrue += hungryValue;
    }
}
