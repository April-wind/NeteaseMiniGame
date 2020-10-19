using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowGrow : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D shadow;
    private int dir = 1;
    private float curTime = 0;
    public float maxA = 0.5f;
    public Vector2 maxSize,minSize;
    public float timeBeforeImpact = 3.0f;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        shadow = GetComponent<CircleCollider2D>();
        shadow.enabled = false;
    }

    void Update()
    {
        Vector2 t;
        curTime += dir * Time.deltaTime;
        if(curTime > timeBeforeImpact){
            dir = -1;
        }
        if(curTime < 0){
            Destroy(transform.parent.gameObject);
        }
        t = curTime / timeBeforeImpact * (maxSize - minSize) + minSize;
        spriteRenderer.color = new Color(0,0,0,curTime /timeBeforeImpact * maxA);
        transform.localScale = new Vector3(t.x,t.y,1);
        if(Mathf.Abs(curTime - timeBeforeImpact) < 1){
            shadow.enabled = true;
        }else{
            shadow.enabled = false;
        }
    }
}
