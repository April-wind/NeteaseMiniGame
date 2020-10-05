using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public enum State
{
    Idle,
    Track,
    Attack,
    AvoidWall
}
public class FixedPathAI : MonoBehaviour
{
    private GameObject[] TargetObj;
    public State state;

    private GameObject player;

    //速度
    private float idleSpeed;
    private float trackSpeed;

    //向每个目标点的运动时间
    private float moveTime;

    //计时器
    private float timer;
    //随机数
    private int randNum;

    //运动位置
    private bool rightPos;
    //这次要去的位置
    private int currentPos;
    // Start is called before the first frame update
    void Start()
    {
        TargetObj = GameObject.FindGameObjectsWithTag("Target");
        state = State.Idle;

        player = GameObject.FindWithTag("Player");

        //speed
        idleSpeed = 2.0f;
        trackSpeed = 3.0f;

        //time
        moveTime = 2.0f;

        timer = 2.0f;

        //Pos
        rightPos = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state){
            case State.Idle:
                timer += Time.deltaTime;
                if (timer > moveTime || rightPos)
                {
                    randNum = Random.Range(0, TargetObj.Length);
                    if(randNum == currentPos)
                    {
                        randNum = TargetObj.Length - randNum - 1;
                    }
                    timer = 0.0f;
                }
                
                Idleing(randNum);
                break;

            case State.Track:
                Tracking(player);
                break;
        }
    }


    /// <summary>
    /// 巡逻状态
    /// </summary>
    /// <param name="num"></param>
    private void Idleing(int num)
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, TargetObj[num].transform.position,
            Time.deltaTime * idleSpeed);
        currentPos = num;
    }

    /// <summary>
    /// 追逐状态
    /// </summary>
    /// <param name="player"></param>
    private void Tracking(GameObject player)
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position,
            Time.deltaTime * trackSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Target")
        {
            rightPos = true;
            //UnityEngine.Debug.Log(this.transform.gameObject.name);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Target")
        {
            rightPos = false;
        }
    }
}
