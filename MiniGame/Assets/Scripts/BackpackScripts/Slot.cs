using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item slotItem;//获取到Item
    public Image slotImage;//Item的图片
    public GameObject itemInSlot;
    //public float transparency;//透明度
    public bool available = true;
    private float time;
    void Start(){
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
        itemInSlot.GetComponent<ItemOnDrag>().enabled = available;
        //有物品则做赋值
        slotImage.sprite = item.itemImage;
        //图片显示为原大小
        itemInSlot.GetComponent<RectTransform>().sizeDelta = new Vector2(item.width * itemInSlot.GetComponent<RectTransform>().sizeDelta.x, item.height * itemInSlot.GetComponent<RectTransform>().sizeDelta.y);
        // itemInSlot.transform.SetParent(transform.parent.parent.parent);
        // itemInSlot.transform.position = transform.position;
   }
   void OnMouseDown()
   {
        Debug.Log("1233");
        if (Time.time - time <= 0.3f)
        {
            if(available){
                slotItem.use();
                BackpackManager.UseItem((int)((transform.position.x - 50) / 100),(int)((-transform.position.y - 50) / 100));
                Debug.Log((int)((transform.position.x - 50) / 100));
                Debug.Log((int)((-transform.position.y - 50) / 100));
            }
        }
        time = Time.time;
   }
}
