using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

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
        followSpeed = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(followTarget.transform.position.x + 1, followTarget.transform.position.y - 1, this.transform.position.z), Time.deltaTime * followSpeed);
    }
}
