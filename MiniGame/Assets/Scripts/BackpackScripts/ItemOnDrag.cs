using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ItemOnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //要实现这个,还得挂载一个组件:CanvasGroup
    public Transform originalParentParent;//原爷
    private Vector2 addFactor;//(屏幕上)初始偏移向量
    public Camera scaleUICamera;
    private Item currentItem = null;//当前物体
    private Item targetItem = null;//射线下的物体
    private Vector2Int currentPosition = new Vector2Int(0, 0);//当前坐标
    private Vector2Int targetPosition = new Vector2Int(0, 0);//目标坐标


    public void OnBeginDrag(PointerEventData eventData)
    {

        scaleUICamera = GameObject.FindWithTag("ScaleUICamera").GetComponent<Camera>();//寻找摄像机
        originalParentParent = transform.parent;//被拖拽物品原来的父物体
        currentItem = originalParentParent.GetComponent<Slot>().slotItem;//当前物体赋值
        currentPosition = originalParentParent.GetComponent<Slot>().position;
        originalParentParent = transform.parent.parent;
        transform.SetParent(transform.parent.parent.parent);//与父同级,这样就能渲染在最上层了

        addFactor = scaleUICamera.WorldToScreenPoint(transform.position) - new Vector3(eventData.position.x, eventData.position.y, 0);
        transform.position = eventData.position + addFactor;//物品和鼠标一起动

        BackpackManager.instance.backpack.ItemReduction(currentPosition.x, currentPosition.y);//删除当前位置
        BackpackManager.RefreshItem();//刷新碰撞体
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position + addFactor;//物品和鼠标一起动
        Debug.Log("1. " + transform.position);
        Debug.Log("2. " + eventData.position);
        Debug.Log("3. " + addFactor);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originalParentParent);//回归原爹
        /**
        共四种操作:
        a.丢弃->应该有个丢弃信号
        b.交换
        c.合成
        d.弹回
        **/
        //碰撞信息类
        RaycastHit hit;
        //Vector3 deleteTest = new Vector3(transform.position.x + currentItem.width * 50, transform.position.y - currentItem.height * 50, transform.position.z - 100);
        //射线起点
        Vector3 origin = new Vector3(transform.position.x + 50, transform.position.y - 50, transform.position.z - 100);
        //射线检测
        if (Physics.Raycast(origin, new Vector3(0, 0, 1), out hit, Mathf.Infinity, 1 << 9))//包内->b/c/d
        {
            targetItem = hit.collider.gameObject.GetComponent<Slot>().slotItem;
            targetPosition = hit.collider.gameObject.GetComponent<Slot>().position;
            Debug.Log("射中" + targetPosition);
        }
        else//包外->a
        {
            //应该有个丢弃信号(关于currentItem的)
            BackpackManager.RefreshItem();
            Debug.Log("丢弃");
            return;
        }

        if (currentItem.width == 1 && currentItem.height == 1)//原物体1x1->b/c/d
        {
            if (targetItem == null)//空格->b
            {
                BackpackManager.instance.backpack.PointPutIn(targetPosition.x, targetPosition.y, currentItem);//当前物体迁移过去
                BackpackManager.RefreshItem();
                Debug.Log("交换");
            }
            else if (targetItem.width == 1 && targetItem.height == 1)//目标1x1->b/c
            {
                //这里先进行合成判定->c
                if (!ItemSynthesis(currentItem.id, targetItem.id))//合成函数返回false->b
                {
                    BackpackManager.instance.backpack.ItemReduction(targetPosition.x, targetPosition.y);//删除目标位置
                    BackpackManager.instance.backpack.PointPutIn(targetPosition.x, targetPosition.y, currentItem);//当前物体迁移过去
                    BackpackManager.instance.backpack.PointPutIn(currentPosition.x, currentPosition.y, targetItem);//目标物体迁移过来
                    BackpackManager.RefreshItem();
                    Debug.Log("交换");
                }
            }
            else//目标mxn->c/d
            {
                //这里先进行合成判定->c
                if (!ItemSynthesis(currentItem.id, targetItem.id))//合成函数返回false->d
                {
                    BackpackManager.instance.backpack.PointPutIn(currentPosition.x, currentPosition.y, currentItem);//恢复原物体
                    BackpackManager.RefreshItem();
                    Debug.Log("弹回");
                }
            }
            return;
        }
        else//原物体mxn->b/c/d
        {
            if (targetItem == null)//空格->b/d
            {
                //判断能否放下->b
                if (BackpackManager.instance.backpack.PointPutIn(targetPosition.x, targetPosition.y, currentItem))
                {
                    BackpackManager.RefreshItem();
                    Debug.Log("交换");
                }
                else//d
                {
                    BackpackManager.instance.backpack.PointPutIn(currentPosition.x, currentPosition.y, currentItem);//恢复原物体
                    BackpackManager.RefreshItem();
                    Debug.Log("弹回");
                }

            }
            else//非空格->c/d
            {
                //这里先进行合成判定->c
                if (!ItemSynthesis(currentItem.id, targetItem.id))//合成函数返回false->d
                {
                    BackpackManager.instance.backpack.PointPutIn(currentPosition.x, currentPosition.y, currentItem);//恢复原物体
                    BackpackManager.RefreshItem();
                    Debug.Log("弹回");
                }
                return;
            }
        }
    }

    //TODO
    private bool ItemSynthesis(int currentItemId, int targetItemId)
    {
        return false;
    }
}
