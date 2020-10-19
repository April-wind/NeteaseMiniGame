using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EagleSpriteMove : MonoBehaviour
{
    Tweener tweener;
    public float timeBeforeImpact = 3.0f;
    void Start()
    {
        float x,y;
        Tweener tweener;
        x = transform.position.x;
        y = transform.position.y;
        tweener = transform.DOLocalMove(new Vector3(0,0,transform.position.z),timeBeforeImpact).SetAutoKill(true).SetEase(Ease.InCubic);
        tweener.OnComplete(()=>{
            transform.DOLocalMove(new Vector3(-x,y,transform.position.z),timeBeforeImpact);
        });
    }

    void Update()
    {
        
    }
}
