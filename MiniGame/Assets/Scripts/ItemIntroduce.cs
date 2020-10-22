using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemIntroduce : MonoBehaviour
{
    //道具功能说明文字
    public Text text;

    //字显现出来的速度
    [SerializeField]
    private float textAwakeSpeed;

    //camera
    private Rect screenRect;
    private Camera camera;
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
            IsVisible();
        }
    }

    private void IsVisible()
    {
        text.color = Color.Lerp(text.color, new Color(text.color.r, text.color.g, text.color.b, 100), textAwakeSpeed * Time.deltaTime); 
    }
}
