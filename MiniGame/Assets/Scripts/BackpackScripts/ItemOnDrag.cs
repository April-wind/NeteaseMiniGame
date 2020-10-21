using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ItemOnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //要实现这个,还得挂载一个组件:CanvasGroup
    public Transform originalParent;
    public Inventory myInventory;
    private Vector2 addFactor;//(屏幕上)初始偏移向量
    public Camera scaleUICamera;
    //private int currentItemID;//当前物品ID

    public void OnBeginDrag(PointerEventData eventData)
    {
        scaleUICamera = GameObject.FindWithTag("ScaleUICamera").GetComponent<Camera>();
        originalParent = transform.parent;//被拖拽物品原来的父物体
        //currentItemID = originalParent.GetComponent<Slot>().slotID;
        transform.SetParent(transform.parent.parent);//与父同级,这样就能渲染在最上层了

        addFactor = scaleUICamera.WorldToScreenPoint(transform.position) - new Vector3(eventData.position.x, eventData.position.y, 0);
        transform.position = eventData.position + addFactor;//物品和鼠标一起动
        GetComponent<CanvasGroup>().blocksRaycasts = false;//关掉手下这个物品遮挡鼠标射线
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position + addFactor;//物品和鼠标一起动
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // //不太靠谱 还是先取消这个功能
        // if (eventData.pointerCurrentRaycast.gameObject == null)
        // {//删除物品,更新描述为"空",更新背包
        //     myInventory.itemList[currentItemID] = null;
        //     InventoryManager.UpdateItemInfo("", Resources.Load<Sprite>("Graphics/Others/Transparent"));
        //     InventoryManager.RefreshItem();
        // }

        // if (eventData.pointerCurrentRaycast.gameObject != null)
        // {
        //     if (eventData.pointerCurrentRaycast.gameObject.name == "Item Image")//检测鼠标射线下的物品的名字
        //     {
        //         transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);//避免进入Grid在组中闪烁
        //         transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;
        //         var temp = myInventory.itemList[currentItemID];
        //         //把目标物品的ID赋值给当前的物品
        //         myInventory.itemList[currentItemID] = myInventory.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID];
        //         myInventory.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = temp;



        //         eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalParent.position;
        //         eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalParent);
        //         GetComponent<CanvasGroup>().blocksRaycasts = true;//重新开启遮挡功能
        //         return;
        //     }
        //     if (eventData.pointerCurrentRaycast.gameObject.name == "slot(Clone)")
        //     {
        //         transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
        //         transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
        //         myInventory.itemList[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotID] = myInventory.itemList[currentItemID];
        //         //解决自己放自己的问题
        //         if (eventData.pointerCurrentRaycast.gameObject.transform.gameObject.GetComponent<Slot>().slotID != currentItemID)
        //             myInventory.itemList[currentItemID] = null;


        //         GetComponent<CanvasGroup>().blocksRaycasts = true;//重新开启遮挡功能
        //         return;
        //     }
        // }

        Debug.Log(eventData.pointerCurrentRaycast.gameObject);
        //其他任何位置都归位
        transform.SetParent(originalParent);
        transform.position = originalParent.position;
        BackpackManager.RefreshItem();
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
