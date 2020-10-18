using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//需要修改, 饥饿值与数量无关(每一小段表示一只老鼠)
//改成直条, 可能食物条总量为某常数*种群数量

public class foodQuantity : MonoBehaviour
{
    public int number;//种群数量
    public float health;//饥饿条
    public float healthMax;//饥饿条上限
    public float timer = 100.0f;
    public Transform processTrans;//进度条
    public Transform indicatorTrans;//文字框
    private LemmingSumControl lemmingSumControl;//旅鼠数量脚本

    void Start()
    {
        lemmingSumControl = GameObject.FindWithTag("GameController").GetComponent<LemmingSumControl>();
    }

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
        //测试用,等到用正式的"增加""减少"函数时再修改

    }
}
