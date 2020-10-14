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
    public LemmingSumControl lemmingSumControl;
    void Start()
    {
        BasicSize = 450;
    }

    void Update()
    {
        CameraSizeChange();
        BackpackFollow();
        GridNumCorrect();
    }
    private void CameraSizeChange()
    {
        scaleUICamera.orthographicSize = (BasicSize / 5) * mainCamera.orthographicSize;
    }
    private void BackpackFollow()
    {
        target = Camera.main.WorldToScreenPoint(lemming.transform.position);
        target = scaleUICamera.ScreenToWorldPoint(target);
        this.transform.position = target;
    }
    public void GridNumCorrect()
    {
        if (lemmingSumControl.LemmingNum == 0)
            return;
        else if (lemmingSumControl.LemmingNum > BackpackManager.instance.gridNum)
        {
            for (int i = 0; i < lemmingSumControl.LemmingNum - BackpackManager.instance.gridNum; i++)
                BackpackManager.instance.backpack.GridGeneration();
            BackpackManager.instance.gridNum = lemmingSumControl.LemmingNum;
            BackpackManager.RefreshItem();
            Debug.Log(lemmingSumControl.LemmingNum);
            Debug.Log(BackpackManager.instance.gridNum);
        }
        else if (lemmingSumControl.LemmingNum < BackpackManager.instance.gridNum)
        {
            for (int i = 0; i < BackpackManager.instance.gridNum - lemmingSumControl.LemmingNum; i++)
                BackpackManager.instance.backpack.GridReduction();
            BackpackManager.instance.gridNum = lemmingSumControl.LemmingNum;
            BackpackManager.RefreshItem();
            Debug.Log(lemmingSumControl.LemmingNum);
            Debug.Log(BackpackManager.instance.gridNum);
        }
    }

}
