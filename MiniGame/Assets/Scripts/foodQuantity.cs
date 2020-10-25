using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//需要修改, 饥饿值与数量无关(每一小段表示一只老鼠)
//改成直条, 可能食物条总量为某常数*种群数量

public class foodQuantity : MonoBehaviour
{
    public float health;//饥饿条
    public float counter = 0;//计数器
    public Transform processTrans;//进度条

    void Start()
    {
        health = BackpackManager.instance.gridNum;
    }

    void Update()
    {
        if (BackpackManager.instance.gridNum > 0)
        {
            HealthControl();
        }
    }

    public void HealthControl()
    {
        float subtractionFactor = Mathf.CeilToInt(Mathf.Sqrt(BackpackManager.instance.gridNum)) * 0.0005f;//目前因子:旅鼠数开方向上取整
        health = BackpackManager.instance.gridNum;
        LemmingSumControl._Instance.lemmingNumTrue = BackpackManager.instance.gridNum;
        counter += subtractionFactor;
        processTrans.GetComponent<Image>().fillAmount = Mathf.Sqrt(health - counter) / 10.0f;
        if (counter > 1)
        {
            for (int i = 0; (i < (int)counter) && (BackpackManager.instance.gridNum > 0); i++)
            {
                //Debug.Log(123);
                int id = BackpackManager.instance.backpack.GridReduction();
                if(id!=0)
                LemmingSumControl._Instance.CreateItem(BackpackManager.instance.myInventory.itemList[id]);
                BackpackManager.instance.gridNum--;
            }
            BackpackManager.RefreshItem();
            counter = 0;
        }

    }
}
