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
    [SerializeField]
    private float maxX,maxY,minX,minY;
    private float factorX = 1.8f,factorY = 1f;
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
        maxX = 98 - factorX * gameObject.GetComponent<Camera>().orthographicSize;
        maxY = 35.4f - factorY * gameObject.GetComponent<Camera>().orthographicSize;
        minX = -9.9f + factorX * gameObject.GetComponent<Camera>().orthographicSize;
        minY = -30.5f + factorY * gameObject.GetComponent<Camera>().orthographicSize;
        float newX = Mathf.Min(Mathf.Max(followTarget.transform.position.x + 1,minX),maxX);
        float newY = Mathf.Min(Mathf.Max(followTarget.transform.position.y + 1,minY),maxY);
        this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(newX,newY,transform.position.z), Time.deltaTime * followSpeed);
    }
}
