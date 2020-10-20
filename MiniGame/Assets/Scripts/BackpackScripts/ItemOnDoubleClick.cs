using UnityEngine;
using UnityEngine.EventSystems;
 
public class ItemOnDoubleClick : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.clickCount == 2){
            //Debug.Log("y=" + ((int)((transform.localPosition.x + 400) / 100)).ToString() + "x=" + ((int)((-transform.localPosition.y + 400) / 100)).ToString());
            //这坐标很神奇
            BackpackManager.UseItem((int)((-transform.localPosition.y + 400) / 100),(int)((transform.localPosition.x + 400) / 100));
        }
    }
}