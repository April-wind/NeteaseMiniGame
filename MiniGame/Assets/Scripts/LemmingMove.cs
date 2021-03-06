﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingMove : MonoBehaviour
{
    private static LemmingMove _instance;

    public static LemmingMove _Instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    [SerializeField]
    public Vector3 moveDir;

    [SerializeField]
    private float moveSpeed;

    //运动目标
    private Vector3 target;

    //子物体个数
    private int childNum;

    //Camera
    private Camera camera;
    //
    [SerializeField]
    private float cameraChangeSpeed;
    //基础大小
    [SerializeField]
    private int BasicSize;
    //增加因子
    [SerializeField]
    private float addFactor;
    //缩减因子
    //private float reduceFactor;

    //旅鼠之间间隔
    private float distance;

    private LemmingSumControl lemmingSumControl;
    private BoxCollider2D boxCollider2D;
    //碰撞盒收缩速度
    private float boxChangeSpeed;

    //blood,用于计算血量的削减
    public float bloodChange;

    //场景材质球
    public Material material;
    //材质球属性 阴影
    [SerializeField]
    private float shadowStr;
    //渐变速度
    [SerializeField]
    private float changeSpeed;

    //是否进入左上角
    [SerializeField]
    private bool InDoor;
    //左上角的云
    private GameObject cloud;
    // Start is called before the first frame update
    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        moveSpeed = 2.0f;

        childNum = 0;

        camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        lemmingSumControl = GameObject.FindWithTag("GameController").GetComponent<LemmingSumControl>();
        boxCollider2D = this.GetComponent<BoxCollider2D>();

        cameraChangeSpeed = 1.0f;
        BasicSize = 5;
        addFactor = 1.0f;
        //reduceFactor = 0.8f;
        distance = 1.8f;

        boxChangeSpeed = 10.0f;

        //云
        cloud = GameObject.FindWithTag("Cloud");
        //this.GetComponent<BoxCollider>().size = new Vector3(2, 2, 2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        moveDir = new Vector3(h, v, 0);
        target = transform.position + moveSpeed * moveDir;
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * moveSpeed);

        //更新子物体个数
        //childNum = transform.childCount;

        //进入左上角场景，场景颜色变暗
        SceneChange();
    }

    private void SceneChange()
    {
        if (InDoor)
        {
            Debug.Log("aaa");
            shadowStr = Mathf.Lerp(shadowStr, 1.0f, Time.deltaTime * changeSpeed);material.SetFloat("_ShadowStrength", shadowStr);
        }
        else
        {
            shadowStr = Mathf.Lerp(shadowStr, 0.0f, Time.deltaTime * changeSpeed); material.SetFloat("_ShadowStrength", shadowStr);
        }
        
    }

    private void LateUpdate()
    {
        childNum = transform.childCount;
        //Debug.Log(childNum);
        //摄像机size更改
        if (lemmingSumControl.situation == 1 && childNum > 0)
        {
            //相机Size
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, BasicSize + addFactor * Mathf.Sqrt(childNum), cameraChangeSpeed * Time.deltaTime);

            //碰撞盒Size/Offset
            if (moveDir.x * moveDir.y <= 0)
            {
                int index1 = 0;
                int index2 = childNum - 1;
                //Debug.Log(childNum);
                boxCollider2D.offset = new Vector2((transform.GetChild(index1).localPosition.x + transform.GetChild(index2).localPosition.x) / 2,
                    (transform.GetChild(index1).localPosition.y + transform.GetChild(index2).localPosition.y) / 2);
                boxCollider2D.size = Vector2.Lerp(boxCollider2D.size,
                    new Vector2((transform.GetChild(index2).localPosition.x - transform.GetChild(index1).localPosition.x) + distance, (transform.GetChild(index1).localPosition.y - transform.GetChild(index2).localPosition.y) + distance),
                    Time.deltaTime * boxChangeSpeed
                    );
            }
            if (moveDir.x * moveDir.y >= 0)
            {
                int index1 = (int)Mathf.Pow((int)Mathf.Sqrt(childNum) - 1, 2);
                int index2 = childNum - 1 - ((int)Mathf.Sqrt(childNum) - 1);
                boxCollider2D.offset = new Vector2((transform.GetChild(index1).localPosition.x + transform.GetChild(index2).localPosition.x) / 2,
                    (transform.GetChild(index1).localPosition.y + transform.GetChild(index2).localPosition.y) / 2);
                boxCollider2D.size = Vector2.Lerp(boxCollider2D.size,
                    new Vector2((transform.GetChild(index1).localPosition.x - transform.GetChild(index2).localPosition.x) + distance, (transform.GetChild(index1).localPosition.y - transform.GetChild(index2).localPosition.y) + distance),
                    Time.deltaTime * boxChangeSpeed
                    );
            }
        }
        else if (lemmingSumControl.situation == 2)
        {
            //Todo
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, BasicSize + addFactor * Mathf.Sqrt(Mathf.Pow((int)Mathf.Sqrt(childNum), 2) + (int)Mathf.Sqrt(childNum)), cameraChangeSpeed * Time.deltaTime);

            if (moveDir.x * moveDir.y <= 0)
            {
                int index1 = 0;
                int index2 = childNum - 1;
                //Debug.Log(index1 + " " + index2);
                int index3 = (int)Mathf.Pow((int)Mathf.Sqrt(childNum), 2) - 1;

                //碰撞盒Size/Offset
                boxCollider2D.offset = new Vector2((transform.GetChild(index1).localPosition.x + transform.GetChild(index2).localPosition.x) / 2,
                    (transform.GetChild(index1).localPosition.y + transform.GetChild(index3).localPosition.y) / 2);
                boxCollider2D.size = Vector2.Lerp(boxCollider2D.size,
                    new Vector2((transform.GetChild(index2).localPosition.x - transform.GetChild(index1).localPosition.x) + distance, (transform.GetChild(index1).localPosition.y - transform.GetChild(index3).localPosition.y) + distance),
                    Time.deltaTime * boxChangeSpeed
                    );
            }

            if (moveDir.x * moveDir.y >= 0)
            {
                int index1 = (int)Mathf.Pow((int)Mathf.Sqrt(childNum), 2);
                int index2 = (int)Mathf.Pow((int)Mathf.Sqrt(childNum), 2) - (int)Mathf.Sqrt(childNum);


                //碰撞盒Size/Offset
                boxCollider2D.offset = new Vector2((transform.GetChild(index1).localPosition.x + transform.GetChild(index2).localPosition.x) / 2,
                    (transform.GetChild(index1).localPosition.y + transform.GetChild(index2).localPosition.y) / 2);
                boxCollider2D.size = Vector2.Lerp(boxCollider2D.size,
                    new Vector2((transform.GetChild(index1).localPosition.x - transform.GetChild(index2).localPosition.x) + distance, (transform.GetChild(index1).localPosition.y - transform.GetChild(index2).localPosition.y) + distance),
                    Time.deltaTime * boxChangeSpeed
                    );
            }
        }
        else if (lemmingSumControl.situation == 3)
        {
            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, BasicSize + addFactor * ((int)Mathf.Sqrt(childNum) + 1), cameraChangeSpeed
                * Time.deltaTime);

            if (moveDir.x * moveDir.y <= 0)
            {
                int index1 = 0;
                int index2 = (int)Mathf.Pow((int)Mathf.Sqrt(childNum), 2) - 1 + (int)Mathf.Sqrt(childNum);
                int index3 = childNum - 1;
                //Debug.Log(index1 + " " + index2);
                //碰撞盒Size/Offset
                boxCollider2D.offset = new Vector2((transform.GetChild(index1).localPosition.x + transform.GetChild(index2).localPosition.x) / 2,
                    (transform.GetChild(index1).localPosition.y + transform.GetChild(index3).localPosition.y) / 2);
                boxCollider2D.size = Vector2.Lerp(boxCollider2D.size,
                    new Vector2((transform.GetChild(index2).localPosition.x - transform.GetChild(index1).localPosition.x) + distance, (transform.GetChild(index1).localPosition.y - transform.GetChild(index3).localPosition.y) + distance),
                    Time.deltaTime * boxChangeSpeed
                    );
            }

            if (moveDir.x * moveDir.y >= 0)
            {
                int index1 = (int)Mathf.Pow((int)Mathf.Sqrt(childNum), 2);
                int index2 = (int)Mathf.Pow((int)Mathf.Sqrt(childNum), 2) - 1 + (int)Mathf.Sqrt(childNum) + 1;

                //碰撞盒Size/Offset
                boxCollider2D.offset = new Vector2((transform.GetChild(index1).localPosition.x + transform.GetChild(index2).localPosition.x) / 2,
                    (transform.GetChild(index1).localPosition.y + transform.GetChild(index2).localPosition.y) / 2);
                boxCollider2D.size = Vector2.Lerp(boxCollider2D.size,
                    new Vector2((transform.GetChild(index1).localPosition.x - transform.GetChild(index2).localPosition.x) + distance, (transform.GetChild(index1).localPosition.y - transform.GetChild(index2).localPosition.y) + distance),
                    Time.deltaTime * boxChangeSpeed
                    );
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            bloodChange = 40;
            BackpackManager.RemoveGrid();
            BackpackManager.instance.gridNum--;
            Debug.Log(collision.gameObject.name);
            Destroy(collision.gameObject,0.5f);
        }
        if (collision.tag == "SnakeCost")
        {
            bloodChange = 20;
            BackpackManager.RemoveGrid();
            BackpackManager.instance.gridNum--;
            Debug.Log(collision.gameObject.name);
        }
        //猫头鹰扣血
        if(collision.tag == "Eagle")
        {
            bloodChange = 30;
            BackpackManager.RemoveGrid();
            BackpackManager.instance.gridNum--;
            Debug.Log(collision.gameObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Door")
        {
            if(moveDir.y > 0)
            {
                InDoor = true;
                cloud.SetActive(false);
            }
            else
            {
                InDoor = false;
                cloud.SetActive(true);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}
