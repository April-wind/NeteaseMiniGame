using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingManager : MonoBehaviour
{
    //移动速度
    [SerializeField]
    private float moveSpeed;
    //归位速度
    [SerializeField]
    private float returnSpeed;
    //移动方向
    [SerializeField]
    private Vector3 moveDir;
    //运动目标
    private Vector3 target;
    //当前方向因子
    private float currentX;
    private float currentY;

    //旅鼠之间的间隔
    [SerializeField]
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

        distance = 1.8f;
        moveSpeed = 1.0f;
        returnSpeed = 10.0f;

        PositionCheck(xPosition, yPosition);

        haveInput = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (lemmingMove.moveDir.x > 0 && lemmingMove.moveDir.y > 0)
        {
            int index = (int)Mathf.Sqrt(lemmingSumControl.LemmingNum);
            float param = Mathf.Sqrt(Mathf.Pow((0 - xPosition), 2) + Mathf.Pow((index - yPosition), 2));
            ChangeMoveDir(lemmingMove.moveDir.x, lemmingMove.moveDir.y);
            if (haveInput)
            {
                LemMove(param);
            }
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, target, moveSpeed * Time.deltaTime);
        }
        else if (lemmingMove.moveDir.x > 0 && lemmingMove.moveDir.y < 0)
        {
            int index = (int)Mathf.Sqrt(lemmingSumControl.LemmingNum);
            float param = Mathf.Sqrt(Mathf.Pow((0 - xPosition), 2) + Mathf.Pow((0 - yPosition), 2));
            ChangeMoveDir(lemmingMove.moveDir.x, lemmingMove.moveDir.y);
            if (haveInput)
            {
                LemMove(param);
            }
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, target, moveSpeed * Time.deltaTime);
        }
        else if (lemmingMove.moveDir.x < 0 && lemmingMove.moveDir.y > 0)
        {
            int index = (int)Mathf.Sqrt(lemmingSumControl.LemmingNum);
            float param = Mathf.Sqrt(Mathf.Pow((index - xPosition), 2) + Mathf.Pow((index - yPosition), 2));
            ChangeMoveDir(lemmingMove.moveDir.x, lemmingMove.moveDir.y);
            if (haveInput)
            {
                LemMove(param);
            }
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, target, moveSpeed * Time.deltaTime);
        }
        else if (lemmingMove.moveDir.x < 0 && lemmingMove.moveDir.y < 0)
        {
            int index = (int)Mathf.Sqrt(lemmingSumControl.LemmingNum);
            float param = Mathf.Sqrt(Mathf.Pow((index - xPosition), 2) + Mathf.Pow((0 - yPosition), 2));
            ChangeMoveDir(lemmingMove.moveDir.x, lemmingMove.moveDir.y);
            if (haveInput)
            {
                LemMove(param);
            }
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, target, moveSpeed * Time.deltaTime);
        }
        else if (lemmingMove.moveDir.x < 0 && lemmingMove.moveDir.y == 0)
        {
            int index = (int)Mathf.Sqrt(lemmingSumControl.LemmingNum);
            float param = Mathf.Abs(index - xPosition);
            ChangeMoveDir(lemmingMove.moveDir.x, lemmingMove.moveDir.y);
            if (haveInput)
            {
                LemMove(param);
            }
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, target, moveSpeed * Time.deltaTime);
        }
        else if (lemmingMove.moveDir.x > 0 && lemmingMove.moveDir.y == 0)
        {
            int index = (int)Mathf.Sqrt(lemmingSumControl.LemmingNum);
            float param = Mathf.Abs(0 - xPosition);
            ChangeMoveDir(lemmingMove.moveDir.x, lemmingMove.moveDir.y);
            if (haveInput)
            {
                LemMove(param);
            }
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, target, moveSpeed * Time.deltaTime);
        }
        else if (lemmingMove.moveDir.x == 0 && lemmingMove.moveDir.y > 0)
        {
            int index = (int)Mathf.Sqrt(lemmingSumControl.LemmingNum);
            float param = Mathf.Abs(index - yPosition);
            ChangeMoveDir(lemmingMove.moveDir.x, lemmingMove.moveDir.y);
            if (haveInput)
            {
                LemMove(param);
            }
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, target, moveSpeed * Time.deltaTime);
        }
        else if (lemmingMove.moveDir.x == 0 && lemmingMove.moveDir.y < 0)
        {
            int index = (int)Mathf.Sqrt(lemmingSumControl.LemmingNum);
            float param = Mathf.Abs(0 - yPosition);
            ChangeMoveDir(lemmingMove.moveDir.x, lemmingMove.moveDir.y);
            if (haveInput)
            {
                LemMove(param);
            }
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, target, moveSpeed * Time.deltaTime);
        }
        else
        {
            //haveInput = true;
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, new Vector3(distance * xPosition, -1 * distance * yPosition,
                -1), returnSpeed * Time.deltaTime);
        }
    }

    private void LemMove(float param)
    {
        target = this.transform.localPosition + new Vector3(param * lemmingMove.moveDir.x, param * lemmingMove.moveDir.y, 0);

        haveInput = false;
    }


    /// <summary>
    /// 检测是否改变移动方向
    /// </summary>
    /// <param name="param"></param>
    private void ChangeMoveDir(float x, float y)
    {
        if (currentX != x || currentY != y)
        {
            currentX = x;
            currentY = y;
            haveInput = true;
        }
    }
}
