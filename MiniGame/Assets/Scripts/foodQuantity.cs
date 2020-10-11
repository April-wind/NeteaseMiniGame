using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//需要修改, 饥饿值与数量无关(每一小段表示一只老鼠)
//改成直条, 可能食物条总量为某常数*种群数量

public class foodQuantity : MonoBehaviour
{
    public int number;//种群数量
    public float health = 100;//饥饿条
    public float healthMax;//饥饿条上限
    public float timer = 100.0f;
    public Transform processTrans;//进度条

    public Transform indicatorTrans;//文字框

    void Update()
    {
        HealthAutoReduction();
    }
    public void HealthIncrease()
    {
        healthMax += 1;
        health += 1;
    }
    public void HealthAutoReduction()
    {
        health -= Time.deltaTime * (float)System.Math.Sqrt(number);
        timer -= Time.deltaTime * (float)System.Math.Sqrt(number);

        if (timer < 9.0f)
        {
            Debug.Log(timer);
            for (int i = 0; i < (int)(100.0f - timer); i++)
                BackpackManager.RemoveItem();
            Debug.Log((int)(100.0f - timer));
            timer = 100.0f;
        }
        indicatorTrans.GetComponent<Text>().text = ((int)health).ToString() + "%";
        processTrans.GetComponent<Image>().fillAmount = health / 100.0f;
    }
}
