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
        x = transform.localPosition.x;
        y = transform.localPosition.y;
        tweener = transform.DOLocalMove(new Vector3(0,0,transform.localPosition.z),timeBeforeImpact).SetAutoKill(true).SetEase(Ease.InCubic);
        tweener.OnComplete(()=>{
            transform.DOLocalMove(new Vector3(-x - 40,y + 40,transform.localPosition.z),timeBeforeImpact);
        });
    }

    void Update()
    {
        
    }
}
