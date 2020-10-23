using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item slotItem;//获取到Item
    public Image slotImage;//Item的图片
    public GameObject itemInSlot;

    public bool available = false;
    private float time;
    void Start()
    {
        time = Time.time;
    }

    public void SetUpSlot(Item item)
    {
        //无物体设为空格, 不显示itemInSlot
        if (item == null)
        {
            itemInSlot.SetActive(false);
            return;
        }
        itemInSlot.GetComponent<CanvasGroup>().blocksRaycasts = available;//关掉遮挡鼠标射线
        //有物品则做赋值
        slotItem = item;
        slotImage.sprite = item.itemImage;
        //图片显示为原大小
        itemInSlot.GetComponent<RectTransform>().sizeDelta = new Vector2(item.width * itemInSlot.GetComponent<RectTransform>().sizeDelta.x, item.height * itemInSlot.GetComponent<RectTransform>().sizeDelta.y);
        //碰撞器与图片全重叠
        this.GetComponent<BoxCollider>().size = new Vector3(item.width * 100, item.height * 100, 1);
        this.GetComponent<BoxCollider>().center = new Vector3((item.width - 1) * 50, -(item.height - 1) * 50, 0);
    }


}
