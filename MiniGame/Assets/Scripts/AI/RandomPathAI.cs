using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class RandomPathAI : MonoBehaviour
{

    public State state;
    public State lastState;

    private GameObject player;

    private BoxCollider2D boxCollider2D;

    public LayerMask mask;

    //速度
    private float idleSpeed;
    private float trackSpeed;

    //向每个目标点的运动时间
    private float moveTime;

    //运动方向
    [SerializeField]
    private Vector2 moveDir;
    //碰撞后的运动方向
    private Vector2 curMoveDir;

    //碰墙后计算出来的切线方向
    private Vector3 tangent;

    //计时器
    private float timer;
    //随机数
    private int randNum;
    //是否重新选取随机数
    private bool canSrand;

    //射线相关
    private Ray2D ray;
    private RaycastHit2D info;
    // Start is called before the first frame update
    void Start()
    {
        //border


        state = State.Idle;
        lastState = State.Idle;

        player = GameObject.FindWithTag("Player");

        boxCollider2D = this.transform.GetComponent<BoxCollider2D>();

        //speed
        idleSpeed = 2.0f;
        trackSpeed = 3.0f;

        //time
        moveTime = 2.0f;

        timer = 2.0f;

        canSrand = true;
        //Pos
        //rightPos = false;
    }

    // Update is called once per frame
    void Update()
    {

        RaysDetection(moveDir);
        //Debug.Log(moveDir + "moveDir");
        switch (state)
        {
            case State.Idle:
                timer += Time.deltaTime;
                if (timer > moveTime || canSrand)
                {
                    randNum = Random.Range(0, 8);
                    timer = 0;
                    canSrand = false;
                }

                Idleing(randNum);
                break;

            case State.Track:
                Tracking(player);
                break;

            case State.AvoidWall:
                //Debug.Log("avoidWall");
                AvoidWalling();
                break;
        }

    }


    private void RaysDetection(Vector2 moveDir)
    {
        
        ray = new Ray2D(new Vector2(this.transform.position.x, this.transform.position.y) + moveDir.normalized * boxCollider2D.size.x * Mathf.Sqrt(2) / 2, moveDir.normalized);
        UnityEngine.Debug.DrawRay(ray.origin, ray.direction, Color.blue);

        info = Physics2D.Raycast(ray.origin, ray.direction, 1f, mask);
        //UnityEngine.Debug.DrawRay(this.transform.position, moveDir, Color.red);
        if (info && info.collider.tag == "Border")
        {
            canSrand = true;
            state = State.AvoidWall;
            lastState = State.AvoidWall;
            curMoveDir = moveDir;           
        }
        else
        {
            if(lastState == State.AvoidWall)
            {
                state = State.Idle;
                lastState = State.Idle;
            }
        }
    }

    /// <summary>
    /// 巡逻状态
    /// </summary>
    /// <param name="num"></param>
    private void Idleing(int num)
    {
        //if(info.collider.)
        this.transform.rotation = Quaternion.Euler(0, 0, num * 45 * (-1));
        moveDir = new Vector2(Mathf.Cos((3 - num) * 45 * Mathf.Deg2Rad), Mathf.Sin((3 - num) * 45 * Mathf.Deg2Rad));
        this.transform.Translate(moveDir.normalized * Time.deltaTime * idleSpeed, Space.World);
    }

    /// <summary>
    /// 追逐状态
    /// </summary>
    /// <param name="player"></param>
    private void Tracking(GameObject player)
    {
        moveDir = (player.transform.position - this.transform.position);
        //有一定距离，则向主角移动
        if (moveDir.magnitude >= 0.5f)
        {
            //Vector3 trackDir = player.transform.position - this.transform.position;
            float angle = Vector3.SignedAngle(moveDir.normalized, Vector2.right, Vector3.back);
            //Debug.Log(angle);
            this.transform.rotation = Quaternion.Euler(0, 0, angle - 135);
            this.transform.Translate(moveDir.normalized * Time.deltaTime * trackSpeed, Space.World);
        }
        //this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position,
        //    Time.deltaTime * trackSpeed);
    }

    private void AvoidWalling()
    {
        //canSrand = true;

        //UnityEngine.Debug.Log("击中");
        Vector3 dir = new Vector3(ray.direction.x, ray.direction.y, 0);
        Vector3 wall = new Vector3((info.collider.gameObject.GetComponent<EdgeCollider2D>().points[0] - info.collider.gameObject.GetComponent<EdgeCollider2D>().points[1]).x,
            (info.collider.gameObject.GetComponent<EdgeCollider2D>().points[0] - info.collider.gameObject.GetComponent<EdgeCollider2D>().points[1]).y, 0);

        Vector3 normal = Vector3.Cross(curMoveDir, wall);
        //Debug.Log(Vector3.Dot(curMoveDir, wall));
        if (Vector3.Dot(curMoveDir, wall) <= -0.01f)
        {
            tangent = Vector3.Cross(dir, normal).normalized * 2;
        }
        if (Vector3.Dot(curMoveDir, wall) >= 0.01f)
        {
            tangent = Vector3.Cross(dir, normal).normalized * (-2);
        }
        if (Vector3.Dot(curMoveDir, wall) < 0.001f && Vector3.Dot(curMoveDir, wall) > -0.001f)
        {

            tangent = -2 * curMoveDir.normalized;
        }

        //Debug.Log(tangent + "tangent");
        UnityEngine.Debug.DrawRay(info.point, wall, Color.yellow);
        UnityEngine.Debug.DrawRay(info.point, tangent, Color.black);
        
        curMoveDir = new Vector2(tangent.x, tangent.y) + curMoveDir;
        
        UnityEngine.Debug.DrawRay(this.transform.position, curMoveDir, Color.red);
        
        //this.transform.rotation = Quaternion.Euler(0, 0, angle + 90);
        this.transform.Translate(curMoveDir.normalized * Time.deltaTime * idleSpeed, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Target")
        {
            //rightPos = true;
            //UnityEngine.Debug.Log(this.transform.gameObject.name);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Target")
        {
            //rightPos = false;
        }
    }
}
