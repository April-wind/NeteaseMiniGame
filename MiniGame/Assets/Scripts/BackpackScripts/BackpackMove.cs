using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackMove : MonoBehaviour
{
    //旅鼠祖宗物体
    public GameObject lemming;
    //旅鼠中心的屏幕坐标
    private Vector3 target;

    //对应的Camera
    public Camera scaleUICamera;
    //主摄
    public Camera mainCamera;

    //基础大小
    [SerializeField]
    private int BasicSize;

    //格子一半大小
    [SerializeField] private int gridSize = 50;

    //旅鼠数量脚本
    public LemmingSumControl lemmingSumControl;
    //缩放因子
    [SerializeField]
    private float scaleFactor = 0.79f;
    //旅鼠碰撞体
    public BoxCollider2D boxCollider2D;
    void Start()
    {
        //摄像机初始大小
        BasicSize = 540;
    }

    void Update()
    {
        CameraSizeChange();
        BackpackFollow();
    }
    private void CameraSizeChange()
    {
        //其实应该是双摄原始大小的商作为修正因子,但是主摄的BasicSize是private的
        scaleUICamera.orthographicSize = (BasicSize / 5) * mainCamera.orthographicSize * scaleFactor;
    }
    private void BackpackFollow()
    {
        //得到目标的屏幕坐标
        target = Camera.main.WorldToScreenPoint(new Vector3(lemming.transform.position.x + boxCollider2D.offset.x, lemming.transform.position.y + boxCollider2D.offset.y, 0));
        //将其屏幕坐标转化为该摄像机下的世界坐标
        target = scaleUICamera.ScreenToWorldPoint(target);
        //x,y方向的中心修正值
        int yAdd = 90+Mathf.CeilToInt(Mathf.Sqrt(lemmingSumControl.LemmingNum)) * gridSize;
        int xAdd = 510 + yAdd-90;
        if (lemmingSumControl.LemmingNum != 1 && Mathf.CeilToInt(Mathf.Sqrt(lemmingSumControl.LemmingNum - Mathf.Sqrt(lemmingSumControl.LemmingNum))) != Mathf.CeilToInt(Mathf.Sqrt(lemmingSumControl.LemmingNum)))
        {
            yAdd -= 50;
        }
        //同步移动
        this.transform.position = new Vector3(target.x - xAdd, target.y + yAdd, target.z);
    }
    // public void GridNumCorrect()
    // {
    //     if (lemmingSumControl.LemmingNum == 0)
    //         return;
    //     else if (lemmingSumControl.LemmingNum > BackpackManager.instance.gridNum)
    //     {
    //         for (int i = 0; i < lemmingSumControl.LemmingNum - BackpackManager.instance.gridNum; i++)
    //             BackpackManager.instance.backpack.GridGeneration();
    //         BackpackManager.instance.gridNum = lemmingSumControl.LemmingNum;
    //         BackpackManager.RefreshItem();
    //     }
    //     else if (lemmingSumControl.LemmingNum < BackpackManager.instance.gridNum)
    //     {
    //         for (int i = 0; i < BackpackManager.instance.gridNum - lemmingSumControl.LemmingNum; i++)
    //             BackpackManager.instance.backpack.GridReduction();
    //         BackpackManager.instance.gridNum = lemmingSumControl.LemmingNum;
    //         BackpackManager.RefreshItem();
    //     }
    // }
}
