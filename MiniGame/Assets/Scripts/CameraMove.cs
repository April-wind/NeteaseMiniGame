using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //跟随物体
    public GameObject followTarget;

    //运动速度
    [SerializeField]
    private float followSpeed;
    // Start is called before the first frame update
    void Start()
    {
        followSpeed = 20.0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        float newX = Mathf.Min(Mathf.Max(followTarget.transform.position.x + 1,0),88.1f);
        float newY = Mathf.Min(Mathf.Max(followTarget.transform.position.y + 1,-24.9f),29.8f);
        this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(newX,newY,transform.position.z), Time.deltaTime * followSpeed);
    }
}
