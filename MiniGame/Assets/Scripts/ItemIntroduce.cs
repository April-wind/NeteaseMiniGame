using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemIntroduce : MonoBehaviour
{
    //道具功能说明文字
    public Text text;
    public string introduce;

    //字显现出来的速度
    [SerializeField]
    private float textAwakeSpeed;

    //间隔时间
    [SerializeField]
    private float textIntervalTime;

    //camera
    private Rect screenRect;
    private Camera camera;

    //记录上一帧的物体
    private string name;
    private string lastName;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        screenRect = new Rect(0, 0, Screen.width, Screen.height);

    }

    // Update is called once per frame
    void Update()
    {
        //道具在屏幕范围内 出现提示文字
        if (screenRect.Contains(camera.WorldToScreenPoint(this.transform.position))){
            name = this.transform.name;
            IsVisible();
        }
    }

    private void IsVisible()
    {
        text.text = introduce;

        //队列创建
        if (lastName != name)
        {
            lastName = name;
            Sequence quence = DOTween.Sequence();
            
            quence.Append(text.DOColor(new Color(text.color.r, text.color.g, text.color.b, 255), textAwakeSpeed));
            quence.AppendInterval(textIntervalTime);
            quence.Append(text.DOColor(new Color(text.color.r, text.color.g, text.color.b, 0), textAwakeSpeed));
        }
        
    }
    
}
