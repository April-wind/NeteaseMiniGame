using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//需要修改, 饥饿值与数量无关(每一小段表示一只老鼠)
//测试用, 数值全都是偏高偏快的赋值
public class foodQuantity : MonoBehaviour
{
    public int number;//种群数量
    public float health = 100;//食物数量?饥饿值?健康度?
    public float timer = 2.0f;
    public Transform processTrans;//进度条

    public Transform indicatorTrans;//文字框

    void Update()
    {
        HealthAutoReduction();
    }

    public void HealthAutoReduction()
    {
        health -= (float)(0.001 * number);
        indicatorTrans.GetComponent<Text>().text = ((int)health).ToString() + "%";
        processTrans.GetComponent<Image>().fillAmount = health / 100.0f;
        if (health >= 30 && health < 50)
        {
            timer -= 2 * Time.deltaTime;
        }

        if (health >= 10 && health < 30)
        {
            timer -= 5 * Time.deltaTime;
        }

        if (health < 10)
        {
            timer -= 10 * Time.deltaTime;
        }

        if (health <= 0)
        {
            health = 0;
            //GameOver();
        }

        if (timer <= 0)
        {
            number -= (int)(0.1 * number);
            timer = 2.0f;
        }

    }
}
