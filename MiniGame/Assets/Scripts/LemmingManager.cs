using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class LemmingManager : MonoBehaviour
{
    [SerializeField]
    [Header("移动速度")]
    public float moveSpeed;
    [SerializeField]
    [Header("归位速度")]
    public float returnSpeed;
    //private float returnSpeedSlow;
    [SerializeField]
    [Header("移动方向")]
    private Vector3 moveDir;
    //运动目标
    private Vector3 target;
    //当前方向因子
    private float currentX;
    private float currentY;
    private float param;
    [SerializeField]
    [Header("距离因子基数")]
    private float paramAdd;
    [SerializeField]
    [Header("旅鼠相对移动的距离")]
    public float targetDistance;
    [SerializeField]
    [Header("旅鼠速度因子")]
    public float speedFactor;
    [SerializeField]
    [Header("旅鼠的动画速度基数")]
    private float animSpeedBasic;
    [SerializeField]
    [Header("旅鼠的动画速度衰减因子")]
    private float animSpeedFactor;

    [SerializeField]
    [Header("旅鼠之间的间隔")]
    private float distance;
    //旅鼠所处相对位置
    
    private float xPosition;
    private float yPosition;

    //是否有输入
    private bool haveInput;

    //旅鼠群
    private GameObject player;
    private LemmingMove lemmingMove;
    private LemmingSumControl lemmingSumControl;
    private SpriteRenderer spriteRenderer;

    public void GetPosition(float xPos, float yPos)
    {
        this.xPosition = xPos;
        this.yPosition = yPos;
    }

    public void PositionCheck(float xPos, float yPos)
    {
        this.transform.localPosition = new Vector3(this.transform.position.x + distance * xPos,
            this.transform.position.y - distance * yPos, this.transform.localPosition.z);
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        lemmingMove = player.GetComponent<LemmingMove>();
        lemmingSumControl = GameObject.FindWithTag("GameController").GetComponent<LemmingSumControl>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();

        //dir
        //targetDistance = 5.0f;
        //speedFactor = 2.0f;

        //distance = 1.8f;
        //moveSpeed = 1.0f;
        //returnSpeed = 10.0f;
        //returnSpeedSlow = 2.0f;

        PositionCheck(xPosition, yPosition);

        haveInput = false;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeMoveDir(lemmingMove.moveDir.x, lemmingMove.moveDir.y, param);
        if (lemmingMove.moveDir.x > 0 && lemmingMove.moveDir.y > 0)
        {
            int index = (int)Mathf.Sqrt(lemmingSumControl.LemmingNum);
            if (index * index == lemmingSumControl.LemmingNum)
                index -= 1;
            param = Mathf.Sqrt(Mathf.Pow((0 - xPosition), 2) + Mathf.Pow((index - yPosition), 2));
            //ChangeMoveDir(lemmingMove.moveDir.x, lemmingMove.moveDir.y);
            if (haveInput)
            {
                LemMove(param);
            }
             
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, target, moveSpeed * (param + paramAdd) * Time.deltaTime);

            //旋转
            spriteRenderer.flipX = true;
            spriteRenderer.flipY = false;
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
            this.GetComponent<Animator>().speed = animSpeedBasic * (param * animSpeedFactor + 1);
        }
        else if (lemmingMove.moveDir.x > 0 && lemmingMove.moveDir.y < 0)
        {
            int index = (int)Mathf.Sqrt(lemmingSumControl.LemmingNum);
            param = Mathf.Sqrt(Mathf.Pow((0 - xPosition), 2) + Mathf.Pow((0 - yPosition), 2));
            //ChangeMoveDir(lemmingMove.moveDir.x, lemmingMove.moveDir.y);
            if (haveInput)
            {
                LemMove(param);
            }
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, target, moveSpeed * speedFactor *(param + paramAdd) * Time.deltaTime);

            //旋转
            spriteRenderer.flipX = true;
            spriteRenderer.flipY = false;
            this.transform.rotation = Quaternion.Euler(0, 0, -90);
            this.GetComponent<Animator>().speed = animSpeedBasic * (param * animSpeedFactor + 1);
        }
        else if (lemmingMove.moveDir.x < 0 && lemmingMove.moveDir.y > 0)
        {
            int index = (int)Mathf.Sqrt(lemmingSumControl.LemmingNum);
            if (index * index == lemmingSumControl.LemmingNum)
                index -= 1;
            param = Mathf.Sqrt(Mathf.Pow((index - xPosition), 2) + Mathf.Pow((index - yPosition), 2));
            //ChangeMoveDir(lemmingMove.moveDir.x, lemmingMove.moveDir.y);
            if (haveInput)
            {
                LemMove(param);
            }
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, target, moveSpeed * speedFactor * (param + paramAdd) * Time.deltaTime);

            //旋转
            spriteRenderer.flipX = false;
            spriteRenderer.flipY = false;
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
            this.GetComponent<Animator>().speed = animSpeedBasic * (param * animSpeedFactor + 1);
        }
        else if (lemmingMove.moveDir.x < 0 && lemmingMove.moveDir.y < 0)
        {
            int index = (int)Mathf.Sqrt(lemmingSumControl.LemmingNum);
            if (index * index == lemmingSumControl.LemmingNum)
                index -= 1;
            param = Mathf.Sqrt(Mathf.Pow((index - xPosition), 2) + Mathf.Pow((0 - yPosition), 2));
            //ChangeMoveDir(lemmingMove.moveDir.x, lemmingMove.moveDir.y);
            if (haveInput)
            {
                LemMove(param);
            }
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, target, moveSpeed * speedFactor * (param + paramAdd) * Time.deltaTime);

            //旋转
            spriteRenderer.flipX = false;
            spriteRenderer.flipY = false;
            this.transform.rotation = Quaternion.Euler(0, 0, 90);
            this.GetComponent<Animator>().speed = animSpeedBasic * (param * animSpeedFactor + 1);
        }
        else if (lemmingMove.moveDir.x < 0 && lemmingMove.moveDir.y == 0)
        {
            int index = (int)Mathf.Sqrt(lemmingSumControl.LemmingNum);
            if (index * index == lemmingSumControl.LemmingNum)
                index -= 1;
            param = Mathf.Abs(index - xPosition);
            
            if (haveInput)
            {
                LemMove(param);
            }
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, target, moveSpeed * speedFactor * (param + paramAdd) * Time.deltaTime);

            //旋转
            spriteRenderer.flipX = false;
            spriteRenderer.flipY = false;
            this.transform.rotation = Quaternion.Euler(0, 0, 45);
            this.GetComponent<Animator>().speed = animSpeedBasic * (param * animSpeedFactor + 1);
        }
        else if (lemmingMove.moveDir.x > 0 && lemmingMove.moveDir.y == 0)
        {
            int index = (int)Mathf.Sqrt(lemmingSumControl.LemmingNum);
            param = Mathf.Abs(0 - xPosition);
            //ChangeMoveDir(lemmingMove.moveDir.x, lemmingMove.moveDir.y);
            if (haveInput)
            {
                LemMove(param);
            }
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, target, moveSpeed * speedFactor * (param + paramAdd) * Time.deltaTime);

            //旋转
            spriteRenderer.flipX = true;
            spriteRenderer.flipY = false;
            this.transform.rotation = Quaternion.Euler(0, 0, -45);
            this.GetComponent<Animator>().speed = animSpeedBasic * (param * animSpeedFactor + 1);
        }
        else if (lemmingMove.moveDir.x == 0 && lemmingMove.moveDir.y > 0)
        {
            int index = (int)Mathf.Sqrt(lemmingSumControl.LemmingNum);
            if (index * index == lemmingSumControl.LemmingNum)
                index -= 1;
            param = Mathf.Abs(index - yPosition);
            
            //ChangeMoveDir(lemmingMove.moveDir.x, lemmingMove.moveDir.y);
            if (haveInput)
            {
                LemMove(param);
            }
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, target, moveSpeed * speedFactor * (param + paramAdd) * Time.deltaTime);

            //旋转
            spriteRenderer.flipX = false;
            spriteRenderer.flipY = false;
            this.transform.rotation = Quaternion.Euler(0, 0, -45);
            this.GetComponent<Animator>().speed = animSpeedBasic * (param * animSpeedFactor + 1);
        }
        else if (lemmingMove.moveDir.x == 0 && lemmingMove.moveDir.y < 0)
        {
            int index = (int)Mathf.Sqrt(lemmingSumControl.LemmingNum);
            param = Mathf.Abs(0 - yPosition);
            //ChangeMoveDir(lemmingMove.moveDir.x, lemmingMove.moveDir.y);
            if (haveInput)
            {
                LemMove(param);
            }
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, target, moveSpeed * speedFactor * (param + paramAdd) * Time.deltaTime);

            //旋转
            spriteRenderer.flipX = false;
            spriteRenderer.flipY = true;
            this.transform.rotation = Quaternion.Euler(0, 0, 45);
            this.GetComponent<Animator>().speed = animSpeedBasic * (param * animSpeedFactor + 1);
        }
        else
        {
            this.GetComponent<Animator>().speed = Mathf.Lerp(this.GetComponent<Animator>().speed, 0, returnSpeed * Time.deltaTime);
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, new Vector3(distance * xPosition, -1 * distance * yPosition,
                -1), returnSpeed * Time.deltaTime);
            
        }
    }

    private void LemMove(float param)
    {
        target = new Vector3(distance * xPosition, -distance * yPosition, 0) - new Vector3(lemmingMove.moveDir.x * targetDistance, lemmingMove.moveDir.y * targetDistance, 0) / (param + paramAdd);
        haveInput = false;
    }


    /// <summary>
    /// 检测是否改变移动方向
    /// </summary>
    /// <param name="param"></param>
    private void ChangeMoveDir(float x, float y, float param)
    {
        if (currentX != x || currentY != y)
        {
            target = new Vector3(xPosition * distance, -yPosition * distance, 0) - new Vector3(lemmingMove.moveDir.x,lemmingMove.moveDir.y, 0) / (param + paramAdd);

            currentX = x;
            currentY = y;
            haveInput = true;
        }
    }
}
