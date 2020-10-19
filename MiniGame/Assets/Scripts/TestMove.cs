using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    [SerializeField]
    public Vector3 moveDir;

    [SerializeField]
    private float moveSpeed;
    //运动目标
    private Vector3 target;

    //blood,用于计算血量的削减
    public float bloodChange;
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        moveDir = new Vector3(h, v, 0);
        target = transform.position + moveSpeed * moveDir;
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Monster")
        {
            bloodChange = 40;
            Debug.Log("你扣除了40点血");
        }
        if(collision.tag == "SnakeCost")
        {
            bloodChange = 20;
            Debug.Log("你扣除了20点血");
        }
        //猫头鹰扣血
    }
}
