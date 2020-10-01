using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

//测试脚本
public class autoDrop : MonoBehaviour
{
    public Transform startTrans;
    public Transform midTrans;
    public Transform endTrans;
    public Transform thing;
    //public Transform[] PointList;//生成路径的保存的目标点

    public float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1)
            timer = 0;
        Drop();
    }

    public void Drop()
    {

        thing.position = Bezier(startTrans.position, midTrans.position, endTrans.position, (float)Mathf.Sqrt(timer));
        // for (int i = 0; i < PointList.Length; i++)
        // {
        //     PointList[i].position = Bezier(startTrans.position, midTrans.position, endTrans.position, (float)((i + 1) / 10.0));
        // }
        // gameObject.transform.position = PointList[0].position;//物品位置更新为第一个目标点(保证相对玩家的轨迹正常)
        // var positions = PointList.Select(u => u.position).ToArray();
        // transform.DOPath(positions, 10, PathType.CatmullRom, 0, 10);//(目标点, 运动总耗时, 运动方式:曲线, Lookat, 轨迹精度)
    }

    //绘制贝塞尔曲线作为路径
    Vector3 Bezier(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        Vector3 p0p1 = (1 - t) * p0 + t * p1;
        Vector3 p1p2 = (1 - t) * p1 + t * p2;
        Vector3 result = (1 - t) * p0p1 + t * p1p2;
        return result;
    }

}
